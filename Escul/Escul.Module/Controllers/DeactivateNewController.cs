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
using Escul.Module.BusinessObjects;
using Cap.Generales.BusinessObjects.Empresa;
using jc;

namespace Escul.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class DeactivateNewController : ObjectViewController
    {
        public DeactivateNewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.

            NewObjectViewController newController =
                        Frame.GetController<NewObjectViewController>();

            if (View != null && View.ObjectTypeInfo != null && View.ObjectTypeInfo.Type == typeof(Alumno)
                && newController != null && !Dueno())
            {
                newController.Active["Deactivation in code"] =
                    !(View.ObjectTypeInfo.Type == typeof(Alumno) && (View is ListView || View is DetailView));
            }

            if (View != null && View.ObjectTypeInfo != null
                && View.ObjectTypeInfo.Type == typeof(Horario)
                && newController != null)
            {
                /*
                newController.Active["Deactivation in code"] =
                    !(View.ObjectTypeInfo.Type == typeof(Horario)
                    && (View is ListView || View is DetailView));*/
                newController.Active["Deactivation in code"] =
                    View.Id != "Horario_ListView" && View.Id != "Horario_DetailView";
            }
            else if (View != null && View.ObjectTypeInfo != null
                && View.ObjectTypeInfo.Type == typeof(MateriaGrp)
                && newController != null)
            {
                newController.Active["Deactivation in code"] =
                    View.Id != "MateriaGrp_ListView" && View.Id != "MateriaGrp_DetailView";
            }
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

        static private string yek = "5bhd7312";
        private bool Dueno()
        {
            // TIT Oct 2018, ahora lo dejamos libre
            bool puede = true; // SecuritySystem.CurrentUserName == "root";

            Empresa emp = View.ObjectSpace.FindObject<Empresa>(null);
            const string contra = "mdBc591/Q7UJwKzkmTEQ8w==";
            string rfc = emp != null ? emp.Compania.Rfc : "AAA010101AAA";

            string rfcaux = new SymmCrypto(SymmCrypto.SymmProvEnum.DES).
                Decrypting((emp == null || string.IsNullOrEmpty(emp.Contra)) ? contra /*"LICENCIA"*/ : emp.Contra, yek);

            string[] toks = rfcaux.Split('|');
            return puede || (toks[0] == rfc);
        }
    }
}
