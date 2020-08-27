#region Copyright (c) 2017-2020 cjlc
/*
{+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++)
{                                                                   }
{     cjlc Cap control administrativo personal                      }
{                                                                   }
{     Copyrigth (c) 2017-2020 cjlc                                  }
{     Todos los derechos reservados                                 }
{                                                                   }
{*******************************************************************}
 */
#endregion

using apl.Log;
using Cap.Generales.BusinessObjects.General;
using Cap.Generales.BusinessObjects.Unidades;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Drawing;

namespace Cap.Bancos.BusinessObjects
{
    [DefaultProperty("DisplayLook")]
    [NavigationItem("Bancos")]
    [Appearance("M.Transito", TargetItems = "*", Context = "ListView", Criteria = "[Status] = 'Transito'", FontColor = "Orange")]
    [Appearance("ActionVisibility", Id = "Delete", AppearanceItemType = "Action", TargetItems = "Delete", Criteria = "Status != 'Cancelado'", Context = "Any", Enabled = false)]
    [Appearance("M.Cancel", TargetItems = "*", Context = "ListView", Criteria = "[Status] = 'Cancelado'", FontColor = "Red")]
    [Appearance("M.List", TargetItems="Cuenta", Context = "ListView", Visibility=ViewItemVisibility.Hide)]
    [ImageName("Refund")]
    public partial class MovimientoB
    {
        private Bancaria FCuenta;
        [DataSourceCriteria("Status != 'Baja'")]
        [Indexed]
        // >[Association("CuentaMovimientos")]
        public Bancaria Cuenta
        {
            get { return FCuenta; }
            set
            {
                SetPropertyValue("Cuenta", ref FCuenta, value);
                if (IsLoading)
                    CuentaA = value;
                if (value != null && IsNewObject())
                    SaldoAnterior = value.SaldoFinal;
            }
        }

        /// <summary>
        /// Para uso interno
        /// </summary>
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [NonPersistent]
        public Bancaria CuentaA { get; set; }

        private ConceptoB FConcepto;
        // [Association("Concepto-Movimientos")]
        [ImmediatePostData]
        [RuleRequiredField("RuleRequiredField for MovimientoB.ConceptoA", 
            DefaultContexts.Save, "Debe asignar un Concepto", SkipNullOrEmptyValues = false)]
        public ConceptoB Concepto
        {
            get { return FConcepto; }
            set
            {
                SetPropertyValue("Concepto", ref FConcepto, value);
                if (FConcepto != null && IsNewObject())
                    ConceptoTipo = FConcepto.Tipo;

                if (IsLoading)
                    ConceptoA = value;
            }
        }

        /// <summary>
        /// Para uso interno
        /// </summary>
        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [NonPersistent]
        public ConceptoB ConceptoA { get; set; }

        //#region + Tipo concepto, creo que se refiere a A o C o H para cheque y es siempre cargo
        private EConceptoTipo FConceptoTipo;
        [Appearance("ConceptoTipo", Context = "DetailView", Enabled = false, 
            FontStyle = FontStyle.Italic)]
        public EConceptoTipo ConceptoTipo
        {
            get { return FConceptoTipo; }
            set { SetPropertyValue("ConceptoTipo", ref FConceptoTipo, value); }
        }

        private decimal monto;
        public decimal Monto
        {
            get { return monto; }
            set
            {
                SetPropertyValue("Monto", ref monto, value);
                if (IsLoading)
                    MontoA = value;
            }
        }

        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [NonPersistent]
        public decimal MontoA { get; set; }

        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [Appearance("MontoAplicar", Context = "DetailView", Enabled = false, 
            FontStyle = FontStyle.Italic)]
        [VisibleInListView(false)]
        [NonPersistent]
        public decimal MontoAplicar
        {
            get { return monto * TipoCambio; }
        }

        private DateTime fAplicacion;
        [NonCloneable]
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        public DateTime FechaAplicacion
        {
            get { return fAplicacion; }
            set
            {
                SetPropertyValue("FechaAplicacion", ref fAplicacion, value);

                if (IsLoading)
                    FAplicacionBak = value;
            }
        }

        // Lo uso por ejemplo para el número de cheque.
        private int FFolio;
        [VisibleInLookupListView(false)]
        [VisibleInListView(false)]
        [XafDisplayName("Núm. Cheque")]
        [Appearance("Folio", AppearanceItemType = "LayoutItem", Context = "DetailView", 
            Criteria = "Cuenta.Tipo != 'Maestra'", Visibility = ViewItemVisibility.Hide)]
        public int Folio
        {
            get { return FFolio; }
            set { SetPropertyValue("Folio", ref FFolio, value); }
        }

        private string referencia;
        [VisibleInListView(false)]
        [Size(12)]
        public string Referencia
        {
            get { return referencia; }
            set { SetPropertyValue("Referencia", ref referencia, ValorString("Referencia", value)); }
        }

        /// <summary>
        /// Los movimientos cancelados no pueden ser conciliados.
        /// </summary>
        private MovimientoStatus FStatus;
        public MovimientoStatus Status
        {
            get { return FStatus; }
            set
            {
                SetPropertyValue("Status", ref FStatus, value);
                if (IsLoading)
                    StatusA = value;
            }
        }

        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [NonPersistent]
        public MovimientoStatus StatusA { get; set; }

        //#region + Emision de movimiento
        private bool FEmitido;
        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public bool Emitido
        {
            get { return FEmitido; }
            set { SetPropertyValue("Emitido", ref FEmitido, value); }
        }

        //#region + Conciliado
        private bool FConciliado;
        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public bool Conciliado
        {
            get { return FConciliado; }
            set { SetPropertyValue("Conciliado", ref FConciliado, value); }
        }

        //#region + Tipo movimiento, normal, cheque, gasto
        private EMovimientoTipo FTipo;
        public EMovimientoTipo Tipo
        {
            get { return FTipo; }
            set
            {
                SetPropertyValue("Tipo", ref FTipo, value);
                if (IsLoading)
                    TipoA = Tipo;
            }
        }

        /// <summary>
        /// Para uso interno
        /// </summary>
        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [NonPersistent]
        public EMovimientoTipo TipoA { get; set; }



        //#region + Saldo anterior, creo que ya no se usa
        /// <summary>
        /// Se puede usar para saber como estaba la cuenta al hacer este movimiento.
        /// </summary>
        private decimal FSaldoAnterior;
        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [Appearance("SaldoAnterior", Context = "DetailView", Enabled = false, 
            FontStyle = FontStyle.Italic)]
        [VisibleInListView(false)]
        [NonPersistent]
        public decimal SaldoAnterior
        {
            get { return FSaldoAnterior; }
            set { SetPropertyValue("SaldoAnterior", ref FSaldoAnterior, value); }
        }
        //#endregion


        //#region + Fecha de alta, registro
        private DateTime fRegistro;
        [NonCloneable]
        [Appearance("FechaAlta", Context = "DetailView", Enabled = false, 
            FontStyle = FontStyle.Italic)]
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        [VisibleInListView(false)]
        public DateTime FechaAlta
        {
            get { return fRegistro; }
            set { SetPropertyValue("FechaAlta", ref fRegistro, value); }
        }
        //#endregion


        //#region + Fecha aplicacion original
        // Se usa al cambiar la fecha de aplicacion
        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [NonPersistent]
        public DateTime FAplicacionBak { get; set; }
        //#endregion

        //#region + Moneda
        private Moneda FMoneda;
        [RuleRequiredField("RuleRequiredField for MovimientoB.Moneda", 
            DefaultContexts.Save, "Debe asignar una Moneda", SkipNullOrEmptyValues = false)]
        public Moneda Moneda
        {
            get { return FMoneda; }
            set { SetPropertyValue("Moneda", ref FMoneda, value); }
        }
        //#endregion

        //#region + Tipo de cambio
        private decimal tipoCambio;
        [VisibleInLookupListView(false)]
        [Appearance("MovimientoB.TipoCambio", Context = "DetailView", 
            Criteria = "Moneda.Clave = 'MXN'", Enabled = false, FontStyle = FontStyle.Italic)]
        [VisibleInListView(false)]
        public decimal TipoCambio
        {
            get { return tipoCambio; }
            set
            {
                SetPropertyValue("TipoCambio", ref tipoCambio, value);

                if (IsLoading)
                    TipoCambioA = value;
            }
        }

        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [NonPersistent]
        public decimal TipoCambioA { get; set; }
        //#endregion

        //#region + Notas
        // private string observa;
        [Size(SizeAttribute.Unlimited)]
        [Delayed]
        public string Notas
        {
            get { return GetDelayedPropertyValue<string>("Notas"); /*  observa;*/ }
            set { SetDelayedPropertyValue("Notas", value); /*SetPropertyValue("Notas", ref observa, value);*/ }
        }

        // Lo movimos a Cadena pero luego hay que quitarlo de aqui
        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [Appearance("CantidadLetra", Context = "DetailView", Enabled = false, 
            FontStyle = FontStyle.Italic)]
        [VisibleInListView(false)]
        [NonPersistent]
        public string CantidadLetra
        {
            get
            {
                string text;

                if (Moneda != null)
                {
                    text = string.Format("{0}{1}", Cadena.CantLetr(Monto.ToString()), 
                        Cadena.Terminacion(Monto, Moneda.Nmbr /*.Nombre*/, Moneda.Terminacion));
                }
                else
                    text = "Falta asignar la cuenta";

                return text;
            }
        }

        // Para la agenda, pues el dlg pasa un objeto y la agenda es otro, entonces al guardar hacemos el 
        // desma y por eso agregue estas non persistents
        //
        //#region + Periodicidad
        private EPeriodicidad FPeriodicidad;
        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public EPeriodicidad Periodo
        {
            get { return FPeriodicidad; }
            set { SetPropertyValue("Periodo", ref FPeriodicidad, value); }
        }

        //#region + Numero de incidencias
        private short FIncidencias;
        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [NonPersistent]
        public short Incidencias
        {
            get { return FIncidencias; }
            set { SetPropertyValue("Incidencias", ref FIncidencias, value); }
        }

        // Para el cheque, me dijeron que era bueno saber a qué proveedor se le paga.
        // Pero nada mas necesito el nombre al capturar, digo yo.
        #region + Proveedor
        /*
        private Proveedor FProveedor;
        public Proveedor Proveedor
        {
            get { return FProveedor; }
            set { SetPropertyValue("Proveedor", ref FProveedor, value); }
        }*/
        #endregion

        /*
        private EPagoTipo FFormaPago;
        [Obsolete("Pues hay que recompilar para agregar más formas de pago. En su lugar usar FrmPg")]
        [VisibleInListView(false)]
        public EPagoTipo FormaPago
        {
            get { return FFormaPago; }
            set { SetPropertyValue("FormaPago", ref FFormaPago, value); }
        }*/

        /*
        private Catalogo mFrmPg;
        [Obsolete("En su lugar usar FrmPgB")]
        [DataSourceCriteria("Tipo == 'FormaPago'")]
        [XafDisplayName("Forma de Pago")]
        [VisibleInListView(false)]
        public Catalogo FrmPg
        {
            get { return mFrmPg; }
            set { SetPropertyValue("FrmPg", ref mFrmPg, value); }
        }*/

        private CatalogoBancos mFrmPgB;
        [DataSourceCriteria("Tp == 'FormaPago'")]
        [XafDisplayName("Forma de Pago")]
        [VisibleInListView(false)]
        public CatalogoBancos FrmPgB
        {
            get { return mFrmPgB; }
            set { SetPropertyValue("FrmPgB", ref mFrmPgB, value); }
        }

        //#region + Libre alfanumérico
        private string FLibreCadena00;
        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [Size(20)]
        public string LibreCadena00
        {
            get { return FLibreCadena00; }
            set { SetPropertyValue("LibreCadena00", ref FLibreCadena00, ValorString("LibreCadena00", value)); }
        }

        
        private FileData mArchvImgn;
        [NonCloneable]
        [VisibleInLookupListView(false)]
        [VisibleInListView(false)]
        [DevExpress.Xpo.DisplayName("Archivo Imagen")]
        [FileTypeFilter("Imagenes", 1, "*.bmp", "*.png", "*.gif", "*.jpg")]
        [FileTypeFilter("Todos", 2, "*.*")]
        public FileData ArchvImgn
        {
            get { return mArchvImgn; }
            set { SetPropertyValue("ArchvImgn", ref mArchvImgn, value); }
        }


        private string mImagenName;
        [Obsolete("Usar mejor MyFileData")]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public string ImagenName
        {
            get { return mImagenName; }
            set { SetPropertyValue("ImageName", ref mImagenName, ValorString("ImageName", value)); }
        }


        [Obsolete("Usar mejor MyFileData")]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [NonPersistent]
        public Image Imagen
        {
            get
            {
                try
                {
                    if (!string.IsNullOrEmpty(ImagenName))
                        return Image.FromFile(ImagenName);
                    else
                        return null;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        private DateTime mFchCan;
        [Appearance("FchCanV", AppearanceItemType = "LayoutItem", Context = "DetailView", 
            Criteria = "Status != 'Cancelado'", Visibility = ViewItemVisibility.Hide)]
        [Appearance("FchCan", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        [ModelDefault("DisplayFormat", "{0:dd MMM yy}")]
        /// <summary>
        /// La fecha de cancelación
        /// </summary>
        public DateTime FchCan
        {
            get { return mFchCan; }
            set { SetPropertyValue("FchCan", ref mFchCan, value); }
        }

        private MovimientoB mRlcnd;
        [VisibleInLookupListView(false)]
        [XafDisplayName("Relacionado")]
        [Appearance("MovimientoB.RlcndR", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        [Appearance("MovimientoB.Rlcnd", AppearanceItemType = "LayoutItem", Context = "DetailView", 
            Criteria = "Rlcnd = null", Visibility = ViewItemVisibility.Hide)]
        public MovimientoB Rlcnd
        {
            get { return mRlcnd; }
            set { SetPropertyValue("Rlcnd", ref mRlcnd, value); }
        }

        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [NonPersistent]
        public string DisplayLook
        {
            get 
            {
                string aux = string.Empty;

                if (Cuenta != null || Concepto != null)
                {
                    if (Cuenta != null)
                        aux += $"{Cuenta.Clave} - {Cuenta.Descripcion} - ";
                    aux += string.Format(" {0:c2}", Monto);
                    if (Concepto != null)
                        aux += $" - {Concepto.Descripcion}";
                }
                else
                    aux = $"{Monto:c2}";

                return aux;
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ConceptoTipo = EConceptoTipo.Ninguno;
            Conciliado = false;
            Cuenta = null;
            Emitido = false;

            FechaAlta = DateTime.Today;
            FechaAplicacion = DateTime.Today;
            Folio = 0;
            Monto = 0.0m;
            Notas = string.Empty;
            Referencia = string.Empty;
            Status = MovimientoStatus.Firme;
            SaldoAnterior = 0.0m;
            Tipo = EMovimientoTipo.Normal;
            TipoCambio = 1.0m;
            Periodo = EPeriodicidad.Invalido;
            Moneda = Session.FindObject(typeof(Moneda), new BinaryOperator("Sistema", true)) as Moneda;
            FrmPgB = Session.FindObject(typeof(CatalogoBancos), new BinaryOperator("Nmbr", "Efectivo")) as CatalogoBancos;

        }

        [RuleFromBoolProperty("MovimientoB.Monto", DefaultContexts.Save, "El Monto debe ser diferente de 0")]
        protected bool MontoOk
        {
            get { return Monto != 0; }
        }

        [RuleFromBoolProperty("MovimientoB.Cuenta", DefaultContexts.Save, "Debe capturar una Cuenta")]
        protected bool CuentaOk
        {
            get { return (Tipo == EMovimientoTipo.Gasto && Cuenta == null) ||
                    (Tipo != EMovimientoTipo.Gasto && Cuenta != null); }
        }
    }
}
