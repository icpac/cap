using System;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using SSRV.Module.BusinessObjects.Servicio;
using SSRV.Module.Utilerias;
using SSRV.Module.BusinessObjects;
using SSRV.Module.BusinessObjects.Admin;

namespace SSRV.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class VCIncidencia : ViewController
    {
        public VCIncidencia()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(Incidencia);

            simpleActionReincidencia.TargetObjectType = typeof(Incidencia);
            simpleActionReincidencia.TargetObjectsCriteria = "Status = 'OPERATIVO'";

            popupWindowShowActionIncdncRspnsbl.TargetObjectType = typeof(Incidencia);
            popupWindowShowActionIncdncRspnsbl.TargetObjectsCriteria = "Status == 'ASIGNACION' || Status == 'PENDIENTE'";
            popupWindowShowActionIncdncRspnsbl.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;

            popupWindowShowActionIncdncStart.TargetObjectType = typeof(Incidencia);
            popupWindowShowActionIncdncStart.TargetObjectsCriteria = "Status == 'ESPERA'";
            popupWindowShowActionIncdncStart.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;

            popupWindowShowActionIncdncEspr.TargetObjectType = typeof(Incidencia);
            popupWindowShowActionIncdncEspr.TargetObjectsCriteria = "Status == 'SITIO'";
            popupWindowShowActionIncdncEspr.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;

            popupWindowShowActionIncdncTrmnd.TargetObjectType = typeof(Incidencia);
            popupWindowShowActionIncdncTrmnd.TargetObjectsCriteria = "Status == 'SITIO'";
            popupWindowShowActionIncdncTrmnd.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;

            popupWindowShowActionIncdncCncld.TargetObjectType = typeof(Incidencia);
            popupWindowShowActionIncdncCncld.TargetObjectsCriteria = "Status != 'CANCELADO' && Status != 'OPERATIVO'";
            popupWindowShowActionIncdncCncld.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;

            popupWindowShowActionAgrgrBtcr.TargetObjectType = typeof(Incidencia);
            popupWindowShowActionAgrgrBtcr.TargetObjectsCriteria = "Status != 'OPERATIVO' && Status != 'CANCELADO'";

            simpleActionCnclr.TargetObjectType = typeof(Incidencia);
            simpleActionCnclr.TargetObjectsCriteria = "Status != 'CANCELADO'";
            simpleActionCnclr.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.

            if (View != null && View is DetailView)
            {
                if (View.ObjectSpace != null)
                    View.ObjectSpace.Committing += ObjectSpace_Committing;
            }

            // No sé por el momento si será así
            // Ago 19
            simpleActionReincidencia.Active.SetItemValue("Visible", false);
            popupWindowShowActionIncdncRspnsbl.Active.SetItemValue("Visible", false);
            popupWindowShowActionIncdncStart.Active.SetItemValue("Visible", false);
            popupWindowShowActionIncdncTrmnd.Active.SetItemValue("Visible", false);
            popupWindowShowActionIncdncEspr.Active.SetItemValue("Visible", false);
            popupWindowShowActionIncdncCncld.Active.SetItemValue("Visible", false);
            popupWindowShowActionAgrgrBtcr.Active.SetItemValue("Visible", false);
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

        void ObjectSpace_Committing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (View != null)
            {
                Incidencia obj = View.CurrentObject as Incidencia;

                if (obj != null)
                {
                    if (View.ObjectSpace.IsNewObject(obj))
                    {
                        NegocioSrvc.GrabaNewServicio(obj);
                        e.Cancel = false;
                    }
                    else
                    {
                        NegocioSrvc.GrabaServicio(obj);
                        e.Cancel = false;
                    }
                }
            }
        }

        private void simpleActionReincidencia_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Incidencia srv = View.CurrentObject as Incidencia;

            // Create IObjectSpace or use the existing one, e.g. View.ObjectSpace, if it is suitable for your scenario.
            IObjectSpace os = Application.CreateObjectSpace();

            //Find an existing object.
            //Contact obj = os.FindObject<Contact>(CriteriaOperator.Parse("FirstName=?", "My Contact"));
            //Or create a new object.
            Incidencia obj = os.CreateObject<Incidencia>();

            if (srv != null)
            {
                obj.Fll = srv.Fll;
                obj.Antrr = obj.Session.FindObject<Incidencia>(new BinaryOperator("Oid", srv.Oid));
            }

            //Save the changes if necessary.
            //os.CommitChanges();

            //Specify the IsRoot parameter if necessary.
            DetailView dv = Application.CreateDetailView(os, obj);
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            e.ShowViewParameters.CreatedView = dv;

            //Specify various display settings if necessary
            //e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            //e.ShowViewParameters.Context = TemplateContext.PopupWindow;
            e.ShowViewParameters.CreateAllControllers = true;
            //You can pass custom Controllers for intercommunication or to provide a standard functionality).
            //DialogController dc = Application.CreateController<DialogController>();
            //e.ShowViewParameters.Controllers.Add(dc);
        }

        private void VCIncidencia_ViewControlsCreated(object sender, EventArgs e)
        {
            if (View != null)
            {
                Incidencia obj = View.CurrentObject as Incidencia;

                if (View.ObjectSpace != null && View.ObjectSpace.IsNewObject(obj))
                    NegocioSrvc.IniciaSrvc(obj);
            }
        }

        private void popupWindowShowActionIncdncHrr_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            ResponsableAtencion newObj;

            newObj = objectSpace.FindObject<ResponsableAtencion>(null);
            if (newObj != null)
                objectSpace.Delete(newObj);
            /*
            if (newObj == null)*/
                newObj = objectSpace.CreateObject<ResponsableAtencion>();
            e.View = Application.CreateDetailView(objectSpace, "ResponsableAtencion_DetailView", true, newObj);
        }

        private void popupWindowShowActionIncdncHrr_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            ResponsableAtencion ha = e.PopupWindowViewCurrentObject as ResponsableAtencion;

            if (View != null)
            {
                IObjectSpace os = View.ObjectSpace;
                Incidencia obj = View.CurrentObject as Incidencia;

                obj.FchCt = ha.FchCt;
                obj.TmCt = ha.TmCt;
                obj.Rspnsbl = os.FindObject<Empleado>(new BinaryOperator("Oid", ha.Rspnsbl.Oid));
                obj.Status = StsSrvc.ESPERA;

                os.CommitChanges();
            }
        }

        private void popupWindowShowActionIncdncStart_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            InicioAtencion newObj;

            newObj = objectSpace.FindObject<InicioAtencion>(null);
            if (newObj != null)
                objectSpace.Delete(newObj);

            /*
            if (newObj == null)*/
                newObj = objectSpace.CreateObject<InicioAtencion>();
            /*
            else
            {
                newObj.Fch = DateTime.Today;
                newObj.Tm = DateTime.Now;
            }*/
            e.View = Application.CreateDetailView(objectSpace, "InicioAtencion_DetailView", true, newObj);
        }

        private void popupWindowShowActionIncdncStart_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            InicioAtencion ia = e.PopupWindowViewCurrentObject as InicioAtencion;

            if (View != null)
            {
                IObjectSpace os = View.ObjectSpace;
                Incidencia obj = View.CurrentObject as Incidencia;

                obj.Status = StsSrvc.SITIO;

                Bitacora btcr = os.CreateObject<Bitacora>();

                btcr.FchIni = ia.Fch;
                btcr.TimeIni = ia.Tm;
                btcr.Observ = ia.Nts;
                btcr.Stts = EBITCORATP.Inicio;

                obj.Partidas.Add(btcr);
                os.CommitChanges();
            }
        }

        private void popupWindowShowActionIncdncEspr_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            PendienteAtencion newObj;

            newObj = objectSpace.FindObject<PendienteAtencion>(null);
            if (newObj != null)
                objectSpace.Delete(newObj);

            newObj = objectSpace.CreateObject<PendienteAtencion>();
            e.View = Application.CreateDetailView(objectSpace, "PendienteAtencion_DetailView", true, newObj);
        }

        private void popupWindowShowActionIncdncEspr_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            PendienteAtencion ia = e.PopupWindowViewCurrentObject as PendienteAtencion;

            if (View != null)
            {
                IObjectSpace os = View.ObjectSpace;
                Incidencia obj = View.CurrentObject as Incidencia;

                obj.Status = StsSrvc.PENDIENTE;

                Bitacora btcr = os.CreateObject<Bitacora>();

                btcr.FchIni = ia.Fch;
                btcr.TimeIni = ia.Tm;
                btcr.Observ = ia.Nts;
                btcr.Stts = EBITCORATP.Proceso;

                obj.Partidas.Add(btcr);
                os.CommitChanges();
            }
        }

        private void popupWindowShowActionIncdncTrmnd_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            SolucionAtencion newObj;

            newObj = objectSpace.FindObject<SolucionAtencion>(null);
            if (newObj != null)
                objectSpace.Delete(newObj);

            newObj = objectSpace.CreateObject<SolucionAtencion>();
            e.View = Application.CreateDetailView(objectSpace, "SolucionAtencion_DetailView", true, newObj);
        }

        private void popupWindowShowActionIncdncTrmnd_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            SolucionAtencion ia = e.PopupWindowViewCurrentObject as SolucionAtencion;

            if (View != null)
            {
                IObjectSpace os = View.ObjectSpace;
                Incidencia obj = View.CurrentObject as Incidencia;

                obj.Status = StsSrvc.OPERATIVO;

                Bitacora btcr = os.CreateObject<Bitacora>();

                btcr.FchFin = ia.Fch;
                btcr.TimeFin = ia.Tm;
                btcr.Dscrpcn = ia.Dscrpcn;
                btcr.Atnd = ia.Atnd;
                btcr.Observ = ia.Nts;
                btcr.Stts = EBITCORATP.Terminacion;

                obj.Partidas.Add(btcr);
                os.CommitChanges();
            }
        }

        private void popupWindowShowActionIncdncCncld_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            CancelaAtencion newObj;

            newObj = objectSpace.FindObject<CancelaAtencion>(null);
            if (newObj != null)
                objectSpace.Delete(newObj);

            newObj = objectSpace.CreateObject<CancelaAtencion>();
            e.View = Application.CreateDetailView(objectSpace, "CancelaAtencion_DetailView", true, newObj);
        }

        private void popupWindowShowActionIncdncCncld_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            CancelaAtencion ca = e.PopupWindowViewCurrentObject as CancelaAtencion;

            if (View != null)
            {
                IObjectSpace os = View.ObjectSpace;
                Incidencia obj = View.CurrentObject as Incidencia;

                obj.Status = StsSrvc.CANCELADO;

                Bitacora btcr = os.CreateObject<Bitacora>();

                btcr.FchFin = ca.Fch;
                btcr.Observ = ca.Nts;
                btcr.Stts = EBITCORATP.Cancelacion;

                obj.Partidas.Add(btcr);
                os.CommitChanges();
            }
        }

        private void popupWindowShowActionAgrgrBtcr_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            AgregarBitacora newObj;

            newObj = objectSpace.FindObject<AgregarBitacora>(null);
            if (newObj != null)
                objectSpace.Delete(newObj);

            newObj = objectSpace.CreateObject<AgregarBitacora>();
            e.View = Application.CreateDetailView(objectSpace, "AgregarBitacora_DetailView", true, newObj);
        }

        private void popupWindowShowActionAgrgrBtcr_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            AgregarBitacora ca = e.PopupWindowViewCurrentObject as AgregarBitacora;

            if (View != null)
            {
                IObjectSpace os = View.ObjectSpace;
                Incidencia obj = View.CurrentObject as Incidencia;


                Bitacora btcr = os.CreateObject<Bitacora>();

                btcr.FchFin = ca.Fch;
                btcr.TimeFin = ca.Tm;
                btcr.Dscrpcn = ca.Dscrpcn;
                btcr.Stts = EBITCORATP.Proceso;

                obj.Partidas.Add(btcr);
                os.CommitChanges();
            }
        }

        private void simpleActionCnclr_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Incidencia obj = View.CurrentObject as Incidencia;

            if (obj != null)
            {
                NegocioSrvc.CancelaIncidencia(obj);
                View.ObjectSpace.CommitChanges();
            }
        }
    }
}
