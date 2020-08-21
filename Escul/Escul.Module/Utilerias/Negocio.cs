using Cap.Generales.BusinessObjects.General;
using Cap.Generales.Utilerias;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
//using DevExpress.Persistent.Base.ReportsV2;
using DevExpress.Xpo;
using Escul.Module.BusinessObjects;
using Escul.Module.BusinessObjects.Admin;
using System;
using System.Collections;
using System.Collections.Generic;
using DevExpress.Data.Extensions;
using DevExpress.Xpo.DB;

namespace Escul.Module.Utilerias
{
    public class Negocio : NegocioBase
    {
        public static void IniciaAlmn(Alumno obj)
        {
            if (obj != null)
            {
                Parametros prm = obj.Session.FindObject<Parametros>(null);

                if (prm != null)
                {
                    prm.Reload();
                    // 203213858, una regla de negocio para la clave o matricula
                    if (!string.IsNullOrEmpty(prm.UltmMtrcl) && apl.Log.Cadena.IsNumber(prm.UltmMtrcl))
                        obj.Clave = prm.UltmMtrcl;
                }
            }
        }

        public static void IniciaDcmntSld(DocumentoSalida obj)
        {
            if (obj != null)
            {
                Parametros prm = obj.Session.FindObject<Parametros>(null);

                if (prm != null)
                {
                    prm.Reload();
                    // 203213858, una regla de negocio para la clave o matricula
                    if (prm.UltmFl > 0)
                        obj.Fl = prm.UltmFl.ToString();
                }
            }
        }

        public static void GrabaCxc(Cxc obj, Parametros prm, IObjectSpace osp)
        {
            if (obj != null && obj.IsNewObject())
            {
                if (obj.Cliente != null)
                {
                    obj.Cliente.Saldo += obj.Cepto.Tipo == EConceptoTipo.Cargo ? obj.Monto : -obj.Monto;
                }

                if (prm == null)
                    prm = obj.Session.FindObject<Parametros>(null);

                if (prm != null)
                {
                    if (obj.Cepto.Tipo == EConceptoTipo.Abono)
                    {
                        if (obj.Dclrbl)
                        {
                            obj.Folio = prm.UltmPg.ToString();
                            prm.UltmPg++;
                        }
                        else
                        {
                            obj.Folio = prm.UltmPgNDclrbl.ToString();
                            prm.UltmPgNDclrbl++;
                        }
                    }
                }

                obj.Saldo += obj.Monto;
                if (obj.Cepto.Tipo == EConceptoTipo.Abono)
                {
                    decimal aux = obj.Monto;
                    decimal totmont = 0;

                    int pos = 0;
                    while (pos < obj.Crgs.Count)
                    {
                        if (((CxcItem)(obj.Crgs[pos])).Monto <= 0)
                        {
                            CxcItem it = ((CxcItem)(obj.Crgs[pos]));
                            osp.Delete(it);
                            osp.RemoveFromModifiedObjects(it);
                            // obj.Crgs.Remove(obj.Crgs[pos]);
                            // pos++;
                        }
                        else
                            pos++;
                    }
                    /*
                    pos = 0;
                    while (pos < obj.Crgs.Count)
                    {
                        if (((CxcItem)(obj.Crgs[pos])).Monto <= 0)
                        {
                            obj.Crgs.Remove(obj.Crgs[pos]);
                        }
                        else
                            pos++;
                    }*/



                    foreach (CxcItem crgs in obj.Crgs)
                    {
                        totmont += crgs.Monto;
                    }
                    if (totmont != aux)
                        throw new Exception("El Total NO coincide con la suma de los Parciales !");

                    foreach (CxcItem crgs in obj.Crgs)
                    {
                        if (crgs.Monto > crgs.Crg.Saldo && (crgs.Monto - crgs.Crg.Saldo) > 0.009m)
                            throw new Exception("El PAGO  NO  puede ser mayor a lo que DEBE !");
                    }

                    foreach (CxcItem crgs in obj.Crgs)
                    {
                        /*
                        if (aux > 0)
                        {*/
                        /*
                            if (crgs.Monto / * aux* / >= crgs.Crg.Saldo) // crgs.Saldo)
                            {
                                aux -= crgs.Crg.Saldo; // crgs.Saldo;
                                crgs.Crg.Saldo = 0; // crgs.Saldo = 0;
                            }
                            else
                            {
                                crgs.Crg.Saldo -= crgs.Monto; // aux; // crgs.Saldo -= aux;
                                aux = 0;
                            }*/
                        crgs.Crg.Saldo -= crgs.Monto;
                        aux -= crgs.Monto;

                        //---------------------------------------------------------------------
                        // Balance
                        GroupOperator fil2 = new GroupOperator();

                        string axCncpt = crgs.Crg.CncptSrvc; //.Cncpt;

                        if (string.IsNullOrEmpty(axCncpt))
                        {
                            axCncpt = crgs.Crg.Cncpt.Trim();
                            if (axCncpt.Contains("COLEGIATURA"))
                                axCncpt = "COLEGIATURA";
                            else if (axCncpt.Contains("UNIFORMES"))
                                axCncpt = "UNIFORMES";
                            else if (axCncpt.Contains("TRANSPORTE"))
                                axCncpt = "TRANSPORTE";
                            else if (axCncpt.Contains("LUNCHS Y AGUA"))
                                axCncpt = "LUNCHS Y AGUA";
                        }

                        fil2.Operands.Add(new BinaryOperator("Cncpt", axCncpt));
                        fil2.Operands.Add(new BinaryOperator("Dclrbl", obj.Dclrbl));
                        fil2.Operands.Add(new BinaryOperator("FrmPg", obj.FrmPg));
                        fil2.Operands.Add(new BinaryOperator("Fch", new DateTime(obj.FchApl.Year, obj.FchApl.Month, obj.FchApl.Day)));
                        fil2.Operands.Add(new BinaryOperator("Tp", EBalanceTipo.Cobro));
                        Balance bl = obj.Session.FindObject<Balance>(fil2);

                        if (bl == null)
                        {
                            bl = new Balance(obj.Session);

                            // Estos pueden ser diferentes
                            bl.Dclrbl = obj.Dclrbl;
                            if (obj.FrmPg != null)
                                bl.FrmPg = obj.Session.FindObject<Catalogo>(new BinaryOperator("Oid", obj.FrmPg.Oid));
                            bl.Fch = new DateTime(obj.FchApl.Year, obj.FchApl.Month, obj.FchApl.Day); // obj.FchApl;

                            bl.Mnt = crgs.Monto;
                            bl.Cncpt = axCncpt;
                            bl.Tp = EBalanceTipo.Cobro;
                        }
                        else
                        {
                            bl.Mnt += crgs.Monto;
                        }

                    }
                    obj.Saldo = aux;
                }
            }
        }

        public static void InscribeAlmn(Alumno obj, IObjectSpace spc)
        {
            obj.StatusAcdmc = SttsAcdmc.Inscrito;

            /* Ahora lanzamos la forma para un alumno
            DocumentoSalida doc = spc.CreateObject<DocumentoSalida>();
            ItemSalida it = spc.CreateObject<ItemSalida>();

            it.Srvc = spc.FindObject<Servicio>(new BinaryOperator("Dscrpcn", "Inscripción"));
            doc.Almn = obj;
            doc.ItemsSalida.Add(it);

            GrabaDocSal(doc, spc);*/
        }

        /// <summary>
        /// Graba el documento, crea la cxc y actualiza parametros
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="spc"></param>
        public static void GrabaDocSal(DocumentoSalida obj, IObjectSpace spc, bool dVcs)
        {
            /* Ahora se calcula en el objeto ! 
            obj.Ttl = 0;
            foreach (ItemSalida it in obj.ItemsSalida)
                obj.Ttl += it.Prc;*/
            Parametros prm = spc.FindObject<Parametros>(null);
            if (prm != null)
            {
                prm.Reload();
                if (prm.UltmFl > 0 && apl.Log.Cadena.IsNumber(obj.Fl))
                    prm.UltmFl = Convert.ToUInt32(obj.Fl) + 1;
            }

            CreaCxc(obj, spc, prm.CncptCbr, prm, dVcs);
        }

        private static void CreaCxc(DocumentoSalida doc, IObjectSpace spc, ConceptoCxcxp cpt, Parametros prm, bool dVcs)
        {

            /* TI Folio sólo cuando es un pago !
            IniciaCxc(cnt, prm);*/

            foreach (ItemSalida itm in doc.ItemsSalida)
            {
                Cxc cnt = spc.CreateObject<Cxc>();

                cnt.Cepto = spc.FindObject<ConceptoCxcxp>(new BinaryOperator("Descripcion", cpt.Descripcion));
                cnt.Cliente = doc.Almn;
                cnt.Monto = Convert.ToDecimal(itm.Imprt);
                cnt.DcmntSld = doc;
                cnt.FchApl = doc.FchAplccn; // doc.FchAlt;

                if (prm.SrvcBc != itm.Srvc)
                    cnt.Cncpt = string.Format("{0} {1}", itm.Srvc.Dscrpcn, itm.Obsrvcns);
                else
                {
                    cnt.Cncpt = string.Format("{0} {1} {2}", itm.Srvc.Dscrpcn, itm.Obsrvcns, cnt.FchApl.ToString("MMM").ToUpper());
                    if (dVcs)
                        doc.Almn.Clgtr = cnt.Monto;
                }
                cnt.CncptSrvc = itm.Srvc.Dscrpcn;

                GrabaCxc(cnt, prm, spc);
            }

            /*
            if (cnt.Cepto.Tipo == EConceptoTipo.Cargo)
                doc.Almn.Saldo += cnt.Monto;*/
        }

        public static void IniciaCxc(Cxc obj, Parametros prm)
        {
            if (obj != null)
            {
                if (prm == null)
                {
                    prm = obj.Session.FindObject<Parametros>(null);

                    if (prm != null)
                        prm.Reload();
                }

                if (prm != null)
                {
                    if (prm.UltmPg > 0)
                        obj.Folio = prm.UltmPg.ToString();
                }
            }
        }

        public static void GrabaEvento(Evento evnt, IObjectSpace spc)
        {
            if (evnt != null)
            {
                foreach (Alumno alm in evnt.Almns)
                {
                    DocumentoSalida doc = spc.CreateObject<DocumentoSalida>();

                    doc.Almn = alm;
                    doc.FchAlt = evnt.Fch;
                    doc.FchAplccn = evnt.FchAplccn;

                    ItemSalida its = spc.CreateObject<ItemSalida>();
                    its.DcmntSld = doc;
                    its.Srvc = evnt.Srvc;
                    its.Prc = evnt.Prc;
                    its.Obsrvcns = evnt.Obsrvcns;

                    doc.ItemsSalida.Add(its);

                    bool dVcs = false;
                    GrabaDocSal(doc, spc, dVcs);
                }
            }
            //spc.CommitChanges();
        }

        public static void CancelaDoc(DocumentoSalida doc, IObjectSpace os, bool commit)
        {
            doc.Stts = EEstadoDcmnt.Cancelado;

            IList<Cxc> cxcs = os.GetObjects<Cxc>(new BinaryOperator("DcmntSld", doc));

            foreach (Cxc c in cxcs)
            {
                if (c.Monto == c.Saldo)
                {
                    c.Stts = EEstadoDcmnt.Cancelado;
                    c.Cliente.Saldo -= c.Monto;
                }
            }
            if (commit)
                os.CommitChanges();
        }

        public static void CancelaCxc(IList cxcs, IObjectSpace os, bool commit)
        {
            foreach (Cxc c in cxcs)
            {
                if (c.Stts == EEstadoDcmnt.Alta)
                {
                    if (c.Cepto.Tipo == EConceptoTipo.Cargo && c.Monto == c.Saldo)
                    {
                        c.Cliente.Saldo -= c.Monto;
                        c.Stts = EEstadoDcmnt.Cancelado;
                    }
                    else if (c.Cepto.Tipo == EConceptoTipo.Abono)
                    {
                        foreach (CxcItem ci in c.Crgs)
                        {
                            ci.Crg.Saldo += ci.Monto;



                            //---------------------------------------------------------------------
                            // Balance
                            GroupOperator fil2 = new GroupOperator();

                            string axCncpt = ci.Crg.CncptSrvc;

                            if (string.IsNullOrEmpty(axCncpt))
                            {
                                axCncpt = ci.Crg.Cncpt.Trim();
                                if (axCncpt.Contains("COLEGIATURA"))
                                    axCncpt = "COLEGIATURA";
                                else if (axCncpt.Contains("UNIFORMES"))
                                    axCncpt = "UNIFORMES";
                                else if (axCncpt.Contains("TRANSPORTE"))
                                    axCncpt = "TRANSPORTE";
                                else if (axCncpt.Contains("LUNCHS Y AGUA"))
                                    axCncpt = "LUNCHS Y AGUA";
                            }

                            fil2.Operands.Add(new BinaryOperator("Cncpt", axCncpt));
                            fil2.Operands.Add(new BinaryOperator("Dclrbl", c.Dclrbl));
                            if (c.FrmPg != null)
                                fil2.Operands.Add(new BinaryOperator("FrmPg", c.FrmPg));
                            fil2.Operands.Add(new BinaryOperator("Fch", new DateTime(c.FchApl.Year, c.FchApl.Month, c.FchApl.Day)));
                            fil2.Operands.Add(new BinaryOperator("Tp", EBalanceTipo.Cobro));
                            Balance bl = c.Session.FindObject<Balance>(fil2);

                            if (bl != null)
                            {
                                bl.Mnt -= ci.Monto;
                            }


                        }
                        c.Cliente.Saldo += c.Monto;
                        c.Stts = EEstadoDcmnt.Cancelado;
                    }
                }
            }
            if (commit)
                os.CommitChanges();
        }

        public static void GrabaGasto(Gasto gst, IObjectSpace os)
        {
            if (os.IsNewObject(gst))
            {
                GroupOperator fil3 = new GroupOperator();

                fil3.Operands.Add(new BinaryOperator("Cncpt", gst.Cncpt.Dscrpcn));
                fil3.Operands.Add(new BinaryOperator("Dclrbl", gst.Dclrbl));
                fil3.Operands.Add(new BinaryOperator("FrmPg", gst.FrmPg));
                fil3.Operands.Add(new BinaryOperator("Fch", new DateTime(gst.Fch.Year, gst.Fch.Month, gst.Fch.Day)));
                fil3.Operands.Add(new BinaryOperator("Tp", EBalanceTipo.Gasto));

                Balance bl = os.FindObject<Balance>(fil3);

                if (bl == null)
                {
                    bl = os.CreateObject<Balance>();
                    bl.Cncpt = gst.Cncpt.Dscrpcn;
                    bl.Dclrbl = gst.Dclrbl;
                    bl.Fch = new DateTime(gst.Fch.Year, gst.Fch.Month, gst.Fch.Day);
                    bl.FrmPg = os.FindObject<Catalogo>(new BinaryOperator("Oid", gst.FrmPg.Oid));
                    bl.Mnt = gst.Mnt;
                    bl.Tp = EBalanceTipo.Gasto;
                }
                else
                {
                    bl.Mnt += gst.Mnt;
                }
            }
        }

        public static void CancelaGasto(IList gsts, IObjectSpace os)
        {
            foreach (Gasto gst in gsts)
            {
                if (gst.Stts == EEstadoDcmnt.Alta)
                {
                    GroupOperator fil3 = new GroupOperator();

                    fil3.Operands.Add(new BinaryOperator("Cncpt", gst.Cncpt.Dscrpcn));
                    fil3.Operands.Add(new BinaryOperator("Dclrbl", gst.Dclrbl));
                    fil3.Operands.Add(new BinaryOperator("FrmPg", gst.FrmPg));
                    fil3.Operands.Add(new BinaryOperator("Fch", new DateTime(gst.Fch.Year, gst.Fch.Month, gst.Fch.Day)));
                    fil3.Operands.Add(new BinaryOperator("Tp", EBalanceTipo.Gasto));

                    Balance bl = os.FindObject<Balance>(fil3);

                    if (bl != null)
                    {
                        bl.Mnt -= gst.Mnt;
                    }
                    gst.Stts = EEstadoDcmnt.Cancelado;
                }
            }
            os.CommitChanges();
        }


        public static void IniciaCalificacionMtr(CalificacionMtr obj, IObjectSpace os)
        {
            if (obj.MtrGrp != null)
            {
                foreach (AlumnoMtr am in obj.MtrGrp.Alumnos)
                {
                    bool existe = false;
                    foreach (CalificacionMtrAlmns clf in obj.Alumnos)
                    {
                        existe = am.Almn == clf.Almn;

                        if (existe)
                            break;
                    }

                    if (!existe)
                    {
                        CalificacionMtrAlmns calm = os.CreateObject<CalificacionMtrAlmns>();

                        calm.Almn = am.Almn;
                        calm.Stts = am.Stts;
                        obj.Alumnos.Add(calm);
                    }
                }
            }
        }

        public static void BajaAlumno(Alumno alm, IObjectSpace oS)
        {
            if (alm != null && alm.StatusAcdmc != SttsAcdmc.Baja)
            {
                /*TI Ene 2017 Parece que mejor no haga esto y que en los reportes se pueda filtrar que es lo que se quiere ver
                IList lista = oS.GetObjects(typeof(DocumentoSalida), new BinaryOperator("Almn", alm));

                foreach (DocumentoSalida dS in lista)
                {
                    if (dS.Stts != EEstadoDcmnt.Cancelado)
                        CancelaDoc(dS, oS, false);
                }

                lista = oS.GetObjects(typeof(Cxc), new BinaryOperator("Cliente", alm));
                CancelaCxc(lista, oS, false);
                CancelaCxc(lista, oS, false);
                */

                alm.StatusAcdmc = SttsAcdmc.Baja;
                oS.CommitChanges();
            }
        }

        public static void IniciaAsistenciaMtr(AsistenciaMtr obj, IObjectSpace os)
        {
            if (obj.MtrGrp != null)
            {
                // Sólo si no hay alumnos dados de alta.
                if (obj.Alumnos.Count == 0)
                {
                    foreach (AlumnoMtr am in obj.MtrGrp.Alumnos)
                    {
                        bool existe = false;
                        foreach (AsistenciaMtrAlmns sis in obj.Alumnos)
                        {
                            existe = am.Almn == sis.Almn;

                            if (existe)
                                break;
                        }

                        if (!existe)
                        {
                            AsistenciaMtrAlmns aalm = os.CreateObject<AsistenciaMtrAlmns>();

                            aalm.Almn = am.Almn;
                            aalm.Stts = am.Stts;
                            obj.Alumnos.Add(aalm);
                        }
                    }
                }
            }
        }


        public static float HorasPorDia(string dia)
        {
            float ttl = 0;
            if (!string.IsNullOrEmpty(dia))
            {
                string[] arrtoks = dia.Split(';');

                foreach (string hrs in arrtoks)
                {
                    ttl += HorasDia(hrs);
                }
            }

            return ttl;
        }

        private static float HorasDia(string dia)
        {
            float hrs = 0;
            if (!string.IsNullOrEmpty(dia))
            {
                string[] arrtoks = dia.Split('-');

                if (arrtoks.Length > 1)
                {
                    float ini;
                    float fin;

                    // Buscamos :
                    string[] min = arrtoks[0].Split(':');
                    if (min.Length > 1)
                    {
                        ini = Convert.ToSingle(min[0]);
                        ini += Convert.ToSingle(min[1]) / 60; //Debe ser entre 1 o 6 ?
                    }
                    else
                    {
                        ini = Convert.ToSingle(arrtoks[0]);
                    }

                    min = arrtoks[1].Split(':');
                    if (min.Length > 1)
                    {
                        fin = Convert.ToSingle(min[0]);
                        fin += Convert.ToSingle(min[1]) / 60;
                    }
                    else
                    {
                        fin = Convert.ToSingle(arrtoks[1]);
                    }

                    if (fin >= ini)
                    {
                        hrs = Convert.ToSingle(Math.Round(fin - ini, 2));
                    }
                }
            }
            return hrs;
        }

        public static float HrsDisponiblesPlaneacion(ref DateTime diaIni, CargaHoraria hrr,
            DateTime fin, Calendario Clndr)
        {
            float hrsDspnbls = 0;
            diaIni = diaIni.AddDays(-1);

            while (hrsDspnbls == 0 && diaIni <= fin)
            {
                diaIni = diaIni.AddDays(1);


                bool excp = false;
                foreach (CalendarioEvento evnt in Clndr.Eventos)
                    if (!excp)
                        excp = diaIni == evnt.Fch;

                if (!excp)
                {
                    if (diaIni.DayOfWeek == DayOfWeek.Monday)
                        hrsDspnbls = hrr.TtlLns;
                    else if (diaIni.DayOfWeek == DayOfWeek.Tuesday)
                        hrsDspnbls = hrr.TtlMrts;
                    else if (diaIni.DayOfWeek == DayOfWeek.Wednesday)
                        hrsDspnbls = hrr.TtlMrcls;
                    else if (diaIni.DayOfWeek == DayOfWeek.Thursday)
                        hrsDspnbls = hrr.TtlJvs;
                    else if (diaIni.DayOfWeek == DayOfWeek.Friday)
                        hrsDspnbls = hrr.TtlVrns;
                    else if (diaIni.DayOfWeek == DayOfWeek.Saturday)
                        hrsDspnbls = hrr.TtlSbd;
                }
            }
            return hrsDspnbls;
        }

        public static void HorasSemana(MateriaGrp mtgrp)
        {
            MateriaGrp obj = mtgrp;

            if (obj != null)
            {
                obj.Hrs = 0;
                foreach (Horario hr in obj.Horarios)
                {
                    hr.TtlLns = HorasPorDia(hr.Lns);
                    hr.TtlMrts = HorasPorDia(hr.Mrts);
                    hr.TtlMrcls = HorasPorDia(hr.Mrcls);
                    hr.TtlJvs = HorasPorDia(hr.Jvs);
                    hr.TtlVrns = HorasPorDia(hr.Vrns);
                    hr.TtlSbd = HorasPorDia(hr.Sbd);

                    obj.Hrs += Convert.ToInt16(hr.TtlLns + hr.TtlMrts + hr.TtlMrcls + hr.TtlJvs + hr.TtlVrns + hr.TtlSbd);
                }
            }
        }

        public static void HorasCalendario(MateriaGrp mtgrp)
        {
            MateriaGrp obj = mtgrp;

            obj.HrsClndr = 0;
            if (obj != null && obj.Clndr != null && obj.Horarios.Count > 0)
            {
                DateTime dia = obj.Clndr.FchIncl;
                Horario hrr = obj.Horarios[0];
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

        public static void ObtenTemas(MateriaGrp mtrGrp, IObjectSpace ojSp)
        {
            MateriaGrp mg = mtrGrp;
            SortingCollection sortProps = new SortingCollection();
            TemaMtr padre = null;


            sortProps.Add(new SortProperty("Nmr", SortingDirection.Ascending));
            mg.Mtr.Temas.Sorting = sortProps;


            foreach (Tema tm in mg.Mtr.Temas)
            {
                TemaMtr tmM = ojSp.CreateObject<TemaMtr>();

                tmM.Nmr = tm.Nmr;
                tmM.Dscrpcn = tm.Dscrpcn;
                tmM.Drcn = tm.Drcn;
                tmM.FchPrgrmd = tm.FchAplccn;
                tmM.CmptncEspcfc = tm.CmptncEspcfc;
                tmM.CmptncGnrc = tm.CmptncGnrc;
                tmM.Actvdds = tm.Actvdds;


                if (tm.TemaPadre != null)
                    tmM.TemaPadre = padre;
                if (tm.Children.Count > 0)
                    padre = tmM;


                mg.Temas.Add(tmM);
            }
        }


        public static float HrsDisponibles(ref DateTime diaIni, Horario hrr, 
            DateTime fin, Calendario Clndr)
        {
            float hrsDspnbls = 0;
            diaIni = diaIni.AddDays(-1);

            while (hrsDspnbls == 0 && diaIni <= fin)
            {
                diaIni = diaIni.AddDays(1);


                bool excp = false;
                foreach (CalendarioEvento evnt in Clndr.Eventos)
                    if (!excp)
                        excp = diaIni == evnt.Fch;

                if (!excp)
                {
                    if (diaIni.DayOfWeek == DayOfWeek.Monday)
                        hrsDspnbls = hrr.TtlLns;
                    else if (diaIni.DayOfWeek == DayOfWeek.Tuesday)
                        hrsDspnbls = hrr.TtlMrts;
                    else if (diaIni.DayOfWeek == DayOfWeek.Wednesday)
                        hrsDspnbls = hrr.TtlMrcls;
                    else if (diaIni.DayOfWeek == DayOfWeek.Thursday)
                        hrsDspnbls = hrr.TtlJvs;
                    else if (diaIni.DayOfWeek == DayOfWeek.Friday)
                        hrsDspnbls = hrr.TtlVrns;
                    else if (diaIni.DayOfWeek == DayOfWeek.Saturday)
                        hrsDspnbls = hrr.TtlSbd;
                }
            }
            return hrsDspnbls;
        }

        public static void FechasAplicacionTemas(MateriaGrp mtGrp)
        {
            MateriaGrp obj = mtGrp;

            if (obj != null && obj.Temas != null && obj.Temas.Count > 0
                && obj.Clndr != null && obj.Horarios != null && obj.Horarios.Count > 0)
            {
                SortingCollection sortProps = new SortingCollection();
                float hrsDspnbls = 0;
                float hrsNecesarias;
                float hrsCls = 0;
                DateTime diaIni = obj.Clndr.FchIncl;


                sortProps.Add(new SortProperty("Nmr", SortingDirection.Ascending));
                obj.Temas.Sorting = sortProps;


                hrsDspnbls = HrsDisponibles(ref diaIni,
                    obj.Horarios[0], obj.Clndr.FchFnl, obj.Clndr);
                hrsCls += hrsDspnbls;

                if (hrsDspnbls > 0)
                {
                    foreach (TemaMtr tm in obj.Temas)
                    {
                        hrsNecesarias = tm.Drcn;
                        tm.FchPrgrmd = diaIni;

                        if (tm.Children == null
                            || tm.Children.Count == 0)
                        {
                            // Este día es suficiente
                            if (hrsNecesarias == hrsDspnbls)
                            {
                                diaIni = diaIni.AddDays(1);
                                hrsDspnbls = HrsDisponibles(ref diaIni,
                                    obj.Horarios[0], obj.Clndr.FchFnl,
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
                                    hrsDspnbls = HrsDisponibles(ref diaIni, obj.Horarios[0], obj.Clndr.FchFnl, obj.Clndr);

                                    hrsCls += hrsDspnbls;
                                }
                                hrsDspnbls -= hrsNecesarias;
                            }
                            // Este día sobran horas
                            else
                            {
                                hrsDspnbls -= hrsNecesarias;
                            }

                            /*
                            if (hrsDspnbls <= 0)
                                break;*/
                            if (hrsDspnbls <= 0)
                            {
                                diaIni = diaIni.AddDays(1);
                                hrsDspnbls = Negocio.HrsDisponibles(ref diaIni,
                                    obj.Horarios[0], obj.Clndr.FchFnl, obj.Clndr);

                                hrsCls += hrsDspnbls;
                                if (diaIni > obj.Clndr.FchFnl)
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public static bool HayClase(DateTime dia, Horario hrr)
        {
            if (dia.DayOfWeek == DayOfWeek.Monday)
            {
                // Por el momento supondré que sólo hay un horario
                return hrr.TtlLns > 0;
            }
            else if (dia.DayOfWeek == DayOfWeek.Tuesday)
            {
                return hrr.TtlMrts > 0;
            }
            else if (dia.DayOfWeek == DayOfWeek.Wednesday)
            {
                return hrr.TtlMrcls > 0;
            }
            else if (dia.DayOfWeek == DayOfWeek.Thursday)
            {
                return hrr.TtlJvs > 0;
            }
            else if (dia.DayOfWeek == DayOfWeek.Friday)
            {
                return hrr.TtlVrns > 0;
            }
            else if (dia.DayOfWeek == DayOfWeek.Saturday)
            {
                return hrr.TtlSbd > 0;
            }
            else
                return false;
        }

        public static bool DiaExcep(DateTime dia, Calendario clndr)
        {
            bool excp = false;

            /*
            while (dia <= clndr.FchFnl)
            {
                excp = false;*/
            foreach (CalendarioEvento evnt in clndr.Eventos)
                if (!excp)
                    excp = dia == evnt.Fch;
            /*
        }*/

            return excp;
        }

    }
}
