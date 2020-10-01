using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.Data;
using DevExpress.Xpo.DB;

namespace FCap.Module.Controllers
{
    using FCap.Module.BusinessObjects.Ventas;
    using DevExpress.ExpressApp.Xpo;
    using DevExpress.Data.Filtering;
    using System.Xml;
    using System.Collections;
    using DevExpress.Xpo;
    using System.IO;
    using FCap.Module.Utilerias;
    using DevExpress.Spreadsheet;
    using Cap.Generales.BusinessObjects.Empresa;
    using System.Drawing;
    using Cap.Ventas.BusinessObjects;
    using Cap.Compras.BusinessObjects;
    using DevExpress.Persistent.Base;
    using sw.descargamasiva;
    using System.IO.Compression;

    // For more information on Controllers and their life cycle, check out the http://documentation.devexpress.com/#Xaf/CustomDocument2621 and http://documentation.devexpress.com/#Xaf/CustomDocument3118 help articles.
    public partial class VCFacturaList : ViewController
    {
        // Use this to do something when a Controller is instantiated (do not execute heavy operations here!).
        public VCFacturaList()
        {
            InitializeComponent();
            RegisterActions(components);
            // For instance, you can specify activation conditions of a Controller or create its Actions (http://documentation.devexpress.com/#Xaf/CustomDocument2622).
            //TargetObjectType = typeof(DomainObject1);
            //TargetViewType = ViewType.DetailView;
            //TargetViewId = "DomainObject1_DetailView";
            //TargetViewNesting = Nesting.Root;
            //SimpleAction myAction = new SimpleAction(this, "MyActionId", DevExpress.Persistent.Base.PredefinedCategory.RecordEdit);
        
            TargetObjectType = typeof(DocumentoSalida);
            TargetViewType = ViewType.ListView;

            simpleActionCncilr.TargetObjectType = typeof(DocumentoSalida);
            simpleActionCncilr.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;

            simpleActionGetXml.TargetObjectType = typeof(DocumentoSalida);
            simpleActionGetXml.TargetViewType = ViewType.ListView;

            simpleActionRprtCntbl.TargetObjectType = typeof(DocumentoSalida);
            simpleActionRprtCntbl.TargetViewType = ViewType.ListView;

            popupWindowShowActionDscrgMsva.TargetObjectType = typeof(DocumentoSalida);
            popupWindowShowActionDscrgMsva.TargetViewType = ViewType.ListView;
        }

        // Override to do something before Controllers are activated within the current Frame (their View property is not yet assigned).
        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();
            //For instance, you can access another Controller via the Frame.GetController<AnotherControllerType>() method to customize it or subscribe to its events.
        }

        // Override to do something when a Controller is activated and its View is assigned.
        protected override void OnActivated()
        {
            base.OnActivated();
            //For instance, you can customize the current View and its editors (http://documentation.devexpress.com/#Xaf/CustomDocument2729) or manage the Controller's Actions visibility and availability (http://documentation.devexpress.com/#Xaf/CustomDocument2728).


            string propertyName = "FechaDoc";
            bool demoFlag = true;
            //Dennis: This code applies a client side sorting.
            if (demoFlag)
            {
                IModelColumn columnInfo = ((IModelList<IModelColumn>)((ListView)View).Model.Columns)[propertyName];
                if (columnInfo != null)
                {
                    columnInfo.SortIndex = 0;
                    columnInfo.SortOrder = ColumnSortOrder.Descending;
                }
            }
            else
            {
                //Dennis: This code is used for the server side sorting.
                if (((IModelList<IModelSortProperty>)((ListView)View).Model.Sorting)[propertyName] == null)
                {
                    IModelSortProperty sortProperty = ((ListView)View).Model.Sorting.AddNode<IModelSortProperty>(propertyName);
                    sortProperty.Direction = SortingDirection.Descending;
                    sortProperty.PropertyName = propertyName;
                }
            }

            UpdateAction();


            bool puede = SecuritySystem.CurrentUserName == "root";
            simpleActionRprtCntbl.Enabled.SetItemValue("SecurityAllowance", puede);
            simpleActionRprtCntbl.Active.SetItemValue("Visible", puede);
        }

        // Override to access the controls of a View for which the current Controller is intended.
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // For instance, refer to the http://documentation.devexpress.com/Xaf/CustomDocument3165.aspx help article to see how to access grid control properties.
        }
        // Override to do something when a Controller is deactivated.
        protected override void OnDeactivated()
        {
            // For instance, you can unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void singleChoiceActionPolz_Execute(object sender, DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventArgs e)
        {
            XPObjectSpace objectSpace = (XPObjectSpace)View.ObjectSpace;
            int mes = Convert.ToInt32((e.SelectedChoiceActionItem.Data));
            int ano = mes == 12 && DateTime.Today.Month == 1 ? DateTime.Today.Year - 1 : DateTime.Today.Year; 
            DateTime mFechaIni = apl.Log.Fecha.FechaInicial(mes, ano);
            DateTime mFechaFin = apl.Log.Fecha.FechaFinal(mes, ano);
            CriteriaOperator[] operands = new CriteriaOperator[2];
            XmlDocument docXml = new XmlDocument();
            Ventas vta = objectSpace.FindObject<Ventas>(null);

            operands[0] = GroupOperator.And(new BinaryOperator("FechaDoc", mFechaIni, BinaryOperatorType.GreaterOrEqual), new BinaryOperator("FechaDoc", mFechaFin, BinaryOperatorType.LessOrEqual));
            operands[1] = new BinaryOperator("Status", DocumentoStatus.Cancelado, BinaryOperatorType.NotEqual);
            
            SortProperty []sortCollection = new SortProperty[1];
            sortCollection[0] = new SortProperty("FechaDoc", SortingDirection.Ascending);

            IList arr = objectSpace.CreateCollection(typeof(DocumentoSalida), new GroupOperator(operands), sortCollection);
            if (arr.Count > 0)
            {
                string filename = string.Format("{0}.POL", mFechaIni.Month);
                using (new apl.Log.CWaitCursor())
                {
                    XmlDeclaration declaracion = docXml.CreateXmlDeclaration("1.0", null, "yes");
                    docXml.InsertBefore(declaracion, docXml.DocumentElement);
                    // docXml.LoadXml("<?xml version=\"1.0\" standalone=\"yes\"?>");
                    XmlElement data = docXml.CreateElement("DATAPACKET");
                    data.SetAttribute("Version", "2.0");
                    docXml.AppendChild(data);

                    XmlElement meta = docXml.CreateElement("METADATA");
                    data.AppendChild(meta);

                    XmlElement fields = docXml.CreateElement("FIELDS");
                    meta.AppendChild(fields);

                    XmlElement field = docXml.CreateElement("FIELD");
                    field.SetAttribute("attrname", "VersionCOI");
                    field.SetAttribute("fieldtype", "i2");
                    fields.AppendChild(field);

                    field = docXml.CreateElement("FIELD");
                    field.SetAttribute("attrname", "TipoPoliz");
                    field.SetAttribute("fieldtype", "string");
                    field.SetAttribute("WIDTH", "2");
                    fields.AppendChild(field);

                    field = docXml.CreateElement("FIELD");
                    field.SetAttribute("attrname", "DiaPoliz");
                    field.SetAttribute("fieldtype", "string");
                    field.SetAttribute("WIDTH", "2");
                    fields.AppendChild(field);

                    field = docXml.CreateElement("FIELD");
                    field.SetAttribute("attrname", "ConcepPoliz");
                    field.SetAttribute("fieldtype", "string");
                    field.SetAttribute("WIDTH", "120");
                    fields.AppendChild(field);

                    XmlElement fieldP = docXml.CreateElement("FIELD");
                    fieldP.SetAttribute("attrname", "Partidas");
                    fieldP.SetAttribute("fieldtype", "nested");
                    fields.AppendChild(fieldP);

                    XmlElement fields2 = docXml.CreateElement("FIELDS");
                    fieldP.AppendChild(fields2);

                    field = docXml.CreateElement("FIELD");
                    field.SetAttribute("attrname", "Cuenta");
                    field.SetAttribute("fieldtype", "string");
                    field.SetAttribute("WIDTH", "21");
                    fields2.AppendChild(field);

                    field = docXml.CreateElement("FIELD");
                    field.SetAttribute("attrname", "Depto");
                    field.SetAttribute("fieldtype", "i4");
                    fields2.AppendChild(field);

                    field = docXml.CreateElement("FIELD");
                    field.SetAttribute("attrname", "ConceptoPol");
                    field.SetAttribute("fieldtype", "string");
                    field.SetAttribute("WIDTH", "120");
                    fields2.AppendChild(field);

                    field = docXml.CreateElement("FIELD");
                    field.SetAttribute("attrname", "Monto");
                    field.SetAttribute("fieldtype", "r8");
                    fields2.AppendChild(field);

                    field = docXml.CreateElement("FIELD");
                    field.SetAttribute("attrname", "TipoCambio");
                    field.SetAttribute("fieldtype", "r8");
                    fields2.AppendChild(field);

                    field = docXml.CreateElement("FIELD");
                    field.SetAttribute("attrname", "DebeHaber");
                    field.SetAttribute("fieldtype", "string");
                    field.SetAttribute("WIDTH", "1");
                    fields2.AppendChild(field);


                    XmlElement parm = docXml.CreateElement("PARAMS");
                    fieldP.AppendChild(parm);

                    XmlElement parm1 = docXml.CreateElement("PARAMS");
                    meta.AppendChild(parm1);

                    XmlElement rowd = docXml.CreateElement("ROWDATA");
                    data.AppendChild(rowd);

                    XmlElement row = docXml.CreateElement("ROW");
                    row.SetAttribute("VersionCOI", "50");
                    row.SetAttribute("TipoPoliz", "Dr");
                    row.SetAttribute("DiaPoliz", string.Format("{0}", arr.Count+1));
                    row.SetAttribute("ConcepPoliz", string.Format("RELACION DE FACTURAS DE {0} {1}", mFechaIni.ToString("MMMM").ToUpper(), mFechaIni.ToString("yyyy")));
                    rowd.AppendChild(row);

                    XmlElement par = docXml.CreateElement("Partidas");
                    row.AppendChild(par);

                    decimal tot = 0;
                    string[] nivs;
                    string aux = string.Empty;

                    foreach (DocumentoSalida doc in arr)
                    {
                        XmlElement part = docXml.CreateElement("ROWPartidas");
                        nivs = string.IsNullOrEmpty(doc.Cliente.Cuenta) ? null : doc.Cliente.Cuenta.Split('-');
                        if (nivs != null)
                            aux = string.Concat(nivs);

                        part.SetAttribute("Cuenta", string.Format("{0}2", aux.PadRight(20, '0')));
                        part.SetAttribute("Depto", "0");

                        aux = string.Format("F-{0}  {1}", doc.Clave.Trim(), ((PartidaSalida)(doc.VentaItems[0])).Descripcion);
                        part.SetAttribute("ConceptoPol", aux.Length > 120 ? aux.Substring(0, 119) : aux);
                        part.SetAttribute("Monto", doc.Total.ToString("F2"));
                        part.SetAttribute("TipoCambio", "1");
                        part.SetAttribute("DebeHaber", "D");
                        par.AppendChild(part);

                        tot += doc.Total;
                    }

                    XmlElement parth = docXml.CreateElement("ROWPartidas");

                    nivs = string.IsNullOrEmpty(vta.Cuenta) ? null : vta.Cuenta.Split('-');
                    if (nivs != null)
                        aux = string.Concat(nivs);

                    parth.SetAttribute("Cuenta", string.Format("{0}2", aux.PadRight(20, '0')));
                    parth.SetAttribute("Depto", "0");
                    parth.SetAttribute("ConceptoPol", string.Format("RELACION DE FACTURAS DE {0} {1}", mFechaIni.ToString("MMMM").ToUpper(), mFechaIni.ToString("yyyy")));
                    parth.SetAttribute("Monto", tot.ToString("F2"));
                    parth.SetAttribute("TipoCambio", "1");
                    parth.SetAttribute("DebeHaber", "H");
                    par.AppendChild(parth);

                    docXml.Save(filename);
                }
            }
        }

        private void simpleActionCncilr_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            if (View != null && View.ObjectSpace != null)
            {
                IObjectSpace objectSpace = Application.CreateObjectSpace(); 
                CriteriaOperator[] operands = new CriteriaOperator[1];
                operands[0] = new BinaryOperator("Concilia", true, BinaryOperatorType.NotEqual);

                SortProperty[] sortCollection = new SortProperty[1];
                sortCollection[0] = new SortProperty("FechaDoc", SortingDirection.Ascending);

                IList arr = objectSpace.CreateCollection(typeof(DocumentoSalida),
                    new GroupOperator(operands), sortCollection);
                Ventas vta = objectSpace.FindObject<Ventas>(null);

                foreach (string file in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml"))
                {
                    XmlDocument xml = new XmlDocument();

                    xml.Load(file);
                    XmlNodeList nl = xml.GetElementsByTagName("tfd:TimbreFiscalDigital");
                    if (nl != null && nl.Count > 0)
                    {
                        XmlNode se = nl[0].Attributes.GetNamedItem("UUID");
                        XmlNode fe = nl[0].Attributes.GetNamedItem("FechaTimbrado");
                        if (se != null)
                        {
                            foreach (DocumentoSalida fac in arr)
                            {
                                if (!string.IsNullOrEmpty(fac.Uuid))
                                {
                                    if (se.Value == fac.Uuid)
                                    {
                                        string aux;
                                        aux = Path.Combine(vta.VntCfdi.RutaPdfVnts, fac.Tipo.ToString());
                                        aux = NegocioAdmin.CreaDirs(aux, fac.FechaDoc);

                                        aux = Path.Combine(aux, Path.GetFileName(file));
                                        File.Move(file, aux);

                                        fac.Concilia = true;
                                    }
                                }
                            }
                        }
                    }
                }
                objectSpace.CommitChanges();
            }
        }

        private void simpleActionGetXml_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
        }

        private void UpdateAction()
        {
            bool puede = SecuritySystem.CurrentUserName == "root";

            this.simpleActionGetXml.Enabled.SetItemValue("SecurityAllowance", puede);
            if (!puede)
                simpleActionGetXml.Active.SetItemValue("Visible", false);

            popupWindowShowActionDscrgMsva.Active.SetItemValue("Visible", Licencia());
        }

        private bool Licencia()
        {
            return true;
        }

        private void simpleActionRprtCntbl_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            if (View != null)
            {
                int i = 5;
                Empresa emp = View.ObjectSpace.FindObject<Empresa>(null); 

                // Servicios Profesionales
                Workbook book = new Workbook();
                var sheet = book.Worksheets.ActiveWorksheet;
                sheet.Cells[0, 0].Value = emp.Compania.Nombre;
                sheet.Cells[0, 3].Value = emp.Compania.Rfc;
                sheet.Cells[1, 0].Value =
                    (emp.Regimenes != null && emp.Regimenes.Count > 0)
                    ? (emp.Regimenes[0] as RegimenEmpresa).Rgmn.Dscrpcn
                    : string.Empty;

                int ano = DateTime.Today.Month == 1 ? DateTime.Today.Year - 1 : DateTime.Today.Year;

                sheet.Cells["A4"].Value = string.Format("Ejercicio {0}.", ano);
                sheet.Cells[i++, 0].Value = "ENERO";
                sheet.Cells[i++, 0].Value = "FEBRERO";
                sheet.Cells[i++, 0].Value = "Acumulado";
                sheet.Cells[i++, 0].Value = "MARZO";
                sheet.Cells[i++, 0].Value = "Acumulado";
                sheet.Cells[i++, 0].Value = "ABRIL";
                sheet.Cells[i++, 0].Value = "Acumulado";
                sheet.Cells[i++, 0].Value = "MAYO";
                sheet.Cells[i++, 0].Value = "Acumulado";
                sheet.Cells[i++, 0].Value = "JUNIO";
                sheet.Cells[i++, 0].Value = "Acumulado";
                sheet.Cells[i++, 0].Value = "JULIO";
                sheet.Cells[i++, 0].Value = "Acumulado";
                sheet.Cells[i++, 0].Value = "AGOSTO";
                sheet.Cells[i++, 0].Value = "Acumulado";
                sheet.Cells[i++, 0].Value = "SEPTIEMBRE";
                sheet.Cells[i++, 0].Value = "Acumulado";
                sheet.Cells[i++, 0].Value = "OCTUBRE";
                sheet.Cells[i++, 0].Value = "Acumulado";
                sheet.Cells[i++, 0].Value = "NOVIEMBRE";
                sheet.Cells[i++, 0].Value = "Acumulado";
                sheet.Cells[i++, 0].Value = "DICIEMBRE";
                sheet.Cells[i++, 0].Value = "Tota Acumulado";

                sheet.Cells["c3"].Value = "ISR";
                sheet.Cells["c3"].Font.FontStyle = SpreadsheetFontStyle.Bold;


                // Access the range of cells to be formatted.
                CellRange range = sheet.Range["C4:G4"];

                // Begin updating of the range formatting. 
                DevExpress.Spreadsheet.Formatting rangeFormatting = range.BeginUpdateFormatting();

                // Specify font settings (font name, color, size and style).
                rangeFormatting.Font.Name = "Arial";
                // rangeFormatting.Font.Color = Color.Blue;
                rangeFormatting.Font.Size = 8;
                // rangeFormatting.Font.FontStyle = SpreadsheetFontStyle.Bold;

                // Specify cell background color.
                rangeFormatting.Fill.BackgroundColor = Color.LightGray;

                // Specify text alignment in cells.
                rangeFormatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                rangeFormatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                rangeFormatting.Alignment.WrapText = true;
                // End updating of the range formatting.
                range.EndUpdateFormatting(rangeFormatting);





                range = sheet.Range["G3:K3"];

                // Begin updating of the range formatting. 
                rangeFormatting = range.BeginUpdateFormatting();

                // Specify font settings (font name, color, size and style).
                rangeFormatting.Font.Name = "Arial";
                // rangeFormatting.Font.Color = Color.Blue;
                rangeFormatting.Font.Size = 8;
                // rangeFormatting.Font.FontStyle = SpreadsheetFontStyle.Bold;

                // Specify cell background color.
                rangeFormatting.Fill.BackgroundColor = Color.LightGray;

                // Specify text alignment in cells.
                rangeFormatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                rangeFormatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                rangeFormatting.Alignment.WrapText = true;

                // End updating of the range formatting.
                range.EndUpdateFormatting(rangeFormatting);


                sheet.Cells["c4"].Value = string.Format("INGRESOS{0}(Cobrados por sus{0}ventas o servicios){0}Sin incluir IVA",
                    Environment.NewLine);

                sheet.Columns[2].Width = 350;
                sheet.Columns[3].Width = 340;
                sheet.Columns[4].Width = 340;

                sheet.Columns[6].Width = 330;
                sheet.Columns[7].Width = 330;
                sheet.Columns[8].Width = 330;
                sheet.Columns[9].Width = 330;
                sheet.Columns[10].Width = 330;


                sheet.Cells["d4"].Value = string.Format("DEDUCCIONES{0}(Compras y/o{0}gastos, sin incluir{0}IVA",
                    Environment.NewLine);
                sheet.Cells["e4"].Value = string.Format("INGRESOS ACUMULABLES -{0}DEDUCCIONES ACUMULABLES ={0}BASE",
                    Environment.NewLine);
                sheet.Cells["f4"].Value = "ISR RETENIDO";
                
                sheet.Cells["g3"].Value = "PAGOS (Provisionales)";
                sheet.Cells["g4"].Value = "ISR";
                sheet.Rows[3].Height = 260;

                sheet.Cells["h3"].Value = string.Format("IVA{0}(Causado por{0}sus ventas o{0}servicios)",
                    Environment.NewLine);
                sheet.Cells["i3"].Value = string.Format("IVA{0}(Acreditable por{0}sus compras y/o{0}gastos)",
                    Environment.NewLine);
                sheet.Cells["j3"].Value = string.Format("IVA{0}RETENIDO", Environment.NewLine);
                sheet.Cells["k3"].Value = string.Format("IVA PAGADO (+){0}O{0}A FAVOR (-)", Environment.NewLine);
                sheet.Cells["l3"].Value = "DIOT";
                sheet.Rows[2].Height = 240;

                decimal[] total = new decimal[12];
                decimal[] reten = new decimal[12];
                decimal[] ivaTras = new decimal[12];
                decimal[] ivaRet = new decimal[12];
                decimal[] ivaAcr = new decimal[12];
                decimal[] totAcm = new decimal[12];
                decimal[] retAcm = new decimal[12];
                decimal[] totalDdc = new decimal[12];
                decimal[] totAcmDdc = new decimal[12];

                CriteriaOperator[] operands = new CriteriaOperator[2];

                i = 5;
                operands[1] = new BinaryOperator("Status", DocumentoStatus.Sellada, BinaryOperatorType.Equal);
                for (int mesini = 1; mesini < 13; mesini++)
                {
                    DateTime mFechaIni = apl.Log.Fecha.FechaInicial(mesini, ano);
                    DateTime mFechaFin = apl.Log.Fecha.FechaFinal(mesini, ano);

                    operands[0] = GroupOperator.And(
                        new BinaryOperator("FechaDoc", mFechaIni, BinaryOperatorType.GreaterOrEqual),
                        new BinaryOperator("FechaDoc", mFechaFin, BinaryOperatorType.LessOrEqual));
                    operands[1] = new BinaryOperator("Status", DocumentoStatus.Sellada, BinaryOperatorType.Equal);
                
                    IList arr = ((XPObjectSpace)View.ObjectSpace).CreateCollection(typeof(DocumentoSalida), new GroupOperator(operands), null);

                    total[mesini - 1] = 0;
                    totAcm[mesini - 1] = 0;
                    reten[mesini - 1] = 0;
                    retAcm[mesini - 1] = 0;
                    ivaTras[mesini - 1] = 0;
                    ivaRet[mesini - 1] = 0;
                    ivaAcr[mesini - 1] = 0;
                    totalDdc[mesini - 1] = 0;
                    totAcmDdc[mesini - 1] = 0;

                    if (arr.Count > 0)
                    {
                        foreach (DocumentoSalida doc in arr)
                        {
                            total[mesini - 1] += doc.SubTotal;
                            reten[mesini - 1] += doc.RetenISR;
                            ivaTras[mesini - 1] += doc.Impuesto04;
                            ivaRet[mesini - 1] += doc.RetenIVA;
                        }
                    }


                    operands[0] = GroupOperator.And(
                        new BinaryOperator("FechaDoc", mFechaIni, BinaryOperatorType.GreaterOrEqual),
                        new BinaryOperator("FechaDoc", mFechaFin, BinaryOperatorType.LessOrEqual));
                    operands[1] = null;

                    arr = ((XPObjectSpace)View.ObjectSpace).CreateCollection(typeof(Recepcion), new GroupOperator(operands), null);

                    if (arr.Count > 0)
                    {
                        foreach (Recepcion doc in arr)
                        {
                            totalDdc[mesini - 1] += doc.SubTotal;
                            ivaAcr[mesini - 1] += doc.Impuesto04;
                        }
                    }


                    if ((mesini - 1) > 0)
                    {
                        //     1, 2, ...,11          0, 1, ...,10        1, 2, ...,11 
                        totAcm[mesini - 1] += totAcm[mesini - 2] + total[mesini - 1];
                        retAcm[mesini - 1] += retAcm[mesini - 2] + reten[mesini - 1];
                        totAcmDdc[mesini - 1] += totAcmDdc[mesini - 2] + totalDdc[mesini - 1];
                    }
                    else
                    {
                        //     0                   0
                        totAcm[mesini - 1] = total[mesini - 1];
                        retAcm[mesini - 1] = reten[mesini - 1];
                        totAcmDdc[mesini - 1] = totalDdc[mesini - 1];                                        
                    }



                    if (total[mesini - 1] != 0)
                    {
                        sheet.Cells[i, 2].SetValue(total[mesini - 1]);
                        sheet.Cells[i, 2].NumberFormat = "$#,##0.00;[Red]$#,##0.00";
                    }
                    if (totalDdc[mesini - 1] != 0)
                    {
                        sheet.Cells[i, 3].SetValue(totalDdc[mesini - 1]);
                        sheet.Cells[i, 3].NumberFormat = "$#,##0.00;[Red]$#,##0.00";
                    }

                    if (reten[mesini - 1] != 0)
                    {
                        sheet.Cells[i, 5].SetValue(reten[mesini - 1]);
                        sheet.Cells[i, 5].NumberFormat = "$#,##0.00;[Red]$#,##0.00";
                    }

                    if (ivaTras[mesini - 1] != 0)
                    {
                        sheet.Cells[i, 7].SetValue(ivaTras[mesini - 1]);
                        sheet.Cells[i, 7].NumberFormat = "$#,##0.00;[Red]$#,##0.00";
                    }
                    if (ivaAcr[mesini - 1] != 0)
                    {
                        sheet.Cells[i, 8].SetValue(ivaAcr[mesini - 1]);
                        sheet.Cells[i, 8].NumberFormat = "$#,##0.00;[Red]$#,##0.00";
                    }
                    if (ivaRet[mesini - 1] != 0)
                    {
                        sheet.Cells[i, 9].SetValue(ivaRet[mesini - 1]);
                        sheet.Cells[i, 9].NumberFormat = "$#,##0.00;[Red]$#,##0.00";
                    }
                    sheet.Cells[i, 10].Formula = string.Format("=h{0}-i{0}-j{0}", i+1);
                    sheet.Cells[i++, 10].NumberFormat = "$#,##0.00;[Red]$#,##0.00";

                    if (mesini > 1)
                    {
                        sheet.Rows[i].FillColor = Color.FromName("BurlyWood");

                        if (totAcm[mesini - 1] != 0)
                        {
                            sheet.Cells[i, 2].SetValue(totAcm[mesini - 1]);
                            sheet.Cells[i, 2].NumberFormat = "$#,##0.00;[Red]$#,##0.00";
                        }
                        if (totAcmDdc[mesini - 1] != 0)
                        {
                            sheet.Cells[i, 3].SetValue(totAcmDdc[mesini - 1]);
                            sheet.Cells[i, 3].NumberFormat = "$#,##0.00;[Red]$#,##0.00";
                        }
                        sheet.Cells[i, 4].Formula = string.Format("=c{0}-d{0}", i+1);
                        sheet.Cells[i, 4].NumberFormat = "$#,##0.00;[Red]$#,##0.00";

                        if (retAcm[mesini - 1] != 0)
                        {
                            sheet.Cells[i, 5].SetValue(retAcm[mesini - 1]);
                            sheet.Cells[i++, 5].NumberFormat = "$#,##0.00;[Red]$#,##0.00";
                        }
                        else
                            i++;
                    }
                }
                
                book.SaveDocument(string.Format("Contable.xls"));
            }
        }

        private void popupWindowShowActionGetXml_CustomizePopupWindowParams(object sender, DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            // Lo estamos usando en Recepción y Facturas !!
            CargaRecepcion newObj;

            newObj = objectSpace.FindObject<CargaRecepcion>(null);

            if (newObj == null)
            {
                newObj = objectSpace.CreateObject<CargaRecepcion>();
            }
            newObj.Rt = Prms.RutaPdfVnts;

            e.View = Application.CreateDetailView(objectSpace, "CargaRecepcion_DetailView", true, newObj);
            e.View.Caption = "Cargar Facturas";
        }

        private void popupWindowShowActionGetXml_Execute(object sender, DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventArgs e)
        {
            CargaRecepcion obj = e.PopupWindowViewCurrentObject as CargaRecepcion;

            CrgDeXml(obj);
        }

        private VentaCfdi mPrms;
        private VentaCfdi Prms
        {
            get
            {
                if (mPrms == null)
                {
                    if (View != null && View.ObjectSpace != null)
                        mPrms = View.ObjectSpace.FindObject<VentaCfdi>(null);
                }
                return mPrms;
            }
        }

        private void popupWindowShowActionDscrgMsv_CustomizePopupWindowParams(object sender, DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            DescargaMasiva newObj;
            Empresa mEmpresa = View.ObjectSpace.FindObject<Empresa>(null);

            newObj = objectSpace.FindObject<DescargaMasiva>(null);
            if (newObj == null)
                newObj = objectSpace.CreateObject<DescargaMasiva>();

            newObj.RfcEmsr = mEmpresa.Compania.Rfc;
            newObj.RfcRcptr = mEmpresa.Compania.Rfc;
            newObj.RtDscrg = Prms.RutaPdfVnts;

            e.View = Application.CreateDetailView(objectSpace, "DescargaMasiva_DetailView", true, newObj);
        }

        private void popupWindowShowActionDscrgMsv_Execute(object sender, DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventArgs e)
        {
            DescargaMasiva obj = e.PopupWindowViewCurrentObject as DescargaMasiva;

            if (obj != null)
            {
                int tntos = 0, mxTntos = 10;
                string startPath = Path.Combine(obj.RtDscrg, "Paquetes");
                if (!Directory.Exists(startPath))
                    Directory.CreateDirectory(startPath);

                string fi = obj.FchIncl.ToString("yyyy-MM-dd");
                string ff = obj.FcnFnl.ToString("yyyy-MM-dd");
                do
                {
                    if (DescargaMasivaSAT.Prueba(obj.ArchvPfx.FullName, obj.Cntrs,
                        obj.RfcEmsr, obj.RfcRcptr, fi, ff, obj.Slctd, obj.Emtds, obj.Rcbds, startPath, obj.Mtdt))
                    {
                        string zipPath = Path.Combine(startPath, DescargaMasivaSAT.idPaquete + ".gzip");
                        string extractPath = Path.Combine(obj.RtDscrg, "Extract");

                        if (File.Exists(zipPath))
                            ZipFile.ExtractToDirectory(zipPath, extractPath);
                    }
                    obj.EstdSlctd = (EEstadoSolicitud)DescargaMasivaSAT.estadoSolicitud;

                    if (obj.EstdSlctd == EEstadoSolicitud.EnProceso
                        || obj.EstdSlctd == EEstadoSolicitud.Aceptada)
                    {
                        obj.Slctd = DescargaMasivaSAT.idSolicitud;
                        System.Threading.Thread.Sleep(5000);
                    }

                } while ((obj.EstdSlctd == EEstadoSolicitud.Aceptada
                || obj.EstdSlctd == EEstadoSolicitud.EnProceso) && tntos++ < mxTntos);

                if (obj.EstdSlctd == EEstadoSolicitud.Rechazada
                    || obj.EstdSlctd == EEstadoSolicitud.Vencida
                    || obj.EstdSlctd == EEstadoSolicitud.Terminada)
                    obj.Slctd = string.Empty;

                View.ObjectSpace.CommitChanges();


                if (!obj.SlDscrgr)
                {
                    CargaRecepcion crgRc = View.ObjectSpace.FindObject<CargaRecepcion>(null);

                    if (crgRc == null)
                        crgRc = View.ObjectSpace.CreateObject<CargaRecepcion>();
                    crgRc.Rt = Prms.RutaPdfVnts;
                    crgRc.Mtdt = obj.Mtdt;

                    CrgDeXml(crgRc);
                    View.RefreshDataSource();
                }
            }
        }

        private void CrgDeXml(CargaRecepcion obj)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();

            Empresa emprs = objectSpace.FindObject<Empresa>(null);
            Ventas vts = objectSpace.FindObject<Ventas>(null);

            string aux = Path.Combine(obj.Rt, "Extract");
            string[] dirs = Directory.GetFiles(aux, obj.Mtdt ? "*.txt" : "*.xml");

            foreach (string dir in dirs)
            {
                if (obj.Mtdt)
                    NegocioAdmin.CargaMetaData(obj, dir, objectSpace, "factura");
                else
                    NegocioAdmin.CargaDeArchivo(dir, emprs, vts, obj, objectSpace);
            }
        }
    }
}
