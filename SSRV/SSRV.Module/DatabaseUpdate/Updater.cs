using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using Cap.Generales.BusinessObjects.General;
using SSRV.Module.BusinessObjects.Config;
using Cap.Clientes.BusinessObjects.Clientes;
using Cap.Clientes.BusinessObjects.Generales;
using SSRV.Module.BusinessObjects.Servicio;
using SSRV.Module.BusinessObjects.Admin;
using DevExpress.ExpressApp.Dashboards;
using SSRV.Module.Properties;

namespace SSRV.Module.DatabaseUpdate
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion)
        {
        }
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();
            //string name = "MyName";
            //DomainObject1 theObject = ObjectSpace.FindObject<DomainObject1>(CriteriaOperator.Parse("Name=?", name));
            //if(theObject == null) {
            //    theObject = ObjectSpace.CreateObject<DomainObject1>();
            //    theObject.Name = name;
            //}
            Datos();

            PermissionPolicyUser sampleUser = ObjectSpace.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", "soporte"));
            if (sampleUser == null)
            {
                sampleUser = ObjectSpace.CreateObject<PermissionPolicyUser>();
                sampleUser.UserName = "soporte";
                sampleUser.SetPassword("");
            }
            PermissionPolicyRole defaultRole = CreateDefaultRole();
            sampleUser.Roles.Add(defaultRole);

            PermissionPolicyUser userAdmin = ObjectSpace.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", "root"));
            if (userAdmin == null)
            {
                userAdmin = ObjectSpace.CreateObject<PermissionPolicyUser>();
                userAdmin.UserName = "root";
                // Set a password if the standard authentication type is used
                userAdmin.SetPassword("");
            }
            // If a role with the Administrators name doesn't exist in the database, create this role
            PermissionPolicyRole adminRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "Administrators"));
            if (adminRole == null)
            {
                adminRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                adminRole.Name = "Administrators";
            }
            adminRole.IsAdministrative = true;
            userAdmin.Roles.Add(adminRole);


            DashboardsModule.AddDashboardData<DashboardData>(
                    ObjectSpace, "Servicios", Resources.Servicios);
            ObjectSpace.CommitChanges(); //This line persists created object(s).
        }
        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }
        private PermissionPolicyRole CreateDefaultRole()
        {
            PermissionPolicyRole defaultRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "Default"));
            if (defaultRole == null)
            {
                defaultRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                defaultRole.Name = "Default";

                defaultRole.AddObjectPermission<PermissionPolicyUser>(SecurityOperations.Read, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
                defaultRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "StoredPassword", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
                defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);

                defaultRole.AddTypePermissionsRecursively<Catalogo>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<Config>(SecurityOperations.FullObjectAccess, SecurityPermissionState.Allow);

                defaultRole.AddTypePermissionsRecursively<Cliente>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<Clasificacion>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);

                defaultRole.AddTypePermissionsRecursively<Servicio>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<Poliza>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<Incidencia>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);

                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Configuración/Items/Config_ListView", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Configuración/Items/Catalogo_ListView", SecurityPermissionState.Allow);

                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Clientes/Items/Cliente_ListView", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Clientes/Items/Clasificacion_ListView", SecurityPermissionState.Allow);

                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Servicios/Items/Servicio_ListView", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Servicios/Items/Poliza_ListView", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Servicios/Items/Incidencia_ListView", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Servicios/Items/NewIncidencia", SecurityPermissionState.Allow);
            }
            return defaultRole;
        }

        private void Datos()
        {
        }
    }
}
