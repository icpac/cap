using System;
using Cap.Ventas.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win.Editors;

namespace LCap.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class VCVentas : ViewController
    {
        public VCVentas()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(DocumenSal);
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
            DetailView dt = View as DetailView;

            if (dt != null)
            {
                StringPropertyEditor de = dt.FindItem("DocEnlace") as StringPropertyEditor;
                if (de != null && de.Control != null)
                    de.Control.Validated += Control_ValidatedDocEnl;
            }
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        void Control_ValidatedDocEnl(object sender, EventArgs e)
        {
            if (View != null)
            {
                DocumenSal fac = View.CurrentObject as DocumenSal;
                PartSal pit = null;

                if (!string.IsNullOrEmpty(fac.DocEnlace))
                {
                    string aux = fac.DocEnlace;
                    string[] arrdocs = aux.Split(':');
                    DocumenSal docOrig = null;

                    foreach (string doc in arrdocs)
                    {
                        string cve = DocumenSal.ClaveFto(doc.Substring(1));

                        if (doc[0].ToString().ToUpper() == "C")
                            docOrig = fac.Session.FindObject<Cotizacion>(new BinaryOperator("Clave", cve));
                        else if (doc[0].ToString().ToUpper() == "P")
                            docOrig = fac.Session.FindObject<Pedido>(new BinaryOperator("Clave", cve));

                        if (fac != null && docOrig != null)
                        {
                            fac.Cliente = docOrig.Cliente;
                            fac.Vnddr = docOrig.Vnddr;
                            foreach (PartSal cit in docOrig.LasPartidas())
                            {
                                if (cit.CantidadRemanente > 0)
                                {
                                    if (cit is CotizacionItem)
                                    {
                                        pit = ObjectSpace.CreateObject<PedidoItem>();
                                        pit.Antrr /*.Anterior*/ = cit as CotizacionItem;
                                    }
                                    else if (cit is PedidoItem)
                                    {
                                        pit = ObjectSpace.CreateObject<RemisionItem>();
                                        pit.Antrr /*.Anterior*/ = cit as PedidoItem;
                                    }
                                    pit.Item = cit.Item;
                                    pit.Cantidad = cit.Cantidad;
                                    pit.Producto = cit.Producto;
                                    pit.MontoUnitario = cit.MontoUnitario;
                                    pit.Costo = cit.Costo;

                                    fac.LasPartidas().Add(pit);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
