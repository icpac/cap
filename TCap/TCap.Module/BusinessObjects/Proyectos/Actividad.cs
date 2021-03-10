using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using Cap.Generales.BusinessObjects.Object;
using TCap.Module.BusinessObjects.Empresa;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using System.Drawing;
using DevExpress.Persistent.Base.General;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.ExpressApp.SystemModule.Notifications;
using DevExpress.ExpressApp.Filtering;
using System.Linq;
using DevExpress.ExpressApp.SystemModule;

namespace TCap.Module.BusinessObjects.Proyectos
{
    // [ListViewFilter("Todas", "")]
    [ListViewFilter("Mis Actividades", "[Asgnd.Oid] = CurrentUserId()" +
        "AND [Pryct.VvMrt] <> 'Cancelado'")]    
    // [Hint(Hints.TaskNotificationsHint)]
    [Appearance("Actividad.SttsEjecucionA", TargetItems = "Stts", Context = "ListView", Criteria = "[Stts] == 'Ejecución' && [Prrdd] == 'Alta'", FontColor = "Green")]
    [Appearance("Actividad.SttsEjecucion", TargetItems = "Stts", Context = "ListView", Criteria = "[Stts] == 'Ejecución' && [Prrdd] != 'Alta'", FontColor = "Olive")]
    [Appearance("Actividad.SttsEsperaA", TargetItems = "Stts", Context = "ListView", Criteria = "[Stts] == 'Espera' && [Prrdd] == 'Alta'", FontColor = "Red")]
    [Appearance("Actividad.SttsEspera", TargetItems = "Stts", Context = "ListView", Criteria = "[Stts] == 'Espera' && [Prrdd] != 'Alta'", FontColor = "Orange")]
    [Appearance("Actividad.Completa", AppearanceItemType = "ViewItem", Context = "DetailView", TargetItems = "*", Enabled = false, Criteria = "[Stts] == 'Completada'")]
    [Appearance("Actividad.Alta", TargetItems = "Pryct", Context = "ListView", Criteria = "[Prrdd] == 'Alta'", FontColor = "Red")]
    [Appearance("Actividad.New", AppearanceItemType = "LayoutItem", Context = "DetailView", Criteria = "IsNewObject(This)", Visibility = ViewItemVisibility.Hide, TargetItems = "Incidencias")]
    [NavigationItem("Proyectos")]
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Actividad : PObject, ISupportNotifications
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Actividad(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).

            FchAlt = DateTime.Now;
            Stts = TASKSTATUS.Espera;
            FchInc = DateTime.Now;
            Infrmcn = new Informacion(Session);
            /*
            Infrmcn.Pryct = Pryct;*/
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



        private Proyecto mPryct;
        [Association("Proyecto-Tareas")]
        public Proyecto Pryct
        {
            get { return mPryct; }
            set 
            { 
                Proyecto oldPryct = mPryct;
                SetPropertyValue("Pryct", ref mPryct, value);
                if (!IsLoading && !IsSaving && oldPryct != mPryct)
                {
                    oldPryct = oldPryct ?? mPryct;
                    oldPryct.UpdateHrsEstmdsTtl(true);
                }
                if (!IsLoading && Infrmcn != null && Infrmcn.Pryct == null)
                    Infrmcn.Pryct = value;
            }
        }

        private string mAsnt;
        [RuleRequiredField("RuleRequiredField for Actividad.Asnt", DefaultContexts.Save, "Debe capturar el Asunto", SkipNullOrEmptyValues = false)]
        [Size(150)]
        [XafDisplayName("Asunto")]
        public string Asnt
        {
            get { return mAsnt; }
            set { SetPropertyValue("Asnt", ref mAsnt, value); }
        }

        private TASKSTATUS mStts;
        [XafDisplayName("Status")]
        public TASKSTATUS Stts
        {
            get { return mStts; }
            set { SetPropertyValue("Stts", ref mStts, value); }
        }

        private EmpleadoProyecto mAsgnd;
        [XafDisplayName("Asignado A")]
        public EmpleadoProyecto Asgnd
        {
            get { return mAsgnd; }
            set { SetPropertyValue("Asgnd", ref mAsgnd, value); }
        }

        private DateTime mFchAlt;
        [Appearance("Actividad.FchAlt", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        [XafDisplayName("Fecha Alta")]
        public DateTime FchAlt
        {
            get { return /*mFchAlt*/ task.StartDate; }
            set
            {
                DateTime oldValue = task.StartDate;
                task.StartDate = value;
                OnChanged("FchAlt", oldValue, task.StartDate);
                if (!IsLoading && oldValue != value && remindIn != null)
                {
                    SetAlarmTime(value, remindIn.Value);
                }

                // SetPropertyValue("FchAlt", ref mFchAlt, value);
            }
        }

        private TaskImpl task = new TaskImpl();
        private DateTime mFchInc;
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        [XafDisplayName("Fecha de Inicio")]
        public DateTime FchInc
        {
            get { return mFchInc; }
            set { SetPropertyValue("FchInc", ref mFchInc, value); }
        }

        private DateTime? mFchFn = null;
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        [XafDisplayName("Fecha de Término")]
        public DateTime? FchFn
        {
            get { return mFchFn; }
            set { SetPropertyValue("FchFn", ref mFchFn, value); }
        }

        private float mHrsEstmds;
        [XafDisplayName("Horas Estimadas")]
        public float HrsEstmds
        {
            get { return mHrsEstmds; }
            set 
            { 
                SetPropertyValue("HrsEstmds", ref mHrsEstmds, value);
                if (!IsLoading && !IsSaving && Pryct != null)
                {
                    Pryct.UpdateHrsEstmdsTtl(true);
                }
            }
        }

        [Persistent("HrsRls")]
        private float? mHrsRls = null;
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

        private EPRIORITY mPrrdd;
        [XafDisplayName("Prioridad")]
        public EPRIORITY Prrdd
        {
            get { return mPrrdd; }
            set { SetPropertyValue("Prrdd", ref mPrrdd, value); }
        }

        /*
        private Catalogo mTp;
        [Obsolete("En su lugar usar TpP")]
        [DataSourceCriteria("Tipo == 'Actividad'")]
        [XafDisplayName("Tipo")]
        public Catalogo Tp
        {
            get { return mTp; }
            set { SetPropertyValue("Tp", ref mTp, value); }
        }*/

        private CatalogoProyecto mTpP;
        [DataSourceCriteria("Tp == 'Actividad'")]
        [XafDisplayName("Tipo")]
        public CatalogoProyecto TpP
        {
            get { return mTpP; }
            set { SetPropertyValue("TpP", ref mTpP, value); }
        }

        private string _Text;
        [EditorAlias("RTF")]
        [Size(SizeAttribute.Unlimited)]
        [XafDisplayName("Notas")]
        public string Nts
        {
            get
            {
                /*
                RichEditDocumentServer server = new RichEditDocumentServer();
                server.Text = value;
                return server.RtfText*/
                if (_Text == null) return _Text;
                if (_Text.StartsWith(@"{\rtf"))
                    return _Text;
                else return TextHelper.GetRTFText(_Text);
                // return mNts;
            }
            set
            {
                if(value != null && value.StartsWith(@"{\rtf"))
                   SetPropertyValue("Text", ref _Text, value);
               else
                   SetPropertyValue("Text", ref _Text, TextHelper.GetRTFText(value));                
                //SetPropertyValue("Nts", ref mNts, value);
            }
        }

        // Porque en gueb listview pone rtf editor y ocupa mucho espacio.
        string mNts;
        [XafDisplayName("Observaciones")]
        [Size(SizeAttribute.Unlimited)]
        public string Obsrvcns
        {
            get { return mNts; }
            set { SetPropertyValue("Obsrvcns", ref mNts, value); }
        }

        //[Obsolete("Usar mejor Informacion")]
        [Association("Actividad-Incidencias", typeof(Incidencia)), DevExpress.Xpo.Aggregated]
        public XPCollection Incidencias
        {
            get { return GetCollection("Incidencias"); }
        }

        [VisibleInListView(false)]
        //[Obsolete("Usar mejor Informacion")]
        //[Appearance("Actividad.Archivos", AppearanceItemType = "LayoutItem", Context = "DetailView", Criteria = "Archivos.Count = 0", Visibility = ViewItemVisibility.Hide)]
        [Association("Actividad-ExpedienteArchivos", typeof(ExpedienteArchivo)), DevExpress.Xpo.Aggregated]
        public XPCollection Archivos
        {
            get { return GetCollection("Archivos"); }
        }

        // private EADJUNTO mArchvs;
        [XafDisplayName(" ")]
        [VisibleInDetailView(false)]
        public EADJUNTO Archvs
        {
            get { return Archivos == null || Archivos.Count == 0 ? EADJUNTO.NoTiene : EADJUNTO.Tiene; }
            //set { SetPropertyValue("Archvs", ref mArchvs, value); }
        }





        private Informacion mInfrmcn;
        [DevExpress.Xpo.Aggregated]
        [ExpandObjectMembers(ExpandObjectMembers.Always)]
        [XafDisplayName("Informacion")]
        public Informacion Infrmcn
        {
            get { return mInfrmcn; }
            set { SetPropertyValue("Infrmcn", ref mInfrmcn, value); }
        }


        private DateTime? mFchVncmnt = null;
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        [XafDisplayName("Fecha de Vencimiento")]
        public DateTime? FchVncmnt
        {
            get { return task.DueDate.Year == 1 ? mFchVncmnt : task.DueDate; }
            set
            {
                DateTime? oldValue = task.DueDate;
                task.DueDate = Convert.ToDateTime(value);
                OnChanged("DueDate", oldValue, task.DueDate);
            }
        }

        [ImmediatePostData]
        [NonPersistent]
        [ModelDefault("AllowClear", "False")]
        [DataSourceProperty("PostponeTimeList")]
        [SearchMemberOptions(SearchMemberMode.Exclude)]
        public PostponeTime ReminderTime
        {
            get
            {
                if (RemindIn.HasValue)
                {
                    return PostponeTimeList.Where(x => (x.RemindIn != null && x.RemindIn.Value == remindIn.Value)).FirstOrDefault();
                }
                else
                {
                    return PostponeTimeList.Where(x => x.RemindIn == null).FirstOrDefault();
                }
            }
            set
            {
                if (!IsLoading)
                {
                    if (value.RemindIn.HasValue)
                    {
                        RemindIn = value.RemindIn.Value;
                    }
                    else
                    {
                        RemindIn = null;
                    }
                }
            }
        }

        private IList<PostponeTime> CreatePostponeTimes()
        {
            IList<PostponeTime> result = PostponeTime.CreateDefaultPostponeTimesList();
            result.Add(new PostponeTime("None", null, "None"));
            result.Add(new PostponeTime("AtStartTime", TimeSpan.Zero, "At Start Time"));
            PostponeTime.SortPostponeTimesList(result);
            return result;
        }

        private IList<PostponeTime> postponeTimes;
        [Browsable(false), NonPersistent]
        public IEnumerable<PostponeTime> PostponeTimeList
        {
            get
            {
                if (postponeTimes == null)
                {
                    postponeTimes = CreatePostponeTimes();
                }
                return postponeTimes;
            }
        }

        private TimeSpan? remindIn;
        [Browsable(false)]
        public TimeSpan? RemindIn
        {
            get { return remindIn; }
            set
            {
                SetPropertyValue("RemindIn", ref remindIn, value);
                if (!IsLoading)
                {
                    if (value != null)
                    {
                        SetAlarmTime(FchVncmnt, value.Value);
                    }
                    else
                    {
                        alarmTime = null;
                    }
                }
            }
        }

        private void SetAlarmTime(DateTime? startDate, TimeSpan remindTime)
        {
            alarmTime = ((startDate - DateTime.MinValue) > remindTime) ? startDate - remindTime : DateTime.MinValue;
        }

        private DateTime? alarmTime;
        [Browsable(false)]
        public DateTime? AlarmTime
        {
            get { return alarmTime; }
            set
            {
                SetPropertyValue("AlarmTime", ref alarmTime, value);
                if (value == null)
                {
                    remindIn = null;
                    IsPostponed = false;
                }
            }
        }

        [NonPersistent]
        [Browsable(false)]
        public object UniqueId
        {
            get { return Oid; }
        }

        [Browsable(false)]
        [NonPersistent]
        public string NotificationMessage
        {
            get { return Asnt; }
        }

        [Browsable(false)]
        public bool IsPostponed
        {
            get;
            set;
        }




        public void UpdateHrsTtl(bool forceChangeEvents)
        {
            float? oldHrsTtl = mHrsRls;
            float tempTotal = 0;

            foreach (Incidencia ac in Incidencias)
            {
                tempTotal += ac.Hrs;
            }
            mHrsRls = tempTotal;

            if (forceChangeEvents)
                OnChanged("HrsRls", oldHrsTtl, mHrsRls);

            if (Pryct != null)
                Pryct.UpdateHrsTtl(forceChangeEvents);
        }



        [RuleFromBoolProperty("Actividad.FchIniFin", DefaultContexts.Save, "La fecha Final debe ser mayor o igual a la de Inicio")]
        protected bool FchsOk
        {
            get { return mFchFn == null || (mFchFn >= mFchInc); }
        }

        [RuleFromBoolProperty("Actividad.HrsEstmds", DefaultContexts.Save, "El tiempo estimado debe ser mayor o igual a CERO")]
        protected bool HrsEstmdsOk
        {
            get { return mHrsEstmds >= 0; }
        }
    }

    public enum EADJUNTO
    {
        [ImageName("Attachment"), XafDisplayName("")]
        Tiene,
        [XafDisplayName("")]
        NoTiene
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

    public enum TASKSTATUS
    {
        [ImageName("State_task_notstarted")]
        Espera,
        [ImageName("State_task_inprogress")]
        Ejecución,
        [ImageName("State_task_completed")]
        Completada,
        [ImageName("State_Task_WaitingForSomeoneElse")]
        Suspendida
    }


    /*
    public class TextHelper
    {
        public static string GetRTFText(string text)
        {
            RichEditDocumentServer serv = new RichEditDocumentServer();
            serv.Text = text;

            serv.Document.DefaultCharacterProperties.FontName = "Times New Roman";
            serv.Document.DefaultCharacterProperties.FontSize = 12;
            return serv.RtfText;
        }
    }*/
}