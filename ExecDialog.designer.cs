namespace UpASAP
{
    partial class ExecDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.OKButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.exelabel = new System.Windows.Forms.Label();
            this.optLabel = new System.Windows.Forms.Label();
            this.ExeText = new System.Windows.Forms.TextBox();
            this.OptionsText = new System.Windows.Forms.TextBox();
            this.ExecButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.TemplateCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.NameText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(204, 227);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 12;
            this.OKButton.Text = "&OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(285, 227);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // exelabel
            // 
            this.exelabel.Location = new System.Drawing.Point(12, 69);
            this.exelabel.Name = "exelabel";
            this.exelabel.Size = new System.Drawing.Size(86, 15);
            this.exelabel.TabIndex = 7;
            this.exelabel.Text = ".exe filename";
            this.exelabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // optLabel
            // 
            this.optLabel.Location = new System.Drawing.Point(12, 95);
            this.optLabel.Name = "optLabel";
            this.optLabel.Size = new System.Drawing.Size(86, 15);
            this.optLabel.TabIndex = 9;
            this.optLabel.Text = "options";
            this.optLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ExeText
            // 
            this.ExeText.Location = new System.Drawing.Point(100, 66);
            this.ExeText.Name = "ExeText";
            this.ExeText.Size = new System.Drawing.Size(468, 23);
            this.ExeText.TabIndex = 8;
            this.toolTip1.SetToolTip(this.ExeText, "アプリケーションの実行ファイル名をフルパスで指定します。");
            // 
            // OptionsText
            // 
            this.OptionsText.Location = new System.Drawing.Point(100, 92);
            this.OptionsText.Name = "OptionsText";
            this.OptionsText.Size = new System.Drawing.Size(468, 23);
            this.OptionsText.TabIndex = 10;
            this.toolTip1.SetToolTip(this.OptionsText, "実行時のコマンドラインオプションを指定します。");
            // 
            // ExecButton
            // 
            this.ExecButton.Location = new System.Drawing.Point(51, 227);
            this.ExecButton.Name = "ExecButton";
            this.ExecButton.Size = new System.Drawing.Size(107, 23);
            this.ExecButton.TabIndex = 11;
            this.ExecButton.Text = "Try &Execute";
            this.toolTip1.SetToolTip(this.ExecButton, "指定したアプリケーションの実行を試みます。");
            this.ExecButton.UseVisualStyleBackColor = true;
            this.ExecButton.Click += new System.EventHandler(this.execButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(394, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "-";
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoSize = true;
            this.DescriptionLabel.Location = new System.Drawing.Point(12, 9);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(70, 15);
            this.DescriptionLabel.TabIndex = 0;
            this.DescriptionLabel.Text = "select .exe";
            // 
            // TemplateCombo
            // 
            this.TemplateCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TemplateCombo.FormattingEnabled = true;
            this.TemplateCombo.Items.AddRange(new object[] {
            "Google Chrome",
            "Mozilla Firefox",
            "IE",
            "edge"});
            this.TemplateCombo.Location = new System.Drawing.Point(447, 6);
            this.TemplateCombo.Name = "TemplateCombo";
            this.TemplateCombo.Size = new System.Drawing.Size(121, 23);
            this.TemplateCombo.TabIndex = 2;
            this.toolTip1.SetToolTip(this.TemplateCombo, "起動するプログラムのテンプレートを選んでください。");
            this.TemplateCombo.SelectedIndexChanged += new System.EventHandler(this.TemplateCombo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(375, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "templates";
            // 
            // NameText
            // 
            this.NameText.Location = new System.Drawing.Point(100, 40);
            this.NameText.Name = "NameText";
            this.NameText.Size = new System.Drawing.Size(229, 23);
            this.NameText.TabIndex = 5;
            this.toolTip1.SetToolTip(this.NameText, "アプリケーションをこの名前で登録します。");
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // RemoveButton
            // 
            this.RemoveButton.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.RemoveButton.Location = new System.Drawing.Point(377, 227);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveButton.TabIndex = 14;
            this.RemoveButton.Text = "&Remove";
            this.toolTip1.SetToolTip(this.RemoveButton, "アプリケーションの登録を解除します。");
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Info;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.textBox1.Location = new System.Drawing.Point(100, 121);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(328, 100);
            this.textBox1.TabIndex = 16;
            this.textBox1.Text = "options variable expansions:\r\n${ip}=${host}=192.168.0.xxx\r\n${user}=root, ${pass}=" +
    "****\r\n${url}=http://hostname/index.hrml\r\n${local}= ${dir}=c:\\Users\\me\\web\r\n${ful" +
    "lpath}=c:\\Users\\me\\web\\index.html";
            this.toolTip1.SetToolTip(this.textBox1, "コマンドラインオプションに指定できるマクロの例");
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(472, 130);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "export templ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ExecDialog
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(584, 262);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.NameText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TemplateCombo);
            this.Controls.Add(this.DescriptionLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ExecButton);
            this.Controls.Add(this.OptionsText);
            this.Controls.Add(this.ExeText);
            this.Controls.Add(this.optLabel);
            this.Controls.Add(this.exelabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.OKButton);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExecDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UpASAP";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label exelabel;
        private System.Windows.Forms.Label optLabel;
        private System.Windows.Forms.TextBox ExeText;
        private System.Windows.Forms.TextBox OptionsText;
        private System.Windows.Forms.Button ExecButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox TemplateCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox NameText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}