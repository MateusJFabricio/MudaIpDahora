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
        private bool atalhoCriado;
        private bool concluidoComErro = false;
        private bool concluido = false;

        public FormAtualizacao()
        {
            InitializeComponent();
            new Thread(Atualizar).Start();
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
                            atalhoCriado = atualizador.CriarAtalho();
                    }
                    else
                    {
                        MessageBox.Show("Não foi encontrado uma nova vesao", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        concluido = true;
                    }
                        
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                concluidoComErro = true;
            }
            finally
            {
                concluido = true;
            }
            
        }

        private void timer_Tick(object sender, EventArgs e)
        {
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

            if (!atalhoCriado)
            {
                lblPasso.Text = "Passo 4 de 4";
                progressBar.Value = 75;
                lblAtividade.Text = "Atividade: Criando o atalho de inicializacao";
                return;
            }
        }
    }
}
