// ExecDialog.cs - select external application executable
// fork from GvsIcon.SshDialog

// usage
// var dialog = new ExecDialog(ExecType...)

// dialog.ExecCallback = ProcessStart;

// DialogResult ret = dialog.Popup(name, exe, options)
// if (ret == DialogResult.OK) {
//  ... = dialog.AppName
//  ... = dialog.ExeFilename;
//  ... = dialog.Options;

// if (ret == DialogResult.Abort) { [Remove] was clicked
// 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace UpASAP
{
    public enum ExecType
    {
        NONE,
        SSH_TERM,   // select terminal application for Secure Shell connection.
        WEB_BROWSER,// select web browser: select web browser.
        HTML_EDITOR
    }

    public partial class ExecDialog: Form
    {
        protected ExecDialog()
        {
            InitializeComponent();
            //Localizer.Current.Apply(toolTip1, this);
        }

        public ExecDialog(ExecType templ)
        {
            InitializeComponent();
            //Localizer.Current.Apply(toolTip1, this);

            switch (templ) {
            case ExecType.SSH_TERM:
                DescriptionLabel.Text = "select terminal application for SSH (Secure Shell) connection.";
                Text = "UpASAP: SSH";
                break;
            case ExecType.WEB_BROWSER:
                DescriptionLabel.Text = "select web browser.";
                Text = "UpASAP: web browser";
                break;
            case ExecType.HTML_EDITOR:
                DescriptionLabel.Text = "select HTML editor.";
                Text = "UpASAP: HTML editor";
                break;
            }

            if (File.Exists(templates_filename))
                try {
                    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<ExecTemplate>), new Type[] { typeof(ExecTemplate) });
                    using (var sr = new StreamReader(templates_filename)) {
                        var data = (List<ExecTemplate>)serializer.Deserialize(sr);
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }

            TemplateCombo.Items.Clear();
            TemplateCombo.Items.AddRange(templates.Where(x => x.templ == templ).ToArray());
        }

        public DialogResult Popup(string name, string exe, string options)
        {
            AppName = name;
            Exe = exe;
            Options = options;

            RemoveButton.Enabled = name != "" || exe != "" || options != "";

            return ShowDialog();
        }

        public string AppName
        {
            get { return NameText.Text; }
            set { NameText.Text = value; }
        }

        public string Exe
        {
            get { return ExeText.Text; }
            set { ExeText.Text = value; }
        }

        public string Options
        {
            get { return OptionsText.Text; }
            set { OptionsText.Text = value; }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            AppName = "";
            Exe = "";
            Options = "";

            // exit modal, and Popup() returns DialogResult.Abort means remove
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (this.AppName == "")
                this.AppName = TryGetProductName(this.Exe);
        }

        public delegate void ProcessStartProc(string exe, string options);

        public ProcessStartProc ExecCallback
        {
            get { return _callback; }
            set {
                _callback = value;
                ExecButton.Enabled = _callback != null;
            }
        }

        ProcessStartProc _callback = null;

        private void execButton_Click(object sender, EventArgs e)
        {
            if (ExecCallback != null)
                try {
                    ExecCallback(Exe, Options);
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message, "exec error");
                }
        }

        public class ExecTemplate
        {
            public ExecType templ;
            public string name;
            public string exe;
            public string opts;

            public string exe_pat;
            //public string first_path;
            public string[] paths;

            public ExecTemplate() { }

            public ExecTemplate(ExecType templ, string name, string first_path, string exe_pat, string opts = "")
            {
                this.templ = templ;
                this.name = name;
                this.exe_pat = exe_pat;
                this.paths = new string[] { first_path };
                this.opts = opts;
            }

            public ExecTemplate(ExecType templ, string name, string[] paths, string exe_pat, string opts = "")
            {
                this.templ = templ;
                this.name = name;
                this.exe_pat = exe_pat;
                this.paths = paths;
                this.opts = opts;
            }

            public override string ToString()
            {
                return name;
            }
        }

        static string pp86 = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFilesX86);
        //string pp = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles);
        static string pp = pp86.Replace(@"Program Files (x86)", @"Program Files");
        static string ll = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);

        List<ExecTemplate> templates = new List<ExecTemplate>()
        {
            new ExecTemplate(ExecType.WEB_BROWSER, "Google chrome", 
               pp86+@"\Google\Chrome\Application\", "chrome.exe"),
            new ExecTemplate(ExecType.WEB_BROWSER, "Mozilla Firefox",
               new string[] { pp86+@"\Mozilla Firefox\",
                              pp+@"\Mozilla Firefox\" }, "firefox.exe"),
            new ExecTemplate(ExecType.WEB_BROWSER, "IE11",
               pp+@"\Internet Explorer\", "iexplore.exe"),
            new ExecTemplate(ExecType.WEB_BROWSER,
               "edge", @"?", "microsoft-edge:"),

            new ExecTemplate(ExecType.SSH_TERM, "RLogin", @"c:\", "RLogin.exe", 
               "/inuse /ssh /user ${user} /pass ${pass} /ip ${host}"),
            new ExecTemplate(ExecType.SSH_TERM, "putty", pp86, "puttyjp.exe",
               "-ssh -pw ${pass} ${user}@${host}"),
            new ExecTemplate(ExecType.SSH_TERM, "Tera Term", pp86, "ttermpro*.exe",
               "/auth=passwd /user=${user} /passwd=${pass} ${host}"),

            new ExecTemplate(ExecType.HTML_EDITOR, "VSCode",
               new string[] { pp+@"\Microsoft VS Code", ll }, "Code.exe")
        };

        static string TryGetProductName(string filename)
        {
            try {
                var vi = System.Diagnostics.FileVersionInfo.GetVersionInfo(filename);
                if (vi.ProductName != null)
                    return vi.ProductName;
                if (vi.FileDescription != null)
                    return vi.FileDescription;
            } catch (Exception) { }
            return "";
        }

        bool search_busy = false;

        private void TemplateCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (search_busy) {
                abort_flag = true;
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            var t = TemplateCombo.SelectedItem as ExecTemplate;
            OptionsText.Text = t.opts;
            NameText.Text = t.name;
            ExeText.Text = "?";

            if (t.exe_pat.EndsWith(":")) {
                ExeText.Text = t.exe_pat;
                return;
            }

            string[] paths = t.paths;
            //paths = paths.Distinct().ToArray();

            label3.Text = "searching ...";
            abort_flag = false;
            search_busy = true;
            var task = Task.Factory.StartNew(() => {
                var s = findFile(paths, t.exe_pat);
                Invoke((MethodInvoker)(() => {
                    if (s != null) {
                        label3.Text = "found";
                        ExeText.Text = s;
                        NameText.Text = TryGetProductName(s);
                    } else
                        label3.Text = (abort_flag) ? "canceled" : "not found";
                    this.Cursor = Cursors.Default;
                }));
                search_busy = false;
            });
        }

        bool abort_flag;

        string findFile(string[] paths, string pattern)
        {
            if (abort_flag)
                return null;

            foreach (string path in paths)
                try {
                    var files = Directory.GetFiles(path, pattern, SearchOption.TopDirectoryOnly);
                    if (files.Length > 0)
                        return Path.Combine(path, files[0]);

                    var dir = Directory.GetDirectories(path);
                    if (dir.Length > 0) {
                        var f = findFile(dir, pattern);
                        if (f != null)
                            return f;
                    }
                } catch (Exception) {    // UnauthorizedAccessException
                    // IGNORE
                }
            // TODO 見つからなかったときのキャンセル処理
            return null;
        }

        string templates_filename = Path.ChangeExtension(Application.ExecutablePath, "exec.xml");

        private void button1_Click(object sender, EventArgs e)
        {
            //string filename = Path.ChangeExtension(Application.ExecutablePath, "exec.xml");
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<ExecTemplate>), new Type[] { typeof(ExecTemplate) });
            using (var sw = new StreamWriter(templates_filename, false)) {
                serializer.Serialize(sw, templates);
            }
        }

    }

}
