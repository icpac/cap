using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.DC;
using Cap.Generales.BusinessObjects.General;
using HCap.Module.BusinessObjects.Hospital;
using Cap.Bancos.BusinessObjects;

namespace Hospital.Module.Controllers
{
    // For more information on Controllers and their life cycle, check out the http://documentation.devexpress.com/#Xaf/CustomDocument2621 and http://documentation.devexpress.com/#Xaf/CustomDocument3118 help articles.
    public partial class WCSingleton : WindowController
    {
        // Use this to do something when a Controller is instantiated (do not execute heavy operations here!).
        public WCSingleton()
        {
            InitializeComponent();
            RegisterActions(components);
            // For instance, you can specify activation conditions of a Controller or create its Actions.
            //TargetWindowType = WindowType.Main;
            //SimpleAction myAction = new SimpleAction(this, "MyActionId", DevExpress.Persistent.Base.PredefinedCategory.RecordEdit);
            TargetWindowType = WindowType.Main;
        }
        // Override to do something before Controllers are activated within the current Frame (their Window property is not yet assigned).
        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();
            //For instance, you can access another Controller via the Frame.GetController<AnotherControllerType>() method to customize it or subscribe to its events.
        }
        // Override to do somethig when a Controller is activated and its Window is assigned.
        protected override void OnActivated()
        {
            base.OnActivated();
            //For instance, you can customize the current Window and its Action Containers (http://documentation.devexpress.com/#Xaf/CustomDocument2617) or manage the Controller's Actions visibility and availability (http://documentation.devexpress.com/#Xaf/CustomDocument2728).
            showNavigationItemController = Frame.GetController<ShowNavigationItemController>();
            if (showNavigationItemController != null)
                showNavigationItemController.CustomShowNavigationItem += showNavigationItemController_CustomShowNavigationItem;
        }
        // Override to do something when a Controller is deactivated.
        protected override void OnDeactivated()
        {
            showNavigationItemController.CustomShowNavigationItem -= showNavigationItemController_CustomShowNavigationItem;
            // For instance, you can unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }


        void showNavigationItemController_CustomShowNavigationItem(object sender, CustomShowNavigationItemEventArgs e)
        {
            if (e.ActionArguments.SelectedChoiceActionItem.Id == "AddNtMdc")
            {
                IObjectSpace objectSpace = Application.CreateObjectSpace();
                NotaMedica newDoc = objectSpace.CreateObject<NotaMedica>();
                DetailView dv = Application.CreateDetailView(objectSpace, "NotaMedica_DetailView", true, newDoc);
                dv.ViewEditMode = ViewEditMode.Edit;
                e.ActionArguments.ShowViewParameters.CreatedView = dv;
                e.Handled = true;
            }
            else if (e.ActionArguments.SelectedChoiceActionItem.Id == "AddMvmnt")
            {
                IObjectSpace objectSpace = Application.CreateObjectSpace();
                MovimientoB newDoc = objectSpace.CreateObject<MovimientoB>();
                DetailView dv = Application.CreateDetailView(objectSpace, "MovimientoB_DetailView", true, newDoc);
                dv.ViewEditMode = ViewEditMode.Edit;
                e.ActionArguments.ShowViewParameters.CreatedView = dv;
                e.Handled = true;
            }
            else if (e.ActionArguments.SelectedChoiceActionItem != null)
            {
                IModelView modelView = Application.FindModelView(e.ActionArguments.SelectedChoiceActionItem.Id);

                if (modelView is IModelObjectView)
                {
                    IObjectSpace os = Application.CreateObjectSpace();
                    ITypeInfo typeInfo = ((IModelObjectView)modelView).ModelClass.TypeInfo;

                    if (typeInfo.Implements<ISingleton>())
                    {
                        object obj = SingletonFactory.GetSingletonInstance(os, typeInfo);
                        DetailView view = base.Application.CreateDetailView(os, obj, true);
                        e.ActionArguments.ShowViewParameters.CreatedView = view;
                        e.ActionArguments.ShowViewParameters.TargetWindow = TargetWindow.Current;
                        e.Handled = true;
                    }
                }
            }
        }

        ShowNavigationItemController showNavigationItemController;
    }
}
