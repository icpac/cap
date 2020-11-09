using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using LCap.Module.BusinessObjects.Clientes;
using DevExpress.ExpressApp.Model;
using DevExpress.Data;
using DevExpress.Xpo.DB;
using Cap.Clientes.BusinessObjects.Clientes;
using LCap.Module.Utilerias;

namespace LCap.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class VCVendedor : ViewController
    {
        public VCVendedor()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(Vendedor);
            // TargetViewType = ViewType.ListView;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.

            if (View != null && View is DetailView && View.ObjectSpace != null)
                View.ObjectSpace.Committing += ObjectSpace_Committing;
            /*
            Ordenar();*/
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.

            if (View is DetailView)
            {
                if (View.ObjectSpace.IsNewObject(View.CurrentObject))
                    NegocioAdmin.IniciaVendedor(View.CurrentObject as Vendedor);
            }
        }
        protected override void OnDeactivated()
        {
            if (View != null && View is DetailView && View.ObjectSpace != null)
                View.ObjectSpace.Committing -= ObjectSpace_Committing;

            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void ObjectSpace_Committing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Vendedor obj = View.CurrentObject as Vendedor;

            if (obj != null && View.ObjectSpace.IsNewObject(obj))
            {
                NegocioAdmin.GrabaVendedor(obj);
                e.Cancel = false;
            }
        }

        /*
        void Ordenar()
        {
            string propertyName = "Clave";
            bool demoFlag = true;
            ListView lv = View as ListView;

            //Dennis: This code applies a client side sorting.
            if (demoFlag)
            {
                IModelColumn columnInfo = ((IModelList<IModelColumn>)lv.Model.Columns)[propertyName];
                if (columnInfo != null)
                {
                    columnInfo.SortIndex = 0;
                    columnInfo.SortOrder = ColumnSortOrder.Descending;
                }
            }
            else
            {
                //Dennis: This code is used for the server side sorting.
                if (((IModelList<IModelSortProperty>)lv.Model.Sorting)[propertyName] == null)
                {
                    IModelSortProperty sortProperty = lv.Model.Sorting.AddNode<IModelSortProperty>(propertyName);
                    sortProperty.Direction = SortingDirection.Descending;
                    sortProperty.PropertyName = propertyName;
                }
            }
        }*/
    }
}
