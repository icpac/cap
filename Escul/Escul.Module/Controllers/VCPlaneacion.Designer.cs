namespace Escul.Module.Controllers
{
    partial class VCPlaneacion
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
            this.simpleActionHrsSmn = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.simpleActionHrsTtls = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.simpleActionClclFchAplccn = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.popupWindowShowActionCrgTms = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // simpleActionHrsSmn
            // 
            this.simpleActionHrsSmn.Caption = "Calcula Horas Semana";
            this.simpleActionHrsSmn.ConfirmationMessage = "¿Está seguro de Calcular las Horas de la Semana?";
            this.simpleActionHrsSmn.Id = "234ee10c-a0ce-409f-a2a9-c4776ed16fea";
            this.simpleActionHrsSmn.ToolTip = null;
            this.simpleActionHrsSmn.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // simpleActionHrsTtls
            // 
            this.simpleActionHrsTtls.Caption = "Calcula Horas Totales";
            this.simpleActionHrsTtls.ConfirmationMessage = "¿Desea Calcular las Horas Totales?";
            this.simpleActionHrsTtls.Id = "31491a12-6b93-450a-ac95-d59ed88826fb";
            this.simpleActionHrsTtls.ToolTip = null;
            this.simpleActionHrsTtls.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionHrsTtls_Execute);
            // 
            // simpleActionClclFchAplccn
            // 
            this.simpleActionClclFchAplccn.Caption = "Calcula Fechas de Aplicación";
            this.simpleActionClclFchAplccn.ConfirmationMessage = "¿Desea Calcular las Fechas de Aplicación?";
            this.simpleActionClclFchAplccn.Id = "1c98adc2-3b36-4a98-903f-9be591d2bd36";
            this.simpleActionClclFchAplccn.ToolTip = null;
            this.simpleActionClclFchAplccn.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionClclFchAplccn_Execute);
            // 
            // popupWindowShowActionCrgTms
            // 
            this.popupWindowShowActionCrgTms.AcceptButtonCaption = "Cargar";
            this.popupWindowShowActionCrgTms.CancelButtonCaption = "Cancelar";
            this.popupWindowShowActionCrgTms.Caption = "Cargar Temas";
            this.popupWindowShowActionCrgTms.ConfirmationMessage = null;
            this.popupWindowShowActionCrgTms.Id = "b9de6b2e-33d7-434d-8eba-f3294af2145e";
            this.popupWindowShowActionCrgTms.ToolTip = null;
            this.popupWindowShowActionCrgTms.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionCrgTms_CustomizePopupWindowParams);
            this.popupWindowShowActionCrgTms.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionCrgTms_Execute);
            // 
            // VCPlaneacion
            // 
            this.Actions.Add(this.simpleActionHrsSmn);
            this.Actions.Add(this.simpleActionHrsTtls);
            this.Actions.Add(this.simpleActionClclFchAplccn);
            this.Actions.Add(this.popupWindowShowActionCrgTms);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionHrsSmn;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionHrsTtls;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionClclFchAplccn;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionCrgTms;
    }
}
