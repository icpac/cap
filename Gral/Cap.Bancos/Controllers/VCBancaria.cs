#region Copyright (c) 2017-2019 cjlc
/*
{+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++)
{                                                                   }
{     cjlc Cap control administrativo personal                      }
{                                                                   }
{     Copyrigth (c) 2017-2019 cjlc                                  }
{     Todos los derechos reservados                                 }
{                                                                   }
{*******************************************************************}
 */
#endregion

using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using Cap.Bancos.BusinessObjects;
using Cap.Bancos.Utilerias;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Reports;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections;
using DevExpress.ExpressApp.Model;

namespace Cap.Bancos.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class VCBancaria : ViewController
    {
        public VCBancaria()
        {
            InitializeComponent();
            RegisterActions(components);

            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(Bancaria);
            TargetViewType = ViewType.ListView;

            simpleActionBajCnta.TargetObjectsCriteria = "Status = 'Activa'";
            simpleActionAltCnta.TargetObjectsCriteria = "Status = 'Baja'";
            simpleActionAltCnta.TargetObjectType = typeof(Bancaria);

            popupWindowShowActionAddMov.TargetObjectsCriteria = "Status != 'Baja'";
            popupWindowShowActionAddMov.TargetObjectType = typeof(Bancaria);

            popupWindowShowActionTrnsfrr.TargetObjectsCriteria = "Status != 'Baja'";
            popupWindowShowActionTrnsfrr.TargetObjectType = typeof(Bancaria);
            popupWindowShowActionTrnsfrr.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.

            simpleActionReprtCunts.Enabled.SetItemValue("SecurityAllowance", false);
            simpleActionReprtCunts.Active.SetItemValue("Visible", false);
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void simpleActionBajCnta_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Bancaria cuenta = View.CurrentObject as Bancaria;

            if (cuenta != null)
            {
                NegocioBancos.Baja(cuenta);

                View.ObjectSpace.CommitChanges();
            }
        }

        private void simpleActionAltCnta_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Bancaria cuenta = View.CurrentObject as Bancaria;

            if (cuenta != null)
            {
                NegocioBancos.Activa(cuenta);

                View.ObjectSpace.CommitChanges();
            }
        }

        // Dic 2019, No sé si será necesario un reporte de cuentas !
        private void simpleActionReprtCunts_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            /*Obsolete
            string nameR = "Cuentas";

            ReportData donneesEtat = (from reportData in new XPQuery<ReportData>(((XPObjectSpace)View.ObjectSpace).Session)
                                      where reportData.ReportName == nameR
                                      select reportData).FirstOrDefault();

            Frame.GetController<ReportServiceController>().ShowPreview(donneesEtat, null);*/
        }

        private void popupWindowShowActionTrnsfrr_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            Transferencia newObj = objectSpace.CreateObject<Transferencia>();

            newObj.FecApli = DateTime.Today;
            if (View != null && View.CurrentObject != null)
            {
                Bancaria bc = View.CurrentObject as Bancaria;
                newObj.CtaOrigen = objectSpace.FindObject<Bancaria>
                    (new BinaryOperator("Oid", bc.Oid));
            }
            e.View = Application.CreateDetailView(objectSpace, "Transferencia_DetailView", true, newObj);
        }

        private void popupWindowShowActionTrnsfrr_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            Transferencia obj = e.PopupWindowViewCurrentObject as Transferencia;

            if (View != null)
            {
                NegocioBancos.GrabaTransfer(obj);
                View.ObjectSpace.CommitChanges();
            }
        }

        private void popupWindowShowActionAddMov_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            MovimientoB newObj = objectSpace.CreateObject<MovimientoB>();

            if (View != null && View.CurrentObject != null)
            {
                Bancaria bc = View.CurrentObject as Bancaria;
                newObj.Cuenta = objectSpace.FindObject<Bancaria>
                    (new BinaryOperator("Oid", bc.Oid));
            }
            e.View = Application.CreateDetailView(objectSpace, "MovimientoB_DetailView", true, newObj);
        }

        private void popupWindowShowActionAddMov_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            /* Ya lo hace el view de movimientoB
            MovimientoB obj = e.PopupWindowViewCurrentObject as MovimientoB;

            if (View != null)
            {
                NegocioBancos.GrabaMovimiento(obj);
                View.ObjectSpace.CommitChanges();
                
                View.Refresh();
            }*/
        }

        private void popupWindowShowActionMovsCta_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            /*
            
            MovimientoB newObj = objectSpace.CreateObject<MovimientoB>();

            if (View != null && View.CurrentObject != null)
            {
                Bancaria bc = View.CurrentObject as Bancaria;
                newObj.Cuenta = objectSpace.FindObject<Bancaria>
                    (new BinaryOperator("Oid", bc.Oid));
            }*/
            // e.View = Application.CreateDetailView(objectSpace, "MovimientoB_DetailView", true, newObj);
            Bancaria obj = View.CurrentObject as Bancaria;


            Type objectType = typeof(MovimientoB);
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            string listViewId = Application.FindListViewId /*.FindLookupListViewId*/(objectType);
            IModelListView modelListView = (IModelListView)Application.FindModelView(listViewId);
            CollectionSourceBase collectionSource = Application.CreateCollectionSource(
                objectSpace, objectType, listViewId);

            collectionSource.SetCriteria("Cuenta", $"Cuenta.Oid = '{obj.Oid}'");
            e.View = Application.CreateListView(modelListView, collectionSource, true);

            /*
            Bancaria obj = e.View.CurrentObject as Bancaria;
            
            GroupOperator fil = new GroupOperator();

            CollectionSourceBase
            fil.Operands.Add(new BinaryOperator("Cuenta", obj));
            IList col = objectSpace.CreateCollection(typeof(MovimientoB), fil);
            e.View = Application.CreateListView("MovimientoB_ListView", col, true);*/
        }
    }
}
