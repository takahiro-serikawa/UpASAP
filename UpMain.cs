// build options
#define MAC_ARP     // convert MAC address to IP by ARP table/
#define SHMD_UDP    // CDX/OD5000 Seeker by SHMD
#define GVS_NET_UDP // seek GVS sensors on local net by gvs-net-udp
#define GVS_SEEKER_SSH     // import SSH settings from GVS Seeker
#define GVS_MONITOR // intercept GVS GetNetworkMAC (GVS monitor seeker)

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using Renci.SshNet;

namespace UpASAP
{
    public partial class UpMain: Form
    {
        const string UP_ASAP = "UpASAP";
        const string APP_REG_KEY = @"Software\UpASAP";
        const string APP_EXT = "webi";

        // vendor spec.
        const string FASTUS_VENDOR_CODE = "FC-F1-CD-";
        const int GVS_UDP_PORT = 5700;
        const int GetNetworkMAC = 0xD8;
        const int GetNetwork = 0x58;
        const int SHMD_UDP_PORT = 5011;
        const byte SHMD_MEM_READ = 0x30;

        string app_title;

        public UpMain()
        {
            InitializeComponent();

            app_title = this.Text;
            log = new AppLog0(richTextBox1);
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            var ver = asm.GetName().Version;
            log.WriteLine(Color.Blue, "/* {0} ver{1}.{2:D2} - {3} */", UP_ASAP, ver.Major, ver.Minor, "SFTP instant uploader");
            //Greeting();

            ParseArgs(Environment.GetCommandLineArgs());

            //WinMisc.RegisterExt(APP_EXT, "UpASAP settings file (XML)");

            RestoreBrowsers();
            term = new TermEntry();
            TerminalButton.Image = TerminalItem.Image = TryGetIconImage(term.Exe);
            editor = new ExecEntry("editor_");
            EditorButton.Image = EditorItem.Image = TryGetIconImage(editor.Exe);
            ExplorerItem.Image = TryGetIconImage(null);
            mac_filters.Add(FASTUS_VENDOR_CODE);
            //mac_filters.Add("*");
            
#if GVS_MONITOR
            MoniInit();
#endif
            // restore last settings
            if (File.Exists(sett_filename))
                RestoreAppSettings(sett_filename);

            hiddenMenuStrip.SendToBack();
            this.ActiveControl = AutoCheck;

            var dhcp = new SubDhcp(IPAddress.Any, DhcpCallback);
            dhcp.MacFilter.Add("FC-F1-CD-");
        }

        void DhcpCallback(object sender, DhcpEventArgs e)
        {
            string mac = BitConverter.ToString(e.mac.GetAddressBytes());
            log.WriteLine(Color.Green, "{0} {1} {2} class id: {3}",
               mac, Enum.GetName(typeof(DORA), e.op), e.ip, e.class_id);
        }

        private void UpMain_Activated(object sender, EventArgs e)
        {
            // got focus effect
            linkText1.ForeColor = (linkText1.LinkVisited) ? linkText1.VisitedLinkColor : linkText1.LinkColor;
        }

        private void UpMain_Deactivate(object sender, EventArgs e)
        {
            // lost focus effect
            linkText1.ForeColor = Color.Gray;

            if (richTextBox1.Focused)
                this.ActiveControl = AutoCheck;
        }

        private void UpMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && AutoCheck.Checked)
                if (MessageBox.Show("under watching.\r\nquit now?", UP_ASAP + ": quitting",
                     MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                 == DialogResult.Cancel)
                    e.Cancel = true;
        }

        private void UpMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sett_modified) {
                if (sett_filename == "")
                    SaveAs_Click(null, null);
                else
                    SaveAppSettings(sett_filename);
            }
        }

        // message log
        static AppLog0 log;

        public static void AppException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            log.WriteLine(Color.Fuchsia, e.Exception.Message);
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            linkText1.Text = e.LinkText;
        }

        // AppLog - 
        // initilaize
        // AppLog log = new AppLog(RichTextBox rich)
        class AppLog0: IDisposable
        {
            RichTextBox rich1 = null, rich2 = null;
            Timer timer;
            StreamWriter logfile = null;
            const int LOG_LINES = 100;
            string log_time_fmt = "HH:mm:ss.fff ";
            int log_index = 0, log_index0 = -1;

            public AppLog0(RichTextBox rich)
            {
                rich1 = rich;

                rich2 = new RichTextBox();
                rich2.Font = rich1.Font;
                rich2.ForeColor = rich1.ForeColor;

                timer = new Timer();
                timer.Interval = 10;
                timer.Enabled = true;
                timer.Tick += _timer_Tick;
#if DEBUG
                string logname = Path.ChangeExtension(Application.ExecutablePath, ".log");
                logfile = new StreamWriter(logname, true);
#endif
            }

            public void Clear()
            {
                rich1.Clear();
                rich2.Clear();
                log_index = rich1.Text.Length;
                log_index0 = -1;
            }

            public void Dispose()
            {
                if (logfile != null) {
                    logfile.Dispose();
                    logfile = null;
                }

                rich1 = null;
                rich2.Dispose();

                timer.Dispose();
            }

            public void WriteLine(string fmt, params object[] args) { WriteLine(Color.Black, fmt, args); }

            public void WriteLine(Color color, string fmt, params object[] args)
            {
                if (rich1.InvokeRequired) {
                    rich1.BeginInvoke((MethodInvoker)(() => WriteLine(color, fmt, args)));
                } else {
                    string time = DateTime.Now.ToString(log_time_fmt);
                    string text = string.Format(fmt, args);

                    if (logfile != null) {
                        logfile.WriteLine(text);
                    }

                    // delete old lines
                    if (rich2.Lines.Length >= LOG_LINES) {
                        int n = rich2.Lines.Length - LOG_LINES + 1;
                        int index = 0;
                        for (int i = 0; i < n; i++)
                            index = rich2.Text.IndexOf('\n', index + 1);
                        rich2.Select(0, index + 1);
                        rich2.SelectedText = "";
                    }

                    // change inner RichTextBox
                    rich2.Select(rich2.Text.Length, 0);
                    //rich2.SelectionFont = rich2.Font;
                    rich2.SelectionColor = FadedColor;
                    rich2.SelectedText = time;
                    rich2.SelectionColor = color;
                    rich2.SelectedText = text;
                    //rich2.SelectionColor = Color.White;
                    rich2.SelectedText = "\n";

                    // copy inner to visible RichTextBox 
                    rich1.Rtf = rich2.Rtf;
                    rich1.ClearUndo();

                    // scroll to new line
                    log_index0 = log_index;
                    log_index = rich1.Text.Length;
                    rich1.Select(log_index, 0);
                    rich1.ScrollToCaret();
                }
            }

            const int EMPHASIS_MSEC = 2500;
            readonly Color EmphasisColor = Color.Aqua;
            readonly Color FadedColor = Color.Black;
            readonly Color TickColor = Color.Teal;

            void _timer_Tick(object sender, EventArgs e)
            {
                if (rich1 != null && !rich1.Focused) {
                    var now = DateTime.Now;
                    string time = now.ToString(log_time_fmt);

                    for (int i = rich1.Text.LastIndexOf("\n"); i > 0; ) {
                        int j = rich1.Text.LastIndexOf("\n", --i);
                        if (j < 0 || j+1 >= rich1.Text.Length)
                            break;
                        string t = rich1.Text.Substring(j+1, time.Length);
                        TimeSpan ts = now - DateTime.Parse(t);
                        if (ts.TotalMilliseconds < 0 || ts.TotalMilliseconds >= EMPHASIS_MSEC)
                            break;

                        float ratio = (float)ts.TotalMilliseconds/EMPHASIS_MSEC;
                        ratio = 3 * ratio - 1f;
                        rich1.Select(j+1, t.Length);
                        rich1.SelectionColor = MixColor(FadedColor, ratio, EmphasisColor, 1f - ratio);

                        i = j;
                    }

                    rich1.Select(log_index, 100);
                    rich1.SelectionColor = TickColor;
                    rich1.SelectedText = time;
                }
            }

            public void Flash(int n)
            {
                for (int i = 0; i < n; i++) {
                    rich1.BackColor = Color.Silver;
                    rich1.Refresh();
                    System.Threading.Thread.Sleep(70);

                    rich1.BackColor = Color.White;
                    rich1.Refresh();
                    System.Threading.Thread.Sleep(150);
                }
            }
        }

        void Greeting()
        {
            int pos = richTextBox1.Text.Length;
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            var ver = asm.GetName().Version;
            log.WriteLine(Color.Blue, "/* {0} ver{1}.{2:D2} - {3}", UP_ASAP, ver.Major, ver.Minor, "SFTP instant uploader");
            log.WriteLine(Color.Blue, " * © 2019 Takahiro. Serikawa");
            log.WriteLine(Color.Blue, " * SSH.NET © 2012-2017, RENCI");
            // more license(s) ...
            log.WriteLine(Color.Blue, " */");
            richTextBox1.SelectionStart = pos;
            richTextBox1.ScrollToCaret();
            log.Flash(3);
        }

        void ParseArgs(string[] aa)
        {
            sett_modified = false;
            using (var reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(APP_REG_KEY))
                sett_filename = (string)reg.GetValue("last_filename", "");
            if (sett_filename == "") {
                //sett_filename = Path.ChangeExtension(Application.ExecutablePath, APP_EXT);
                string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                sett_filename = Path.Combine(path, UP_ASAP+"."+APP_EXT);
            }

            bool auto_flag = true;
            for (int i = 1; i < aa.Length; i++)
                if (aa[i].Contains('@')) {
                    // user@host/dir
                    string[] ss = aa[i].Split('@');
                    if (ss[0] != "")
                        User.Text = ss[0];
                    if (ss[1] != "") {
                        int k = ss[1].IndexOf('/');
                        if (k > 0) {
                            RemoteHost.Text = ss[1].Substring(0, k);
                            RemoteRoot.Text = ss[1].Substring(k);
                        } else
                            RemoteHost.Text = ss[1];
                    }
                } else if (aa[i][0] == '-') {
                    switch (aa[i]) {
                    case "-H":
                    case "--host":
                    case "--ip":
                        RemoteHost.Text = NextArg(aa, ref i);
                        break;
                    case "-U":
                    case "--user":
                        User.Text = NextArg(aa, ref i);
                        break;
                    case "-P":
                    case "--pass":
                        Pass.Text = NextArg(aa, ref i);
                        break;
                    case "-R":
                    case "--remote":
                        RemoteRoot.Text = NextArg(aa, ref i);
                        break;
                    case "-L":
                    case "--local":
                        LocalRoot.Text = Path.GetFullPath(NextArg(aa, ref i));
                        break;
                    //case "-A":
                    //case "--auto":
                    //    auto_flag = true;
                    //    break;

                    default:
                        Console.WriteLine("unknown option {0}", aa[i]);
                        break;
                    }
                } else
                    sett_filename = aa[i];

            if (sett_modified) {
                if (auto_flag)
                    AutoCheck.Checked = true;   // start after other settings

                // do not restore when some value is specified
                sett_filename = "";
                sett_modified = false;
            }
        }

        string NextArg(string[] aa, ref int i)
        {
            if (++i < aa.Length)
                return aa[i];
            return "";
        }

        // menu/button handlers
        private void AboutItem_Click(object sender, EventArgs e)
        {
            Greeting();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            AutoCheck.Checked = !AutoCheck.Checked;
        }

        private void Manual_Click(object sender, EventArgs e)
        {
            FileInfo[] files;
            if (Directory.Exists(LocalRoot.Text)) {
                var di = new DirectoryInfo(LocalRoot.Text);
                files = di.GetFiles("*.*", SearchOption.AllDirectories);
            }/* else if (File.Exists(LocalRoot.Text))
                files = new FileInfo[] { new FileInfo(LocalRoot.Text) };*/
            else
                return;

            SftpWithComfirm(files);
        }

        private void RemoteRoot_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void RemoteRoot_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            //SftpWithComfirm(files[0]);
        }

        private void Explorer_Click(object sender, EventArgs e)
        {
            ProcessStart("EXPLORER.EXE", "${local}");
        }

        private void NewItem_Click(object sender, EventArgs e)
        {
            if (sett_modified && sett_filename != "")
                SaveAppSettings(sett_filename);
            
            LocalRoot.Text = "";
            RemoteHost.Text = "";
            RemoteRoot.Text = "";
            linkText1.Text = @"http://hostname/index.html";
            AutoCheck.Checked = false;

            SetAppFilename("");
            sett_modified = false;

            log.WriteLine("new settings");
        }

        private void Open_Click(object sender, EventArgs e)
        {
            if (sett_modified && sett_filename != "")
                SaveAppSettings(sett_filename);

            if (sett_filename != "") {
                openFileDialog1.InitialDirectory = Path.GetDirectoryName(sett_filename);
                openFileDialog1.FileName = Path.GetFileName(sett_filename);
            }
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                SftpClose();
                log.WriteLine("load settings \"{0}\"", openFileDialog1.FileName);
                RestoreAppSettings(openFileDialog1.FileName);
            }
        }

        private void SaveAs_Click(object sender, EventArgs e)
        {
            if (sett_filename != "") {
                saveFileDialog1.InitialDirectory = Path.GetDirectoryName(sett_filename);
                saveFileDialog1.FileName = Path.GetFileName(sett_filename);
            }

            if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                SaveAppSettings(saveFileDialog1.FileName);
                log.WriteLine("save settings to \"{0}\"", saveFileDialog1.FileName);
            }
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearLogItem_Click(object sender, EventArgs e)
        {
            log.Clear();
            log.WriteLine("log clear");
        }

        private void selectLocalItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = LocalRoot.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                LocalRoot.Text = folderBrowserDialog1.SelectedPath;
        }

        UriBuilder uri = null;

        void RefreshURL(string host, string path)
        {
            try { //?ex
                uri = new UriBuilder(linkText1.Text);
            } catch (Exception) {
                uri = new UriBuilder("http", last_host);
            }

            if (host != null)
                uri.Host = host;
            if (path != null)
                uri.Path = path;

            string s = uri.ToString();
            if (uri.Scheme == "http" && uri.Port == 80 && linkText1.Text.IndexOf(":80/") < 0)
                linkText1.Text = s.Replace(":80/", "/");
            else
                linkText1.Text = s;
        }

        private void EditUrl_Click(object sender, EventArgs e)
        {
            //linkText1.SelectAll();
            //linkText1.Select(0, 0);
            linkText1.Focus();
        }

        private void linkText1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("UniformResourceLocator")
             || e.Data.GetDataPresent("UniformResourceLocatorW"))
                e.Effect = DragDropEffects.Link;
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void linkText1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("UniformResourceLocator")
             || e.Data.GetDataPresent("UniformResourceLocatorW")) {
                linkText1.Text = e.Data.GetData(DataFormats.UnicodeText).ToString();
                try {
                    uri = new UriBuilder(linkText1.Text);
                } catch (Exception) {
                    uri = new UriBuilder("http", last_host);
                }
            } else if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                string rel = filenames[0];
                if (rel.StartsWith(LocalRoot.Text))
                    rel = rel.Substring(LocalRoot.Text.Length);
                uri.Path = rel.Replace('\\', '/');
                linkText1.Text = uri.ToString().Replace(":80/", "/");
            }
        }

        ExecEntry editor;

        string DefOpt(string o, string def)
        {
            if (o == null || o == "")
                return def;
            return o;
        }

        private void Editor_Click(object sender, EventArgs e)
        {
            // let the user choose, if no setting
            if (editor.Exe == "")
                EditorSettingsItem_Click(null, null);
            if (editor.Exe == "")
                return;

            // execute application

            // ?暫定
            if (linkText1.Modified)
                try {   //?ex
                    uri = new UriBuilder(linkText1.Text);
                } catch (Exception) {
                    uri = new UriBuilder("http", last_host);
                }
          
            ProcessStart(editor.Exe, DefOpt(editor.Options, DefOpt(editor.Options, "${fullpath}")));
        }

        private void EditorSettingsItem_Click(object sender, EventArgs e)
        {
            var dialog = new ExecDialog(ExecType.HTML_EDITOR);
            dialog.ExecCallback = ProcessStart;
            if (dialog.Popup(editor.Name, editor.Exe, editor.Options) != DialogResult.Cancel) {
                editor.Name = dialog.AppName;
                editor.Exe = dialog.Exe;
                editor.Options = dialog.Options;
                editor.Save();

                EditorButton.Image = EditorItem.Image = TryGetIconImage(editor.Exe);
            }
        }

        private void CopyUrl_Click(object sender, EventArgs e)
        {
            //GetRemoteHost();    // update URL
            Clipboard.SetText(linkText1.Text);
            log.WriteLine("clipboard set to {0}", linkText1.Text);
        }

        private void Browser_Click(object sender, EventArgs e)
        {
            //GetRemoteHost();    // update URL
            System.Diagnostics.Process.Start(linkText1.Text);
            linkText1.LinkVisited = true;
            log.WriteLine("open {0}", linkText1.Text);
        }

        // 外部アプリケーションの実行
        class ExecEntry
        {
            public string Name { get; set; }
            public string Exe { get; set; }
            public string Options { get; set; }

            protected string reg_prefix;

            public ExecEntry(string reg_prefix)
            {
                this.reg_prefix = reg_prefix;
                Restore();
            }

            public void Save()
            {
                try {
                    using (var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(APP_REG_KEY)) {
                        key.SetValue(reg_prefix + "name", this.Name);
                        key.SetValue(reg_prefix + "exe", this.Exe);
                        key.SetValue(reg_prefix + "options", this.Options);
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            }

            public virtual void Restore()
            {
                try {
                    using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(APP_REG_KEY)) {
                        this.Name = (string)key.GetValue(reg_prefix + "name", "");
                        this.Exe = (string)key.GetValue(reg_prefix + "exe", "");
                        this.Options = (string)key.GetValue(reg_prefix + "options", "");
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            }
        }


        #region 【SSH terminal login】
        // SSH terminal login 

        class TermEntry: ExecEntry
        {
            public TermEntry() : base("ssh_") { }

            public override void Restore()
            {
                base.Restore();

#if GVS_SEEKER_SSH  // GVS Seeker compatibility; first time only
                if (this.Exe == "")
                    try                     {
                        using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\OptexFA\GvsIcon")) {
                            //this.Name = (string)key.GetValue("ssh_name", "");
                            this.Exe = (string)key.GetValue("ssh_exe", "");
                            this.Options = (string)key.GetValue("ssh_options", "");
                        }

                        var vi = System.Diagnostics.FileVersionInfo.GetVersionInfo(this.Exe);
                        if (vi.ProductName != null)
                            this.Name = vi.ProductName;
                        else if (vi.FileDescription != null)
                            this.Name = vi.FileDescription;

                        string o = this.Options;
                        o = o.Replace(" /user root ", " /user ${user} ");   // RLogin
                        o = o.Replace(" /pass root ", " /pass ${pass} ");
                        o = o.Replace(" -pw root ", " -pw ${pass} ");   // putty
                        o = o.Replace(" root@", " ${user}@");
                        o = o.Replace(" /user=root ", " /user=${user} ");   // tera term
                        o = o.Replace(" /passwd=root ", " /passwd=${pass} ");
                        this.Options = o;

                        this.Save();
                     } catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
#endif
            }
        }


        /* SSH terminal application */
        TermEntry term;

        private void Terminal_Click(object sender, EventArgs e)
        {
            // let the user choose, if no setting
            if (term.Exe == "") {
                TerminalSettings_Click(null, null);
                if (term.Exe == "")
                    return;
            }

            // execute application
            ProcessStart(term.Exe, term.Options);
        }

        void ProcessStart(string exe, string options)
        {
            // parameter expansions; ${ip}, ${user}, ${pass}, ...
            Dictionary<string, string> queries = new Dictionary<string, string>();
            if (options.Contains("${ip}") || options.Contains("${host}")) {
                last_host = GetRemoteHost();
                queries.Add("host", last_host);
                queries.Add("ip", last_host);   // ${ip} is alias of ${host}
            }
            queries.Add("user", User.Text);
            queries.Add("url", linkText1.Text);
            queries.Add("dir", this.LocalRoot.Text);
            queries.Add("local", this.LocalRoot.Text);
            //queries.Add("rel", );
            if (uri != null) {
                string uname = uri.Path;
                if (uname[0] == '/')
                    uname = uname.Substring(1);
                
                // for editor only?
                if (uname == "")
                    uname = "index.html";
                
                string wpath = Path.Combine(LocalRoot.Text, uname);
                queries.Add("fullpath", wpath);
            }

            foreach (var q in queries)
                options = options.Replace("${" + q.Key + "}", q.Value);
            string options1 = options.Replace("${pass}", "****");   // hide password for log
            string options2 = options.Replace("${pass}", Pass.Text);

            //ex try {
                if (exe.EndsWith(":"))   // URL scheme; ex. "microsoft-edge:"
                    System.Diagnostics.Process.Start(exe + options2);
                else
                    System.Diagnostics.Process.Start(exe, options2);
                
                log.WriteLine(Color.Black, "open \"{0} {1}\"", Path.GetFileNameWithoutExtension(exe), options1);
            //ex } catch (Exception ex) {
            //ex     log.WriteLine(Color.Fuchsia, "exec: "+ex.Message);
            //ex }
        }

        private void TerminalSettings_Click(object sender, EventArgs e)
        {
            var dialog = new ExecDialog(ExecType.SSH_TERM);
            dialog.ExecCallback = ProcessStart;
            if (dialog.Popup(term.Name, term.Exe, term.Options) != DialogResult.Cancel) {
                term.Name = dialog.AppName;
                term.Exe = dialog.Exe;
                term.Options = dialog.Options;
                term.Save();

                TerminalButton.Image = TerminalItem.Image = TryGetIconImage(term.Exe);
            }
        }
        #endregion

        #region 【multi. browsers settings】
        // multi. browsers settings

        List<ExecEntry> blist = new List<ExecEntry>();

        public void RestoreBrowsers()
        {
            for (int no = 1; no <= 7; no++) {
                var b = new ExecEntry("browser"+no.ToString()+"_");
                blist.Add(b);

                Control[] buttons = this.Controls.Find("browser"+no.ToString(), true);
                if (buttons.Length <= 0)
                    continue;
                buttons[0].Tag = b;
                RefreshBrowser(buttons[0] as Button);
            }

            string defexe = GetDefaultBrowser();
            if (defexe != "") {
                var b1 = browser1.Tag as ExecEntry;
                if (b1.Exe == "") {
                    b1.Exe = defexe;
                    RefreshBrowser(browser1);
                    b1.Save();
                }

                BrowserItem.Image = TryGetIconImage(defexe);
            }
        }

        void RefreshBrowser(Button button)
        {
            var b = button.Tag as ExecEntry;
            if (b.Name == "" && b.Exe != "") {
                // get file description or product name as caption
                try {
                    var vi = System.Diagnostics.FileVersionInfo.GetVersionInfo(b.Exe);
                    if (b.Name == "" && vi.FileDescription != null)
                        b.Name = vi.FileDescription;
                    if (b.Name == "" && vi.ProductName != null)
                        b.Name = vi.ProductName;
                } catch (Exception) { }

                if (b.Name == "")
                    b.Name = Path.GetFileNameWithoutExtension(b.Exe);
            }

            if (b.Name != "")
                toolTip1.SetToolTip(button, b.Name);
            else
                toolTip1.SetToolTip(button, button.Name + " empty\r\ndrag and drop other browser here");

            button.Image = TryGetIconImage(b.Exe, true);
        }

        static string GetDefaultBrowser()
        {
            using (var key2 = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice")) {
                string progid = (string)key2.GetValue("Progid");
                using (var key = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(progid + @"\shell\open\command")) {
                    if (key == null)
                        return "";

                    string command = (string)key.GetValue(String.Empty);
                    if (command == null)
                        return "";

                    int k = command.IndexOf('"', 1);
                    if (command[0] == '"' && k > 1)
                        command = command.Substring(1, k-1);

                    return command;
                }
            }
        }

        private void browsers_Click(object sender, EventArgs e)
        {
            var b = (sender as Button).Tag as ExecEntry;
            if (b.Exe == "") {
                BrowserAddItem_Click(null, null);
                if (b.Exe == "")
                    return;
            }

            ProcessStart(b.Exe, DefOpt(b.Options, "${url}"));
            linkText1.LinkVisited = true;
        }

        private void hidden_Click(object sender, EventArgs e)
        {
            string no = sender.ToString();
            Control[] buttons = this.Controls.Find("browser"+no.ToString(), true);
            if (buttons.Length > 0)
                browsers_Click(buttons[0], null);
        }

        private void browsers_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
        }

        private void browsers_DragDrop(object sender, DragEventArgs e)
        {
            // drop to register new browser
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            var button = sender as Button;
            var b = button.Tag as ExecEntry;
            if (b.Exe != "") {
                string text = string.Format("{0} is not empty. replace it?", button.Name, b.Name);
                if (MessageBox.Show(text, UP_ASAP + ": browsers settings", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;
            }

            b.Exe = files[0];
            RefreshBrowser(button);
            b.Save();
        }
        
        private void BrowserAddItem_Click(object sender, EventArgs e)
        {
            var button = this.ActiveControl as Button;
            if (button == null || button.Tag == null)
                button = browser1;

            var b = button.Tag as ExecEntry;
            var dialog = new ExecDialog(ExecType.WEB_BROWSER);
            dialog.ExecCallback = ProcessStart;
            if (dialog.Popup(b.Name, b.Exe, b.Options) != DialogResult.Cancel) {
                b.Name = dialog.AppName;
                b.Exe = dialog.Exe;
                b.Options = dialog.Options;
                RefreshBrowser(button);
                b.Save();
            }
        }

        private void BrowserRemoveItem_Click(object sender, EventArgs e)
        {
            var button = this.ActiveControl as Button;
            if (button == null)
                button = browser1;
            var b = button.Tag as ExecEntry;
            if (b.Exe != "") {
                string text = string.Format("remove \"{1}\" of {0}", button.Name, b.Name);
                if (MessageBox.Show(text, UP_ASAP + ": remove browser", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;
            }

            b.Name = b.Exe = b.Options = "";
            RefreshBrowser(button);
            b.Save();
        }

        private void browsers_MouseDown(object sender, MouseEventArgs e)
        {
            (sender as Button).Focus();
        }
        #endregion


        // application settings file
        public class UpAsapSett
        {
            public string local_root;
            public string remote_host;
            public string remote_root;
            public bool auto_upload;
            public int top, left, width, height;

            public string link;
            public string user;
        }

        string sett_filename = "";
        bool sett_modified = false;

        void SetAppFilename(string filename)
        {
            if (sett_filename != filename) {
                sett_filename = filename;
                this.Text = Path.GetFileNameWithoutExtension(filename) + " - " + app_title;
                using (var reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(APP_REG_KEY))
                    reg.SetValue("last_filename", filename);
            }
        }

        void RestoreAppSettings(string filename)
        {
            SetAppFilename(filename);

            UpAsapSett data = null;
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(UpAsapSett));
            using (var sr = new StreamReader(filename)) {
                data = (UpAsapSett)serializer.Deserialize(sr);
            }

            LocalRoot.Text = data.local_root;
            RemoteHost.Text = data.remote_host;
            RemoteRoot.Text = data.remote_root;
            this.StartPosition = FormStartPosition.Manual;
            this.Top = data.top;
            this.Left = data.left;
            this.Width = data.width;
            this.Height = data.height;

            linkText1.Text = data.link;
            if (data.user != null)
                User.Text = data.user;

            try {//?ex
                AutoCheck.Checked = data.auto_upload;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            sett_modified = false;
        }

        void SaveAppSettings(string filename)
        {
            SetAppFilename(filename);

            var data = new UpAsapSett();
            data.local_root = LocalRoot.Text;
            data.remote_host = RemoteHost.Text;
            data.remote_root = RemoteRoot.Text;
            data.auto_upload = AutoCheck.Checked;
            if (this.WindowState == FormWindowState.Normal) {
                data.top = this.Top;
                data.left = this.Left;
                data.width = this.Width;
                data.height = this.Height;
            } else {
                data.top = this.RestoreBounds.Top;
                data.left = this.RestoreBounds.Left;
                data.width = this.RestoreBounds.Width;
                data.height = this.RestoreBounds.Height;
            }

            data.link = linkText1.Text;
            data.user = User.Text;

            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(UpAsapSett));
            using (var sw = new StreamWriter(filename, false)) {
                serializer.Serialize(sw, data);
            }

            sett_modified = false;
        }

        private void event_Changed(object sender, EventArgs e)
        {
            sett_modified = true;
        }

        // host information
        class HostInfo
        {
            string _mac;
            public string Mac
            {
                get { return _mac; }
                set
                {
                    _mac = value;
                    timestamp = DateTime.Now;
                }
            }

            string _addr, _name;
            public string Addr
            {
                get { return _addr; }
                set
                {
                    _addr = value;
                    timestamp = DateTime.Now;
                }
            }

            public string Name
            {
                get { return _name; }
                set
                {
                    _name = value;
                    timestamp = DateTime.Now;
                }
            }

            /*public */DateTime timestamp { get; set; }

            public bool IsActive { get; set; }

            public HostInfo(string attr, string addr, string name)
            {
                this.Attr = attr;
                this._addr = addr;
                this._name = name;
                this.timestamp = DateTime.Now;

                this.IsActive = true;
            }

            public HostInfo(string mac, string attr, string addr, string name)
            {
                this._mac = mac;
                this.Attr = attr;
                this._addr = addr;
                this._name = name;
                this.timestamp = DateTime.Now;

                this.IsActive = true;
            }

            public string Attr { get; set; }
            public bool HasAttr(char c) { return Attr.IndexOf(c) >= 0; }
            public void AddAttr(char c) { if (!HasAttr(c)) Attr += c; }
            public void RemoveAttr(char c) { Attr = Attr.Replace(c, '\0'); }
        }

        List<HostInfo> hosts = new List<HostInfo>();

        void UpdateHosts(string mac, string attr, string ip, string name)
        {
            var host = hosts.Find(h => h.Mac == mac);
            if (host == null)
                hosts.Add(new HostInfo(mac, attr, ip, name));
            else {
                if (attr != null && attr.Length > 0) host.AddAttr(attr[0]);
                if (ip != null) host.Addr = ip;
                if (name != null) host.Name = name;
                host.IsActive = true;
            }
        }

        private void RemoteHost_DropDown(object sender, EventArgs e)
        {
            Socket udp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udp.EnableBroadcast = true;

            // seek sensors by broadcast
#if GVS_NET_UDP
            {
                byte[] cmd = { 0x3E, GetNetwork, 0, 0 };    // GetNetworkMAC
                var broadcast = new IPEndPoint(IPAddress.Broadcast, GVS_UDP_PORT);
                udp.SendTo(cmd, cmd.Length, 0, broadcast);
            }
#endif
#if SHMD_UDP
            {
                byte[] cmd = { SHMD_MEM_READ, 6, 0,0,0,0x18, 0,6 };  // read MAC 6bytes
                var broadcast = new IPEndPoint(IPAddress.Broadcast, SHMD_UDP_PORT);
                udp.SendTo(cmd, cmd.Length, 0, broadcast);
            }
#endif
            for (int tc0 = Environment.TickCount; Environment.TickCount < tc0 + 10; )
                if (udp.Available > 0) {
                    byte[] reply = new byte[100];
                    EndPoint from = new IPEndPoint(IPAddress.Any, 0);
                    int rlen = udp.ReceiveFrom(reply, 100, 0, ref from);

                    // add IP address
                    var ip = (from as IPEndPoint).Address.ToString();
                    if (RemoteHost.Items.IndexOf(ip) < 0)
                        RemoteHost.Items.Add(ip);

#if GVS_NET_UDP
                    if (reply[0] == 0x3C && (reply[1] == GetNetworkMAC || reply[1] == GetNetwork)) {
                        // add MAC address
                        string mac = BitConverter.ToString(reply, 6, 6);
                        if (RemoteHost.Items.IndexOf(mac) < 0)
                            RemoteHost.Items.Add(mac);

                        // update host list
                        string name = (rlen > 28) ? Encoding.UTF8.GetString(reply, 28, rlen-28): null;
                        UpdateHosts(mac, "G", ip, name);
                    }
#endif
#if SHMD_UDP
                    if (reply[0] == 0xB0 && reply[1] == 6) {    // MEM_READ reply 6 bytes
                        // add MAC address
                        string mac = BitConverter.ToString(reply, 2, 6);
                        if (RemoteHost.Items.IndexOf(mac) < 0)
                            RemoteHost.Items.Add(mac);

                        // update host list
                        //string name = Encoding.UTF8.GetString(reply, 28, rlen-28);
                        UpdateHosts(mac, "S", ip, "");
                    }
#endif
                }

#if MAC_ARP
            GetArpA();
#endif
        }

        private void RemoteHost_SelectedIndexChanged(object sender, EventArgs e)
        {
            SftpClose();    // re-login later
        }

        string last_host = "";

        string GetRemoteHost()
        {
            if (client != null)
                try {//?ex
                    var attr = client.GetAttributes(".");
                    return last_host;
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                    SftpClose();
                }

            last_host = RemoteHost.Text;
            if (last_host.Contains('-') || last_host.Contains(':'))    // maybe MAC
                try {//?ex
                    //GetArpA();
                    RemoteHost_DropDown(null, null);

                    string mac = last_host.ToUpper().Replace(':', '-');
                    var host = hosts.Find(h => h.Mac == mac);
                    if (host != null && last_host != host.Addr) {
                        last_host = host.Addr;
                        log.WriteLine("MAC: {0} turned out to be IP: {1}", RemoteHost.Text, last_host);
                    }
                } catch (Exception ex) {
                    log.WriteLine(Color.Fuchsia, "MAC address not found; " + ex.Message);
                }

            RefreshURL(last_host, null);
            return last_host;
        }

        #region 【SFTP support】
        // SFTP support
        SftpClient client = null;
        int passwd_valid = 0;

        void SftpConnect()
        {
            Cursor.Current = Cursors.WaitCursor;
            int tc0 = Environment.TickCount;

            // https://yoshinorin.net/2016/12/08/csharp-sftp/
            //秘密鍵使う場合
            //例としてユーザーディレクトリ配下の.sshディレクトリ内のsecret_key.pemを秘密鍵として使う
            //string secretKeyPath = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".ssh", "secret_key.pem");
            //var authMethod = new PrivateKeyAuthenticationMethod("username", new PrivateKeyFile(secretKeyPath, "finger_print"));

            string user = User.Text;
            string pass = Pass.Text;

            var authMethod = new PasswordAuthenticationMethod(user, pass);
            last_host = GetRemoteHost();
            var connectionInfo = new ConnectionInfo(last_host, 22, user, authMethod);
            client = new SftpClient(connectionInfo);
            client.ErrorOccurred += SftpErrorOccurred;
            client.OperationTimeout = new TimeSpan(0, 0, 3);
            try {//?ex
                client.Connect();
                log.WriteLine("SFTP login to {0} as {1}; {2}msec", last_host, user, Environment.TickCount-tc0);
                passwd_valid = 1;

                client.ChangeDirectory(RemoteRoot.Text);

                SftpLabel.Font = new Font(SftpLabel.Font, FontStyle.Bold);
                SftpLabel.ForeColor = this.ForeColor;

                //CurrentHost.IsActive = true;
            } catch (Exception ex) {
                if (ex is Renci.SshNet.Common.SshAuthenticationException) {
                    log.WriteLine(Color.Fuchsia, "SFTP login failed; " + ex.Message);
                    passwd_valid = -1;
                } else if (ex is SocketException && (ex as SocketException).ErrorCode == 11001) {
                    log.WriteLine(Color.Fuchsia, "SFTP login failed; host not found {0}", last_host);
                } else if (ex is Renci.SshNet.Common.SftpPathNotFoundException) {
                    log.WriteLine(Color.Fuchsia, "SFTP login failed; no such directory {0}", RemoteRoot.Text);
                } else if (ex is Renci.SshNet.Common.SshOperationTimeoutException) {
                    log.WriteLine(Color.Fuchsia, "SFTP login failed; timeout {0}", RemoteRoot.Text);
                } else // unknown error
                    log.WriteLine(Color.Fuchsia, "SFTP login failed; " + ex.Message);
                SftpClose();
                SftpLabel.ForeColor = Color.Fuchsia;

                //CurrentHost.IsActive = false;
                throw;
            }
        }

        void SftpErrorOccurred(object sender, EventArgs e)
        {
            Console.WriteLine("SftpErrorOccurred:"+e.ToString());
        }

        void SftpClose()
        {
            if (client != null) {
                client.Disconnect();
                client.Dispose();
                client = null;
            }
            SftpLabel.Font = new Font(SftpLabel.Font, FontStyle.Regular);
            SftpLabel.ForeColor = Color.Gray;
        }

        long SftpUploadFile(string uname)
        {
            //if (client == null)
            //    SftpConnect();

            Cursor.Current = Cursors.WaitCursor;
            string last_err = "";
            string wpath = Path.Combine(LocalRoot.Text, uname);
            for (int retry = 0; retry < 3; retry++)
                try {//?ex
                    if (client == null)
                        SftpConnect();

                    int tc0 = Environment.TickCount;
                    SftpMakeDirectories(uname);

                    long length = 0;
                    using (var fs = new FileStream(wpath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                        client.UploadFile(fs, uname, true);
                        length = fs.Length;
                    }

                    var attr = client.GetAttributes(uname);
                    attr.LastWriteTime = File.GetLastWriteTime(wpath);
                    client.SetAttributes(uname, attr);

                    float nbytes = length;
                    string unit = ByteUnit(ref nbytes);
                    last_tc = Environment.TickCount;
                    log.WriteLine("SFTP upload \"{0}\": {1:0.##}{2}, {3}msec", uname, nbytes, unit, last_tc-tc0);

                    RefreshURL(null, uname);
                    return length;                        // SUCCEED; return copy length as bytes

                } catch (Renci.SshNet.Common.SshException ex) {
                    log.WriteLine(Color.Fuchsia, ex.Message);
                    last_err = ex.Message;
                    SftpClose();
                } catch (IOException ex) {
                    // maybe file is busy, upload later
                    last_err = ex.Message;
                    System.Threading.Thread.Sleep(300);
                } catch (Exception ex) {
                    last_err = ex.Message;
                    Console.WriteLine(ex.Message);
                }

            throw new Exception(last_err);
        }

        DateTime refDate;// = DateTime.Now;

        long SftpWithComfirm(FileInfo[] files)
        {
            if (client == null)
                SftpConnect();

            this.Cursor = Cursors.WaitCursor;
            progress = 0;
            ProgressBar.Invalidate();

            float upload_bytes = 0;
            int upload_count = 0;

            var udlg = new UploadDialog();
            DialogResult confirm = DialogResult.Yes;

            int msec = 0;
            string last_dir = "";
            for (int n = 0; n < files.Count(); n++) {
                progress = (float)n/files.Count();
                ProgressBar.Invalidate();

                var f = files[n];
                string winname = GetRelativePath(LocalRoot.Text, f.FullName);
                string uname = winname.Replace('\\', '/');

                if (skip_invisible
                 && (f.Attributes.HasFlag(FileAttributes.Hidden) ||  uname[0] == '.' || uname.Contains("/."))) {
                    Console.WriteLine("skip {0}", winname);
                    continue;
                }

                bool modified = false;
                if (!client.Exists(uname)) {
                    modified |= f.LastWriteTime >= refDate;
                    //Console.WriteLine("new file {0}", uname);
                } else
                    try {//?ex
                        var attr = client.GetAttributes(uname);
                        TimeSpan ts = f.LastWriteTime - attr.LastWriteTime;
                        if ((-1 >= ts.Seconds || ts.Seconds >= 1)
                         && f.LastWriteTime >= refDate)
                            modified = true;

                        if (f.Length != attr.Size)
                            modified = true;
                    } catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                        //continue;
                    }

                if (modified) {
                    if (confirm != DialogResult.OK) {   // OK is ALL
                        udlg.FileName = uname;
                        confirm = udlg.ShowDialog();
                        this.Cursor = Cursors.WaitCursor;
                    }
                    switch (confirm) {
                    case DialogResult.No:
                        continue;
                    case DialogResult.Abort:    // [Cancel]
                        goto abort;
                    }

                    string dir = Path.GetDirectoryName(uname);
                    if (last_dir != dir) {
                        last_dir = dir;
                        SftpMakeDirectories(uname);
                    }

                    using (var fs = new FileStream(f.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                        int tc1 = Environment.TickCount;
                        client.UploadFile(fs, uname, true);

                        var attr = client.GetAttributes(uname);
                        attr.LastWriteTime = f.LastWriteTime;
                        client.SetAttributes(uname, attr);

                        upload_bytes += fs.Length;
                        upload_count++;
                        msec += Environment.TickCount - tc1;
                    }
                }
            }

        abort:
            string unit = ByteUnit(ref upload_bytes);
            log.WriteLine("upload {0}/{1}files {2}{3:0.##}; {4:F3}sec", 
               upload_count, files.Count(), upload_bytes, unit, msec/1000f);
            this.Cursor = Cursors.Default;

            //refDate = DateTime.Now;
            
            return 0;
        }

        float progress = -1f;

        private void ProgressBar_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            if (progress >= 0) {
                g.Clear(Color.Black);
                g.FillRectangle(Brushes.Lime, 1f, 1f, (ProgressBar.Width-2)*progress, ProgressBar.Height-2);
            }
        }

        [DllImport("shlwapi.dll", CharSet = CharSet.Auto)]
        private static extern bool PathRelativePathTo(
             [Out] StringBuilder pszPath,
             [In] string pszFrom,
             [In] System.IO.FileAttributes dwAttrFrom,
             [In] string pszTo,
             [In] System.IO.FileAttributes dwAttrTo
        );

        public static string GetRelativePath(string root, string path)
        {
            var sb = new StringBuilder(260);
            if (!PathRelativePathTo(sb, root, FileAttributes.Directory, path, FileAttributes.Normal))
                throw new Exception("ERROR PathRelativePathTo");
            string s = sb.ToString();
            if (s.StartsWith(".\\"))
                return s.Substring(2);
            return s;
        }

        // create directory with sub directories
        int SftpMakeDirectories(string uname)
        {
            string[] dd = uname.Split('/');
            string dir = "";
            int n = 0;
            for (int i = 0; i < dd.Length-1; i++) {
                if (dir != "")
                    dir = dir + "/" + dd[i];
                else
                    dir = dd[i];

                if (!client.Exists(dir))
                    try {//?ex
                        client.CreateDirectory(dir);
                        n++;
                    } catch (Renci.SshNet.Common.SshException ex) {
                        Console.WriteLine("IGNORE: mkdir error " + ex.Message);
                    }
            }
            return n;
        }

        int SftpDeleteFile(string uname)
        {
            //if (client == null)
            //    SftpConnect();

            Cursor.Current = Cursors.WaitCursor;
            try {//?ex
                if (client == null)
                    SftpConnect();

                var attr = client.GetAttributes(uname);
                if (attr.IsDirectory)
                    return DeleteDirectories(uname);
                else {
                    client.DeleteFile(uname);
                    return 0;
                }
            } catch (Exception ex) {
                log.WriteLine(Color.Fuchsia, "SFTP delete error: " + ex.Message);
            }
            return -1;
        }

        int DeleteDirectories(string uname)
        {
            int n = 0;
            foreach (var e in client.ListDirectory(uname))
                if (e.IsDirectory) {
                    if (e.Name != "." && e.Name != "..")
                        n += DeleteDirectories(uname + "/" + e.Name);
                } else {
                    client.DeleteFile(uname + "/" + e.Name);
                    n++;
                }
                
            client.DeleteDirectory(uname);
            return n+1;
        }

        void SftpRename(string uname, string newname)
        {
            //if (client == null)
            //    SftpConnect();

            Cursor.Current = Cursors.WaitCursor;
            string last_err = "";
            for (int retry = 0; retry < 3; retry++)
                try {//?ex
                    if (client == null)
                        SftpConnect();

                    SftpMakeDirectories(uname);
                    client.RenameFile(uname, newname);
                    RefreshURL(null, newname);
                    return;

                } catch (Renci.SshNet.Common.SftpPathNotFoundException ex) {
                    log.WriteLine(Color.Fuchsia, ex.Message);
                    last_err = ex.Message;
                    //SftpClose();
                } catch (Renci.SshNet.Common.SshException ex) {
                    log.WriteLine(Color.Fuchsia, ex.Message);
                    last_err = ex.Message;
                    SftpClose();
                } catch (Exception ex) {
                    last_err = ex.Message;
                    //log.WriteLine(Color.Fuchsia, ex.Message);
                }

            throw new Exception(last_err);
        }
        #endregion

        private void AutoCheck_CheckedChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            StartItem.Checked = AutoCheck.Checked;
            if (AutoCheck.Checked) {
                fileSystemWatcher1.Path = LocalRoot.Text;
                fileSystemWatcher1.EnableRaisingEvents = true;
                log.WriteLine("watching \"{0}\"", fileSystemWatcher1.Path);

                //ex ユーザーがチェックを入れたとき、一応接続を試みる。失敗しても無視するかわり、アップロード実行時に再接続を試みる
                try {
                    SftpConnect();
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            } else {
                fileSystemWatcher1.EnableRaisingEvents = false;
                log.WriteLine("watch stopped");

                SftpClose();
            }

            sett_modified = true;
        }

        bool skip_invisible = true;
        
        bool bring_to_front_on_action = true;
        string last_path;
        int last_tc;

        static string ByteUnit(ref float n)
        {
            string[] units = { "B", "KB", "MB", "GB" };
            int u = 0;
            for ( ; n >= 1024f && u+1 < units.Length; n /= 1024, u++)
                ;
            return units[u];
        }

        private void label_Click(object sender, EventArgs e)
        {
            this.SelectNextControl(sender as Control, true, true, true, true);
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(this, 0, -20);
        }


        #region 【FS watcher】

        void AppBringToFront()
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;
            this.TopMost = true;
            this.TopMost = false;
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            if (bring_to_front_on_action)
                AppBringToFront();

            string uname = e.Name.Replace('\\', '/');
            if (Directory.Exists(e.FullPath))
#if XXX
                return; // ignore directory change
#else
                SftpMkDir(uname + '/');
#endif
            else {
                if (last_path == e.Name && Environment.TickCount - last_tc < 1000)
                    return; // ignore duplicate event

                float nbytes = SftpUploadFile(uname);
                last_path = e.Name;
            }
        }

        void SftpMkDir(string uname)
        {
            if (client == null)
                SftpConnect();

            try {//?ex
                int tc0 = Environment.TickCount;
                int n = SftpMakeDirectories(uname);

                //var attr = client.GetAttributes(uname);
                //attr.LastWriteTime = File.GetLastWriteTime(wpath);
                //client.SetAttributes(uname, attr);

                if (n > 0) {
                    RefreshURL(null, uname);
                    log.WriteLine("SFTP mkdir \"{0}", uname, Environment.TickCount - tc0);
                }
            } catch (Renci.SshNet.Common.SshException ex) {
                log.WriteLine(Color.Fuchsia, ex.Message);
                //last_err = ex.Message;
                SftpClose();
            } catch (Exception ex) {
                log.WriteLine(Color.Fuchsia, ex.Message);
                //last_err = ex.Message;
            }
        }

        private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
        {
            if (bring_to_front_on_action)
                AppBringToFront();

            string uname = e.Name.Replace('\\', '/');
            RefreshURL(null, uname);

            int tc0 = Environment.TickCount;
            int n = SftpDeleteFile(uname);
            int msec = Environment.TickCount - tc0;
            if (n > 0)
                log.WriteLine("SFTP delete \"{0}/\" and {1}files in it; {2}msec", uname, n, msec);
            else
                log.WriteLine("SFTP delete \"{0}\"; {1}msec", uname, msec);

            // VSCode 別名で保存時のへんな挙動対策
            last_tc = tc0 - 10000;
        }

        private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            if (bring_to_front_on_action)
                AppBringToFront();

            string oldname = e.OldName.Replace('\\', '/'), newname = e.Name.Replace('\\', '/');
            SftpRename(oldname, newname);
            log.WriteLine("SFTP rename \"{0}\" to \"{1}\"", oldname, newname);
        }

        private void LocalRoot_TextChanged(object sender, EventArgs e)
        {
            AutoCheck.Checked = false;

            sett_modified = true;
        }

        private void LocalRoot_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                if (Directory.Exists(files[0]))
                    e.Effect = DragDropEffects.Link;
            }
        }

        private void LocalRoot_DragDrop(object sender, DragEventArgs e)
        {
            // drop directory to register PC local path (upload source)
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            AutoCheck.Checked = false;
            LocalRoot.Text = files[0];
            log.WriteLine("local dir changed to \"{0}\"", files[0]);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (fileSystemWatcher1.EnableRaisingEvents) {
                DescriptionLabel.ForeColor = ColorCycle(Environment.TickCount/30);
            }

#if GVS_MONITOR
            if (moni != null && moni.Available > 0)
                /*lock (moni) */MoniIntercept();
#endif
        }
        #endregion

#if MAC_ARP
        // ex http://furuya02.hatenablog.com/entry/20111107/1399766912
        [DllImport("iphlpapi.dll")]
        extern static int GetIpNetTable(IntPtr pTcpTable, ref int pdwSize, bool bOrder);

        [StructLayout(LayoutKind.Sequential)]
        public struct MIB_IPNETROW
        {
            public int Index;
            public int PhysAddrLen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] PhysAddr;
            //public int Addr;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Addr;
            public int Type;
        }

        void GetArpA()
        {
            int size = 0;
            GetIpNetTable(IntPtr.Zero, ref size, false);
            IntPtr mem = Marshal.AllocHGlobal(size);
            if (GetIpNetTable(mem, ref size, true) == 0) {
                int num = Marshal.ReadInt32(mem);
                IntPtr p = IntPtr.Add(mem, 4);
                for (int i = 0; i < num; i++) {
                    var n = (MIB_IPNETROW)Marshal.PtrToStructure(p, typeof(MIB_IPNETROW));
                    if (n.Type >= 3) { //   MIB_IPNET_TYPE is 3: DYNAMIC, 4: STATIC
                        string mac = BitConverter.ToString(n.PhysAddr, 0, 6);
                        string ip = new IPAddress(n.Addr).ToString();
                        UpdateHosts(mac, "", ip, null);

                        if (MacFilter(mac)) {
                            if (RemoteHost.Items.IndexOf(ip) < 0)
                                RemoteHost.Items.Add(ip);
                            if (RemoteHost.Items.IndexOf(mac) < 0)
                                RemoteHost.Items.Add(mac);
                        }
                    }
                    p = IntPtr.Add(p, Marshal.SizeOf(typeof(MIB_IPNETROW)));
                }
                Marshal.FreeHGlobal(mem);
            }
        }

        List<string> mac_filters = new List<string>();

        bool MacFilter(string mac) 
        {
            mac = mac.ToUpper();
            foreach (string ff in mac_filters) {
                string f = ff.ToUpper();
                if (f.Contains('*') || f.Contains('?')) {
                    string pat = f.Replace("*", ".*");
                    if (System.Text.RegularExpressions.Regex.IsMatch(mac, pat))
                        return true;
                } else if (mac.StartsWith(f))
                    return true;
            }
            return false;
        }
#endif

        /* get icon image from file */

        static Image TryGetIconImage(string filename, bool large = false)
        {
            if (filename == "")
                return null;

            try {
                var shinfo = new SHFILEINFO();
                IntPtr h = SHGetFileInfo(filename, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), 
                    SHGFI_ICON | (large ? 0 : SHGFI_SMALLICON));
                //Console.WriteLine("{0}", h);
                return Icon.FromHandle(shinfo.hIcon).ToBitmap();
            } catch (Exception) { }
            return null;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        [DllImport("shell32.dll")]
        static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_LARGEICON = 0x0;
        public const uint SHGFI_SMALLICON = 0x1;


        class NetworkInterfaceInfo
        {
            public string MAC;
            public string Name;
            public string Description;
            public IPAddress Address;
            public IPAddress Mask;
            public NetworkInterfaceType Type;   // Ether or Wireless

            public Socket udp;
            public bool active;
            public int metric;

            public NetworkInterfaceInfo(string mac, string name, string description)
            {
                this.MAC = mac;
                this.Name = name;
                this.Description = description;
                this.Address = null;
                this.Mask = null;
                this.udp = null;
                this.metric = -1;   // unknwon
            }

            // this string appear in combobox
            public override string ToString()
            {
                if (Address != null)
                    return Name + " (" + Address.ToString() + ")";
                else
                    return Name;
            }

        }

        Dictionary<string, NetworkInterfaceInfo> interfaces = new Dictionary<string, NetworkInterfaceInfo>();

        private void PingAllItem_Click(object sender, EventArgs e)
        {
            foreach (var ni in NetworkInterface.GetAllNetworkInterfaces()) {
                if (ni.NetworkInterfaceType != NetworkInterfaceType.Loopback
                     && ni.NetworkInterfaceType != NetworkInterfaceType.Tunnel
                     && !ni.Name.StartsWith("VMware Network Adapter")) {
                    //string key = ToMacStr(ni.GetPhysicalAddress());
                    string mac = BitConverter.ToString(ni.GetPhysicalAddress().GetAddressBytes(), 0, 6);
                    var n = new NetworkInterfaceInfo(mac, ni.Name, ni.Description);
                    n.Type = ni.NetworkInterfaceType;
                    //if (n.Type == NetworkInterfaceType.Wireless80211)
                    //    wifi_mode = true;

                    foreach (var ip in ni.GetIPProperties().UnicastAddresses)
                        if (ip.IPv4Mask != IPAddress.Any) {
                            n.Address = ip.Address;
                            n.Mask = ip.IPv4Mask;
                            if (ni.OperationalStatus == OperationalStatus.Up)
                                n.active = true;

                            Console.WriteLine("{0} {1}", mac, n.Address);
                            break;
                        }

                    // refresh network interface menu
                    //int index = networkMenu.DropDownItems.IndexOfKey("n_"+n.MAC);
                    //var item = (index >= 0) ? networkMenu.DropDownItems[index] : AddInterfaceMenu(n);
                    //item.Tag = n;
                    interfaces[mac] = n;

                    if (ni.Supports(NetworkInterfaceComponent.IPv4)) {
                        var ipv4 = ni.GetIPv4Statistics();
                        // ...
                    }
                }
            }
#if xxx
            using (var p = new System.Net.NetworkInformation.Ping()) {
                //var reply = p.Send("192.168.100.79");
                //var reply = p.Send("192.168.100.255");

                //var reply = p.Send("255.255.255.255");
                var po = new PingOptions();
                var reply = p.Send(IPAddress.Parse("192.168.100.255"), 1000, new byte[] { 0x12, 0x34, 0x56, 0x78 }, po);
                p.SendAsync()

                //結果を取得
                if (reply.Status == IPStatus.Success) {
                    Console.WriteLine("Reply from {0}:bytes={1} time={2}ms TTL={3}",
                        reply.Address, reply.Buffer.Length,
                        reply.RoundtripTime, reply.Options.Ttl);
                } else {
                    Console.WriteLine("Ping送信に失敗。({0})",
                        reply.Status);
                }
            }
#endif
        }

#if GVS_MONITOR
        const byte GVS_CMD_HEAD = 0x3E;
        const byte GVS_REP_HEAD = 0x3C;

        Socket moni = null;

        void MoniInit()
        {
            try {
                moni = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                moni.EnableBroadcast = true;
                moni.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                moni.Bind(new IPEndPoint(IPAddress.Any, GVS_UDP_PORT));
            } catch (Exception ex) {
                log.WriteLine(Color.Fuchsia, "monitor: unknwon "+ex.Message);
            }
        }

        Ping moniPing = null;//new Ping();

        void MoniIntercept()
        {
            try {
                byte[] buffer = new byte[4096];
                EndPoint ep = new IPEndPoint(IPAddress.Any, 0);
                int r = moni.ReceiveFrom(buffer, ref ep);
                if (buffer[0] != GVS_CMD_HEAD || buffer[1] != GetNetworkMAC)
                    return;

                //GetArpA();

                string ip = (ep as IPEndPoint).Address.ToString();
                var host = hosts.Find(h => h.Addr == ip);
                if (host != null/* && hh[0].Key.StartsWith(FASTUS_VENDOR_CODE)*/) {
                    log.WriteLine(Color.Gray, "GetNetworkMAC from {0}({1})", ip, host.Mac);
                } else {
                    log.WriteLine(Color.Gray, "GetNetworkMAC from {0}", ip);
                    
                    //if (moniPing == null) {
                    //    moniPing = new Ping();
                    //    moniPing.PingCompleted += PingCompleted;
                    //}

                    //var opts = new PingOptions(64, true);
                    //byte[] bs = System.Text.Encoding.ASCII.GetBytes(new string('A', 32));
                    //moniPing.SendAsync(ip, 10000, bs, opts, null);
                }

            } catch (Exception ex) {
                log.WriteLine(Color.Fuchsia, "monitor: unknwon "+ex.Message);
            }
        }

        void PingCompleted(object sender, PingCompletedEventArgs e)
        {
            if (e.Cancelled) {
                Console.WriteLine("Pingがキャンセルされました。");
            } else if (e.Error != null) {
                Console.WriteLine("エラー:" + e.Error.Message);
            } else if (e.Reply.Status == IPStatus.Success) {
                Console.WriteLine("Reply from {0}:bytes={1} time={2}ms TTL={3}",
                    e.Reply.Address, e.Reply.Buffer.Length,
                    e.Reply.RoundtripTime, e.Reply.Options.Ttl);
                var host = hosts.Find(h => h.Addr == e.Reply.Address.ToString());
                if (host != null/* && hh[0].Key.StartsWith(FASTUS_VENDOR_CODE)*/) {
                    log.WriteLine(Color.Gray, "GetNetworkMAC from {0}({1})", e.Reply.Address, host.Mac);
                } else
                    log.WriteLine(Color.Gray, "GetNetworkMAC from {0}", e.Reply.Address);
            } else
                Console.WriteLine("Ping送信に失敗。({0})", e.Reply.Status);
        }
#endif

        static Color ColorCycle(int deg)
        {
            int a = 255 * (deg % 60) / 59;
            int n = deg / 60;
            switch (n % 6) {
            case 0: return Color.FromArgb(255, a, 0);
            case 1: return Color.FromArgb(255-a, 255, 0);
            case 2: return Color.FromArgb(0, 255, a);
            case 3: return Color.FromArgb(0, 255-a, 255);
            case 4: return Color.FromArgb(a, 0, 255);
            default: return Color.FromArgb(255, 0, 255-a);
            }
        }

        static Color MixColor(Color color0, float ratio0, Color color1, float ratio1)
        {
            if (ratio0 > 1f)
                ratio0 = 1f;
            else if (ratio0 < 0f)
                ratio0 = 0f;
            if (ratio1 > 1f)
                ratio1 = 1f;
            else if (ratio1 < 0f)
                ratio1 = 0f;

            int r = (int)(color0.R*ratio0 + color1.R*ratio1);
            int g = (int)(color0.G*ratio0 + color1.G*ratio1);
            int b = (int)(color0.B*ratio0 + color1.B*ratio1);
            return Color.FromArgb(r, g, b);
        }

    }
}
