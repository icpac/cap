using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cap.Inventarios.BusinessObjects;
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
using MicroStore.Module.Utilerias;

namespace MicroStore.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class VCProducto : ViewController
    {
        public VCProducto()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.

            TargetObjectType = typeof(Producto);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.

            if (View is DetailView)
            {
                foreach (PropertyEditor editor in ((DetailView)View).GetItems<PropertyEditor>())
                {
                    // disable controls in DetailView
                    if (editor.PropertyName == "Existencia"
                        || editor.PropertyName == "CostoUltimo")
                    {
                        editor.AllowEdit.SetItemValue("AllowEdit", false);
                    }
                    else
                    {
                        editor.AllowEdit.RemoveItem("AllowEdit");
                    }
                }

                PropertyEditor pt = ((DetailView)View).GetItems<PropertyEditor>().First(p => p.PropertyName == "Tipo");

                pt.AllowEdit.SetItemValue("AllowEdit", false);
            }
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void popupWindowShowActionActPrecs_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            ProductoFiltroPrecio fil = objectSpace.FindObject<ProductoFiltroPrecio>(null);

            if (fil == null)                
                fil = objectSpace.CreateObject<ProductoFiltroPrecio>();

            e.View = Application.CreateDetailView(objectSpace, "ProductoFiltroPrecio_DetailView", true, fil);
        }

        private void popupWindowShowActionActPrecs_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            ProductoFiltroPrecio dlgFil = e.PopupWindowViewCurrentObject as ProductoFiltroPrecio;
            IObjectSpace objectSpace = Application.CreateObjectSpace();

            if (dlgFil != null)
            {
                if (dlgFil.InputFile != null)
                {
                    Negocio.ActualizaPreciosFile(dlgFil, objectSpace);
                }
                else
                { 
                    Negocio.ActualizaPrecios(dlgFil, objectSpace);
                }
            }
        }
    }
}
