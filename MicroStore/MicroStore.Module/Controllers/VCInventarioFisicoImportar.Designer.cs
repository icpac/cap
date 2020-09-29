namespace MicroStore.Module.Controllers
{
    partial class VCInventarioFisicoImportar
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
            this.popupWindowShowActionImprtrFl = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popupWindowShowActionImprtrFl
            // 
            this.popupWindowShowActionImprtrFl.AcceptButtonCaption = "Importar";
            this.popupWindowShowActionImprtrFl.CancelButtonCaption = null;
            this.popupWindowShowActionImprtrFl.Caption = "Importar Inventario Físico";
            this.popupWindowShowActionImprtrFl.ConfirmationMessage = null;
            this.popupWindowShowActionImprtrFl.Id = "783f04e9-7fc6-4229-9e5a-50486e03d182";
            this.popupWindowShowActionImprtrFl.ToolTip = null;
            this.popupWindowShowActionImprtrFl.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionImprtrFl_CustomizePopupWindowParams);
            this.popupWindowShowActionImprtrFl.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionImprtrFl_Execute);
            // 
            // VCInventarioFisicoImportar
            // 
            this.Actions.Add(this.popupWindowShowActionImprtrFl);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionImprtrFl;
    }
}
