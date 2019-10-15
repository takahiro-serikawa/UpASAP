using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpASAP
{
    public partial class UploadDialog: Form
    {
        public UploadDialog()
        {
            InitializeComponent();

            string msg = "確認付き一括コピー\r\n"
             + "指定フォルダ内のファイルをサブフォルダを含めて一括アップロード\r\n"
             + "・シンボリックリンク考慮しない\r\n"
             + "・ディレクトリにファイルを上書きしようとした、\r\n"
             + "・アップロードもとにないファイルを削除しない\r\n"
             + "・ディレクトリにファイルを上書きしようとした、\r\n"
             + "\r\n";

            textBox2.Text = msg;
        }

        public string FileName
        {
            set { textBox1.Text = value; }
        }

        public bool CopyIfNewer
        {
            get { return radioButton2.Checked; }
            set
            {
                if (value)
                    radioButton2.Checked = true;
                else
                    radioButton1.Checked = true;
            }
        }
    }
}
