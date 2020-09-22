namespace Cap.Bancos.Controllers
{
    partial class VCBancaria
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
            this.simpleActionBajCnta = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.simpleActionAltCnta = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.simpleActionReprtCunts = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.popupWindowShowActionTrnsfrr = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.popupWindowShowActionAddMov = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.popupWindowShowActionMovsCta = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // simpleActionBajCnta
            // 
            this.simpleActionBajCnta.Caption = "Dar de Baja la Cuenta";
            this.simpleActionBajCnta.ConfirmationMessage = "¿Está seguro de dar de Baja la Cuenta ?";
            this.simpleActionBajCnta.Id = "a90f12d8-0e0e-47cc-8500-39d4473411f9";
            this.simpleActionBajCnta.TargetObjectsCriteria = "Status = StatusTipo.Alta";
            this.simpleActionBajCnta.ToolTip = null;
            this.simpleActionBajCnta.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionBajCnta_Execute);
            // 
            // simpleActionAltCnta
            // 
            this.simpleActionAltCnta.Caption = "Activar la Cuenta";
            this.simpleActionAltCnta.ConfirmationMessage = null;
            this.simpleActionAltCnta.Id = "09b5a443-42ea-40bf-9905-01ad48cb5a5e";
            this.simpleActionAltCnta.ToolTip = null;
            this.simpleActionAltCnta.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionAltCnta_Execute);
            // 
            // simpleActionReprtCunts
            // 
            this.simpleActionReprtCunts.Caption = "Reporte Cuentas";
            this.simpleActionReprtCunts.Category = "Print";
            this.simpleActionReprtCunts.ConfirmationMessage = null;
            this.simpleActionReprtCunts.Id = "37fad02c-913e-404c-80e1-8db9d5c7fb82";
            this.simpleActionReprtCunts.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.simpleActionReprtCunts.ToolTip = null;
            this.simpleActionReprtCunts.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.simpleActionReprtCunts.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionReprtCunts_Execute);
            // 
            // popupWindowShowActionTrnsfrr
            // 
            this.popupWindowShowActionTrnsfrr.AcceptButtonCaption = "Transferir";
            this.popupWindowShowActionTrnsfrr.CancelButtonCaption = null;
            this.popupWindowShowActionTrnsfrr.Caption = "Transferencia";
            this.popupWindowShowActionTrnsfrr.Category = "Menu";
            this.popupWindowShowActionTrnsfrr.ConfirmationMessage = null;
            this.popupWindowShowActionTrnsfrr.Id = "8087cca2-48d5-4bff-b128-17cda83a95a3";
            this.popupWindowShowActionTrnsfrr.ToolTip = null;
            this.popupWindowShowActionTrnsfrr.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionTrnsfrr_CustomizePopupWindowParams);
            this.popupWindowShowActionTrnsfrr.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionTrnsfrr_Execute);
            // 
            // popupWindowShowActionAddMov
            // 
            this.popupWindowShowActionAddMov.AcceptButtonCaption = "Aceptar";
            this.popupWindowShowActionAddMov.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
            this.popupWindowShowActionAddMov.CancelButtonCaption = "Cancelar";
            this.popupWindowShowActionAddMov.Caption = "Agregar Movimiento";
            this.popupWindowShowActionAddMov.Category = "Print";
            this.popupWindowShowActionAddMov.ConfirmationMessage = null;
            this.popupWindowShowActionAddMov.Id = "dd28d94a-98c6-48a9-8ea6-77538f42f89e";
            this.popupWindowShowActionAddMov.ToolTip = null;
            this.popupWindowShowActionAddMov.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionAddMov_CustomizePopupWindowParams);
            this.popupWindowShowActionAddMov.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionAddMov_Execute);
            // 
            // popupWindowShowActionMovsCta
            // 
            this.popupWindowShowActionMovsCta.AcceptButtonCaption = "Aceptar";
            this.popupWindowShowActionMovsCta.CancelButtonCaption = "Cancelar";
            this.popupWindowShowActionMovsCta.Caption = "Movimientos de la Cuenta";
            this.popupWindowShowActionMovsCta.Category = "Print";
            this.popupWindowShowActionMovsCta.ConfirmationMessage = null;
            this.popupWindowShowActionMovsCta.Id = "94220764-9626-4265-9e5a-01e448c7f746";
            this.popupWindowShowActionMovsCta.ToolTip = null;
            this.popupWindowShowActionMovsCta.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionMovsCta_CustomizePopupWindowParams);
            // 
            // VCBancaria
            // 
            this.Actions.Add(this.simpleActionBajCnta);
            this.Actions.Add(this.simpleActionAltCnta);
            this.Actions.Add(this.simpleActionReprtCunts);
            this.Actions.Add(this.popupWindowShowActionTrnsfrr);
            this.Actions.Add(this.popupWindowShowActionAddMov);
            this.Actions.Add(this.popupWindowShowActionMovsCta);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionBajCnta;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionAltCnta;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionReprtCunts;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionTrnsfrr;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionAddMov;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionMovsCta;
    }
}
