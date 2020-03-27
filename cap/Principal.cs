/**************************************
 * Carlos Javier López Cruz
 * javier1604@gmail.com
 * ***********************************/

using apl.Gui;
using System;

namespace cap
{
    public partial class Principal : FMain
    {
        public Principal()
        {
            InitializeComponent();

            Text = "Control Administrativo Personal";
            this.statusStripMain.Items.Add("iCPAC-cap");
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
