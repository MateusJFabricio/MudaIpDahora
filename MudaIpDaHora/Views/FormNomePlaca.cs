using System;
using System.Windows.Forms;

namespace MudaIpDahora.Views
{
    public partial class FormNomePlaca : Form
    {
        public string NomePlaca { get; set; }
        public FormNomePlaca()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtNome.TextLength > 0)
            {
                DialogResult = DialogResult.OK;
                NomePlaca = txtNome.Text;
            }else
                DialogResult = DialogResult.Cancel;

        }

        private void txtNome_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk_Click(sender, e);
            }
        }
    }
}
