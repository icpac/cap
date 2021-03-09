using System;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using Cap.Generales.BusinessObjects.Object;
using SSRV.Module.BusinessObjects.Admin;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Drawing;
using DevExpress.ExpressApp.Editors;
using Cap.Clientes.BusinessObjects.Clientes;
using Cap.Inventarios.BusinessObjects;

namespace SSRV.Module.BusinessObjects.Servicio
{
    [Appearance("Incidencia.Change", Context ="DetailView", Enabled = false, TargetItems= "Plz, HrsUsds", FontStyle = FontStyle.Italic, Criteria = "IsNewObject(This) == false")]
    [Appearance("Incidencia.Wait", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Criteria = "Status != 'ASIGNACION' || Status != 'ESPERA'", TargetItems = "FchInc, TmInc")]
    [Appearance("Incidencia.New", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Method = "IsNewObject", TargetItems = "FchCt, TmCt, FchTrmncn, TimeTrmncn, Partidas, Rspnsbl")] 
    [Appearance("Incidencia.NewD", Context ="DetailView", Enabled = false, Method = "IsNewObject", TargetItems = "TimeSolictd, FchSolctd", FontStyle = FontStyle.Italic)]
    [Appearance("Incidencia.Trmnd", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Criteria = "Status != 'OPERATIVO'", TargetItems = "FchTrmncn, TimeTrmncn")]
    [Appearance("Incidencia.Visible", AppearanceItemType = "LayoutItem", Context ="DetailView", Visibility =ViewItemVisibility.Hide, TargetItems = "Status")]
    [NavigationItem("Servicios")]
    [DefaultClassOptions]
    [DefaultProperty("Folio")]
    //[ImageName("BO_Contact")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Incidencia : PObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Incidencia(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            /*
            FchSolctd = DateTime.Today;*/
            TimeSolictd = DateTime.Now;

            FchCt = DateTime.Today;
            TmCt = DateTime.Now;
            mHrsUsds = 1;
            //Status = StsSrvc.ASIGNACION;
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
        [RuleRequiredField("RuleRequiredField for Incidencia.Clnt", DefaultContexts.Save, "Debe capturar el Cliente", SkipNullOrEmptyValues = false)]
        [XafDisplayName("Cliente")]
        [ImmediatePostData]
        public Cliente Clnt
        {
            get { return mClnt; }
            set
            {
                /*
                if (mClnt != value)
                {*/
                    if (!IsLoading)
                    {
                    // Refresh the Accessory Property data source 
                    SetPropertyValue("Clnt", ref mClnt, value);
                    RefreshAvailablePolizas();
                    }
                    /*
                }*/
            }
        }

        private Poliza mPlz;
        [VisibleInListView(false)]
        [ImmediatePostData]
        [DataSourceProperty("AvailablePolizas")]
        [XafDisplayName("Póliza")]
        public Poliza Plz
        {
            get { return mPlz; }
            set
            {
                /*
                if (mPlz != value)
                {*/
                    SetPropertyValue("Plz", ref mPlz, value);

                    /* TIT Mrz 2020 Todavía no lo uso
                    if (!IsLoading && value != null)
                    {
                        // Refresh the Accessory Property data source 
                        RefreshAvailableServicios();
                        if (mClnt == null || (mPlz != null && mClnt != mPlz.Clnt))
                            mClnt = mPlz.Clnt;
                    }*/
                    /*
                }*/
            }
        }

        private Servicio mSrvc;
        [Obsolete("En su lugar usar Prdct")]
        [XafDisplayName("Servicio")]
        public Servicio Srvc
        {
            get { return mSrvc; }
            set { SetPropertyValue("Srvc", ref mSrvc, value); }
        }

        private Producto mPrdct;
        [Obsolete("Todavía no sé si es necesario !")]
        [DataSourceProperty("AvailableServicios")]
        [XafDisplayName("Servicio")]
        public Producto Prdct
        {
            get { return mPrdct; }
            set { SetPropertyValue("Prdct", ref mPrdct, value); }
        }

        private DateTime mFchSolctd;
        [Obsolete("Todavía no sé si lo usaré")]
        [DevExpress.Xpo.DisplayName("Fecha de Solicitud")]
        //[Appearance("SFchSolctd", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        // [ModelDefault("DisplayFormat", "{0:dd MMM yyyyTHH:mm:ss}")]
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        public DateTime FchSolctd
        {
            get { return mFchSolctd; }
            set { SetPropertyValue("FchSolctd", ref mFchSolctd, value); }
        }

        private DateTime mtimeSolctd;
        [Obsolete("Todavía no sé si lo usaré")]
        // [Obsolete("En su lugar mejor usar TmpSlctd")]
        [DevExpress.Xpo.DisplayName("Hora de Solicitud")]
        [VisibleInListView(false)]
        //[Appearance("STimeSolctd", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        [ModelDefault("PropertyEditorType", "SSRV.Module.Web.Editors.ASPxTimePropertyEditor")]
        public DateTime TimeSolictd
        {
            get { return mtimeSolctd; }
            set { SetPropertyValue<DateTime>("TimeSolictd", ref mtimeSolctd, value); }
        }

        /*
        private TimeSpan mTmpSlctd;
        [DevExpress.Xpo.DisplayName("Hora de Solicitud")]
        [VisibleInListView(false)]
        //[Appearance("STimeSolctd", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        [ModelDefault("PropertyEditorType", "SSRV.Module.Web.Editors.ASPxTimePropertyEditor")]
        public TimeSpan TmpSlctd
        {
            get { return mTmpSlctd; }
            set { SetPropertyValue("TmpSlctd", ref mTmpSlctd, value); }
        }*/

        private string mFolio;
        [Size(10)]
        [Appearance("SFolio", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        // [Appearance("SFolio", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Method = "IsCliente")]
        public string Folio
        {
            get { return mFolio; }
            set { SetPropertyValue("Folio", ref mFolio, value); }
        }

        private StsSrvc? mStatus;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        // [Appearance("SNotas", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Method = "IsCliente")]
        [Appearance("Incidencia.Status", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        public StsSrvc? Status
        {
            get { return mStatus; }
            set { SetPropertyValue("Status", ref mStatus, value); }
        }

        private Empleado mRspnsbl;
        [Obsolete("Todavía no sé si lo usaré")]
        [XafDisplayName("Responsable")]
        public Empleado Rspnsbl
        {
            get { return mRspnsbl; }
            set { SetPropertyValue("Rspnsbl", ref mRspnsbl, value); }
        }

        // Cambiamos FchCita  a  FchAtncn
        private DateTime mFchCita;
        [Obsolete("Cambiar a fecha de atención")]
        // Si se me olvida capturarla (?)
        [DevExpress.Xpo.DisplayName("Fecha de Atención")]
        // [Appearance("Incidencia.FchCt", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Method = "IsCliente")]
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        [ModelDefault("EditMask", "d-MM-yyyy")]
        public DateTime FchCt
        {
            get { return mFchCita; }
            set { SetPropertyValue("FchCt", ref mFchCita, value); }
        }

        private DateTime mtimeCita;
        [Obsolete("Cambiar a fecha de atención")]
        [ModelDefault("PropertyEditorType", "SSRV.Module.Web.Editors.ASPxTimePropertyEditor")]
        [ImmediatePostData]
        [VisibleInListView(false)]
        // [Appearance("SHrCit", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Method = "IsCliente")]
        [DevExpress.Xpo.DisplayName("Hora de Atención")]
        public DateTime TmCt
        {
            get { return mtimeCita; }
            set
            {
                SetPropertyValue("TmCt", ref mtimeCita, value);
                if (!IsLoading && FchCt != null)
                    FchCt = new DateTime(FchCt.Year, FchCt.Month, FchCt.Day, value.Hour, value.Minute, 0);
            }
        }

        private DateTime mFchAtncn;
        // Si se me olvida capturarla (?)
        [DevExpress.Xpo.DisplayName("Fecha de Atención")]
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        [ModelDefault("EditMask", "d-MM-yyyy")]
        public DateTime FchAtncn
        {
            get { return mFchAtncn; }
            set { SetPropertyValue("FchAtncn", ref mFchAtncn, value); }
        }


        private DateTime mTmAtncn;
        [ModelDefault("PropertyEditorType", "SSRV.Module.Web.Editors.ASPxTimePropertyEditor")]
        [ImmediatePostData]
        [VisibleInListView(false)]
        // [Appearance("SHrCit", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Method = "IsCliente")]
        [DevExpress.Xpo.DisplayName("Hora de Atención")]
        public DateTime TmAtncn
        {
            get { return mTmAtncn; }
            set
            {
                SetPropertyValue("TmCt", ref mTmAtncn, value);
                if (!IsLoading && FchAtncn != null)
                    FchAtncn = new DateTime(FchAtncn.Year, FchAtncn.Month, FchAtncn.Day, 
                        value.Hour, value.Minute, 0);
            }
        }

        private DateTime mFchInc;
        [VisibleInListView(false)]
        [Appearance("Incidencia.FchInc", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        [DevExpress.Xpo.DisplayName("Fecha de Inicio")]
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        [ModelDefault("EditMask", "d-MM-yyyy")]
        public DateTime FchInc
        {
            get { return mFchInc; }
            set { SetPropertyValue("FchInc", ref mFchInc, value); }
        }

        private DateTime mtimeInc;
        [Appearance("Incidencia.TmInc", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        [ModelDefault("PropertyEditorType", "SSRV.Module.Web.Editors.ASPxTimePropertyEditor")]
        [VisibleInListView(false)]
        [DevExpress.Xpo.DisplayName("Hora de Incio")]
        public DateTime TmInc
        {
            get { return mtimeInc; }
            set
            {
                SetPropertyValue("TmInc", ref mtimeInc /*mtimeCita*/, value);
                if (!IsLoading && FchInc != null)
                    FchInc = new DateTime(FchInc.Year, FchInc.Month, FchInc.Day, value.Hour, value.Minute, 0);
            }
        }

        private DateTime mFchTrmncn;
        [VisibleInListView(false)]
        [DevExpress.Xpo.DisplayName("Fecha de Terminación")]
        //[Appearance("SFchSolctd", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyyTHH:mm:ss}")]
        public DateTime FchTrmncn
        {
            get { return mFchTrmncn; }
            set { SetPropertyValue("FchTrmncn", ref mFchTrmncn, value); }
        }

        private DateTime mTimeTrmncn;
        [DevExpress.Xpo.DisplayName("Hora de Terminación")]
        [VisibleInListView(false)]
        //[Appearance("STimeSolctd", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        [ModelDefault("PropertyEditorType", "SSRV.Module.Web.Editors.ASPxTimePropertyEditor")]
        public DateTime TimeTrmncn
        {
            get { return mTimeTrmncn; }
            set { SetPropertyValue<DateTime>("TimeTrmncn", ref mTimeTrmncn, value); }
        }

        private string mFll;
        [EditorAlias(EditorAliases.RichTextPropertyEditor)]
        [VisibleInListView(true)]
        [XafDisplayName("Falla (Situación)")]
        [RuleRequiredField("RuleRequiredField for Incidencia.Falla", DefaultContexts.Save, "Debe capturar la Falla", SkipNullOrEmptyValues = false)]
        [Size(SizeAttribute.Unlimited)]
        public string Fll
        {
            get { return mFll; }
            set { SetPropertyValue("Fll", ref mFll, value); }
        }

        private EPRIORITY mPrrdd;
        [Obsolete("Todavía no sé si debe ir")]
        [XafDisplayName("Prioridad")]
        public EPRIORITY Prrdd
        {
            get { return mPrrdd; }
            set { SetPropertyValue("Prrdd", ref mPrrdd, value); }
        }

        [DevExpress.Xpo.DisplayName("Bitácora")]
        [Obsolete("Todavía no sé si lo usaré")]
        //[Appearance("SPartidas", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Method = "IsNuevo")]
        [Association("Incidencia-Bitacoras", typeof(Bitacora)), DevExpress.Xpo.Aggregated]
        public XPCollection Partidas
        {
            get { return GetCollection("Partidas"); }
        }

        private Incidencia mAntrr;
        [VisibleInListView(false)]
        [Appearance("Incidencia.Antrr", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Criteria = "Antrr = null")]
        [Appearance("SAntrr", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        [DevExpress.Xpo.DisplayName("Reporte Anterior")]
        public Incidencia Antrr
        {
            get { return mAntrr; }
            set { SetPropertyValue("Antrr", ref mAntrr, value); }
        }

        [VisibleInListView(false)]
        [NonPersistent]
        [XafDisplayName("Horas Restantes")]
        public float HrsRstnts
        {
            get
            {
                if (mPlz != null)
                {
                    foreach (PolizaItem pi in mPlz.PolizaItems)
                        if (pi.Srvc == Prdct)
                            return Convert.ToSingle(pi.Cntdd);

                    return mPlz.HrsRstnts; // return 0;
                }
                else
                    return 0;
            }
        }

        private float mHrsUsds;
        [XafDisplayName("Horas Usadas")]
        public float HrsUsds
        {
            get { return mHrsUsds; }
            set { SetPropertyValue("HrsUsds", ref mHrsUsds, value); }
        }

        private string mSlcn;
        [VisibleInListView(true)]
        [XafDisplayName("Solución")]
        [Size(SizeAttribute.Unlimited)]
        public string Slcn
        {
            get { return mSlcn; }
            set { SetPropertyValue("Slcn", ref mSlcn, value); }
        }





        private XPCollection<Poliza> availablePolizas;
        [Browsable(false)] // Prohibits showing the AvailableTasks collection separately 
        public XPCollection<Poliza> AvailablePolizas
        {
            get
            {
                if (availablePolizas == null)
                {
                    // Retrieve all Task objects 
                    availablePolizas = new XPCollection<Poliza>(Session);
                    // Filter the retrieved collection according to the current conditions 
                    RefreshAvailablePolizas();
                }
                // Return the filtered collection of Task objects 
                return availablePolizas;
            }
        }

        private void RefreshAvailablePolizas()
        {
            if (availablePolizas == null)
                return;

            if (Clnt == null)
            {
                //Remove the applied filter 
                availablePolizas.Criteria = null;
            }
            else
            {
                //Filter the collection 
                GroupOperator filt = new GroupOperator();
                filt.Operands.Add(new BinaryOperator("Clnt", Clnt));
                filt.Operands.Add(new BinaryOperator("Stts", ESTTSPLZ.Activa));
                filt.Operands.Add(new BinaryOperator("HrsRstnts", 0, BinaryOperatorType.Greater));
                availablePolizas.Criteria = filt;
            }
            // Plz = null;
        }


        private XPCollection<Producto> availableServicios;
        [Browsable(false)] // Prohibits showing the AvailableTasks collection separately 
        public XPCollection<Producto> AvailableServicios
        {
            get
            {
                if (availableServicios == null)
                {
                    // Retrieve all Task objects 
                    availableServicios = new XPCollection<Producto>(Session);
                    // Filter the retrieved collection according to the current conditions 
                    RefreshAvailableServicios();
                }
                // Return the filtered collection of Task objects 
                return availableServicios;
            }
        }

        private void RefreshAvailableServicios()
        {
            if (availableServicios == null)
                return;
            if (Plz == null)
            {
                //Remove the applied filter 
                availableServicios.Criteria = null;
                int len = availableServicios.Count;
                for (int i = 0; i < len; i++)
                    availableServicios.Remove(availableServicios[0]);
            }
            else
            {
                availableServicios.Criteria = null;
                int len = availableServicios.Count;
                for (int i = 0; i < len; i++)
                    availableServicios.Remove(availableServicios[0]);

                foreach (PolizaItem pi in Plz.PolizaItems)
                {
                    availableServicios.Add(pi.Srvc);
                    /*
                    //Filter the collection 
                    GroupOperator filt = new GroupOperator();
                    filt.Operands.Add(new BinaryOperator("Clnt", Clnt));
                    availablePolizas.Criteria = filt;*/
                }
            }
        }


        [RuleFromBoolProperty("Incidencia_HrsUsdsOk", DefaultContexts.Save, 
            " El número de horas Usadas debe ser  MENOR  O  IGUAL  al de horas disponibles" +
            " O debe ser mayor que  CERO")]
        protected bool Incidencia_HrsUsdsOk
        {
            get { return (mPlz == null && mHrsUsds > 0) 
                    || (mPlz != null && (mHrsUsds > 0 && mHrsUsds <= mPlz.HrsRstnts)); }
        }


        protected override void OnSaving()
        {
            if (Status == null)
                Status = StsSrvc.ASIGNACION;
            base.OnSaving();
        }
    }

    public enum StsSrvc
    {
        [XafDisplayName("ESPERA ASIGNACIÓN DE HORARIO DE ATENCIÓN")]
        ASIGNACION,
        [XafDisplayName("ESPERA QUE SE ATIENDA"), ImageName("State_task_notstarted")]
        ESPERA,
        [XafDisplayName("ATENCIÓN EN CURSO"), ImageName("State_task_inprogress")]
        SITIO,
        [XafDisplayName("PENDIENTE"), ImageName("State_Task_WaitingForSomeoneElse")]
        PENDIENTE,
        [XafDisplayName("INCIDENCIA RESUELTA"), ImageName("State_task_completed")]
        OPERATIVO,
        [XafDisplayName("ESPERA AUTORIZACIÓN")]
        AUTORIZACION,
        [XafDisplayName("SERVICIO CANCELADO")]
        CANCELADO,
        TODOS
    }

    public enum EPRIORITY
    {
        [ImageName("State_priority_high")]
        Alta,
        [ImageName("State_priority_normal")]
        Media,
        [ImageName("State_priority_low")]
        Baja
    }
}