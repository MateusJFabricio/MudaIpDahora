using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace MudaIpDahora.Models
{
    public class IniFile   // revision 11
    {
        string FilePath;
        string Directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "mudaipdahora");

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public IniFile(string fileName = null)
        {
            FilePath = Directory + "\\" + fileName;
            System.IO.Directory.CreateDirectory(Directory);
            if (!File.Exists(FilePath))
            {
                var f = File.Create(FilePath);
                f.Close();
            }
        }

        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? Directory, Key, "", RetVal, 255, FilePath);
            return RetVal.ToString();
        }

        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? Directory, Key, Value, FilePath);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? Directory);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? Directory);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
    }
}