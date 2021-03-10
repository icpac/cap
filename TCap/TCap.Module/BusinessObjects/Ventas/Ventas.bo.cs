using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.ExpressApp.DC;
using Cap.Generales.BusinessObjects.General;
using DevExpress.Persistent.Validation;
using TCap.Module.BusinessObjects.Empresa;
using Cap.Ventas.BusinessObjects;

namespace TCap.Module.BusinessObjects.Ventas
{
    [NavigationItem("Configuración")]
    [XafDisplayName("Parámetros")]
    [VisibleInReports(false)]
    public partial class Ventas : ISingleton
    {
        // Creo que no es necesario que tenga Clave 27 enero 2009
        // Provocamos que se caiga pues si cambia el nombre ya no lo encuentra 
        private string FClave;
        [Obsolete("Es singleton, no es necesaria la Clave")]
        [Index(0)]
        [VisibleInDetailView(false)]
        [Size(10)]
        public string Clave
        {
            get { return FClave/*.Trim()*/; }
            set { SetPropertyValue("Clave", ref FClave, value); }
        }

        private UInt32 FUltFac;
        [Obsolete("En su lugar usar VntCfdi.UltFactura")]
        [Index(1)]
        [DisplayName("Última Factura")]
        [Appearance("UltFactura", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Method = "SinCfdi")]
        public UInt32 UltFactura
        {
            get { return FUltFac; }
            set { SetPropertyValue("UltFactura", ref FUltFac, value); }
        }

        private UInt32 FUltDev;
        [Obsolete("En su lugar usar VntCfdi.UltDevolucion")]
        [Index(2)]
        [DisplayName("Última Nota de Crédito")]
        [Appearance("UltDevolucion", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Method = "SinDev")]
        public UInt32 UltDevolucion
        {
            get { return FUltDev; }
            set { SetPropertyValue("UltDevolucion", ref FUltDev, value); }
        }

        private UInt32 FUltNotaC;
        [Obsolete("En su lugar usar VntCfdi.UltNotaC")]
        [Index(3)]
        [DisplayName("Última Nota de Cargo")]
        [Appearance("UltNotaC", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Method = "SinNotaC")]
        public UInt32 UltNotaC
        {
            get { return FUltNotaC; }
            set { SetPropertyValue("UltNotaC", ref FUltNotaC, value); }
        }

        private Cap.Generales.BusinessObjects.Empresa.Empresa mEmpresa;
        [NonPersistent]
        private Cap.Generales.BusinessObjects.Empresa.Empresa Empresa 
        {
            get
            {
                if (mEmpresa == null)
                    mEmpresa = Session.FindObject<Cap.Generales.BusinessObjects.Empresa.Empresa>(null); //  (CriteriaOperator.Parse("Clave = 'icpac'"));
                return mEmpresa;
            }
        }
        private bool SinCfdi()
        {
            return SinDev();
        }

        private bool SinDev()
        {
            return SinNotaC();
        }

        private bool SinNotaC()
        {
            bool isAdm = false;

            /*TIT Mar 2021 El empleadoproyecto  ya no tiene roles
            EmpleadoProyecto currentUser = SecuritySystem.CurrentUser as EmpleadoProyecto;
            if (currentUser != null)
            {
                foreach (EmpleadoRol role in currentUser.EmpleadosRoles)
                {
                    if (role.Name == "Administrator")
                    {
                        isAdm = true;
                        break;
                    }
                }
            }*/

            return !isAdm && (Empresa == null || !Empresa.ConCfdi);
        }

        private string mRutaPdfVnts;
        [Obsolete("En su lugar usar VntCfdi.RutaPdfVnts")]
        [Index(14)]
        [DisplayName("Ruta para archivos PDF")]
        public string RutaPdfVnts
        {
            get { return  mRutaPdfVnts; }
            set { SetPropertyValue("RutaPdfVnts", ref mRutaPdfVnts, value); }
        }

        private bool mCapMayus;
        [Obsolete("En su lugar usar VntCfdi.CapMayus")]
        [Index(10)]
        [DisplayName("Dirección a Mayúsculas")]
        public bool CapMayus
        {
            get { return mCapMayus; }
            set { SetPropertyValue("CapMayus", ref mCapMayus, value); }
        }

        private UInt32 FUltClie;
        [Obsolete("En su lugar usar VntCfdi.UltClie")]
        [Index(7)]
        [DisplayName("Último Cliente")]
        public UInt32 UltClie
        {
            get { return FUltClie; }
            set { SetPropertyValue("UltClie", ref FUltClie, value); }
        }

        private string mCuenta;
        [Index(12)]
        [DisplayName("Cuenta Contable")]
        [VisibleInDetailView(true)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(20)]
        public string Cuenta
        {
            get { return mCuenta; }
            set { SetPropertyValue("Cuenta", ref mCuenta, value); }
        }

        private UInt32 mUltProd;
        [Obsolete("En su lugar usar VntCfdi.UltProd")]
        [Index(9)]
        [DisplayName("Último Producto")]
        public UInt32 UltProd
        {
            get { return mUltProd; }
            set { SetPropertyValue("UltProd", ref mUltProd, value); }
        }

        private bool mSendM;
        [Obsolete("En su lugar usar VntCfdi.SendM")]
        [Index(11)]
        [DisplayName("Grabar -> Enviar por correo")]
        public bool SendM
        {
            get { return mSendM; }
            set { SetPropertyValue("SendM", ref mSendM, value); }
        }

        private UInt32 FUltRem;
        [Index(4)]
        [DisplayName("Última Remisión")]
        public UInt32 UltRem
        {
            get { return FUltRem; }
            set { SetPropertyValue("UltRem", ref FUltRem, value); }
        }

        private UInt32 FUltCot;
        [Index(6)]
        [DisplayName("Última Cotización")]
        public UInt32 UltCot
        {
            get { return FUltCot; }
            set { SetPropertyValue("UltCot", ref FUltCot, value); }
        }

        private UInt32 FUltProv;
        [Index(8)]
        [DisplayName("Último Proveedor")]
        public UInt32 UltProv
        {
            get { return FUltProv; }
            set { SetPropertyValue("UltProv", ref FUltProv, value); }
        }

        private string mSendCopy;
        [Obsolete("En su lugar usar VntCfdi.SendCopy")]
        [Index(13)]
        [DisplayName("Enviar Copia A")]
        [VisibleInDetailView(true)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(80)]
        public string SendCopy
        {
            get { return mSendCopy; }
            set { SetPropertyValue("SendCopy", ref mSendCopy, value); }
        }

        private UInt32 FUltPed;
        [Index(5)]
        [DisplayName("Último Pedido")]
        public UInt32 UltPed
        {
            get { return FUltPed; }
            set { SetPropertyValue("UltPed", ref FUltPed, value); }
        }

        private bool mCxcInt;
        [Index(15)]
        [DisplayName("Cxc Integrado")]
        public bool CxcInt
        {
            get { return mCxcInt; }
            set { SetPropertyValue("CxcInt", ref mCxcInt, value); }
        }

        private bool mImpsLcles;
        [Obsolete("En su lugar usar VntCfdi.ImpsLcles")]
        // [Obsolete("Parece que puede ser más flexible.")]
        [DisplayName("Impuestos Locales")]
        public bool ImpsLcles
        {
            get { return mImpsLcles; }
            set { SetPropertyValue("", ref mImpsLcles, value); }
        }

        private string mLeyImpst1;
        [Obsolete("En su lugar usar VntCfdi.LeyImpst1")]
        [DisplayName("Leyenda Impuesto 1")]
        [Index(16)]
        [Size(30)]
        public string LeyImpst1
        {
            get { return mLeyImpst1; }
            set { SetPropertyValue("LeyImpst1", ref mLeyImpst1, ValorString("LeyImpst1", value)); }
        }

        private string mLeyImpst2;
        [Obsolete("En su lugar usar VntCfdi.LeyImpst2")]
        [DisplayName("Leyenda Impuesto 2")]
        [Index(17)]
        [Size(30)]
        public string LeyImpst2
        {
            get { return mLeyImpst2; }
            set { SetPropertyValue("LeyImpst2", ref mLeyImpst2, ValorString("LeyImpst2", value)); }
        }

        private string mLeyImpst3;
        [Obsolete("En su lugar usar VntCfdi.LeyImpst3")]
        [DisplayName("Leyenda Impuesto 3")]
        [Index(18)]
        [Size(30)]
        public string LeyImpst3
        {
            get { return mLeyImpst3; }
            set { SetPropertyValue("LeyImpst3", ref mLeyImpst3, ValorString("LeyImpst3", value)); }
        }

        private string mLeyImpst4;
        [Obsolete("En su lugar usar VntCfdi.LeyImpst4")]
        [DisplayName("Leyenda Impuesto 4")]
        [Index(19)]
        [Size(30)]
        public string LeyImpst4
        {
            get { return mLeyImpst4; }
            set { SetPropertyValue("LeyImpst4", ref mLeyImpst4, ValorString("LeyImpst4", value)); }
        }

        private MyFileData mCmpltXml;
        [Obsolete("En su lugar usar VntCfdi.CmpltXml")]
        [Index(20)]
        [DevExpress.Xpo.DisplayName("Archivo Complementario (XML)")]
        [FileTypeFilter("Complemento", 1, "*.xml")]
        public MyFileData CmpltXml
        {
            get { return mCmpltXml; }
            set { SetPropertyValue("CmpltXml", ref mCmpltXml, value); }
        }

        private UInt32 FUltRecp;
        [Index(5)]
        [DisplayName("Última Recepción")]
        public UInt32 UltRecp
        {
            get { return FUltRecp; }
            set { SetPropertyValue("UltRecp", ref FUltRecp, value); }
        }

        private UInt32 mUltVnddr;
        [DisplayName("Último Vendedor")]
        public UInt32 UltVnddr
        {
            get { return mUltVnddr; }
            set { SetPropertyValue("UltVnddr", ref mUltVnddr, value); }
        }

        private string mSerie;
        [Obsolete("En su lugar usar VntCfdi.Serie")]
        [Size(10)]
        public string Serie
        {
            get { return mSerie; }
            set { SetPropertyValue("Serie", ref mSerie, value); }
        }

        private short mNumrDcmls;
        [Obsolete("En su lugar usar VntCfdi.NumrDecmls")]
        [DisplayName("Número de Decimales en Cantidades")]
        public short NumrDecmls
        {
            get { return mNumrDcmls; }
            set { SetPropertyValue("NumrDecmls", ref mNumrDcmls, value); }
        }


        private UInt32 mUltPolz;
        [DisplayName("Última Póliza")]
        public UInt32 UltPolz
        {
            get { return mUltPolz; }
            set { SetPropertyValue("UltPolz", ref mUltPolz, value); }
        }

        private ushort mDgtPrmrNvl;
        [DisplayName("Digitos Primer Nivel")]
        public ushort DgtPrmrNvl
        {
            get { return mDgtPrmrNvl; }
            set { SetPropertyValue("DgtPrmrNvl", ref mDgtPrmrNvl, value); }
        }

        private ushort mDgtSgndNvl;
        [DisplayName("Digitos Segundo Nivel")]
        public ushort DgtSgndNvl
        {
            get { return mDgtSgndNvl; }
            set { SetPropertyValue("DgtSgndNvl", ref mDgtSgndNvl, value); }
        }


        private ushort mDgtTrcrNvl;
        [DisplayName("Digitos Tercer Nivel")]
        public ushort DgtTrcrNvl
        {
            get { return mDgtTrcrNvl; }
            set { SetPropertyValue("DgtTrcrNvl", ref mDgtTrcrNvl, value); }
        }


        private ushort mDgtCrtNvl;
        [DisplayName("Digitos Cuarto Nivel")]
        public ushort DgtCrtNvl
        {
            get { return mDgtCrtNvl; }
            set { SetPropertyValue("DgtCrtNvl", ref mDgtCrtNvl, value); }
        }

        /*
        private short mNumrDcmlsPrecs;
        [VisibleInDetailView(false)]
        [DisplayName("Número de Decimales en Precios")]
        public short NumrDecmlsPrecs
        {
            get { return mNumrDcmlsPrecs; }
            set { SetPropertyValue("NumrDecmlsPrecs", ref mNumrDcmlsPrecs, value); }
        }*/

        private UInt32 mUltmPryct;
        [Obsolete("Lo movimos a Cliente !")]
        [DisplayName("Último Proyecto")]
        public UInt32 UltmPryct
        {
            get { return mUltmPryct; }
            set { SetPropertyValue("UltmPryct", ref mUltmPryct, value); }
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

        // Recibos de nómina
        private UInt32 FUltRec;
        // Todavía se usa en el de nomina ! No recuerdo por qué lo dejé obsoleto
        // [Obsolete("Ya no es necesario !")]
        [Index(1)]
        [DisplayName("Último Recibo")]
        public UInt32 UltRec
        {
            get { return FUltRec; }
            set { SetPropertyValue("UltRec", ref FUltRec, value); }
        }

        private UInt32 FUltEmpl;
        [Index(2)]
        [DisplayName("Último Empleado")]
        public UInt32 UltEmpl
        {
            get { return FUltEmpl; }
            set { SetPropertyValue("UltEmpl", ref FUltEmpl, value); }
        }

        private UInt32 FUltNom;
        [Index(3)]
        [DisplayName("Última Nómina")]
        public UInt32 UltNom
        {
            get { return FUltNom; }
            set { SetPropertyValue("UltNom", ref FUltNom, value); }
        }

        private string mInfijo;
        [Index(8)]
        [Size(5)]
        public string Infijo
        {
            get { return mInfijo; }
            set { SetPropertyValue("Infijo", ref mInfijo, value); }
        }

        private VentaCfdi mVntCfdi;
        [DevExpress.Xpo.Aggregated]
        public VentaCfdi VntCfdi
        {
            get { return mVntCfdi; }
            set { SetPropertyValue("VntCfdi", ref mVntCfdi, value); }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Clave = "icpac";
            RutaPdfVnts = string.Empty;
            CapMayus = false;
            UltClie = 1;
            Cuenta = string.Empty;
            UltProd = 1;
            SendM = false;
            UltRem = 1;
            UltCot = 1;
            SendCopy = string.Empty;
            UltFactura = 1;
            UltNotaC = 1;
            UltDevolucion = 1;
            UltPed = 1;
            UltProv = 1;
            LeyImpst4 = "IVA";
            UltRecp = 1;
            UltVnddr = 1;
            Serie = "A";
            NumrDecmls = 2;
            /*
            UltmPryct = 1;*/
            /*
            NumrDecmlsPrecs = 2;*/

            UltPolz = 1;
            VntCfdi = new VentaCfdi(Session);
        }

        [RuleFromBoolProperty("Ventas.NumrDecmls", DefaultContexts.Save, "El Número de Decimales debe ser entre 0 y 6")]
        protected bool NumrDecmlsOk
        {
            get { return NumrDecmls >= 0 && NumrDecmls <= 6; }
        }
    }
}
