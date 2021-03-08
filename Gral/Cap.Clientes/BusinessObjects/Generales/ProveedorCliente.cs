using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using Cap.Generales.BusinessObjects.Object;
using DevExpress.ExpressApp.ConditionalAppearance;
using Cap.Generales.BusinessObjects.Empresa;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using apl.Log;
using DevExpress.ExpressApp.DC;

namespace Cap.Clientes.BusinessObjects.Generales
{
    [Appearance("ProveedorCliente.Status", TargetItems = "*", Context = "ListView", Criteria = "Status = 'Suspendido'", FontColor = "tomato"/*, FontStyle = FontStyle.Strikeout*/)]
    [Appearance("Saldo", TargetItems = "Compania.Nombre;Status", Criteria = "Saldo > 0", BackColor = "MistyRose")]
    [DefaultProperty("DisplayLook")]
    [NonPersistent]
    //[DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    public class ProveedorCliente : PObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public ProveedorCliente(Session session)
            : base(session)
        {
        }
        /*
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        }*/
        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue("PersistentProperty", ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
        //    this.PersistentProperty = "Paid";
        //}




        protected const ushort LONCLAVE = 15;   // 6;
        protected const ushort LONATNVTA = 25; // 30
        const ushort LONATNPAG = 25; // 30
        protected const ushort LONATNMAIL = 25;
        const ushort LONATNTEL = 15;

        //#region + Tipo causante
        // Tal vez no es necesario porque Cada uno tiene su propia tabla
        private ECausanteTipo FTipo;
        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        public ECausanteTipo Tipo
        {
            get { return FTipo; }
            set { SetPropertyValue("Tipo", ref FTipo, value); }
        }

        private UInt16 FDiasCredito;
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public UInt16 DiasCredito
        {
            get { return FDiasCredito; }
            set { SetPropertyValue("DiasCredito", ref FDiasCredito, value); }
        }

        private DateTime FFAlta;
        // Creo que tengo el dato para todo registro, será ?
        [DevExpress.Xpo.DisplayName("Fecha Alta")]
        [Appearance("FAlta", Context = "DetailView", Enabled = false, FontStyle = System.Drawing.FontStyle.Italic)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        [ModelDefault("HighlightHolidays", "True")]
        public DateTime FAlta
        {
            get { return FFAlta; }
            set { SetPropertyValue("FAlta", ref FFAlta, value); }
        }

        private DateTime FFUltMovimiento;
        [DevExpress.Xpo.DisplayName("Fch. Últ. Mov.")]
        [Appearance("FUltMovimiento", Context = "DetailView", Enabled = false, FontStyle = System.Drawing.FontStyle.Italic)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        public DateTime FUltMovimiento
        {
            get { return FFUltMovimiento; }
            set { SetPropertyValue("FUltMovimiento", ref FFUltMovimiento, value); }
        }

        private decimal FSaldo;
        [Appearance("PC.Saldo", Enabled = false, FontStyle = System.Drawing.FontStyle.Italic, FontColor = "Gray")]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        public decimal Saldo
        {
            get { return FSaldo; }
            set { SetPropertyValue("Saldo", ref FSaldo, value); }
        }

        //#region + Monto anual
        // Deberiamos moverlo a una tabla por años...
        /*
        private decimal FMontoAnual;
        [Obsolete("Tal vez hacer un reporte")]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        public decimal MontoAnual
        {
            get { return FMontoAnual; }
            set { SetPropertyValue("MontoAnual", ref FMontoAnual, value); }
        }*/

        // Ago 2018, No será mejor usar CatalogoClientes?
        //#region + Clasificacion
        private Clasificacion FClasifica;
        [XafDisplayName("Clasificación")]
        [VisibleInListView(false)]
        public Clasificacion Clasifica
        {
            get { return FClasifica; }
            set { SetPropertyValue("Clasifica", ref FClasifica, value); }
        }

        private decimal FLimCredito;
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        public decimal LimCredito
        {
            get { return FLimCredito; }
            set { SetPropertyValue("LimCredito", ref FLimCredito, value); }
        }

        [RuleFromBoolProperty("ProveedorCliente_PorcDescuentoOk", DefaultContexts.Save, "El Porcentaje de Descuento debe estar entre 0 y 100")]
        protected bool ProveedorCliente_PorcDescuentoOk
        {
            get { return PorcDescuento >= 0 && PorcDescuento < 100; }
        }

        //#region + Porcentaje de descuento
        private float FPorcDescuento;
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:n2}%")]
        [ModelDefault("EditMask", "P2"), ModelDefault("EditMaskType", "Default")]
        public float PorcDescuento
        {
            get { return FPorcDescuento; }
            set { SetPropertyValue("PorcDescuento", ref FPorcDescuento, value); }
        }

        //#region + Atencion ventas compras, que tal si se usa Contactos ?
        /*
        private string FAtencionVtsCmp;
        [DevExpress.Xpo.DisplayName("Atención Cmprs.")]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(LONATNVTA)]
        public string AtencionVts
        {
            get { return FAtencionVtsCmp; }
            set
            {
                if (value != null)
                    SetPropertyValue("AtencionVts", ref FAtencionVtsCmp, value.Trim().Length > LONATNVTA ? value.Trim().Substring(0, LONATNVTA) : value.Trim());
            }
        }*/
        //#endregion

        //#region + Atencion pagos(tesoreria?) cobranza, usamos Contactos?
        /*
        private string FAtencionPgsCbr;
        [DevExpress.Xpo.DisplayName("Atención Pags.")]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(LONATNPAG)]
        public string AtencionCbr
        {
            get { return FAtencionPgsCbr; }
            set
            {
                if (value != null)
                    SetPropertyValue("AtencionCbr", ref FAtencionPgsCbr, value.Trim(new char[] { ' ', '\0' }).Length > LONATNPAG ? value.Trim(new char[] { ' ', '\0' }).Substring(0, LONATNPAG) : value.Trim(new char[] { ' ', '\0' }));
            }
        }*/
        //#endregion

        //#region + Atencion meil
        /* TI Usamos Contactos ?
        private string FAtencionCbrEmail;
        [DevExpress.Xpo.DisplayName("Atncn Pags. Email")]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        // [VisibleInDetailView(false)]
        [Size(LONATNMAIL)]
        // [NonPersistent]
        public string AtencionCbrEmail
        {
            get { return FAtencionCbrEmail; }
            set { SetPropertyValue("AtencionCbrEmail", ref FAtencionCbrEmail, value); }
        }*/
        //#endregion

        //#region + Atencion telefono
        /*
        // O mejor como persona?
        private string FAtencionTelefono;
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [Size(LONATNTEL)]
        [NonPersistent]
        public string AtencionTelefono
        {
            get { return FAtencionTelefono; }
            set { SetPropertyValue("AtencionTelefono", ref FAtencionTelefono, value); }
        }*/
        //#endregion

        //#region + Rfc, RFC, CURP y Direccion los tenemos en compania
        //#endregion

        private Compania FCompania;
        [ExpandObjectMembers(ExpandObjectMembers.Always)]
        public Compania Compania
        {
            get { return FCompania; }
            set { SetPropertyValue("Compania", ref FCompania, value); }
        }

        [VisibleInDetailView(false)]
        [NonPersistent]
        [VisibleInLookupListView(true)]
        public string Nombre
        {
            get { return Compania != null ? Compania.Nombre : string.Empty; }
        }

        //#region + Notas, observacion
        private XPDelayedProperty mNts = new XPDelayedProperty();
        // private string FNotas;
        /* No recuerdo por qué le puse esto
        [ModelDefault("EditMask", @"[A-Z0-9]{1,12}")]
        [ModelDefault("EditMaskType", "RegEx")]*/
        [Delayed("mNts", true)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(SizeAttribute.Unlimited)]
        public string Notas
        {
            get { return (string)mNts.Value; }
            set { mNts.Value = value; }
            /*
            get { return FNotas; }
            set { SetPropertyValue("Notas", ref FNotas, value.Trim(new char[] { ' ', '\0' })); }*/
        }

        private EStatusPrvdClnt /*TI StatusTipo*/ FStatus;
        [VisibleInListView(true)]
        [VisibleInLookupListView(false)]
        public EStatusPrvdClnt /*StatusTipo*/ Status
        {
            get { return FStatus; }
            set { SetPropertyValue("Status", ref FStatus, value); }
        }

        //#region + Acreedor/Deudor
        // Uno que no provee o se le provee, cosas que se venden sino que se usan internamente.
        private bool FDor;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public bool Dor
        {
            get { return FDor; }
            set { SetPropertyValue("Dor", ref FDor, value); }
        }

        /* Creo que no mas tiene que ir en proveedores ? Será?
        private string FClabe;
        [Size(18)]
        public string Clabe
        {
            get { return FClabe; }
            set
            {
                SetPropertyValue("Clabe", ref FClabe,
                    value.Trim().Length > 18 ? value.Trim().Substring(0, 18) : value.Trim());
            }
        }*/

        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [NonPersistent]
        public string DisplayLook
        {
            get { return string.Format("{0} - {1}", getClave(), Compania != null ? Compania.Nombre : string.Empty); }
        }

        private string mCuenta;
        [VisibleInDetailView(true)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(20)]
        public string Cuenta
        {
            get { return mCuenta; }
            set { SetPropertyValue("Cuenta", ref mCuenta, value); }
        }

        /*
        Estaban antes pero no sé para qué

        [VisibleInLookupListView(false)]
        [VisibleInListView(true)]
        [NonPersistent]
        public string Correo
        {
            get { return Compania != null ? Compania.Direccion.Email : string.Empty; }
        }

        [VisibleInLookupListView(false)]
        [VisibleInListView(true)]
        [NonPersistent]
        public string Telefono
        {
            get { return Compania != null ? Compania.Direccion.TelefonoGeneral : string.Empty; }
        }
        */


        //#region + override After construction
        // Falta el último proveedor último cliente
        // Función de si es Baja el ProveedorCliente
        public override void AfterConstruction()
        {
            base.AfterConstruction();

            /*
            AtencionVts = string.Empty;*/
            Clasifica = null;
            Compania = new Compania(Session);
            Dor = false;
            FFAlta = DateTime.Today;
            FUltMovimiento = DateTime.Today;
            /*
            MontoAnual = 0.0m;*/
            Notas = string.Empty;
            Saldo = 0.0m;
            Status = EStatusPrvdClnt /*StatusTipo*/.Activo;
            Tipo = ECausanteTipo.Ninguno;
            Cuenta = string.Empty;
        }

        public static string ClaveFto(string val)
        {
            string cve = val.Trim().Length > LONCLAVE ? val.Trim().Substring(0, LONCLAVE).ToUpper() : val.Trim().ToUpper();

            if (Cadena.IsNumber(cve))
                cve = cve.PadLeft(LONCLAVE, ' ');
            return cve;
        }

        protected virtual string getClave() { return string.Empty; }

        [Action(ToolTip = "Suspende, no se muestra en las ayudas", TargetObjectsCriteria = "Status = 'Activo'")]
        public void Suspende()
        {
            Status = EStatusPrvdClnt /*StatusTipo*/.Suspendido;
        }

        [Action(ToolTip = "Activa, se muestra en las ayudas", TargetObjectsCriteria = "Status = 'Suspendido'")]
        public void Activa()
        {
            Status = EStatusPrvdClnt /*StatusTipo*/.Activo;
        }


        [RuleFromBoolProperty("ProveedorCliente_LimCreditoOk", DefaultContexts.Save, "El Límite debe ser mayor o igual a cero")]
        protected bool ProveedorCliente_LimCreditoOk
        {
            get { return LimCredito >= 0; }
        }
    }

    // Dejamos este pero es mejor como en Catalogo particularizar para no tener conflictos entre clases
    //#region + Status tipo enums
    public enum EStatusPrvdClnt
    {
        // Status de empresa, sucursal.
        /*
        Activa = 1,*/
        Baja = 2,
        Activo = 3,
        Suspendido = 4,
        /*
        Todos = 5,
        Indefinido = 0,
        // Para clientes
        Prospecto = 6*/
    }
}
