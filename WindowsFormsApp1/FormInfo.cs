using System;
using System.Windows.Forms;

namespace MudaIpDahora
{
    public partial class FormInfo : Form
    {
        public FormInfo()
        {
            InitializeComponent();
            rtBox.AppendText("Olá. Esta ferramenta foi desenvolvida pelo Mateus Fabricio" + Environment.NewLine);
            rtBox.AppendText("Gostaria de contribuir?" + Environment.NewLine);
            rtBox.AppendText("Visite o GitHub no Link:" + Environment.NewLine);
            rtBox.AppendText("https://github.com/MateusJFabricio/MudaIpDahora");
        }
    }
}
