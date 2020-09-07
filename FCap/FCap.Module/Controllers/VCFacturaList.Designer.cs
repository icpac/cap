using Cap.Ventas.BusinessObjects;

namespace FCap.Module.Controllers
{
    partial class VCFacturaList
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
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem25 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem26 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem27 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem28 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem29 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem30 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem31 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem32 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem33 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem34 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem35 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem36 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            this.simpleActionCncilr = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.simpleActionGetXml = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.singleChoiceActionPolz = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.simpleActionRprtCntbl = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.popupWindowShowActionGetXml = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // simpleActionCncilr
            // 
            this.simpleActionCncilr.Caption = "Conciliar Documentos";
            this.simpleActionCncilr.Category = "Options";
            this.simpleActionCncilr.ConfirmationMessage = "Está seguro de Conciliar?";
            this.simpleActionCncilr.Id = "b80efa59-e226-4707-9470-ebdfdf29e21a";
            this.simpleActionCncilr.ToolTip = null;
            this.simpleActionCncilr.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionCncilr_Execute);
            // 
            // simpleActionGetXml
            // 
            this.simpleActionGetXml.Caption = "Obtener del XML";
            this.simpleActionGetXml.Category = "Tools";
            this.simpleActionGetXml.ConfirmationMessage = "Está seguro de Obtener del XML ?";
            this.simpleActionGetXml.Id = "GetXml";
            this.simpleActionGetXml.ToolTip = "Lee los Xml y obtiene Clientes, Productos y Facturas";
            this.simpleActionGetXml.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionGetXml_Execute);
            // 
            // singleChoiceActionPolz
            // 
            this.singleChoiceActionPolz.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
            this.singleChoiceActionPolz.Caption = "Crear Póliza";
            this.singleChoiceActionPolz.Category = "Tools";
            this.singleChoiceActionPolz.ConfirmationMessage = "Está seguro de generar la Póliza";
            this.singleChoiceActionPolz.Id = "8d342eda-0fed-44e8-b389-530f615ee539";
            choiceActionItem25.Caption = "Enero";
            choiceActionItem25.Data = "1";
            choiceActionItem25.Id = "Enero";
            choiceActionItem25.ImageName = null;
            choiceActionItem25.Shortcut = null;
            choiceActionItem25.ToolTip = null;
            choiceActionItem26.Caption = "Febrero";
            choiceActionItem26.Data = "2";
            choiceActionItem26.Id = "Febrero";
            choiceActionItem26.ImageName = null;
            choiceActionItem26.Shortcut = null;
            choiceActionItem26.ToolTip = null;
            choiceActionItem27.Caption = "Marzo";
            choiceActionItem27.Data = "3";
            choiceActionItem27.Id = "Marzo";
            choiceActionItem27.ImageName = null;
            choiceActionItem27.Shortcut = null;
            choiceActionItem27.ToolTip = null;
            choiceActionItem28.Caption = "Abril";
            choiceActionItem28.Data = "4";
            choiceActionItem28.Id = "Abril";
            choiceActionItem28.ImageName = null;
            choiceActionItem28.Shortcut = null;
            choiceActionItem28.ToolTip = null;
            choiceActionItem29.Caption = "Mayo";
            choiceActionItem29.Data = "5";
            choiceActionItem29.Id = "Mayo";
            choiceActionItem29.ImageName = null;
            choiceActionItem29.Shortcut = null;
            choiceActionItem29.ToolTip = null;
            choiceActionItem30.Caption = "Junio";
            choiceActionItem30.Data = "6";
            choiceActionItem30.Id = "Junio";
            choiceActionItem30.ImageName = null;
            choiceActionItem30.Shortcut = null;
            choiceActionItem30.ToolTip = null;
            choiceActionItem31.Caption = "Julio";
            choiceActionItem31.Data = "7";
            choiceActionItem31.Id = "Julio";
            choiceActionItem31.ImageName = null;
            choiceActionItem31.Shortcut = null;
            choiceActionItem31.ToolTip = null;
            choiceActionItem32.Caption = "Agosto";
            choiceActionItem32.Data = "8";
            choiceActionItem32.Id = "Agosto";
            choiceActionItem32.ImageName = null;
            choiceActionItem32.Shortcut = null;
            choiceActionItem32.ToolTip = null;
            choiceActionItem33.Caption = "Septiembre";
            choiceActionItem33.Data = "9";
            choiceActionItem33.Id = "Septiembre";
            choiceActionItem33.ImageName = null;
            choiceActionItem33.Shortcut = null;
            choiceActionItem33.ToolTip = null;
            choiceActionItem34.Caption = "Octubre";
            choiceActionItem34.Data = "10";
            choiceActionItem34.Id = "Octubre";
            choiceActionItem34.ImageName = null;
            choiceActionItem34.Shortcut = null;
            choiceActionItem34.ToolTip = null;
            choiceActionItem35.Caption = "Noviembre";
            choiceActionItem35.Data = "11";
            choiceActionItem35.Id = "Noviembre";
            choiceActionItem35.ImageName = null;
            choiceActionItem35.Shortcut = null;
            choiceActionItem35.ToolTip = null;
            choiceActionItem36.Caption = "Diciembre";
            choiceActionItem36.Data = "12";
            choiceActionItem36.Id = "Diciembre";
            choiceActionItem36.ImageName = null;
            choiceActionItem36.Shortcut = null;
            choiceActionItem36.ToolTip = null;
            this.singleChoiceActionPolz.Items.Add(choiceActionItem25);
            this.singleChoiceActionPolz.Items.Add(choiceActionItem26);
            this.singleChoiceActionPolz.Items.Add(choiceActionItem27);
            this.singleChoiceActionPolz.Items.Add(choiceActionItem28);
            this.singleChoiceActionPolz.Items.Add(choiceActionItem29);
            this.singleChoiceActionPolz.Items.Add(choiceActionItem30);
            this.singleChoiceActionPolz.Items.Add(choiceActionItem31);
            this.singleChoiceActionPolz.Items.Add(choiceActionItem32);
            this.singleChoiceActionPolz.Items.Add(choiceActionItem33);
            this.singleChoiceActionPolz.Items.Add(choiceActionItem34);
            this.singleChoiceActionPolz.Items.Add(choiceActionItem35);
            this.singleChoiceActionPolz.Items.Add(choiceActionItem36);
            this.singleChoiceActionPolz.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Caption;
            this.singleChoiceActionPolz.ShowItemsOnClick = true;
            this.singleChoiceActionPolz.TargetObjectType = typeof(Cap.Ventas.BusinessObjects.DocumentoSalida);
            this.singleChoiceActionPolz.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.singleChoiceActionPolz.ToolTip = null;
            this.singleChoiceActionPolz.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.singleChoiceActionPolz.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.singleChoiceActionPolz_Execute);
            // 
            // simpleActionRprtCntbl
            // 
            this.simpleActionRprtCntbl.Caption = "Reporte Contable";
            this.simpleActionRprtCntbl.Category = "Tools";
            this.simpleActionRprtCntbl.ConfirmationMessage = "Está seguro de Crear el Reporte ?";
            this.simpleActionRprtCntbl.Id = "RprtCntbl";
            this.simpleActionRprtCntbl.ToolTip = "Genera la hoja para la Declaración";
            this.simpleActionRprtCntbl.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleActionRprtCntbl_Execute);
            // 
            // popupWindowShowActionGetXml
            // 
            this.popupWindowShowActionGetXml.AcceptButtonCaption = "Cargar";
            this.popupWindowShowActionGetXml.CancelButtonCaption = "Cancelar";
            this.popupWindowShowActionGetXml.Caption = "Cargar Facturas de XML";
            this.popupWindowShowActionGetXml.ConfirmationMessage = null;
            this.popupWindowShowActionGetXml.Id = "61fe98b5-e613-4bc6-8c1a-0f34cbb01dad";
            this.popupWindowShowActionGetXml.ToolTip = "Lee los Xml y obtiene Clientes, Productos y Facturas";
            this.popupWindowShowActionGetXml.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionGetXml_CustomizePopupWindowParams);
            this.popupWindowShowActionGetXml.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionGetXml_Execute);
            // 
            // VCFacturaList
            // 
            this.Actions.Add(this.simpleActionCncilr);
            this.Actions.Add(this.simpleActionGetXml);
            this.Actions.Add(this.singleChoiceActionPolz);
            this.Actions.Add(this.simpleActionRprtCntbl);
            this.Actions.Add(this.popupWindowShowActionGetXml);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction singleChoiceActionPolz;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionCncilr;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionGetXml;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionRprtCntbl;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionGetXml;
    }
}
