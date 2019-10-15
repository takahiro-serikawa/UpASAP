// DHCP intercept - dummy DHCP server

// usage:
// var dhcp = new SubDhcp(IPAddress.Any, callback)
//
// dhcp.Callback += callback
//
// void callback(object sender, DhcpEventArgs args)
// args.op: DISCOVER, OFFER, REQUEST, PACK ...
// args.mac: ex. FC-F1-CD-00-70-2D
// args.ip: ex. 192.168.100.90
// args.class_id: 'udhcp 1.24.1 ...'
//
// dhcp.MacFilter.Add("FC-F1-CD-")

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace UpASAP
{
    // DHCP event
    public class DhcpEventArgs: EventArgs
    {
        public DORA op;
        public IPAddress ip;
        public PhysicalAddress mac;
        public string class_id;

        public DhcpEventArgs() { }
    }

    public enum DORA
    {
        NOP = 0,
        DISCOVER = 1,
        OFFER = 2,
        REQUEST = 3,
        DECLINE = 4,
        PACK = 5,
        PNAK = 6,
        RELEASE = 7,
        INFRM = 8
    }

    public class SubDhcp
    {
        public SubDhcp(IPAddress ip = null, DhcpEventHandler callback = null)
        {
            MacFilter = new MacList();
            Callback = callback;
            try {
                InitDhcp(ip != null ? ip : IPAddress.Any);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        public delegate void DhcpEventHandler(object sender, DhcpEventArgs e);
        public event DhcpEventHandler Callback;

        // MAC filter
        public class MacList : List<string>
        {
            public MacList() { }

            public new void Add(string s)
            {
                base.Add(s.Replace(":", "").Replace("-", ""));
            }

            public new bool Contains(string mac)
            {
                return this.Any(s => mac.StartsWith(s));
            }

            public bool Contains(PhysicalAddress mac)
            {
                return Contains(mac.ToString());
            }
        }

        public MacList MacFilter { get; private set; }

        // DHCP server thread
        const int DHCP_SERV_PORT = 67;
        const int DHCP_CLI_PORT = 68;

        bool stop_req = false;

        public void Abort()
        {
            stop_req = true;
        }

        void InitDhcp(IPAddress ip)
        {
            var serv = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            serv.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            serv.EnableBroadcast = true;
            //dhcp.ReceiveTimeout = 10;
            serv.Bind(new IPEndPoint(ip, DHCP_SERV_PORT));

            var task = Task.Factory.StartNew(() => {
                for (stop_req = false; !stop_req; )
                    try {
                        if (serv.Available > 0) {
                            EndPoint from = new IPEndPoint(IPAddress.Any, 0);
                            byte[] buffer = new byte[1500];
                            int rlen = serv.ReceiveFrom(buffer, ref from);
                            ParseDhcp(buffer, rlen);
                        } else
                            System.Threading.Thread.Sleep(10);
                    } catch (Exception ex) {
                        Console.WriteLine("dhcp: " + ex.Message);
                    }

                Console.WriteLine("dhcp task quit normally");
            });
        }

        int ParseDhcp(byte[] buffer, int rlen)
        {
            if (rlen < 300-60)
                return -1;

            // 0x63825363
            int j = 300-64;
            if (buffer[j] != 0x63 || buffer[j+1] != 0x82 || buffer[j+2] != 0x53 || buffer[j+3] != 0x63)
                return -1;

            byte[] tmp6 = new byte[6];
            Buffer.BlockCopy(buffer, 28, tmp6, 0, 6);
            var mac = new PhysicalAddress(tmp6);
            if (MacFilter.Count > 0 && !MacFilter.Contains(mac))
                return 0;

            var e = new DhcpEventArgs();
            e.mac = mac;
            for (j += 4; j < buffer.Length && j < rlen; ) {
                byte op = buffer[j++];
                byte len = buffer[j++];
                switch (op) {
                case 0xFF:
                    goto chunk_end;
                case 0x35:  // 53 DHCP Msg Type	1 DHCPメッセージ・タイプ
                    e.op = (DORA)buffer[j];
                    break;
                case 0x3D:  // 61 Client Id	可変 クライアントIdentifier
                    if (buffer[j] == 0x01) {
                        Buffer.BlockCopy(buffer, j+1, tmp6, 0, 6);
                        e.mac = new PhysicalAddress(tmp6);
                    } else {
                        Buffer.BlockCopy(buffer, j, tmp6, 0, 6);
                        string tmp = BitConverter.ToString(tmp6, 6, 6);
                        Console.WriteLine("unknown Client Id type {0}, len={1}, {2}", buffer[j], len, tmp);
                    }
                    break;
                case 0x3C:  // 60 Class Id	使用するクラス名 e.g. 'udhcp 1.21.1'
                    e.class_id = Encoding.UTF8.GetString(buffer, j, len);
                    break;
                case 0x32: // 50 Address Request
                    byte[] tmp4 = new byte[4];
                    Buffer.BlockCopy(buffer, j, tmp4, 0, 4);
                    e.ip = new IPAddress(tmp4);
                    break;
                }
                j += len;
            }

        chunk_end:
            if (Callback != null)
                Callback(this, e);
            return 1;
        }

    }
}
