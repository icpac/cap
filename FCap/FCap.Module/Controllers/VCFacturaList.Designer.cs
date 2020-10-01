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
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem1 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem2 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem3 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem4 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem5 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem6 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem7 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem8 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem9 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem10 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem11 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem12 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            this.simpleActionCncilr = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.simpleActionGetXml = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.simpleActionRprtCntbl = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.popupWindowShowActionGetXml = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.popupWindowShowActionDscrgMsva = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.singleChoiceActionPolz = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
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
            // popupWindowShowActionDscrgMsva
            // 
            this.popupWindowShowActionDscrgMsva.AcceptButtonCaption = "Descargar";
            this.popupWindowShowActionDscrgMsva.CancelButtonCaption = "Cancelar";
            this.popupWindowShowActionDscrgMsva.Caption = "Descarga Masiva";
            this.popupWindowShowActionDscrgMsva.ConfirmationMessage = null;
            this.popupWindowShowActionDscrgMsva.Id = "8889800e-c855-45ce-9392-cd3a4d20f86aa";
            this.popupWindowShowActionDscrgMsva.ToolTip = null;
            this.popupWindowShowActionDscrgMsva.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionDscrgMsv_CustomizePopupWindowParams);
            this.popupWindowShowActionDscrgMsva.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionDscrgMsv_Execute);
            // 
            // singleChoiceActionPolz
            // 
            this.singleChoiceActionPolz.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
            this.singleChoiceActionPolz.Caption = "Crear Póliza";
            this.singleChoiceActionPolz.Category = "Tools";
            this.singleChoiceActionPolz.ConfirmationMessage = "Está seguro de generar la Póliza";
            this.singleChoiceActionPolz.Id = "8d342eda-0fed-44e8-b389-530f615ee539";
            choiceActionItem1.Caption = "Enero";
            choiceActionItem1.Data = "1";
            choiceActionItem1.Id = "Enero";
            choiceActionItem1.ImageName = null;
            choiceActionItem1.Shortcut = null;
            choiceActionItem1.ToolTip = null;
            choiceActionItem2.Caption = "Febrero";
            choiceActionItem2.Data = "2";
            choiceActionItem2.Id = "Febrero";
            choiceActionItem2.ImageName = null;
            choiceActionItem2.Shortcut = null;
            choiceActionItem2.ToolTip = null;
            choiceActionItem3.Caption = "Marzo";
            choiceActionItem3.Data = "3";
            choiceActionItem3.Id = "Marzo";
            choiceActionItem3.ImageName = null;
            choiceActionItem3.Shortcut = null;
            choiceActionItem3.ToolTip = null;
            choiceActionItem4.Caption = "Abril";
            choiceActionItem4.Data = "4";
            choiceActionItem4.Id = "Abril";
            choiceActionItem4.ImageName = null;
            choiceActionItem4.Shortcut = null;
            choiceActionItem4.ToolTip = null;
            choiceActionItem5.Caption = "Mayo";
            choiceActionItem5.Data = "5";
            choiceActionItem5.Id = "Mayo";
            choiceActionItem5.ImageName = null;
            choiceActionItem5.Shortcut = null;
            choiceActionItem5.ToolTip = null;
            choiceActionItem6.Caption = "Junio";
            choiceActionItem6.Data = "6";
            choiceActionItem6.Id = "Junio";
            choiceActionItem6.ImageName = null;
            choiceActionItem6.Shortcut = null;
            choiceActionItem6.ToolTip = null;
            choiceActionItem7.Caption = "Julio";
            choiceActionItem7.Data = "7";
            choiceActionItem7.Id = "Julio";
            choiceActionItem7.ImageName = null;
            choiceActionItem7.Shortcut = null;
            choiceActionItem7.ToolTip = null;
            choiceActionItem8.Caption = "Agosto";
            choiceActionItem8.Data = "8";
            choiceActionItem8.Id = "Agosto";
            choiceActionItem8.ImageName = null;
            choiceActionItem8.Shortcut = null;
            choiceActionItem8.ToolTip = null;
            choiceActionItem9.Caption = "Septiembre";
            choiceActionItem9.Data = "9";
            choiceActionItem9.Id = "Septiembre";
            choiceActionItem9.ImageName = null;
            choiceActionItem9.Shortcut = null;
            choiceActionItem9.ToolTip = null;
            choiceActionItem10.Caption = "Octubre";
            choiceActionItem10.Data = "10";
            choiceActionItem10.Id = "Octubre";
            choiceActionItem10.ImageName = null;
            choiceActionItem10.Shortcut = null;
            choiceActionItem10.ToolTip = null;
            choiceActionItem11.Caption = "Noviembre";
            choiceActionItem11.Data = "11";
            choiceActionItem11.Id = "Noviembre";
            choiceActionItem11.ImageName = null;
            choiceActionItem11.Shortcut = null;
            choiceActionItem11.ToolTip = null;
            choiceActionItem12.Caption = "Diciembre";
            choiceActionItem12.Data = "12";
            choiceActionItem12.Id = "Diciembre";
            choiceActionItem12.ImageName = null;
            choiceActionItem12.Shortcut = null;
            choiceActionItem12.ToolTip = null;
            this.singleChoiceActionPolz.Items.Add(choiceActionItem1);
            this.singleChoiceActionPolz.Items.Add(choiceActionItem2);
            this.singleChoiceActionPolz.Items.Add(choiceActionItem3);
            this.singleChoiceActionPolz.Items.Add(choiceActionItem4);
            this.singleChoiceActionPolz.Items.Add(choiceActionItem5);
            this.singleChoiceActionPolz.Items.Add(choiceActionItem6);
            this.singleChoiceActionPolz.Items.Add(choiceActionItem7);
            this.singleChoiceActionPolz.Items.Add(choiceActionItem8);
            this.singleChoiceActionPolz.Items.Add(choiceActionItem9);
            this.singleChoiceActionPolz.Items.Add(choiceActionItem10);
            this.singleChoiceActionPolz.Items.Add(choiceActionItem11);
            this.singleChoiceActionPolz.Items.Add(choiceActionItem12);
            this.singleChoiceActionPolz.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Caption;
            this.singleChoiceActionPolz.ShowItemsOnClick = true;
            this.singleChoiceActionPolz.TargetObjectType = typeof(Cap.Ventas.BusinessObjects.DocumentoSalida);
            this.singleChoiceActionPolz.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.singleChoiceActionPolz.ToolTip = null;
            this.singleChoiceActionPolz.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.singleChoiceActionPolz.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.singleChoiceActionPolz_Execute);
            // 
            // VCFacturaList
            // 
            this.Actions.Add(this.simpleActionCncilr);
            this.Actions.Add(this.simpleActionGetXml);
            this.Actions.Add(this.singleChoiceActionPolz);
            this.Actions.Add(this.simpleActionRprtCntbl);
            this.Actions.Add(this.popupWindowShowActionGetXml);
            this.Actions.Add(this.popupWindowShowActionDscrgMsva);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction singleChoiceActionPolz;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionCncilr;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionGetXml;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleActionRprtCntbl;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionGetXml;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionDscrgMsva;
    }
}
