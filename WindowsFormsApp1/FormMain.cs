using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;


namespace WindowsFormsApp1
{

    public partial class FormMain : Form
    {
        List<Placa> placas = new List<Placa>();
        public FormMain()
        {
            InitializeComponent();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            AtualizarPlacas();
        }

        private void AtualizarPlacas()
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                try
                {
                    var ipProperties = ni.GetIPProperties();
                    var ipInfo = ipProperties.UnicastAddresses.FirstOrDefault(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork);
                    if (ipInfo == null)
                        continue;

                    var currentIPaddress = ipInfo.Address.ToString();
                    var currentSubnetMask = ipInfo.IPv4Mask.ToString();

                    var placa = new Placa
                    {
                        Id = ni.Id,
                        Nome = ni.Name,
                        Descricao = ni.Description
                    };

                    placa.SetIpAddr(currentIPaddress);
                    placa.SetSubNet(currentSubnetMask);
                    placas.Add(placa);
                }
                catch
                {

                }
                
            }

            cbPlacas.Items.Clear();
            foreach (var p in placas)
            {
                cbPlacas.Items.Add(p.Descricao);
            }
        }

        public bool SetIP(string networkInterfaceName, string ipAddress, string subnetMask, string gateway = null)
        {
            var networkInterface = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(nw => nw.Name == networkInterfaceName);
            var ipProperties = networkInterface.GetIPProperties();
            var ipInfo = ipProperties.UnicastAddresses.FirstOrDefault(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork);
            var currentIPaddress = ipInfo.Address.ToString();
            var currentSubnetMask = ipInfo.IPv4Mask.ToString();
            var isDHCPenabled = ipProperties.GetIPv4Properties().IsDhcpEnabled;

            if (!isDHCPenabled && currentIPaddress == ipAddress && currentSubnetMask == subnetMask)
                return true;    // no change necessary

            var process = new Process
            {
                StartInfo = new ProcessStartInfo("netsh", $"interface ip set address \"{networkInterfaceName}\" static {ipAddress} {subnetMask}" + (string.IsNullOrWhiteSpace(gateway) ? "" : $"{gateway} 1")) { Verb = "runas" }
            };
            process.Start();
            process.WaitForExit();
            var successful = process.ExitCode == 0;
            process.Dispose();
            return successful;
        }

        private void btnSetIP_Click(object sender, EventArgs e)
        {
            string ip, subnet;
            if (ValidaIp(txtIP_1.Text, txtIP_2.Text, txtIP_3.Text, txtIP_4.Text, out ip))
            {
                if (ValidaIp(txtSubNet_1.Text, txtSubNet_2.Text, txtSubNet_3.Text, txtSubNet_4.Text, out subnet))
                {
                    SetIP(placas[cbPlacas.SelectedIndex].Nome, ip, subnet);
                }
                else
                    MessageBox.Show("IP Invalido");
            }
            else
                MessageBox.Show("IP Invalido");

        }

        private bool ValidaIp(string text1, string text2, string text3, string text4, out string valorIp)
        {
            IPAddress iP;
            valorIp = text1 + "." + text2 + "." + text3 + "." + text4;
            return IPAddress.TryParse(valorIp, out iP);
        }

        private void cbPlacas_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var p in placas)
            {
                if (cbPlacas.Text == p.Descricao)
                {
                    txtIP_1.Text = p.IpAddr[0];
                    txtIP_2.Text = p.IpAddr[1];
                    txtIP_3.Text = p.IpAddr[2];
                    txtIP_4.Text = p.IpAddr[3];

                    txtSubNet_1.Text = p.SubNetAddr[0];
                    txtSubNet_2.Text = p.SubNetAddr[1];
                    txtSubNet_3.Text = p.SubNetAddr[2];
                    txtSubNet_4.Text = p.SubNetAddr[3];
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnAtualizar_Click(sender, e);
            if (placas.Count > 0)
                cbPlacas.SelectedIndex = 0;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            if (ShowInTaskbar)
                this.ShowInTaskbar = false;
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            if (!ShowInTaskbar)
                this.ShowInTaskbar = true;
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.BringToFront();
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            CreateShortcut();
        }

        private static void CreateShortcut()
        {
            string link = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + Path.DirectorySeparatorChar + Application.ProductName + ".lnk";
            var shell = new WshShell();
            var shortcut = shell.CreateShortcut(link) as IWshShortcut;
            shortcut.Hotkey = "Ctrl+Shift+M";
            shortcut.TargetPath = Application.ExecutablePath;
            shortcut.WorkingDirectory = Application.StartupPath;
            shortcut.Save();
        }

    }

    class Placa
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string IP { get => ipAddr[0] + "." + ipAddr[1] + "." + ipAddr[2] + "." + ipAddr[3]; }
        public string Subnet { get => subnet[0] + "." + subnet[1] + "." + subnet[2] + "." + subnet[3]; }

        private string[] ipAddr = new string[4], subnet = new string[4];
        public string[] IpAddr { get => ipAddr; }
        public string[] SubNetAddr { get => subnet; }

        public void SetIpAddr(string ip)
        {
            ipAddr = new string[4];
            ipAddr = convertIpToArray(ip);
        }

        public void SetSubNet(string ip)
        {
            subnet = new string[4];
            subnet = convertIpToArray(ip);
        }

        private string[] convertIpToArray(string valor)
        {
            string[] retorno = new string[4];
            int tamanho = 0;

            for (int i = 0; i < 4; i++)
            {
                if (valor.Contains("."))
                {
                    retorno[i] = valor.Substring(0, valor.IndexOf("."));
                }else
                {
                    retorno[i] = valor.Substring(0, valor.Length);
                    break;
                }
                    

                tamanho = valor.Length - (retorno[i] + 1).Length;

                valor = valor.Substring(valor.IndexOf(retorno[i]) + retorno[i].Length + 1, tamanho);
            }

            return retorno;
        }
    }
}
