using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Cap.Generales.BusinessObjects.Empresa;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace Cap.Generales.BusinessObjects.Direccion
{
    [DefaultProperty("DisplayLook")]
    public partial class Direccion
    {
        const ushort LONNUM = 45;  // antes 15
        const ushort LONTEL = 15;  //16;  Parece que con la lada debe ser de 10
        const ushort LONURL = 100;
        const ushort LONMAIL = 120;
        const ushort LONCP = 5;
        const ushort LONCALLE = 60;
        const ushort LONCOL = 60;
        const ushort LONMNC = 40;
        const ushort LONCIU = 40;
        const ushort LONEDO = 20;
        const ushort LONPAIS = 20;

        private string FCp;
        [ImmediatePostData]
        [ModelDefault("EditMask", "([0-9]{5})?"), ModelDefault("EditMaskType", "RegEx")]
        [DevExpress.Xpo.DisplayName("Código Postal")]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(LONCP)]
        public string CP
        {
            get { return FCp; }
            set { SetPropertyValue("CP", ref FCp, ValorString("CP", value)); }
        }

        private string FCalle;
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(LONCALLE)]
        public string Calle
        {
            get { return FCalle; }
            set { SetPropertyValue("Calle", ref FCalle, ValorString("Calle", value)); }
        }

        private string FNumero;
        [DevExpress.Xpo.DisplayName("Número")]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(LONNUM)]
        public string Numero
        {
            get { return FNumero; }
            set { SetPropertyValue("Numero", ref FNumero, ValorString("Numero", value)); }
        }

        private string FInterior;
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(20)]
        public string Interior
        {
            get { return FInterior; }
            set { SetPropertyValue("Interior", ref FInterior, value != null ? value.Trim() : value); }
        }

        private string FColonia;
        [ModelDefault("DataSourceProperty", "Colonias")]
        [ModelDefault("PropertyEditorType", "Cap.Module.Win.Editors.CustomStringEditor")]
        [ModelDefault("PropertyEditorType", "Escul.Module.Win.Editors.CustomStringEditor")]
        [ModelDefault("PropertyEditorType", "FCap.Module.Web.Editors.CustomStringEditor")]
        [ModelDefault("PropertyEditorType", "FCap.Module.Win.Editors.CustomStringEditor")]
        [ModelDefault("PropertyEditorType", "LCap.Module.Win.Editors.CustomStringEditor")]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(LONCOL)]
        public string Colonia
        {
            get { return FColonia; }
            set
            {
                if (value != null)
                    SetPropertyValue("Colonia", ref FColonia, ValorString("Colonia", value));
            }
        }

        private string FMunicipio;
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(LONMNC)]
        public string Municipio
        {
            get { return FMunicipio; }
            set { SetPropertyValue("Municipio", ref FMunicipio, ValorString("Municipio", value)); }
        }
        //#endregion

        //#region + Ciudad
        private string FCiudad;
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(LONCIU)]
        public string Ciudad
        {
            get { return FCiudad; }
            set { SetPropertyValue("Ciudad", ref FCiudad, value != null ? value.Trim() : null); }
        }
        //#endregion

        //#region + Estado
        private string FEstado;
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(LONEDO)]
        public string Estado
        {
            get { return FEstado; }
            set { SetPropertyValue("Estado", ref FEstado, ValorString("Estado", value)); }
        }
        
        private string FPais;
        [DevExpress.Xpo.DisplayName("País")]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(LONPAIS)]
        public string Pais
        {
            get { return FPais; }
            set
            {
                SetPropertyValue("Pais", ref FPais, ValorString("Pais", value)
              /*value != null ? value.Trim() : null*/);
            }
        }

        //#region + Referencia, cerca de tal lugar, o es de color blanca
        private string mReferencia;
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(120)]
        public string Referencia
        {
            get { return mReferencia; }
            set { SetPropertyValue("Referencia", ref mReferencia, ValorString("Referencia", value)); }
        }

        private string mTelGnralLad;
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [DevExpress.Xpo.DisplayName("General Lada")]
        // [ModelDefault("EditMask", "\\d+"), ModelDefault("EditMaskType", "RegEx")]
        [Size(3)]
        public string TelGnralLad
        {
            get { return mTelGnralLad; }
            set { SetPropertyValue("TelGnralLad", ref mTelGnralLad, value); }
        }

        private string FTelefonoGeneral;
        // [ModelDefault("EditMask", @"\((\d{2})\)-(\d{4})-(\d{4}) (x(\d*))?"), ModelDefault("EditMaskType", "RegEx")]
        // TI No le gustó Ago 2016 [ModelDefault("EditMask", @"(\d{4})-(\d{4}) (x(\d*))?"), ModelDefault("EditMaskType", "RegEx")]
        [DevExpress.Xpo.DisplayName("Teléfono General")]
        [VisibleInListView(true)]
        [VisibleInLookupListView(false)]
        [Size(LONTEL)]
        public string TelefonoGeneral
        {
            get { return FTelefonoGeneral; }
            set { SetPropertyValue("TelefonoGeneral", ref FTelefonoGeneral,  ValorString("TelefonoGeneral", value)); }
        }

        private string mTelMovLad;
        [DevExpress.Xpo.DisplayName("Móvil Lada")]
        //[ModelDefault("EditMask", "\\d+"), ModelDefault("EditMaskType", "RegEx")]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(3)]
        //[VisibleInListView(false)]
        public string TelMovLad
        {
            get { return mTelMovLad; }
            set { SetPropertyValue("TelMovLad", ref mTelMovLad, value); }
        }
        //#endregion

        private string FTelefonoMovil;
        [DevExpress.Xpo.DisplayName("Teléfono Móvil")]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(LONTEL)]
        public string TelefonoMovil
        {
            get { return FTelefonoMovil; }
            set { SetPropertyValue("TelefonoMovil", ref FTelefonoMovil, value); }
        }

        //#region + Telefono casa
        private string FTelefonoCasa;
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [Size(LONTEL)]
        public string TelefonoCasa
        {
            get { return FTelefonoCasa; }
            set { SetPropertyValue("TelefonoCasa", ref FTelefonoCasa, value); }
        }

        #region + Telefono trabajo
        private string FTelefonoTrabajo;
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [Size(LONTEL)]
        public string TelefonoTrabajo
        {
            get { return FTelefonoTrabajo; }
            set { SetPropertyValue("TelefonoTrabajo", ref FTelefonoTrabajo, value); }
        }
        #endregion

        private string FTelefonoFax;
        [VisibleInDetailView(false)]
        [DevExpress.Xpo.DisplayName("Teléfono Fax")]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(LONTEL)]
        public string TelefonoFax
        {
            get { return FTelefonoFax; }
            set { SetPropertyValue("TelefonoFax", ref FTelefonoFax, value); }
        }

        #region + Url
        private string FUrl;
        [EditorAlias("HyperLinkPropertyEditor")]
        [ModelDefault("EditMask", @"(((http|https|ftp)\://)?[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;amp;%\$#\=~])*)|([a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6})"), ModelDefault("EditMaskType", "RegEx")]
        [DevExpress.Xpo.DisplayName("Página Web")]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(LONURL)]
        public string Url
        {
            get { return FUrl; }
            set { SetPropertyValue("Url", ref FUrl, value); }
        }
        #endregion

        //#region + Email
        private string FEmail;
        // [EditorAlias("HyperLinkPropertyEditor")]
        // [ModelDefault("EditMask", @"(((http|https|ftp)\://)?[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;amp;%\$#\=~])*)|([a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6})"), ModelDefault("EditMaskType", "RegEx")]
        [DevExpress.Xpo.DisplayName("Correo")]
        [VisibleInListView(true)]
        [VisibleInLookupListView(false)]
        [Size(LONMAIL)]
        public string Email
        {
            get { return FEmail; }
            set { SetPropertyValue("Email", ref FEmail, value); }
        }


        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public string Domicilio
        {
            get
            {

                return String.Format("{0} {1}-{2} \n{3} \n{4} {5} \n{6}, {7} \n{8}", Calle, Numero, Interior, Colonia, Ciudad, Municipio, Estado, Pais, 
                    string.IsNullOrEmpty(CP) ? string.Empty : $"C.P.: {CP}");
            }
        }

        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public string Domicilio2
        {
            get
            {
                string aux = CP;

                if (!string.IsNullOrEmpty(aux))
                    aux = $"CP.: {aux.Trim()}";

                if (!string.IsNullOrEmpty(aux) && aux.Length > 1 &&
                    aux[aux.Length - 1] != ',' && !string.IsNullOrEmpty(Calle))
                    aux = $"{aux},  {Calle.Trim()}";
                
                if (!string.IsNullOrEmpty(aux) && aux.Length > 1 &&
                    aux[aux.Length - 1] != ',' && !string.IsNullOrEmpty(Numero))
                    aux = $"{aux},  {Numero.Trim()}";
                
                if (!string.IsNullOrEmpty(aux) && aux.Length > 1 &&
                    aux[aux.Length - 1] != ',' && !string.IsNullOrEmpty(Interior))
                    aux = $"{aux}-{Interior.Trim()}";
                
                if (!string.IsNullOrEmpty(aux) && aux.Length > 1 &&
                    aux[aux.Length - 1] != ',' && !string.IsNullOrEmpty(Colonia))
                    aux = $"{aux},  {Colonia.Trim()}";
                
                if (!string.IsNullOrEmpty(aux) && aux.Length > 1 &&
                    aux[aux.Length - 1] != ',' && !string.IsNullOrEmpty(Municipio))
                    aux = $"{aux},  {Municipio.Trim()}";
                
                if (!string.IsNullOrEmpty(aux) && aux.Length > 1 &&
                    aux[aux.Length - 1] != ',' && !string.IsNullOrEmpty(Ciudad))
                    aux = $"{aux},  {Ciudad.Trim()}";
                
                if (!string.IsNullOrEmpty(aux) && aux.Length > 1 &&
                    aux[aux.Length - 1] != ',' && !string.IsNullOrEmpty(Estado))
                    aux = $"{aux},  {Estado.Trim()}";
                
                if (!string.IsNullOrEmpty(aux) && aux.Length > 1 &&
                    aux[aux.Length - 1] != ',' && !string.IsNullOrEmpty(Pais))
                    aux = $"{aux},  {Pais.Trim()}";
                return aux;
            }
        }

        [VisibleInDetailView(false)]
        [Association("Direcciones")]
        public Compania Compania;

        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [NonPersistent]
        public string DisplayLook
        {
            get { return string.Format("{0} - {1} - {2}", CP, Calle, Email); }
        }

        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public string Calctel
        {
            get
            {
                return string.Format("Correo: {0},  Tel.: {1}", Email, TelefonoGeneral);
            }
        }

        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public string Calcmncp
        {
            get
            {
                string aux = string.Empty;

                aux += Municipio;
                aux = aux.Trim();
                if (aux.Length > 0 && !string.IsNullOrEmpty(Estado.Trim()))
                    aux += ", ";
                aux += Estado;
                aux = aux.Trim();
                if (aux.Length > 0 && !string.IsNullOrEmpty(Pais.Trim()))
                    aux += ", ";
                aux += Pais;

                // string.Format("{0}, {1}, {2}", Municipio, Estado, Pais);
                return aux.Trim();
            }
        }

        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public string Calccall
        {
            get
            {
                string aux = string.Format("{0}   {1}   {2}", Calle, Numero, Interior);
                return aux.Trim();
            }
        }

        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public string Calccoln
        {
            get
            {
                string aux = string.Format("{0}   {1}   ", Colonia, CP);
                return aux.Trim();
            }
        }

        private List<string> mColonias;
        private List<string> Colonias
        {
            get { return mColonias; }
        }

        private Location mLocation;
        [XafDisplayName("Coordenadas")]
        [DevExpress.Xpo.Aggregated]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        public Location Location
        {
            get { return mLocation; }
            set { SetPropertyValue("Location", ref mLocation, value); }
        }

        public void ColoniasEmpty()
        {
            if (mColonias == null)
                mColonias = new List<string>();
            mColonias.Clear();
        }

        public void ColoniasAdd(string value)
        {
            Colonias.Add(value);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Calle = string.Empty;
            Ciudad = string.Empty;
            Colonia = string.Empty;
            CP = string.Empty;
            Email = string.Empty;
            Estado = "CIUDAD DE MÉXICO";
            Municipio = string.Empty;
            Numero = string.Empty;
            Pais = "MÉXICO";
            TelefonoCasa = string.Empty;
            TelefonoFax = string.Empty;
            TelefonoGeneral = string.Empty;
            TelefonoMovil = string.Empty;
            TelefonoTrabajo = string.Empty;
            Url = string.Empty;

            TelGnralLad = string.Empty;
            TelMovLad = string.Empty;
        }
    }
}
