namespace UpASAP
{
    partial class UpMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpMain));
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AboutItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StartItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ManuallyItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BrowserMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.BrowserItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.BrowserAddItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BrowserRemoveItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyUrlItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditUrlItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TerminalItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TerminalSettingsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExplorerItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditorItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditorSettingsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.NewSettItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenSettItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveSettItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.QuitItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearLogItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectLocalItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PingAllItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.AutoCheck = new System.Windows.Forms.CheckBox();
            this.RemoteRoot = new System.Windows.Forms.TextBox();
            this.RemoteHost = new System.Windows.Forms.ComboBox();
            this.LocalRoot = new System.Windows.Forms.TextBox();
            this.Pass = new System.Windows.Forms.TextBox();
            this.User = new System.Windows.Forms.TextBox();
            this.TerminalButton = new System.Windows.Forms.Button();
            this.ExplorerButton = new System.Windows.Forms.Button();
            this.ManualButton = new System.Windows.Forms.Button();
            this.EditorButton = new System.Windows.Forms.Button();
            this.linkText1 = new UpASAP.LinkText();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.browser7 = new System.Windows.Forms.Button();
            this.browser6 = new System.Windows.Forms.Button();
            this.browser5 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.browser4 = new System.Windows.Forms.Button();
            this.browser3 = new System.Windows.Forms.Button();
            this.browser2 = new System.Windows.Forms.Button();
            this.browser1 = new System.Windows.Forms.Button();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.UrlLabel = new System.Windows.Forms.Label();
            this.hiddenMenuStrip = new System.Windows.Forms.MenuStrip();
            this.hiddenMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.dummyF7 = new System.Windows.Forms.ToolStripMenuItem();
            this.hidden1 = new System.Windows.Forms.ToolStripMenuItem();
            this.hidden2 = new System.Windows.Forms.ToolStripMenuItem();
            this.hidden3 = new System.Windows.Forms.ToolStripMenuItem();
            this.hidden4 = new System.Windows.Forms.ToolStripMenuItem();
            this.hidden5 = new System.Windows.Forms.ToolStripMenuItem();
            this.hidden6 = new System.Windows.Forms.ToolStripMenuItem();
            this.hidden7 = new System.Windows.Forms.ToolStripMenuItem();
            this.assignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SftpLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ServerGroup = new System.Windows.Forms.GroupBox();
            this.ProgressBar = new System.Windows.Forms.PictureBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.hiddenMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProgressBar)).BeginInit();
            this.SuspendLayout();
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.IncludeSubdirectories = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
            this.fileSystemWatcher1.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
            this.fileSystemWatcher1.Deleted += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Deleted);
            this.fileSystemWatcher1.Renamed += new System.IO.RenamedEventHandler(this.fileSystemWatcher1_Renamed);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutItem,
            this.StartItem,
            this.ManuallyItem,
            this.BrowserMenu,
            this.CopyUrlItem,
            this.EditUrlItem,
            this.TerminalItem,
            this.TerminalSettingsItem,
            this.ExplorerItem,
            this.EditorItem,
            this.EditorSettingsItem,
            this.toolStripMenuItem1,
            this.NewSettItem,
            this.OpenSettItem,
            this.SaveSettItem,
            this.toolStripMenuItem2,
            this.QuitItem,
            this.clearLogItem,
            this.SelectLocalItem,
            this.PingAllItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(210, 412);
            // 
            // AboutItem
            // 
            this.AboutItem.BackColor = System.Drawing.Color.OrangeRed;
            this.AboutItem.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.AboutItem.ForeColor = System.Drawing.Color.White;
            this.AboutItem.Name = "AboutItem";
            this.AboutItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.AboutItem.Size = new System.Drawing.Size(209, 22);
            this.AboutItem.Text = "UpASAP ...";
            this.AboutItem.ToolTipText = "ライセンス/詳細設定画面を開きます。";
            this.AboutItem.Click += new System.EventHandler(this.AboutItem_Click);
            // 
            // StartItem
            // 
            this.StartItem.CheckOnClick = true;
            this.StartItem.Name = "StartItem";
            this.StartItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.StartItem.Size = new System.Drawing.Size(209, 22);
            this.StartItem.Text = "&Auto Upload";
            this.StartItem.ToolTipText = "PCのローカルファイルが変更されると、そのファイルを直ちにアップロードします。";
            this.StartItem.Click += new System.EventHandler(this.Start_Click);
            // 
            // ManuallyItem
            // 
            this.ManuallyItem.Image = global::UpASAP.Properties.Resources.upload1;
            this.ManuallyItem.Name = "ManuallyItem";
            this.ManuallyItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.ManuallyItem.Size = new System.Drawing.Size(209, 22);
            this.ManuallyItem.Text = "Upload &Manually";
            this.ManuallyItem.Click += new System.EventHandler(this.Manual_Click);
            // 
            // BrowserMenu
            // 
            this.BrowserMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BrowserItem,
            this.toolStripMenuItem3,
            this.BrowserAddItem,
            this.BrowserRemoveItem});
            this.BrowserMenu.Name = "BrowserMenu";
            this.BrowserMenu.ShortcutKeyDisplayString = "";
            this.BrowserMenu.Size = new System.Drawing.Size(209, 22);
            this.BrowserMenu.Text = "Web &Browsers";
            // 
            // BrowserItem
            // 
            this.BrowserItem.Name = "BrowserItem";
            this.BrowserItem.ShortcutKeyDisplayString = "F7";
            this.BrowserItem.Size = new System.Drawing.Size(156, 22);
            this.BrowserItem.Text = "&Default ...";
            this.BrowserItem.ToolTipText = "ウェブブラウザ(OS規定)を開きます。";
            this.BrowserItem.Click += new System.EventHandler(this.Browser_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(153, 6);
            // 
            // BrowserAddItem
            // 
            this.BrowserAddItem.Name = "BrowserAddItem";
            this.BrowserAddItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F7)));
            this.BrowserAddItem.ShowShortcutKeys = false;
            this.BrowserAddItem.Size = new System.Drawing.Size(156, 22);
            this.BrowserAddItem.Text = "Assign ...";
            this.BrowserAddItem.ToolTipText = "ブラウザボタンにブラウザを登録します。";
            this.BrowserAddItem.Click += new System.EventHandler(this.BrowserAddItem_Click);
            // 
            // BrowserRemoveItem
            // 
            this.BrowserRemoveItem.Name = "BrowserRemoveItem";
            this.BrowserRemoveItem.Size = new System.Drawing.Size(156, 22);
            this.BrowserRemoveItem.Text = "Remove ...";
            this.BrowserRemoveItem.ToolTipText = "ブラウザボタンの登録を取り消します。";
            this.BrowserRemoveItem.Click += new System.EventHandler(this.BrowserRemoveItem_Click);
            // 
            // CopyUrlItem
            // 
            this.CopyUrlItem.Name = "CopyUrlItem";
            this.CopyUrlItem.Size = new System.Drawing.Size(209, 22);
            this.CopyUrlItem.Text = "&Copy URL";
            this.CopyUrlItem.ToolTipText = "URLをクリップボードにコピーします。";
            this.CopyUrlItem.Click += new System.EventHandler(this.CopyUrl_Click);
            // 
            // EditUrlItem
            // 
            this.EditUrlItem.Name = "EditUrlItem";
            this.EditUrlItem.Size = new System.Drawing.Size(209, 22);
            this.EditUrlItem.Text = "Select &URL";
            this.EditUrlItem.ToolTipText = "URLを編集します。";
            this.EditUrlItem.Click += new System.EventHandler(this.EditUrl_Click);
            // 
            // TerminalItem
            // 
            this.TerminalItem.Name = "TerminalItem";
            this.TerminalItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.TerminalItem.Size = new System.Drawing.Size(209, 22);
            this.TerminalItem.Text = "&Terminal ...";
            this.TerminalItem.ToolTipText = "端末ソフトを起動してSSH接続します。";
            this.TerminalItem.Click += new System.EventHandler(this.Terminal_Click);
            // 
            // TerminalSettingsItem
            // 
            this.TerminalSettingsItem.Name = "TerminalSettingsItem";
            this.TerminalSettingsItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F8)));
            this.TerminalSettingsItem.ShowShortcutKeys = false;
            this.TerminalSettingsItem.Size = new System.Drawing.Size(209, 22);
            this.TerminalSettingsItem.Text = "Terminal settings ...";
            this.TerminalSettingsItem.ToolTipText = "SSH接続するための端末ソフトを選択します。";
            this.TerminalSettingsItem.Click += new System.EventHandler(this.TerminalSettings_Click);
            // 
            // ExplorerItem
            // 
            this.ExplorerItem.Name = "ExplorerItem";
            this.ExplorerItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.ExplorerItem.Size = new System.Drawing.Size(209, 22);
            this.ExplorerItem.Text = "&Explorer ...";
            this.ExplorerItem.ToolTipText = "アップロード元ディレクトリをエクスプローラで表示します。";
            this.ExplorerItem.Click += new System.EventHandler(this.Explorer_Click);
            // 
            // EditorItem
            // 
            this.EditorItem.Name = "EditorItem";
            this.EditorItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.EditorItem.Size = new System.Drawing.Size(209, 22);
            this.EditorItem.Text = "&Editor";
            this.EditorItem.ToolTipText = "エディタを起動してHTMLファイルを編集します。";
            this.EditorItem.Click += new System.EventHandler(this.Editor_Click);
            // 
            // EditorSettingsItem
            // 
            this.EditorSettingsItem.Name = "EditorSettingsItem";
            this.EditorSettingsItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F10)));
            this.EditorSettingsItem.ShowShortcutKeys = false;
            this.EditorSettingsItem.Size = new System.Drawing.Size(209, 22);
            this.EditorSettingsItem.Text = "Editor settings ...";
            this.EditorSettingsItem.ToolTipText = "HTMLファイルを編集するためのエディタを選択します。";
            this.EditorSettingsItem.Click += new System.EventHandler(this.EditorSettingsItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(206, 6);
            // 
            // NewSettItem
            // 
            this.NewSettItem.Name = "NewSettItem";
            this.NewSettItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.NewSettItem.ShowShortcutKeys = false;
            this.NewSettItem.Size = new System.Drawing.Size(209, 22);
            this.NewSettItem.Text = "&New settings";
            this.NewSettItem.Click += new System.EventHandler(this.NewItem_Click);
            // 
            // OpenSettItem
            // 
            this.OpenSettItem.Name = "OpenSettItem";
            this.OpenSettItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenSettItem.ShowShortcutKeys = false;
            this.OpenSettItem.Size = new System.Drawing.Size(209, 22);
            this.OpenSettItem.Text = "&Open *.webi ..";
            this.OpenSettItem.ToolTipText = "設定ファイル(*.webi)を開きます。";
            this.OpenSettItem.Click += new System.EventHandler(this.Open_Click);
            // 
            // SaveSettItem
            // 
            this.SaveSettItem.Name = "SaveSettItem";
            this.SaveSettItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveSettItem.ShowShortcutKeys = false;
            this.SaveSettItem.Size = new System.Drawing.Size(209, 22);
            this.SaveSettItem.Text = "&Save *.webi ...";
            this.SaveSettItem.ToolTipText = "設定ファイル(*.webi)に保存します。";
            this.SaveSettItem.Click += new System.EventHandler(this.SaveAs_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(206, 6);
            // 
            // QuitItem
            // 
            this.QuitItem.Image = ((System.Drawing.Image)(resources.GetObject("QuitItem.Image")));
            this.QuitItem.Name = "QuitItem";
            this.QuitItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.QuitItem.Size = new System.Drawing.Size(209, 22);
            this.QuitItem.Text = "e&Xit";
            this.QuitItem.ToolTipText = "UpASAP を終了します。";
            this.QuitItem.Click += new System.EventHandler(this.Quit_Click);
            // 
            // clearLogItem
            // 
            this.clearLogItem.BackColor = System.Drawing.Color.NavajoWhite;
            this.clearLogItem.Name = "clearLogItem";
            this.clearLogItem.Size = new System.Drawing.Size(209, 22);
            this.clearLogItem.Text = "debug: clear log";
            this.clearLogItem.Visible = false;
            this.clearLogItem.Click += new System.EventHandler(this.clearLogItem_Click);
            // 
            // SelectLocalItem
            // 
            this.SelectLocalItem.BackColor = System.Drawing.Color.NavajoWhite;
            this.SelectLocalItem.Name = "SelectLocalItem";
            this.SelectLocalItem.Size = new System.Drawing.Size(209, 22);
            this.SelectLocalItem.Text = "debug: select Local Dir";
            this.SelectLocalItem.Visible = false;
            this.SelectLocalItem.Click += new System.EventHandler(this.selectLocalItem_Click);
            // 
            // PingAllItem
            // 
            this.PingAllItem.BackColor = System.Drawing.Color.NavajoWhite;
            this.PingAllItem.Name = "PingAllItem";
            this.PingAllItem.Size = new System.Drawing.Size(209, 22);
            this.PingAllItem.Text = "debug: ping all";
            this.PingAllItem.Click += new System.EventHandler(this.PingAllItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "webi";
            this.openFileDialog1.FileName = "*.webi";
            this.openFileDialog1.Filter = "UpASAP settings|*.webi";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "webi";
            this.saveFileDialog1.FileName = "*.webi";
            this.saveFileDialog1.Filter = "UpASAP settings|*.webi";
            // 
            // AutoCheck
            // 
            this.AutoCheck.AutoSize = true;
            this.AutoCheck.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AutoCheck.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.AutoCheck.Location = new System.Drawing.Point(20, 104);
            this.AutoCheck.Name = "AutoCheck";
            this.AutoCheck.Size = new System.Drawing.Size(55, 19);
            this.AutoCheck.TabIndex = 12;
            this.AutoCheck.Text = "&auto";
            this.toolTip1.SetToolTip(this.AutoCheck, "F5: PCのローカルファイルが変更されると、そのファイルを直ちにアップロードします。");
            this.AutoCheck.UseVisualStyleBackColor = true;
            this.AutoCheck.CheckedChanged += new System.EventHandler(this.AutoCheck_CheckedChanged);
            // 
            // RemoteRoot
            // 
            this.RemoteRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoteRoot.Location = new System.Drawing.Point(249, 15);
            this.RemoteRoot.Name = "RemoteRoot";
            this.RemoteRoot.Size = new System.Drawing.Size(300, 23);
            this.RemoteRoot.TabIndex = 4;
            this.RemoteRoot.Text = "/home/root/www/";
            this.toolTip1.SetToolTip(this.RemoteRoot, "アップロード先のフォルダ (ウェブサーバのWEB ROOT) を指定します。");
            this.RemoteRoot.TextChanged += new System.EventHandler(this.event_Changed);
            this.RemoteRoot.DragDrop += new System.Windows.Forms.DragEventHandler(this.RemoteRoot_DragDrop);
            this.RemoteRoot.DragEnter += new System.Windows.Forms.DragEventHandler(this.RemoteRoot_DragEnter);
            // 
            // RemoteHost
            // 
            this.RemoteHost.FormattingEnabled = true;
            this.RemoteHost.Location = new System.Drawing.Point(104, 15);
            this.RemoteHost.Name = "RemoteHost";
            this.RemoteHost.Size = new System.Drawing.Size(141, 23);
            this.RemoteHost.Sorted = true;
            this.RemoteHost.TabIndex = 3;
            this.RemoteHost.Text = "hostname";
            this.toolTip1.SetToolTip(this.RemoteHost, "アップロード先のSFTPサーバを選びます。");
            this.RemoteHost.DropDown += new System.EventHandler(this.RemoteHost_DropDown);
            this.RemoteHost.SelectedIndexChanged += new System.EventHandler(this.RemoteHost_SelectedIndexChanged);
            this.RemoteHost.TextChanged += new System.EventHandler(this.event_Changed);
            // 
            // LocalRoot
            // 
            this.LocalRoot.AllowDrop = true;
            this.LocalRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LocalRoot.Location = new System.Drawing.Point(76, 140);
            this.LocalRoot.Name = "LocalRoot";
            this.LocalRoot.Size = new System.Drawing.Size(480, 23);
            this.LocalRoot.TabIndex = 23;
            this.toolTip1.SetToolTip(this.LocalRoot, "アップロード元のPCローカルディレクトリを指定します。");
            this.LocalRoot.TextChanged += new System.EventHandler(this.LocalRoot_TextChanged);
            this.LocalRoot.DragDrop += new System.Windows.Forms.DragEventHandler(this.LocalRoot_DragDrop);
            this.LocalRoot.DragEnter += new System.Windows.Forms.DragEventHandler(this.LocalRoot_DragEnter);
            // 
            // Pass
            // 
            this.Pass.Location = new System.Drawing.Point(194, 41);
            this.Pass.Name = "Pass";
            this.Pass.PasswordChar = '*';
            this.Pass.Size = new System.Drawing.Size(80, 23);
            this.Pass.TabIndex = 8;
            this.Pass.Text = "root";
            this.toolTip1.SetToolTip(this.Pass, "SFTP および SSH 接続に用いるパスワードを指定します。");
            // 
            // User
            // 
            this.User.Location = new System.Drawing.Point(104, 41);
            this.User.Name = "User";
            this.User.Size = new System.Drawing.Size(80, 23);
            this.User.TabIndex = 6;
            this.User.Text = "root";
            this.toolTip1.SetToolTip(this.User, "SFTP および SSH 接続に用いるユーザ名を指定します。");
            // 
            // TerminalButton
            // 
            this.TerminalButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TerminalButton.Location = new System.Drawing.Point(476, 39);
            this.TerminalButton.Name = "TerminalButton";
            this.TerminalButton.Size = new System.Drawing.Size(80, 25);
            this.TerminalButton.TabIndex = 9;
            this.TerminalButton.Text = "&terminal";
            this.TerminalButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.TerminalButton, "F8: 端末ソフトを起動してSSH接続します。");
            this.TerminalButton.UseVisualStyleBackColor = true;
            this.TerminalButton.Click += new System.EventHandler(this.Terminal_Click);
            // 
            // ExplorerButton
            // 
            this.ExplorerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExplorerButton.Location = new System.Drawing.Point(552, 140);
            this.ExplorerButton.Name = "ExplorerButton";
            this.ExplorerButton.Size = new System.Drawing.Size(27, 23);
            this.ExplorerButton.TabIndex = 24;
            this.ExplorerButton.Text = "...";
            this.toolTip1.SetToolTip(this.ExplorerButton, "F9: アップロード元ディレクトリをエクスプローラで表示します。");
            this.ExplorerButton.UseVisualStyleBackColor = true;
            this.ExplorerButton.Visible = false;
            this.ExplorerButton.Click += new System.EventHandler(this.Explorer_Click);
            // 
            // ManualButton
            // 
            this.ManualButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ManualButton.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ManualButton.Image = global::UpASAP.Properties.Resources.upload1;
            this.ManualButton.Location = new System.Drawing.Point(75, 90);
            this.ManualButton.Name = "ManualButton";
            this.ManualButton.Size = new System.Drawing.Size(75, 40);
            this.ManualButton.TabIndex = 13;
            this.ManualButton.Text = "upload";
            this.ManualButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.ManualButton, "F6: 手動一括アップロード (未完)");
            this.ManualButton.UseVisualStyleBackColor = true;
            this.ManualButton.Click += new System.EventHandler(this.Manual_Click);
            // 
            // EditorButton
            // 
            this.EditorButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EditorButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.EditorButton.Location = new System.Drawing.Point(476, 100);
            this.EditorButton.Name = "EditorButton";
            this.EditorButton.Size = new System.Drawing.Size(80, 25);
            this.EditorButton.TabIndex = 21;
            this.EditorButton.Text = "&editor";
            this.EditorButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.EditorButton, "F10: エディタを起動してHTMLファイルを編集します。");
            this.EditorButton.UseVisualStyleBackColor = true;
            this.EditorButton.Click += new System.EventHandler(this.Editor_Click);
            // 
            // linkText1
            // 
            this.linkText1.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.linkText1.AllowDrop = true;
            this.linkText1.BackColor = System.Drawing.SystemColors.Window;
            this.linkText1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.linkText1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkText1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Underline);
            this.linkText1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.linkText1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.linkText1.LinkVisited = false;
            this.linkText1.Location = new System.Drawing.Point(75, 68);
            this.linkText1.Name = "linkText1";
            this.linkText1.Size = new System.Drawing.Size(174, 16);
            this.linkText1.TabIndex = 11;
            this.linkText1.Text = "http://hostname/index.html";
            this.toolTip1.SetToolTip(this.linkText1, "F7: ウェブブラウザ(OS規定)でこのページを開きます。");
            this.linkText1.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.linkText1.TextChanged += new System.EventHandler(this.event_Changed);
            this.linkText1.DragDrop += new System.Windows.Forms.DragEventHandler(this.linkText1_DragDrop);
            this.linkText1.DragEnter += new System.Windows.Forms.DragEventHandler(this.linkText1_DragEnter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(183, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "/";
            this.label3.Click += new System.EventHandler(this.label_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "user";
            this.label2.Click += new System.EventHandler(this.label_Click);
            // 
            // browser7
            // 
            this.browser7.AllowDrop = true;
            this.browser7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.browser7.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.browser7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browser7.Location = new System.Drawing.Point(426, 84);
            this.browser7.Name = "browser7";
            this.browser7.Size = new System.Drawing.Size(44, 44);
            this.browser7.TabIndex = 20;
            this.browser7.Tag = "";
            this.browser7.UseVisualStyleBackColor = true;
            this.browser7.Click += new System.EventHandler(this.browsers_Click);
            this.browser7.DragDrop += new System.Windows.Forms.DragEventHandler(this.browsers_DragDrop);
            this.browser7.DragEnter += new System.Windows.Forms.DragEventHandler(this.browsers_DragEnter);
            this.browser7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.browsers_MouseDown);
            // 
            // browser6
            // 
            this.browser6.AllowDrop = true;
            this.browser6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.browser6.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.browser6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browser6.Location = new System.Drawing.Point(381, 84);
            this.browser6.Name = "browser6";
            this.browser6.Size = new System.Drawing.Size(44, 44);
            this.browser6.TabIndex = 19;
            this.browser6.Tag = "";
            this.browser6.UseVisualStyleBackColor = true;
            this.browser6.Click += new System.EventHandler(this.browsers_Click);
            this.browser6.DragDrop += new System.Windows.Forms.DragEventHandler(this.browsers_DragDrop);
            this.browser6.DragEnter += new System.Windows.Forms.DragEventHandler(this.browsers_DragEnter);
            this.browser6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.browsers_MouseDown);
            // 
            // browser5
            // 
            this.browser5.AllowDrop = true;
            this.browser5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.browser5.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.browser5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browser5.Location = new System.Drawing.Point(336, 84);
            this.browser5.Name = "browser5";
            this.browser5.Size = new System.Drawing.Size(44, 44);
            this.browser5.TabIndex = 18;
            this.browser5.Tag = "";
            this.browser5.UseVisualStyleBackColor = true;
            this.browser5.Click += new System.EventHandler(this.browsers_Click);
            this.browser5.DragDrop += new System.Windows.Forms.DragEventHandler(this.browsers_DragDrop);
            this.browser5.DragEnter += new System.Windows.Forms.DragEventHandler(this.browsers_DragEnter);
            this.browser5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.browsers_MouseDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 22;
            this.label5.Text = "&local dir";
            this.label5.Click += new System.EventHandler(this.label_Click);
            // 
            // browser4
            // 
            this.browser4.AllowDrop = true;
            this.browser4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.browser4.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.browser4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browser4.Location = new System.Drawing.Point(291, 84);
            this.browser4.Name = "browser4";
            this.browser4.Size = new System.Drawing.Size(44, 44);
            this.browser4.TabIndex = 17;
            this.browser4.Tag = "";
            this.browser4.UseVisualStyleBackColor = true;
            this.browser4.Click += new System.EventHandler(this.browsers_Click);
            this.browser4.DragDrop += new System.Windows.Forms.DragEventHandler(this.browsers_DragDrop);
            this.browser4.DragEnter += new System.Windows.Forms.DragEventHandler(this.browsers_DragEnter);
            this.browser4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.browsers_MouseDown);
            // 
            // browser3
            // 
            this.browser3.AllowDrop = true;
            this.browser3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.browser3.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.browser3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browser3.Location = new System.Drawing.Point(246, 84);
            this.browser3.Name = "browser3";
            this.browser3.Size = new System.Drawing.Size(44, 44);
            this.browser3.TabIndex = 16;
            this.browser3.Tag = "";
            this.browser3.UseVisualStyleBackColor = true;
            this.browser3.Click += new System.EventHandler(this.browsers_Click);
            this.browser3.DragDrop += new System.Windows.Forms.DragEventHandler(this.browsers_DragDrop);
            this.browser3.DragEnter += new System.Windows.Forms.DragEventHandler(this.browsers_DragEnter);
            this.browser3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.browsers_MouseDown);
            // 
            // browser2
            // 
            this.browser2.AllowDrop = true;
            this.browser2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.browser2.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.browser2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browser2.Location = new System.Drawing.Point(201, 84);
            this.browser2.Name = "browser2";
            this.browser2.Size = new System.Drawing.Size(44, 44);
            this.browser2.TabIndex = 15;
            this.browser2.Tag = "";
            this.browser2.UseVisualStyleBackColor = true;
            this.browser2.Click += new System.EventHandler(this.browsers_Click);
            this.browser2.DragDrop += new System.Windows.Forms.DragEventHandler(this.browsers_DragDrop);
            this.browser2.DragEnter += new System.Windows.Forms.DragEventHandler(this.browsers_DragEnter);
            this.browser2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.browsers_MouseDown);
            // 
            // browser1
            // 
            this.browser1.AllowDrop = true;
            this.browser1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.browser1.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.browser1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browser1.Location = new System.Drawing.Point(156, 84);
            this.browser1.Name = "browser1";
            this.browser1.Size = new System.Drawing.Size(44, 44);
            this.browser1.TabIndex = 14;
            this.browser1.Tag = "";
            this.browser1.UseVisualStyleBackColor = true;
            this.browser1.Click += new System.EventHandler(this.browsers_Click);
            this.browser1.DragDrop += new System.Windows.Forms.DragEventHandler(this.browsers_DragDrop);
            this.browser1.DragEnter += new System.Windows.Forms.DragEventHandler(this.browsers_DragEnter);
            this.browser1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.browsers_MouseDown);
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoSize = true;
            this.DescriptionLabel.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.DescriptionLabel.Location = new System.Drawing.Point(59, 166);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(420, 15);
            this.DescriptionLabel.TabIndex = 25;
            this.DescriptionLabel.Text = "SFTP instant uploader; watch the file system, and upload changed files";
            // 
            // UrlLabel
            // 
            this.UrlLabel.AutoSize = true;
            this.UrlLabel.Location = new System.Drawing.Point(44, 68);
            this.UrlLabel.Name = "UrlLabel";
            this.UrlLabel.Size = new System.Drawing.Size(31, 15);
            this.UrlLabel.TabIndex = 10;
            this.UrlLabel.Text = "&URL";
            this.UrlLabel.Click += new System.EventHandler(this.label_Click);
            // 
            // hiddenMenuStrip
            // 
            this.hiddenMenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.hiddenMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hiddenMenu});
            this.hiddenMenuStrip.Location = new System.Drawing.Point(424, 209);
            this.hiddenMenuStrip.Name = "hiddenMenuStrip";
            this.hiddenMenuStrip.Size = new System.Drawing.Size(40, 26);
            this.hiddenMenuStrip.TabIndex = 25;
            this.hiddenMenuStrip.Text = "menuStrip1";
            // 
            // hiddenMenu
            // 
            this.hiddenMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dummyF7,
            this.hidden1,
            this.hidden2,
            this.hidden3,
            this.hidden4,
            this.hidden5,
            this.hidden6,
            this.hidden7,
            this.assignToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.hiddenMenu.Name = "hiddenMenu";
            this.hiddenMenu.Size = new System.Drawing.Size(32, 22);
            this.hiddenMenu.Text = "@";
            // 
            // dummyF7
            // 
            this.dummyF7.Name = "dummyF7";
            this.dummyF7.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.dummyF7.Size = new System.Drawing.Size(174, 22);
            this.dummyF7.Click += new System.EventHandler(this.Browser_Click);
            // 
            // hidden1
            // 
            this.hidden1.Name = "hidden1";
            this.hidden1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.hidden1.Size = new System.Drawing.Size(174, 22);
            this.hidden1.Text = "1";
            this.hidden1.Click += new System.EventHandler(this.hidden_Click);
            // 
            // hidden2
            // 
            this.hidden2.Name = "hidden2";
            this.hidden2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.hidden2.Size = new System.Drawing.Size(174, 22);
            this.hidden2.Text = "2";
            this.hidden2.Click += new System.EventHandler(this.hidden_Click);
            // 
            // hidden3
            // 
            this.hidden3.Name = "hidden3";
            this.hidden3.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.hidden3.Size = new System.Drawing.Size(174, 22);
            this.hidden3.Text = "3";
            this.hidden3.Click += new System.EventHandler(this.hidden_Click);
            // 
            // hidden4
            // 
            this.hidden4.Name = "hidden4";
            this.hidden4.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.hidden4.Size = new System.Drawing.Size(174, 22);
            this.hidden4.Text = "4";
            this.hidden4.Click += new System.EventHandler(this.hidden_Click);
            // 
            // hidden5
            // 
            this.hidden5.Name = "hidden5";
            this.hidden5.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5)));
            this.hidden5.Size = new System.Drawing.Size(174, 22);
            this.hidden5.Text = "5";
            this.hidden5.Click += new System.EventHandler(this.hidden_Click);
            // 
            // hidden6
            // 
            this.hidden6.Name = "hidden6";
            this.hidden6.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D6)));
            this.hidden6.Size = new System.Drawing.Size(174, 22);
            this.hidden6.Text = "6";
            this.hidden6.Click += new System.EventHandler(this.hidden_Click);
            // 
            // hidden7
            // 
            this.hidden7.Name = "hidden7";
            this.hidden7.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D7)));
            this.hidden7.Size = new System.Drawing.Size(174, 22);
            this.hidden7.Text = "7";
            this.hidden7.Click += new System.EventHandler(this.hidden_Click);
            // 
            // assignToolStripMenuItem
            // 
            this.assignToolStripMenuItem.Name = "assignToolStripMenuItem";
            this.assignToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Insert)));
            this.assignToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.assignToolStripMenuItem.Text = "assign";
            this.assignToolStripMenuItem.Click += new System.EventHandler(this.BrowserAddItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Delete)));
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.removeToolStripMenuItem.Text = "remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.BrowserRemoveItem_Click);
            // 
            // SftpLabel
            // 
            this.SftpLabel.AutoSize = true;
            this.SftpLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SftpLabel.Location = new System.Drawing.Point(36, 18);
            this.SftpLabel.Name = "SftpLabel";
            this.SftpLabel.Size = new System.Drawing.Size(37, 15);
            this.SftpLabel.TabIndex = 2;
            this.SftpLabel.Text = "&SFTP";
            this.SftpLabel.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "&host";
            this.label1.Click += new System.EventHandler(this.label_Click);
            // 
            // ServerGroup
            // 
            this.ServerGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ServerGroup.Location = new System.Drawing.Point(30, 15);
            this.ServerGroup.Name = "ServerGroup";
            this.ServerGroup.Size = new System.Drawing.Size(536, 65);
            this.ServerGroup.TabIndex = 0;
            this.ServerGroup.TabStop = false;
            // 
            // ProgressBar
            // 
            this.ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar.Location = new System.Drawing.Point(66, 131);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(500, 4);
            this.ProgressBar.TabIndex = 0;
            this.ProgressBar.TabStop = false;
            this.ProgressBar.Paint += new System.Windows.Forms.PaintEventHandler(this.ProgressBar_Paint);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.AutoWordSelection = true;
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(0, 187);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(584, 95);
            this.richTextBox1.TabIndex = 28;
            this.richTextBox1.Text = "";
            // 
            // UpMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(584, 282);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.TerminalButton);
            this.Controls.Add(this.EditorButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SftpLabel);
            this.Controls.Add(this.hiddenMenuStrip);
            this.Controls.Add(this.linkText1);
            this.Controls.Add(this.RemoteRoot);
            this.Controls.Add(this.RemoteHost);
            this.Controls.Add(this.Pass);
            this.Controls.Add(this.ExplorerButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.User);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.UrlLabel);
            this.Controls.Add(this.AutoCheck);
            this.Controls.Add(this.ManualButton);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.browser1);
            this.Controls.Add(this.browser2);
            this.Controls.Add(this.browser3);
            this.Controls.Add(this.browser4);
            this.Controls.Add(this.browser5);
            this.Controls.Add(this.browser6);
            this.Controls.Add(this.browser7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LocalRoot);
            this.Controls.Add(this.DescriptionLabel);
            this.Controls.Add(this.ServerGroup);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(100, 50);
            this.MainMenuStrip = this.hiddenMenuStrip;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 263);
            this.Name = "UpMain";
            this.Text = "UpASAP ver0.83 alpha";
            this.Activated += new System.EventHandler(this.UpMain_Activated);
            this.Deactivate += new System.EventHandler(this.UpMain_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UpMain_FormClosed);
            this.Move += new System.EventHandler(this.event_Changed);
            this.Resize += new System.EventHandler(this.event_Changed);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.hiddenMenuStrip.ResumeLayout(false);
            this.hiddenMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProgressBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem AboutItem;
        private System.Windows.Forms.ToolStripMenuItem StartItem;
        private System.Windows.Forms.ToolStripMenuItem ManuallyItem;
        private System.Windows.Forms.ToolStripMenuItem BrowserMenu;
        private System.Windows.Forms.ToolStripMenuItem BrowserItem;
        private System.Windows.Forms.ToolStripMenuItem BrowserAddItem;
        private System.Windows.Forms.ToolStripMenuItem BrowserRemoveItem;
        private System.Windows.Forms.ToolStripMenuItem CopyUrlItem;
        private System.Windows.Forms.ToolStripMenuItem EditUrlItem;
        private System.Windows.Forms.ToolStripMenuItem ExplorerItem;
        private System.Windows.Forms.ToolStripMenuItem TerminalItem;
        private System.Windows.Forms.ToolStripMenuItem TerminalSettingsItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem NewSettItem;
        private System.Windows.Forms.ToolStripMenuItem OpenSettItem;
        private System.Windows.Forms.ToolStripMenuItem SaveSettItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem QuitItem;
        private System.Windows.Forms.ToolStripMenuItem clearLogItem;
        private System.Windows.Forms.ToolStripMenuItem SelectLocalItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ComboBox RemoteHost;
        private System.Windows.Forms.TextBox RemoteRoot;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox User;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Pass;
        private System.Windows.Forms.Button TerminalButton;
        private System.Windows.Forms.Label UrlLabel;
        private LinkText linkText1;
        private System.Windows.Forms.CheckBox AutoCheck;
        private System.Windows.Forms.Button ManualButton;
        private System.Windows.Forms.PictureBox ProgressBar;
        private System.Windows.Forms.Button browser1;
        private System.Windows.Forms.Button browser2;
        private System.Windows.Forms.Button browser3;
        private System.Windows.Forms.Button browser4;
        private System.Windows.Forms.Button browser5;
        private System.Windows.Forms.Button browser6;
        private System.Windows.Forms.Button browser7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox LocalRoot;
        private System.Windows.Forms.Button ExplorerButton;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.MenuStrip hiddenMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem hiddenMenu;
        private System.Windows.Forms.ToolStripMenuItem dummyF7;
        private System.Windows.Forms.ToolStripMenuItem hidden1;
        private System.Windows.Forms.ToolStripMenuItem hidden2;
        private System.Windows.Forms.ToolStripMenuItem hidden3;
        private System.Windows.Forms.ToolStripMenuItem hidden4;
        private System.Windows.Forms.ToolStripMenuItem hidden5;
        private System.Windows.Forms.ToolStripMenuItem hidden6;
        private System.Windows.Forms.ToolStripMenuItem hidden7;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem PingAllItem;
        private System.Windows.Forms.ToolStripMenuItem assignToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label SftpLabel;
        private System.Windows.Forms.GroupBox ServerGroup;
        private System.Windows.Forms.ToolStripMenuItem EditorItem;
        private System.Windows.Forms.ToolStripMenuItem EditorSettingsItem;
        private System.Windows.Forms.Button EditorButton;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

