/*
 * Carlos Javier López Cruz
 * 
 * Correo:	javier1604@gmail.com
 */

using apl.Gui;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cap
{
    public partial class AcercaDe : FDlgGeneral
    {
        public AcercaDe()
        {
            InitializeComponent();

            Salir = false;
        }

        protected override void Cancelar()
        {
            base.Cancelar();

            Close();
        }
    }
}
