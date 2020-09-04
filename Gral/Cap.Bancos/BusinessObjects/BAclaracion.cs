#region 2020-2020 TIT
/*
{+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++)
{                                                                   }
{     Cap control administrativo personal                           }
{                                                                   }
{     2020-2020 tit                                                 }
{     tlacaelel.icpac@gmail.com                                     }
{                                                                   }
{*******************************************************************}
 */
#endregion

using System;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using Cap.Generales.BusinessObjects.Object;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace Cap.Bancos.BusinessObjects
{
    [Appearance("Aclaracion.Espera", TargetItems = "Estd", Context = "ListView", Criteria = "[Estd] == 'Espera'", FontColor = "Red")]
    [Appearance("Aclaracion.NoAtendida", TargetItems = "Estd", Context = "ListView", Criteria = "[Estd] == 'NoAtendida'", FontColor = "Green")]
    [XafDisplayName("Aclaracion")]
    //[DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class BAclaracion : PObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public BAclaracion(Session session)
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


        private Bancaria mCnt;
        [Association("Bancaria-Aclaraciones")]
        public Bancaria Cnt
        {
            get { return mCnt; }
            set { SetPropertyValue("Cnt", ref mCnt, value); }
        }

        private DateTime mFch;
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        [XafDisplayName("Fecha")]
        public DateTime Fch
        {
            get { return mFch; }
            set { SetPropertyValue("Fch", ref mFch, value); }
        }

        private string mFl;
        [XafDisplayName("Folio")]
        [Size(20)]
        public string Fl
        {
            get { return mFl; }
            set { SetPropertyValue("Fl", ref mFl, value); }
        }

        private string mAtncn;
        [XafDisplayName("Atención")]
        [Size(30)]
        public string Atncn
        {
            get { return mAtncn; }
            set { SetPropertyValue("Atncn", ref mAtncn, value); }
        }

        private EAtencion mEstd;
        [XafDisplayName("Estado")]
        public EAtencion Estd
        {
            get { return mEstd; }
            set { SetPropertyValue("Estd", ref mEstd, value); }
        }

        private string mNts;
        [XafDisplayName("Notas")]
        [Size(SizeAttribute.Unlimited)]
        public string Nts
        {
            get { return mNts; }
            set { SetPropertyValue("Nts", ref mNts, value); }
        }

        private MovimientoB mMvmnt;
        [DataSourceProperty("Movimientos")]
        [XafDisplayName("Movimiento")]
        public MovimientoB Mvmnt
        {
            get { return mMvmnt; }
            set { SetPropertyValue("Mvmnt", ref mMvmnt, value); }
        }


        private XPCollection<MovimientoB> movimientos;
        [Browsable(false)] // Prohibits showing the collection separately 
        public XPCollection<MovimientoB> Movimientos
        {
            get
            {
                if (movimientos == null)
                    movimientos = new XPCollection<MovimientoB>(Session, false);

                if (movimientos != null)
                {
                    if (Cnt != null)
                    {
                        movimientos.Criteria = new BinaryOperator("Cuenta.Oid", Cnt.Oid);
                        movimientos.LoadingEnabled = true;
                    }
                    else
                    {
                        movimientos.Criteria = new BinaryOperator("Cuenta.Oid", -1);
                        //movimientos.LoadingEnabled = true;
                    }
                }

                return movimientos;
            }
        }
    }

    public enum EAtencion
    {
        Espera,
        Terminada,
        NoAtendida
    }
}
