namespace cap
{
    partial class AcercaDe
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
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.pBottom.SuspendLayout();
            this.pButtons.SuspendLayout();
            this.pCent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pBottom
            // 
            this.pBottom.Location = new System.Drawing.Point(0, 219);
            this.pBottom.Size = new System.Drawing.Size(452, 31);
            // 
            // pButtons
            // 
            this.pButtons.Location = new System.Drawing.Point(184, 0);
            // 
            // pCent
            // 
            this.pCent.Controls.Add(this.label2);
            this.pCent.Controls.Add(this.linkLabel1);
            this.pCent.Controls.Add(this.label1);
            this.pCent.Size = new System.Drawing.Size(442, 209);
            // 
            // pDow
            // 
            this.pDow.Location = new System.Drawing.Point(5, 214);
            this.pDow.Size = new System.Drawing.Size(442, 5);
            // 
            // pTop
            // 
            this.pTop.Size = new System.Drawing.Size(442, 5);
            // 
            // pRig
            // 
            this.pRig.Location = new System.Drawing.Point(447, 0);
            this.pRig.Size = new System.Drawing.Size(5, 219);
            // 
            // pIzq
            // 
            this.pIzq.Size = new System.Drawing.Size(5, 219);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Control Administrativo Personal";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(18, 46);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(76, 13);
            this.linkLabel1.TabIndex = 1;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "www.icpac.mx";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Versión: 1.0";
            // 
            // AcercaDe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 250);
            this.Name = "AcercaDe";
            this.Text = "Acerca de";
            this.pBottom.ResumeLayout(false);
            this.pButtons.ResumeLayout(false);
            this.pCent.ResumeLayout(false);
            this.pCent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label1;
    }
}