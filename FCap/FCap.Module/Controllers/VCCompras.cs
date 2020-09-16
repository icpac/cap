using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Reports;
using Cap.Compras.BusinessObjects;
using System.IO;
using Cap.Ventas.BusinessObjects;
using FCap.Module.Utilerias;
using Cap.Generales.BusinessObjects.Empresa;
using FCap.Module.BusinessObjects.Ventas;

namespace FCap.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class VCCompras : ViewController
    {
        public VCCompras()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.

            TargetObjectType = typeof(Recepcion);
            TargetViewType = ViewType.ListView;

            simpleActionRprtCmprs.TargetObjectType = typeof(Recepcion);
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

        private void simpleActionRprtCmprs_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            string nameR = string.Empty;

            nameR = "ResumenCompras";

            ReportData donneesEtat = (from reportData in new XPQuery<ReportData>(((XPObjectSpace)View.ObjectSpace).Session)
                                      where reportData.ReportName == nameR
                                      select reportData).FirstOrDefault();

            Frame.GetController<ReportServiceController>().ShowPreview(donneesEtat, null);
        }

        private void popupWindowShowActionLdXml_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            CargaRecepcion newObj;

            newObj = objectSpace.FindObject<CargaRecepcion>(null);

            if (newObj == null)
            {
                newObj = objectSpace.CreateObject<CargaRecepcion>();
            }
            newObj.Rt = Prms.RutaPdfVnts;

            e.View = Application.CreateDetailView(objectSpace, "CargaRecepcion_DetailView", true, newObj);
        }

        private void popupWindowShowActionLdXml_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //http://icpac.blogspot.com/
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            CargaRecepcion obj = e.PopupWindowViewCurrentObject as CargaRecepcion;

            Empresa emprs = View.ObjectSpace.FindObject<Empresa>(null);
            Ventas prms = objectSpace.FindObject<Ventas>(null);

            string aux = Path.Combine(obj.Rt, "Extract");
            string[] dirs = Directory.GetFiles(aux, obj.Mtdt ? "*.txt" : "*.xml");

            foreach (string dir in dirs)
            {
                if (obj.Mtdt)
                    NegocioAdmin.CargaMetaData(obj, "recepcion", objectSpace);
                else
                    NegocioAdmin.CargaDeArchivo(dir, emprs, prms, obj, objectSpace);

                /*
                List<string> dts = NegocioAdmin.GetInfXml(dir);
                bool addItms;    // Para ver si agrego los items o no

                // Si el xml pertenece al Rfc de la bd
                if (dts.Count > 2 && dts[2] == emprs.Compania.Rfc)
                {
                    // Hay que ver si el uuid está dado de alta ya.
                    rcpcn = objectSpace.FindObject<Recepcion>
                        (new BinaryOperator("Uuid", dts[0]));

                    addItms = false;
                    if (rcpcn == null)
                    {
                        addItms = true;
                        rcpcn = objectSpace.CreateObject<Recepcion>();
                    }

                    NegocioAdmin.ObtenDelXml(rcpcn, emprs, Prms, dir, addItms);
                    if (rcpcn.Proveedor != null)
                    {
                        if (addItms)
                            NegocioAdmin.GrabaDocs(rcpcn, Prms);
                        objectSpace.CommitChanges();

                        try
                        {
                            //string auxX = $"{rcpcn.FechaDoc.Year}{rcpcn.FechaDoc.Month:d2}{rcpcn.FechaDoc.Day:d2}{Path.GetFileName(dir)}";
                            string auxX = Path.Combine(obj.Rt, "Recepcion");
                            auxX = NegocioAdmin.CreaDirs(auxX, rcpcn.FechaDoc, true, Cap.Generales.Utilerias.ENivelDir.Mes);


                            NegocioAdmin.Mueve(Path.GetDirectoryName(dir), / *Path.Combine(obj.Rt, "Recepcion")* /
                auxX,
                                Path.GetFileName(dir), Path.GetFileName(dir)/ *auxX* /);
                        }
                        catch(Exception ex)
                        {
                            Tracing.Tracer.LogText($"Compras: Obten del Xml {ex.Message}");
                        }

                        rcpcn = null;
                    }
                }*/
            }
        }

        private VentaCfdi mPrms;
        private VentaCfdi Prms
        {
            get
            {
                if (mPrms == null)
                {
                    if (View != null && View.ObjectSpace != null)
                        mPrms = View.ObjectSpace.FindObject<VentaCfdi>(null);
                }
                return mPrms;
            }
        }
    }
}
