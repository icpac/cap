using DevExpress.Xpo;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using Cap.Generales.BusinessObjects.Object;
using Cap.Generales.BusinessObjects.General;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Drawing;

namespace Cap.Inventarios.BusinessObjects
{
    [Appearance("ProductoFiltroPrecio.InputFile", Context = "DetailView",
        Enabled = false, FontStyle = FontStyle.Italic, Criteria = "InputFile == null",
        TargetItems = "PscnClv, PscnDscrpcn, PscnPrcP")]
    [Appearance("ProductoFiltroPrecio._InputFile", Context = "DetailView",
        Enabled = false, FontStyle = FontStyle.Italic, Criteria = "InputFile != null",
        TargetItems = "PrcntjIncrmnt, Mrc")]
    //[DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class ProductoFiltroPrecio : PObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public ProductoFiltroPrecio(Session session)
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


        [XafDisplayName("Archivo a Cargar")]
        [FileTypeFilter("Excel", 1, "*.xlsx")]
        public MyFileData InputFile { get; set; }

        private float mPrcntjIncrmnt;
        [XafDisplayName("Porcentaje Incremento")]
        [ModelDefault("DisplayFormat", "{0:n2}%")]
        [ModelDefault("EditMask", "P2"), ModelDefault("EditMaskType", "Default")]
        public float PrcntjIncrmnt
        {
            get { return mPrcntjIncrmnt; }
            set { SetPropertyValue("PrcntjIncrmnt", ref mPrcntjIncrmnt, value); }
        }

        [XafDisplayName("Marca")]
        public Marca Mrc
        { get; set; }


        [XafDisplayName("Clave")]
        public string PscnClv { get; set; }
        
        [XafDisplayName("Descripción")]
        public string PscnDscrpcn { get; set; }

        [XafDisplayName("Precio P.")]
        public string PscnPrcP { get; set; }
    }
}