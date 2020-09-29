using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using Cap.Inventarios.BusinessObjects;
using MicroStore.Module.Utilerias;

namespace MicroStore.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class VCInventarioFisicoImportar : ViewController
    {
        public VCInventarioFisicoImportar()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(InventarioFisico);
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


        private void popupWindowShowActionImprtrFl_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            InvFisImporta fil = objectSpace.FindObject<InvFisImporta>(null);

            if (fil == null)
                fil = objectSpace.CreateObject<InvFisImporta>();

            e.View = Application.CreateDetailView(objectSpace, "InvFisImporta_DetailView", true, fil);
        }

        /*
        private void popupWindowShowActionImprtrFl_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            e.View = Application.CreateDetailView(this.Application.CreateObjectSpace(), newFileInputParameter());
            ((DetailView)e.View).ViewEditMode = ViewEditMode.Edit;
        }

        private FileInputParameter newFileInputParameter()
        {
            return View.ObjectSpace.CreateObject<FileInputParameter>();
        }*/

        private void popupWindowShowActionImprtrFl_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            InvFisImporta parameter = (InvFisImporta)e.PopupWindowViewCurrentObject;

            Negocio.ImportaInvFis(parameter, Application.CreateObjectSpace());
        }
    }
}
