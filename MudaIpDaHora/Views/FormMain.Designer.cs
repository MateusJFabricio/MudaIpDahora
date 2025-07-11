﻿namespace MudaIpDahora.Views
{
    partial class FormMain
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.cbPlacas = new System.Windows.Forms.ComboBox();
            this.txtIP_1 = new System.Windows.Forms.TextBox();
            this.txtIP_2 = new System.Windows.Forms.TextBox();
            this.txtIP_3 = new System.Windows.Forms.TextBox();
            this.txtIP_4 = new System.Windows.Forms.TextBox();
            this.txtSubNet_4 = new System.Windows.Forms.TextBox();
            this.txtSubNet_3 = new System.Windows.Forms.TextBox();
            this.txtSubNet_2 = new System.Windows.Forms.TextBox();
            this.txtSubNet_1 = new System.Windows.Forms.TextBox();
            this.gpConfig = new System.Windows.Forms.GroupBox();
            this.btnSetIP = new System.Windows.Forms.Button();
            this.pnlIpAndMask = new System.Windows.Forms.Panel();
            this.lblMascara = new System.Windows.Forms.Label();
            this.lblIP = new System.Windows.Forms.Label();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.btnSalvar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.btnDHCP_Toogle = new System.Windows.Forms.ToolStripButton();
            this.label3 = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRecolher = new System.Windows.Forms.Button();
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.dgvListaIps = new System.Windows.Forms.DataGridView();
            this.Nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPlaca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DHCP = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mascara = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ferramentasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inicializarComWindowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gerenciadorDePlacasDeRedeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRebootApp = new System.Windows.Forms.ToolStripMenuItem();
            this.sobreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dadosDoProjetoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.atualizacaoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tabPageProfinet = new System.Windows.Forms.TabPage();
            this.dgvProfinet = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnRefreshProfinetDevice = new System.Windows.Forms.ToolStripButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listBoxScanner = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTimeout = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnStartScanner = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.progressBarScanner = new System.Windows.Forms.ProgressBar();
            this.txtScannerIp1 = new System.Windows.Forms.TextBox();
            this.txtScannerIp3 = new System.Windows.Forms.TextBox();
            this.txtScannerIp2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlListas = new System.Windows.Forms.Panel();
            this.localizarConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gpConfig.SuspendLayout();
            this.pnlIpAndMask.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaIps)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabPageProfinet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProfinet)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlListas.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbPlacas
            // 
            this.cbPlacas.FormattingEnabled = true;
            this.cbPlacas.Items.AddRange(new object[] {
            "asd",
            "as",
            "das",
            "d"});
            this.cbPlacas.Location = new System.Drawing.Point(4, 28);
            this.cbPlacas.Margin = new System.Windows.Forms.Padding(4);
            this.cbPlacas.Name = "cbPlacas";
            this.cbPlacas.Size = new System.Drawing.Size(419, 24);
            this.cbPlacas.TabIndex = 1;
            this.cbPlacas.SelectedIndexChanged += new System.EventHandler(this.cbPlacas_SelectedIndexChanged);
            this.cbPlacas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbPlacas_KeyDown);
            // 
            // txtIP_1
            // 
            this.txtIP_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIP_1.Location = new System.Drawing.Point(108, 15);
            this.txtIP_1.Margin = new System.Windows.Forms.Padding(4);
            this.txtIP_1.Name = "txtIP_1";
            this.txtIP_1.Size = new System.Drawing.Size(43, 30);
            this.txtIP_1.TabIndex = 2;
            this.txtIP_1.Text = "192";
            this.txtIP_1.TextChanged += new System.EventHandler(this.txtIP_1_TextChanged);
            this.txtIP_1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIP_1_KeyPress);
            // 
            // txtIP_2
            // 
            this.txtIP_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIP_2.Location = new System.Drawing.Point(155, 15);
            this.txtIP_2.Margin = new System.Windows.Forms.Padding(4);
            this.txtIP_2.Name = "txtIP_2";
            this.txtIP_2.Size = new System.Drawing.Size(43, 30);
            this.txtIP_2.TabIndex = 3;
            this.txtIP_2.Text = "192";
            this.txtIP_2.TextChanged += new System.EventHandler(this.txtIP_2_TextChanged);
            this.txtIP_2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIP_2_KeyPress);
            // 
            // txtIP_3
            // 
            this.txtIP_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIP_3.Location = new System.Drawing.Point(201, 15);
            this.txtIP_3.Margin = new System.Windows.Forms.Padding(4);
            this.txtIP_3.Name = "txtIP_3";
            this.txtIP_3.Size = new System.Drawing.Size(43, 30);
            this.txtIP_3.TabIndex = 4;
            this.txtIP_3.Text = "192";
            this.txtIP_3.TextChanged += new System.EventHandler(this.txtIP_3_TextChanged);
            this.txtIP_3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIP_3_KeyPress);
            // 
            // txtIP_4
            // 
            this.txtIP_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIP_4.Location = new System.Drawing.Point(248, 15);
            this.txtIP_4.Margin = new System.Windows.Forms.Padding(4);
            this.txtIP_4.Name = "txtIP_4";
            this.txtIP_4.Size = new System.Drawing.Size(43, 30);
            this.txtIP_4.TabIndex = 5;
            this.txtIP_4.Text = "192";
            this.txtIP_4.TextChanged += new System.EventHandler(this.txtIP_4_TextChanged);
            this.txtIP_4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIP_4_KeyPress);
            // 
            // txtSubNet_4
            // 
            this.txtSubNet_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubNet_4.Location = new System.Drawing.Point(248, 55);
            this.txtSubNet_4.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubNet_4.Name = "txtSubNet_4";
            this.txtSubNet_4.Size = new System.Drawing.Size(43, 30);
            this.txtSubNet_4.TabIndex = 9;
            this.txtSubNet_4.Text = "192";
            this.txtSubNet_4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSubNet_4_KeyPress);
            // 
            // txtSubNet_3
            // 
            this.txtSubNet_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubNet_3.Location = new System.Drawing.Point(201, 55);
            this.txtSubNet_3.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubNet_3.Name = "txtSubNet_3";
            this.txtSubNet_3.Size = new System.Drawing.Size(43, 30);
            this.txtSubNet_3.TabIndex = 8;
            this.txtSubNet_3.Text = "192";
            this.txtSubNet_3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSubNet_3_KeyPress);
            // 
            // txtSubNet_2
            // 
            this.txtSubNet_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubNet_2.Location = new System.Drawing.Point(155, 55);
            this.txtSubNet_2.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubNet_2.Name = "txtSubNet_2";
            this.txtSubNet_2.Size = new System.Drawing.Size(43, 30);
            this.txtSubNet_2.TabIndex = 7;
            this.txtSubNet_2.Text = "192";
            this.txtSubNet_2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSubNet_2_KeyPress);
            // 
            // txtSubNet_1
            // 
            this.txtSubNet_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubNet_1.Location = new System.Drawing.Point(108, 55);
            this.txtSubNet_1.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubNet_1.Name = "txtSubNet_1";
            this.txtSubNet_1.Size = new System.Drawing.Size(43, 30);
            this.txtSubNet_1.TabIndex = 6;
            this.txtSubNet_1.Text = "192";
            this.txtSubNet_1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSubNet_1_KeyPress);
            // 
            // gpConfig
            // 
            this.gpConfig.Controls.Add(this.btnSetIP);
            this.gpConfig.Controls.Add(this.pnlIpAndMask);
            this.gpConfig.Controls.Add(this.toolStrip3);
            this.gpConfig.Location = new System.Drawing.Point(4, 62);
            this.gpConfig.Margin = new System.Windows.Forms.Padding(4);
            this.gpConfig.Name = "gpConfig";
            this.gpConfig.Padding = new System.Windows.Forms.Padding(4);
            this.gpConfig.Size = new System.Drawing.Size(420, 154);
            this.gpConfig.TabIndex = 10;
            this.gpConfig.TabStop = false;
            this.gpConfig.Text = "Configuração de IP:";
            // 
            // btnSetIP
            // 
            this.btnSetIP.Location = new System.Drawing.Point(304, 62);
            this.btnSetIP.Margin = new System.Windows.Forms.Padding(4);
            this.btnSetIP.Name = "btnSetIP";
            this.btnSetIP.Size = new System.Drawing.Size(105, 73);
            this.btnSetIP.TabIndex = 14;
            this.btnSetIP.Text = "Set IP";
            this.btnSetIP.UseVisualStyleBackColor = true;
            this.btnSetIP.Click += new System.EventHandler(this.btnSetIP_Click);
            // 
            // pnlIpAndMask
            // 
            this.pnlIpAndMask.Controls.Add(this.txtIP_2);
            this.pnlIpAndMask.Controls.Add(this.txtSubNet_1);
            this.pnlIpAndMask.Controls.Add(this.lblMascara);
            this.pnlIpAndMask.Controls.Add(this.txtIP_4);
            this.pnlIpAndMask.Controls.Add(this.lblIP);
            this.pnlIpAndMask.Controls.Add(this.txtSubNet_2);
            this.pnlIpAndMask.Controls.Add(this.txtIP_1);
            this.pnlIpAndMask.Controls.Add(this.txtIP_3);
            this.pnlIpAndMask.Controls.Add(this.txtSubNet_4);
            this.pnlIpAndMask.Controls.Add(this.txtSubNet_3);
            this.pnlIpAndMask.Location = new System.Drawing.Point(4, 50);
            this.pnlIpAndMask.Margin = new System.Windows.Forms.Padding(4);
            this.pnlIpAndMask.Name = "pnlIpAndMask";
            this.pnlIpAndMask.Size = new System.Drawing.Size(300, 100);
            this.pnlIpAndMask.TabIndex = 22;
            // 
            // lblMascara
            // 
            this.lblMascara.AutoSize = true;
            this.lblMascara.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMascara.Location = new System.Drawing.Point(4, 59);
            this.lblMascara.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMascara.Name = "lblMascara";
            this.lblMascara.Size = new System.Drawing.Size(94, 25);
            this.lblMascara.TabIndex = 11;
            this.lblMascara.Text = "Mascara:";
            this.lblMascara.Click += new System.EventHandler(this.lblMascara_Click);
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIP.Location = new System.Drawing.Point(65, 18);
            this.lblIP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(36, 25);
            this.lblIP.TabIndex = 10;
            this.lblIP.Text = "IP:";
            this.lblIP.Click += new System.EventHandler(this.lblIP_Click);
            // 
            // toolStrip3
            // 
            this.toolStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSalvar,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.btnDHCP_Toogle});
            this.toolStrip3.Location = new System.Drawing.Point(4, 19);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(412, 27);
            this.toolStrip3.TabIndex = 13;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // btnSalvar
            // 
            this.btnSalvar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSalvar.Image = global::MudaIpDahora.Properties.Resources.baseline_save_black_18dp;
            this.btnSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(29, 24);
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvarConfiguracao_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(48, 24);
            this.toolStripLabel1.Text = "DHCP";
            // 
            // btnDHCP_Toogle
            // 
            this.btnDHCP_Toogle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDHCP_Toogle.Image = global::MudaIpDahora.Properties.Resources.baseline_check_box_black_18dp;
            this.btnDHCP_Toogle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDHCP_Toogle.Name = "btnDHCP_Toogle";
            this.btnDHCP_Toogle.Size = new System.Drawing.Size(29, 24);
            this.btnDHCP_Toogle.Text = "DHCP State";
            this.btnDHCP_Toogle.Click += new System.EventHandler(this.btnDHCP_Toogle_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Placas de rede:";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Muda IP Dahora =)";
            this.notifyIcon.Visible = true;
            this.notifyIcon.BalloonTipClicked += new System.EventHandler(this.notifyIcon_BalloonTipClicked);
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gpConfig);
            this.panel1.Controls.Add(this.btnRecolher);
            this.panel1.Controls.Add(this.cbPlacas);
            this.panel1.Controls.Add(this.btnAtualizar);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(451, 218);
            this.panel1.TabIndex = 18;
            // 
            // btnRecolher
            // 
            this.btnRecolher.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRecolher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecolher.Location = new System.Drawing.Point(430, 0);
            this.btnRecolher.Margin = new System.Windows.Forms.Padding(4);
            this.btnRecolher.Name = "btnRecolher";
            this.btnRecolher.Size = new System.Drawing.Size(21, 218);
            this.btnRecolher.TabIndex = 21;
            this.btnRecolher.Text = "<";
            this.btnRecolher.UseVisualStyleBackColor = true;
            this.btnRecolher.Click += new System.EventHandler(this.btnRecolher_Click);
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.BackgroundImage = global::MudaIpDahora.Properties.Resources.baseline_autorenew_black_18dp;
            this.btnAtualizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAtualizar.Location = new System.Drawing.Point(393, 1);
            this.btnAtualizar.Margin = new System.Windows.Forms.Padding(4);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(31, 26);
            this.btnAtualizar.TabIndex = 12;
            this.btnAtualizar.UseVisualStyleBackColor = true;
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            // 
            // dgvListaIps
            // 
            this.dgvListaIps.AllowUserToAddRows = false;
            this.dgvListaIps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaIps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nome,
            this.clmPlaca,
            this.DHCP,
            this.IP,
            this.Mascara});
            this.dgvListaIps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvListaIps.Location = new System.Drawing.Point(4, 31);
            this.dgvListaIps.Margin = new System.Windows.Forms.Padding(4);
            this.dgvListaIps.Name = "dgvListaIps";
            this.dgvListaIps.RowHeadersVisible = false;
            this.dgvListaIps.RowHeadersWidth = 51;
            this.dgvListaIps.Size = new System.Drawing.Size(692, 182);
            this.dgvListaIps.TabIndex = 17;
            this.dgvListaIps.DoubleClick += new System.EventHandler(this.dgvListaIps_DoubleClick);
            // 
            // Nome
            // 
            this.Nome.HeaderText = "Nome";
            this.Nome.MinimumWidth = 6;
            this.Nome.Name = "Nome";
            this.Nome.ReadOnly = true;
            this.Nome.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Nome.Width = 125;
            // 
            // clmPlaca
            // 
            this.clmPlaca.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.clmPlaca.HeaderText = "Placa";
            this.clmPlaca.MinimumWidth = 6;
            this.clmPlaca.Name = "clmPlaca";
            this.clmPlaca.ReadOnly = true;
            this.clmPlaca.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmPlaca.Width = 150;
            // 
            // DHCP
            // 
            this.DHCP.HeaderText = "DHCP";
            this.DHCP.MinimumWidth = 6;
            this.DHCP.Name = "DHCP";
            this.DHCP.ReadOnly = true;
            this.DHCP.Width = 50;
            // 
            // IP
            // 
            this.IP.HeaderText = "IP";
            this.IP.MinimumWidth = 6;
            this.IP.Name = "IP";
            this.IP.ReadOnly = true;
            this.IP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.IP.Width = 125;
            // 
            // Mascara
            // 
            this.Mascara.HeaderText = "Mascara";
            this.Mascara.MinimumWidth = 6;
            this.Mascara.Name = "Mascara";
            this.Mascara.ReadOnly = true;
            this.Mascara.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Mascara.Width = 125;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ferramentasToolStripMenuItem,
            this.sobreToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(451, 28);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // ferramentasToolStripMenuItem
            // 
            this.ferramentasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inicializarComWindowsToolStripMenuItem,
            this.gerenciadorDePlacasDeRedeToolStripMenuItem,
            this.localizarConfigToolStripMenuItem,
            this.btnRebootApp});
            this.ferramentasToolStripMenuItem.Image = global::MudaIpDahora.Properties.Resources.baseline_miscellaneous_services_black_18dp;
            this.ferramentasToolStripMenuItem.Name = "ferramentasToolStripMenuItem";
            this.ferramentasToolStripMenuItem.Size = new System.Drawing.Size(124, 24);
            this.ferramentasToolStripMenuItem.Text = "Ferramentas";
            // 
            // inicializarComWindowsToolStripMenuItem
            // 
            this.inicializarComWindowsToolStripMenuItem.Image = global::MudaIpDahora.Properties.Resources.baseline_shortcut_black_24dp;
            this.inicializarComWindowsToolStripMenuItem.Name = "inicializarComWindowsToolStripMenuItem";
            this.inicializarComWindowsToolStripMenuItem.Size = new System.Drawing.Size(298, 26);
            this.inicializarComWindowsToolStripMenuItem.Text = "Inicializar com Windows";
            this.inicializarComWindowsToolStripMenuItem.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // gerenciadorDePlacasDeRedeToolStripMenuItem
            // 
            this.gerenciadorDePlacasDeRedeToolStripMenuItem.Image = global::MudaIpDahora.Properties.Resources.baseline_cable_black_18dp;
            this.gerenciadorDePlacasDeRedeToolStripMenuItem.Name = "gerenciadorDePlacasDeRedeToolStripMenuItem";
            this.gerenciadorDePlacasDeRedeToolStripMenuItem.Size = new System.Drawing.Size(298, 26);
            this.gerenciadorDePlacasDeRedeToolStripMenuItem.Text = "Gerenciador de Placas de Rede";
            this.gerenciadorDePlacasDeRedeToolStripMenuItem.Click += new System.EventHandler(this.gerenciadorDePlacasDeRedeToolStripMenuItem_Click);
            // 
            // btnRebootApp
            // 
            this.btnRebootApp.Image = global::MudaIpDahora.Properties.Resources.restart;
            this.btnRebootApp.Name = "btnRebootApp";
            this.btnRebootApp.Size = new System.Drawing.Size(298, 26);
            this.btnRebootApp.Text = "Reboot App";
            this.btnRebootApp.Click += new System.EventHandler(this.btnRebootApp_Click);
            // 
            // sobreToolStripMenuItem
            // 
            this.sobreToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.sobreToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dadosDoProjetoToolStripMenuItem,
            this.atualizacaoToolStripMenuItem});
            this.sobreToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sobreToolStripMenuItem.Image = global::MudaIpDahora.Properties.Resources.baseline_contact_support_black_18dp;
            this.sobreToolStripMenuItem.Name = "sobreToolStripMenuItem";
            this.sobreToolStripMenuItem.Size = new System.Drawing.Size(82, 24);
            this.sobreToolStripMenuItem.Text = "Sobre";
            // 
            // dadosDoProjetoToolStripMenuItem
            // 
            this.dadosDoProjetoToolStripMenuItem.Image = global::MudaIpDahora.Properties.Resources.baseline_local_cafe_black_18dp;
            this.dadosDoProjetoToolStripMenuItem.Name = "dadosDoProjetoToolStripMenuItem";
            this.dadosDoProjetoToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.dadosDoProjetoToolStripMenuItem.Text = "Dados do Projeto";
            this.dadosDoProjetoToolStripMenuItem.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // atualizacaoToolStripMenuItem
            // 
            this.atualizacaoToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.atualizacaoToolStripMenuItem.Image = global::MudaIpDahora.Properties.Resources.baseline_cloud_sync_black_18dp;
            this.atualizacaoToolStripMenuItem.Name = "atualizacaoToolStripMenuItem";
            this.atualizacaoToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.atualizacaoToolStripMenuItem.Text = "Atualizacao";
            this.atualizacaoToolStripMenuItem.Click += new System.EventHandler(this.btnAtualizacao_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPageProfinet);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(708, 246);
            this.tabControl.TabIndex = 23;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvListaIps);
            this.tabPage1.Controls.Add(this.toolStrip1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(700, 217);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "IPs Cadastrados";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.toolStripSeparator2,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(4, 4);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(692, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(113, 24);
            this.toolStripLabel2.Text = "IPs Cadastrados";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::MudaIpDahora.Properties.Resources.baseline_delete_outline_black_18dp;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton1.Text = "Excluir";
            this.toolStripButton1.ToolTipText = "btnExcluir";
            this.toolStripButton1.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // tabPageProfinet
            // 
            this.tabPageProfinet.Controls.Add(this.dgvProfinet);
            this.tabPageProfinet.Controls.Add(this.toolStrip2);
            this.tabPageProfinet.Location = new System.Drawing.Point(4, 25);
            this.tabPageProfinet.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageProfinet.Name = "tabPageProfinet";
            this.tabPageProfinet.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageProfinet.Size = new System.Drawing.Size(700, 217);
            this.tabPageProfinet.TabIndex = 1;
            this.tabPageProfinet.Text = "Profinet";
            this.tabPageProfinet.UseVisualStyleBackColor = true;
            // 
            // dgvProfinet
            // 
            this.dgvProfinet.AllowUserToAddRows = false;
            this.dgvProfinet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProfinet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.MAC,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dgvProfinet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProfinet.Location = new System.Drawing.Point(4, 31);
            this.dgvProfinet.Margin = new System.Windows.Forms.Padding(4);
            this.dgvProfinet.Name = "dgvProfinet";
            this.dgvProfinet.RowHeadersVisible = false;
            this.dgvProfinet.RowHeadersWidth = 51;
            this.dgvProfinet.Size = new System.Drawing.Size(692, 182);
            this.dgvProfinet.TabIndex = 18;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Nome";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 125;
            // 
            // MAC
            // 
            this.MAC.HeaderText = "MAC";
            this.MAC.MinimumWidth = 6;
            this.MAC.Name = "MAC";
            this.MAC.ReadOnly = true;
            this.MAC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MAC.Width = 125;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "IP";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 125;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Mascara";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 125;
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefreshProfinetDevice});
            this.toolStrip2.Location = new System.Drawing.Point(4, 4);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(692, 27);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnRefreshProfinetDevice
            // 
            this.btnRefreshProfinetDevice.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefreshProfinetDevice.Image = global::MudaIpDahora.Properties.Resources.baseline_autorenew_black_18dp;
            this.btnRefreshProfinetDevice.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefreshProfinetDevice.Name = "btnRefreshProfinetDevice";
            this.btnRefreshProfinetDevice.Size = new System.Drawing.Size(29, 24);
            this.btnRefreshProfinetDevice.Text = "Atualizar Devices";
            this.btnRefreshProfinetDevice.Click += new System.EventHandler(this.btnRefreshProfinetDevice_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listBoxScanner);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(700, 217);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "IP Scanner";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listBoxScanner
            // 
            this.listBoxScanner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxScanner.FormattingEnabled = true;
            this.listBoxScanner.ItemHeight = 16;
            this.listBoxScanner.Location = new System.Drawing.Point(3, 55);
            this.listBoxScanner.Name = "listBoxScanner";
            this.listBoxScanner.Size = new System.Drawing.Size(694, 159);
            this.listBoxScanner.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtTimeout);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btnStartScanner);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.textBox5);
            this.panel2.Controls.Add(this.textBox4);
            this.panel2.Controls.Add(this.progressBarScanner);
            this.panel2.Controls.Add(this.txtScannerIp1);
            this.panel2.Controls.Add(this.txtScannerIp3);
            this.panel2.Controls.Add(this.txtScannerIp2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(694, 52);
            this.panel2.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label5.Location = new System.Drawing.Point(597, 11);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 18);
            this.label5.TabIndex = 22;
            this.label5.Text = "ms";
            // 
            // txtTimeout
            // 
            this.txtTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtTimeout.Location = new System.Drawing.Point(520, 8);
            this.txtTimeout.Margin = new System.Windows.Forms.Padding(4);
            this.txtTimeout.Name = "txtTimeout";
            this.txtTimeout.Size = new System.Drawing.Size(69, 24);
            this.txtTimeout.TabIndex = 1000;
            this.txtTimeout.Text = "2000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label4.Location = new System.Drawing.Point(445, 11);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 18);
            this.label4.TabIndex = 21;
            this.label4.Text = "Time out:";
            // 
            // btnStartScanner
            // 
            this.btnStartScanner.Location = new System.Drawing.Point(318, 5);
            this.btnStartScanner.Name = "btnStartScanner";
            this.btnStartScanner.Size = new System.Drawing.Size(99, 31);
            this.btnStartScanner.TabIndex = 19;
            this.btnStartScanner.Text = "Start";
            this.btnStartScanner.UseVisualStyleBackColor = true;
            this.btnStartScanner.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(238, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 25);
            this.label2.TabIndex = 18;
            this.label2.Text = "-";
            // 
            // textBox5
            // 
            this.textBox5.Enabled = false;
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.Location = new System.Drawing.Point(254, 5);
            this.textBox5.Margin = new System.Windows.Forms.Padding(4);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(43, 30);
            this.textBox5.TabIndex = 17;
            this.textBox5.Text = "254";
            // 
            // textBox4
            // 
            this.textBox4.Enabled = false;
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(194, 5);
            this.textBox4.Margin = new System.Windows.Forms.Padding(4);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(43, 30);
            this.textBox4.TabIndex = 16;
            this.textBox4.Text = "0";
            // 
            // progressBarScanner
            // 
            this.progressBarScanner.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarScanner.Location = new System.Drawing.Point(0, 42);
            this.progressBarScanner.Maximum = 254;
            this.progressBarScanner.Name = "progressBarScanner";
            this.progressBarScanner.Size = new System.Drawing.Size(694, 10);
            this.progressBarScanner.TabIndex = 15;
            // 
            // txtScannerIp1
            // 
            this.txtScannerIp1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScannerIp1.Location = new System.Drawing.Point(50, 5);
            this.txtScannerIp1.Margin = new System.Windows.Forms.Padding(4);
            this.txtScannerIp1.Name = "txtScannerIp1";
            this.txtScannerIp1.Size = new System.Drawing.Size(43, 30);
            this.txtScannerIp1.TabIndex = 11;
            this.txtScannerIp1.Text = "192";
            // 
            // txtScannerIp3
            // 
            this.txtScannerIp3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScannerIp3.Location = new System.Drawing.Point(143, 5);
            this.txtScannerIp3.Margin = new System.Windows.Forms.Padding(4);
            this.txtScannerIp3.Name = "txtScannerIp3";
            this.txtScannerIp3.Size = new System.Drawing.Size(43, 30);
            this.txtScannerIp3.TabIndex = 13;
            this.txtScannerIp3.Text = "1";
            // 
            // txtScannerIp2
            // 
            this.txtScannerIp2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScannerIp2.Location = new System.Drawing.Point(97, 5);
            this.txtScannerIp2.Margin = new System.Windows.Forms.Padding(4);
            this.txtScannerIp2.Name = "txtScannerIp2";
            this.txtScannerIp2.Size = new System.Drawing.Size(43, 30);
            this.txtScannerIp2.TabIndex = 12;
            this.txtScannerIp2.Text = "168";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 25);
            this.label1.TabIndex = 14;
            this.label1.Text = "IP:";
            // 
            // pnlListas
            // 
            this.pnlListas.Controls.Add(this.tabControl);
            this.pnlListas.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlListas.Location = new System.Drawing.Point(451, 0);
            this.pnlListas.Margin = new System.Windows.Forms.Padding(4);
            this.pnlListas.Name = "pnlListas";
            this.pnlListas.Size = new System.Drawing.Size(708, 246);
            this.pnlListas.TabIndex = 20;
            // 
            // localizarConfigToolStripMenuItem
            // 
            this.localizarConfigToolStripMenuItem.Name = "localizarConfigToolStripMenuItem";
            this.localizarConfigToolStripMenuItem.Size = new System.Drawing.Size(298, 26);
            this.localizarConfigToolStripMenuItem.Text = "Localizar Config";
            this.localizarConfigToolStripMenuItem.Click += new System.EventHandler(this.localizarConfigToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 246);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pnlListas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agora sim.... Bora mudar esse IP maldito";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.gpConfig.ResumeLayout(false);
            this.gpConfig.PerformLayout();
            this.pnlIpAndMask.ResumeLayout(false);
            this.pnlIpAndMask.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaIps)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPageProfinet.ResumeLayout(false);
            this.tabPageProfinet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProfinet)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlListas.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cbPlacas;
        private System.Windows.Forms.TextBox txtIP_1;
        private System.Windows.Forms.TextBox txtIP_2;
        private System.Windows.Forms.TextBox txtIP_3;
        private System.Windows.Forms.TextBox txtIP_4;
        private System.Windows.Forms.TextBox txtSubNet_4;
        private System.Windows.Forms.TextBox txtSubNet_3;
        private System.Windows.Forms.TextBox txtSubNet_2;
        private System.Windows.Forms.TextBox txtSubNet_1;
        private System.Windows.Forms.GroupBox gpConfig;
        private System.Windows.Forms.Label lblMascara;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Button btnAtualizar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvListaIps;
        private System.Windows.Forms.Button btnRecolher;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.TabPage tabPageProfinet;
        private System.Windows.Forms.DataGridView dgvProfinet;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnRefreshProfinetDevice;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ferramentasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inicializarComWindowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gerenciadorDePlacasDeRedeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dadosDoProjetoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem atualizacaoToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton btnSalvar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton btnDHCP_Toogle;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Panel pnlListas;
        private System.Windows.Forms.Button btnSetIP;
        private System.Windows.Forms.Panel pnlIpAndMask;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nome;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPlaca;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DHCP;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mascara;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAC;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.ToolStripMenuItem btnRebootApp;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtScannerIp2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtScannerIp1;
        private System.Windows.Forms.TextBox txtScannerIp3;
        private System.Windows.Forms.ListBox listBoxScanner;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ProgressBar progressBarScanner;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button btnStartScanner;
        private System.Windows.Forms.TextBox txtTimeout;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem localizarConfigToolStripMenuItem;
    }
}

