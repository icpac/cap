using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
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




        /* TI
        private Contratacion mRegimen;
        [RuleRequiredField("RuleRequiredField for Empleado.Regimen", DefaultContexts.Save, "Debe capturar el Régimen del Empleado", SkipNullOrEmptyValues = false)]
        [DisplayName("Régimen")]
        [VisibleInListView(false)]
        public Contratacion Regimen
        {
            get { return mRegimen; }
            set { SetPropertyValue("Regimen", ref mRegimen, value); }
        }*/

        /* TI
        private Riesgo mRiesgo;
        [VisibleInListView(false)]
        public Riesgo Riesgo
        {
            get { return mRiesgo; }
            set { SetPropertyValue("Riesgo", ref mRiesgo, value); }
        }*/

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

        /*
        private Banco mBanco;
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public Banco Banco
        {
            get { return mBanco; }
            set { SetPropertyValue("Banco", ref mBanco, value); }
        }*/

        /* TI
        private Puesto mPuesto;
        public Puesto Puesto
        {
            get { return mPuesto; }
            set { SetPropertyValue("Puesto", ref mPuesto, value); }
        }*/

        /* TI
        private Departamento mDepartamento;
        public Departamento Departamento
        {
            get { return mDepartamento; }
            set { SetPropertyValue("Departamento", ref mDepartamento, value); }
        }*/

        /*TI
        // #region + Formas de Pago
        [DisplayName("Formas de Pago")]
        [Association("Empleado-FormasP", typeof(NItemFormaP)), DevExpress.Xpo.Aggregated]
        public XPCollection FormasP
        {
            get { return GetCollection("FormasP"); }
        }*/

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

        /*TI
        private Zona mZona;
        /// <summary>
        /// Qué región atiende este empleado.
        /// </summary>
        public Zona Zona
        {
            get { return mZona; }
            set { SetPropertyValue("Zona", ref mZona, value); }
        }*/

        /*TI
        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Persona.Curp = "C";
            Persona.Rfc = "AAA010101AAA";
        }*/
    }
}