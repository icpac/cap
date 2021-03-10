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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCap.Module.BusinessObjects.Empresa;
using TCap.Module.Utilerias;

namespace TCap.Module.Controllers.Proyectos
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class VCEmpleadoP : ViewController
    {
        public VCEmpleadoP()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.

            TargetObjectType = typeof(EmpleadoProyecto);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.

            if (View is DetailView)
            {
                View.ObjectSpace.Committing += ObjectSpace_Committing;
            }
        }

        private void ObjectSpace_Committing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            EmpleadoProyecto obj = View.CurrentObject as EmpleadoProyecto;
            if (View.ObjectSpace.IsNewObject(obj))
            {
                Negocio.GrabaEmpleadoProyecto(obj);
            }
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.

            if (View is DetailView)
            {
                EmpleadoProyecto obj = View.CurrentObject as EmpleadoProyecto;
                if (View.ObjectSpace.IsNewObject(obj))
                {
                    Negocio.IniciaEmpleadoProyecto(obj);
                }
            }
        }
        protected override void OnDeactivated()
        {
            if (View is DetailView)
            {
                View.ObjectSpace.Committing -= ObjectSpace_Committing;
            }
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
