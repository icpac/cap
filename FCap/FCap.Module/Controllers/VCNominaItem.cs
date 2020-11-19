using System;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using Nomina.Utilerias;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using Cap.Generales.BusinessObjects.Empresa;
using DevExpress.XtraReports.UI;
using Cap.Generales.BusinessObjects.General;
using Cap.Generales.Utilerias;
using System.IO;
using System.Net.Mime;
using FCap.Module.BusinessObjects.Ventas;
using Cap.Nomina.BusinessObjects;
using DevExpress.ExpressApp.ReportsV2;
using System.Collections.Generic;
using Cap.Fe.BusinessObjects;
using Cap.Ventas.Utilerias;

namespace FCap.Module.Controllers.NominaF
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class VCNominaItem : ViewController
    {
        public VCNominaItem()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(NominaItem);

            simpleActionSvSllr.TargetObjectType = typeof(NominaItem);
            simpleActionSvSllr.TargetObjectsCriteria = "Status == 'Alta'";
            simpleActionSvSllr.ImageName = "Save_and_Close";
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            View.ObjectSpace.Committing += ObjectSpace_Committing;


            simpleActionSvSllr.Active.SetItemValue("Visible", 
                NegocioVentas.Licencia(View.ObjectSpace));

            simpleActionCnclar.TargetObjectType = typeof(NominaItem);
            simpleActionCnclar.TargetObjectsCriteria = "Status != 'Cancelado'";

            simpleActionMail.TargetObjectType = typeof(NominaItem);
            simpleActionMail.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            simpleActionMail.TargetObjectsCriteria = "Status == 'Sellada'";

            simpleActionPrntRcb.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            View.ObjectSpace.Committing -= ObjectSpace_Committing;
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        bool Save = false;
        void ObjectSpace_Committing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (View != null)
            {
                NominaItem obj = View.CurrentObject as NominaItem;

                if (obj != null)
                {
                    if (View.ObjectSpace.IsNewObject(obj))
                    {
                        ;
                        // Jun 2014 Ya no es necesario
                        // NegocioNom.GrabaRec(obj);
                    }
                    else
                    {
                        e.Cancel = !Save && (obj.Status == NominaItemStatus.Cancelado ||
                            (obj.Status == NominaItemStatus.Sellada));
                        Save = false;
                    }
                }
            }
        }

        private void VCNominaItem_ViewControlsCreated(object sender, EventArgs e)
        {
            if (View != null)
            {
                NominaItem doc = View.CurrentObject as NominaItem;

                /* Jun 2014 Ya no es necesario
                if (View.ObjectSpace != null && View.ObjectSpace.IsNewObject(doc))
                    NegocioNom.IniciaRec(doc);*/

                if (doc != null)
                {
                    doc.Percepciones.ListChanged += Percepciones_ListChanged;
                    doc.Deducciones.ListChanged += Deducciones_ListChanged;
                }
            }
        }

        void Deducciones_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            if (View != null && View.ObjectSpace != null && !View.ObjectSpace.IsCommitting)
            {
                NominaItem fac = View.CurrentObject as NominaItem;

                Nomina.Utilerias.NegocioNom.CalculaRecibo(fac);
            }
        }

        void Percepciones_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            if (View != null && View.ObjectSpace != null && !View.ObjectSpace.IsCommitting)
            {
                NominaItem fac = View.CurrentObject as NominaItem;

                NegocioNom.CalculaRecibo(fac);
            }
        }

        bool InCommited = false;
        private void Sella()
        {
            if (!InCommited && View != null)
            {
                NominaItem doc = View.CurrentObject as NominaItem;

                if (doc != null)
                {
                    if (string.IsNullOrEmpty(doc.SelloSat))
                    {
                        NegocioNom.Sella(doc, View.ObjectSpace);
                        doc.Status = NominaItemStatus.Sellada;

                        /* TIT Jun 2019
                        Save = true;
                        InCommited = true;
                        View.ObjectSpace.CommitChanges();
                        InCommited = false;*/
                    }

                    /*TIT Feb 2018
                    CreaPdfImprime(true, true);
                    if (Param.SendM && doc.EnvioC != EnvioCorreo.Enviado)
                    {
                        simpleActionMail_Execute(this, null);
                        InCommited = true;
                        View.ObjectSpace.CommitChanges();
                        InCommited = false;
                    }*/
                }
            }
        }

        private void CreaPdfImprime(bool imprime, bool pdf)
        {
            if (View != null)
            {
                NominaItem doc = View.CurrentObject as NominaItem;

                if (doc != null)
                {
                    // Parece que se hace en ImprFto, Jun 2014
                    // doc.Empresa = View.ObjectSpace.FindObject<Empresa>(CriteriaOperator.Parse("Clave = 'icpac'"));

                    string auxrep = "Recibo";
                    ImprFto(auxrep, imprime, pdf, NegocioNom.NamePdf(doc));
                }
            }
        }

        private void ImprFto(string format, bool imprime, bool pdf, string nameF)
        {
            if (View != null && View.ObjectSpace != null)
            {
                XPObjectSpace objectSpace = (XPObjectSpace)View.ObjectSpace;
                NominaItem fac = View.CurrentObject as NominaItem;
                if (fac != null)
                {
                    Session uow = fac.Session;
                    //ReportData reportData = uow.FindObject<ReportData>(new BinaryOperator("Name", format));
                    // XPCollection dosyas = new XPCollection(fac.Session, typeof(NominaItem), false);
                    //XafReport report = null;
                    NominaItem doc = fac;

                    IReportDataV2 reportData2 = objectSpace.FindObject<ReportDataV2>(
                        new BinaryOperator("DisplayName", format));

                    if (doc != null)
                    {
                        doc.Empresa = uow.FindObject<Empresa>(/*CriteriaOperator.Parse("Clave = 'icpac'")*/null);

                        if (doc.Status == NominaItemStatus.Alta)
                        {
                            if (doc.Empleado != null)
                                doc.Empleado.Reload();
                        }

                        QRCode2(doc);

                        /*
                        if (reportData != null)
                        {
                            report = reportData.LoadReport(objectSpace) as XafReport;
                            report.ObjectSpace = objectSpace;
                            dosyas.Add(doc);
                            report.DataSource = dosyas;

                            if (pdf)
                            {
                                report.ExportToPdf(nameF);
                            }
                            if (imprime)
                            {
                                try
                                {
                                    ReportPrintTool printTool = new ReportPrintTool(report);

                                    // Solo funciona para windows !!
                                    printTool.ShowPreviewDialog();
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }*/

                        if (reportData2 != null)
                        {
                            string reportContainerHandler =
                                ReportDataProvider.ReportsStorage.GetReportContainerHandle(reportData2);

                            XtraReport report2 = ReportDataProvider.ReportsStorage.LoadReport(reportData2);

                            List<NominaItem> list = new List<NominaItem>();
                            list.Add(doc);

                            report2.DataSource = list;

                            if (pdf)
                            {
                                string namF = NegocioNom.NamePdf(doc);

                                DEFile fd = new DEFile(doc.Session);
                                MemoryStream ms = new MemoryStream();

                                report2.ExportToPdf(ms);
                                ms.Seek(0, SeekOrigin.Begin);

                                fd.File = new FileData(doc.Session);
                                fd.File.LoadFromStream(Path.GetFileName(namF), ms);
                                fd.TpArchv = ETipoArchivo.Pdf;

                                doc.DElctrnc.Archivos.Add(fd);
                            }
                            if (imprime)
                            {
                                try
                                {
                                    ReportsModuleV2.FindReportsModule(Application.Modules).ReportsDataSourceHelper.SetupBeforePrint(report2);

                                    report2.ShowPreview();
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }

                        // dosyas.Dispose();
                    }
                }
            }
        }

        private void QRCode2(NominaItem fac)
        {
            /* Dic 2015 Ahora debe ser desde el diseñador de reportes
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();

            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 4;
            qrCodeEncoder.QRCodeVersion = 8;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;

            Image image;*/
            string aux = fac.Total.ToString("F6");
            String data;
            if (fac != null && fac.Empresa != null && fac.Empresa.Compania != null
                && fac.Empleado != null && fac.Empleado.Persona != null)
            {
                data = string.Format("?re={0}&rr={1}&tt={2}&id={3}", fac.Empresa.Compania.Rfc, fac.Empleado.Persona.Rfc, aux, fac.Uuid);
                data = string.Format("https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?id={0}&re={1}&rr={2}&tt={3}&fe={4}",
                                    fac.Uuid, fac.Empresa.Compania.Rfc, fac.Empleado.Persona.Rfc,
                                    fac.Total.ToString("F6"),
                                    string.IsNullOrEmpty(fac.Sello) ? string.Empty : fac.Sello.Substring(fac.Sello.Length - 8));
            }
            else
                data = string.Empty;

            /*
            image = qrCodeEncoder.Encode(data);
            fac.CBB = image;*/
            
            fac.CadenaCBB = data;
        }

        private void simpleActionSvSllr_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View != null && View.ObjectSpace != null)
            {
                View.ObjectSpace.CommitChanges();
                Sella();

                View.ObjectSpace.Committing -= ObjectSpace_Committing;
                View.ObjectSpace.CommitChanges();
                View.ObjectSpace.Committing += ObjectSpace_Committing;

                CreaPdfImprime(true, true);
                View.ObjectSpace.Committing -= ObjectSpace_Committing;
                View.ObjectSpace.CommitChanges();
                View.ObjectSpace.Committing += ObjectSpace_Committing;

                View.Close();
            }
        }

        private void simpleActionPrntRcb_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            CreaPdfImprime(true, false);
        }

        private void simpleActionCnclar_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View != null && View.ObjectSpace != null)
            {
                NominaItem fac = View.CurrentObject as NominaItem;

                if (fac != null)
                {
                    NegocioNom.CancelaDocumento(fac);

                    Save = true;
                    View.ObjectSpace.CommitChanges();

                    if (fac.EnvioC == EnvioCorreo.Enviado)
                        EnviaNotifCan(fac);

                    // Imprime acuse
                    if (Empresa != null && /*fac != null &&*/ !string.IsNullOrEmpty(fac.SelloSatCan))
                        ImprFto("AcuseCan", true, true, NegocioNom.NamePdf(fac));
                }
            }
        }

        private void EnviaNotifCan(NominaItem sal)
        {
            if (View != null && View.ObjectSpace != null && sal != null)
            {
                Correo objCorreo = View.ObjectSpace.FindObject<Correo>(CriteriaOperator.Parse("Clave = 'cancel'"));

                if (objCorreo == null)
                {
                    objCorreo = View.ObjectSpace.CreateObject<Correo>();
                    objCorreo.Clave = "cancel";
                }

                Correo objSend = View.ObjectSpace.FindObject<Correo>(CriteriaOperator.Parse("Clave = 'icpac'"));
                if (objSend != null)
                {
                    objCorreo.Cuenta = objSend.Cuenta;
                    objCorreo.Identificdr = objSend.Identificdr;
                    objCorreo.Passw = objSend.Passw;
                    objCorreo.Puerto = objSend.Puerto;
                    objCorreo.SegurdSSL = objSend.SegurdSSL;
                    objCorreo.ServidorSMTP = objSend.ServidorSMTP;
                    objCorreo.Usuario = objSend.Usuario;
                }



                objCorreo.Asunto = string.Format("El documento {0}, se ha cancelado", sal.ReciboN);
                objCorreo.Mensaje1 = "Notificación de cancelación";

                Empleado pc = sal.Empleado;
                Empresa emp = sal.Session.FindObject<Empresa>(CriteriaOperator.Parse("Clave = 'icpac'"));

                if (pc != null && pc.Persona != null && pc.Persona.Direccion != null)
                    pc.Persona.Direccion.Reload();


                if (emp != null)
                {
                    objCorreo.Mensaje1 = "Estimado, le notificamos por este medio que el CFDI (Comprobante Fiscal Digital por Internet) con las siguientes características ha sido cancelado:<br>";
                }

                objCorreo.Mensaje2 = htmlCan(emp.Compania.Rfc, emp.Compania.Nombre, sal.Empleado.Persona.Rfc,
                    sal.Empleado.Nombre, sal.ReciboN, String.Format("{0:yyyy-MM-ddTHH:mm:ss}", sal.FechaDoc),
                    string.Format("{0}({1})", "egreso", "RECIBO"),
                    string.Format("{0}, {1}", sal.Total.ToString("c2"), sal.Moneda.Clave));

                CorreoSend.MandaCorreo(objCorreo, pc.Persona.Direccion.Email, objCorreo.Asunto, objCorreo.Identificdr, null);
                objCorreo.Mensaje1 =
                objCorreo.Mensaje2 = string.Empty;
            }
        }

        private string htmlCan(string rfcemi, string emi, string rfcrec, string rec, string sf, string fch, string tp, string tt)
        {
            return string.Format("<table>" +
            /*
        "<tbody>"+
            "<tr>"+
                "<td colspan='2'>"+
                    "<span style='font-size:medium; font-family:Arial'>"+
"Estimado, le notificamos por este medio que el CFDI (Comprobante Fiscal Digital por Internet) con las siguientes características ha sido cancelado:"+
                          "</span>"+
            "</td>"+
            "</tr>"+
        "</tbody>"+*/
            "<tr>" +
                    "<td width='40%'><span style='font-size: small; font-family: Arial'>RFC Emisor:</span></td>" +
                    "<td width='60%'><span style='font-weight: bold; font-size: small; font-family: Arial'>{0}</span></td>" +
"</tr>" +
            "<tr>" +
                "<td><span style='font-size: small; font-family: Arial'>Razón Social Emisor:</span></td>" +
                "<td><span style='font-weight: bold; font-size: small; font-family: Arial'>{1}</span> </td>" +
            "</tr>" +
            "<tr>" +
                "<td><span style='font-size: small; font-family: Arial'>RFC Receptor: </span> </td>" +
                "<td><span style='font-weight: bold; font-size: small; font-family: Arial'>{2}</span> </td>" +
            "</tr>" +
            "<tr>" +
                "<td><span style='font-size: small; font-family: Arial'>Nombre Receptor: </span> </td>" +
                "<td><span style='font-weight: bold; font-size: small; font-family: Arial'>{3}</span> </td>" +
            "</tr>" +
            "<tr>" +
                "<td><span style='font-size: small; font-family: Arial'>Serie y Folio: </span> </td>" +
                "<td><span style='font-weight: bold; font-size: small; font-family: Arial'>{4}</span> </td>" +
            "</tr>" +
            "<tr>" +
                "<td><span style='font-size: small; font-family: Arial'>Fecha de Emisión: </span> </td>" +
                "<td><span style='font-weight: bold; font-size: small; font-family: Arial'>{5}</span> </td>" +
            "</tr>" +
            "<tr>" +
                "<td><span style='font-size: small; font-family: Arial'>Tipo de Comprobante: </span> </td>" +
                "<td><span style='font-weight: bold; font-size: small; font-family: Arial'>{6}</span> </td>" +
            "</tr>" +
            "<tr>" +
                "<td><span style='font-size: small; font-family: Arial'>Monto Total: </span></td>" +
                "<td><span style='font-weight: bold; font-size: small; font-family: Arial'>{7}</span> </td>" +
            "</tr>" +
        "</table>", rfcemi, emi, rfcrec, rec, sf, fch, tp, tt);
        }

        private Empresa mEmpresa;
        private Empresa Empresa
        {
            get
            {
                if (mEmpresa == null)
                {
                    if (View != null && View.ObjectSpace != null)
                        mEmpresa = View.ObjectSpace.FindObject<Empresa>(CriteriaOperator.Parse("Clave = 'icpac'"));
                }
                return mEmpresa;
            }
        }

        private Ventas mParam;
        private Ventas Param
        {
            get
            {
                if (mParam == null)
                {
                    if (View != null && View.ObjectSpace != null)
                        mParam = View.ObjectSpace.FindObject<Ventas>(CriteriaOperator.Parse("Clave = 'icpac'"));
                }
                return mParam;
            }
        }

        private void simpleActionMail_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View != null)
            {
                foreach (NominaItem doc in View.SelectedObjects)
                {
                    Correo(doc);
                }
            }
        }

        private void Correo(NominaItem fac)
        {
            if (View != null)
            {
                if (fac != null)
                {
                    Correo objCorreo = View.ObjectSpace.FindObject<Correo>(CriteriaOperator.Parse("Clave = 'icpac'"));

                    if (objCorreo != null)
                    {
                        string aux = NegocioNom.NamePdf(fac);
                        string asn = String.Format("{0}({1})", objCorreo.Asunto, Path.GetFileNameWithoutExtension(aux));

                        string sn = NegocioNom.SoloPdf(fac);
                        MemoryStream fmem = new MemoryStream();
                        MemoryStream fmem1 = new MemoryStream();

                        fac.DElctrnc.Archivos[0].File.SaveToStream(fmem);
                        sn = Path.ChangeExtension(sn, ".xml");
                        // Set the stream position to the beginning of the stream.
                        fmem.Seek(0, SeekOrigin.Begin);
                        CorreoSend.AddFile(fmem, 
                            MediaTypeNames.Application.Soap, sn);

                        fac.DElctrnc.Archivos[1].File.SaveToStream(fmem1);
                        sn = Path.ChangeExtension(sn, ".pdf");
                        // Set the stream position to the beginning of the stream.
                        fmem1.Seek(0, SeekOrigin.Begin);
                        CorreoSend.AddFile(fmem1, 
                            MediaTypeNames.Application.Pdf, sn);

                        // CorreoSend.AddFile(aux, MediaTypeNames.Application.Pdf);

                        // CorreoSend.AddFile(aux, MediaTypeNames.Application.Soap);

                        /* No lo logre asi
                        MemoryStream ms = new MemoryStream();
                        emp.Logo.Save(ms, ImageFormat.Jpeg);*/
                        string mail = string.Empty;

                        fac.Empleado.Persona.Direccion.Reload();
                        if (!string.IsNullOrEmpty(Param.VntCfdi.SendCopy))
                            mail = string.Format("{0}, {1}", fac.Empleado.Persona.Direccion.Email, Param.VntCfdi.SendCopy);
                        else
                            mail = fac.Empleado.Persona.Direccion.Email;
                        if (CorreoSend.MandaCorreo(objCorreo, mail, asn, null, null))
                        {
                            fac.EnvioC = EnvioCorreo.Enviado;
                            Save = true;

                            InCommited = true;
                            View.ObjectSpace.CommitChanges();
                            InCommited = false;
                        }
                    }
                }
            }
        }
    }
}
