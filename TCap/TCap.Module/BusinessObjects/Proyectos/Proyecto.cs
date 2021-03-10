using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using TCap.Module.BusinessObjects.Empresa;
using Cap.Generales.BusinessObjects.Object;
using Cap.Generales.BusinessObjects.Unidades;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using System.Drawing;
using TCap.Module.BusinessObjects.Clientes;
using Cap.Clientes.BusinessObjects.Clientes;

namespace TCap.Module.BusinessObjects.Proyectos
{
    [System.ComponentModel.DefaultProperty("Nmbr")]
    [Appearance("Proyecto.New", Priority = 2, AppearanceItemType = "ViewItem", TargetItems = "Cxcs, Gastos, Tareas, GstTtl, HrsRls, HrsEstmds, FchFn, FchInc, Rspnsbl, Involucrados, Incidencias", Visibility = ViewItemVisibility.Hide, Criteria = "[Stts] == 'Prospecto' || ([Stts] == 'Cancelado' && [SttsAntrr] == 'Prospecto')")]
    [Appearance("Proyecto.ActivoD", Priority = 2, AppearanceItemType = "ViewItem", TargetItems = "Clnt, Fl, Nmbr, FchAlt, Dscrpcn, Prc, Mnd, CndcnsPg", Enabled = false, Criteria = "Clnt != null &&  ([Stts] == 'Aceptado' || [Stts] == 'EnProceso' || [Stts] == 'Terminado' || [VvMrt] == 'Cancelado')")]
    [Appearance("Proyecto.Cancelado1", Priority = 2, AppearanceItemType = "ViewItem", TargetItems = "FchCnclcn, MtvCnclcnP, Cnclcn", Visibility = ViewItemVisibility.Hide, Criteria = "[VvMrt] == 'Alta'" /*"[Stts] == 'Activo' || [Stts] == 'Terminado' || [Stts] == 'Prospecto'"*/)]
    [Appearance("Proyecto.Cancelado2", Priority = 2, AppearanceItemType = "ViewItem", TargetItems = "FchCnclcn, MtvCnclcnP, Cnclcn", Enabled = false, Criteria = "[MtvCnclcnP] != null")]
    [Appearance("Proyecto.Cancelado3", Priority =2, AppearanceItemType ="ViewItem", TargetItems = "ClvClnt, Rspnsbl, FchInc, FchFn", Enabled = false, Criteria = "[VvMrt] == 'Cancelado'")]
    [Appearance("Proyecto.Prospecto", TargetItems = "Nmbr, Clv", Context = "ListView", Criteria = "[Stts] == 'Prospecto'", FontColor = "Orange")]
    [Appearance("Proyecto.Aceptado", TargetItems = "Nmbr, Clv", Context = "ListView", Criteria = "[Stts] == 'Aceptado' && [VvMrt] != 'Cancelado'", FontColor = "Blue")]
    [Appearance("Proyecto.Activo", TargetItems = "Nmbr, Clv", Context = "ListView", Criteria = "[Stts] == 'EnProceso'", FontColor = "Green")]
    [Appearance("Proyecto.Terminado", TargetItems = "Nmbr, Clv", Context = "ListView", Criteria = "[Stts] == 'Terminado'", FontColor = "Gray")]
    [Appearance("Proyecto.Cancelado", TargetItems = "VvMrt, Clv, Nmbr", Context = "ListView", Criteria = "[VvMrt] == 'Cancelado'", FontColor = "Red")]
    [NavigationItem("Proyectos")]
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Proyecto : PObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Proyecto(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            Mnd = Session.FindObject<Moneda>(new BinaryOperator("Sistema", true));
            FchAlt = DateTime.Now;
            SttsAntrr = EProyectoStatus.Prospecto;
            VvMrt = EEstadoDcmntPryct.Alta;
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


        private Cliente mClnt;
        [RuleRequiredField("RuleRequiredField for Proyecto.Clnt", DefaultContexts.Save, "Debe capturar el Cliente", SkipNullOrEmptyValues = false)]
        [ImmediatePostData]
        [XafDisplayName("Cliente")]
        public Cliente Clnt
        {
            get { return mClnt; }
            set
            {
                SetPropertyValue("Clnt", ref mClnt, value);
                if (IsNewObject() && !IsLoading && !IsSaving && value != null)
                {
                    Fl = mClnt.UltmPryct.ToString();
                }
            }
        }

        private string mFl;
        [XafDisplayName("Folio")]
        [FieldSize(10)]
        [RuleRequiredField("RuleRequiredField for Proyecto.Fl", DefaultContexts.Save, "Debe capturar el Folio", SkipNullOrEmptyValues = false)]
        public string Fl
        {
            get { return mFl; }
            set { SetPropertyValue("Fl", ref mFl, value); }
        }

        [XafDisplayName("Clave")]
        public string Clv
        {
            get
            {
                return string.Format("{0}-{1}-{2}", (Clnt == null || string.IsNullOrEmpty(Clnt.Als)) 
                    ? string.Empty 
                    : Clnt.Als.Length > 4 ? Clnt.Als.Trim().Substring(0,4) : Clnt.Als.Trim(),
                    string.IsNullOrEmpty(Fl) 
                    ? string.Empty 
                    : Fl.PadLeft(3, '0'), CreadoEl.ToString("yy"));
            }
        }

        private string mClvClnt;
        [VisibleInListView(false)]
        [FieldSize(20)]
        [XafDisplayName("Núm. Proyecto (Cliente)")]
        public string ClvClnt
        {
            get { return mClvClnt; }
            set { SetPropertyValue("ClvClnt", ref mClvClnt, value); }
        }

        private string mNmbr;
        [Size(100)]
        [RuleRequiredField("RuleRequiredField for Proyecto.Nombre", DefaultContexts.Save, "Debe capturar el Nombre", SkipNullOrEmptyValues = false)]
        [XafDisplayName("Nombre")]
        public string Nmbr
        {
            get { return mNmbr; }
            set { SetPropertyValue("Nmbr", ref mNmbr, value); }
        }

        [VisibleInListView(false)]
        [XafDisplayName("Nombre")]
        [Appearance("Proyecto.SlNmbr", AppearanceItemType = "LayoutItem", Context = "DetailView", Criteria = "IsNewObject(This)", Visibility = ViewItemVisibility.Hide)]
        [NonPersistent]
        public string SlNmbr
        {
            get { return mNmbr; }
        }

        private DateTime mFchAlt;
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        [XafDisplayName("Fecha de Alta")]
        public DateTime FchAlt
        {
            get { return mFchAlt; }
            set { SetPropertyValue("FchAlt", ref mFchAlt, value); }
        }

        private DateTime? mFchInc;
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        [XafDisplayName("Fecha de Inicio")]
        public DateTime? FchInc
        {
            get { return mFchInc; }
            set { SetPropertyValue("FchInc", ref mFchInc, value); }
        }

        private DateTime? mFchFn = null;
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        [XafDisplayName("Fecha de Terminación")]
        public DateTime? FchFn
        {
            get { return mFchFn; }
            set { SetPropertyValue("FchFn", ref mFchFn, value); }
        }

        private decimal mCst;
        [Obsolete("Creo que debí poner Precio, no Costo")]
        [XafDisplayName("Costo")]
        public decimal Cst
        {
            get { return mCst; }
            set { SetPropertyValue("Cst", ref mCst, value); }
        }

        private decimal mPrc;
        [VisibleInListView(false)]
        [XafDisplayName("Precio")]
        public decimal Prc
        {
            get { return mPrc; }
            set { SetPropertyValue("Prc", ref mPrc, value); }
        }

        private Moneda mMnd;
        [VisibleInListView(false)]
        [XafDisplayName("Moneda")]
        public Moneda Mnd
        {
            get { return mMnd; }
            set { SetPropertyValue("Mnd", ref mMnd, value); }
        }

        [Size(SizeAttribute.Unlimited)]
        [XafDisplayName("Descripción")]
        public string Dscrpcn;


        private EProyectoStatus mStts;
        [Appearance("Proyecto.Stts", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        [XafDisplayName("Estado")]
        public EProyectoStatus Stts
        {
            get { return mStts; }
            set { SetPropertyValue("Stts", ref mStts, value); }
        }

        private EProyectoStatus mSttsAntrr;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [XafDisplayName("Estado Anterior")]
        public EProyectoStatus SttsAntrr
        {
            get { return mSttsAntrr; }
            set { SetPropertyValue("SttsAntrr", ref mSttsAntrr, value); }
        }

        private EEstadoDcmntPryct mVvMrt;
        /// <summary>
        /// Para saber si está activo o cancelado
        /// </summary>
        [Appearance("Proyecto.VvMrt", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        [XafDisplayName("Status")]
        public EEstadoDcmntPryct VvMrt
        {
            get { return mVvMrt; }
            set { SetPropertyValue("VvMrt", ref mVvMrt, value); }
        } 

        private EmpleadoProyecto mRspnsbl;
        [XafDisplayName("Responsable")]
        public EmpleadoProyecto Rspnsbl
        {
            get { return mRspnsbl; }
            set { SetPropertyValue("Rspnsbl", ref mRspnsbl, value); }
        }

        [Persistent("HrsEstmds")]
        private float? mHrsEstmds = null;
        [VisibleInListView(false)]
        [XafDisplayName("Horas Estimadas")]
        [PersistentAlias("mHrsEstmds")]
        public float? HrsEstmds
        {
            get 
            {
                if (!IsLoading && !IsSaving && mHrsEstmds == null)
                    UpdateHrsEstmdsTtl(false);
                return mHrsEstmds; 
            }
            // set { SetPropertyValue("HrsEstmds", ref mHrsEstmds, value); }
        }

        [Persistent("HrsRls")]
        private float? mHrsRls = null;
        [VisibleInListView(false)]
        [XafDisplayName("Horas Reales")]
        [PersistentAlias("mHrsRls")]
        public float? HrsRls
        {
            get
            {
                if (!IsLoading && !IsSaving && mHrsRls == null)
                    UpdateHrsTtl(false);
                return mHrsRls;
            }
        }

        [Persistent("GstTtl")]
        private decimal? mGstTtl = null;
        [VisibleInListView(false)]
        [XafDisplayName("Gasto Total")]
        [PersistentAlias("mGstTtl")]
        public decimal? GstTtl
        {
            get
            {
                if (!IsLoading && !IsSaving && mGstTtl == null)
                    UpdateGstTtl(false);
                return mGstTtl;
            }
        }

        private string mCndcnsPg;
        [VisibleInListView(false)]
        [Size(100)]
        [XafDisplayName("Condiciones de Pago")]
        public string CndcnsPg
        {
            get { return mCndcnsPg; }
            set { SetPropertyValue("CndcnsPg", ref mCndcnsPg, value); }
        }


        private DateTime mFchCnclcn;
        [VisibleInListView(false)]
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        [XafDisplayName("Fecha de Cancelación")]
        public DateTime FchCnclcn
        {
            get { return mFchCnclcn; }
            set { SetPropertyValue("FchCnclcn", ref mFchCnclcn, value); }
        }

        private CatalogoProyecto mMtvCnclcnP;
        [VisibleInListView(false)]
        [XafDisplayName("Motivo Cancelación")]
        [DataSourceCriteria("Tp == 'MotivoCancelacion'")]
        public CatalogoProyecto MtvCnclcnP
        {
            get { return mMtvCnclcnP; }
            set { SetPropertyValue("MtvCnclcnP", ref mMtvCnclcnP, value); }
        }

        private string mCnclcn;
        [VisibleInListView(false)]
        [Size(100)]
        [XafDisplayName("Cancelación")]
        public string Cnclcn
        {
            get { return mCnclcn; }
            set { SetPropertyValue("Cnclcn", ref mCnclcn, value); }
        }

        [Association("Proyecto-Tareas", typeof(Actividad)), DevExpress.Xpo.Aggregated]
        public XPCollection Tareas
        {
            get { return GetCollection("Tareas"); }
        }

        [Association("Proyecto-Gastos", typeof(Gasto)), DevExpress.Xpo.Aggregated]
        public XPCollection Gastos
        {
            get { return GetCollection("Gastos"); }
        }

        [Association("Proyecto-Cxcs", typeof(CxcProyecto)), DevExpress.Xpo.Aggregated]
        public XPCollection Cxcs
        {
            get { return GetCollection("Cxcs"); }
        }        

        /*TI
        [Association("Proyecto-Ingresos", typeof(Ingreso)), DevExpress.Xpo.Aggregated]
        public XPCollection Ingresos
        {
            get { return GetCollection("Ingresos"); }
        }*/

        [Obsolete("En su lugar usar Informacion")]
        [Association("Proyecto-ExpedienteArchivos", typeof(ExpedienteArchivo)), DevExpress.Xpo.Aggregated]
        public XPCollection Archivos
        {
            get { return GetCollection("Archivos"); }
        }

        [Association("Proyecto-Involucrados", typeof(Involucrado)), DevExpress.Xpo.Aggregated]
        public XPCollection Involucrados
        {
            get { return GetCollection("Involucrados"); }
        }

        [Obsolete("Usar mejor Informacion")]
        [Association("Proyecto-Incidencias", typeof(Incidencia)), DevExpress.Xpo.Aggregated]
        public XPCollection Incidencias
        {
            get { return GetCollection("Incidencias"); }
        }

        [Association("Proyecto-Informaciones", typeof(Informacion)), DevExpress.Xpo.Aggregated]
        public XPCollection Informaciones
        {
            get { return GetCollection("Informaciones"); }
        }

        private string mRqrmnt;
        [XafDisplayName("Requerimientos")]
        [Size(SizeAttribute.Unlimited)]
        public string Rqrmnt
        {
            get { return mRqrmnt; }
            set { SetPropertyValue("Rqrmnt", ref mRqrmnt, value); }
        }

        private string mNts;
        [XafDisplayName("Observaciones")]
        [Size(SizeAttribute.Unlimited)]
        public string Nts
        {
            get { return mNts; }
            set { SetPropertyValue("Nts", ref mNts, value); }
        }

        private string mTmpEntrg;
        [Size(SizeAttribute.Unlimited)]
        [XafDisplayName("Tiempo Entrega")]
        public string TmpEntrg
        {
            get { return mTmpEntrg; }
            set { SetPropertyValue("TmpEntrg", ref mTmpEntrg, value); }
        }

        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public string PrcFrmt
        {
            get { return Prc.ToString("c2"); }
        }

        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public string FchFrmt
        {
            get { return FchAlt.ToString("dd/MMM/yyy"); }
        }

        private Cap.Generales.BusinessObjects.Empresa.Empresa mEmpresa;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public Cap.Generales.BusinessObjects.Empresa.Empresa Emprs
        {
            get
            {
                if (mEmpresa == null)
                    mEmpresa = Session.FindObject<Cap.Generales.BusinessObjects.Empresa.Empresa>(null);                 
                return mEmpresa;
            }
        }




        public void UpdateHrsTtl(bool forceChangeEvents)
        {
            float? oldHrsTtl = mHrsRls;
            float? tempTotal = 0;

            foreach (Actividad ac in Tareas)
            {
                tempTotal += ac.HrsRls;
            }
            mHrsRls = tempTotal;

            if (forceChangeEvents)
                OnChanged("HrsRls", oldHrsTtl, mHrsRls);
        }

        public void UpdateHrsEstmdsTtl(bool forceChangeEvents)
        {
            float? oldHrsEstmdsTtl = mHrsEstmds;
            float? tempTotal = 0;

            foreach (Actividad ac in Tareas)
            {
                tempTotal += ac.HrsEstmds;
            }
            mHrsEstmds = tempTotal;

            if (forceChangeEvents)
                OnChanged("HrsEstmds", oldHrsEstmdsTtl, mHrsEstmds);
        }

        public void UpdateGstTtl(bool forceChangeEvents)
        {
            decimal? oldGstTtl = mGstTtl;
            decimal? tempTotal = 0;

            foreach (Gasto gt in Gastos)
            {
                tempTotal += gt.Mnt;
            }
            mGstTtl = tempTotal;

            if (forceChangeEvents)
                OnChanged("GstTtl", oldGstTtl, mGstTtl);
        }



        [RuleFromBoolProperty("Proyecto.FchIniFin", DefaultContexts.Save, "La fecha Final debe ser mayor o igual a la de Inicio")]
        protected bool FchsOk
        {
            get { return (mFchFn == null) ||  (mFchFn >= mFchInc); }
        }

        [RuleFromBoolProperty("Proyecto.Cst", DefaultContexts.Save, "El Costo debe ser mayor o igual a CERO")]
        protected bool CstOk
        {
            get { return mCst >= 0; }
        }

        [RuleFromBoolProperty("Proyecto.Prc", DefaultContexts.Save, "El Precio debe ser mayor o igual a CERO")]
        protected bool PrcOk
        {
            get { return mPrc >= 0; }
        }

        [RuleFromBoolProperty("Proyecto.SttsCncld", DefaultContexts.Save, "Debe capturar la Fecha y el Motivo de Cancelación")]
        protected bool SttsCncldOk
        {
            get { return VvMrt != EEstadoDcmntPryct.Cancelado  || (FchCnclcn.Year > 1 && MtvCnclcnP != null); }
        }


        [Action(ToolTip = "Activa, se ACTIVA de nuevo el Proyecto", 
            TargetObjectsCriteria = "VvMrt == 'Cancelado'")]
        public void ActivaProyecto()
        {
            VvMrt = EEstadoDcmntPryct.Alta;
        }

    }
}