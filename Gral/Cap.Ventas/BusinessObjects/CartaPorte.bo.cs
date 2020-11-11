using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using Cap.Inventarios.BusinessObjects;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using Cap.Generales.BusinessObjects.Empresa;

namespace Cap.Ventas.BusinessObjects
{
    public partial class CartaPorte
    {
        private string mRecogerEn; // Recoger en
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [Appearance("CartaPorte.RecogerEn", AppearanceItemType = "LayoutItem", Visibility = ViewItemVisibility.Hide, Context = "DetailView", Method = "EsCartaPorte")]
        [Size(40)]
        public string RecogerEn
        {
            get { return mRecogerEn; }
            set { SetPropertyValue("RecogerEn", ref mRecogerEn, value != null ? value.Trim(new char[] { ' ', '\0' }) : string.Empty); }
        }

        private Compania mDstn;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        // [Appearance("DocumentoSalida.Dstn", AppearanceItemType = "LayoutItem", Visibility = ViewItemVisibility.Hide, Context = "DetailView", Method = "CartaPorte")]
        public Compania Dstn
        {
            get { return mDstn; }
            set { SetPropertyValue("Dstn", ref mDstn, value); }
        }

        private string mTransprt;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        // [Appearance("DocumentoSalida.Transprt", AppearanceItemType = "LayoutItem", Visibility = ViewItemVisibility.Hide, Context = "DetailView", Method = "CartaPorte")]
        [Size(20)]
        public string Transprt
        {
            get { return mTransprt; }
            set { SetPropertyValue("Transprt", ref mTransprt, value); }
        }

        private string mOprdr;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        // [Appearance("DocumentoSalida.Oprdr", AppearanceItemType = "LayoutItem", Visibility = ViewItemVisibility.Hide, Context = "DetailView", Method = "CartaPorte")]
        [Size(20)]
        public string Oprdr
        {
            get { return mOprdr; }
            set { SetPropertyValue("Oprdr", ref mOprdr, value); }
        }

        private string mPlcs;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        // [Appearance("DocumentoSalida.Plcs", AppearanceItemType = "LayoutItem", Visibility = ViewItemVisibility.Hide, Context = "DetailView", Method = "CartaPorte")]
        [Size(10)]
        public string Plcs
        {
            get { return mPlcs; }
            set { SetPropertyValue("Plcs", ref mPlcs, value); }
        }

        private decimal mOtros;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        // [Appearance("DocumentoSalida.Otros", AppearanceItemType = "LayoutItem", Visibility = ViewItemVisibility.Hide, Context = "DetailView", Method = "CartaPorte")]
        // [Size(10)]
        public decimal Otros
        {
            get { return mOtros; }
            set { SetPropertyValue("Otros", ref mOtros, value); }
        }

        private decimal mAutopis;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        // [Appearance("DocumentoSalida.Autopis", AppearanceItemType = "LayoutItem", Visibility = ViewItemVisibility.Hide, Context = "DetailView", Method = "CartaPorte")]
        // [Size(10)]
        public decimal Autopis
        {
            get { return mAutopis; }
            set { SetPropertyValue("Autopis", ref mAutopis, value); }
        }

        // Maniobras
        private decimal mManio;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        // [Appearance("DocumentoSalida.Manio", AppearanceItemType = "LayoutItem", Visibility = ViewItemVisibility.Hide, Context = "DetailView", Method = "CartaPorte")]
        // [Size(10)]
        public decimal Manio
        {
            get { return mManio; }
            set { SetPropertyValue("Manio", ref mManio, value); }
        }

        // Entrega a domicilio
        private decimal mEntregaD;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        // [Appearance("DocumentoSalida.EntregaD", AppearanceItemType = "LayoutItem", Visibility = ViewItemVisibility.Hide, Context = "DetailView", Method = "CartaPorte")]
        public decimal EntregaD
        {
            get { return mEntregaD; }
            set { SetPropertyValue("EntregaD", ref mEntregaD, value); }
        }

        // Recolección
        private decimal mRecolecc;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        // [Appearance("DocumentoSalida.Recolecc", AppearanceItemType = "LayoutItem", Visibility = ViewItemVisibility.Hide, Context = "DetailView", Method = "CartaPorte")]
        public decimal Recolecc
        {
            get { return mRecolecc; }
            set { SetPropertyValue("Recolecc", ref mRecolecc, value); }
        }

        private decimal mSeguro;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        // [Appearance("DocumentoSalida.Seguro", AppearanceItemType = "LayoutItem", Visibility = ViewItemVisibility.Hide, Context = "DetailView", Method = "CartaPorte")]
        public decimal Seguro
        {
            get { return mSeguro; }
            set { SetPropertyValue("Seguro", ref mSeguro, value); }
        }

        private decimal mFlete;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        // [Appearance("DocumentoSalida.Flete", AppearanceItemType = "LayoutItem", Visibility = ViewItemVisibility.Hide, Context = "DetailView", Method = "CartaPorte")]
        public decimal Flete
        {
            get { return mFlete; }
            set { SetPropertyValue("Flete", ref mFlete, value); }
        }

        private string mConduceA;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        // [Appearance("DocumentoSalida.CuotaA", AppearanceItemType = "LayoutItem", Visibility = ViewItemVisibility.Hide, Context = "DetailView", Method = "CartaPorte")]
        [Size(30)]
        public string ConduceA
        {
            get { return mConduceA; }
            set { SetPropertyValue("ConduceA", ref mConduceA, value); }
        }

        private string mConduceDe;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        // [Appearance("DocumentoSalida.ConduceDe", AppearanceItemType = "LayoutItem", Visibility = ViewItemVisibility.Hide, Context = "DetailView", Method = "CartaPorte")]
        [Size(30)]
        public string ConduceDe
        {
            get { return mConduceDe; }
            set { SetPropertyValue("ConduceDe", ref mConduceDe, value); }
        }

        // Reembarcarse con
        private string mReembCn;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        // [Appearance("DocumentoSalida.ReembCn", AppearanceItemType = "LayoutItem", Visibility = ViewItemVisibility.Hide, Context = "DetailView", Method = "CartaPorte")]
        [Size(40)]
        public string ReembCn
        {
            get { return mReembCn; }
            set { SetPropertyValue("ReembCn", ref mReembCn, value); }
        }

        // Reembarco
        private string mReemb;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        // [Appearance("DocumentoSalida.Reemb", AppearanceItemType = "LayoutItem", Visibility = ViewItemVisibility.Hide, Context = "DetailView", Method = "CartaPorte")]
        [Size(40)]
        public string Reemb
        {
            get { return mReemb; }
            set { SetPropertyValue("Reemb", ref mReemb, value); }
        }

        // Valor Declarado, no sé si es sólo un SI o un NO
        private string mValorD;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        // [Appearance("DocumentoSalida.ValorD", AppearanceItemType = "LayoutItem", Visibility = ViewItemVisibility.Hide, Context = "DetailView", Method = "CartaPorte")]
        [Size(10)]
        public string ValorD
        {
            get { return mValorD; }
            set { SetPropertyValue("ValorD", ref mValorD, value); }
        }

        // Cuota Convenida
        private string mCuotaC;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        // [Appearance("DocumentoSalida.CuotaC", AppearanceItemType = "LayoutItem", Visibility = ViewItemVisibility.Hide, Context = "DetailView", Method = "CartaPorte")]
        [Size(15)]
        public string CuotaC
        {
            get { return mCuotaC; }
            set { SetPropertyValue("CuotaC", ref mCuotaC, value); }
        }




        private bool EsCartaPorte()
        {
            //TIT
            //return Tipo != DocumentoTipo.CartaPorte;
            return true;
        }
    }
}
