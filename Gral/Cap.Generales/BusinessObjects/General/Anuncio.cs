using DevExpress.Xpo;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using Cap.Generales.BusinessObjects.Object;

namespace Cap.Generales.BusinessObjects.General
{
    [NavigationItem("Clientes")]
    [ImageName("Preferred_Customers")]
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Anuncio : PObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Anuncio(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue("PersistentProperty", ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}



        private string mMensaje;
        // https://docs.devexpress.com/eXpressAppFramework/400004/concepts/extra-modules/office-module/use-rich-text-documents-in-business-objects
        [EditorAlias("RTF")]
        [Size(SizeAttribute.Unlimited)]
        public string Mensaje
        {
            get { return mMensaje; }
            set { SetPropertyValue("Mensaje", ref mMensaje, value); }
        }


        private string mDescrip;
        [RuleRequiredField("RuleRequiredField for Anuncio.Descrip", DefaultContexts.Save, "Debe capturar una Descripción", SkipNullOrEmptyValues = false)]
        [XafDisplayName("Descripción")]
        [Size(20)]
        public string Descrip
        {
            get { return mDescrip; }
            set { SetPropertyValue("Descrip", ref mDescrip, value); }
        }

        private EnvioCorreo mEnvioC;
        [XafDisplayName("Status de Envío")]
        public EnvioCorreo EnvioC
        {
            get { return mEnvioC; }
            set { SetPropertyValue("EnvioC", ref mEnvioC, value); }
        }
    }
}