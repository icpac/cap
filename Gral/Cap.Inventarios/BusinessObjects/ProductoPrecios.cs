using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using Cap.Generales.BusinessObjects.Object;
using DevExpress.ExpressApp.Model;

namespace Cap.Inventarios.BusinessObjects
{
    [DefaultProperty("Prc")] // Por qué le puse Dscrpcn?
    // [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class ProductoPrecios : PObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public ProductoPrecios(Session session)
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


        private Producto mPrdct;
        [Association("Producto-Precios")]
        public Producto Prdct
        {
            get { return mPrdct; }
            set { SetPropertyValue("Prdct", ref mPrdct, value); }
        }

        private string mDscrpcn;
        [Index(1)]
        [VisibleInLookupListView(true)]
        [RuleRequiredField("RuleRequiredField for ProductoPrecios.Dscrpcn", DefaultContexts.Save, "Debe capturar la Descripción", SkipNullOrEmptyValues = false)]
        [Size(50)]
        [XafDisplayName("Descripción")]
        public string Dscrpcn
        {
            get { return mDscrpcn; }
            set { SetPropertyValue("Dscrpcn", ref mDscrpcn, value); }
        }

        private decimal mPrc;
        [Index(2)]
        [VisibleInLookupListView(true)]
        [XafDisplayName("Precio")]
        public decimal Prc
        {
            get { return mPrc; }
            set { SetPropertyValue("Prc", ref mPrc, value); }
        }

        private float mIncrmnt;
        [XafDisplayName("Incremento")]
        //[ModelDefault("DisplayFormat", "{0:n3}%")]
        [ModelDefault("DisplayFormat", "{0:n1}%")]
        [VisibleInLookupListView(false)]
        public float Incrmnt
        {
            get { return mIncrmnt; }
            set { SetPropertyValue("Incrmnt", ref mIncrmnt, value); }
        }

        private float? mCntdd;
        [XafDisplayName("Cantidad")]
        public float? Cntdd
        {
            get { return mCntdd; }
            set { SetPropertyValue("Cntdd", ref mCntdd, value); }
        }
    }
}