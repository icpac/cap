using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using MicroStore.Module.BusinessObjects;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.ExpressApp.Win.Layout;
using DevExpress.XtraLayout;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Utils;

namespace MicroStore.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class VCVentaABC : ViewController
    {
        WinLayoutManager lm;
        public VCVentaABC()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.

            TargetObjectType = typeof(Venta);
            TargetViewType = ViewType.DetailView;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            lm = (View as DetailView).LayoutManager as WinLayoutManager;

            lm.ItemCreated += lm_ItemCreated;
            
            ((CompositeView)View).ItemsChanged += WinNullTextEditorController_ItemsChanged;
            TryInitializeAnniversaryItem();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            ((CompositeView)View).ItemsChanged -= WinNullTextEditorController_ItemsChanged;
            lm.ItemCreated -= lm_ItemCreated;
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void VCVentaABC_ViewControlsCreated(object sender, EventArgs e)
        {
            DetailView dv = View as DetailView;

            if (dv != null)
            {
                foreach (ViewItem w in dv.Items)
                {
                    DXPropertyEditor ww = w as DXPropertyEditor;
                    if (ww != null && ww.Control != null)
                        ww.Control.EnterMoveNextControl = true;
                }
            }

            StringPropertyEditor prd = dv.FindItem("PrdctCptr") as StringPropertyEditor;
            if (prd != null)
            {
                prd.Control.Validated += Control_Validated;
                prd.Control.KeyUp += Control_KeyUp;
            }

            // Primero lo primero con el ratón
            LookupPropertyEditor prdl = dv.FindItem("Prdct") as LookupPropertyEditor;
            if (prdl != null)
            {
                prdl.Control.KeyUp += Control_KeyUp1;
                prdl.Control.KeyPress += Control_KeyPress;
                prdl.Control.KeyDown += Control_KeyDown;
                prdl.Control.Validating += Control_Validating;
                prdl.Control.Validated += Control_Validated1;
                prdl.Control.CloseUp += Control_CloseUp;
                prdl.Control.Closed += Control_Closed;
            }                
        }

        private void Control_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            Venta doc = View.CurrentObject as Venta;

            // Agrega item
            doc.AddItem();
        }

        private void Control_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            /*
            Venta doc = View.CurrentObject as Venta;

            doc.AddItem();*/
        }

        private void Control_Validated1(object sender, EventArgs e)
        {
            /*
            Venta doc = View.CurrentObject as Venta;

            doc.AddItem();*/
        }

        private void Control_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            /*
                Venta doc = View.CurrentObject as Venta;

                doc.AddItem();*/
        }

        private void Control_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            /*
            if (e.Control / *&& e.KeyCode == System.Windows.Forms.Keys.Shift* /)
            {
                Venta doc = View.CurrentObject as Venta;

                doc.AddItem();

                e.Handled = true;
            }*/
        }

        private void Control_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            /*
            if (e.KeyChar == 13)
            {
                Venta doc = View.CurrentObject as Venta;

                doc.AddItem();
            }*/
        }

        private void Control_KeyUp1(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            /*
            if (e.Control && e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                Venta doc = View.CurrentObject as Venta;

                doc.AddItem();
            }*/
        }

        private void Control_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            /*
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                Venta doc = View.CurrentObject as Venta;

                doc.AddItem();
            }*/
        }

        void Control_Validated(object sender, EventArgs e)
        {
            DetailView dv = View as DetailView;

            if (dv != null)
            {
                StringPropertyEditor prd = dv.FindItem("PrdctCptr") as StringPropertyEditor;

                if (prd != null && !string.IsNullOrEmpty(prd.ControlValue.ToString()))
                {
                    prd.Refresh();
                    if (string.IsNullOrEmpty(prd.ControlValue.ToString()))
                        prd.Control.Focus();
                }
            }
        }

        public void lm_ItemCreated(Object sender, ItemCreatedEventArgs e)
        {
            try
            {
                LayoutControlItem item = e.Item as LayoutControlItem;

                if (item == null)
                    return;

                // 'TODO: Adjust font to show MICR  
                if (item.CustomizationFormText.ToLower().Contains("total"))
                {
                    //'This failed since the Item.Control is Nothing  
                    //'item.Control.Font = New System.Drawing.Font("NorMICR", 12)  

                    item.PaintAppearanceItemCaption.Font = new System.Drawing.Font("NorMICR", 16);
                    item.AppearanceItemCaption.Font = new System.Drawing.Font("NorMICR", 16);
                }
            }
            catch (Exception /*ex*/)
            {
                throw new ArgumentException("Font Problem");
            }
        }

        private void WinNullTextEditorController_ItemsChanged(Object sender, ViewItemsChangedEventArgs e)
        {
            if (e.ChangedType == ViewItemsChangedType.Added && e.Item.Id == "Ttl")
            {
                TryInitializeAnniversaryItem();
            }
        }
        public void TryInitializeAnniversaryItem()
        {
            List<string> cmps = new List<string>();
            
            cmps.Add("Ttl");
            cmps.Add("Pg");
            cmps.Add("Cmb");

            foreach (string it in cmps)
            {
                PropertyEditor propertyEditor = ((DetailView)View).FindItem(it) as PropertyEditor;
                if (propertyEditor != null)
                {
                    if (propertyEditor.Control != null)
                    {
                        InitNullText(propertyEditor);
                    }
                    else
                    {
                        propertyEditor.ControlCreated += propertyEditor_ControlCreated;
                    }
                }
            }
        }
        private void InitNullText(PropertyEditor propertyEditor)
        {
            // ((BaseEdit)propertyEditor.Control).Properties.NullText = CaptionHelper.NullValueText;
            ((BaseEdit)propertyEditor.Control).Font = new System.Drawing.Font("NorMICR", 14);
        }
        private void propertyEditor_ControlCreated(Object sender, EventArgs e)
        {
            InitNullText((PropertyEditor)sender);
        }
    }
}
