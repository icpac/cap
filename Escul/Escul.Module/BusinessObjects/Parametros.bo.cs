using Cap.Generales.BusinessObjects.General;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Escul.Module.BusinessObjects.Admin;

// https://www.youtube.com/watch?v=QC5C5EiWuXw
namespace Escul.Module.BusinessObjects
{
    [XafDisplayName("Parámetros")]
    [NavigationItem("Configuración")]
    public partial class Parametros : ISingleton
    {
        /*
        [DisplayName("Ciclo Actual")]
        public CicloEsclr CiclAct
        {
            get { return _ciclAct; }
            set { SetPropertyValue("CiclAct", ref _ciclAct, value); }
        }*/


        private Catalogo mCiclActl;
        [DataSourceCriteria("Tipo == 'CicloEsclr'")]
        [DisplayName("Ciclo Actual")]
        public Catalogo CiclActl
        {
            get { return mCiclActl; }
            set { SetPropertyValue("CiclActl", ref mCiclActl, value); }
        }

        private string mUltmMtrcl;
        [VisibleInDetailView(false)]
        [DisplayName("Última Matrícula")]
        /// <summary>
        /// Última matrícula, será que puede ser secuencial ? Para este caso lo hacemos invisible
        /// </summary>
        public string UltmMtrcl
        {
            get { return mUltmMtrcl; }
            set { SetPropertyValue("UltmMtrcl", ref mUltmMtrcl, value); }
        }

        private uint mUltmFl;
        [DisplayName("Última Cobro")]
        public uint UltmFl
        {
            get { return mUltmFl; }
            set { SetPropertyValue("UltmFl", ref mUltmFl, value); }
        }

        private Servicio mInscrpcn;
        /// <summary>
        /// Servicio a cobrar al dar de alta el alumno y hacer la inscripción
        /// Tal vez son más de un servicio?
        /// </summary>
        [DisplayName("Inscripción (Servicio)")]
        public Servicio Inscrpcn
        {
            get { return mInscrpcn; }
            set { SetPropertyValue("Inscrpcn", ref mInscrpcn, value); }
        }

        private ConceptoCxcxp mCncptPg;
        /// <summary>
        /// Concepto de pago que se usa al pagar un DocumentoSalida
        /// </summary>
        [DataSourceCriteria("Tipo == 'Abono'")]
        [DisplayName("Concepto para Pagos")]
        public ConceptoCxcxp CncptPg
        {
            get { return mCncptPg; }
            set { SetPropertyValue("CncptPg", ref mCncptPg, value); }
        }

        private ConceptoCxcxp mCncptCbr;
        /// <summary>
        /// Concepto de cobro que se usa al crear un DocumentoSalida
        /// </summary>
        [DataSourceCriteria("Tipo == 'Cargo'")]
        [DisplayName("Concepto para Cobros")]
        public ConceptoCxcxp CncptCbr
        {
            get { return mCncptCbr; }
            set { SetPropertyValue("CncptCbr", ref mCncptCbr, value); }
        }

        private uint mUltmPg;
        [DisplayName("Última Pago, Declarable")]
        public uint UltmPg
        {
            get { return mUltmPg; }
            set { SetPropertyValue("UltmPg", ref mUltmPg, value); }
        }

        private uint mUltmPgNDclrbl;
        [DisplayName("Última Pago, NO Declarable")]
        public uint UltmPgNDclrbl
        {
            get { return mUltmPgNDclrbl; }
            set { SetPropertyValue("UltmPgNDclrbl", ref mUltmPgNDclrbl, value); }
        }

        // TI Se podrá modelar como una politica de descuento?
        private float mPrctj1;
        [ModelDefault("DisplayFormat", "{0:n2}%")]
        [DevExpress.Xpo.DisplayName("Febrero")]
        public float Prctj1
        {
            get { return mPrctj1; }
            set { SetPropertyValue("Prctj1", ref mPrctj1, value); }
        }

        private float mPrctj2;
        [ModelDefault("DisplayFormat", "{0:n2}%")]
        [DevExpress.Xpo.DisplayName("Marzo")]
        public float Prctj2
        {
            get { return mPrctj2; }
            set { SetPropertyValue("Prctj2", ref mPrctj2, value); }
        }

        private float mPrctj3;
        [ModelDefault("DisplayFormat", "{0:n2}%")]
        [DevExpress.Xpo.DisplayName("Abril")]
        public float Prctj3
        {
            get { return mPrctj3; }
            set { SetPropertyValue("Prctj2", ref mPrctj3, value); }
        }

        private float mPrctj4;
        [ModelDefault("DisplayFormat", "{0:n2}%")]
        [DevExpress.Xpo.DisplayName("Mayo")]
        public float Prctj4
        {
            get { return mPrctj4; }
            set { SetPropertyValue("Prctj4", ref mPrctj4, value); }
        }

        private float mPrctj5;
        [ModelDefault("DisplayFormat", "{0:n2}%")]
        [DevExpress.Xpo.DisplayName("Junio")]
        public float Prctj5
        {
            get { return mPrctj5; }
            set { SetPropertyValue("Prctj2", ref mPrctj5, value); }
        }

        private Servicio mSrvcBc;
        [XafDisplayName("Servicio Beca")]
        /// <summary>
        /// Servicio al que se le aplica el porcentaje de Beca, normalmente es a la colegiatura-
        /// </summary>
        public Servicio SrvcBc
        {
            get { return mSrvcBc; }
            set { SetPropertyValue("SrvcBc", ref mSrvcBc, value); }
        }

        private bool mUppr;
        //TIT Oct 2018 Ahora lo dejamos libre
        //[Appearance("Parametros.Uppr", AppearanceItemType = "LayoutItem", Context = "DetailView", Criteria = "!IsCurrentUserInRole('Administrator')", Visibility = ViewItemVisibility.Hide)]
        [XafDisplayName("Texto Mayúsculas")]
        public bool Uppr
        {
            get { return mUppr; }
            set { SetPropertyValue("Uppr", ref mUppr, value); }
        }

        private string mRutaArchvs;
        [DisplayName("Ruta para archivos")]
        public string RutaArchvs
        {
            get { return mRutaArchvs; }
            set { SetPropertyValue("RutaArchvs", ref mRutaArchvs, value); }
        }


        [RuleFromBoolProperty("ParametrosPrctj1", DefaultContexts.Save, "El Porcentaje de Febrero debe estar entre 0 y 100")]
        protected bool Prctj1Ok
        {
            get { return Prctj1 >= 0 && Prctj1 < 100; }
        }

        [RuleFromBoolProperty("ParametrosPrctj2", DefaultContexts.Save, "El Porcentaje de Marzo debe estar entre 0 y 100")]
        protected bool Prctj2Ok
        {
            get { return Prctj2 >= 0 && Prctj2 < 100; }
        }

        [RuleFromBoolProperty("ParametrosPrctj3", DefaultContexts.Save, "El Porcentaje de Abril debe estar entre 0 y 100")]
        protected bool Prctj3Ok
        {
            get { return Prctj3 >= 0 && Prctj3 < 100; }
        }

        [RuleFromBoolProperty("ParametrosPrctj4", DefaultContexts.Save, "El Porcentaje de Mayo debe estar entre 0 y 100")]
        protected bool Prctj4Ok
        {
            get { return Prctj4 >= 0 && Prctj4 < 100; }
        }

        [RuleFromBoolProperty("ParametrosPrctj5", DefaultContexts.Save, "El Porcentaje de Junio debe estar entre 0 y 100")]
        protected bool Prctj5Ok
        {
            get { return Prctj5 >= 0 && Prctj5 < 100; }
        }
    }
}
