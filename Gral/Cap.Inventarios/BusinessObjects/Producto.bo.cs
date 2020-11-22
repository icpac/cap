using System;
using System.ComponentModel;
using System.Drawing;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Editors;
using Cap.Generales.BusinessObjects.Unidades;
using DevExpress.ExpressApp.DC;
using Cap.Fe.BusinessObjects;

namespace Cap.Inventarios.BusinessObjects
{
    [XafDisplayName("Servicios")] // Mejor modificarlo en FCap, por ejemplo, pero no sé si afecte la bd de lo que existe
    [NavigationItem("Inventarios")]
    [Appearance("Suspendido", TargetItems = "*", Context = "ListView", Criteria = "[Status] == 4", FontColor = "Green", BackColor = "AliceBlue")]
    [ImageName("BO_Product")]
    [DefaultProperty("DisplayLook")]
    public partial class Producto
    {
        const ushort LONCVE = 20;  //*/ 17; 
        const ushort LONDES = 120; // 50

        private string FClave;
        [Index(0)]
        [RuleRequiredField("RuleRequiredField for Producto.Clave", DefaultContexts.Save, "Debe capturar la Clave", SkipNullOrEmptyValues = false)]
        [Size(LONCVE), Indexed(Unique = true)]
        public string Clave
        {
            get { return FClave; }
            set
            {
                if (value != null)
                {
                    if (IsLoading)
                        SetPropertyValue("Clave", ref FClave, value);
                    else
                    {
                        if (apl.Log.Cadena.IsNumber(value.Trim()))
                            SetPropertyValue("Clave", ref FClave, value.Trim().PadLeft(LONCVE, ' '));
                        else
                            SetPropertyValue("Clave", ref FClave, ValorString("Clave", value.ToUpper()));
                    }
                }
            }
        }

        public static string ClaveFto(string val)
        {
            string cve;

            if (apl.Log.Cadena.IsNumber(val.Trim()))
                cve = val.Trim().PadLeft(LONCVE, ' ');
            else
                cve = val.Trim().Length > LONCVE ? val.Trim().Substring(0, LONCVE).ToUpper() : val.Trim().ToUpper();

            return cve;
        }

        private string FAlias;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(LONCVE), Indexed]
        public string Alias
        {
            get { return FAlias; }
            set { SetPropertyValue("Alias", ref FAlias, ValorString("Alias", value)); }
        }
        
        private string FDescrip;
        [Index(1)]
        [DevExpress.Xpo.DisplayName("Descripción")]
        [Size(LONDES)]
        public string Descripcion
        {
            get { return FDescrip/*.Trim()*/; }
            set { SetPropertyValue("Descripcion", ref FDescrip, ValorString("Descripcion", value)); }
        }

        private string FDescripL;
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(SizeAttribute.Unlimited)]
        public string DescripcionLarga
        {
            get { return FDescripL; }
            set { SetPropertyValue("DescripcionLarga", ref FDescripL, value); }
        }

        //#region + Linea
        /*>
        private Linea FLinea;
        // [Columna(Name = "Linea.Clave", Caption = "Linea", VisibleIndex = 2)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public Linea Linea
        {
            get { return FLinea; }
            set { SetPropertyValue("Linea", ref FLinea, value); }
        }*/

        private bool FLotes;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        //[NonPersistent] TIT jul 2018
        public bool Lotes
        {
            get { return FLotes; }
            set { SetPropertyValue("Lotes", ref FLotes, value); }
        }

        private bool FPedimento;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public bool Pedimento
        {
            get { return FPedimento; }
            set { SetPropertyValue("Pedimento", ref FPedimento, value); }
        }

        //#region + Serie, maneja números
        private bool FSerie;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public bool Serie
        {
            get { return FSerie; }
            set { SetPropertyValue("Serie", ref FSerie, value); }
        }

        private string FNotas;
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(SizeAttribute.Unlimited)]
        public string Notas
        {
            get { return FNotas; }
            set { SetPropertyValue("Notas", ref FNotas, value != null ? value.Trim(new char[] { ' ', '\0' }) : string.Empty); }
        }

        private EsquemaImpuesto FEsquema;
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public EsquemaImpuesto Esquema
        {
            get { return FEsquema; }
            set { SetPropertyValue("Esquema", ref FEsquema, value); }
        }

        private Marca FMarca;
        [VisibleInLookupListView(false)]
        public Marca Marca
        {
            get { return FMarca; }
            set { SetPropertyValue("Marca", ref FMarca, value); }
        }

        /* Será mejor usarlo así ? Jun 2018
        private CatalogoProducto mMrca;
        [DataSourceCriteria("Tp = 'Marca'")]
        [XafDisplayName("Marca")]
        public CatalogoProducto Mrca
        {
            get { return mMrca; }
            set { SetPropertyValue("Mrca", ref mMrca, value); }
        }*/

        /* Jun 2018
        private CatalogoProducto mLna;
        [DataSourceCriteria("Tp = 'Linea'")]
        [XafDisplayName("Línea")]
        public CatalogoProducto Lna
        {
            get { return mLna; }
            set { SetPropertyValue("Lna", ref mLna, value); }
        }*/

        // Será que lo podemos modelar como lo haciamos antes?
        // Acumulado de todos los almacenes
        private double FExist;
        [Appearance("Producto.Existencia", Context = "DetailView", Enabled = false, FontStyle = System.Drawing.FontStyle.Italic)]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        // [NonPersistent] TIT jul 2018
        public double Existencia
        {
            get { return FExist; }
            set { SetPropertyValue("Existencia", ref FExist, value); }
        }

        // Podría ser el minimo de todos los almacenes
        private double FMinimo;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [XafDisplayName("Existencia minima")]
        public double StockMinimo
        {
            get { return FMinimo; }
            set { SetPropertyValue("StockMinimo", ref FMinimo, value); }
        }

        // podria ser el maximo de todos los almacenes
        private double FMaximo;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public double StockMaximo
        {
            get { return FMaximo; }
            set { SetPropertyValue("StockMaximo", ref FMaximo, value); }
        }

        private double FEProduccion;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public double EProduccion
        {
            get { return FEProduccion; }
            set { SetPropertyValue("EProduccion", ref FEProduccion, value); }
        }

        private double mEFisica;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public double EFisica
        {
            get { return mEFisica; }
            set { SetPropertyValue("EFisica", ref mEFisica, value); }
        }

        private double mEApartado;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public double EApartado
        {
            get { return mEApartado; }
            set { SetPropertyValue("EFisica", ref mEApartado, value); }
        }

        //#region No logré que esto lo hiciera el grid, si lo encuentro pues luego quito esto
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public double Disponible
        {
            get { return Existencia - EApartado; }
        }

        // Por el momento todos tienen el mismo precio publico, aunque el almacen también lo tiene
        private decimal FPrecPub;
        [XafDisplayName("Precio Publico")]
        public decimal PrecioPublico
        {
            get { return FPrecPub; }
            set { SetPropertyValue("PrecioPublico", ref FPrecPub, value); }
        }

        private ProductoPrecios mPrcPblc;
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [DataSourceProperty("Precios")]
        [XafDisplayName("Precio Público")]
        public ProductoPrecios PrcPblc
        {
            get { return mPrcPblc; }
            set { SetPropertyValue("PrcPblc", ref mPrcPblc, value); }
        }

        [VisibleInDetailView(false)]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        [Association("Producto-Precios", typeof(ProductoPrecios)), DevExpress.Xpo.Aggregated]
        public XPCollection Precios
        {
            get { return GetCollection("Precios"); }
        }

        private decimal FPrecMin;
        [Obsolete("En su lugar usar Precios")]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public decimal PrecioMinimo
        {
            get { return FPrecMin; }
            set { SetPropertyValue("PrecioMinimo", ref FPrecMin, value); }
        }

        //#region Factores de incremento
        private float FIncrementoP;
        [XafDisplayName("Incremento Precio")]
        // Nov 2020, por qué tenía esto ?
        // [ModelDefault("DisplayFormat", "{0:n3}%")]
        [ModelDefault("DisplayFormat", "{0:n1}%")]
        [VisibleInListView(false)]
        //[VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        public float IncrementoP
        {
            get { return FIncrementoP; }
            set { SetPropertyValue("IncrementoP", ref FIncrementoP, value); }
        }

        private float FIncrementoM;
        [Obsolete("En su lugar usar Precios")]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public float IncrementoM
        {
            get { return FIncrementoM; }
            set { SetPropertyValue("IncrementoM", ref FIncrementoM, value); }
        }

        private decimal FCtoProm;
        [Appearance("Producto.CostoPromedio", Context = "DetailView", Enabled = false, FontStyle = System.Drawing.FontStyle.Italic)]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        public decimal CostoPromedio
        {
            get { return FCtoProm; }
            set { SetPropertyValue("CostoPromedio", ref FCtoProm, value); }
        }

        private decimal FCtoUlt;
        [Appearance("Producto.CostoUltimo", Context = "DetailView", Enabled = false, FontStyle = System.Drawing.FontStyle.Italic)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public decimal CostoUltimo
        {
            get { return FCtoUlt; }
            set { SetPropertyValue("CostoUltimo", ref FCtoUlt, value); }
        }

        // Suma de todos los almacenes
        private double FPorRecibir;
        [Appearance("Producto.PorRecibir", Context = "DetailView", Enabled = false, FontStyle = System.Drawing.FontStyle.Italic)]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        public double PorRecibir
        {
            get { return FPorRecibir; }
            set { SetPropertyValue("PorRecibir", ref FPorRecibir, value < 0 ? 0 : value); }
        }

        private double FPorSurtir;
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [Appearance("Producto.PorSurtir", Context = "DetailView", Enabled = false, FontStyle = System.Drawing.FontStyle.Italic)]
        public double PorSurtir
        {
            get { return FPorSurtir; }
            set { SetPropertyValue("PorSurtir", ref FPorSurtir, value); }
        }

        private DateTime FFechUltCmp;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public DateTime FUltimaCompra
        {
            get { return FFechUltCmp; }
            set { SetPropertyValue("FUltimaCompra", ref FFechUltCmp, value); }
        }

        //#region + Fecha de ultima venta
        private DateTime FFechUltVta;
        [DevExpress.Xpo.DisplayName("F. Últ. Venta")]
        [Appearance("FUltimaVenta", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        public DateTime FUltimaVenta
        {
            get { return FFechUltVta; }
            set { SetPropertyValue("FUltimaVenta", ref FFechUltVta, value); }
        }

        //#region + Fecha de alta
        private DateTime FFechaAlta;
        [Appearance("FechaAlta", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        public DateTime FechaAlta
        {
            get { return FFechaAlta; }
            set { SetPropertyValue("FechaAlta", ref FFechaAlta, value); }
        }

        // Todos los almacenes usarán el mismo costeo
        private EProductoCosteo FCosteo;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        // [NonPersistent]  Nov 2020
        public EProductoCosteo Costeo
        {
            get { return FCosteo; }
            set { SetPropertyValue("Costeo", ref FCosteo, value); }
        }

        //#region + Unidad de entrada
        // Para que tenga sentido la existencia, aunque se puede hacer un proceso 
        // que convierta entre unidades, pero por lo pronto todos usan la misma unidad
        private Unidad FUEntrada;
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public Unidad UEntrada
        {
            get { return FUEntrada; }
            set { SetPropertyValue("UEntrada", ref FUEntrada, value); }
        }

        //#region + Unidad de salida
        private Unidad FUSalida;
        [RuleRequiredField("RuleRequiredField for Producto.USalida", DefaultContexts.Save, "Debe capturar la Unidad", SkipNullOrEmptyValues = false)]
        [DevExpress.Xpo.DisplayName("Unidad")]
        public Unidad USalida
        {
            get { return FUSalida; }
            set { SetPropertyValue("USalida", ref FUSalida, value); }
        }

        //#region  + Unidad del producto
        private Unidad FUProducto;
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        public Unidad UProducto
        {
            get { return FUProducto; }
            set { SetPropertyValue("UProducto", ref FUProducto, value); }
        }

        private float FEquivalencia;
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        public float Equivalencia
        {
            get { return FEquivalencia; }
            set { SetPropertyValue("Equivalencia", ref FEquivalencia, value); }
        }

        // Hay que ver si sirve, porque el sae no lo usa...
        private Moneda FMoneda;
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public Moneda Moneda
        {
            get { return FMoneda; }
            set { SetPropertyValue("Moneda", ref FMoneda, value); }
        }

        private StatusTipo status;
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public StatusTipo Status
        {
            get { return status; }
            set { SetPropertyValue("Status", ref status, value); }
        }

        /*TI, lo malo de ponerlo en la bd es que ocupa mucho espacio, 
         aunque se dice que para eso son las bd
         Si lo dejamos en archivo el problema es el trabajo en Red 
         Optemos por el momento en que esté en la bd por ser inmediato 
         Y porque suponemos que son muy pocos productos */
        [ImageEditor(ListViewImageEditorCustomHeight = 75, DetailViewImageEditorFixedHeight = 150)]
        [XafDisplayName("Imágen")]
        [Delayed, /*ImageEditor,*/ VisibleInListView(true)]
        public byte[] Image
        {
            get { return GetDelayedPropertyValue<byte[]>("Image"); }
            set { SetDelayedPropertyValue<byte[]>("Image", value); }
        }

        // private XPDelayedProperty bytesImagen = new XPDelayedProperty();
        // [Delayed("bytesImagen")]
        // [ValueConverter(typeof(JpegStorageConverter))]
        [Obsolete("Usar mejor ImagenName")]
        // private Image mImagen;
        // [Columna(VisibleIndex = 6)]
        public Image Imagen
        {
            get
            {
                return null;
                /*TI 
                try
                {
                    if (!string.IsNullOrEmpty(ImagenName))
                        return Image.FromFile(ImagenName);
                    else
                        return null; // mImagen; //  (Image)bytesImagen.Value;
                }
                catch (Exception)
                {
                    return null;
                }*/
            }
            set
            {
                /*
                bytesImagen.Value = value;
                if (!IsLoading)
                    OnChanged("Imagen");*/
                // SetPropertyValue("Imagen", ref mImagen, value);
            }
        }

        private string mImagenName;
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [Obsolete("Mejor usar Imagen")]
        public string ImagenName
        {
            get { return mImagenName; }
            set { SetPropertyValue("ImageName", ref mImagenName, ValorString("ImageName", value)); }
        }

        //#region + Proveedores
        /* Por gordura May 2009
        [Association("Producto-Proveedores", typeof(ProveedorCosto)), Aggregated]
        public XPCollection Proveedores
        {
            get { return GetCollection("Proveedores"); }
        }*/

        //#region + KitItems
        /*>
        // Apply the Association attribute to mark the Orders property 
        // as the one end of the Customer-Orders association.
        [VisibleInDetailView(false)]
        [Association("Producto-Items", typeof(KitItem)), DevExpress.Xpo.Aggregated]
        public XPCollection Items
        {
            get { return GetCollection("Items"); }
        }*/

        //#region + Colores y Tallas del Producto
        /*>
        [VisibleInDetailView(false)]
        [Association("Producto-ColorTallas", typeof(ColorTalla)), DevExpress.Xpo.Aggregated]
        public XPCollection ColorTallas
        {
            get { return GetCollection("ColorTallas"); }
        }*/
        // [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [Association("Producto-ColorTallas", typeof(ColorTalla)), DevExpress.Xpo.Aggregated]
        public XPCollection ColorTallas
        {
            get { return GetCollection("ColorTallas"); }
        }

        //#region + Tipo (Producto o Servicio)
        private EProductoTipo mTipo;
        [ImmediatePostData]
        [VisibleInListView(false)]
        public EProductoTipo Tipo
        {
            get { return mTipo; }
            set { SetPropertyValue("Tipo", ref mTipo, value); }
        }

        //#region + Porc de Retención de ISR
        [RuleFromBoolProperty("Producto.PRetIsr", DefaultContexts.Save, "El Porcentaje de Retención de ISR debe estar entre 0 y 100")]
        protected bool PRetIsrOk
        {
            get { return Tipo != EProductoTipo.Servicio || (PRetIsr >= 0 && PRetIsr < 100); }
        }

        private float mPRetIsr;
        [Obsolete("En su lugar usar EsquemaImpuesto")]
        [Appearance("PRetIsr", AppearanceItemType = "LayoutItem", Context = "DetailView", Criteria = "Tipo != 2", Visibility = ViewItemVisibility.Hide)]
        [ModelDefault("DisplayFormat", "{0:n2}%")]
        [VisibleInListView(false)]
        // [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        // [NonPersistent]
        public float PRetIsr
        {
            get { return mPRetIsr; }
            set { SetPropertyValue("PRetIsr", ref mPRetIsr, value); }
        }

        //#region + Porc de Retención de IVA
        [RuleFromBoolProperty("Producto.PRetIva", DefaultContexts.Save, "El Porcentaje de Retención de IVA debe estar entre 0 y 100")]
        protected bool PRetIvaOk
        {
            get { return Tipo != EProductoTipo.Servicio || (PRetIva >= 0 && PRetIva < 100); }
        }

        private float mPRetIva;
        [Obsolete("En su lugar usar EsquemaImpuesto")]
        [Appearance("PRetIva", AppearanceItemType = "LayoutItem", Context = "DetailView", Criteria = "Tipo != 2", Visibility = ViewItemVisibility.Hide)]
        [ModelDefault("DisplayFormat", "{0:n3}%")]
        [VisibleInListView(false)]
        // [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        // [NonPersistent]
        public float PRetIva
        {
            get { return mPRetIva; }
            set { SetPropertyValue("PRetIva", ref mPRetIva, value); }
        }

        //#region + Porc de Descuento
        private float mPDesc;
        /// <summary>
        /// La P es de Porcentaje, creo
        /// </summary>        
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public float PDesc
        {
            get { return mPDesc; }
            set { SetPropertyValue("PDesc", ref mPDesc, value); }
        }

        //#region + Sku (Número de referencia)
        private string mSku;
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        [Size(30)]
        public string Sku
        {
            get { return mSku; }
            set { SetPropertyValue("Sku", ref mSku, ValorString("Sku", value)); }
        }

        //#region + Cap en Fac, descripcion
        private bool mCapEnFact;
        [Index(2)]
        [DevExpress.Xpo.DisplayName("Captura Descripción en Factura")]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public bool CapEnFact
        {
            get { return mCapEnFact; }
            set { SetPropertyValue("CapEnFact", ref mCapEnFact, value); }
        }

        //#region + Tiempo de Surtido
        private int mTiempoSurtido;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public int TiempoSurtido
        {
            get { return mTiempoSurtido; }
            set { SetPropertyValue("TiempoSurtido", ref mTiempoSurtido, value); }
        }

        //#region + Ctrl de Almacén
        private string mCtrlAlm;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        [Size(10)]
        public string CtrlAlm
        {
            get { return mCtrlAlm; }
            set { SetPropertyValue("CtrlAlm", ref mCtrlAlm, value); }
        }

        private float mTiempo;
        /// <summary>
        /// Para Servicios, lo que lleva hacerlo medido en minutos
        /// </summary>
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [NonPersistent]
        public float Tiempo
        {
            get { return mTiempo; }
            set { SetPropertyValue("Tiempo", ref mTiempo, value); }
        }

        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [NonPersistent]
        public string DisplayLook
        {
            get { return string.Format("{0} - {1}", Clave, Descripcion); }
        }

        /*
        private EsquemaImpuesto FEsquemaLcl1;
        /// <summary>
        /// Impuesto local 1
        /// </summary>
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        public EsquemaImpuesto EsquemaLcl1
        {
            get { return FEsquemaLcl1; }
            set { SetPropertyValue("EsquemaLcl1", ref FEsquemaLcl1, value); }
        }

        private EsquemaImpuesto FEsquemaLcl2;
        /// <summary>
        /// Impuesto local 2
        /// </summary>
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        public EsquemaImpuesto EsquemaLcl2
        {
            get { return FEsquemaLcl2; }
            set { SetPropertyValue("EsquemaLcl2", ref FEsquemaLcl2, value); }
        }

        private EsquemaImpuesto FEsquemaLcl3;
        /// <summary>
        /// Impuesto local 3
        /// </summary>
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        public EsquemaImpuesto EsquemaLcl3
        {
            get { return FEsquemaLcl3; }
            set { SetPropertyValue("EsquemaLcl3", ref FEsquemaLcl3, value); }
        }*/

        private bool mPrVnta;
        [DevExpress.Xpo.DisplayName("Se muestra en Ventas")]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        /// <summary>
        /// Determina qué productos se muestran al facturar
        /// </summary>
        public bool PrVnta
        {
            get { return mPrVnta; }
            set { SetPropertyValue("PrVnta", ref mPrVnta, value); }
        }

        private bool mImprmFrmt;
        [DevExpress.Xpo.DisplayName("Se imprime en Formato")]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        /// <summary>
        /// Determina qué productos se muestran al facturar
        /// </summary>
        public bool ImprmFrmt
        {
            get { return mImprmFrmt; }
            set { SetPropertyValue("ImprmFrmt", ref mImprmFrmt, value); }
        }

        private ProductoServicio mPrdctSrvcCfdi;
        [VisibleInListView(false)]
        // Todavía no sé si es como una clasificación
        [XafDisplayName("Clave CFDI")]
        public ProductoServicio PrdctSrvcCfdi
        {
            get { return mPrdctSrvcCfdi; }
            set { SetPropertyValue("PrdctSrvcCfdi", ref mPrdctSrvcCfdi, value); }
        }

        private bool mFchCdcdd;
        [VisibleInListView(false)]
        [DevExpress.Xpo.DisplayName("Con Fecha de Caducidad")]
        public bool FchCdcdd
        {
            get { return mFchCdcdd; }
            set { SetPropertyValue("FchCdcdd", ref mFchCdcdd, value); }
        }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [XafDisplayName("Costo Total")]
        public decimal CstTtl
        {
            get { return (decimal)Existencia * CostoUltimo; }
        }

        [VisibleInLookupListView(false)]
        public double PorComprar
        {
            get
            {
                if (StockMaximo > 0)
                {
                    if (StockMinimo > 0)
                    {
                        if (StockMinimo > Existencia)
                            return StockMaximo - Existencia;
                        else
                            return 0;
                    }
                    else
                        return StockMaximo - Existencia;
                }
                else
                    return 0;
            }
        }

        private Medicamento mMdcmnt;
        [DevExpress.Xpo.Aggregated]
        [ExpandObjectMembers(ExpandObjectMembers.Always)]
        public Medicamento Mdcmnt
        {
            get { return mMdcmnt; }
            set { SetPropertyValue("Mdcmnt", ref mMdcmnt, value); }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            CapEnFact = false;
            Clave = string.Empty;
            Costeo = EProductoCosteo.Promedio;
            CostoPromedio = 0;
            CostoUltimo = 0;
            CtrlAlm = string.Empty;
            Descripcion = string.Empty;

            FechaAlta = DateTime.Today;
            FUltimaCompra = DateTime.Today;
            FUltimaVenta = DateTime.Today;
            Equivalencia = 1.0f;
            Esquema = Session.FindObject(typeof(EsquemaImpuesto), new BinaryOperator("Clave", "1")) as EsquemaImpuesto;
            Existencia = 0;
            EProduccion = 0;
            EFisica = 0;
            EApartado = 0;

            ImagenName = string.Empty;

            Lotes = false;
            Marca = null;
            Notas = string.Empty;

            Pedimento = false;
            PorRecibir = 0;
            PorSurtir = 0;
            PrecioMinimo = 0;
            PrecioPublico = 0;

            PDesc = 0;
            /*
            PRetIsr = 10;
            PRetIva = 66.67f;*/

            Serie = false;
            Sku = string.Empty;
            Status = StatusTipo.Activo;

            StockMaximo = 0;
            StockMinimo = 0;

            TiempoSurtido = 0;
            Tipo = EProductoTipo.Producto;

            // Esto puede no ser lo ideal cuando paso datos de una bd a otra y para qué hacer doble busqueda !
            UEntrada =
            USalida =
            UProducto = (Unidad)Session.FindObject(typeof(Unidad), new BinaryOperator("Clave", "PZA"));
            Moneda = Session.FindObject<Moneda>(new BinaryOperator("Sistema", true));

            Tiempo = 0;

            PrVnta = true;
            ImprmFrmt = true;

            Mdcmnt = new Medicamento(Session);
        }


        protected override void OnSaving()
        {
            if (Mdcmnt == null)
                Mdcmnt = new Medicamento(Session);

            base.OnSaving();
        }

        [RuleFromBoolProperty("PrecioP", DefaultContexts.Save, "El Precio Publico debe ser mayor o igual a 0")]
        protected bool PrecioP
        {
            get { return PrecioPublico >= 0; }
        }

        [RuleFromBoolProperty("CostoUltimoOk", DefaultContexts.Save, "El Costo debe ser mayor o igual a 0")]
        protected bool CostoUltimoOk
        {
            get { return CostoUltimo >= 0; }
        }

        [RuleFromBoolProperty("IncrementoPOk", DefaultContexts.Save, "El Incremento debe ser mayor o igual a 0 y menor a 100")]
        protected bool IncrementoPOk
        {
            get { return IncrementoP >= 0 && IncrementoP < 100; }
        }






        [RuleFromBoolProperty("Producto.minimaMaxima", DefaultContexts.Save, "La existencia Máxima debe ser mayor que la mínima ")]
        protected bool minimaMaximaOk
        {
            get { return StockMaximo >= StockMinimo; }
        }

        [RuleFromBoolProperty("Producto.Prc", DefaultContexts.Save, "El Precio debe ser mayor o igual a CERO")]
        protected bool PrcOk
        {
            get { return PrecioPublico >= 0; }
        }

        [RuleFromBoolProperty("Producto.ExstncMnm", DefaultContexts.Save, "La Existencia mínima debe ser mayor o igual a CERO")]
        protected bool ExstncMnmOk
        {
            get { return StockMinimo >= 0; }
        }

        [RuleFromBoolProperty("Producto.ExstncMxm", DefaultContexts.Save, "La Existencia Máxima debe ser mayor o igual a CERO")]
        protected bool ExstncMxmOk
        {
            get { return StockMaximo >= 0; }
        }



        [Action(ToolTip = "Activo, se muestra en las ayudas", TargetObjectsCriteria = "Stts = 'Baja'")]
        public void Activa()
        {
            Status = StatusTipo.Activo;
        }

        [Action(ToolTip = "Baja, NO se muestra en las ayudas", TargetObjectsCriteria = "Stts = 'Activo'")]
        public void Baja()
        {
            Status = StatusTipo.Suspendido; //  EProductoStatus.Baja;
        }


        //#region + Valida objeto
        public bool ValidaObjeto()
        {
            return !string.IsNullOrEmpty(Clave); // != string.Empty && Nombre != string.Empty;
        }
        //#endregion

        [NonPersistent]
        static public string ClaveMoneda { get; set; }
    }
}
