using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using Cap.Generales.BusinessObjects.Object;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Drawing;
using Cap.Generales.BusinessObjects.General;

namespace Cap.Ventas.BusinessObjects
{
    //[NonPersistent]
    //[DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class DescargaMasiva : PObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public DescargaMasiva(Session session)
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
        //    set { SetPropertyValue(nameof(PersistentProperty), ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}

        /*
        [XafDisplayName("Nombre de archivo")]
        public string FlNm { get; set; }*/
        private MyFileData mArchvPfx;
        //[NonCloneable]
        //[VisibleInLookupListView(false)]
        //[VisibleInListView(false)]
        [DevExpress.Xpo.DisplayName("Archivo Pfx")]
        [FileTypeFilter("Pfx", 1, "*.pfx")]
        public MyFileData ArchvPfx
        {
            get { return mArchvPfx; }
            set { SetPropertyValue("ArchvPfx", ref mArchvPfx, value); }
        }

        // Archivo Pfx
        [XafDisplayName("Contraseña")]
        public string Cntrs { get; set; }

        [Appearance("DescargaMasiva.RfcEmsr", Context = "DetailView", Enabled = false,
            FontStyle = FontStyle.Italic)]
        [XafDisplayName("RFC Emisor")]
        public string RfcEmsr { get; set; }

        [Appearance("DescargaMasiva.RfcRcptr", Context = "DetailView", Enabled = false,
            FontStyle = FontStyle.Italic)]
        [XafDisplayName("RFC Receptor")]
        public string RfcRcptr { get; set; }

        private DateTime mFechaIni;
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        [XafDisplayName("Fecha Inicial")]
        public DateTime FchIncl 
        { 
            get { return mFechaIni; }
            set { SetPropertyValue("FchIncl", ref mFechaIni, value); } 
        }

        private DateTime mFechaFin;
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        [XafDisplayName("Fecha Final")]
        public DateTime FcnFnl 
        { 
            get { return mFechaFin; }
            set { SetPropertyValue("FcnFnl", ref mFechaFin, value); } 
        }

        private EFechas mPeriodo;
        [ImmediatePostData]
        public EFechas Periodo
        {
            get { return mPeriodo; }
            set
            {
                mPeriodo = value;
                Fechas.CalculaFechas(value.GetHashCode() + 1, ref mFechaIni, ref mFechaFin);
            }
        }

        private string mSlctd;
        [XafDisplayName("Id Solicitud")]
        [Appearance("DescargaMasiva.Slctd", Context = "DetailView", Enabled = false,
            FontStyle = FontStyle.Italic)]
        public string Slctd 
        { 
            get { return mSlctd; }
            set { SetPropertyValue("Slctd", ref mSlctd, value); }
        }

        private EEstadoSolicitud mEstdSlctd;
        [Appearance("DescargaMasiva.EstdSlctd", Context = "DetailView", Enabled = false,
            FontStyle = FontStyle.Italic)]
        [XafDisplayName("Estado Solicitud")]
        public EEstadoSolicitud EstdSlctd 
        { 
            get { return mEstdSlctd; }
            set { SetPropertyValue("EstdSlctd", ref mEstdSlctd, value); }
        }

        private bool mEmtds;
        [ImmediatePostData]
        [XafDisplayName("Emitidas")]
        public bool Emtds 
        {
            get { return mEmtds; }
            set 
            { 
                SetPropertyValue("Emtds", ref mEmtds, value);
                if (value == true)
                    Rcbds = false;
            }
        }

        private bool mRcbds;
        [ImmediatePostData]
        [XafDisplayName("Recibidas")]
        public bool Rcbds 
        { 
            get { return mRcbds; }
            set 
            { 
                SetPropertyValue("Rcbds", ref mRcbds, value);
                if (value == true)
                    Emtds = false;
            }
        }

        [XafDisplayName("Metadata")]
        public bool Mtdt { get; set; }

        [XafDisplayName("Sólo Descarga Archivos")]
        public bool SlDscrgr { get; set; }
        /*
        private MyFileData mRtDscrg;
        [DevExpress.Xpo.DisplayName("Ruta Descarga")]
        public MyFileData RtDscrg
        {
            get { return mRtDscrg; }
            set { SetPropertyValue("RtDscrg", ref mRtDscrg, value); }
        }*/

        [Appearance("DescargaMasiva.RtDscrg", Context = "DetailView", Enabled = false,
            FontStyle = FontStyle.Italic)]
        [XafDisplayName("Ruta Descarga")]
        public string RtDscrg { get; set; }

    }

    public enum EEstadoSolicitud
    {
        Aceptada = 1,
        EnProceso = 2,
        Terminada = 3,
        Error = 4,
        Rechazada = 5,
        Vencida = 6
    }
}
