using MudaIpDahora.Models;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace MudaIpDahora.Views
{
    public partial class FormAtualizacao : Form
    {
        Atualizador atualizador = new Atualizador();
        private bool localizadoRelease;
        private bool versaoAtualInferior;
        private bool downloadConcluido;
        private bool instaladorIniciado;
        private bool concluidoComErro = false;
        private bool concluido = false;
        public bool Silent { get; set; }
        public string ShortcutPath { get; set; } = "";
        public Thread AtualizacaoProcess { get; set; }

        public FormAtualizacao(bool silent)
        {
            //Tamanho menor
            Height = 110;
            progressBar_Click(null, null);

            Silent = silent;
            InitializeComponent();
            AtualizacaoProcess = new Thread(Atualizar);
            AtualizacaoProcess.Start();
        }
        private void ResizeForm()
        {
            if (Height == 110)
                Height = 344;
            else
                Height = 110;
        }
        private void Atualizar()
        {
            try
            {
                localizadoRelease = atualizador.LocalizarUltimaRelease();

                if (localizadoRelease)
                {    
                    versaoAtualInferior = atualizador.CompararVersao();
                    if (versaoAtualInferior)
                    {
                        downloadConcluido = atualizador.DownloadComponentes();
                        if (downloadConcluido)
                        {
                            if (MessageBox.Show("Gostaria de iniciar o instalador?", "Iniciar Instalador", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                instaladorIniciado = atualizador.IniciarInstalador();
                            }
                        }
                    }
                    else
                    {
                        if (!Silent)
                            MessageBox.Show("Não foi encontrado uma nova versão", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        concluido = true;
                    }
                        
                }
            }catch(Exception ex)
            {
                if (!Silent)
                    MessageBox.Show(ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                concluidoComErro = true;
            }
            finally
            {
                concluido = true;
            }
            
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //Update Log
            if (atualizador.Log.Count > 0)
            {
                rtbLog.AppendText(atualizador.Log.Dequeue().ToString() + Environment.NewLine);
            }

            if (!atualizador.Download.Finished)
            {
                progressBar.Value = atualizador.Download.Progress;
                lblAtividade.Text = "Download - " + atualizador.Download.Name + " - Bytes: " +
                    atualizador.Download.BytesDownloaded + " de " + atualizador.Download.TotalBytes;
                return;
            }

            //Update Step progress
            if (concluido)
            {
                progressBar.Value = 100;
                lblPasso.Text = "Passo 4 de 4";

                if (concluidoComErro)
                {
                    progressBar.ForeColor = Color.Red;
                    lblAtividade.Text = "Concluido com erro";
                }
                else
                    lblAtividade.Text = "Concluido";

                timer.Enabled = false;
                if (Silent)
                {
                    Close();
                }
                return;
            }
            
            if (!localizadoRelease)
            {
                lblAtividade.Text = "Atividade: Localizando a release no GitHub";
                return;
            }

            if (!versaoAtualInferior)
            {
                progressBar.Value = 25;
                lblPasso.Text = "Passo 2 de 4";
                lblAtividade.Text = "Atividade: Comparando as versões";
                return;
            }

            if (!downloadConcluido)
            {
                lblPasso.Text = "Passo 3 de 4";
                progressBar.Value = 50;
                lblAtividade.Text = "Atividade: Fazendo o download dos componentes";
                return;
            }

            if (!instaladorIniciado)
            {
                lblPasso.Text = "Passo 4 de 4";
                progressBar.Value = 75;
                lblAtividade.Text = "Atividade: Iniciando Instalador";
                return;
            }
        }

        private void progressBar_Click(object sender, EventArgs e)
        {
            ResizeForm();
        }
    }
}
