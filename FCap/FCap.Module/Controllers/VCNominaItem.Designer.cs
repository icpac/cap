namespace FCap.Module.Controllers.NominaF
{
    partial class VCNominaItem
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.simpleActionSvSllr = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.simpleActionCnclar = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.simpleActionMail = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.simpleActionPrntRcb = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // simpleActionSvSllr
            // 
            this.simpleActionSvSllr.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
            this.simpleActionSvSllr.Caption = "Guardar y Sellar";
            this.simpleActionSvSllr.ConfirmationMessage = "Está seguro de Guardar y Sellar?";
            this.simpleActionSvSllr.Category = "Save";
            this.simpleActionSvSllr.Id = "NmnItmSvSllr";
            this.simpleActionSvSllr.ToolTip = null;
            this.simpleActionSvSllr.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionSvSllr_Execute);
            // 
            // simpleActionCnclar
            // 
            this.simpleActionCnclar.Caption = "Cancelar";
            this.simpleActionCnclar.ConfirmationMessage = "Está seguro de Cancelar ?";
            this.simpleActionCnclar.Id = "NmnItmCnclr";
            this.simpleActionCnclar.ImageName = "Product_Record_Delete";
            this.simpleActionCnclar.ToolTip = null;
            this.simpleActionCnclar.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionCnclar_Execute);
            // 
            // simpleActionMail
            // 
            this.simpleActionMail.Caption = "Enviar Correo";
            this.simpleActionMail.ConfirmationMessage = null;
            this.simpleActionMail.Id = "NmnItmMl";
            this.simpleActionMail.ImageName = "Mail_Forward_2";
            this.simpleActionMail.ToolTip = null;
            this.simpleActionMail.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionMail_Execute);
            // 
            // simpleActionPrntRcb
            // 
            this.simpleActionPrntRcb.Caption = "Imprimir Recibo";
            this.simpleActionPrntRcb.ConfirmationMessage = null;
            this.simpleActionPrntRcb.Id = "NmnItmPrnt";
            this.simpleActionPrntRcb.ToolTip = null;
            this.simpleActionPrntRcb.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionPrntRcb_Execute);
            // 
            // VCNominaItem
            // 
            this.Actions.Add(this.simpleActionSvSllr);
            this.Actions.Add(this.simpleActionCnclar);
            this.Actions.Add(this.simpleActionMail);
            this.Actions.Add(this.simpleActionPrntRcb);
            this.ViewControlsCreated += new System.EventHandler(this.VCNominaItem_ViewControlsCreated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionSvSllr;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionCnclar;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionMail;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionPrntRcb;
    }
}
