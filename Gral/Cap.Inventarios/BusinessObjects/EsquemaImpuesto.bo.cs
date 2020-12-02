using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Cap.Generales.BusinessObjects.Object;
using DevExpress.ExpressApp.DC;

namespace Cap.Inventarios.BusinessObjects
{
    [NavigationItem("Inventarios")]
    [VisibleInReports(false)]
    [ImageName("Total")]
    [DefaultProperty("DisplayLook")]
    public partial class EsquemaImpuesto : PObject
    {
        const ushort LONCVE = 3; // 10;
        const ushort LONDES = 45; // 50

        private string FClave;
        [RuleRequiredField("RuleRequiredField for Esquema.Clave", DefaultContexts.Save, "Debe capturar la Clave", SkipNullOrEmptyValues = false)]
        [VisibleInLookupListView(true)]
        [Size(LONCVE), Indexed(Unique = true)]
        public string Clave
        {
            get { return FClave; }
            set
            {
                if (value != null)
                    SetPropertyValue("Clave", ref FClave, value.ToUpper());
            }
        }

        private string FDescripcion;
        [DevExpress.Xpo.DisplayName("Descripción")]
        [VisibleInLookupListView(true)]
        [Size(LONDES)]
        public string Descripcion
        {
            get { return FDescripcion; }
            set { SetPropertyValue("Descripcion", ref FDescripcion, value); }
        }

        [RuleFromBoolProperty("Impuesto1Ok", DefaultContexts.Save, "El Impuesto 1 debe estar entre 0 y 100")]
        protected bool Impuesto1Ok
        {
            get { return Impuesto1 > -100 && Impuesto1 < 100; }
        }

        private float FImpuesto1;
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:n2}%")]
        [ModelDefault("EditMask", "P2"), ModelDefault("EditMaskType", "Default")]
        public float Impuesto1
        {
            get { return FImpuesto1; }
            set { SetPropertyValue("Impuesto1", ref FImpuesto1, value); }
        }

        private EAplicaImpuesto FAplImpuesto1;
        [DevExpress.Xpo.DisplayName("Aplicar Impuesto1 a")]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public EAplicaImpuesto AplImpuesto1
        {
            get { return FAplImpuesto1; }
            set { SetPropertyValue("AplImpuesto1", ref FAplImpuesto1, value); }
        }

        private Impuesto mTpImpst1;
        [VisibleInListView(false)]
        [XafDisplayName("Tipo Impuesto1")]
        public Impuesto TpImpst1
        {
            get { return mTpImpst1; }
            set { SetPropertyValue("TpImpst1", ref mTpImpst1, value); }
        }

        [RuleFromBoolProperty("Impuesto2Ok", DefaultContexts.Save, "El Impuesto 2 debe estar entre 0 y 100")]
        protected bool Impuesto2Ok
        {
            get { return Impuesto2 > -100 && Impuesto2 < 100; }
        }

        private float FImpuesto2;
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:n2}%")]
        public float Impuesto2
        {
            get { return FImpuesto2; }
            set { SetPropertyValue("Impuesto2", ref FImpuesto2, value); }
        }

        private EAplicaImpuesto FAplImpuesto2;
        [DevExpress.Xpo.DisplayName("Aplicar Impuesto2 a")]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public EAplicaImpuesto AplImpuesto2
        {
            get { return FAplImpuesto2; }
            set { SetPropertyValue("AplImpuesto2", ref FAplImpuesto2, value); }
        }

        private Impuesto mTpImpst2;
        [VisibleInListView(false)]
        [XafDisplayName("Tipo Impuesto2")]
        public Impuesto TpImpst2
        {
            get { return mTpImpst2; }
            set { SetPropertyValue("TpImpst2", ref mTpImpst2, value); }
        }

        [RuleFromBoolProperty("Impuesto3Ok", DefaultContexts.Save, "El Impuesto 3 debe estar entre 0 y 100")]
        protected bool Impuesto3Ok
        {
            get { return Impuesto3 > -100 && Impuesto3 < 100; }
        }

        private float FImpuesto3;
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:n2}%")]
        public float Impuesto3
        {
            get { return FImpuesto3; }
            set { SetPropertyValue("Impuesto3", ref FImpuesto3, value); }
        }

        private EAplicaImpuesto FAplImpuesto3;
        [DevExpress.Xpo.DisplayName("Aplicar Impuesto3 a")]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public EAplicaImpuesto AplImpuesto3
        {
            get { return FAplImpuesto3; }
            set { SetPropertyValue("AplImpuesto3", ref FAplImpuesto3, value); }
        }

        private Impuesto mTpImpst3;
        [VisibleInListView(false)]
        [XafDisplayName("Tipo Impuesto3")]
        public Impuesto TpImpst3
        {
            get { return mTpImpst3; }
            set { SetPropertyValue("TpImpst3", ref mTpImpst3, value); }
        }

        [RuleFromBoolProperty("Impuesto4Ok", DefaultContexts.Save, "El Impuesto 4 debe estar entre 0 y 100")]
        protected bool Impuesto4Ok
        {
            get { return Impuesto4 > -100 && Impuesto4 < 100; }
        }

        private float FImpuesto4;
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:n2}%")]
        public float Impuesto4
        {
            get { return FImpuesto4; }
            set { SetPropertyValue("Impuesto4", ref FImpuesto4, value); }
        }

        private EAplicaImpuesto FAplImpuesto4;
        [DevExpress.Xpo.DisplayName("Aplicar Impuesto4 a")]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public EAplicaImpuesto AplImpuesto4
        {
            get { return FAplImpuesto4; }
            set { SetPropertyValue("AplImpuesto4", ref FAplImpuesto4, value); }
        }

        private Impuesto mTpImpst4;
        [VisibleInListView(false)]
        [XafDisplayName("Tipo Impuesto4")]
        public Impuesto TpImpst4
        {
            get { return mTpImpst4; }
            set { SetPropertyValue("TpImpst4", ref mTpImpst4, value); }
        }

        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [NonPersistent]
        public string DisplayLook
        {
            get { return string.Format("{0} - {1}", Clave, Descripcion); }
        }


        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Clave = string.Empty;
            Descripcion = string.Empty;

            Impuesto1 = 0;
            Impuesto2 = 0;
            Impuesto3 = 0;
            Impuesto4 = 0;

            AplImpuesto1 = EAplicaImpuesto.Precio;
            AplImpuesto2 = EAplicaImpuesto.Precio;
            AplImpuesto3 = EAplicaImpuesto.Precio;
            AplImpuesto4 = EAplicaImpuesto.Precio;
        }
    }
}
