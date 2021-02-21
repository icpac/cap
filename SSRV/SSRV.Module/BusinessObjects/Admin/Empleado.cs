using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using Cap.Empresa.BusinessObjects.Empresa;

namespace SSRV.Module.BusinessObjects.Admin
{
    [NavigationItem("Empresa")]
    [ImageName("Worker")]
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Empleado : EmpleadoBase
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Empleado(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).


            Persona.Curp = "C";
            Persona.Rfc = "AAA010101AAA";
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




        private string mContrato;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(20)]
        public string Contrato
        {
            get { return mContrato; }
            set { SetPropertyValue("Contrato", ref mContrato, value); }
        }

        private string mJornada;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(20)]
        public string Jornada
        {
            get { return mJornada; }
            set { SetPropertyValue("Jornada", ref mJornada, value); }
        }


        // #region + Clabe interbancaria
        private string FClabe;
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("EditMask", "[0-9]{18}"), ModelDefault("EditMaskType", "RegEx")]
        [Size(20)]
        public string Clabe
        {
            get { return FClabe; }
            set
            {
                if (value != null)
                    SetPropertyValue("Clabe", ref FClabe, ValorString("Clabe", value.ToUpper()));
            }
        }

        private DateTime FFUltMovimiento;
        [VisibleInDetailView(false)]
        [DevExpress.Xpo.DisplayName("Fch. Últ. Mov.")]
        [Appearance("FUltMovimiento", Context = "DetailView", Enabled = false, FontStyle = System.Drawing.FontStyle.Italic)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        public DateTime FUltMovimiento
        {
            get { return FFUltMovimiento; }
            set { SetPropertyValue("FUltMovimiento", ref FFUltMovimiento, value); }
        }

        [RuleFromBoolProperty("RfcOk", DefaultContexts.Save, "Debe capturar el RFC")]
        protected bool RfcOk
        {
            get { return Persona != null && !string.IsNullOrEmpty(Persona.Rfc); }
        }

        [RuleFromBoolProperty("CurpOk", DefaultContexts.Save, "Debe capturar el CURP")]
        protected bool CurpOk
        {
            get { return Persona != null && !string.IsNullOrEmpty(Persona.Curp); }
        }

    }
}