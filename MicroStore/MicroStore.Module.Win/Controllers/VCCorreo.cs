using System;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Win.Editors;
using MicroStore.Module.Win.Editors;
using DevExpress.XtraEditors;
using Cap.Generales.BusinessObjects.General;

namespace MicroStore.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class VCCorreo : ViewController
    {
        public VCCorreo()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(Correo);
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

        private void VCCorreo_ViewControlsCreated(object sender, EventArgs e)
        {
            DetailView dv = View as DetailView;

            if (dv != null)
            {
                List<string> mails = new List<string>();
                mails.Clear();
                mails.Add("smtp.gmail.com");
                mails.Add("smtp.live.com");
                mails.Add("smtp.prodigy.net.mx");
                mails.Add("smtp.mail.yahoo.com");

                CustomStringEditor clE = dv.FindItem("ServidorSMTP") as CustomStringEditor;

                if (clE != null)
                {
                    clE.Control.Validated += Control_Validated;
                    (clE.Control as ComboBoxEdit).Properties.Items.Clear();
                    for (int i = 0; i < mails.Count; i++)
                    {                        
                        (clE.Control as ComboBoxEdit).Properties.Items.Add(mails[i]);
                    }
                }

                foreach (ViewItem w in dv.Items)
                {
                    DXPropertyEditor ww = w as DXPropertyEditor;
                    if (ww != null && ww.Control != null)
                        ww.Control.EnterMoveNextControl = true;
                }
            }
        }

        void Control_Validated(object sender, EventArgs e)
        {
            if (View != null)
            {
                Correo mail = View.CurrentObject as Correo;
                if (string.IsNullOrEmpty(mail.Passw))
                {
                    if (mail.ServidorSMTP == "smtp.gmail.com")
                    {
                        mail.Puerto = "587";
                        mail.SegurdSSL = true;
                        mail.Cuenta = "@gmail.com";
                        mail.Usuario = string.Empty;
                    }
                    else if (mail.ServidorSMTP == "smtp.live.com")
                    {
                        mail.Puerto = "587"; //"25";
                        mail.SegurdSSL = true;
                        mail.Cuenta = "@hotmail.com";
                        mail.Usuario = "@hotmail.com";
                    }
                    else if (mail.ServidorSMTP == "smtp.prodigy.net.mx")
                    {
                        mail.Puerto = "587";
                        mail.SegurdSSL = false;
                        mail.Cuenta = "@prodigy.net.mx";
                        mail.Usuario = string.Empty;
                    }
                    else if (mail.ServidorSMTP == "smtp.mail.yahoo.com")
                    {
                        mail.Puerto = "587";
                        mail.SegurdSSL = true;
                        mail.Usuario = "@yahoo.com.mx";
                        mail.Cuenta = "@yahoo.com.mx";
                    }

                }
            }
        }
    }
}
