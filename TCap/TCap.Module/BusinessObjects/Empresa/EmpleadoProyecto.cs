using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using Cap.Nomina.BusinessObjects;

namespace TCap.Module.BusinessObjects.Empresa
{
    [ImageName("Payment")]
    [NavigationItem("Nomina")]
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    // TIT No sé si jale ! public class Empleado : EmpleadoBase, ISecurityUser, IAuthenticationStandardUser,
    public class EmpleadoProyecto : Empleado
    /*TIT Nov 2018 no todos los usuarios del sistema son Empleados  
    , IPermissionPolicyUser, ISecurityUser, ISecurityUserWithRoles, 
    IAuthenticationActiveDirectoryUser, IAuthenticationStandardUser*/

    /*Nov 2018 ICanInitialize*//*, IOperationPermissionProvider*/
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public EmpleadoProyecto(Session session)
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
        //    set { SetPropertyValue("PersistentProperty", ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}


        /*
        #region ISecurityUser Members
        private bool isActive = true;
        [XafDisplayName("Está Activo")]
        public bool IsActive
        {
            get { return isActive; }
            set { SetPropertyValue("IsActive", ref isActive, value); }
        }
        #endregion*/


        /* Este también está en ISecurityUser
        #region IAuthenticationStandardUser Members*/
        /*
        private string userName = String.Empty;
        [XafDisplayName("Nombre de Usuario")]
        [RuleRequiredField("EmployeeUserNameRequired", DefaultContexts.Save)]
        [RuleUniqueValue("EmployeeUserNameIsUnique", DefaultContexts.Save,
            "El inicio de sesión con el nombre de usuario introducido ya se ha registrado en el sistema.")]
        public string UserName
        {
            get { return userName; }
            set { SetPropertyValue("UserName", ref userName, value); }
        }*/

        /*
        private bool changePasswordOnFirstLogon;
        [XafDisplayName("Cambiar contraseña al iniciar sesión")]
        [VisibleInListView(false)]
        public bool ChangePasswordOnFirstLogon
        {
            get { return changePasswordOnFirstLogon; }
            set
            {
                SetPropertyValue("ChangePasswordOnFirstLogon", ref changePasswordOnFirstLogon, value);
            }
        }

        public bool ComparePassword(string password)
        {
            return PasswordCryptographer.VerifyHashedPasswordDelegate(this.storedPassword, password);
            // return SecurityUserBase.ComparePassword(this.storedPassword, password);
        }

        public void SetPassword(string password)
        {
            // this.storedPassword = new PasswordCryptographer().GenerateSaltedPassword(password);
            this.storedPassword = PasswordCryptographer.HashPasswordDelegate(password);
            OnChanged("StoredPassword");
        }
        #endregion*/




        /*
        #region ISecurityUserWithRoles Members
        IList<ISecurityRole> ISecurityUserWithRoles.Roles
        {
            get
            {
                IList<ISecurityRole> result = new List<ISecurityRole>();
                foreach (EmpleadoRol role in GetSecurityRoles())
                {
                    result.Add(role);
                }
                return result;
            }
        }
        #endregion*/



        /*
        [Obsolete("Ya no se hará así")]
        [Association("EmpleadoProyecto-EmpleadoRoles")]
        [RuleRequiredField("EmployeeRoleIsRequired", DefaultContexts.Save,
            TargetCriteria = "IsActive",
            CustomMessageTemplate = "Un Empleado activo debe tener al menos un rol asignado")]
        public XPCollection<EmpleadoRol> EmpleadosRoles
        {
            get
            {
                return GetCollection<EmpleadoRol>("EmpleadosRoles");
            }
        }*/


        /*
        #region IOperationPermissionProvider Members
        IEnumerable<IOperationPermission> IOperationPermissionProvider.GetPermissions()
        {
            return new IOperationPermission[0];
        }
        IEnumerable<IOperationPermissionProvider> IOperationPermissionProvider.GetChildren()
        {
            return new EnumerableConverter<IOperationPermissionProvider, EmpleadoRol>(EmpleadosRoles);
        }
        #endregion*/


        /*
        #region IPermissionPolicyUser Members
        IEnumerable<IPermissionPolicyRole> IPermissionPolicyUser.Roles
        {
            get { return EmpleadosRoles.OfType<IPermissionPolicyRole>(); }
        }
        #endregion*/


        /*Nov 2018
        #region ICanInitialize Members
        void ICanInitialize.Initialize(IObjectSpace objectSpace, SecurityStrategyComplex security)
        {
            / *TIT
            PracownikRola newUserRole = (PracownikRola)objectSpace.FindObject<PracownikRola>(
                new BinaryOperator("Name", security.NewUserRoleName));
            if (newUserRole == null)
            {
                newUserRole = objectSpace.CreateObject<PracownikRola>();
                newUserRole.Name = security.NewUserRoleName;
                newUserRole.IsAdministrative = true;
                newUserRole.Pracownicy.Add(this);
            }* /
        }
        #endregion*/


        /* Esto para qué fue ? No está en las interfaz implementadas
         private string storedPassword;
        [Browsable(false), Size(SizeAttribute.Unlimited), Persistent, SecurityBrowsable]
        protected string StoredPassword
        {
            get { return storedPassword; }
            set { storedPassword = value; }
        }

        protected virtual IEnumerable<ISecurityRole> GetSecurityRoles()
        {
            IList<ISecurityRole> result = new List<ISecurityRole>();
            foreach (EmpleadoRol role in EmpleadosRoles)
            {
                result.Add(role);
            }
            return new ReadOnlyCollection<ISecurityRole>(result);
        }*/
    }
}