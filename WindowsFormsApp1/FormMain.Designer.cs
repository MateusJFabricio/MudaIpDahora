namespace WindowsFormsApp1
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
            this.btnSetIP = new System.Windows.Forms.Button();
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSalvarConfiguracao = new System.Windows.Forms.Button();
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnInit = new System.Windows.Forms.Button();
            this.btnInfo = new System.Windows.Forms.Button();
            this.cbDhcp = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvListaIps = new System.Windows.Forms.DataGridView();
            this.Placa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DHCP = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mascara = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gpConfig.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaIps)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSetIP
            // 
            this.btnSetIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetIP.Location = new System.Drawing.Point(319, 77);
            this.btnSetIP.Name = "btnSetIP";
            this.btnSetIP.Size = new System.Drawing.Size(153, 76);
            this.btnSetIP.TabIndex = 0;
            this.btnSetIP.Text = "Set";
            this.btnSetIP.UseVisualStyleBackColor = true;
            this.btnSetIP.Click += new System.EventHandler(this.btnSetIP_Click);
            // 
            // cbPlacas
            // 
            this.cbPlacas.FormattingEnabled = true;
            this.cbPlacas.Items.AddRange(new object[] {
            "asd",
            "as",
            "das",
            "d"});
            this.cbPlacas.Location = new System.Drawing.Point(12, 23);
            this.cbPlacas.Name = "cbPlacas";
            this.cbPlacas.Size = new System.Drawing.Size(378, 21);
            this.cbPlacas.TabIndex = 1;
            this.cbPlacas.SelectedIndexChanged += new System.EventHandler(this.cbPlacas_SelectedIndexChanged);
            this.cbPlacas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbPlacas_KeyDown);
            // 
            // txtIP_1
            // 
            this.txtIP_1.Location = new System.Drawing.Point(97, 19);
            this.txtIP_1.Name = "txtIP_1";
            this.txtIP_1.Size = new System.Drawing.Size(29, 20);
            this.txtIP_1.TabIndex = 2;
            this.txtIP_1.Text = "192";
            // 
            // txtIP_2
            // 
            this.txtIP_2.Location = new System.Drawing.Point(131, 19);
            this.txtIP_2.Name = "txtIP_2";
            this.txtIP_2.Size = new System.Drawing.Size(29, 20);
            this.txtIP_2.TabIndex = 3;
            this.txtIP_2.Text = "192";
            // 
            // txtIP_3
            // 
            this.txtIP_3.Location = new System.Drawing.Point(165, 19);
            this.txtIP_3.Name = "txtIP_3";
            this.txtIP_3.Size = new System.Drawing.Size(29, 20);
            this.txtIP_3.TabIndex = 4;
            this.txtIP_3.Text = "192";
            // 
            // txtIP_4
            // 
            this.txtIP_4.Location = new System.Drawing.Point(199, 19);
            this.txtIP_4.Name = "txtIP_4";
            this.txtIP_4.Size = new System.Drawing.Size(29, 20);
            this.txtIP_4.TabIndex = 5;
            this.txtIP_4.Text = "192";
            // 
            // txtSubNet_4
            // 
            this.txtSubNet_4.Location = new System.Drawing.Point(199, 45);
            this.txtSubNet_4.Name = "txtSubNet_4";
            this.txtSubNet_4.Size = new System.Drawing.Size(29, 20);
            this.txtSubNet_4.TabIndex = 9;
            this.txtSubNet_4.Text = "192";
            // 
            // txtSubNet_3
            // 
            this.txtSubNet_3.Location = new System.Drawing.Point(165, 45);
            this.txtSubNet_3.Name = "txtSubNet_3";
            this.txtSubNet_3.Size = new System.Drawing.Size(29, 20);
            this.txtSubNet_3.TabIndex = 8;
            this.txtSubNet_3.Text = "192";
            // 
            // txtSubNet_2
            // 
            this.txtSubNet_2.Location = new System.Drawing.Point(131, 45);
            this.txtSubNet_2.Name = "txtSubNet_2";
            this.txtSubNet_2.Size = new System.Drawing.Size(29, 20);
            this.txtSubNet_2.TabIndex = 7;
            this.txtSubNet_2.Text = "192";
            // 
            // txtSubNet_1
            // 
            this.txtSubNet_1.Location = new System.Drawing.Point(97, 45);
            this.txtSubNet_1.Name = "txtSubNet_1";
            this.txtSubNet_1.Size = new System.Drawing.Size(29, 20);
            this.txtSubNet_1.TabIndex = 6;
            this.txtSubNet_1.Text = "192";
            // 
            // gpConfig
            // 
            this.gpConfig.Controls.Add(this.label2);
            this.gpConfig.Controls.Add(this.label1);
            this.gpConfig.Controls.Add(this.txtIP_1);
            this.gpConfig.Controls.Add(this.txtSubNet_4);
            this.gpConfig.Controls.Add(this.txtIP_2);
            this.gpConfig.Controls.Add(this.txtSubNet_3);
            this.gpConfig.Controls.Add(this.txtIP_3);
            this.gpConfig.Controls.Add(this.txtSubNet_2);
            this.gpConfig.Controls.Add(this.txtIP_4);
            this.gpConfig.Controls.Add(this.txtSubNet_1);
            this.gpConfig.Location = new System.Drawing.Point(12, 70);
            this.gpConfig.Name = "gpConfig";
            this.gpConfig.Size = new System.Drawing.Size(301, 83);
            this.gpConfig.TabIndex = 10;
            this.gpConfig.TabStop = false;
            this.gpConfig.Text = "Configuração de IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Mascara:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "IP:";
            // 
            // btnSalvarConfiguracao
            // 
            this.btnSalvarConfiguracao.Location = new System.Drawing.Point(349, 50);
            this.btnSalvarConfiguracao.Name = "btnSalvarConfiguracao";
            this.btnSalvarConfiguracao.Size = new System.Drawing.Size(123, 21);
            this.btnSalvarConfiguracao.TabIndex = 12;
            this.btnSalvarConfiguracao.Text = "Salvar Configuracao";
            this.btnSalvarConfiguracao.UseVisualStyleBackColor = true;
            this.btnSalvarConfiguracao.Click += new System.EventHandler(this.btnSalvarConfiguracao_Click);
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.Location = new System.Drawing.Point(396, 23);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(77, 21);
            this.btnAtualizar.TabIndex = 12;
            this.btnAtualizar.Text = "Atualizar";
            this.btnAtualizar.UseVisualStyleBackColor = true;
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Placas de rede:";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Muda IP Dahora =)";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(12, 163);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(228, 23);
            this.btnInit.TabIndex = 14;
            this.btnInit.Text = "Criar atalho para inicializar com o Windows";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.Location = new System.Drawing.Point(438, 163);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(35, 23);
            this.btnInfo.TabIndex = 15;
            this.btnInfo.Text = "?";
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // cbDhcp
            // 
            this.cbDhcp.AutoSize = true;
            this.cbDhcp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbDhcp.Location = new System.Drawing.Point(12, 50);
            this.cbDhcp.Name = "cbDhcp";
            this.cbDhcp.Size = new System.Drawing.Size(65, 21);
            this.cbDhcp.TabIndex = 16;
            this.cbDhcp.Text = "DHCP";
            this.cbDhcp.UseVisualStyleBackColor = true;
            this.cbDhcp.CheckedChanged += new System.EventHandler(this.cbDhcp_CheckedChanged);
            this.cbDhcp.MouseCaptureChanged += new System.EventHandler(this.cbDhcp_MouseCaptureChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSalvarConfiguracao);
            this.panel1.Controls.Add(this.btnExcluir);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dgvListaIps);
            this.panel1.Controls.Add(this.btnSetIP);
            this.panel1.Controls.Add(this.btnInfo);
            this.panel1.Controls.Add(this.cbPlacas);
            this.panel1.Controls.Add(this.gpConfig);
            this.panel1.Controls.Add(this.btnAtualizar);
            this.panel1.Controls.Add(this.cbDhcp);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnInit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(928, 194);
            this.panel1.TabIndex = 18;
            // 
            // btnExcluir
            // 
            this.btnExcluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcluir.Location = new System.Drawing.Point(862, 3);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(61, 19);
            this.btnExcluir.TabIndex = 19;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(476, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Lista IP\'s:";
            // 
            // dgvListaIps
            // 
            this.dgvListaIps.AllowUserToAddRows = false;
            this.dgvListaIps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaIps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Placa,
            this.DHCP,
            this.IP,
            this.Mascara});
            this.dgvListaIps.Location = new System.Drawing.Point(479, 23);
            this.dgvListaIps.Name = "dgvListaIps";
            this.dgvListaIps.Size = new System.Drawing.Size(445, 163);
            this.dgvListaIps.TabIndex = 17;
            this.dgvListaIps.DoubleClick += new System.EventHandler(this.dgvListaIps_DoubleClick);
            // 
            // Placa
            // 
            this.Placa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Placa.HeaderText = "Placa";
            this.Placa.Name = "Placa";
            this.Placa.ReadOnly = true;
            this.Placa.Width = 150;
            // 
            // DHCP
            // 
            this.DHCP.HeaderText = "DHCP";
            this.DHCP.Name = "DHCP";
            this.DHCP.ReadOnly = true;
            this.DHCP.Width = 50;
            // 
            // IP
            // 
            this.IP.HeaderText = "IP";
            this.IP.Name = "IP";
            this.IP.ReadOnly = true;
            // 
            // Mascara
            // 
            this.Mascara.HeaderText = "Mascara";
            this.Mascara.Name = "Mascara";
            this.Mascara.ReadOnly = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 194);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "Agora sim.... Bora mudar esse IP maldito";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.gpConfig.ResumeLayout(false);
            this.gpConfig.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaIps)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSetIP;
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAtualizar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.CheckBox cbDhcp;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvListaIps;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSalvarConfiguracao;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.DataGridViewTextBoxColumn Placa;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DHCP;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mascara;
    }
}

