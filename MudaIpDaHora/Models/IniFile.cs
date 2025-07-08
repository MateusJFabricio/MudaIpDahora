using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace MudaIpDahora.Models
{
    public class IniFile   // revision 11
    {
        string Path;
        string Directory = Environment.CurrentDirectory;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public IniFile(string fileName = null)
        {
            Path = Directory + "\\" + fileName;
            System.IO.Directory.CreateDirectory(Directory);
            if (!File.Exists(Path))
            {
                var f = File.Create(Path);
                f.Close();
            }
        }

        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? Directory, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }

        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? Directory, Key, Value, Path);
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