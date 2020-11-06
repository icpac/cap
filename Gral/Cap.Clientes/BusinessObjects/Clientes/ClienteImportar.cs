using DevExpress.Xpo;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using Cap.Generales.BusinessObjects.Object;
using Cap.Generales.BusinessObjects.General;

namespace Cap.Clientes.BusinessObjects.Clientes
{
    //[DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class ClienteImportar : PObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public ClienteImportar(Session session)
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
        //    set { SetPropertyValue(nameof(PersistentProperty), ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}

        [RuleRequiredField("RuleRequiredField for ClienteImportar.InputFile", DefaultContexts.Save, "Debe capturar el Archivo", SkipNullOrEmptyValues = false)]
        [XafDisplayName("Archivo a Cargar")]
        [FileTypeFilter("Excel", 1, "*.xlsx")]
        public MyFileData InputFile { get; set; }

        [RuleRequiredField("RuleRequiredField for ClienteImportar.PscnNmbr", DefaultContexts.Save, "Debe capturar la posición de la Clave", SkipNullOrEmptyValues = false)]
        [XafDisplayName("Nombre")]
        public string PscnNmbr { get; set; }

        [XafDisplayName("Correo")]
        public string PscnCrr { get; set; }

        [XafDisplayName("Rfc")]
        public string PscnRfc { get; set; }

        [XafDisplayName("Teléfono")]
        public string PscnTlfn { get; set; }

        [XafDisplayName("Direccion")]
        public string PscnDrccn { get; set; }

        [XafDisplayName("Notas")]
        public string PscnNts { get; set; }
        
        [XafDisplayName("Número de Hoja")]
        public short NmrHj { get; set; }

        [XafDisplayName("Renglón")]
        public short Rngln { get; set; }
    }
}