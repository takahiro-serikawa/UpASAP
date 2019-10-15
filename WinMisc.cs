using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace UpASAP
{
    class WinMisc
    {
        public static void RegisterExt(string ext, string description)
        {
            if (ext[0] != '.')
                ext = "." + ext;
            string commandline = "\"" + Application.ExecutablePath + "\" \"%1\"";
            string fileType = Application.ProductName + ".0";
            string verb = "open";
            string verbDescription = "MyApplicationで開く(&O)";

            string iconPath = Application.ExecutablePath;
            int iconIndex = 0;

            using (var root = Microsoft.Win32.Registry.ClassesRoot) {
                using (var regkey = root.CreateSubKey(ext, Microsoft.Win32.RegistryKeyPermissionCheck.Default))
                    regkey.SetValue("", fileType);

                using (var typekey = root.CreateSubKey(fileType))
                    typekey.SetValue("", description);

                using (var verblkey = root.CreateSubKey(fileType + "\\shell\\" + verb))
                    verblkey.SetValue("", verbDescription);

                using (var cmdkey = root.CreateSubKey(fileType + "\\shell\\" + verb + "\\command"))
                    cmdkey.SetValue("", commandline);

                using (var iconkey = root.CreateSubKey(fileType + "\\DefaultIcon"))
                    iconkey.SetValue("", iconPath + "," + iconIndex.ToString());
            }
        }

        public static void UnRegisterExt(string ext)
        {
            string fileType = Application.ProductName;

            Microsoft.Win32.Registry.ClassesRoot.DeleteSubKeyTree(ext);
            Microsoft.Win32.Registry.ClassesRoot.DeleteSubKeyTree(fileType);
        }

    }
}
