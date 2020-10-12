using IWshRuntimeLibrary;
using MudaIpDahora;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Windows.Forms;


namespace WindowsFormsApp1
{

    public partial class FormMain : Form
    {
        List<Placa> placas = new List<Placa>();
        private ContextMenu contextMenu;
        private MenuItem menuItem;
        public FormMain()
        {
            InitializeComponent();
            this.contextMenu = new ContextMenu();
            this.menuItem = new MenuItem();
            // Initialize contextMenu
            this.contextMenu.MenuItems.AddRange(
                        new MenuItem[] { this.menuItem });

            // Initialize menuItem1
            this.menuItem.Index = 0;
            this.menuItem.Text = "E&xit";
            this.menuItem.Click += new EventHandler(this.menuItem1_Click);
            notifyIcon.ContextMenu = this.contextMenu;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            AtualizarPlacas();
        }
        private void menuItem1_Click(object Sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AtualizarPlacas()
        {
            placas.Clear();
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if ((ni.NetworkInterfaceType != NetworkInterfaceType.Wireless80211 && ni.OperationalStatus != OperationalStatus.Up) ||
                    (ni.NetworkInterfaceType != NetworkInterfaceType.Ethernet && ni.OperationalStatus != OperationalStatus.Up)) continue;

                try
                {
                    var ipProperties = ni.GetIPProperties();
                    var placa = new Placa();

                    if (!ipProperties.GetIPv4Properties().IsDhcpEnabled)
                    {
                        var ipInfo = ipProperties.UnicastAddresses.FirstOrDefault(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork);

                        if (ipInfo == null)
                            continue;

                        var currentIPaddress = ipInfo.Address.ToString();
                        var currentSubnetMask = ipInfo.IPv4Mask.ToString();

                        placa.SetIpAddr(currentIPaddress);
                        placa.SetSubNet(currentSubnetMask);
                    }

                    placa.Nome = ni.Name;
                    placa.Descricao = ni.Description;
                    placa.DhcpEnable = ipProperties.GetIPv4Properties().IsDhcpEnabled;

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

            if (placas.Count <= 0)
            {
                gpConfig.Enabled = false;
                cbDhcp.Enabled = false;
            }
            else
            {
                cbPlacas.SelectedIndex = 1;
                gpConfig.Enabled = true;
                cbDhcp.Enabled = true;
            }
        }

        public bool SetIP(string nomePlaca, string ipAddress, string subnetMask, string gateway = null)
        {

            var process = new Process
            {
                StartInfo = new ProcessStartInfo("netsh", $"interface ip set address \"{nomePlaca}\" static {ipAddress} {subnetMask}" + (string.IsNullOrWhiteSpace(gateway) ? "" : $"{gateway} 1")) { Verb = "runas" }
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

                    gpConfig.Enabled = !p.DhcpEnable;
                    cbDhcp.Checked = p.DhcpEnable;
                    cbDhcp.Refresh();
                }                
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            btnAtualizar_Click(sender, e);
            if (placas.Count > 0)
            {
                cbPlacas.SelectedIndex = 0;
                gpConfig.Enabled = true;
            }else
                gpConfig.Enabled = false;

        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            if (e.CloseReason == CloseReason.ApplicationExitCall)
                Process.GetCurrentProcess().Kill();
            else
                e.Cancel = true;
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Show();
                this.BringToFront();
            }
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

        private void btnInfo_Click(object sender, EventArgs e)
        {
            new FormInfo().ShowDialog();
        }

        private void cbDhcp_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void EnableDhcp()
        {
            if (!placas[cbPlacas.SelectedIndex].DhcpEnable)
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo("netsh", "interface ip set address \"" + placas[cbPlacas.SelectedIndex].Nome + "\" source=dhcp") { Verb = "runas" }
                };
                process.Start();
                process.WaitForExit();
                var successful = process.ExitCode == 0;
                process.Dispose();
                gpConfig.Enabled = false;
            }
            else
            {
                txtIP_1.Clear();
                txtIP_2.Clear();
                txtIP_3.Clear();
                txtIP_4.Clear();

                txtSubNet_1.Text = "255";
                txtSubNet_2.Text = "255";
                txtSubNet_3.Text = "255";
                txtSubNet_4.Text = "0";

                gpConfig.Enabled = true;
            }

            placas[cbPlacas.SelectedIndex].DhcpEnable = !placas[cbPlacas.SelectedIndex].DhcpEnable;
        }

        private void cbDhcp_Click(object sender, EventArgs e)
        {

        }

        private void cbDhcp_MouseCaptureChanged(object sender, EventArgs e)
        {
            EnableDhcp();
        }

        private void cbPlacas_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }
    }

    class Placa
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string IP { get => IpAddr[0] + "." + IpAddr[1] + "." + IpAddr[2] + "." + IpAddr[3]; }
        public string Subnet { get => SubNetAddr[0] + "." + SubNetAddr[1] + "." + SubNetAddr[2] + "." + SubNetAddr[3]; }
        public string[] IpAddr { get; private set; } = new string[4];
        public string[] SubNetAddr { get; private set; } = new string[4];
        public bool DhcpEnable { get; set; }

        public void SetIpAddr(string ip)
        {
            IpAddr = new string[4];
            IpAddr = convertIpToArray(ip);
        }

        public void SetSubNet(string ip)
        {
            SubNetAddr = new string[4];
            SubNetAddr = convertIpToArray(ip);
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
