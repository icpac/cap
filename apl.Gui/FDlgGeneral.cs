/*
 * Carlos Javier L�pez Cruz
 * 
 * Correo:	javier1604@gmail.com
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace apl.Gui
{
    /// <summary>
    /// Di�logo general, para cualquier forma que necesite un esquema
    /// como el de aceptar, cancelar, ayuda, o salir y ayuda. 
    /// S�lo le pusimos los botones y que se pueda modificar el tama�o
    /// y llevar los botones en la posici�n relativa.
    /// 
    /// Se puede sobre escribir Aceptar.
    /// Si se quiere se puede ver s�lo el bot�n Cancelar como Salir.
    /// </summary>
    public partial class FDlgGeneral : Form
    {
        public FDlgGeneral()
        {
            InitializeComponent();
        }

        private void bAceptar_Click(object sender, EventArgs e)
        {
            Aceptar();
        }

        protected virtual void Aceptar()
        {
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        protected virtual void Cancelar()
        {
        }

        public bool Salir
        {
            set
            {
                bAceptar.Visible = value;
                if (bAceptar.Visible == false)
                    bCancel.Text = "Salir";
                else
                    bCancel.Text = "Cancelar";
            }
        }

        /// <summary>
        /// Control que toma el foco al show o al new
        /// </summary>
        private Control FControlNew;
        protected Control ControlNew
        {
            set { FControlNew = value; }
            get { return FControlNew; }
        }

        private void FDlgGeneral_Shown(object sender, EventArgs e)
        {
            if (ControlNew != null)
                ControlNew.Focus();
        }
    }
}