#region Copyright (c) 2015-2020 cjlc
/*
{+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++)
{                                                                   }
{     cjlc Cap control administrativo personal                      }
{                                                                   }
{     Copyrigth (c) 2015-2020 cjlc                                  }
{     Todos los derechos reservados                                 }
{                                                                   }
{*******************************************************************}
 */
#endregion

using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using Cap.Generales.BusinessObjects.Object;
using Cap.Personas.BusinessObjects;

namespace Cap.Bancos.BusinessObjects
{
    // [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112701.aspx).
    public class Beneficiario : PObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Beneficiario(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).

            Bnfcr = new Persona(Session);
            Bnfcr.Rfc = "XAXX010101000";
            Bnfcr.Curp = "C";
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

        private Bancaria FCuenta;
        [Association("Bancaria-Beneficiarios")]
        public Bancaria Cnt
        {
            get { return FCuenta; }
            set { SetPropertyValue("Cnt", ref FCuenta, value); }
        }

        private Persona mBnfcr;
        [ExpandObjectMembers(ExpandObjectMembers.Always)]
        [DisplayName("Beneficiario")]
        public Persona Bnfcr
        {
            get { return mBnfcr; }
            set { SetPropertyValue("Bnfcr", ref mBnfcr, value); }
        }

        private float mPrcntj;
        [DisplayName("Porcentaje")]
        public float Prcntj
        {
            get { return mPrcntj; }
            set { SetPropertyValue("Prcntj", ref mPrcntj, value); }
        }
    }
}
