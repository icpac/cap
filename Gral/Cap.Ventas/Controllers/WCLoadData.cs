using System.Collections.Generic;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using System.IO;
using Cap.Generales.BusinessObjects.General;
using System;
using Cap.Fe.BusinessObjects;
using Cap.Generales.BusinessObjects.Unidades;
using Cap.Generales.BusinessObjects.Empresa;
using DevExpress.Xpo;
using Cap.Inventarios.BusinessObjects;

namespace Cap.Ventas.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppWindowControllertopic.aspx.
    public partial class WCLoadData : WindowController
    {
        public WCLoadData()
        {
            InitializeComponent();
            // Target required Windows (via the TargetXXX properties) and create their Actions.
            TargetWindowType = WindowType.Main;

            simpleActionUpldCtlg.TargetObjectType = typeof(Catalogo);
            simpleActionUpldCtlg.TargetViewType = ViewType.DetailView;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target Window.
            UpdateActions();
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void UpdateActions()
        {
            bool puede = SecuritySystem.CurrentUserName == "root";

            this.simpleActionUpldCtlg.Enabled.SetItemValue("SecurityAllowance", puede);
            this.simpleActionUpldCtlg.Active.SetItemValue("Visible", /*puede*/false);

            popupWindowShowActionCrgCtlgsCfdi.Enabled.SetItemValue("SecurityAllowance", puede);
            popupWindowShowActionCrgCtlgsCfdi.Active.SetItemValue("Visible", puede);
        }

        private void simpleActionUpldCtlg_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            CrgrCtlg("Catalogo.txt");
        }

        private void CrgrCtlg(string flnm)
        {
            /* TI No para este sistema
            IObjectSpace os = Application.CreateObjectSpace();
            List<string> lineas = new List<string>();

            lineas.Clear();
            using (StreamReader sr = new StreamReader(flnm, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    lineas.Add(line);
                }
            }


            foreach (string ob in lineas)
            {
                if (!string.IsNullOrEmpty(ob.Trim()))
                {
                    Catalogo objt = null;
                    string[] arrtoks = ob.Split('|');

                    objt = os.FindObject<Catalogo>(new BinaryOperator("Nombre", arrtoks[0]));
                    if (objt == null)
                    {
                        objt = os.CreateObject<Catalogo>();
                        objt.Nombre = arrtoks[0].ToUpper();
                        objt.Tps / *.Tipo* / = arrtoks[1].Trim() == "FormaPago" ? ETipoSrvc.FormaPago
                            : arrtoks[1].Trim() == "TipoComprobante" ? ETipoSrvc.TipoComprobante
                            : arrtoks[1].Trim() == "Profesion" ? ETipoSrvc.Profesion
                            : arrtoks[1].Trim() == "Puesto" ? ETipoSrvc.Puesto
                            : arrtoks[1].Trim() == "Horario" ? ETipoSrvc.Horario
                            : arrtoks[1].Trim() == "Salario" ? ETipoSrvc.Salario
                            : ETipoSrvc.FormaPago; //.Clasificacion;
                    }
                }
            }
            os.CommitChanges();*/
        }

        private void popupWindowShowActionCrgCtlgsCfdi_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            FilterCFDI newObj;

            newObj = objectSpace.FindObject<FilterCFDI>(null);
            if (newObj != null)
                objectSpace.Delete(newObj);
             
            newObj = objectSpace.CreateObject<FilterCFDI>();
            e.View = Application.CreateDetailView(objectSpace, "FilterCFDI_DetailView", true, newObj);
        }

        private void popupWindowShowActionCrgCtlgsCfdi_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            FilterCFDI obj = e.PopupWindowViewCurrentObject as FilterCFDI;

            if (obj != null && obj.File != null && !string.IsNullOrEmpty(obj.File.FileName))
            {
                Cfdi.Cfdi.LoadData(obj, Application.CreateObjectSpace(), Application.ConnectionString);

                /*
                // aquí debe convertir los bytes del archivo cargado hacia libro excel

                // errorEn = "No se pudo cargar el archivo. Posiblemente esté dañado o no corresponda al formato adecuado.";
                Workbook wb = new Workbook();

                DocumentFormat tipoLibro = Path.GetExtension(obj.File.FileName.ToUpper()).Contains("XLSX") ? DocumentFormat.Xlsx : DocumentFormat.Xls;
                wb.LoadDocument(obj.File.Content, tipoLibro);

                // errorEn = "No se pudo procesar el archivo. Posiblemente esté dañado.";
                if (wb != null && wb.Worksheets.Count > 0)
                {
                    if (obj.FrmnsPg)
                    {
                        Worksheet fp = wb.Worksheets["c_FormaPago"];

                        cargaFormaPago(fp, obj.Rngln);
                    }

                    if (obj.Rgmn)
                    {
                        Worksheet mtd = wb.Worksheets["c_RegimenFiscal"];

                        cargaRegimen(mtd, obj.Rngln);
                    }

                    if (obj.UsCFDI)
                    {
                        Worksheet mtd = wb.Worksheets["c_USOCFDI"];

                        cargaUsoCfdi(mtd, 7);
                    }

                    if (obj.PrdctsSrvcs)
                    {
                        Worksheet mtd = wb.Worksheets["c_ClaveProdServ"];

                        cargaProdServ(mtd, obj.Rngln, obj.RnglnFnl);
                    }

                    if (obj.Undds)
                    {
                        Worksheet mtd = wb.Worksheets["c_ClaveUnidad"];

                        cargaUndds(mtd, obj.Rngln);
                    }

                    if (obj.Mnds)
                    {
                        Worksheet md = wb.Worksheets["c_Moneda"];

                        cargaMonedas(md, 6);
                    }

                    if (obj.Impsts)
                    {
                        Worksheet md = wb.Worksheets["c_Impuesto"];

                        cargaImpuestos(md, 6);
                    }

                    if (obj.TpRlcn)
                    {
                        Worksheet md = wb.Worksheets["c_TipoRelacion"];

                        cargaTipoRelacion(md, 6);
                    }

                    if (obj.TpPrcpcns)
                    {
                        Worksheet md = wb.Worksheets["c_TipoPercepcion"];

                        cargaTipoPercepcion(md, obj.Rngln);
                    }

                    if (obj.TpDdcns)
                    {
                        Worksheet md = wb.Worksheets["c_TipoDeduccion"];

                        cargaTipoDeduccion(md, obj.Rngln);
                    }

                    if (obj.Rsg)
                    {
                        Worksheet md = wb.Worksheets["c_RiesgoPuesto"];

                        cargaRiesgos(md, obj.Rngln);
                    }

                    if (obj.Incpcdd)
                    {
                        Worksheet md = wb.Worksheets["c_TipoIncapacidad"];

                        cargaIncapacidad(md, obj.Rngln);
                    }

                    if (obj.TpCntrt)
                    {
                        Worksheet md = wb.Worksheets["c_TipoContrato"];

                        cargaTipoContratos(md, obj.Rngln);
                    }

                    if (obj.Prdcdd)
                    {
                        Worksheet md = wb.Worksheets["c_PeriodicidadPago"];

                        cargaCatalogosNomina(md, obj.Rngln, 0);
                    }

                    if (obj.TpNmn)
                    {
                        Worksheet md = wb.Worksheets["c_TipoNomina"];

                        cargaCatalogosNomina(md, obj.Rngln, 1);
                    }
                }*/
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {

        }

        // Carga datos Marca
        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
        }

        /*
        private void cargaMetodoPago(Worksheet ws, int renIni, int renFin)
        {
            int iclave, idscrpcn;
            string colClave = "A";
            string colDscrpcn = "B";

            iclave = NegocioAdmin.IndiceDeColumna(ws, colClave);
            idscrpcn = NegocioAdmin.IndiceDeColumna(ws, colDscrpcn);

            Range rango = ws.GetUsedRange();
            if (rango.ColumnCount >= 2)
            {
                int i;
                string clv;
                IObjectSpace objectSpace = Application.CreateObjectSpace();
                int rf = renFin == 0 ? rango.RowCount : renFin;

                for (i = renIni - 1; i < rf; i++)
                {
                    clv = NegocioAdmin.CeldaTexto(ws, i, iclave);

                    if (!string.IsNullOrEmpty(clv))
                    {
                        if (objectSpace.FindObject<Pago>(new BinaryOperator("Clv", clv)) == null)
                        {
                            Pago pg = objectSpace.CreateObject<Pago>();

                            pg.Clv = clv;
                            pg.Descrip = NegocioAdmin.CeldaTexto(ws, i, idscrpcn);
                            pg.Tipo = TipoPago.Metodo; //.Leyenda;
                        }
                    }
                }
                objectSpace.CommitChanges();
            }
        }*/


        /*
        private void cargaRegimen(Worksheet ws, int renIni)
        {
            int iclave, idscrpcn;
            string colClave = "A";
            string colDscrpcn = "B";

            iclave = NegocioAdmin.IndiceDeColumna(ws, colClave);
            idscrpcn = NegocioAdmin.IndiceDeColumna(ws, colDscrpcn);

            Range rango = ws.GetUsedRange();
            if (rango.ColumnCount >= 2)
            {
                int i;
                int clv;
                IObjectSpace objectSpace = Application.CreateObjectSpace();

                for (i = renIni - 1; i < rango.RowCount; i++)
                {
                    // clv = Negocio.CeldaTexto(ws, i, iclave);
                    clv = NegocioAdmin.CeldaEntero(ws, i, iclave);

                    if (clv > 0)
                    {
                        if (objectSpace.FindObject<Regimen>(new BinaryOperator("Clv", clv.ToString())) == null)
                        {
                            Regimen rgm = objectSpace.CreateObject<Regimen>();

                            rgm.Clv = clv.ToString();
                            rgm.Dscrpcn = NegocioAdmin.CeldaTexto(ws, i, idscrpcn);
                        }
                    }
                }
                objectSpace.CommitChanges();
            }
        }*/

        /*
        private void cargaUsoCfdi(Worksheet ws, int renIni)
        {
            int iclave, idscrpcn;
            string colClave = "A";
            string colDscrpcn = "B";

            iclave = NegocioAdmin.IndiceDeColumna(ws, colClave);
            idscrpcn = NegocioAdmin.IndiceDeColumna(ws, colDscrpcn);

            Range rango = ws.GetUsedRange();
            if (rango.ColumnCount >= 2)
            {
                int i;
                string clv;
                IObjectSpace objectSpace = Application.CreateObjectSpace();

                for (i = renIni - 1; i < rango.RowCount; i++)
                {
                    clv = NegocioAdmin.CeldaTexto(ws, i, iclave);

                    if (!string.IsNullOrEmpty(clv))
                    {
                        if (objectSpace.FindObject<Pago>(new BinaryOperator("Clv", clv)) == null)
                        {
                            Pago pg = objectSpace.CreateObject<Pago>();

                            pg.Clv = clv;
                            pg.Descrip = NegocioAdmin.CeldaTexto(ws, i, idscrpcn);
                            pg.Tipo = TipoPago.UsoCFDI;
                        }
                    }
                }
                objectSpace.CommitChanges();
            }
        }*/

        /*
        private void cargaProdServ(Worksheet ws, int renIni, int renFin)
        {
            int iclave, idscrpcn;
            string colClave = "A";
            string colDscrpcn = "B";

            iclave = NegocioAdmin.IndiceDeColumna(ws, colClave);
            idscrpcn = NegocioAdmin.IndiceDeColumna(ws, colDscrpcn);

            Range rango = ws.GetUsedRange();
            if (rango.ColumnCount >= 2)
            {
                int i;
                string clv;
                IObjectSpace objectSpace = Application.CreateObjectSpace();
                int rf = renFin == 0 ? rango.RowCount : renFin;

                for (i = renIni - 1; i < rf; i++)
                {
                    Range celda = ws.Cells[i, iclave];
                    if (celda.Value.Type == CellValueType.Text)
                        clv = NegocioAdmin.CeldaTexto(ws, i, iclave);
                    else
                    {
                        int cv = NegocioAdmin.CeldaEntero(ws, i, iclave);
                        clv = cv.ToString();
                    }


                    if (!string.IsNullOrEmpty(clv))
                    {
                        / *
                        ProductoServicio ps = objectSpace.CreateObject<ProductoServicio>();

                        ps.Clv = clv;
                        ps.Dscrpcn = Negocio.CeldaTexto(ws, i, idscrpcn);* /
                        // ps.Save(); No funciona sin Save y con Save

                        / *
                        if (i % 16 == 0)
                        {
                            objectSpace.CommitChanges();
                            objectSpace.Dispose();
                            objectSpace = Application.CreateObjectSpace();
                        }* /

                        if (objectSpace.FindObject<ProductoServicio>(new BinaryOperator("Clv", clv)) == null)
                        {
                            / *
                            string insertQuery = string.Format("INSERT INTO \"ProductoServicio\" (\"Oid\", \"Clv\", \"Dscrpcn\") VALUES ('{0}', '{1}', '{2}')", Guid.NewGuid(), clv, NegocioAdmin.CeldaTexto(ws, i, idscrpcn));

                            ((DevExpress.ExpressApp.Xpo.XPObjectSpace)objectSpace).Session.ExecuteNonQuery(insertQuery);* /

                            string insertQuery = string.Empty;
                            if (!Application.ConnectionString.Contains("postgres"))
                            {
                                insertQuery = string.Format(@"INSERT INTO [ProductoServicio] ([Oid], [Clv], [Dscrpcn]) VALUES ('{0}', '{1}', @d)", Guid.NewGuid(), clv);
                            }
                            else
                            {
                                insertQuery = string.Format("INSERT INTO \"ProductoServicio\" (\"Oid\", \"Clv\", \"Dscrpcn\") VALUES ('{0}', '{1}', @d)", Guid.NewGuid(), clv);
                            }
                            ((DevExpress.ExpressApp.Xpo.XPObjectSpace)objectSpace).Session.ExecuteNonQuery(insertQuery, new string[] { "d" }, new object[] { NegocioAdmin.CeldaTexto(ws, i, idscrpcn) });
                        }
                    }
                }
                / *
                objectSpace.CommitChanges();* /
            }
        }*/


        /*
        private void cargaUndds(Worksheet ws, int renIni)
        {
            int iclave, inmbr, idscrpcn, isgls;
            string colClave = "A";
            string colNmbr = "B";
            string colDscrpcn = "C";
            string colSgls = "F";

            iclave = NegocioAdmin.IndiceDeColumna(ws, colClave);
            inmbr = NegocioAdmin.IndiceDeColumna(ws, colNmbr);
            idscrpcn = NegocioAdmin.IndiceDeColumna(ws, colDscrpcn);
            isgls = NegocioAdmin.IndiceDeColumna(ws, colSgls);

            Range rango = ws.GetUsedRange();
            if (rango.ColumnCount >= 2)
            {
                int i;
                string clv;
                IObjectSpace objectSpace = Application.CreateObjectSpace();

                UnitOfWork uw = new UnitOfWork(((DevExpress.ExpressApp.Xpo.XPObjectSpaceProvider)Application.ObjectSpaceProvider).DataLayer);

                for (i = renIni - 1; i < rango.RowCount; i++)
                {
                    Range celda = ws.Cells[i, iclave];
                    if (celda.Value.Type == CellValueType.Text)
                        clv = NegocioAdmin.CeldaTexto(ws, i, iclave);
                    else
                    {
                        int cv = NegocioAdmin.CeldaEntero(ws, i, iclave);
                        clv = cv.ToString();
                    }


                    if (!string.IsNullOrEmpty(clv))
                    {
                        if (objectSpace.FindObject<Unidad>(new BinaryOperator("Clave", clv)) == null)
                        {

                            Unidad und = objectSpace.CreateObject<Unidad>();

                            und.Clave = clv;
                            und.Nmbr = NegocioAdmin.CeldaTexto(ws, i, inmbr);
                            und.Descripcion = NegocioAdmin.CeldaTexto(ws, i, idscrpcn);
                            und.Siglas = NegocioAdmin.CeldaTexto(ws, i, isgls);


                            // ps.Save(); No funciona sin Save y con Save

                            if (i % 64 == 0)
                            {
                                objectSpace.CommitChanges();
                                objectSpace.Dispose();
                                objectSpace = Application.CreateObjectSpace();
                            }
                        }

                        / *
                        string insertQuery = string.Format(@"INSERT INTO [ProductoServicio] ([Oid], [Clv], [Dscrpcn]) VALUES ('{0}', '{1}', @d)", Guid.NewGuid(), clv);

                        ((DevExpress.ExpressApp.Xpo.XPObjectSpace)objectSpace).Session.ExecuteNonQuery(insertQuery, new string[] { "d" }, new object[] { Negocio.CeldaTexto(ws, i, idscrpcn) });* /
                    }
                }
                objectSpace.CommitChanges();
            }
        }*/


        /*
        private void cargaMonedas(Worksheet ws, int renIni)
        {
            int iclave, idscrpcn;
            string colClave = "A";
            string colDscrpcn = "B";

            iclave = NegocioAdmin.IndiceDeColumna(ws, colClave);
            idscrpcn = NegocioAdmin.IndiceDeColumna(ws, colDscrpcn);

            Range rango = ws.GetUsedRange();
            if (rango.ColumnCount >= 3)
            {
                int i;
                IObjectSpace objectSpace = Application.CreateObjectSpace();

                for (i = renIni - 1; i < rango.RowCount; i++)
                {
                    string aux = NegocioAdmin.CeldaTexto(ws, i, iclave);
                    
                    if (!string.IsNullOrEmpty(aux) && objectSpace.FindObject<Moneda>(new BinaryOperator("Clave", Moneda.ClaveFto(aux))) == null)
                    {
                        Moneda mnd = objectSpace.CreateObject<Moneda>();

                        mnd.Clave = aux;
                        mnd.Descripcion = NegocioAdmin.CeldaTexto(ws, i, idscrpcn);
                        //  viene un dato de numero de decimales que no se si se use
                    }
                }
                objectSpace.CommitChanges();
            }
        }*/


        /*
        private void cargaImpuestos(Worksheet ws, int renIni)
        {
            int iclave, idscrpcn, irtn, itrs;
            string colClave = "A";
            string colDscrpcn = "B";
            string colRtnc = "C";
            string colTrsld = "D";

            iclave = NegocioAdmin.IndiceDeColumna(ws, colClave);
            idscrpcn = NegocioAdmin.IndiceDeColumna(ws, colDscrpcn);
            irtn = NegocioAdmin.IndiceDeColumna(ws, colRtnc);
            itrs = NegocioAdmin.IndiceDeColumna(ws, colTrsld);

            Range rango = ws.GetUsedRange();
            if (rango.ColumnCount >= 3)
            {
                int i;
                IObjectSpace objectSpace = Application.CreateObjectSpace();

                for (i = renIni - 1; i < rango.RowCount; i++)
                {
                    int aux = NegocioAdmin.CeldaEntero(ws, i, iclave);
                    string aux2 = aux.ToString();
                    string aux3 = aux2.PadLeft(3, '0');

                    if (!string.IsNullOrEmpty(aux3) && objectSpace.FindObject<Impuesto>(new BinaryOperator("Clv", aux3)) == null)
                    {
                        Impuesto impt = objectSpace.CreateObject<Impuesto>();

                        impt.Clv = aux3;
                        impt.Dscrpcn = NegocioAdmin.CeldaTexto(ws, i, idscrpcn);
                        impt.Rtncn = NegocioAdmin.CeldaTexto(ws, i, irtn) == "Si";
                        impt.Trsld = NegocioAdmin.CeldaTexto(ws, i, itrs) == "Si";

                        //  viene un dato de numero de decimales que no se si se use
                    }
                }
                objectSpace.CommitChanges();
            }
        }*/


        /*
        private void cargaTipoRelacion(Worksheet ws, int renIni)
        {
            int iclave, idscrpcn;
            string colClave = "A";
            string colDscrpcn = "B";

            iclave = NegocioAdmin.IndiceDeColumna(ws, colClave);
            idscrpcn = NegocioAdmin.IndiceDeColumna(ws, colDscrpcn);

            Range rango = ws.GetUsedRange();
            if (rango.ColumnCount >= 3)
            {
                int i;
                IObjectSpace objectSpace = Application.CreateObjectSpace();

                for (i = renIni - 1; i < rango.RowCount; i++)
                {
                    int aux = NegocioAdmin.CeldaEntero(ws, i, iclave);
                    string aux2 = string.Empty;
                    string aux3 = string.Empty;
                    if (aux != 0)
                    {
                        aux2 = aux.ToString();
                        aux3 = aux2.PadLeft(2, '0');
                    }
                    else
                    {
                        aux2 = NegocioAdmin.CeldaTexto(ws, i, iclave);
                        aux3 = aux2.Trim();
                    }
                    GroupOperator fil = new GroupOperator();

                    fil.Operands.Add(new BinaryOperator("Clv", aux3));
                    fil.Operands.Add(new BinaryOperator("Tipo", TipoPago.TipoRelacion));

                    if (!string.IsNullOrEmpty(aux3)
                        && objectSpace.FindObject<Pago>(fil) == null)
                    {
                        Pago pg = objectSpace.CreateObject<Pago>();

                        pg.Clv = aux3;
                        pg.Descrip = NegocioAdmin.CeldaTexto(ws, i, idscrpcn);
                        pg.Tipo = TipoPago.TipoRelacion;

                    }
                }
                objectSpace.CommitChanges();
            }
        }*/


        /*
        private void cargaTipoPercepcion(Worksheet ws, int renIni)
        {
            int iclave, idscrpcn;
            string colClave = "A";
            string colDscrpcn = "B";

            iclave = NegocioAdmin.IndiceDeColumna(ws, colClave);
            idscrpcn = NegocioAdmin.IndiceDeColumna(ws, colDscrpcn);

            Range rango = ws.GetUsedRange();
            if (rango.ColumnCount >= 3)
            {
                int i;
                IObjectSpace objectSpace = Application.CreateObjectSpace();

                for (i = renIni - 1; i < rango.RowCount; i++)
                {
                    int aux = NegocioAdmin.CeldaEntero(ws, i, iclave);
                    string aux2 = string.Empty;
                    string aux3 = string.Empty;
                    if (aux != 0)
                    {
                        aux2 = aux.ToString();
                        aux3 = aux2.PadLeft(3, '0');
                    }
                    else
                    {
                        aux2 = NegocioAdmin.CeldaTexto(ws, i, iclave);
                        aux3 = aux2.Trim();
                    }
                    if (!string.IsNullOrEmpty(aux3) 
                        && objectSpace.FindObject<TipoPer>(new BinaryOperator("Clave", aux3)) == null)
                    {
                        TipoPer impt = objectSpace.CreateObject<TipoPer>();

                        impt.Clave = aux3;
                        impt.Descrip = NegocioAdmin.CeldaTexto(ws, i, idscrpcn);

                        //  viene un dato de numero de decimales que no se si se use
                    }
                }
                objectSpace.CommitChanges();
            }
        }*/


        /*
        private void cargaTipoDeduccion(Worksheet ws, int renIni)
        {
            int iclave, idscrpcn;
            string colClave = "A";
            string colDscrpcn = "B";

            iclave = NegocioAdmin.IndiceDeColumna(ws, colClave);
            idscrpcn = NegocioAdmin.IndiceDeColumna(ws, colDscrpcn);

            Range rango = ws.GetUsedRange();
            if (rango.ColumnCount >= 3)
            {
                int i;
                IObjectSpace objectSpace = Application.CreateObjectSpace();

                for (i = renIni - 1; i < rango.RowCount; i++)
                {
                    int aux = NegocioAdmin.CeldaEntero(ws, i, iclave);
                    string aux2 = string.Empty;
                    string aux3 = string.Empty;
                    if (aux != 0)
                    {
                        aux2 = aux.ToString();
                        aux3 = aux2.PadLeft(3, '0');
                    }
                    else
                    {
                        aux2 = NegocioAdmin.CeldaTexto(ws, i, iclave);
                        aux3 = aux2.Trim();
                    }
                    if (!string.IsNullOrEmpty(aux3)
                        && objectSpace.FindObject<TipoDed>(new BinaryOperator("Clave", aux3)) == null)
                    {
                        TipoDed impt = objectSpace.CreateObject<TipoDed>();

                        impt.Clave = aux3;
                        impt.Descrip = NegocioAdmin.CeldaTexto(ws, i, idscrpcn);

                        //  viene un dato de numero de decimales que no se si se use
                    }
                }
                objectSpace.CommitChanges();
            }
        }*/


        /*
        private void cargaRiesgos(Worksheet ws, int renIni)
        {
            int iclave, idscrpcn;
            string colClave = "A";
            string colDscrpcn = "B";

            iclave = NegocioAdmin.IndiceDeColumna(ws, colClave);
            idscrpcn = NegocioAdmin.IndiceDeColumna(ws, colDscrpcn);

            Range rango = ws.GetUsedRange();
            if (rango.ColumnCount >= 3)
            {
                int i;
                IObjectSpace objectSpace = Application.CreateObjectSpace();

                for (i = renIni - 1; i < rango.RowCount; i++)
                {
                    int aux = NegocioAdmin.CeldaEntero(ws, i, iclave);
                    string aux2 = string.Empty;
                    string aux3 = string.Empty;
                    if (aux != 0)
                    {
                        aux2 = aux.ToString();
                        aux3 = aux2.PadLeft(3, '0');
                    }
                    else
                    {
                        aux2 = NegocioAdmin.CeldaTexto(ws, i, iclave);
                        aux3 = aux2.Trim();
                    }
                    if (!string.IsNullOrEmpty(aux3)
                        && objectSpace.FindObject<Riesgo>(new BinaryOperator("Clave", aux3)) == null)
                    {
                        Riesgo impt = objectSpace.CreateObject<Riesgo>();

                        impt.Clave = aux3;
                        impt.Descrip = NegocioAdmin.CeldaTexto(ws, i, idscrpcn);

                        //  viene un dato de numero de decimales que no se si se use
                    }
                }
                objectSpace.CommitChanges();
            }
        }*/

        /*
        private void cargaIncapacidad(Worksheet ws, int renIni)
        {
            int iclave, idscrpcn;
            string colClave = "A";
            string colDscrpcn = "B";

            iclave = NegocioAdmin.IndiceDeColumna(ws, colClave);
            idscrpcn = NegocioAdmin.IndiceDeColumna(ws, colDscrpcn);

            Range rango = ws.GetUsedRange();
            if (rango.ColumnCount >= 3)
            {
                int i;
                IObjectSpace objectSpace = Application.CreateObjectSpace();

                for (i = renIni - 1; i < rango.RowCount; i++)
                {
                    int aux = NegocioAdmin.CeldaEntero(ws, i, iclave);
                    string aux2 = string.Empty;
                    string aux3 = string.Empty;
                    if (aux != 0)
                    {
                        aux2 = aux.ToString();
                        aux3 = aux2.PadLeft(2, '0');
                    }
                    else
                    {
                        aux2 = NegocioAdmin.CeldaTexto(ws, i, iclave);
                        aux3 = aux2.Trim();
                    }
                    if (!string.IsNullOrEmpty(aux3)
                        && objectSpace.FindObject<Incapacidad>(new BinaryOperator("Clave", aux3)) == null)
                    {
                        Incapacidad impt = objectSpace.CreateObject<Incapacidad>();

                        impt.Clave = aux3;
                        impt.Descrip = NegocioAdmin.CeldaTexto(ws, i, idscrpcn);

                        //  viene un dato de numero de decimales que no se si se use
                    }
                }
                objectSpace.CommitChanges();
            }
        }*/


        /*
        private void cargaTipoContratos(Worksheet ws, int renIni)
        {
            int iclave, idscrpcn;
            string colClave = "A";
            string colDscrpcn = "B";

            iclave = NegocioAdmin.IndiceDeColumna(ws, colClave);
            idscrpcn = NegocioAdmin.IndiceDeColumna(ws, colDscrpcn);

            Range rango = ws.GetUsedRange();
            if (rango.ColumnCount >= 3)
            {
                int i;
                IObjectSpace objectSpace = Application.CreateObjectSpace();

                for (i = renIni - 1; i < rango.RowCount; i++)
                {
                    int aux = NegocioAdmin.CeldaEntero(ws, i, iclave);
                    string aux2 = string.Empty;
                    string aux3 = string.Empty;
                    if (aux != 0)
                    {
                        aux2 = aux.ToString();
                        aux3 = aux2.PadLeft(2, '0');
                    }
                    else
                    {
                        aux2 = NegocioAdmin.CeldaTexto(ws, i, iclave);
                        aux3 = aux2.Trim();
                    }
                    if (!string.IsNullOrEmpty(aux3)
                        && objectSpace.FindObject<Contratacion>(new BinaryOperator("Clave", aux3)) == null)
                    {
                        Contratacion impt = objectSpace.CreateObject<Contratacion>();

                        impt.Clave = aux3;
                        impt.Descrip = NegocioAdmin.CeldaTexto(ws, i, idscrpcn);

                        //  viene un dato de numero de decimales que no se si se use
                    }
                }
                objectSpace.CommitChanges();
            }
        }*/

    }
}
