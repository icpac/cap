namespace Cap.Ventas.Controllers
{
    partial class WCLoadData
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
            this.simpleActionUpldCtlg = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.popupWindowShowActionCrgCtlgsCfdi = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // simpleActionUpldCtlg
            // 
            this.simpleActionUpldCtlg.Caption = "Cargar Catálogo";
            this.simpleActionUpldCtlg.ConfirmationMessage = "Está seguro de Cargar los Datos ?";
            this.simpleActionUpldCtlg.Id = "UpldCtlg";
            this.simpleActionUpldCtlg.ToolTip = null;
            this.simpleActionUpldCtlg.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionUpldCtlg_Execute);
            // 
            // popupWindowShowActionCrgCtlgsCfdi
            // 
            this.popupWindowShowActionCrgCtlgsCfdi.AcceptButtonCaption = "Cargar Datos";
            this.popupWindowShowActionCrgCtlgsCfdi.CancelButtonCaption = null;
            this.popupWindowShowActionCrgCtlgsCfdi.Caption = "Cargar Catalogos CFDI";
            this.popupWindowShowActionCrgCtlgsCfdi.ConfirmationMessage = null;
            this.popupWindowShowActionCrgCtlgsCfdi.Id = "1ba7a303-dc23-42e2-ad6b-0f700cec7475";
            this.popupWindowShowActionCrgCtlgsCfdi.ToolTip = null;
            this.popupWindowShowActionCrgCtlgsCfdi.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionCrgCtlgsCfdi_CustomizePopupWindowParams);
            this.popupWindowShowActionCrgCtlgsCfdi.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionCrgCtlgsCfdi_Execute);
            // 
            // WCLoadData
            // 
            this.Actions.Add(this.simpleActionUpldCtlg);
            this.Actions.Add(this.popupWindowShowActionCrgCtlgsCfdi);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionUpldCtlg;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionCrgCtlgsCfdi;
    }
}
