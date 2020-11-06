#region Copyright (c) 2016-2020 cjlc
/*
{+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++)
{                                                                   }
{     cjlc Cap control administrativo personal                      }
{                                                                   }
{     Copyrigth (c) 2016-2020 cjlc                                  }
{     Todos los derechos reservados                                 }
{                                                                   }
{*******************************************************************}
 */
#endregion

using System;
using Cap.Clientes.BusinessObjects.Generales;
using Cap.Generales.BusinessObjects.Empresa;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using apl.Log;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using FCE;

namespace Cap.Clientes.BusinessObjects.Clientes
{
    [NavigationItem("Clientes")]
    [Appearance("Suspendido", TargetItems = "*", Context = "ListView", Criteria = "[Status] == 'Suspendido'", FontColor = "Green"/*, FontStyle = FontStyle.Strikeout*/)]
    [ImageName("BO_Customer")]
    [DefaultClassOptions]
    //[NonPersistent]
    //[Appearance("Suspendido", TargetItems = "*", Context = "ListView", Criteria = "[Status] == 4", FontColor = "Green")]
    //[ImageName("BO_Customer")]
    public partial class Cliente
    {
        private string FClave;
        [VisibleInLookupListView(true)]
        [RuleRequiredField("RuleRequiredField for ProveedorCliente.Clave", DefaultContexts.Save, "Debe capturar la Clave", SkipNullOrEmptyValues = false)]
        [Size(LONCLAVE), Indexed(Unique = true)]
        public string Clave
        {
            get { return FClave; }
            set
            {
                if (value != null)
                {
                    if (IsLoading)
                    {
                        SetPropertyValue("Clave", ref FClave, value);
                    }
                    else
                    {
                        SetPropertyValue("Clave", ref FClave, value.ToUpper());

                        if (Cadena.IsNumber(FClave))
                            SetPropertyValue("Clave", ref FClave, FClave.PadLeft(LONCLAVE, ' '));
                    }
                }
            }
        }

        private EDiasSemana FDiasRevision;
        [DisplayName("Días Revisión")]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public EDiasSemana DiasRevision
        {
            get { return FDiasRevision; }
            set { SetPropertyValue("DiasRevision", ref FDiasRevision, value); }
        }

        private EDiasSemana FDiasPago;
        [DisplayName("Días Pago")]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public EDiasSemana DiasPago
        {
            get { return FDiasPago; }
            set { SetPropertyValue("DiasPago", ref FDiasPago, value); }
        }

        //#region + Addenda file
        /* Si se usa tal vez hay que agregar un FileData
        private string mFileAdd;
        // Addenda+Clave
        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [Size(100)]
        public string FileAdd
        {
            get { return mFileAdd; }
            set { SetPropertyValue("FileAdd", ref mFileAdd, value); }
        }*/
        //#endregion

        //#region + Formas de Pago
        [Obsolete("Parece que ya no se va usar 3.3")]
        [XafDisplayName("Formas de Pago")]
        [Association("Cliente-FormasP", typeof(ItemFormaP)), DevExpress.Xpo.Aggregated]
        public XPCollection FormasP
        {
            get { return GetCollection("FormasP"); }
        }

        private Clasificacion FClasifica;
        [VisibleInListView(false)]
        [XafDisplayName("Clasificación")]
        [VisibleInLookupListView(false)]
        [DataSourceCriteria("Tipo == 1")]
        public new Clasificacion Clasifica
        {
            get { return FClasifica; }
            set { SetPropertyValue("Clasifica", ref FClasifica, value); }
        }

        private Vendedor FVendedor;
        [DataSourceCriteria("Status != 'Baja'")]  // Estaba en 4 ! pero Baja = 2
        [Appearance("Vendedor", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Method = "NoHayVend")]
        [VisibleInListView(false)]
        public Vendedor Vendedor
        {
            get { return FVendedor; }
            set { SetPropertyValue("Vendedor", ref FVendedor, value); }
        }

        private string mNmrPrvdr;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public string NmrPrvdr
        {
            get { return mNmrPrvdr; }
            set { SetPropertyValue("NmrPrvdr", ref mNmrPrvdr, value); }
        }

        private EAddenda mTpAddnd;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public EAddenda TpAddnd
        {
            get { return mTpAddnd; }
            set { SetPropertyValue("TpAddnd", ref mTpAddnd, value); }
        }

        /*
        private string mAddndERI;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(10)]
        public string AddndERI
        {
            get { return mAddndERI; }
            set { SetPropertyValue("AddndERI", ref mAddndERI, value); }
        }*/

        /*
        private string mAddndRRI;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(10)]
        public string AddndRRI
        {
            get { return mAddndRRI; }
            set { SetPropertyValue("AddndRRI", ref mAddndRRI, value); }
        }

        private string mAddndNmrPrv;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(10)]
        public string AddndNmrPrv
        {
            get { return mAddndNmrPrv; }
            set { SetPropertyValue("AddndNmrPrv", ref mAddndNmrPrv, value); }
        }*/

        private string mAls;
        [DisplayName("Alias")]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(80)]
        public string Als
        {
            get { return mAls; }
            set { SetPropertyValue("Als", ref mAls, value); }
        }

        //#region + ProvClie - Documentos
        /* No será que esto hace lento el modelado
         * Para qué lo necesito?
        [Association("Cliente-Ventas")]
        public XPCollection<DocumentoSalida> Documentos
        {
            get
            {
                return GetCollection<DocumentoSalida>("Documentos");
            }
        }*/
        //#endregion

        [Association("Cliente-Contactos", typeof(PCContacto)), DevExpress.Xpo.Aggregated]
        public XPCollection Contactos
        {
            get { return GetCollection("Contactos"); }
        }

        private uint mUltmPryct;   // UInt32
        [VisibleInListView(false)]
        [DisplayName("Último Proyecto")]
        public uint UltmPryct
        {
            get { return mUltmPryct; }
            set { SetPropertyValue("UltmPryct", ref mUltmPryct, value); }
        }

        private CatalogoCliente mClntPrspct;
        [VisibleInListView(false)]
        //[DataSourceCriteria("Tipo = 'ClienteProspecto'")]
        [XafDisplayName("Cliente_Prospecto")]
        public CatalogoCliente ClntPrspct
        {
            get { return mClntPrspct; }
            set { SetPropertyValue("ClntPrspct", ref mClntPrspct, value); }
        }



        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DiasPago = EDiasSemana.Ninguno;
            DiasRevision = EDiasSemana.Ninguno;
            // FileAdd = string.Empty;
            Tipo = ECausanteTipo.Cliente;

            Empresa emp =
            Session.FindObject(typeof(Empresa), new BinaryOperator("Clave", "icpac")) as Empresa;

            if (emp != null)
            {
                Compania.Direccion.Pais = emp.Compania.Direccion.Pais;
                Compania.Direccion.Estado = emp.Compania.Direccion.Estado;
            }

            NmrPrvdr = string.Empty;
            TpAddnd = EAddenda.Ninguna;

            UltmPryct = 1;
            Vendedor = null;
        }


        protected override string getClave() { return Clave; }  // return string.IsNullOrEmpty(Clave) ? string.Empty : Clave.Trim();


        private bool NoHayVend()
        {
            return Session.FindObject<Vendedor>(new BinaryOperator("Status", StatusVnd.Activo)) == null;
        }
    }
}
