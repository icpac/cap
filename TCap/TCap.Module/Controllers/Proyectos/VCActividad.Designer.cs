namespace TCap.Module.Controllers.Proyectos
{
    partial class VCActividad
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
            this.popupWindowShowActionActvddCmpltd = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.simpleActionActvddEjccn = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.simpleActionActvddSspndr = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.popupWindowShowActionActvddSspndr = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popupWindowShowActionActvddCmpltd
            // 
            this.popupWindowShowActionActvddCmpltd.AcceptButtonCaption = "Listo";
            this.popupWindowShowActionActvddCmpltd.CancelButtonCaption = null;
            this.popupWindowShowActionActvddCmpltd.Caption = "Completada";
            this.popupWindowShowActionActvddCmpltd.Category = "Edit";
            this.popupWindowShowActionActvddCmpltd.ConfirmationMessage = null;
            this.popupWindowShowActionActvddCmpltd.Id = "2ca59123-c5e6-4a78-8f74-cc0c27c02cd7";
            this.popupWindowShowActionActvddCmpltd.ToolTip = null;
            this.popupWindowShowActionActvddCmpltd.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionActvddCmpltd_CustomizePopupWindowParams);
            this.popupWindowShowActionActvddCmpltd.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionActvddCmpltd_Execute);
            // 
            // simpleActionActvddEjccn
            // 
            this.simpleActionActvddEjccn.Caption = "En Ejecución";
            this.simpleActionActvddEjccn.Category = "Edit";
            this.simpleActionActvddEjccn.ConfirmationMessage = "Está seguro de Iniciar la Actividad?";
            this.simpleActionActvddEjccn.Id = "4828753c-4027-460a-9d7c-b19895b60ab3";
            this.simpleActionActvddEjccn.ToolTip = null;
            this.simpleActionActvddEjccn.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionActvddEjccn_Execute);
            // 
            // simpleActionActvddSspndr
            // 
            this.simpleActionActvddSspndr.Caption = "Suspeder Actividad";
            this.simpleActionActvddSspndr.ConfirmationMessage = "Está seguro de Suspender la Actividad?";
            this.simpleActionActvddSspndr.Id = "660afd78-9c16-49d8-89e6-bca137270718";
            this.simpleActionActvddSspndr.ToolTip = null;
            this.simpleActionActvddSspndr.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionActvddSspndr_Execute);
            // 
            // popupWindowShowActionActvddSspndr
            // 
            this.popupWindowShowActionActvddSspndr.AcceptButtonCaption = "Suspender";
            this.popupWindowShowActionActvddSspndr.CancelButtonCaption = null;
            this.popupWindowShowActionActvddSspndr.Caption = "Suspender Actividad";
            this.popupWindowShowActionActvddSspndr.Category = "Edit";
            this.popupWindowShowActionActvddSspndr.ConfirmationMessage = "Está seguro de Suspender?";
            this.popupWindowShowActionActvddSspndr.Id = "461707b0-b917-4f12-988f-d3647692dd79";
            this.popupWindowShowActionActvddSspndr.ToolTip = null;
            this.popupWindowShowActionActvddSspndr.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionActvddSspndr_CustomizePopupWindowParams);
            this.popupWindowShowActionActvddSspndr.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionActvddSspndr_Execute);
            // 
            // VCActividad
            // 
            this.Actions.Add(this.popupWindowShowActionActvddCmpltd);
            this.Actions.Add(this.simpleActionActvddEjccn);
            this.Actions.Add(this.simpleActionActvddSspndr);
            this.Actions.Add(this.popupWindowShowActionActvddSspndr);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionActvddCmpltd;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionActvddEjccn;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionActvddSspndr;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionActvddSspndr;
    }
}
