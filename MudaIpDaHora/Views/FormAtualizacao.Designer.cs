
namespace MudaIpDahora.Views
{
    partial class FormAtualizacao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblAtividade = new System.Windows.Forms.Label();
            this.lblPasso = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 25);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(343, 23);
            this.progressBar.TabIndex = 0;
            // 
            // lblAtividade
            // 
            this.lblAtividade.AutoSize = true;
            this.lblAtividade.Location = new System.Drawing.Point(12, 9);
            this.lblAtividade.Name = "lblAtividade";
            this.lblAtividade.Size = new System.Drawing.Size(171, 13);
            this.lblAtividade.TabIndex = 1;
            this.lblAtividade.Text = "Atividade: Localizando atualizacao";
            // 
            // lblPasso
            // 
            this.lblPasso.AutoSize = true;
            this.lblPasso.Location = new System.Drawing.Point(286, 51);
            this.lblPasso.Name = "lblPasso";
            this.lblPasso.Size = new System.Drawing.Size(69, 13);
            this.lblPasso.TabIndex = 2;
            this.lblPasso.Text = "Passo 1 de 4";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // FormAtualizacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 70);
            this.Controls.Add(this.lblPasso);
            this.Controls.Add(this.lblAtividade);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormAtualizacao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Atualizacao";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblAtividade;
        private System.Windows.Forms.Label lblPasso;
        private System.Windows.Forms.Timer timer;
    }
}