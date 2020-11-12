using Cap.Generales.BusinessObjects.General;
using Cap.Inventarios.BusinessObjects;
using Cap.Ventas.BusinessObjects;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;
using System.Drawing;

namespace MicroStore.Module.BusinessObjects
{
    [XafDisplayName("Parámetros")]
    [NavigationItem("Configuración")]
    public partial class Parametros : ISingleton
    {
        private UInt16 mUltmCmpr;
        [XafDisplayName("Última Compra")]
        public ushort /*UInt16*/ UltmCmpr
        {
            get { return mUltmCmpr; }
            set { SetPropertyValue("UltmCmpr", ref mUltmCmpr, value); }
        }

        private UInt16 mUltmVnt;
        [XafDisplayName("Última Venta")]
        public ushort UltmVnt
        {
            get { return mUltmVnt; }
            set { SetPropertyValue("UltmVnt", ref mUltmVnt, value); }
        }

        private ConceptoMI mCncptVnt;
        [DataSourceCriteria("Tipo = 'Salida'")]
        [XafDisplayName("Concepto de Venta")]
        public ConceptoMI CncptVnt
        {
            get { return mCncptVnt; }
            set { SetPropertyValue("CncptVnt", ref mCncptVnt, value); }
        }

        private ConceptoMI mCncptCnclrVnt;
        [DataSourceCriteria("Tipo = 'Entrada'")]
        [XafDisplayName("Concepto Cancelar Venta")]
        public ConceptoMI CncptCnclrVnt
        {
            get { return mCncptCnclrVnt; }
            set { SetPropertyValue("CncptCnclrVnt", ref mCncptCnclrVnt, value); }
        }

        private ConceptoMI mCncptCmpr;
        [DataSourceCriteria("Tipo = 'Entrada'")]
        [XafDisplayName("Concepto de Compra")]
        public ConceptoMI CncptCmpr
        {
            get { return mCncptCmpr; }
            set { SetPropertyValue("CncptCmpr", ref mCncptCmpr, value); }
        }

        private ConceptoMI mCncptCnclrCmpr;
        [DataSourceCriteria("Tipo = 'Salida'")]
        [XafDisplayName("Concepto Cancelar Compra")]
        public ConceptoMI CncptCnclrCmpr
        {
            get { return mCncptCnclrCmpr; }
            set { SetPropertyValue("CncptCnclrCmpr", ref mCncptCnclrCmpr, value); }
        }

        private ConceptoMI mCncptAjstInvtrE;
        [DataSourceCriteria("Tipo = 'Entrada'")]
        [XafDisplayName("Ajuste Mas, Inventario")]
        public ConceptoMI CncptAjstInvtrE
        {
            get { return mCncptAjstInvtrE; }
            set { SetPropertyValue("CncptAjstInvtrE", ref mCncptAjstInvtrE, value); }
        }

        private ConceptoMI mCncptAjstInvtrS;
        [DataSourceCriteria("Tipo = 'Salida'")]
        [XafDisplayName("Ajuste Menos, Inventario")]
        public ConceptoMI CncptAjstInvtrS
        {
            get { return mCncptAjstInvtrS; }
            set { SetPropertyValue("CncptAjstInvtrS", ref mCncptAjstInvtrS, value); }
        }

        private bool mVntSnExstnc;
        [XafDisplayName("Venta Sin Existencia")]
        public bool VntSnExstnc
        {
            get { return mVntSnExstnc; }
            set { SetPropertyValue("VntSnExstnc", ref mVntSnExstnc, value); }
        }

        private VentaCfdi mVntCfdi;
        [DevExpress.Xpo.Aggregated]
        public VentaCfdi VntCfdi
        {
            get { return mVntCfdi; }
            set { SetPropertyValue("VntCfdi", ref mVntCfdi, value); }
        }

        private UInt32 FUltPrvdr;
        [XafDisplayName("Último Proveedor")]
        public uint /*UInt32*/ UltPrvdr
        {
            get { return FUltPrvdr; }
            set { SetPropertyValue("UltPrvdr", ref FUltPrvdr, value); }
        }

        private UInt16 mTmpLt;
        [XafDisplayName("Avisar Caducidad Lotes (días)")]
        public ushort TmpLt
        {
            get { return mTmpLt; }
            set { SetPropertyValue("TmpLt", ref mTmpLt, value); }
        }

        private bool mImprmrTckt;
        [XafDisplayName("Imprimir Ticket")]
        public bool ImprmrTckt
        {
            get { return mImprmrTckt; }
            set { SetPropertyValue("ImprmrTckt", ref mImprmrTckt, value); }
        }

        private bool mPrdctCptr;
        [ImmediatePostData]
        [XafDisplayName("Producto en Captura")]
        public bool PrdctCptr
        {
            get { return mPrdctCptr; }
            set { SetPropertyValue("PrdctCptr", ref mPrdctCptr, value); }
        }

        private Unidad mUndd;
        [Appearance("Parametros.Undd", Context = "DetailView", Enabled = false, Criteria = "!PrdctCptr", FontStyle = FontStyle.Italic)]
        [XafDisplayName("Unidad Omisión")]
        public Unidad Undd
        {
            get { return mUndd; }
            set { SetPropertyValue("Undd", ref mUndd, value); }
        }

        private bool mUpdtPrcAuto;
        [XafDisplayName("Actualiza Precios Automaticamente")]
        public bool UpdtPrcAuto
        {
            get { return mUpdtPrcAuto; }
            set { SetPropertyValue("UpdtPrcAuto", ref mUpdtPrcAuto, value); }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            VntCfdi = new VentaCfdi(Session);
            VntSnExstnc = false;
            FUltPrvdr = 1;
        }
    }
}
