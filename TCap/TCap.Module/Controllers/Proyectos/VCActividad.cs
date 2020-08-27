using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using TCap.Module.BusinessObjects.Proyectos;

namespace TCap.Module.Controllers.Proyectos
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class VCActividad : ViewController
    {
        public VCActividad()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.

            TargetObjectType = typeof(Actividad);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.

            popupWindowShowActionActvddCmpltd.TargetObjectType = (typeof(Actividad));
            popupWindowShowActionActvddCmpltd.TargetObjectsCriteria = "Stts = 'Ejecución'";

            simpleActionActvddEjccn.TargetObjectType = typeof(Actividad);
            simpleActionActvddEjccn.TargetObjectsCriteria = "Stts = 'Espera'";

            simpleActionActvddSspndr.TargetObjectType = typeof(Actividad);
            simpleActionActvddSspndr.TargetObjectsCriteria = "Stts = 'Espera' || Stts = 'Ejecución'";

            popupWindowShowActionActvddSspndr.TargetObjectType = typeof(Actividad);
            popupWindowShowActionActvddSspndr.TargetObjectsCriteria = "Stts = 'Espera' || Stts = 'Ejecución'";
            popupWindowShowActionActvddSspndr.ConfirmationMessage = null;

            simpleActionActvddSspndr.Active.SetItemValue("Visible", false);
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

        private void popupWindowShowActionActvddCmpltd_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            ActividadCompletada newObj;

            newObj = objectSpace.FindObject<ActividadCompletada>(null);
            if (newObj == null)
                newObj = objectSpace.CreateObject<ActividadCompletada>();
            newObj.FchTrmncn = DateTime.Now;
            e.View = Application.CreateDetailView(objectSpace, "ActividadCompletada_DetailView", true, newObj);
        }

        private void popupWindowShowActionActvddCmpltd_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            ActividadCompletada obj = e.PopupWindowViewCurrentObject as ActividadCompletada;

            if (View != null)
            {
                IObjectSpace os = View.ObjectSpace;
                Actividad act = View.CurrentObject as Actividad;

                act.FchFn = obj.FchTrmncn;
                act.Stts = obj.Stts;
                act.AlarmTime = null;

                Incidencia inc = os.CreateObject<Incidencia>();

                inc.Dscrpcn = obj.Dscrpcn;
                inc.Hrs = obj.Hrs;
                inc.Fch = obj.FchTrmncn;

                act.Incidencias.Add(inc);
                os.CommitChanges();
            }
        }

        private void simpleActionActvddEjccn_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View != null)
            {
                Actividad obj = View.CurrentObject as Actividad;

                obj.FchInc = DateTime.Today;
                obj.Stts = TASKSTATUS.Ejecución;

                View.ObjectSpace.CommitChanges();
            }
        }

        private void simpleActionActvddSspndr_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View != null && View.CurrentObject != null)
            {
                Actividad act = View.CurrentObject as Actividad;

                act.Stts = TASKSTATUS.Suspendida;

                View.ObjectSpace.CommitChanges();
            }
        }

        private void popupWindowShowActionActvddSspndr_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            ActividadSuspendida newObj = objectSpace.CreateObject<ActividadSuspendida>();
            e.View = Application.CreateDetailView(objectSpace, "ActividadSuspendida_DetailView", true, newObj);
        }

        private void popupWindowShowActionActvddSspndr_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            ActividadSuspendida obj = e.PopupWindowViewCurrentObject as ActividadSuspendida;

            if (View != null)
            {
                IObjectSpace os = View.ObjectSpace;
                Actividad act = View.CurrentObject as Actividad;

                act.FchFn = obj.Fch;
                act.Stts = obj.Stts;

                Incidencia inc = os.CreateObject<Incidencia>();

                inc.Dscrpcn = obj.Nts;
                // inc.Hrs = obj.Hrs;
                inc.Fch = obj.Fch;

                act.Incidencias.Add(inc);
                os.CommitChanges();
            }
        }
    }
}
