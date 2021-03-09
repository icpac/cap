namespace SSRV.Module.Controllers
{
    partial class VCIncidencia
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
            this.simpleActionReincidencia = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.popupWindowShowActionIncdncRspnsbl = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.popupWindowShowActionIncdncStart = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.popupWindowShowActionIncdncEspr = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.popupWindowShowActionIncdncTrmnd = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.popupWindowShowActionIncdncCncld = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.popupWindowShowActionAgrgrBtcr = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.simpleActionCnclr = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // simpleActionReincidencia
            // 
            this.simpleActionReincidencia.Caption = "Reabrir Reporte";
            this.simpleActionReincidencia.ConfirmationMessage = "Está seguro de abrirlo?";
            this.simpleActionReincidencia.Id = "efc512d2-7513-4450-baef-cfb0eda907ad";
            this.simpleActionReincidencia.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleActionReincidencia.ToolTip = null;
            this.simpleActionReincidencia.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionReincidencia_Execute);
            // 
            // popupWindowShowActionIncdncRspnsbl
            // 
            this.popupWindowShowActionIncdncRspnsbl.AcceptButtonCaption = null;
            this.popupWindowShowActionIncdncRspnsbl.CancelButtonCaption = null;
            this.popupWindowShowActionIncdncRspnsbl.Caption = "Asigna Responsable";
            this.popupWindowShowActionIncdncRspnsbl.ConfirmationMessage = null;
            this.popupWindowShowActionIncdncRspnsbl.Id = "d455489a-7a6c-44f6-9740-50c9b948689a";
            this.popupWindowShowActionIncdncRspnsbl.ToolTip = null;
            this.popupWindowShowActionIncdncRspnsbl.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionIncdncHrr_CustomizePopupWindowParams);
            this.popupWindowShowActionIncdncRspnsbl.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionIncdncHrr_Execute);
            // 
            // popupWindowShowActionIncdncStart
            // 
            this.popupWindowShowActionIncdncStart.AcceptButtonCaption = null;
            this.popupWindowShowActionIncdncStart.CancelButtonCaption = null;
            this.popupWindowShowActionIncdncStart.Caption = "Atención en Curso";
            this.popupWindowShowActionIncdncStart.ConfirmationMessage = null;
            this.popupWindowShowActionIncdncStart.Id = "eebeda78-f073-4c25-ba6a-dc8625a3e548";
            this.popupWindowShowActionIncdncStart.ToolTip = null;
            this.popupWindowShowActionIncdncStart.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionIncdncStart_CustomizePopupWindowParams);
            this.popupWindowShowActionIncdncStart.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionIncdncStart_Execute);
            // 
            // popupWindowShowActionIncdncEspr
            // 
            this.popupWindowShowActionIncdncEspr.AcceptButtonCaption = null;
            this.popupWindowShowActionIncdncEspr.CancelButtonCaption = null;
            this.popupWindowShowActionIncdncEspr.Caption = "Sin Solución";
            this.popupWindowShowActionIncdncEspr.ConfirmationMessage = null;
            this.popupWindowShowActionIncdncEspr.Id = "e6e6ee60-b413-4a85-8fae-ac0bf667c6e9";
            this.popupWindowShowActionIncdncEspr.ToolTip = null;
            this.popupWindowShowActionIncdncEspr.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionIncdncEspr_CustomizePopupWindowParams);
            this.popupWindowShowActionIncdncEspr.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionIncdncEspr_Execute);
            // 
            // popupWindowShowActionIncdncTrmnd
            // 
            this.popupWindowShowActionIncdncTrmnd.AcceptButtonCaption = null;
            this.popupWindowShowActionIncdncTrmnd.CancelButtonCaption = null;
            this.popupWindowShowActionIncdncTrmnd.Caption = "Incidencia Resuelta";
            this.popupWindowShowActionIncdncTrmnd.ConfirmationMessage = null;
            this.popupWindowShowActionIncdncTrmnd.Id = "2e5f71ec-6439-46f9-b1c2-fe3128314834";
            this.popupWindowShowActionIncdncTrmnd.ToolTip = null;
            this.popupWindowShowActionIncdncTrmnd.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionIncdncTrmnd_CustomizePopupWindowParams);
            this.popupWindowShowActionIncdncTrmnd.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionIncdncTrmnd_Execute);
            // 
            // popupWindowShowActionIncdncCncld
            // 
            this.popupWindowShowActionIncdncCncld.AcceptButtonCaption = null;
            this.popupWindowShowActionIncdncCncld.CancelButtonCaption = null;
            this.popupWindowShowActionIncdncCncld.Caption = "Cancelar";
            this.popupWindowShowActionIncdncCncld.ConfirmationMessage = null;
            this.popupWindowShowActionIncdncCncld.Id = "5d2a0661-d175-402e-991e-a57673d4b9d9";
            this.popupWindowShowActionIncdncCncld.ToolTip = null;
            this.popupWindowShowActionIncdncCncld.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionIncdncCncld_CustomizePopupWindowParams);
            this.popupWindowShowActionIncdncCncld.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionIncdncCncld_Execute);
            // 
            // popupWindowShowActionAgrgrBtcr
            // 
            this.popupWindowShowActionAgrgrBtcr.AcceptButtonCaption = null;
            this.popupWindowShowActionAgrgrBtcr.CancelButtonCaption = null;
            this.popupWindowShowActionAgrgrBtcr.Caption = "Agregar Bitacora";
            this.popupWindowShowActionAgrgrBtcr.ConfirmationMessage = null;
            this.popupWindowShowActionAgrgrBtcr.Id = "69561462-8a90-4bac-8f72-898569a0a4f0";
            this.popupWindowShowActionAgrgrBtcr.ToolTip = null;
            this.popupWindowShowActionAgrgrBtcr.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionAgrgrBtcr_CustomizePopupWindowParams);
            this.popupWindowShowActionAgrgrBtcr.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionAgrgrBtcr_Execute);
            // 
            // simpleActionCnclr
            // 
            this.simpleActionCnclr.Caption = "Cancelar";
            this.simpleActionCnclr.ConfirmationMessage = "Está seguro de CANCELAR ?";
            this.simpleActionCnclr.Id = "a3dbb6e8-be23-4527-9567-a1a5072d297d";
            this.simpleActionCnclr.ToolTip = null;
            this.simpleActionCnclr.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionCnclr_Execute);
            // 
            // VCIncidencia
            // 
            this.Actions.Add(this.simpleActionReincidencia);
            this.Actions.Add(this.popupWindowShowActionIncdncRspnsbl);
            this.Actions.Add(this.popupWindowShowActionIncdncStart);
            this.Actions.Add(this.popupWindowShowActionIncdncEspr);
            this.Actions.Add(this.popupWindowShowActionIncdncTrmnd);
            this.Actions.Add(this.popupWindowShowActionIncdncCncld);
            this.Actions.Add(this.popupWindowShowActionAgrgrBtcr);
            this.Actions.Add(this.simpleActionCnclr);
            this.ViewControlsCreated += new System.EventHandler(this.VCIncidencia_ViewControlsCreated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionReincidencia;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionIncdncRspnsbl;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionIncdncStart;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionIncdncEspr;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionIncdncTrmnd;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionIncdncCncld;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionAgrgrBtcr;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionCnclr;
    }
}
