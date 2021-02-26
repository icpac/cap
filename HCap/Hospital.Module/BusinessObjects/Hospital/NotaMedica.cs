using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using HCap.Module.BusinessObjects.Hospital;
using Cap.Generales.BusinessObjects.Object;
using Hospital.Module.BusinessObjects.Hospital;
using Cap.Generales.BusinessObjects.Empresa;

namespace HCap.Module.BusinessObjects.Hospital
{
    [Appearance("NotaMedica.Expediente.Read", AppearanceItemType = "ViewItem", Context = "DetailView", TargetItems = "ExpdntPcnt.Ps, " +
        "ExpdntPcnt.Tll, ExpdntPcnt.TnsnArtrl, ExpdntPcnt.FrcncCrdc, ExpdntPcnt.FrcncRsprtr, ExpdntPcnt.Tmprtr, ExpdntPcnt.Glcs, ExpdntPcnt.FchCt", Enabled = false)]
    [NavigationItem("Hospital")]
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class NotaMedica : PObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public NotaMedica(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            Fch = DateTime.Today;
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

        private string mFl;
        [Appearance("NotaMedica.Fl", AppearanceItemType = "ViewItem", Context = "DetailView", Enabled = false)]
        [XafDisplayName("Folio")]
        [FieldSize(10)]
        public string Fl
        {
            get { return mFl; }
            set { SetPropertyValue("Fl", ref mFl, value); }
        }

        private Paciente mPcnt;
        [ImmediatePostData]
        [RuleRequiredField("RuleRequiredField for NotaMedica.Pcnt", DefaultContexts.Save, "Debe capturar el Paciente", SkipNullOrEmptyValues = false)]
        [XafDisplayName("Paciente")]
        public Paciente Pcnt
        {
            get { return mPcnt; }
            set { SetPropertyValue("Pcnt", ref mPcnt, value); }
        }

        private DateTime mFch;
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        [XafDisplayName("Fecha")]
        public DateTime Fch
        {
            get { return mFch; }
            set { SetPropertyValue("Fch", ref mFch, value); }
        }

        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public string FchStr
        {
            get { return Fch.ToLongDateString(); }
        }

        private float mPs;
        // https://www.devexpress.com/Support/Center/Question/Details/Q308742/rulerange-maximum-value-is-not-working
        [ModelDefault("MinValue", "0"), ModelDefault("MaxValue", "150")]
        [ImmediatePostData]
        [XafDisplayName("Peso (Kg)")]
        public float Ps
        {
            get { return mPs; }
            set { SetPropertyValue("Ps", ref mPs, value); }
        }

        private ushort mTll;
        [ModelDefault("MinValue", "0"), ModelDefault("MaxValue", "250")]
        [ImmediatePostData]
        [XafDisplayName("Talla (cm)")]
        public ushort Tll
        {
            get { return mTll; }
            set { SetPropertyValue("Tll", ref mTll, value); }
        }

        [Appearance("NotaMedia.IMCSobre", FontColor ="Orange", Criteria = "IMC > 25 && IMC < 30", FontStyle = System.Drawing.FontStyle.Bold)]
        [Appearance("NotaMedia.IMCObe", FontColor = "Red", Criteria = "IMC > 30", FontStyle = System.Drawing.FontStyle.Bold)]
        [ModelDefault("DisplayFormat", "{0:n2}")]
        public double IMC
        {
            get
            {
                if (mTll == 0)
                    return 0;
                else
                {
                    double auxt = mTll;
                    auxt = auxt / 100;
                    return mPs / (auxt * auxt);
                }
            }
        }

        // No menor que today
        private DateTime? mFchCt;
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        [XafDisplayName("Próxima Cita")]
        public DateTime? FchCt
        {
            get { return mFchCt; }
            set { SetPropertyValue("FchCt", ref mFchCt, value); }
        }

        private DateTime? mHrCt;
        [ModelDefault("PropertyEditorType", "HCap.Module.Win.Editors.TimeEditPropertyEditor")]
        [ModelDefault("PropertyEditorType", "HCap.Module.Web.Editors.ASPxTimePropertyEditor")]
        [DisplayName("Hora de Cita")]
        [VisibleInListView(false)]
        // [ModelDefault("PropertyEditorType", "SSRV.Module.Web.Editors.ASPxTimePropertyEditor")]
        public DateTime? HrCt
        {
            get { return mHrCt; }
            set { SetPropertyValue("HrCt", ref mHrCt, value); }
        }


        private string mTnsnArtrl;
        [ModelDefault("DisplayFormat", "{0: 000 / 000}"), ModelDefault("EditMask", "999 / 999")]
        [RuleRequiredField("RuleRequiredField for NotaMedica.TnsnArtrl", DefaultContexts.Save, "Debe capturar la Tensión Arterial", SkipNullOrEmptyValues = false)]
        [Size(20)]
        [XafDisplayName("Tensión Arterial")]
        public string TnsnArtrl
        {
            get { return mTnsnArtrl; }
            set { SetPropertyValue("TnsnArtrl", ref mTnsnArtrl, value); }
        }

        private ushort mFrcncCrdc;
        [XafDisplayName("Frecuencia Cardiaca (minuto)")]
        public ushort FrcncCrdc
        {
            get { return mFrcncCrdc; }
            set { SetPropertyValue("FrcncCrdc", ref mFrcncCrdc, value); }
        }

        private ushort mFrcncRsprtr;
        [XafDisplayName("Frecuencia Respiratoria (minuto)")]
        public ushort FrcncRsprtr
        {
            get { return mFrcncRsprtr; }
            set { SetPropertyValue("FrcncRsprtr", ref mFrcncRsprtr, value); }
        }

        private float mTmprtr;
        [XafDisplayName("Temperatura (°C)")]
        public float Tmprtr
        {
            get { return mTmprtr; }
            set { SetPropertyValue("Tmprtr", ref mTmprtr, value); }
        }

        private ushort mGlcs;
        [XafDisplayName("Glucosa (mg/dl)")]
        // [Size(5)]
        public ushort Glcs
        {
            get { return mGlcs; }
            set { SetPropertyValue("Glcs", ref mGlcs, value); }
        }

        string mAntcdntPtlgcs;
        [Size(SizeAttribute.Unlimited)]
        // [NonPersistent]
        [XafDisplayName("Antecedentes Patológicos")]
        public string AntcdntPtlgcs
        {
            get
            {
                if (mPcnt != null && mPcnt.Expdnt != null)
                    mAntcdntPtlgcs = mPcnt.Expdnt.OldPtlgcs;

                return mAntcdntPtlgcs;
                /*
                else
                    return string.Empty;*/
            }
            set
            {
                SetPropertyValue("mAntcdntPtlgcs", ref mAntcdntPtlgcs, value);
            }
        }
        private string mPdcmnt;
        [Size(SizeAttribute.Unlimited)]
        [XafDisplayName("Padecimento Actual")]
        public string Pdcmnt
        {
            get { return mPdcmnt; }
            set { SetPropertyValue("Pdcmnt", ref mPdcmnt, value); }
        }

        private string mExplrcnFsc;
        [Size(SizeAttribute.Unlimited)]
        [XafDisplayName("Exploración Física")]
        public string ExplrcnFsc
        {
            get { return mExplrcnFsc; }
            set { SetPropertyValue("ExplrcnFsc", ref mExplrcnFsc, value); }
        }

        private string mDgnstc;
        [Size(SizeAttribute.Unlimited)]
        [XafDisplayName("Diágnostico")]
        public string Dgnstc
        {
            get { return mDgnstc; }
            set { SetPropertyValue("Dgnstc", ref mDgnstc, value); }
        }

        private string mEstds;
        [XafDisplayName("Estudios de Laboratorio y Gabinete")]
        [Size(SizeAttribute.Unlimited)]
        public string Estds
        {
            get { return mEstds; }
            set { SetPropertyValue("Estds", ref mEstds, value); }
        }

        private string mIndccns;
        // TIT Feb 2021 regresamos a esto para ver si es suficiente
        //[Obsolete("En su lugar usar Indicaciones")]
        // TIT jul 2018 [RuleRequiredField("RuleRequiredField for NotaMedica.Indccns", DefaultContexts.Save, "Debe capturar las Indicaciones", SkipNullOrEmptyValues = false)]
        [Size(SizeAttribute.Unlimited)]
        [XafDisplayName("Indicaciones")]
        public string Indccns
        {
            get { return mIndccns; }
            set { SetPropertyValue("Indccns", ref mIndccns, value); }
        }

        // Apply the Association attribute to mark the Orders property 
        // as the one end of the Customer-Orders association.
        [Association("NotaMedica-Prescripciones", typeof(Prescripcion)), DevExpress.Xpo.Aggregated]
        public XPCollection Prescripciones
        {
            get { return GetCollection("Prescripciones"); }
        }

        [Obsolete("Si es lo que creo, sólo dejaré lugar para texto")]
        // Apply the Association attribute to mark the Orders property 
        // as the one end of the Customer-Orders association.
        [Association("NotaMedica-Indicaciones", typeof(Indicacion)), DevExpress.Xpo.Aggregated]
        public XPCollection Indicaciones
        {
            get { return GetCollection("Indicaciones"); }
        }

        // Apply the Association attribute to mark the Orders property 
        // as the one end of the Customer-Orders association.
        [Association("NotaMedica-Estudios", typeof(EstudioLG)), DevExpress.Xpo.Aggregated]
        public XPCollection Estudios
        {
            get { return GetCollection("Estudios"); }
        }
        
        [Association("NotaMedica-Diagnosticos", typeof(Diagnostico)), DevExpress.Xpo.Aggregated]
        public XPCollection Diagnosticos
        {
            get { return GetCollection("Diagnosticos"); }
        }

        [Obsolete("Si es lo que creo, sólo dejaré lugar para texto")]
        [Association("NotaMedica-PadecimientosActuales", typeof(Padecimiento)), DevExpress.Xpo.Aggregated]
        public XPCollection PadecimientosActuales
        {
            get { return GetCollection("PadecimientosActuales"); }
        }

        [Association("NotaMedica-ExploracionFisicas", typeof(ExploracionFisica)), DevExpress.Xpo.Aggregated]
        public XPCollection ExploracionFisicas
        {
            get { return GetCollection("ExploracionFisicas"); }
        }

        //[ExpandObjectMembers(ExpandObjectMembers.Never), ImmediatePostData]
        [Association("NotaMedica-Expediente", typeof(ExpedienteArchivo)), DevExpress.Xpo.Aggregated]
        public XPCollection Archivos
        {
            get { return GetCollection("Archivos"); }
        }

        private Expediente mExpdnt;
        [Association("Expediente-Notas")]
        public Expediente Expdnt
        {
            get { return mExpdnt; }
            set { SetPropertyValue("Expdnt", ref mExpdnt, value); }
        }

        [Appearance("NotaMedica.ExpdntPcnt", Context = "DetailView", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsNewObject(This)")]// Criteria = "IsNull(Expdnt)")]
        //[NonPersistent]
        [ExpandObjectMembers(ExpandObjectMembers.Always)]
        [VisibleInListView(false)]
        [XafDisplayName("Expediente")]
        public Expediente ExpdntPcnt
        {
            get
            {
                if (mPcnt != null)
                    return mPcnt.Expdnt;
                return null;
            }
        }

        [NonPersistent]
        [XafDisplayName("Foto Paciente")]
        [ImageEditor(ListViewImageEditorCustomHeight = 75, DetailViewImageEditorFixedHeight = 150)]
        public byte[] FtPcnt
        {
            get
            {
                if (mPcnt != null && mPcnt.Persona != null)
                    return mPcnt.Persona.Foto;
                return null;
            }
        }

        private Empresa mEmpresa;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public Empresa Emprs
        {
            get
            {
                if (mEmpresa == null)
                    mEmpresa = Session.FindObject<Empresa>(null);                 
                return mEmpresa;
            }
        }

        [NonPersistent]
        [XafDisplayName("Edad")]
        public string Edd
        {
            get
            {
                if (mPcnt != null && mPcnt.Persona != null)
                    return mPcnt.Persona.EdadP;
                return string.Empty;
            }
        }

        [NonPersistent]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public string Alrgs
        {
            get
            {
                if (mPcnt != null && mPcnt.Expdnt != null)
                {
                    int cnt = 0;
                    string aux = string.Empty;
                    foreach (Alergia alrg in mPcnt.Expdnt.Alergias)
                    {
                        if (cnt == 0)
                            aux = string.Format("{0}", alrg.Alrg.Dscrpcn);
                        else
                            aux = string.Format("{0}, {1}", aux, alrg.Alrg.Dscrpcn);
                        cnt++;
                    }
                    return aux;
                }
                else
                    return string.Empty;
            }
        }



        [RuleFromBoolProperty("PsOk", DefaultContexts.Save, "El Peso debe ser mayor o igual que 0 y menor a 150")]
        protected bool PsOk
        {
            get { return mPs >= 0 && mPs <= 150; }
        }

        [RuleFromBoolProperty("FrcncRsprtrOk", DefaultContexts.Save, "Debe capturar la Frecuencia Respiratoria")]
        protected bool FrcncRsprtrOk
        {
            get { return mFrcncRsprtr > 0; }
        }

        [RuleFromBoolProperty("FrcncCrdcOk", DefaultContexts.Save, "Debe capturar la Frecuencia Cardiaca")]
        protected bool FrcncCrdcOk
        {
            get { return mFrcncCrdc > 0; }
        }

        [RuleFromBoolProperty("TmprtrOk", DefaultContexts.Save, "Debe capturar la Temperatura")]
        protected bool TmprtrOk
        {
            get { return mTmprtr > 0 && mTmprtr < 100; }
        }

        [RuleFromBoolProperty("FchCtOk", DefaultContexts.Save, "La fecha de la próxima cita debe ser mayo a la Fecha Actual")]
        protected bool FchCtOk
        {
            get { return mFchCt == null || (mFchCt >= DateTime.Today); }
        }

        [RuleFromBoolProperty("IndicacionesOk", DefaultContexts.Save, "Debe capturar Indicaciones")]
        protected bool IndicacionesOk
        {
            get {
                return !string.IsNullOrEmpty(Indccns);
                /*return Indicaciones.Count > 0;*/ }
        }
    }
}