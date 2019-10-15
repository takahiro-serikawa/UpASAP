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

namespace UpASAP
{
    public partial class AppLog: RichTextBox, IDisposable
    {
        public AppLog()
        {
            InitializeComponent();

            this.BorderStyle = BorderStyle.None;
            this.ReadOnly = true;
            this.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.BackColor = Color.White;

            if (!this.DesignMode) {

                rich2 = new RichTextBox();
                rich2.Font = this.Font;
                rich2.ForeColor = this.ForeColor;

                timer = new Timer();
                timer.Interval = 10;
                timer.Tick += _timer_Tick;
                timer.Enabled = true;
            }
#if DEBUG
                string logname = Path.ChangeExtension(Application.ExecutablePath, ".log");
                logfile = new StreamWriter(logname, true);
#endif

        }

        const int EMPHASIS_MSEC = 2500;
        readonly Color EmphasisColor = Color.Aqua;
        readonly Color FadedColor = Color.Black;
        readonly Color TickColor = Color.Teal;

        void _timer_Tick(object sender, EventArgs e)
        {
            if (/*rich1 != null && */!this.Focused) {
                var now = DateTime.Now;
                string time = now.ToString(log_time_fmt);

                for (int i = this.Text.LastIndexOf("\n"); i > 0; ) {
                    int j = this.Text.LastIndexOf("\n", --i);
                    if (j < 0 || j+1 >= this.Text.Length)
                        break;
                    string t = this.Text.Substring(j+1, time.Length);
                    TimeSpan ts = now - DateTime.Parse(t);
                    if (ts.TotalMilliseconds < 0 || ts.TotalMilliseconds >= EMPHASIS_MSEC)
                        break;

                    float ratio = (float)ts.TotalMilliseconds/EMPHASIS_MSEC;
                    ratio = 3 * ratio - 1f;
                    this.Select(j+1, t.Length);
                    this.SelectionColor = MixColor(FadedColor, ratio, EmphasisColor, 1f - ratio);

                    i = j;
                }

                this.Select(log_index, 100);
                this.SelectionColor = TickColor;
                this.SelectedText = time;
            }
        }

        public void Dispose()
        {
            if (logfile != null) {
                logfile.Dispose();
                logfile = null;
            }

            //rich1 = null;
            rich2.Dispose();
            timer.Dispose();
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

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        RichTextBox /*rich1 = null, */rich2 = null;
        Timer timer;
        StreamWriter logfile = null;
        const int LOG_LINES = 100;
        string log_time_fmt = "HH:mm:ss.fff ";
        int log_index = 0, log_index0 = -1;

        public void WriteLine(string fmt, params object[] args) { WriteLine(Color.Black, fmt, args); }

        public void WriteLine(Color color, string fmt, params object[] args)
        {
            if (this.InvokeRequired) {
                this.BeginInvoke((MethodInvoker)(() => WriteLine(color, fmt, args)));
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
                this.Rtf = rich2.Rtf;
                this.ClearUndo();

                // scroll to new line
                log_index0 = log_index;
                log_index = this.Text.Length;
                this.Select(log_index, 0);
                this.ScrollToCaret();
            }
        }

        public void Flash(int n)
        {
            for (int i = 0; i < n; i++) {
                this.BackColor = Color.Silver;
                this.Refresh();
                System.Threading.Thread.Sleep(70);

                this.BackColor = Color.White;
                this.Refresh();
                System.Threading.Thread.Sleep(150);
            }
        }

    }
}
