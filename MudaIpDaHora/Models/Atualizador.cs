using IWshRuntimeLibrary;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace MudaIpDahora.Models
{
    public class Atualizador
    {
        private string nomeDownload;
        public Release Release = new Release();
        public bool LocalizarUltimaRelease()
        {
            Thread.Sleep(1000);
            int ultimaVersao = 0;
            var client = new RestClient("https://api.github.com/repos/MateusJFabricio/MudaIpDahora/releases");

            RestRequest request = new RestRequest("https://api.github.com/repos/MateusJFabricio/MudaIpDahora/releases");
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                List<Release> releases = JsonConvert.DeserializeObject<List<Release>>(response.Content);

                foreach (var release in releases)
                {
                    int versao;
                    if (release.tag_name.Length >= 4)
                    {
                        if (int.TryParse(release.tag_name.Substring(0, 4), out versao))
                        {
                            if (ultimaVersao < versao)
                            {
                                ultimaVersao = versao;
                                Release = release;
                            }
                        }
                    }
                }
                return true;
            }else
            {
                throw new Exception("Nao foi possivel verificar se há uma atualizacao. Verifique se voce esta com internet");
            }
        }

        public bool CompararVersao()
        {
            Thread.Sleep(1000);
            Version version = new Version(Application.ProductVersion);
            Version versaoDownload = new Version(
                Release.tag_name.Substring(0, 1) + "." +
                Release.tag_name.Substring(1, 1) + "." +
                Release.tag_name.Substring(2, 1) + "." +
                Release.tag_name.Substring(3, 1)
                );

            return version.CompareTo(versaoDownload) < 0;
        }

        public bool DownloadComponentes()
        {
            Thread.Sleep(1000);
            foreach (var asset in Release.Assets)
            {
                using (var cliente = new WebClient())
                {
                    nomeDownload = "MudaIpDaHora_V" + Release.tag_name.ToString() + ".exe";

                    if (asset.name.Contains(".exe"))
                        cliente.DownloadFileAsync(new Uri(asset.browser_download_url), nomeDownload);
                    else
                    {
                        if (!System.IO.File.Exists(asset.name))
                            cliente.DownloadFileAsync(new Uri(asset.browser_download_url), asset.name);
                    }

                }
            }

            return true;
        }

        public bool CriarAtalho()
        {
            Thread.Sleep(1000);
            string link = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + Path.DirectorySeparatorChar + Application.ProductName + ".lnk";
            var shell = new WshShell();
            var shortcut = shell.CreateShortcut(link) as IWshShortcut;
            shortcut.TargetPath = Application.StartupPath + "\\" + nomeDownload;
            shortcut.WorkingDirectory = Application.StartupPath;
            shortcut.Save();

            Process.Start(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));

            return true;
        }

    }
    public class Release
    {
        public string tag_name { get; set; }
        public bool prerelease { get; set; }
        public List<Assets> Assets { get; set; } = new List<Assets>();
    }

    public class Assets
    {
        public string name { get; set; }
        public string browser_download_url { get; set; }
    }
}
