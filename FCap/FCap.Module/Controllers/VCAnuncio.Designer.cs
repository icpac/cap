namespace FCap.Module.Controllers
{
    partial class VCAnuncio
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
            this.SendAction = new DevExpress.ExpressApp.Actions.ParametrizedAction(this.components);
            this.popupWindowShowActionSnd = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // SendAction
            // 
            this.SendAction.Caption = "Envia anuncio ";
            this.SendAction.ConfirmationMessage = "Está seguro de Enviar el ANUNCIO?";
            this.SendAction.Id = "SendAction";
            this.SendAction.NullValuePrompt = null;
            this.SendAction.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.SendAction.ShortCaption = null;
            this.SendAction.ToolTip = null;
            this.SendAction.Execute += new DevExpress.ExpressApp.Actions.ParametrizedActionExecuteEventHandler(this.SendAction_Execute);
            // 
            // popupWindowShowActionSnd
            // 
            this.popupWindowShowActionSnd.AcceptButtonCaption = "Enviar";
            this.popupWindowShowActionSnd.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
            this.popupWindowShowActionSnd.CancelButtonCaption = "";
            this.popupWindowShowActionSnd.Caption = "Enviar Anuncio";
            this.popupWindowShowActionSnd.ConfirmationMessage = null;
            this.popupWindowShowActionSnd.Id = "c47ea378-d7aa-4a70-b8ee-958fd9b6bde6";
            this.popupWindowShowActionSnd.ToolTip = null;
            this.popupWindowShowActionSnd.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionSnd_CustomizePopupWindowParams);
            this.popupWindowShowActionSnd.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionSnd_Execute);
            // 
            // VCAnuncio
            // 
            this.Actions.Add(this.SendAction);
            this.Actions.Add(this.popupWindowShowActionSnd);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.ParametrizedAction SendAction;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionSnd;
    }
}
