namespace Cap.Bancos.Controllers
{
    partial class VCPresupuesto
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
            this.popupWindowShowActionPresupuestoReal = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.simpleActionACrs = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // popupWindowShowActionPresupuestoReal
            // 
            this.popupWindowShowActionPresupuestoReal.AcceptButtonCaption = "Calcular";
            this.popupWindowShowActionPresupuestoReal.CancelButtonCaption = "Cancelar";
            this.popupWindowShowActionPresupuestoReal.Caption = "Presupuesto Real";
            this.popupWindowShowActionPresupuestoReal.ConfirmationMessage = null;
            this.popupWindowShowActionPresupuestoReal.Id = "182dde15-cbc7-4f29-86a5-65d82fe0dd1e";
            this.popupWindowShowActionPresupuestoReal.ToolTip = null;
            this.popupWindowShowActionPresupuestoReal.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionPresupuestoReal_CustomizePopupWindowParams);
            this.popupWindowShowActionPresupuestoReal.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionPresupuestoReal_Execute);
            // 
            // simpleActionACrs
            // 
            this.simpleActionACrs.Caption = "Limpia Montos";
            this.simpleActionACrs.ConfirmationMessage = "Está seguro de limpiar los Montos Reales ?";
            this.simpleActionACrs.Id = "ac362695-2f30-4558-9036-cfe26b44d274";
            this.simpleActionACrs.ToolTip = null;
            this.simpleActionACrs.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionACrs_Execute);
            // 
            // VCPresupuesto
            // 
            this.Actions.Add(this.popupWindowShowActionPresupuestoReal);
            this.Actions.Add(this.simpleActionACrs);
            this.ViewControlsCreated += new System.EventHandler(this.VCPresupuesto_ViewControlsCreated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionPresupuestoReal;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionACrs;
    }
}
