using System;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Web;
using System.Collections.Generic;
using DevExpress.ExpressApp.Web.SystemModule;
using DevExpress.Xpo.DB;
using System.Web;
using LCap.Module;
using ChangeDatabase.Module.Web;
using System.Web.Security;
using DevExpress.ExpressApp.Security;

namespace LCap.Web
{
    // You can override various virtual methods and handle corresponding events to manage various aspects of your XAF application UI and behavior.
    public partial class LCapAspNetApplication : WebApplication
    { // http://documentation.devexpress.com/#Xaf/DevExpressExpressAppWebWebApplicationMembersTopicAll
        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule module2;
        private LCap.Module.LCapModule module3;
        private LCap.Module.Web.LCapAspNetModule module4;
        private DevExpress.ExpressApp.Validation.ValidationModule validationModule1;
        private DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule conditionalAppearanceModule1;
        private DevExpress.ExpressApp.Security.SecurityStrategyComplex securityStrategyComplex1;
        private DevExpress.ExpressApp.Security.AuthenticationStandard authenticationStandard1;
        private DevExpress.ExpressApp.Security.SecurityModule securityModule1;
        private DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule businessClassLibraryCustomizationModule1;
        private DevExpress.ExpressApp.Chart.ChartModule chartModule1;
        private DevExpress.ExpressApp.CloneObject.CloneObjectModule cloneObjectModule1;
        private DevExpress.ExpressApp.ReportsV2.ReportsModuleV2 reportsModuleV21;
        private DevExpress.ExpressApp.Scheduler.SchedulerModuleBase schedulerModuleBase1;
        private DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule viewVariantsModule1;
        private Cap.Nomina.NominaModule nominaModule1;
        private DevExpress.ExpressApp.AuditTrail.AuditTrailModule auditTrailModule1;
        private DevExpress.ExpressApp.PivotChart.PivotChartModuleBase pivotChartModuleBase1;
        private DevExpress.ExpressApp.FileAttachments.Web.FileAttachmentsAspNetModule fileAttachmentsAspNetModule1;
        private DevExpress.ExpressApp.PivotGrid.Web.PivotGridAspNetModule pivotGridAspNetModule1;
        private DevExpress.ExpressApp.PivotGrid.PivotGridModule pivotGridModule1;
        private DevExpress.ExpressApp.PivotChart.Web.PivotChartAspNetModule pivotChartAspNetModule1;
        private Cap.Generales.GeneralesModule generalesModule1;
        private Cap.Inventarios.InventariosModule inventariosModule1;
        private Cap.Clientes.ClientesModule clientesModule1;
        private Cap.Fe.FeModule feModule1;
        private Cap.Bancos.BancosModule bancosModule1;
        private DevExpress.ExpressApp.Notifications.NotificationsModule notificationsModule1;
        private DevExpress.ExpressApp.TreeListEditors.TreeListEditorsModuleBase treeListEditorsModuleBase1;
        private cap.Ventas.VentasModule ventasModule1;
        private Cap.Compras.ComprasModule comprasModule1;
        private Cap.Proveedores.ProveedoresModule proveedoresModule1;
        private System.Data.SqlClient.SqlConnection sqlConnection1;

        public LCapAspNetApplication()
        {
            InitializeComponent();
            reportsModuleV21.ReportStoreMode = DevExpress.ExpressApp.ReportsV2.ReportStoreModes.XML;
            PostgreSqlConnectionProvider.GlobalUseLegacyGuidSupport = true;
        }

        // Override to execute custom code after a logon has been performed, the SecuritySystem object is initialized, logon parameters have been saved and user model differences are loaded.
        protected override void OnLoggedOn(LogonEventArgs args)
        { // http://documentation.devexpress.com/#Xaf/DevExpressExpressAppXafApplication_LoggedOntopic
            base.OnLoggedOn(args);

            ((IModelOptionsWeb)Model.Options).CollectionsEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
        }

        // Override to execute custom code after a user has logged off.
        protected override void OnLoggedOff()
        { // http://documentation.devexpress.com/#Xaf/DevExpressExpressAppXafApplication_LoggedOfftopic
            base.OnLoggedOff();
        }

        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args)
        {
            args.ObjectSpaceProvider = new XPObjectSpaceProvider(args.ConnectionString, args.Connection);
        }

        private void LCapAspNetApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e)
        {
            PostgreSqlConnectionProvider.GlobalUseLegacyGuidSupport = true;
#if EASYTEST
			e.Updater.Update();
			e.Handled = true;
#else
            /*
            e.Updater.Update();
            e.Handled = true;
            if (System.Diagnostics.Debugger.IsAttached)
            {*/
                e.Updater.Update();
                e.Handled = true;
            /*
            }
            else
            {
                string message = "The application cannot connect to the specified database, because the latter doesn't exist or its version is older than that of the application.\r\n" +
                    "This error occurred  because the automatic database update was disabled when the application was started without debugging.\r\n" +
                    "To avoid this error, you should either start the application under Visual Studio in debug mode, or modify the " +
                    "source code of the 'DatabaseVersionMismatch' event handler to enable automatic database update, " +
                    "or manually create a database using the 'DBUpdater' tool.\r\n" +
                    "Anyway, refer to the following help topics for more detailed information:\r\n" +
                    "'Update Application and Database Versions' at http://www.devexpress.com/Help/?document=ExpressApp/CustomDocument2795.htm\r\n" +
                    "'Database Security References' at http://www.devexpress.com/Help/?document=ExpressApp/CustomDocument3237.htm\r\n" +
                    "If this doesn't help, please contact our Support Team at http://www.devexpress.com/Support/Center/";

                if (e.CompatibilityError != null && e.CompatibilityError.Exception != null)
                {
                    message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
                }
                throw new InvalidOperationException(message);
            }*/
#endif
        }

        private void InitializeComponent()
        {
            this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.module2 = new DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule();
            this.module3 = new LCap.Module.LCapModule();
            this.module4 = new LCap.Module.Web.LCapAspNetModule();
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            this.validationModule1 = new DevExpress.ExpressApp.Validation.ValidationModule();
            this.conditionalAppearanceModule1 = new DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule();
            this.securityStrategyComplex1 = new DevExpress.ExpressApp.Security.SecurityStrategyComplex();
            this.authenticationStandard1 = new DevExpress.ExpressApp.Security.AuthenticationStandard();
            this.securityModule1 = new DevExpress.ExpressApp.Security.SecurityModule();
            this.businessClassLibraryCustomizationModule1 = new DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule();
            this.chartModule1 = new DevExpress.ExpressApp.Chart.ChartModule();
            this.cloneObjectModule1 = new DevExpress.ExpressApp.CloneObject.CloneObjectModule();
            this.reportsModuleV21 = new DevExpress.ExpressApp.ReportsV2.ReportsModuleV2();
            this.schedulerModuleBase1 = new DevExpress.ExpressApp.Scheduler.SchedulerModuleBase();
            this.viewVariantsModule1 = new DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule();
            this.nominaModule1 = new Cap.Nomina.NominaModule();
            this.auditTrailModule1 = new DevExpress.ExpressApp.AuditTrail.AuditTrailModule();
            this.pivotChartModuleBase1 = new DevExpress.ExpressApp.PivotChart.PivotChartModuleBase();
            this.fileAttachmentsAspNetModule1 = new DevExpress.ExpressApp.FileAttachments.Web.FileAttachmentsAspNetModule();
            this.pivotGridAspNetModule1 = new DevExpress.ExpressApp.PivotGrid.Web.PivotGridAspNetModule();
            this.pivotGridModule1 = new DevExpress.ExpressApp.PivotGrid.PivotGridModule();
            this.pivotChartAspNetModule1 = new DevExpress.ExpressApp.PivotChart.Web.PivotChartAspNetModule();
            this.generalesModule1 = new Cap.Generales.GeneralesModule();
            this.inventariosModule1 = new Cap.Inventarios.InventariosModule();
            this.clientesModule1 = new Cap.Clientes.ClientesModule();
            this.feModule1 = new Cap.Fe.FeModule();
            this.bancosModule1 = new Cap.Bancos.BancosModule();
            this.notificationsModule1 = new DevExpress.ExpressApp.Notifications.NotificationsModule();
            this.treeListEditorsModuleBase1 = new DevExpress.ExpressApp.TreeListEditors.TreeListEditorsModuleBase();
            this.ventasModule1 = new cap.Ventas.VentasModule();
            this.comprasModule1 = new Cap.Compras.ComprasModule();
            this.proveedoresModule1 = new Cap.Proveedores.ProveedoresModule();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlConnection1
            // 
            this.sqlConnection1.ConnectionString = "Integrated Security=SSPI;Pooling=false;Data Source=.\\SQLEXPRESS;Initial Catalog=L" +
    "Cap";
            this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
            // 
            // validationModule1
            // 
            this.validationModule1.AllowValidationDetailsAccess = true;
            this.validationModule1.IgnoreWarningAndInformationRules = false;
            // 
            // securityStrategyComplex1
            // 
            this.securityStrategyComplex1.AllowAnonymousAccess = false;
            this.securityStrategyComplex1.Authentication = this.authenticationStandard1;
            this.securityStrategyComplex1.PermissionsReloadMode = DevExpress.ExpressApp.Security.PermissionsReloadMode.NoCache;
            this.securityStrategyComplex1.RoleType = typeof(DevExpress.ExpressApp.Security.Strategy.SecuritySystemRole);
            this.securityStrategyComplex1.UseOptimizedPermissionRequestProcessor = false;
            this.securityStrategyComplex1.UserType = typeof(DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser);
            // 
            // authenticationStandard1
            // 
            this.authenticationStandard1.LogonParametersType =
                typeof(LCap.Module.ChangeDatabaseStandardAuthenticationLogonParametersWeb);
            //typeof(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters);
            // 
            // cloneObjectModule1
            // 
            this.cloneObjectModule1.ClonerType = null;
            // 
            // reportsModuleV21
            // 
            this.reportsModuleV21.EnableInplaceReports = true;
            this.reportsModuleV21.ReportDataType = typeof(DevExpress.Persistent.BaseImpl.ReportDataV2);
            // 
            // auditTrailModule1
            // 
            this.auditTrailModule1.AuditDataItemPersistentType = typeof(DevExpress.Persistent.BaseImpl.AuditDataItemPersistent);
            // 
            // pivotChartModuleBase1
            // 
            this.pivotChartModuleBase1.DataAccessMode = DevExpress.ExpressApp.CollectionSourceDataAccessMode.Client;
            this.pivotChartModuleBase1.ShowAdditionalNavigation = false;
            // 
            // notificationsModule1
            // 
            this.notificationsModule1.CanAccessPostponedItems = false;
            this.notificationsModule1.NotificationsRefreshInterval = System.TimeSpan.Parse("00:05:00");
            this.notificationsModule1.NotificationsStartDelay = System.TimeSpan.Parse("00:00:05");
            this.notificationsModule1.ShowDismissAllAction = false;
            this.notificationsModule1.ShowNotificationsWindow = true;
            this.notificationsModule1.ShowRefreshAction = false;
            // 
            // LCapAspNetApplication
            // 
            this.ApplicationName = "LCap";
            this.Connection = this.sqlConnection1;
            this.Modules.Add(this.module1);
            this.Modules.Add(this.module2);
            this.Modules.Add(this.validationModule1);
            this.Modules.Add(this.conditionalAppearanceModule1);
            this.Modules.Add(this.cloneObjectModule1);
            this.Modules.Add(this.securityModule1);
            this.Modules.Add(this.generalesModule1);
            this.Modules.Add(this.businessClassLibraryCustomizationModule1);
            this.Modules.Add(this.inventariosModule1);
            this.Modules.Add(this.clientesModule1);
            this.Modules.Add(this.reportsModuleV21);
            this.Modules.Add(this.feModule1);
            this.Modules.Add(this.bancosModule1);
            this.Modules.Add(this.auditTrailModule1);
            this.Modules.Add(this.notificationsModule1);
            this.Modules.Add(this.treeListEditorsModuleBase1);
            this.Modules.Add(this.viewVariantsModule1);
            this.Modules.Add(this.ventasModule1);
            this.Modules.Add(this.comprasModule1);
            this.Modules.Add(this.nominaModule1);
            this.Modules.Add(this.proveedoresModule1);
            this.Modules.Add(this.module3);
            this.Modules.Add(this.pivotChartModuleBase1);
            this.Modules.Add(this.pivotChartAspNetModule1);
            this.Modules.Add(this.module4);
            this.Modules.Add(this.chartModule1);
            this.Modules.Add(this.schedulerModuleBase1);
            this.Modules.Add(this.fileAttachmentsAspNetModule1);
            this.Modules.Add(this.pivotGridModule1);
            this.Modules.Add(this.pivotGridAspNetModule1);
            this.Security = this.securityStrategyComplex1;
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.LCapAspNetApplication_DatabaseVersionMismatch);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        }




        protected override void ReadSecuredLogonParameters()
        {
            base.ReadSecuredLogonParameters(); // the "UserName" is restored in the base method.

            string databaseName = HttpContext.Current.Request.Params[WebChangeDatabaseController.DatabaseParameterName];
            if (!string.IsNullOrEmpty(databaseName))
            {
                ((IDatabaseNameParameter)SecuritySystem.LogonParameters).DatabaseName = databaseName;
            }
        }
        private bool canReadSecuredLogonParameters = true;
        protected override bool CanReadSecuredLogonParameters()
        {
            if (!canReadSecuredLogonParameters)
                return false;
            return base.CanReadSecuredLogonParameters();
        }
        protected override bool OnLogonFailed(object logonParameters, Exception e)
        {
            if (CanReadSecuredLogonParameters())
            {
                FormsAuthentication.SignOut();
                canReadSecuredLogonParameters = false;
                try
                {
                    Start();
                }
                finally
                {
                    canReadSecuredLogonParameters = true;
                }
                return true;
            }
            else
            {
                return base.OnLogonFailed(logonParameters, e);
            }
        }
        protected override void ReadLastLogonParametersCore(DevExpress.ExpressApp.Utils.SettingsStorage storage, object logonObject)
        {
            // TI
            AuthenticationStandardLogonParameters standardLogonParameters = logonObject as AuthenticationStandardLogonParameters;

            if ((standardLogonParameters != null) && string.IsNullOrEmpty(standardLogonParameters.UserName))
            {
                base.ReadLastLogonParametersCore(storage, logonObject);
            }

            /*TI
            //string databaseName = HttpContext.Current.Request.Params[WebChangeDatabaseController.DatabaseParameterName];
            if (string.IsNullOrEmpty(((IDatabaseNameParameter)logonObject).DatabaseName))
            {
                base.ReadLastLogonParametersCore(storage, logonObject);
            }*/
        }
        protected override void OnLoggingOn(LogonEventArgs args)
        {
            //AuthenticationStandardLogonParameters standardLogonParameters = args.LogonParameters as AuthenticationStandardLogonParameters;
            ChangeDatabaseStandardAuthenticationLogonParametersWeb
                standardLogonParameters = args.LogonParameters as ChangeDatabaseStandardAuthenticationLogonParametersWeb;

            base.OnLoggingOn(args);
            ChangeDatabaseHelper.UpdateDatabaseName(this,
                /*((IDatabaseNameParameter)args.LogonParameters)*/
                standardLogonParameters.Clv);
        }
    }
}
