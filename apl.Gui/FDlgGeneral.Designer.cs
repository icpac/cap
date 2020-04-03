namespace apl.Gui
{
    partial class FDlgGeneral
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.pBottom = new System.Windows.Forms.Panel();
            this.pButtons = new System.Windows.Forms.Panel();
            this.bAceptar = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.bAyuda = new System.Windows.Forms.Button();
            this.pCent = new System.Windows.Forms.Panel();
            this.pDow = new System.Windows.Forms.Panel();
            this.pTop = new System.Windows.Forms.Panel();
            this.pRig = new System.Windows.Forms.Panel();
            this.pIzq = new System.Windows.Forms.Panel();
            this.pBottom.SuspendLayout();
            this.pButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pBottom
            // 
            this.pBottom.Controls.Add(this.pButtons);
            this.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBottom.Location = new System.Drawing.Point(0, 351);
            this.pBottom.Name = "pBottom";
            this.pBottom.Size = new System.Drawing.Size(410, 31);
            this.pBottom.TabIndex = 0;
            // 
            // pButtons
            // 
            this.pButtons.Controls.Add(this.bAceptar);
            this.pButtons.Controls.Add(this.bCancel);
            this.pButtons.Controls.Add(this.bAyuda);
            this.pButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.pButtons.Location = new System.Drawing.Point(142, 0);
            this.pButtons.Name = "pButtons";
            this.pButtons.Size = new System.Drawing.Size(268, 31);
            this.pButtons.TabIndex = 1;
            // 
            // bAceptar
            // 
            this.bAceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bAceptar.Location = new System.Drawing.Point(19, 3);
            this.bAceptar.Name = "bAceptar";
            this.bAceptar.Size = new System.Drawing.Size(75, 23);
            this.bAceptar.TabIndex = 0;
            this.bAceptar.Text = "Aceptar";
            this.bAceptar.UseVisualStyleBackColor = true;
            this.bAceptar.Click += new System.EventHandler(this.bAceptar_Click);
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(100, 3);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 1;
            this.bCancel.Text = "Cancelar";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bAyuda
            // 
            this.bAyuda.Location = new System.Drawing.Point(181, 3);
            this.bAyuda.Name = "bAyuda";
            this.bAyuda.Size = new System.Drawing.Size(75, 23);
            this.bAyuda.TabIndex = 2;
            this.bAyuda.Text = "Ayuda";
            this.bAyuda.UseVisualStyleBackColor = true;
            // 
            // pCent
            // 
            this.pCent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pCent.Location = new System.Drawing.Point(5, 5);
            this.pCent.Name = "pCent";
            this.pCent.Size = new System.Drawing.Size(400, 341);
            this.pCent.TabIndex = 10;
            // 
            // pDow
            // 
            this.pDow.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pDow.Location = new System.Drawing.Point(5, 346);
            this.pDow.Name = "pDow";
            this.pDow.Size = new System.Drawing.Size(400, 5);
            this.pDow.TabIndex = 9;
            // 
            // pTop
            // 
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(5, 0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(400, 5);
            this.pTop.TabIndex = 8;
            // 
            // pRig
            // 
            this.pRig.Dock = System.Windows.Forms.DockStyle.Right;
            this.pRig.Location = new System.Drawing.Point(405, 0);
            this.pRig.Name = "pRig";
            this.pRig.Size = new System.Drawing.Size(5, 351);
            this.pRig.TabIndex = 7;
            // 
            // pIzq
            // 
            this.pIzq.Dock = System.Windows.Forms.DockStyle.Left;
            this.pIzq.Location = new System.Drawing.Point(0, 0);
            this.pIzq.Name = "pIzq";
            this.pIzq.Size = new System.Drawing.Size(5, 351);
            this.pIzq.TabIndex = 6;
            // 
            // FDlgGeneral
            // 
            this.AcceptButton = this.bAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(410, 382);
            this.Controls.Add(this.pCent);
            this.Controls.Add(this.pDow);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.pRig);
            this.Controls.Add(this.pIzq);
            this.Controls.Add(this.pBottom);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FDlgGeneral";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FDlgGeneral";
            this.Shown += new System.EventHandler(this.FDlgGeneral_Shown);
            this.pBottom.ResumeLayout(false);
            this.pButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel pBottom;
        protected System.Windows.Forms.Button bAceptar;
        protected System.Windows.Forms.Button bCancel;
        protected System.Windows.Forms.Button bAyuda;
        protected System.Windows.Forms.Panel pButtons;
        protected System.Windows.Forms.Panel pCent;
        protected System.Windows.Forms.Panel pDow;
        protected System.Windows.Forms.Panel pTop;
        protected System.Windows.Forms.Panel pRig;
        protected System.Windows.Forms.Panel pIzq;
    }
}