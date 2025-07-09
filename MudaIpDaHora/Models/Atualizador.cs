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
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;

namespace MudaIpDahora.Models
{
    public class Atualizador
    {
        //Properties
        public string ShortcutPath { get; set; } = "";
        private string nomeDownload;
        public Release Release = new Release();
        public Download Download { get; internal set; } = new Download();
        public Queue<Log> Log { get; internal set; } = new Queue<Log>();

        //Events
        public event EventHandler NovaVersaoEncontradaEvent;
        protected virtual void OnNovaVersaoEncontrada(EventArgs e)
        {
            NovaVersaoEncontradaEvent?.Invoke(this, e);
        }

        //Methods
        public bool LocalizarUltimaRelease()
        {
            Log.Enqueue(new Log { StepName = "LocalizarUltimaRelease", Text = "Inicio" });
            Thread.Sleep(1000);
            int ultimaVersao = 0;
            var client = new RestClient("https://api.github.com/repos/MateusJFabricio/MudaIpDahora/releases");

            RestRequest request = new RestRequest("https://api.github.com/repos/MateusJFabricio/MudaIpDahora/releases");
            var response = client.Execute(request);
            Log.Enqueue(new Log { StepName = "LocalizarUltimaRelease", Text = "Analiza resposta do servidor GIT" });
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Log.Enqueue(new Log { StepName = "LocalizarUltimaRelease", Text = "Recebido status OK" });
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
                Log.Enqueue(new Log { StepName = "LocalizarUltimaRelease", Text = "Fim" });
                return true;
            }else
            {
                Log.Enqueue(new Log { StepName = "LocalizarUltimaRelease", Text = "Recebido status: " + response.StatusCode.ToString() });
                Log.Enqueue(new Log { StepName = "LocalizarUltimaRelease", Text = "Fim"});
                throw new Exception("Nao foi possivel verificar se há uma atualizacao. Verifique se voce esta com internet");
            }
        }
        public Task<bool> NovaVersaoEncontradaAsync()
        {
            return Task.Run(NovaVersaoEncontrada);
        }
        public bool NovaVersaoEncontrada()
        {
            try
            {
                if (LocalizarUltimaRelease())
                {
                    if (CompararVersao())
                    {
                        OnNovaVersaoEncontrada(new EventArgs());
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        public bool CompararVersao()
        {
            Log.Enqueue(new Log { StepName = "CompararVersao", Text = "Inicio" });
            
            Thread.Sleep(1000);
            Version version = new Version(Application.ProductVersion);
            
            Log.Enqueue(new Log { StepName = "CompararVersao", Text = "Versão atual " + version.ToString() });
            
            Version versaoDownload = new Version(
                Release.tag_name.Substring(0, 1) + "." +
                Release.tag_name.Substring(1, 1) + "." +
                Release.tag_name.Substring(2, 1) + "." +
                Release.tag_name.Substring(3, 1)
                );
            Log.Enqueue(new Log { StepName = "CompararVersao", Text = "Versão comparada " + versaoDownload.ToString() });
            
            return version.CompareTo(versaoDownload) < 0;
        }

        public bool DownloadComponentes()
        {
            Log.Enqueue(new Log { StepName = "DownloadComponentes", Text = "Inicio" });
            Log.Enqueue(new Log { StepName = "DownloadComponentes", Text = "Qnt assets econtrados: " + Release.Assets.Count.ToString() });
            Log.Enqueue(new Log { StepName = "DownloadComponentes", Text = "Iniciando iteração nos assets" });
            Thread.Sleep(1000);
            foreach (var asset in Release.Assets)
            {
                using (var cliente = new WebClient())
                {
                    nomeDownload = "MudaIpDaHora_V" + Release.tag_name.ToString() + ".exe";

                    cliente.DownloadProgressChanged += Cliente_DownloadProgressChanged;
                    cliente.DownloadFileCompleted += Cliente_DownloadFileCompleted;

                    if (asset.name.Contains(".exe"))
                    {
                        //cliente.DownloadFile(new Uri(asset.browser_download_url), nomeDownload);
                        Log.Enqueue(new Log { StepName = "DownloadComponentes", Text = "Download do asset: " + nomeDownload });
                        Download.Init(nomeDownload);
                        cliente.DownloadFileTaskAsync(new Uri(asset.browser_download_url), nomeDownload);
                        
                        while (!Download.Finished)
                        {
                            Thread.Sleep(10);
                        }
                    }
                    else
                    {
                        if (!System.IO.File.Exists(asset.name))
                        {
                            Log.Enqueue(new Log { StepName = "DownloadComponentes", Text = "Download do asset: " + asset.name });
                            Download.Init(asset.name);
                            cliente.DownloadFileAsync(new Uri(asset.browser_download_url), asset.name);
                            while (!Download.Finished)
                            {
                                Thread.Sleep(10);
                            }
                        }
                    }

                }
            }
            Log.Enqueue(new Log { StepName = "DownloadComponentes", Text = "Finalizado download dos assets"});

            return true;
        }

        private void Cliente_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Log.Enqueue(new Log { StepName = "DownloadComponentes", Text = "Download concluido" });
            Download.Finished = true;
        }

        private void Cliente_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Download.TotalBytes = e.TotalBytesToReceive;
            Download.BytesDownloaded = e.BytesReceived;
            Download.Progress = e.ProgressPercentage;
        }

        public bool IniciarInstalador()
        {
            try
            {
                Log.Enqueue(new Log { StepName = "IniciarInstalador", Text = "Inicio" });
                Process process = new Process
                {
                    StartInfo = new ProcessStartInfo
                        { 
                            FileName = nomeDownload,
                            UseShellExecute = true,
                            Verb = "runas"
                        }
                };

                Log.Enqueue(new Log { StepName = "IniciarInstalador", Text = "Start do processo " + nomeDownload });
                process.Start();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool CriarAtalho(bool silent)
        {
            Log.Enqueue(new Log { StepName = "CriarAtalho", Text = "Inicio" });
            Thread.Sleep(1000);
            string link = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + Path.DirectorySeparatorChar + Application.ProductName + ".lnk";
            Log.Enqueue(new Log { StepName = "CriarAtalho", Text = "Criar link do atalho: " + link});
            ShortcutPath = link;
            var shell = new WshShell();
            var shortcut = shell.CreateShortcut(link) as IWshShortcut;
            shortcut.TargetPath = Application.StartupPath + "\\" + nomeDownload;
            shortcut.WorkingDirectory = Application.StartupPath;
            shortcut.Save();

            if (!silent)
            {
                Log.Enqueue(new Log { StepName = "CriarAtalho", Text = "Iniciando a aplicação"});
                Process.Start(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
            }

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

    public class Download
    {
        public int Progress { get; set; }
        public string Name { get; set; }
        public long TotalBytes { get; set; }
        public long BytesDownloaded { get; set; }
        public bool Finished { get; set; } = true;
        public void Init(string name)
        {
            Finished = false;
            Name = name;
        }
        public void Init()
        {
            Finished = false;
        }
    }
}
