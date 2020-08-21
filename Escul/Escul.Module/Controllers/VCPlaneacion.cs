using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Escul.Module.BusinessObjects;
using Escul.Module.Utilerias;
using DevExpress.Spreadsheet;
using System.IO;

namespace Escul.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class VCPlaneacion : ViewController
    {
        public VCPlaneacion()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.

            simpleActionClclFchAplccn.TargetObjectType = typeof(Planeacion);

            simpleActionHrsSmn.TargetObjectType = typeof(Planeacion);
            simpleActionHrsSmn.TargetViewType = ViewType.DetailView;

            simpleActionHrsTtls.TargetObjectType = typeof(Planeacion);
            simpleActionHrsTtls.TargetViewType = ViewType.DetailView;

            popupWindowShowActionCrgTms.TargetObjectType = typeof(Planeacion);
            popupWindowShowActionCrgTms.TargetViewType = ViewType.DetailView;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        // Hora semana
        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View != null)
            {
                Planeacion obj = View.CurrentObject as Planeacion;

                if (obj != null)
                {
                    obj.Hrs = 0;
                    CargaHoraria hr;
                    if ((hr = obj.CrgHrr) != null)
                    {
                        hr.TtlLns = Negocio.HorasPorDia(hr.Lns);
                        hr.TtlMrts = Negocio.HorasPorDia(hr.Mrts);
                        hr.TtlMrcls = Negocio.HorasPorDia(hr.Mrcls);
                        hr.TtlJvs = Negocio.HorasPorDia(hr.Jvs);
                        hr.TtlVrns = Negocio.HorasPorDia(hr.Vrns);
                        hr.TtlSbd = Negocio.HorasPorDia(hr.Sbd);

                        obj.Hrs += hr.TtlLns + hr.TtlMrts + hr.TtlMrcls
                            + hr.TtlJvs + hr.TtlVrns + hr.TtlSbd;
                    }
                }
            }
        }

        private void simpleActionHrsTtls_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View != null)
            {
                Planeacion obj = View.CurrentObject as Planeacion;

                obj.HrsClndr = 0;
                if (obj != null && obj.Clndr != null && obj.CrgHrr != null)
                {
                    DateTime dia = obj.Clndr.FchIncl;
                    CargaHoraria hrr = obj.CrgHrr; //.Horarios[0];
                    bool excp;

                    while (dia <= obj.Clndr.FchFnl)
                    {
                        excp = false;
                        foreach (CalendarioEvento evnt in obj.Clndr.Eventos)
                            if (!excp)
                                excp = dia == evnt.Fch;

                        if (!excp)
                        {
                            if (dia.DayOfWeek == DayOfWeek.Monday)
                            {
                                // Por el momento supondré que sólo hay un horario
                                if (hrr.TtlLns > 0)
                                    obj.HrsClndr += hrr.TtlLns;
                            }
                            else if (dia.DayOfWeek == DayOfWeek.Tuesday)
                            {
                                if (hrr.TtlMrts > 0)
                                    obj.HrsClndr += hrr.TtlMrts;
                            }
                            else if (dia.DayOfWeek == DayOfWeek.Wednesday)
                            {
                                if (hrr.TtlMrcls > 0)
                                    obj.HrsClndr += hrr.TtlMrcls;
                            }
                            else if (dia.DayOfWeek == DayOfWeek.Thursday)
                            {
                                if (hrr.TtlJvs > 0)
                                    obj.HrsClndr += hrr.TtlJvs;
                            }
                            else if (dia.DayOfWeek == DayOfWeek.Friday)
                            {
                                if (hrr.TtlVrns > 0)
                                    obj.HrsClndr += hrr.TtlVrns;
                            }
                            else if (dia.DayOfWeek == DayOfWeek.Saturday)
                            {
                                if (hrr.TtlSbd > 0)
                                    obj.HrsClndr += hrr.TtlSbd;
                            }
                        }
                        dia = dia.AddDays(1);
                    }
                }
            }
        }

        private void simpleActionClclFchAplccn_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View != null)
            {
                Planeacion obj = View.CurrentObject as Planeacion;

                if (obj != null && obj.Temas != null && obj.Temas.Count > 0
                    && obj.Clndr != null && obj.CrgHrr != null)
                {
                    SortingCollection sortProps = new SortingCollection();
                    float hrsDspnbls = 0;
                    float hrsNecesarias;
                    float hrsCls = 0;
                    DateTime diaIni = obj.Clndr.FchIncl;


                    sortProps.Add(new SortProperty("Nmr", SortingDirection.Ascending));
                    obj.Temas.Sorting = sortProps;


                    hrsDspnbls = Negocio.HrsDisponiblesPlaneacion(ref diaIni,
                        obj.CrgHrr, obj.Clndr.FchFnl, obj.Clndr);
                    hrsCls += hrsDspnbls;

                    if (hrsDspnbls > 0)
                    {
                        foreach (PlaneacionTema tm in obj.Temas)
                        {
                            hrsNecesarias = tm.Drcn;
                            tm.FchPrgrmd = diaIni;

                            if (tm.Children == null
                                || tm.Children.Count == 0)
                            {
                                tm.HrsClsAcmlds = hrsCls;
                                // Este día es suficiente
                                if (hrsNecesarias == hrsDspnbls)
                                {
                                    diaIni = diaIni.AddDays(1);
                                    hrsDspnbls = Negocio.HrsDisponiblesPlaneacion(ref diaIni,
                                        obj.CrgHrr, obj.Clndr.FchFnl,
                                        obj.Clndr);
                                    hrsCls += hrsDspnbls;
                                }
                                // Necesito otro día
                                else if (hrsNecesarias > hrsDspnbls)
                                {
                                    while (hrsNecesarias > hrsDspnbls && hrsDspnbls > 0)
                                    {
                                        hrsNecesarias -= hrsDspnbls;

                                        diaIni = diaIni.AddDays(1);
                                        hrsDspnbls = Negocio.HrsDisponiblesPlaneacion(ref diaIni,
                                            obj.CrgHrr, obj.Clndr.FchFnl, obj.Clndr);
                                        
                                        hrsCls += hrsDspnbls;
                                    }
                                    hrsDspnbls -= hrsNecesarias;
                                }
                                // Este día sobran horas
                                else
                                {
                                    hrsDspnbls -= hrsNecesarias;
                                }

                                if (hrsDspnbls <= 0)
                                {
                                    diaIni = diaIni.AddDays(1);
                                    hrsDspnbls = Negocio.HrsDisponiblesPlaneacion(ref diaIni,
                                        obj.CrgHrr, obj.Clndr.FchFnl, obj.Clndr);

                                    hrsCls += hrsDspnbls;
                                    if (diaIni > obj.Clndr.FchFnl)
                                        break;
                                }
                            }
                        }
                    }
                    TestFechaApl(obj);
                    View.ObjectSpace.CommitChanges();
                }
            }
        }

        // Suponemos un calendario del 1 de junio al 31 de julio
        // con dias feriados 3, 11, 19, 27, 5
        // Horas totales 6 + 5 + 6 + 5 + 6 + 7 + 7 + 7 + 4 = 4 + 10 + 18 + 21 
        // = 14 + 39 = 53

        // 0+2+1+2+1 = 6
        // 1+0+1+2+1 = 5
        // 1+2+0+2+1 = 6
        // 1+2+1+0+1 = 5

        // 1+2+1+2+0 = 6
        // 1+2+1+2+1 = 7
        // 1+2+1+2+1 = 7
        // 1+2+1+2+1 = 7 
        // 1+2+1     = 4 

        // Suponemos un horarios de lun 9-10, mar 10-12, mier 11-12, jue 12-2, vier 1-2
        //              L M M J V
        // Horas semana 1+2+1+2+1 = 7
        //
        // Con estos temas por cubrir 
        // Tema 1
        //    Subtema1   1  4 jun mar sobra 1
        //    Subtema2   2  4 jun mar toma 1
        //    Subtema3   3  6 jun  jue
        //    Subtema4   4  10 jun lun
        //    Subtema5   2  14 jun vie
        //    Subtema6   3  18 jun mar
        //    Subtema7   1  20 jun jue  
        //    Subtema8   3  21 jun vie
        // Examen        2  25 jun mar toma 1  
        // 21
        // Tema 2
        //    Subtema1   3  28 jun vie 
        //    Subtema2   2   2 jul mar toma 1 
        //    Subtema3   1   4 jul jue 
        //    Subtema4   4   4 jul jue toma 1
        //    Subtema5   3  10 jul mie 
        //    Subtema6   1  12 jul vie 
        //    Subtema7   2  15 jul lun 
        //    Subtema8   2  16 jul mar toma 1 
        // Examen        2  18 jul jue
        // 20
        // Tema 3
        //    Subtema1   3  19 jul vie
        //    Subtema2   4  23 jul mar toma 1
        //    Subtema3   2  26 jul vie 
        //    Subtema4   3  30 jul mar
        // 12
        // Horas necesarias 53

        private void TestFechaApl(Planeacion pln)
        {
        }

        private void popupWindowShowActionCrgTms_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            ArchivoCarga newObj;

            newObj = objectSpace.FindObject<ArchivoCarga>(null);
            if (newObj != null)
                objectSpace.Delete(newObj);

            newObj = objectSpace.CreateObject<ArchivoCarga>();
            e.View = Application.CreateDetailView(objectSpace, "ArchivoCarga_DetailView", true, newObj);
        }

        private void popupWindowShowActionCrgTms_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            ArchivoCarga obj = e.PopupWindowViewCurrentObject as ArchivoCarga;

            if (obj != null && obj.File != null
                && !string.IsNullOrEmpty(obj.File.FileName))
            {
                using (var stream = new MemoryStream())
                {
                    Workbook wb = new Workbook();

                    obj.File.SaveToStream(stream);
                    stream.Position = 0;
                    wb.LoadDocument(stream, DocumentFormat.Xlsx);

                    // errorEn = "No se pudo procesar el archivo. Posiblemente esté dañado.";
                    if (wb != null && wb.Worksheets.Count > 0)
                    {
                        Worksheet fp = wb.Worksheets[0];

                        Planeacion pln = View.CurrentObject as Planeacion;
                        cargaTemas(fp, 1, obj.CldDrcn, pln);
                    }
                }
            }
        }

        private void cargaTemas(Worksheet ws, int renIni, 
            string cldDrcn, Planeacion pln)
        {
            int ultimo = 5;

            CellRange rango = ws.GetUsedRange();
            if (rango.ColumnCount >= 2)
            {
                int i, clvsec = 0, colsec, colant = -1;
                double drcn = 0;
                double drcnTm = 0;
                string txt = string.Empty;                
                IObjectSpace objectSpace = View.ObjectSpace;
                List<PlaneacionTema> papas = new List<PlaneacionTema>();
                PlaneacionTema posiblePapa = null, papa = null;
                
                int idDrcn = Negocio.IndiceDeColumna(ws, cldDrcn);

                for (i = renIni - 1; i < rango.RowCount; i++)
                {
                    colsec = -1;
                    do
                    {
                        colsec++;
                        txt = Negocio.CeldaTexto(ws, i, colsec);
                    } while (colsec < ultimo && string.IsNullOrEmpty(txt));

                    drcn = Negocio.CeldaNumero(ws, i, idDrcn);

                    if (colsec > colant)
                    {
                        papa = posiblePapa;
                        papas.Add(posiblePapa);
                    }
                    else if (colsec < colant)
                    {
                        while (colsec+1 < papas.Count && papas.Count > 1)
                            papas.RemoveAt(papas.Count - 1);
                        papa = papas[papas.Count - 1];
                    }
                    colant = colsec;

                    if (!string.IsNullOrEmpty(txt))
                    {
                        PlaneacionTema pg = objectSpace.CreateObject<PlaneacionTema>();

                        pg.Nmr = Convert.ToString(++clvsec).PadLeft(5, '0');
                        pg.Dscrpcn = txt;
                        if (drcn > 0)
                        {
                            drcnTm += drcn;
                            pg.Drcn = Convert.ToSingle(drcn);
                            pg.HrsTmAcmlds = Convert.ToSingle(drcnTm);
                        }

                        pg.TemaPadre = papa;
                        posiblePapa = pg;
                        pln.Temas.Add(pg);
                    }
                }
                objectSpace.CommitChanges();
            }
        }
    }
}
