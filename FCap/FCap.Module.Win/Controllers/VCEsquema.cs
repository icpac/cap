using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;
using FCap.Module.BusinessObjects;
using DevExpress.XtraPrinting.Export;
using DevExpress.XtraLayout;
using FCap.Module.BusinessObjects.Ventas;
using Cap.Inventarios.BusinessObjects;

namespace FCap.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class VCEsquema : ViewController
    {
        public VCEsquema()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(EsquemaImpuesto);
            TargetViewType = ViewType.DetailView;
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
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void VCEsquema_ViewControlsCreated(object sender, EventArgs e)
        {
            List<string> cmps = new List<string>();
            List<string> txts = new List<string>();

            cmps.Add("Impuesto1");
            cmps.Add("Impuesto2"); 
            cmps.Add("Impuesto3"); 
            cmps.Add("Impuesto4");

            txts.Add("Impuesto1");
            txts.Add("Impuesto2");
            txts.Add("Impuesto3");
            txts.Add("Impuesto4");

            Ventas param = ObjectSpace.FindObject<Ventas>(null);
            if (!string.IsNullOrEmpty(param.VntCfdi.LeyImpst1))
                txts[0] = param.VntCfdi.LeyImpst1;
            if (!string.IsNullOrEmpty(param.VntCfdi.LeyImpst2))
                txts[1] = param.VntCfdi.LeyImpst2;
            if (!string.IsNullOrEmpty(param.VntCfdi.LeyImpst3))
                txts[2] = param.VntCfdi.LeyImpst3;
            if (!string.IsNullOrEmpty(param.VntCfdi.LeyImpst4))
                txts[3] = param.VntCfdi.LeyImpst4;

            foreach (string cmp in cmps)
            {
                PropertyEditor propertyEditor = (PropertyEditor)((DetailView)View).FindItem(cmp);
                if (propertyEditor != null)
                {
                    int hashCode = propertyEditor.Control.GetHashCode();
                    foreach (BaseLayoutItem item in ((DevExpress.XtraLayout.LayoutControl)View.Control).Items)
                    {
                        if (item is LayoutControlItem && ((LayoutControlItem)item).Control != null)
                        {
                            if (((LayoutControlItem)item).Control.GetHashCode() == hashCode)
                            {
                                if (cmp == cmps[0])
                                {
                                    item.Text = txts[0];
                                }
                                else if (cmp == cmps[1])
                                {
                                    item.Text = txts[1];
                                }
                                else if (cmp == cmps[2])
                                {
                                    item.Text = txts[2];
                                }
                                else
                                {
                                    item.Text = txts[3];
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
