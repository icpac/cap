using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.Data;
using DevExpress.Xpo.DB;

namespace FCap.Module.Controllers
{
    using FCap.Module.BusinessObjects.Ventas;
    using DevExpress.ExpressApp.Xpo;
    using DevExpress.Data.Filtering;
    using System.Xml;
    using System.Collections;
    using DevExpress.Xpo;
    using System.IO;
    using FCap.Module.Utilerias;
    using DevExpress.Spreadsheet;
    using Cap.Generales.BusinessObjects.Empresa;
    using System.Drawing;
    using Cap.Ventas.BusinessObjects;
    using Cap.Compras.BusinessObjects;
    using DevExpress.Persistent.Base;
    using sw.descargamasiva;
    using System.IO.Compression;

    // For more information on Controllers and their life cycle, check out the http://documentation.devexpress.com/#Xaf/CustomDocument2621 and http://documentation.devexpress.com/#Xaf/CustomDocument3118 help articles.
    public partial class VCFacturaList : ViewController
    {
        // Use this to do something when a Controller is instantiated (do not execute heavy operations here!).
        public VCFacturaList()
        {
            InitializeComponent();
            RegisterActions(components);
            // For instance, you can specify activation conditions of a Controller or create its Actions (http://documentation.devexpress.com/#Xaf/CustomDocument2622).
            //TargetObjectType = typeof(DomainObject1);
            //TargetViewType = ViewType.DetailView;
            //TargetViewId = "DomainObject1_DetailView";
            //TargetViewNesting = Nesting.Root;
            //SimpleAction myAction = new SimpleAction(this, "MyActionId", DevExpress.Persistent.Base.PredefinedCategory.RecordEdit);
        
            TargetObjectType = typeof(DocumentoSalida);
            TargetViewType = ViewType.ListView;

            simpleActionCncilr.TargetObjectType = typeof(DocumentoSalida);
            simpleActionCncilr.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;

            simpleActionGetXml.TargetObjectType = typeof(DocumentoSalida);
            simpleActionGetXml.TargetViewType = ViewType.ListView;

            simpleActionRprtCntbl.TargetObjectType = typeof(DocumentoSalida);
            simpleActionRprtCntbl.TargetViewType = ViewType.ListView;

            popupWindowShowActionDscrgMsva.TargetObjectType = typeof(DocumentoSalida);
            popupWindowShowActionDscrgMsva.TargetViewType = ViewType.ListView;
        }

        // Override to do something before Controllers are activated within the current Frame (their View property is not yet assigned).
        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();
            //For instance, you can access another Controller via the Frame.GetController<AnotherControllerType>() method to customize it or subscribe to its events.
        }

        // Override to do something when a Controller is activated and its View is assigned.
        protected override void OnActivated()
        {
            base.OnActivated();
            //For instance, you can customize the current View and its editors (http://documentation.devexpress.com/#Xaf/CustomDocument2729) or manage the Controller's Actions visibility and availability (http://documentation.devexpress.com/#Xaf/CustomDocument2728).


            string propertyName = "FechaDoc";
            bool demoFlag = true;
            //Dennis: This code applies a client side sorting.
            if (demoFlag)
            {
                IModelColumn columnInfo = ((IModelList<IModelColumn>)((ListView)View).Model.Columns)[propertyName];
                if (columnInfo != null)
                {
                    columnInfo.SortIndex = 0;
                    columnInfo.SortOrder = ColumnSortOrder.Descending;
                }
            }
            else
            {
                //Dennis: This code is used for the server side sorting.
                if (((IModelList<IModelSortProperty>)((ListView)View).Model.Sorting)[propertyName] == null)
                {
                    IModelSortProperty sortProperty = ((ListView)View).Model.Sorting.AddNode<IModelSortProperty>(propertyName);
                    sortProperty.Direction = SortingDirection.Descending;
                    sortProperty.PropertyName = propertyName;
                }
            }

            UpdateAction();


            bool puede = SecuritySystem.CurrentUserName == "root";
            simpleActionRprtCntbl.Enabled.SetItemValue("SecurityAllowance", puede);
            simpleActionRprtCntbl.Active.SetItemValue("Visible", puede);
        }

        // Override to access the controls of a View for which the current Controller is intended.
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // For instance, refer to the http://documentation.devexpress.com/Xaf/CustomDocument3165.aspx help article to see how to access grid control properties.
        }
        // Override to do something when a Controller is deactivated.
        protected override void OnDeactivated()
        {
            // For instance, you can unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void singleChoiceActionPolz_Execute(object sender, DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventArgs e)
        {
            int mes = Convert.ToInt32((e.SelectedChoiceActionItem.Data));

            NegocioAdmin.PolizaPel(Application.CreateObjectSpace(), mes);
        }

        private void simpleActionCncilr_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            if (View != null && View.ObjectSpace != null)
            {
                IObjectSpace objectSpace = Application.CreateObjectSpace();

                NegocioAdmin.Concilia(objectSpace);

                objectSpace.CommitChanges();
            }
        }

        private void simpleActionGetXml_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
        }

        private void UpdateAction()
        {
            bool puede = SecuritySystem.CurrentUserName == "root";

            this.simpleActionGetXml.Enabled.SetItemValue("SecurityAllowance", puede);
            if (!puede)
                simpleActionGetXml.Active.SetItemValue("Visible", false);

            popupWindowShowActionDscrgMsva.Active.SetItemValue("Visible", Licencia());
        }

        private bool Licencia()
        {
            return NegocioAdmin.Licencia(View.ObjectSpace);
        }

        private void simpleActionRprtCntbl_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            if (View != null)
            {
                NegocioAdmin.ReporteContable(View.ObjectSpace);
            }
        }

        private void popupWindowShowActionGetXml_CustomizePopupWindowParams(object sender, DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            // Lo estamos usando en Recepción y Facturas !!
            CargaRecepcion newObj;

            newObj = objectSpace.FindObject<CargaRecepcion>(null);

            if (newObj == null)
                newObj = objectSpace.CreateObject<CargaRecepcion>();

            newObj.Rt = Prms.RutaPdfVnts;

            e.View = Application.CreateDetailView(objectSpace, "CargaRecepcion_DetailView", true, newObj);
            e.View.Caption = "Cargar Facturas";
        }

        private void popupWindowShowActionGetXml_Execute(object sender, DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventArgs e)
        {
            CargaRecepcion obj = e.PopupWindowViewCurrentObject as CargaRecepcion;
            IObjectSpace objSp = Application.CreateObjectSpace();

            NegocioAdmin.CrgDeXml(obj, objSp);
        }

        private VentaCfdi mPrms;
        private VentaCfdi Prms
        {
            get
            {
                if (mPrms == null)
                {
                    if (View != null && View.ObjectSpace != null)
                        mPrms = View.ObjectSpace.FindObject<VentaCfdi>(null);
                }
                return mPrms;
            }
        }

        private void popupWindowShowActionDscrgMsv_CustomizePopupWindowParams(object sender, DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            DescargaMasiva newObj;
            Empresa mEmpresa = View.ObjectSpace.FindObject<Empresa>(null);

            newObj = objectSpace.FindObject<DescargaMasiva>(null);
            if (newObj == null)
                newObj = objectSpace.CreateObject<DescargaMasiva>();

            newObj.RfcEmsr = mEmpresa.Compania.Rfc;
            newObj.RfcRcptr = mEmpresa.Compania.Rfc;
            newObj.RtDscrg = Prms.RutaPdfVnts;

            e.View = Application.CreateDetailView(objectSpace, "DescargaMasiva_DetailView", true, newObj);
        }

        private void popupWindowShowActionDscrgMsv_Execute(object sender, DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventArgs e)
        {
            DescargaMasiva obj = e.PopupWindowViewCurrentObject as DescargaMasiva;

            if (obj != null)
            {
                NegocioAdmin.DescargaSat(obj, View.ObjectSpace, Prms);

                View.RefreshDataSource();
            }
        }
    }
}
