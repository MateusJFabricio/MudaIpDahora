using System;
using System.Windows.Forms;

namespace MudaIpDahora.Views
{
    public partial class FormInfo : Form
    {
        public FormInfo()
        {
            Version version = new Version(Application.ProductVersion);
            InitializeComponent();
            rtBox.AppendText("Olá. Esta ferramenta foi desenvolvida pelo Mateus Fabricio" + Environment.NewLine);
            rtBox.AppendText("Versão: " + version.ToString() + Environment.NewLine);
            rtBox.AppendText(Environment.NewLine);
            rtBox.AppendText(" -- Dependencias e Referencias do Projeto --" + Environment.NewLine);
            rtBox.AppendText("Controle Profinet: https://github.com/fbarresi/ProfinetTools" + Environment.NewLine);
            rtBox.AppendText("Instalador: InnoSetup" + Environment.NewLine);
            rtBox.AppendText(Environment.NewLine);
            rtBox.AppendText("Gostaria de contribuir?" + Environment.NewLine);
            rtBox.AppendText("Visite o GitHub no Link:" + Environment.NewLine);
            rtBox.AppendText("https://github.com/MateusJFabricio/MudaIpDahora");
        }
    }
}
