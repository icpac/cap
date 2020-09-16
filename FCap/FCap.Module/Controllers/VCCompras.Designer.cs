namespace FCap.Module.Controllers
{
    partial class VCCompras
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
            this.simpleActionRprtCmprs = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.popupWindowShowActionLdXml = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // simpleActionRprtCmprs
            // 
            this.simpleActionRprtCmprs.Caption = "Resumen Compras";
            this.simpleActionRprtCmprs.Category = "Reports";
            this.simpleActionRprtCmprs.ConfirmationMessage = null;
            this.simpleActionRprtCmprs.Id = "aaebccbc-267c-4466-8425-ae651873e13f";
            this.simpleActionRprtCmprs.ToolTip = null;
            this.simpleActionRprtCmprs.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionRprtCmprs_Execute);
            // 
            // popupWindowShowActionLdXml
            // 
            this.popupWindowShowActionLdXml.AcceptButtonCaption = "Cargar Recepciones de XML";
            this.popupWindowShowActionLdXml.CancelButtonCaption = "Cancelar";
            this.popupWindowShowActionLdXml.Caption = "Cargar Recepciones de XML";
            this.popupWindowShowActionLdXml.ConfirmationMessage = null;
            this.popupWindowShowActionLdXml.Id = "3d814d63-7c64-4b99-969d-d3a914d3fde1";
            this.popupWindowShowActionLdXml.ToolTip = null;
            this.popupWindowShowActionLdXml.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionLdXml_CustomizePopupWindowParams);
            this.popupWindowShowActionLdXml.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionLdXml_Execute);
            // 
            // VCCompras
            // 
            this.Actions.Add(this.simpleActionRprtCmprs);
            this.Actions.Add(this.popupWindowShowActionLdXml);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionRprtCmprs;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionLdXml;
    }
}
