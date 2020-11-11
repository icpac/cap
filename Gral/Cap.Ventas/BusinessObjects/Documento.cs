#region Copyright (c) 2017-2019 cjlc
/*
{+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++)
{                                                                   }
{     cjlc Cap control administrativo personal                      }
{                                                                   }
{     Copyrigth (c) 2017-2019 cjlc                                  }
{     Todos los derechos reservados                                 }
{                                                                   }
{*******************************************************************}
 */
#endregion

using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using System.Drawing;
using apl.Log;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using Cap.Generales.BusinessObjects.Object;
using Cap.Generales.BusinessObjects.Unidades;
using Cap.Generales.BusinessObjects.Empresa;
using Cap.Generales.BusinessObjects.Direccion;
using Cap.Generales.BusinessObjects.General;
using Cap.Clientes.BusinessObjects.Generales;

// With XPO, the data model is declared by classes (so-called Persistent Objects) that will define the database structure, and consequently, the user interface (http://documentation.devexpress.com/#Xaf/CustomDocument2600).
namespace Cap.Ventas.BusinessObjects
{                                   
    //                                                                                                                                                       Cancelado                                   
    [Appearance("Documento.NoCancelado", TargetItems = "MotivCan, FechaCan", AppearanceItemType = "LayoutItem", Context = "DetailView", Criteria = "Status != 4", Visibility = ViewItemVisibility.Hide)]
    [Appearance("Documento.Lectura", TargetItems = "Clave, FechaCan, FechaDoc, Total, Impuesto04, Impuesto03," +
        "Impuesto02, Impuesto01, DescFinanciero, DescuentoTotal, SubTotal, FechaAlta, Domicilio", 
        Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
    [Appearance("Documento.SinCfdi", TargetItems = "Tipo", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Method = "MSinCfdi")]
    [Appearance("Cancel", TargetItems = "*", Context = "ListView", Criteria = "[Status] = 4", FontColor = "Red")]
    // Specify various UI options for your persistent class and its properties using a declarative approach via built-in attributes (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    //[ImageName("BO_Contact")]
    //[DefaultProperty("PersistentProperty")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewAndDetailView, true, NewItemRowPosition.Top)]
    // [DefaultClassOptions]
    // [NonPersistent]
    public class Documento : PObject
    { // You can use a different base persistent class based on your requirements (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public Documento(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here or place it only when the IsLoading property is false.
        }


        //private string _PersistentProperty;
        //[XafDisplayName("My Persistent Property")]
        //[ToolTip("Specify a hint message for a property in the UI.")]
        //[ModelDefault("EditMask", "(000)-00")]
        //[Index(0), VisibleInListView(true), VisibleInDetailView(false), VisibleInLookupListView(true)]
        //[RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue("PersistentProperty", ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My Action Method", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = True)]
        //public void ActionMethod() {
        //    // Define an Action in the UI that can execute your custom code or invoke a dialog for processing information gathered from end-users (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
        //}



        // [Indexed("TipoFolio; Tipo; Clave", Unique = true)]
        // [Indexed("Tipo;Clave", Unique = true)]

        protected const ushort LONCVE = 8; // 10;
        const ushort LONMOD = 20;
        const ushort LONREC = 20;
        const ushort LONCONTRA = 10;
        const ushort LONSUPED = 20;
        const ushort LONCONDI = 250;

        //#region Properties
        // Falta ver algunas funciones que usaba para el set en cpp !!!
        private string FClave;
        [NonCloneable]
        [VisibleInListView(true)]
        [VisibleInDetailView(false)]
        [Index(1)]
        [Indexed("Tipo", Unique = true)]
        [Size(LONCVE)]
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
                        SetPropertyValue("Clave", ref FClave, ValorString("Clave", value));

                        if (Cadena.IsNumber(FClave))
                            SetPropertyValue("Clave", ref FClave, FClave.PadLeft(LONCVE, ' '));
                    }
                }
            }
        }

        public static string ClaveFto(string val)
        {
            string cve = string.Empty;

            if (val != null)
            {
                cve = val.Trim().Length > LONCVE ? val.Trim().Substring(0, LONCVE) : val.Trim();

                if (Cadena.IsNumber(cve))
                    cve = cve.PadLeft(LONCVE, ' ');
            }
            return cve;
        }

        // No lo hacemos small, porque debe llevar la hora también.
        private DateTime FFechaAlta;
        /// <summary>
        /// Fecha de alta en el sistema, por lo que no es capturable.
        /// </summary>
        // [Appearance("Documen.FechaAlta", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyyTHH:mm:ss}")]
        [NonCloneable]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public DateTime FechaAlta
        {
            get { return FFechaAlta; }
            set
            {
                SetPropertyValue("FechaAlta", ref FFechaAlta, value);
                if (IsNewObject())
                    FechaVigencia = value.AddDays(15); // Por qué 15 días?
            }
        }

        //#region + Fecha de vigencia, requisicion
        private DateTime FVigencia;
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public DateTime FechaVigencia
        {
            get { return FVigencia; }
            set { SetPropertyValue("FechaVigencia", ref FVigencia, value); }
        }

        private DocumentoTipo FTipo;
        /// <summary>
        /// Tipo de Documento, FCap no lo usa, pero LCap si lo debe usar?
        /// </summary>
        //[Appearance("Tipo", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Method = "MSinCfdi")]
        [VisibleInDetailView(true)]
        [VisibleInListView(true)]
        public DocumentoTipo Tipo
        {
            get { return FTipo; }
            set { SetPropertyValue("Tipo", ref FTipo, value); }
        }

        //#region + Causante
        /*
        // Como no existe la tabla ProveedorCliente, tons no hay relación... O debería de existir?
        // A lo mejor esto no es necesario
        private ProveedorCliente FCausante;
        // [NonPersistent]
        public ProveedorCliente Causante
        {
            get { return FCausante; }
            set { SetPropertyValue("Causante", ref FCausante, value); }
        }*/

        /*
        private ProveedorCliente FCausante;
        // Apply the Association attribute to mark the Customer property 
        // as the many end of the Customer-Orders association.
        [Association("ProveedorCliente-Documentos")]
        public ProveedorCliente Causante
        {
            get { return FCausante; }
            set { SetPropertyValue("Causante", ref FCausante, value); }
        }*/
        //#endregion

        private DocumentoStatus FStatus;
        [NonCloneable]
        [Index(3)]
        [VisibleInListView(true)]
        [VisibleInDetailView(false)]
        public DocumentoStatus Status
        {
            get { return FStatus; }
            set { SetPropertyValue("Status", ref FStatus, value); }
        }

        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [NonPersistent]
        public string StatusParc
        {
            get
            {
                /* Está activo en LCap Oct 2018
                if ((StatusEnlc == DocumentoStatus.Devuelta
                    || StatusEnlc == DocumentoStatus.Facturado
                    || StatusEnlc == DocumentoStatus.Ordenada
                    || StatusEnlc == DocumentoStatus.Pedida
                    || StatusEnlc == DocumentoStatus.Recibida
                    || StatusEnlc == DocumentoStatus.Remisionada)
                    && Seguimiento)
                    return string.Format("Parc. {0}", StatusEnlc);
                else*/
                    return StatusEnlc.ToString();
            }
        }

        private bool FEmitido;
        [NonCloneable]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public bool Emitido
        {
            get { return FEmitido; }
            set { SetPropertyValue("Emitido", ref FEmitido, value); }
        }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public string Original
        {
            get { return Emitido ? "COPIA" : "ORIGINAL"; }
        }


        /* TIT Pa qué, está en LCap Oct 2018
        #region + Concepto
        private ConceptoDocumento FConcepto;
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public ConceptoDocumento Concepto
        {
            get { return FConcepto; }
            set { SetPropertyValue("Concepto", ref FConcepto, value); }
        }
        #endregion*/

        private Moneda FMoneda;
        [ImmediatePostData]
        [VisibleInListView(false)]
        [RuleRequiredField("RuleRequiredField for Documento.Moneda", DefaultContexts.Save, "Debe asignar una Moneda", SkipNullOrEmptyValues = false)]
        public Moneda Moneda
        {
            get { return FMoneda; }
            set
            {
                SetPropertyValue("Moneda", ref FMoneda, value);
                if (!IsLoading && value != null)
                {
                    TipoCambio = value.MontoTC;
                    /*TIT Ya no se usa en la ver 3.3
                    AsignPorMone();*/             
                }
            }
        }


        [RuleFromBoolProperty("DocTipoC", DefaultContexts.Save, "El Tipo de Cambio debe ser mayor que 0")]
        protected bool DocTipoC
        {
            get { return TipoCambio > 0; }
        }

        private decimal FTipChange;
        [Appearance("Documento.TipoCambio", Context = "DetailView", Criteria = "Moneda.Clave = 'MXN'", Enabled = false, FontStyle = FontStyle.Italic)]
        [VisibleInListView(false)]
        [ModelDefault("DisplayFormat", "{0:c4}")]
        [ModelDefault("EditMask", "c4"), ModelDefault("EditMaskType", "Default")]
        public decimal TipoCambio
        {
            get { return FTipChange; }
            set { SetPropertyValue("TipoCambio", ref FTipChange, value); }
        }

        /*TIT, está en LCap Oct 2018
        #region + Almacen
        private Almacen FAlmacen;
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public Almacen Almacen
        {
            get { return FAlmacen; }
            set { SetPropertyValue("Almacen", ref FAlmacen, value); }
        }
        #endregion*/

        //#region + Elaboro
        // Parece que esto no es importante
        /* En LG lo usan y lo ponen en las observaciones
        private string FElaboro;
        [Size(20)]
        public string Elaboro   
        {
            get { return FElaboro; }
            set { SetPropertyValue("Elaboro", ref FElaboro, value); }
        }*/
        //#endregion

        private decimal FSubT;
        [VisibleInListView(false)]
        //[Appearance("SubTotal", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        public decimal SubTotal
        {
            // Espero pasarlo al negocio pero como todavia no puedo 
            // lo dejo aqui
            // Jul 2013
            get { return FSubT; }
            /*
            get 
            { 
                decimal sub = 0;
                foreach (Partida partida in LasPartidas())
                {
                    sub += partida.Importe;
                }
                return sub;
            }*/
            set { SetPropertyValue("SubTotal", ref FSubT, value); }
        }

        [Obsolete("Mejor usar SubTotal")]
        public decimal TotalSub
        {
            get
            {
                decimal sub = 0;
                foreach (Partida partida in LasPartidas())
                {
                    sub += Math.Round(partida.Importe, 2, MidpointRounding.AwayFromZero); 
                    // (decimal)partida.Cantidad * partida.MontoUnitario;
                }
                return sub;
            }
        }

        private decimal FDsctos;
        // [Appearance("DescuentoTotalV", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        [Appearance("DescuentoTotal", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Criteria = "DescuentoTotal = 0")]
        public decimal DescuentoTotal
        {
            get { return FDsctos; }
            set { SetPropertyValue("DescuentoTotal", ref FDsctos, value); }
        }

        private decimal FDescFinan;
        [VisibleInDetailView(false)]
        [XafDisplayName("Descuento Financiero")]
        // [Appearance("DescFinancieroV", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        [Appearance("DescFinanciero", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Criteria = "DescFinanciero = 0")]
        //[Appearance("DescFinanciero", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Method = "MRetenISR1")]
        public decimal DescFinanciero
        {
            get { return FDescFinan; }
            set { SetPropertyValue("DescFinanciero", ref FDescFinan, value); }
        }

        private decimal mImps1;
        [Appearance("Documento.Impuesto01", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Criteria = "Impuesto01 = 0")]
        // [Appearance("Dcmnt.Impuesto01", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        [VisibleInDetailView(false)]
        public decimal Impuesto01
        {
            get { return mImps1; }
            set { SetPropertyValue("Impuesto01", ref mImps1, value); }
        }

        private decimal Imps2;
        [Appearance("Documento.Impuesto02", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Criteria = "Impuesto02 = 0")]
        // [Appearance("Dcmnt.Impuesto02", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        //[VisibleInDetailView(false)]
        public decimal Impuesto02
        {
            get { return Imps2; }
            set { SetPropertyValue("Impuesto02", ref Imps2, value); }
        }

        private decimal Imps3;
        /// <summary>
        /// IEPS
        /// </summary>
        [Appearance("Documento.Impuesto03", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Criteria = "Impuesto03 = 0")]
        // [Appearance("Dcmnt.Impuesto03", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        //[VisibleInDetailView(false)]
        public decimal Impuesto03
        {
            get { return Imps3; }
            set { SetPropertyValue("Impuesto03", ref Imps3, value); }
        }

        //#region + Impuesto 04
        private decimal mImpuesto04;
        /// <summary>
        /// IVA
        /// </summary>
        [Appearance("Documento.Impuesto04", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Criteria = "Impuesto04 = 0")]
        // [Appearance("Impuesto04", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        // [System.ComponentModel.DisplayName("IVA")] Feb 2016 Ahora es por configuración !
        public decimal Impuesto04
        {
            get { return mImpuesto04; }
            set { SetPropertyValue("Impuesto04", ref mImpuesto04, value); }
        }

        [RuleFromBoolProperty("DocumentoTot", DefaultContexts.Save, "El Total debe ser mayor a 0")]
        protected bool DocumentoTot
        {
            get { return Total > 0; }
        }

        private decimal FTotal;
        [VisibleInLookupListView(true)]
        //[Appearance("Total", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        [Index(4)]
        [VisibleInListView(true)]
        public decimal Total
        {
            get { return FTotal; }
            set { SetPropertyValue("Total", ref FTotal, value); }
        }

        // private string FObserva;
        [Size(SizeAttribute.Unlimited)]
        [Delayed]
        public string Notas
        {
            get { return GetDelayedPropertyValue<string>("Notas"); }
            set { SetDelayedPropertyValue("Notas", value.Trim(new char[] { ' ', '\0' })); }
        }

        //#region + Modo de envío, mercancia o factura?
        private string FModoEnvio;
        [VisibleInDetailView(false)]
        [Size(LONMOD)]
        public string ModoEnvio
        {
            get { return FModoEnvio; }
            set { SetPropertyValue("ModoEnvio", ref FModoEnvio, ValorString("ModoEnvio", value)); }
        }

        //#region + Fecha de envío, factura o mercancia?
        private DateTime FechaE;
        [NonCloneable]
        [VisibleInDetailView(false)]
        public DateTime FechaEnvio
        {
            get { return FechaE; }
            set { SetPropertyValue("FechaEnvio", ref FechaE, value); }
        }

        //#region + Dirección de envío
        private Direccion FDireccionE;
        [VisibleInDetailView(false)]
        public Direccion DireccionEnvio
        {
            get { return FDireccionE; }
            set { SetPropertyValue("DireccionEnvio", ref FDireccionE, value); }
        }

        //#region + Recibió, a qué se refiere a la mercancia o a la factura para pago?
        private string FRecibio;
        [VisibleInDetailView(false)]
        [Size(LONREC)]
        public string Recibio
        {
            get { return FRecibio; }
            set { SetPropertyValue("Recibio", ref FRecibio, ValorString("Recibio", value)); }
        }

        //#region + Fecha de recepción, devolucion
        private DateTime FFechaRecibo;
        /// <summary>
        /// Fecha de pedido
        /// </summary>
        [NonCloneable]
        [VisibleInDetailView(false)]
        public DateTime FechaRecibo
        {
            get { return FFechaRecibo; }
            set { SetPropertyValue("FechaRecibo", ref FFechaRecibo, value); }
        }

        //#region + Numero de contrarecibo
        private string FContraRecibo;
        [VisibleInDetailView(false)]
        [Size(LONCONTRA)]
        public string ContraRecibo
        {
            get { return FContraRecibo; }
            set { SetPropertyValue("ContraRecibo", ref FContraRecibo, ValorString("ContraRecibo", value)); }
        }

        private string FSuPedido;
        // Pedido del cliente, no será orden de compra del cliente?
        // Lo usaré en la recepción de compra como el número de fac del prov.
        // Para el caso de una nota de crédito será usado como numero de factura.
        [VisibleInDetailView(false)]
        [Size(LONSUPED)]
        public string SuPedido
        {
            get { return FSuPedido; }
            set { SetPropertyValue("SuPedido", ref FSuPedido, ValorString("SuPedido", value)); }
        }

        private string FCondicion;
        [Size(LONCONDI)]
        public string Condicion
        {
            get { return FCondicion; }
            set { SetPropertyValue("Condicion", ref FCondicion, ValorString("Condicion", value)); }
        }

        //#region + Imagen
        private XPDelayedProperty bytesImagen = new XPDelayedProperty();
        // [Obsolete("Usar mejor ImagenName")]
        [VisibleInDetailView(false)]
        [NonPersistent]
        [Delayed("bytesImagen")]
        [ValueConverter(typeof(JpegStorageConverter))]
        public Image Imagen
        {
            get
            {
                if (!string.IsNullOrEmpty(ImagenName))
                    return Image.FromFile(ImagenName);
                else
                    return null;
                // return (Image)bytesImagen.Value; 
            }
            set
            {
                /*
                bytesImagen.Value = value;
                if (!IsLoading)
                    OnChanged("Imagen");*/
            }
        }

        private string mImagenName;
        [VisibleInLookupListView(false)]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public string ImagenName
        {
            get { return mImagenName; }
            set { SetPropertyValue("ImageName", ref mImagenName, ValorString("ImageName", value)); }
        }

        private bool FSegui;
        /// <summary>
        /// False si ya no hay nada que enlazar
        /// </summary>
        [NonCloneable]
        [VisibleInDetailView(false)]
        public bool Seguimiento
        {
            get { return FSegui; }
            set { SetPropertyValue("Seguimiento", ref FSegui, value); }
        }

        [VisibleInDetailView(false)]
        public string CantidadLetra
        {
            get
            {
                string text = apl.Log.Cadena.CantLetr(Total.ToString("F2"));

                if (Moneda == null)
                    Moneda = Session.FindObject<Moneda>(new BinaryOperator("Sistema", true));
                if (Moneda != null)
                {
                    if (Moneda.Idioma == EIdioma.Ingles)
                        text = Cadena.CantLetrDlls(Total.ToString());
                    text += Cadena.Terminacion(Convert.ToDecimal(Total.ToString("F2")), 
                        Moneda.Nombre, Moneda.Terminacion);
                }
                return text;
            }
        }

        [VisibleInDetailView(false)]
        [NonPersistent]
        public Documento Enlazado { get; set; }

        private decimal FGEnvio;
        [VisibleInDetailView(false)]
        public decimal GEnvio
        {
            get { return FGEnvio; }
            set { SetPropertyValue("GEnvio", ref FGEnvio, value); }
        }

        //#region + Fecha y hora del Documento
        private DateTime mFechaDoc;
        /// <summary>
        /// Fecha del Documento
        /// </summary>
        [XafDisplayName("Fecha Documento")]
        [NonCloneable]
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyyTHH:mm:ss}")]
        // [Appearance("FechaDoc", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        [VisibleInListView(true)]
        [Index(2)]
        public DateTime FechaDoc
        {
            get { return mFechaDoc; }
            set { SetPropertyValue("FechaDoc", ref mFechaDoc, value); }
        }

        private DateTime mFechaCan;
        /// <summary>
        /// Fecha de la cancelación
        /// </summary>
        [NonCloneable]
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyyTHH:mm:ss}")]
        // [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        // [Appearance("FechaCan", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        // [Appearance("FechaCanV", AppearanceItemType = "LayoutItem", Context = "DetailView", Criteria = "Status != 4", Visibility = ViewItemVisibility.Hide)]
        [VisibleInDetailView(true)]
        public DateTime FechaCan
        {
            get { return mFechaCan; }
            set { SetPropertyValue("FechaCan", ref mFechaCan, value); }
        }

        private Empresa mEmpresa;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        // Para imprimirlo en los ftos
        public Empresa Empresa 
        { 
            get 
            {
                if (mEmpresa == null)
                    mEmpresa = Session.FindObject<Empresa>(null);
                return mEmpresa;
            }
            set { SetPropertyValue("Empresa", ref mEmpresa, value); }
        }

        //#region TipoEnlace
        private DocumentoTipo mTipoE;
        /// <summary>
        /// Tipo de Documento al que se enlaza
        /// </summary>
        [NonCloneable]
        [VisibleInDetailView(false)]
        public DocumentoTipo TipoE
        {
            get { return mTipoE; }
            set { SetPropertyValue("TipoE", ref mTipoE, value); }
        }

        private EnvioCorreo mEnvioC;
        [DevExpress.Xpo.DisplayName("Enviado Correo")]
        [NonCloneable]
        [Index(5)]
        [VisibleInListView(true)]
        [VisibleInDetailView(false)]
        public EnvioCorreo EnvioC
        {
            get { return mEnvioC; }
            set { SetPropertyValue("EnvioC", ref mEnvioC, value); }
        }

        //* Lo usa LCap Oct 2018
        //#region + Status seguimiento
        private DocumentoStatus mStatusEnlc;
        /// <summary>
        /// Lleva el estado del documento, si está enlazado a otro o no.
        /// </summary>
        [NonCloneable]
        [VisibleInDetailView(false)]
        public DocumentoStatus StatusEnlc
        {
            get { return mStatusEnlc; }
            set { SetPropertyValue("StatusEnlc", ref mStatusEnlc, value); }
        }

        private string mDocEnlace;
        [Appearance("Documento.DocEnlace", AppearanceItemType = "LayoutItem", Context = "DetailView", Criteria = "Tipo = 'Orden'", Visibility = ViewItemVisibility.Hide)]
        [VisibleInListView(false)]
        [NonPersistent]
        public string DocEnlace
        {
            get { return mDocEnlace; }
            set { SetPropertyValue("DocEnlace", ref mDocEnlace, value); }
        }

        [RuleFromBoolProperty("FacturaItemsNum", DefaultContexts.Save, "Debe capturar al menos una Partida")]
        protected bool DocItemsNum
        {
            get { return LasPartidas() != null && LasPartidas().Count > 0; }
        }

        private string mDmcl;
        //[Appearance("Documento.Domicilio", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        [VisibleInListView(false)]
        [NonPersistent]
        public string Domicilio
        {
            get
            {
                return (Causante == null || Causante.Compania == null || Causante.Compania.Direccion == null)
              ? string.Empty
              : Causante.Compania.Direccion.Domicilio;
            }
            set { SetPropertyValue("Domicilio", ref mDmcl, value); }
        }

        private string mMotivCan;
        // [Appearance("MotivCanV", AppearanceItemType = "LayoutItem", Context = "DetailView", Criteria = "Status != 'Cancelado'", Visibility = ViewItemVisibility.Hide)]
        [VisibleInListView(false)]
        [Size(20)]
        public string MotivCan
        {
            get { return mMotivCan; }
            set { SetPropertyValue("MotivCan", ref mMotivCan, value); }
        }

        private float mTasa1;
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        /// <summary>
        /// Tasa del Impuesto1
        /// </summary>
        public float Tasa1
        {
            get { return mTasa1; }
            set { SetPropertyValue("Tasa1", ref mTasa1, value); }
        }

        private float mTasa2;
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        /// <summary>
        /// Tasa del Impuesto2
        /// </summary>
        public float Tasa2
        {
            get { return mTasa2; }
            set { SetPropertyValue("Tasa2", ref mTasa2, value); }
        }

        private float mTasa3;
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        /// <summary>
        /// Tasa del Impuesto3
        /// </summary>
        public float Tasa3
        {
            get { return mTasa3; }
            set { SetPropertyValue("Tasa3", ref mTasa3, value); }
        }

        private float mTasa4;
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        /// <summary>
        /// Tasa del Impuesto4
        /// </summary>
        public float Tasa4
        {
            get { return mTasa4; }
            set { SetPropertyValue("Tasa4", ref mTasa4, value); }
        }

        /* Esto estaba veamos si no causa ruido el cambio
         * Oct 2018
        private DcmntSttsEnlc mStatusEnlc;
        /// <summary>
        /// Lleva el estado del documento, si está enlazado a otro o no.
        /// </summary>
        [VisibleInDetailView(false)]
        [NonCloneable]
        public DcmntSttsEnlc StatusEnlc
        {
            get { return mStatusEnlc; }
            set { SetPropertyValue("StatusEnlc", ref mStatusEnlc, value); }
        }*/


        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Emitido = false;
            FechaAlta = DateTime.Now;
            FechaDoc = DateTime.Now;
            FechaRecibo = DateTime.Today;
            GEnvio = 0;
            Imagen = null;
            ImagenName = string.Empty;
            Impuesto04 = 0;
            Notas = string.Empty;
            TipoCambio = 0;
            Status = DocumentoStatus.Alta;
            Seguimiento = true;

            EnvioC = EnvioCorreo.NoEnviado;
            StatusEnlc = DocumentoStatus.Ninguno;
            Moneda = Session.FindObject<Moneda>(new BinaryOperator("Sistema", true));
            Empresa = Session.FindObject<Empresa>(null);
        }

        // #region + Causante
        [VisibleInDetailView(false)]
        public virtual ProveedorCliente Causante
        {
            get { return null; }
        }

        public virtual XPCollection LasPartidas()
        {
            return null;
        }
    }

    public enum DocumentoTipo
    {
        [XafDisplayName("Cotización")]
        Cotizacion = 1,
        Pedido = 2, // Lo usa LCap
        [XafDisplayName("Remisión")]
        Remision = 3,
        [ImageName("BO_Invoice")]
        Factura = 4,
        [ImageName("Cash_Drawer_Out"), XafDisplayName("Nota de Crédito") /*Devolución venta")*/]
        DevolucionVenta = 5,
        [XafDisplayName("Requisición")]
        Requisicion = 6, // Lo usa LCap
        Orden = 7, // Lo usa LCap
        [XafDisplayName("Recepción")]
        Recepcion = 8, // Lo usa LCap
        [XafDisplayName("Devolución compra")]
        DevolucionCompra = 9, // Lo usa LCap
        Ticket = 10, // Lo usa LCap
        [ImageName("Cash_Drawer_In"), XafDisplayName("Nota de Cargo")]
        NotaCargo = 11,
        /*
        Honorario = 12,
        Todos = 13,*/
        Ninguno = 14,   // Lo usa LCap
        CartaPorte = 15
    }
}