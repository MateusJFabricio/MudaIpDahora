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
            this.Coisas = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnInit = new System.Windows.Forms.Button();
            this.Coisas.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSetIP
            // 
            this.btnSetIP.Location = new System.Drawing.Point(267, 19);
            this.btnSetIP.Name = "btnSetIP";
            this.btnSetIP.Size = new System.Drawing.Size(188, 46);
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
            this.cbPlacas.Location = new System.Drawing.Point(12, 28);
            this.cbPlacas.Name = "cbPlacas";
            this.cbPlacas.Size = new System.Drawing.Size(378, 21);
            this.cbPlacas.TabIndex = 1;
            this.cbPlacas.SelectedIndexChanged += new System.EventHandler(this.cbPlacas_SelectedIndexChanged);
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
            // Coisas
            // 
            this.Coisas.Controls.Add(this.label2);
            this.Coisas.Controls.Add(this.label1);
            this.Coisas.Controls.Add(this.txtIP_1);
            this.Coisas.Controls.Add(this.txtSubNet_4);
            this.Coisas.Controls.Add(this.btnSetIP);
            this.Coisas.Controls.Add(this.txtIP_2);
            this.Coisas.Controls.Add(this.txtSubNet_3);
            this.Coisas.Controls.Add(this.txtIP_3);
            this.Coisas.Controls.Add(this.txtSubNet_2);
            this.Coisas.Controls.Add(this.txtIP_4);
            this.Coisas.Controls.Add(this.txtSubNet_1);
            this.Coisas.Location = new System.Drawing.Point(12, 55);
            this.Coisas.Name = "Coisas";
            this.Coisas.Size = new System.Drawing.Size(461, 88);
            this.Coisas.TabIndex = 10;
            this.Coisas.TabStop = false;
            this.Coisas.Text = "Coisas";
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
            // btnAtualizar
            // 
            this.btnAtualizar.Location = new System.Drawing.Point(396, 28);
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
            this.label3.Location = new System.Drawing.Point(15, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Placas";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(12, 149);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(228, 23);
            this.btnInit.TabIndex = 14;
            this.btnInit.Text = "Criar atalho para inicializar com o Windows";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 177);
            this.Controls.Add(this.btnInit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAtualizar);
            this.Controls.Add(this.Coisas);
            this.Controls.Add(this.cbPlacas);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "Agora sim.... Bora mudar esse IP maldito";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.Deactivate += new System.EventHandler(this.Form1_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Coisas.ResumeLayout(false);
            this.Coisas.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.GroupBox Coisas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAtualizar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Button btnInit;
    }
}

