using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using System.Collections.Generic;
using DevExpress.ExpressApp.Security.Strategy;
using LCap.Module.BusinessObjects.Inventarios;
using LCap.Module.BusinessObjects.Clientes;
using LCap.Module.BusinessObjects.Administracion;
using DevExpress.ExpressApp.Reports;
using LCap.Module.BusinessObjects;
using Cap.Generales.BusinessObjects.Empresa;
using Cap.Generales.BusinessObjects.Unidades;
using Cap.Inventarios.BusinessObjects;
using Cap.Generales.BusinessObjects.General;
using Cap.Clientes.BusinessObjects.Clientes;
using Cap.Fe.BusinessObjects;
using Cap.Ventas.BusinessObjects;
using Cap.Clientes.BusinessObjects.Generales;
using Cap.Nomina.BusinessObjects;

namespace LCap.Module.DatabaseUpdate
{
    // Allows you to handle a database update when the application version changes (http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppUpdatingModuleUpdatertopic help article for more details).
    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion)
        {
        }
        // Override to specify the database update code which should be performed after the database schema is updated (http://documentation.devexpress.com/#Xaf/DevExpressExpressAppUpdatingModuleUpdater_UpdateDatabaseAfterUpdateSchematopic).
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();
            //string name = "MyName";
            //DomainObject1 theObject = ObjectSpace.FindObject<DomainObject1>(CriteriaOperator.Parse("Name=?", name));
            //if(theObject == null) {
            //    theObject = ObjectSpace.CreateObject<DomainObject1>();
            //    theObject.Name = name;
            //}

            Usuarios();
            Datos();

            CreateReport("AcuseCan");
            CreateReport("FacturaCFDI");
            CreateReport("Ventas");
            CreateReport("FacturasCanceladas");
            CreateReport("VentasDetalle");
            CreateReport("Pedido");
            CreateReport("Remision");
            CreateReport("OrdenCmp");
            CreateReport("RecepcionCmp");
            CreateReport("Compras");
            // CreateDataToBeAnalysed();
        }

        // Override to perform the required changes with the database structure before the database schema is updated (http://documentation.devexpress.com/#Xaf/DevExpressExpressAppUpdatingModuleUpdater_UpdateDatabaseBeforeUpdateSchematopic).
        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }




        private void PermisosTipos(List<Type> tipos, SecuritySystemRole rol, bool create, bool del, bool navigate, bool read, bool write)
        {
            foreach (Type tp in tipos)
            {
                SecuritySystemTypePermissionObject userTypePermission =
    ObjectSpace.CreateObject<SecuritySystemTypePermissionObject>();

                userTypePermission.TargetType = tp;

                userTypePermission.AllowCreate = create;
                userTypePermission.AllowDelete = del;
                userTypePermission.AllowNavigate = navigate;
                userTypePermission.AllowRead = read;
                userTypePermission.AllowWrite = write;

                rol.TypePermissions.Add(userTypePermission);
            }
        }


        private void Usuarios()
        {
            SecuritySystemRole rolServs = ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", "Servicios"));

            if (rolServs == null)
            {
                rolServs = ObjectSpace.CreateObject<SecuritySystemRole>();
                rolServs.Name = "Servicios";

            }

            if (rolServs.FindTypePermissionObject<Cliente>() == null)
            {
                List<Type> tipos = new List<Type>();

                tipos.Clear();
                tipos.Add(typeof(Cliente));
                PermisosTipos(tipos, rolServs, true, true, true, true, true);
            }

            /*
            SecuritySystemRole rolAlma = ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", "Almacen"));

            if (rolAlma == null)
            {
                List<Type> tipos = new List<Type>();


                rolAlma = ObjectSpace.CreateObject<SecuritySystemRole>();
                rolAlma.Name = "Almacen";

                / *
                SecuritySystemTypePermissionObject userTypePermission =
ObjectSpace.CreateObject<SecuritySystemTypePermissionObject>();
                userTypePermission.TargetType = typeof(SecuritySystemUser);

                SecuritySystemObjectPermissionsObject currentUserObjectPermission =
ObjectSpace.CreateObject<SecuritySystemObjectPermissionsObject>();
                currentUserObjectPermission.Criteria = "[Oid] = CurrentUserId()";
                currentUserObjectPermission.AllowNavigate = true;
                currentUserObjectPermission.AllowRead = true;
                userTypePermission.ObjectPermissions.Add(currentUserObjectPermission);* /

                tipos.Clear();
                tipos.Add(typeof(MovimientoI));
                tipos.Add(typeof(Producto));
                tipos.Add(typeof(Marca));
                tipos.Add(typeof(Linea));
                tipos.Add(typeof(Unidad));
                tipos.Add(typeof(EsquemaImpuesto));
                tipos.Add(typeof(Moneda));
                tipos.Add(typeof(ConceptoMI));
                PermisosTipos(tipos, rolAlma, true, true, true, true, true);

                tipos.Clear();
                tipos.Add(typeof(Cliente));
                tipos.Add(typeof(Proveedor));
                tipos.Add(typeof(Direccion));
                PermisosTipos(tipos, rolAlma, false, false, true, true, true);

                tipos.Clear();
                tipos.Add(typeof(ExportaInvFis));
                PermisosTipos(tipos, rolAlma, true, false, true, true, true);

                tipos.Clear();
                tipos.Add(typeof(MyFileData));
                tipos.Add(typeof(FileData));
                PermisosTipos(tipos, rolAlma, true, true, false, true, true);
            }*/


            /*
            SecuritySystemRole rolContab = ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", "Contabilidad"));

            if (rolContab == null)
            {
                List<Type> tipos = new List<Type>();


                rolContab = ObjectSpace.CreateObject<SecuritySystemRole>();
                rolContab.Name = "Contabilidad";

                / *
                SecuritySystemTypePermissionObject userTypePermission =
ObjectSpace.CreateObject<SecuritySystemTypePermissionObject>();
                userTypePermission.TargetType = typeof(SecuritySystemUser);

                SecuritySystemObjectPermissionsObject currentUserObjectPermission =
ObjectSpace.CreateObject<SecuritySystemObjectPermissionsObject>();
                currentUserObjectPermission.Criteria = "[Oid] = CurrentUserId()";
                currentUserObjectPermission.AllowNavigate = true;
                currentUserObjectPermission.AllowRead = true;
                userTypePermission.ObjectPermissions.Add(currentUserObjectPermission);* /

                tipos.Clear();
                tipos.Add(typeof(Producto));
                tipos.Add(typeof(Cliente));
                tipos.Add(typeof(Documento));
                tipos.Add(typeof(DocumentoSalida));

                PermisosTipos(tipos, rolContab, false, false, true, true, false);
            }*/


            /*
            SecuritySystemRole rolAdmin = ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", "Admin"));

            if (rolAdmin == null)
            {
                List<Type> tipos = new List<Type>();


                rolAdmin = ObjectSpace.CreateObject<SecuritySystemRole>();
                rolAdmin.Name = "Admin";

                / *
                SecuritySystemTypePermissionObject userTypePermission =
ObjectSpace.CreateObject<SecuritySystemTypePermissionObject>();
                userTypePermission.TargetType = typeof(SecuritySystemUser);

                SecuritySystemObjectPermissionsObject currentUserObjectPermission =
ObjectSpace.CreateObject<SecuritySystemObjectPermissionsObject>();
                currentUserObjectPermission.Criteria = "[Oid] = CurrentUserId()";
                currentUserObjectPermission.AllowNavigate = true;
                currentUserObjectPermission.AllowRead = true;
                userTypePermission.ObjectPermissions.Add(currentUserObjectPermission);* /

                tipos.Clear();
                tipos.Add(typeof(Direccion));
                tipos.Add(typeof(Compania));
                tipos.Add(typeof(ItemFormaP));
                tipos.Add(typeof(Documento));
                PermisosTipos(tipos, rolAdmin, true, true, false, true, true);

                tipos.Clear();
                tipos.Add(typeof(Clasificacion));
                tipos.Add(typeof(Cliente));
                tipos.Add(typeof(ConceptoCxc));
                tipos.Add(typeof(Cxc));
                tipos.Add(typeof(Vendedor));
                tipos.Add(typeof(Zona));

                tipos.Add(typeof(LCap.Module.BusinessObjects.Empresa.Empresa));
                tipos.Add(typeof(Moneda));
                tipos.Add(typeof(POFolio));
                tipos.Add(typeof(POCertificado));
                tipos.Add(typeof(POCorreo));
                tipos.Add(typeof(POCompra));
                tipos.Add(typeof(Administracion));

                tipos.Add(typeof(ConceptoMI));
                tipos.Add(typeof(Producto));
                tipos.Add(typeof(EsquemaImpuesto));
                tipos.Add(typeof(MovimientoI));
                tipos.Add(typeof(Linea));
                tipos.Add(typeof(Unidad));
                tipos.Add(typeof(Marca));
                tipos.Add(typeof(ExportaInvFis));

                tipos.Add(typeof(Proveedor));
                tipos.Add(typeof(Cxp));
                tipos.Add(typeof(ConceptoCxp));

                tipos.Add(typeof(DocumentoSalida));

                tipos.Add(typeof(Analysis));

                PermisosTipos(tipos, rolAdmin, true, true, true, true, true);
            }*/

            SecuritySystemUser userR = ObjectSpace.FindObject<SecuritySystemUser>(
new BinaryOperator("UserName", "root"));

            if (userR == null)
            {
                userR = ObjectSpace.CreateObject<SecuritySystemUser>();
                userR.UserName = "root";
                // Set a password if the standard authentication type is used 
                userR.SetPassword("");
            }

            /*
            SecuritySystemUser userS = ObjectSpace.FindObject<SecuritySystemUser>(
                    new BinaryOperator("UserName", "servicio"));

            if (userS == null)
            {
                userS = ObjectSpace.CreateObject<SecuritySystemUser>();
                userS.UserName = "servicio";
                // userS.SetPassword("servicio");
            }*/

            /*
            SecuritySystemUser userA = ObjectSpace.FindObject<SecuritySystemUser>(
                    new BinaryOperator("UserName", "almacen"));

            if (userA == null)
            {
                userA = ObjectSpace.CreateObject<SecuritySystemUser>();
                userA.UserName = "almacen";
                // Set a password if the standard authentication type is used 
                userA.SetPassword("");
            }*/

            /*
            SecuritySystemUser userCont = ObjectSpace.FindObject<SecuritySystemUser>(
                    new BinaryOperator("UserName", "contab"));

            if (userCont == null)
            {
                userCont = ObjectSpace.CreateObject<SecuritySystemUser>();
                userCont.UserName = "contab";
                // Set a password if the standard authentication type is used 
                userCont.SetPassword("");
            }*/

            /*
            SecuritySystemUser userAdmin = ObjectSpace.FindObject<SecuritySystemUser>(
                    new BinaryOperator("UserName", "admin"));

            if (userAdmin == null)
            {
                userAdmin = ObjectSpace.CreateObject<SecuritySystemUser>();
                userAdmin.UserName = "admin";
                // Set a password if the standard authentication type is used 
                userAdmin.SetPassword("");
            }*/

            // ... 
            // Add the "Administrators" Role to the user1 
            userR.Roles.Add(RolAdmin());
            // userS.Roles.Add(rolServs);

            /*
            userA.Roles.Add(rolAlma);
            userCont.Roles.Add(rolContab);
            userAdmin.Roles.Add(rolAdmin);*/
        }

        private SecuritySystemRole RolAdmin()
        {
            SecuritySystemRole adminRole = ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", SecurityStrategy.AdministratorRoleName));

            if (adminRole == null)
            {
                adminRole = ObjectSpace.CreateObject<SecuritySystemRole>();
                adminRole.Name = SecurityStrategy.AdministratorRoleName;
                adminRole.IsAdministrative = true;
            }
            return adminRole;
        }

        private void Datos()
        {
            /* TIT Mrz 2021 Ya son otras unidades
            List<string> unis = new List<string>();

            unis.Add("PZA|Pieza");
            unis.Add("ATDO|Atado");
            unis.Add("BTO|Bulto");
            unis.Add("CAJ|Caja");
            unis.Add("CB|Cubeta");
            unis.Add("PR|Par");
            unis.Add("GL|Galon");
            unis.Add("HJ|Hoja");
            unis.Add("JG|Juego");
            unis.Add("KG|Kilogramo");
            unis.Add("LT|Litro");
            unis.Add("MT|Metro");
            unis.Add("NA|No Aplica");
            unis.Add("PQ|Paquete");
            unis.Add("RL|Rollo");

            foreach (string ob in unis)
            {
                Unidad uni = null;
                string[] arrtoks = ob.Split('|');

                uni = ObjectSpace.FindObject<Unidad>(CriteriaOperator.Parse(string.Format("Clave == '{0}'", arrtoks[0])));
                if (uni == null)
                {
                    uni = ObjectSpace.CreateObject<Unidad>();
                    uni.Clave = arrtoks[0];
                    uni.Descripcion = arrtoks[1];
                    uni.Save();
                }
            }*/


            List<string> CptsCxc = new List<string>();
            CptsCxc.Add("A|Abonos|N");
            CptsCxc.Add("A.A|Anticipo|A");
            CptsCxc.Add("A.E|Efectivo|A");
            CptsCxc.Add("A.C|Cheque|A");
            CptsCxc.Add("A.N|Nota de crédito|A");
            CptsCxc.Add("C|Cargos|N");
            CptsCxc.Add("C.F|Factura|C");
            CptsCxc.Add("C.N|Notas de cargo|C");
            CptsCxc.Add("C.C|Cheque devuelto|C");
            foreach (string ob in CptsCxc)
            {
                ConceptoCxc obj;
                string[] arrtoks = ob.Split('|');

                obj = ObjectSpace.FindObject<ConceptoCxc>(CriteriaOperator.Parse(string.Format("Clave == '{0}'", arrtoks[0])));
                if (obj == null)
                {
                    obj = ObjectSpace.CreateObject<ConceptoCxc>();
                    obj.Clave = arrtoks[0];
                    obj.Descripcion = arrtoks[1];
                    obj.Tipo = arrtoks[2] == "A" ? EConceptoTipo.Abono : EConceptoTipo.Cargo;
                    obj.Save();
                }
            }

            List<string> mones = new List<string>();

            // clave/ des/ |nombre|termina|idioma|monto
            mones.Add("EUR|Euro|||1|0");
            mones.Add("USD|Dólar estadounidense/US Dollar|Dollar/Dollars|USD|1|0");
            mones.Add("MXN|Peso Mexicano|PESO/PESOS|M.N.|0|1");
            //mones.Add("GTQ|Quetzal|QUETZAL/QUETZALES|Q|2|1");
            //mones.Add("CLP|Peso Chileno|PESO/PESOS|CLP|2|1");
            //mones.Add("COP|Peso Colombiano|PESO/PESOS|COP|2|1");
            //mones.Add("ARS|Peso Argentino|PESO/PESOS|ARS|2|1");
            //mones.Add("PEN|Nuevo Sol|NUEVO SOL/NUEVOS SOLES|S/.|2|1");

            foreach (string ob in mones)
            {
                Moneda mone;
                string[] arrtoks = ob.Split('|');

                mone = ObjectSpace.FindObject<Moneda>(CriteriaOperator.Parse(string.Format("Clave == '{0}'", arrtoks[0])));
                if (mone == null)
                {
                    mone = ObjectSpace.CreateObject<Moneda>();
                    mone.Clave = arrtoks[0];
                    mone.Descripcion = arrtoks[1];
                    // mone.Nombre = arrtoks[2];
                    mone.Nmbr = arrtoks[2];
                    mone.Terminacion = arrtoks[3];
                    mone.Idioma = arrtoks[4] == "0" ? EIdioma.Mexicano :
                        arrtoks[4] == "1" ? EIdioma.Ingles : EIdioma.Espanol;
                    mone.MontoTC = Convert.ToDecimal(arrtoks[5]);
                    mone.Sistema = mone.Clave == "MXN";
                    mone.Save();
                }
            }

            EsquemaImpuesto esq = ObjectSpace.FindObject<EsquemaImpuesto>(CriteriaOperator.Parse("Clave == '1'"));

            if (esq == null)
            {
                esq = ObjectSpace.CreateObject<EsquemaImpuesto>();
                esq.Clave = "1";
                esq.Descripcion = "IVA = 16%";
                esq.Impuesto1 = 0;
                esq.AplImpuesto1 = EAplicaImpuesto.Precio;
                esq.Impuesto2 = 0;
                esq.AplImpuesto2 = EAplicaImpuesto.Precio;
                esq.Impuesto3 = 0;
                esq.AplImpuesto3 = EAplicaImpuesto.Precio;
                esq.Impuesto4 = 16;
                esq.AplImpuesto4 = EAplicaImpuesto.Precio;
                esq.Save();
            }

            esq = ObjectSpace.FindObject<EsquemaImpuesto>(CriteriaOperator.Parse("Clave == '2'"));
            if (esq == null)
            {
                esq = ObjectSpace.CreateObject<EsquemaImpuesto>();
                esq.Clave = "2";
                esq.Descripcion = "IVA = 0%";
                esq.Impuesto1 = 0;
                esq.AplImpuesto1 = EAplicaImpuesto.Precio;
                esq.Impuesto2 = 0;
                esq.AplImpuesto2 = EAplicaImpuesto.Precio;
                esq.Impuesto3 = 0;
                esq.AplImpuesto3 = EAplicaImpuesto.Precio;
                esq.Impuesto4 = 0;
                esq.AplImpuesto4 = EAplicaImpuesto.Precio;
                esq.Save();
            }

            /*
            List<string> clis = new List<string>();*/

            /* TI Jul 2015 Podemos cargarlo ahora atra vez de un xml o hoja de etcel
            clis.Add("MOSTRADORE|Cliente de mostrador extranjero|XEXX010101000");
            clis.Add("MOSTRADORN|Cliente de mostrador nacional|XAXX010101000");*/
            /*
            foreach (string ob in clis)
            {
                string[] arrtoks = ob.Split('|');
                Cliente cli = ObjectSpace.FindObject<Cliente>(CriteriaOperator.Parse(string.Format("Clave == '{0}'", arrtoks[0])));

                if (cli == null)
                {
                    cli = ObjectSpace.CreateObject<Cliente>();

                    cli.Clave = arrtoks[0];
                    cli.Compania.Nombre = arrtoks[1];
                    cli.Compania.Rfc = arrtoks[2];
                    cli.Save();
                }
            }*/



            Empresa emp =
                ObjectSpace.FindObject<Empresa>(null); // (CriteriaOperator.Parse("Clave = 'icpac'"));

            if (emp == null)
            {
                emp = ObjectSpace.CreateObject<Empresa>();
                emp.Compania.Nombre = "EMPRESA INVALIDA";
                emp.Compania.Rfc = "AAA010101AAA";
                emp.Compania.Curp = "CURPEMPRESA";
                /*
                emp.Regimen = "Regimen de la empresa";*/
                emp.Compania.Direccion.Calle = "Calle empresa";
                emp.Compania.Direccion.Numero = "Numero";
                emp.Compania.Direccion.Colonia = "Colonia";
                emp.Compania.Direccion.Municipio = "Delegacion";
                emp.Compania.Direccion.CP = "codig";
                emp.Compania.Direccion.Estado = "Uaxaca";
                /*Oct 2018 está obsoleto
                emp.Clave = "icpac";*/
                emp.Usuario = "pruebasWS";
                emp.Passw = "pruebasWS";
                emp.Save();
            }

            /*PO*/Correo corr = ObjectSpace.FindObject</*PO*/Correo>(CriteriaOperator.Parse("Clave = 'icpac'"));
            if (corr == null)
            {
                corr = ObjectSpace.CreateObject</*PO*/Correo>();
                corr.Clave = "icpac";
                corr.ServidorSMTP = "smtp.gmail.com";
                corr.Puerto = "587";
                corr.SegurdSSL = true;
                corr.Cuenta = "@gmail.com";
                corr.Identificdr = "Comprobante";
                corr.Asunto = "Envío de Comprobante: ";
                corr.Mensaje1 = "Estimado Cliente le envíamos su Comprobante:";
                corr.Pie3 = "Saludos";
                corr.Save();
            }

            MovimientosI movs = ObjectSpace.FindObject<MovimientosI>(null);
            if (movs == null)
            {
                movs = ObjectSpace.CreateObject<MovimientosI>();
                movs.Save();
            }
            /*
            Ventas vta = ObjectSpace.FindObject<Ventas>(CriteriaOperator.Parse("Clave = 'icpac'"));
            if (vta == null)
            {
                vta = ObjectSpace.CreateObject<Ventas>();
                vta.Save();
            }*/

            /*PO*/Certificado cer = ObjectSpace.FindObject</*PO*/Certificado>(null); 
            if (cer == null)
            {
                cer = ObjectSpace.CreateObject</*PO*/Certificado>();
                /*
                cer.Clave = "icpac";*/
                cer.PasswCertif = "12345678a";
                cer.Save();
            }


            Producto srvc = ObjectSpace.FindObject<Producto>(CriteriaOperator.Parse("Clave = 'SRVC'"));
            if (srvc == null)
            {
                srvc = ObjectSpace.CreateObject<Producto>();
                srvc.Clave = "SRVC";
                srvc.Descripcion = "SERVICIO CON IMPUESTO";
                srvc.Tipo = EProductoTipo.Servicio;
                srvc.CapEnFact = true;
                srvc.USalida =
                srvc.UProducto = ObjectSpace.FindObject<Unidad>(CriteriaOperator.Parse("Clave = 'NA'"));
                srvc.Esquema = ObjectSpace.FindObject<EsquemaImpuesto>(CriteriaOperator.Parse("Clave = '1'"));

                srvc.Save();
            }

            srvc = ObjectSpace.FindObject<Producto>(CriteriaOperator.Parse("Clave = 'SRVC2'"));
            if (srvc == null)
            {
                srvc = ObjectSpace.CreateObject<Producto>();
                srvc.Clave = "SRVC2";
                srvc.Descripcion = "SERVICIO SIN IMPUESTO";
                srvc.Tipo = EProductoTipo.Servicio;
                srvc.CapEnFact = true;
                srvc.USalida =
                srvc.UProducto = ObjectSpace.FindObject<Unidad>(CriteriaOperator.Parse("Clave = 'NA'"));
                srvc.Esquema = ObjectSpace.FindObject<EsquemaImpuesto>(CriteriaOperator.Parse("Clave = '2'"));

                srvc.Save();
            }

            Administracion adm;

            adm = ObjectSpace.FindObject<Administracion>(new BinaryOperator("Clave", "ICPAC"));

            if (adm != null)
                adm.Delete();
            ObjectSpace.CommitChanges();

            adm = ObjectSpace.FindObject<Administracion>(null);
            if (adm != null && adm.VntCfdi == null)
                adm.VntCfdi = ObjectSpace.CreateObject<VentaCfdi>();
            if (adm != null && adm.PrmtrsNmn == null)
                adm.PrmtrsNmn = ObjectSpace.CreateObject<Parametros>();


            adm = ObjectSpace.FindObject<Administracion>(new BinaryOperator("Clave", Administracion.ClaveFto("ROOT")));
            if (adm == null)
            {
                adm = ObjectSpace.CreateObject<Administracion>();
                adm.Clave = "ROOT";
                adm.LeyImpst4 = "IVA";
                adm.Save();
            }
            /*
            else
                adm.Certificado = null;*/

            /*TI Oct 2017 No recuerdo bien para qué era
            System.Collections.IList facElec = ObjectSpace.GetObjects(typeof(POFacElec));
            foreach (POFacElec po in facElec)
            {
                po.Certificado = null;
            }*/

            try
            {
                /* Feb 2016, TI: Para LG ?
                POCertificado certdel = ObjectSpace.FindObject<POCertificado>(new BinaryOperator("Oid", 1));
                if (certdel != null)
                    certdel.Delete();

                certdel = ObjectSpace.FindObject<POCertificado>(new BinaryOperator("Oid", 20));
                if (certdel != null)
                    certdel.Delete();

                POCorreo cordel = ObjectSpace.FindObject<POCorreo>(new BinaryOperator("Oid", 1));
                if (cordel != null)
                    cordel.Delete();
                cordel = ObjectSpace.FindObject<POCorreo>(new BinaryOperator("Oid", 20));
                if (cordel != null)
                    cordel.Delete();
                cordel = ObjectSpace.FindObject<POCorreo>(new BinaryOperator("Oid", 21));
                if (cordel != null)
                    cordel.Delete();
                cordel = ObjectSpace.FindObject<POCorreo>(new BinaryOperator("Oid", 23));
                if (cordel != null)
                    cordel.Delete();*/
            }
            catch (Exception)
            {
                ;
            }

            IFisico ifis = ObjectSpace.FindObject<IFisico>(null); 
            if (ifis == null)
            {
                ifis = ObjectSpace.CreateObject<IFisico>();
                ifis.Save();
            }



            /* Oct 2018 Se carga de la hoja de etcel.
            List<string> banlis = new List<string>();

            banlis.Add("002|BANAMEX|Banco Nacional de México, S.A., Institución de Banca Múltiple, Grupo Financiero Banamex");
            banlis.Add("006|BANCOMEXT|Banco Nacional de Comercio Exterior, Sociedad Nacional de Crédito, Institución de Banca de Desarrollo");
            banlis.Add("009|BANOBRAS|Banco Nacional de Obras y Servicios Públicos, Sociedad Nacional de Crédito, Institución de Banca de Desarrollo");
            banlis.Add("012|BBVA BANCOMER|BBVA Bancomer, S.A., Institución de Banca Múltiple, Grupo Financiero BBVA Bancomer");
            banlis.Add("014|SANTANDER|Banco Santander (México), S.A., Institución de Banca Múltiple, Grupo Financiero Santander");
            banlis.Add("019|BANJERCITO|Banco Nacional del Ejército, Fuerza Aérea y Armada, Sociedad Nacional de Crédito, Institución de Banca de Desarrollo");
            banlis.Add("021|HSBC|HSBC México, S.A., institución De Banca Múltiple, Grupo Financiero HSBC");
            banlis.Add("030|BAJIO|Banco del Bajío, S.A., Institución de Banca Múltiple");
            banlis.Add("032|IXE|IXE Banco, S.A., Institución de Banca Múltiple, IXE Grupo Financiero");
            banlis.Add("036|INBURSA|Banco Inbursa, S.A., Institución de Banca Múltiple, Grupo Financiero Inbursa");
            banlis.Add("037|INTERACCIONES|Banco Interacciones, S.A., Institución de Banca Múltiple");
            banlis.Add("042|MIFEL|Banca Mifel, S.A., Institución de Banca Múltiple, Grupo Financiero Mifel");
            banlis.Add("044|SCOTIABANK|Scotiabank Inverlat, S.A.");
            banlis.Add("058|BANREGIO|Banco Regional de Monterrey, S.A., Institución de Banca Múltiple, Banregio Grupo Financiero");
            banlis.Add("059|INVEX|Banco Invex, S.A., Institución de Banca Múltiple, Invex Grupo Financiero");
            banlis.Add("060|BANSI|Bansi, S.A., Institución de Banca Múltiple");
            banlis.Add("062|AFIRME|Banca Afirme, S.A., Institución de Banca Múltiple");
            banlis.Add("072|BANORTE|Banco Mercantil del Norte, S.A., Institución de Banca Múltiple, Grupo Financiero Banorte");
            banlis.Add("102|THE ROYAL BANK|The Royal Bank of Scotland México, S.A., Institución de Banca Múltiple");
            banlis.Add("103|AMERICAN EXPRESS|American Express Bank (México), S.A., Institución de Banca Múltiple");
            banlis.Add("106|BAMSA|Bank of America México, S.A., Institución de Banca Múltiple, Grupo Financiero Bank of America");
            banlis.Add("108|TOKYO|Bank of Tokyo-Mitsubishi UFJ (México), S.A.");
            banlis.Add("110|JP MORGAN|Banco J.P. Morgan, S.A., Institución de Banca Múltiple, J.P. Morgan Grupo Financiero");
            banlis.Add("112|BMONEX|Banco Monex, S.A., Institución de Banca Múltiple");
            banlis.Add("113|VE POR MAS|Banco Ve Por Mas, S.A. Institución de Banca Múltiple");
            banlis.Add("116|ING|ING Bank (México), S.A., Institución de Banca Múltiple, ING Grupo Financiero");
            banlis.Add("124|DEUTSCHE|Deutsche Bank México, S.A., Institución de Banca Múltiple");
            banlis.Add("126|CREDIT SUISSE|Banco Credit Suisse (México), S.A. Institución de Banca Múltiple, Grupo Financiero Credit Suisse (México)");
            banlis.Add("127|AZTECA|Banco Azteca, S.A. Institución de Banca Múltiple.");
            banlis.Add("128|AUTOFIN|Banco Autofin México, S.A. Institución de Banca Múltiple");
            banlis.Add("129|BARCLAYS|Barclays Bank México, S.A., Institución de Banca Múltiple, Grupo Financiero Barclays México");
            banlis.Add("130|COMPARTAMOS|Banco Compartamos, S.A., Institución de Banca Múltiple");
            banlis.Add("131|BANCO FAMSA|Banco Ahorro Famsa, S.A., Institución de Banca Múltiple");
            banlis.Add("132|BMULTIVA|Banco Multiva, S.A., Institución de Banca Múltiple, Multivalores Grupo Financiero");
            banlis.Add("133|ACTINVER|Banco Actinver, S.A. Institución de Banca Múltiple, Grupo Financiero Actinver");
            banlis.Add("134|WAL-MART|Banco Wal-Mart de México Adelante, S.A., Institución de Banca Múltiple");
            banlis.Add("135|NAFIN|Nacional Financiera, Sociedad Nacional de Crédito, Institución de Banca de Desarrollo");
            banlis.Add("136|INTERBANCO|Inter Banco, S.A. Institución de Banca Múltiple");
            banlis.Add("137|BANCOPPEL|BanCoppel, S.A., Institución de Banca Múltiple");
            banlis.Add("138|ABC CAPITAL|ABC Capital, S.A., Institución de Banca Múltiple");
            banlis.Add("139|UBS BANK|UBS Bank México, S.A., Institución de Banca Múltiple, UBS Grupo Financiero");
            banlis.Add("140|CONSUBANCO|Consubanco, S.A. Institución de Banca Múltiple");
            banlis.Add("141|VOLKSWAGEN|Volkswagen Bank, S.A., Institución de Banca Múltiple");
            banlis.Add("143|CIBANCO|CIBanco, S.A.");
            banlis.Add("145|BBASE|Banco Base, S.A., Institución de Banca Múltiple");
            banlis.Add("166|BANSEFI|Banco del Ahorro Nacional y Servicios Financieros, Sociedad Nacional de Crédito, Institución de Banca de Desarrollo");
            banlis.Add("168|HIPOTECARIA FEDERAL|Sociedad Hipotecaria Federal, Sociedad Nacional de Crédito, Institución de Banca de Desarrollo");
            banlis.Add("600|MONEXCB|Monex Casa de Bolsa, S.A. de C.V. Monex Grupo Financiero");
            banlis.Add("601|GBM|GBM Grupo Bursátil Mexicano, S.A. de C.V. Casa de Bolsa");
            banlis.Add("602|MASARI|Masari Casa de Bolsa, S.A.");
            banlis.Add("605|VALUE|Value, S.A. de C.V. Casa de Bolsa");
            banlis.Add("606|ESTRUCTURADORES|Estructuradores del Mercado de Valores Casa de Bolsa, S.A. de C.V.");
            banlis.Add("607|TIBER|Casa de Cambio Tiber, S.A. de C.V.");
            banlis.Add("608|VECTOR|Vector Casa de Bolsa, S.A. de C.V.");
            banlis.Add("610|B&B|B y B, Casa de Cambio, S.A. de C.V.");
            banlis.Add("614|ACCIVAL|Acciones y Valores Banamex, S.A. de C.V., Casa de Bolsa");
            banlis.Add("615|MERRILL LYNCH|Merrill Lynch México, S.A. de C.V. Casa de Bolsa");
            banlis.Add("616|FINAMEX|Casa de Bolsa Finamex, S.A. de C.V.");
            banlis.Add("617|VALMEX|Valores Mexicanos Casa de Bolsa, S.A. de C.V.");
            banlis.Add("618|UNICA|Unica Casa de Cambio, S.A. de C.V.");
            banlis.Add("619|MAPFRE|MAPFRE Tepeyac, S.A.");
            banlis.Add("620|PROFUTURO|Profuturo G.N.P., S.A. de C.V., Afore");
            banlis.Add("621|CB ACTINVER|Actinver Casa de Bolsa, S.A. de C.V.");
            banlis.Add("622|OACTIN|OPERADORA ACTINVER, S.A. DE C.V.");
            banlis.Add("623|SKANDIA|Skandia Vida, S.A. de C.V.");
            banlis.Add("626|CBDEUTSCHE|Deutsche Securities, S.A. de C.V. CASA DE BOLSA");
            banlis.Add("627|ZURICH|Zurich Compañía de Seguros, S.A.");
            banlis.Add("628|ZURICHVI|Zurich Vida, Compañía de Seguros, S.A.");
            banlis.Add("629|SU CASITA|Hipotecaria Su Casita, S.A. de C.V. SOFOM ENR");
            banlis.Add("630|CB INTERCAM|Intercam Casa de Bolsa, S.A. de C.V.");
            banlis.Add("631|CI BOLSA|CI Casa de Bolsa, S.A. de C.V.");
            banlis.Add("632|BULLTICK CB|Bulltick Casa de Bolsa, S.A., de C.V.");
            banlis.Add("633|STERLING|Sterling Casa de Cambio, S.A. de C.V.");
            banlis.Add("634|FINCOMUN|Fincomún, Servicios Financieros Comunitarios, S.A. de C.V.");
            banlis.Add("636|HDI SEGUROS|HDI Seguros, S.A. de C.V.");
            banlis.Add("637|ORDER|Order Express Casa de Cambio, S.A. de C.V");
            banlis.Add("638|AKALA|Akala, S.A. de C.V., Sociedad Financiera Popular");
            banlis.Add("640|CB JPMORGAN|J.P. Morgan Casa de Bolsa, S.A. de C.V. J.P. Morgan Grupo Financiero");
            banlis.Add("642|REFORMA|Operadora de Recursos Reforma, S.A. de C.V., S.F.P.");
            banlis.Add("646|STP|Sistema de Transferencias y Pagos STP, S.A. de C.V.SOFOM ENR");
            banlis.Add("647|TELECOMM|Telecomunicaciones de México");
            banlis.Add("648|EVERCORE|Evercore Casa de Bolsa, S.A. de C.V.");
            banlis.Add("649|SKANDIA|Skandia Operadora de Fondos, S.A. de C.V.");
            banlis.Add("651|SEGMTY|Seguros Monterrey New York Life, S.A de C.V.");
            banlis.Add("652|ASEA|Solución Asea, S.A. de C.V., Sociedad Financiera Popular");
            banlis.Add("653|KUSPIT|Kuspit Casa de Bolsa, S.A. de C.V.");
            banlis.Add("655|SOFIEXPRESS|J.P. SOFIEXPRESS, S.A. de C.V., S.F.P.");
            banlis.Add("656|UNAGRA|UNAGRA, S.A. de C.V., S.F.P.");
            banlis.Add("659|OPCIONES EMPRESARIALES DEL NOROESTE|OPCIONES EMPRESARIALES DEL NORESTE, S.A. DE C.V., S.F.P.");
            banlis.Add("901|CLS|Cls Bank International");
            banlis.Add("902|INDEVAL|SD. Indeval, S.A. de C.V.");
            banlis.Add("670|LIBERTAD|Libertad Servicios Financieros, S.A. De C.V.");

            foreach (string ob in banlis)
            {
                Banco ban = null;
                string[] arrtoks = ob.Split('|');

                ban = ObjectSpace.FindObject<Banco>(CriteriaOperator.Parse(string.Format("Clave == '{0}'", arrtoks[0])));
                if (ban == null)
                {
                    ban = ObjectSpace.CreateObject<Banco>();
                    ban.Clave = arrtoks[0];
                    ban.Nombre = arrtoks[1];
                    ban.RazonScl = arrtoks[2];
                }
            }*/
        }

        private void CreateReport(string reportName)
        {
            ReportData reportdata = ObjectSpace.FindObject<ReportData>(new BinaryOperator("Name", reportName));

            if (reportdata == null)
            {
                reportdata = ObjectSpace.CreateObject<ReportData>();
                XafReport rep = new XafReport();
                rep.ObjectSpace = ObjectSpace;
                rep.LoadLayout(GetType().Assembly.GetManifestResourceStream(String.Format("LCap.Module.Reportes.{0}.repx", reportName)));
                rep.ReportName = reportName;
                reportdata.SaveReport(rep);
            }
        }

        private void CreateDataToBeAnalysed()
        {
            MyAnalysis facturasAnalysis = ObjectSpace.FindObject<MyAnalysis>(CriteriaOperator.Parse("Name='Ventas Clientes'"));

            if (facturasAnalysis == null)
            {
                facturasAnalysis = ObjectSpace.CreateObject<MyAnalysis>();
                facturasAnalysis.Name = "Ventas Clientes";
                facturasAnalysis.ObjectTypeName = typeof(DocumentoSalida).FullName;
                facturasAnalysis.Criteria = "[Status] <> ##Enum#cap.Elementales.DocumentoStatus,Cancelado#";
                facturasAnalysis.Save();
            }

            facturasAnalysis = ObjectSpace.FindObject<MyAnalysis>(CriteriaOperator.Parse("Name='Ventas Status'"));

            if (facturasAnalysis == null)
            {
                facturasAnalysis = ObjectSpace.CreateObject<MyAnalysis>();
                facturasAnalysis.Name = "Ventas Status";
                facturasAnalysis.ObjectTypeName = typeof(DocumentoSalida).FullName;
                // facturasAnalysis.Criteria = "[Status] <> ##Enum#FCap.Module.BusinessObjects.DocumentoStatus,Cancelado#";
                facturasAnalysis.Save();
            }

            facturasAnalysis = ObjectSpace.FindObject<MyAnalysis>(CriteriaOperator.Parse("Name='Ventas Productos'"));

            if (facturasAnalysis == null)
            {
                facturasAnalysis = ObjectSpace.CreateObject<MyAnalysis>();
                facturasAnalysis.Name = "Ventas Productos";
                facturasAnalysis.ObjectTypeName = typeof(PartidaSalida).FullName;
                // facturasAnalysis.Criteria = "[Documento.Status] <> ##Enum#cap.Elementales.DocumentoStatus,Cancelado#";
                facturasAnalysis.Save();
            }

            facturasAnalysis = ObjectSpace.FindObject<MyAnalysis>(CriteriaOperator.Parse("Name='Ventas Vendedores'"));

            if (facturasAnalysis == null)
            {
                facturasAnalysis = ObjectSpace.CreateObject<MyAnalysis>();
                facturasAnalysis.Name = "Ventas Vendedores";
                facturasAnalysis.ObjectTypeName = typeof(DocumentoSalida).FullName;
                facturasAnalysis.Save();
            }
        }
    }
}
