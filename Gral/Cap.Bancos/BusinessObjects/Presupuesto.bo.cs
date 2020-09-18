#region Copyright (c) 2017-2020 cjlc
/*
{+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++)
{                                                                   }
{     cjlc Cap control administrativo personal                      }
{                                                                   }
{     Copyrigth (c) 2017-2020 cjlc                                  }
{     Todos los derechos reservados                                 }
{                                                                   }
{*******************************************************************}
 */
#endregion

using Cap.Generales.BusinessObjects.General;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.Drawing;

namespace Cap.Bancos.BusinessObjects
{
    [NavigationItem("Bancos")]
    [ImageName("Total")]
    public partial class Presupuesto : ISingleton
    {
        //#region Properties
        private string FClave;
        [VisibleInDetailView(false)]
        [Size(10), Indexed(Unique = true)]
        public string Clave
        {
            get { return FClave; }
            set
            {
                if (value != null)
                    SetPropertyValue("Clave", ref FClave, ValorString("Clave", value.ToUpper()));
            }
        }

        public static string ClaveFto(string val)
        {
            string cve;

            cve = val.Trim().Length > 10 ? val.Trim().Substring(0, 10).ToUpper() : val.Trim().ToUpper();

            return cve;
        }

        private string FDescripcion;
        [DisplayName("Descripción")]
        [Size(40)]
        public string Descripcion
        {
            get { return FDescripcion; }
            set { SetPropertyValue("Descripcion", ref FDescripcion, ValorString("Descripcion", value)); }
        }

        private decimal FAbonos;
        [Appearance("PAbonos", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        public decimal Abonos
        {
            get
            {
                decimal abono = 0;

                foreach (ConceptoP partida in Partidas)
                {
                    if (partida.Concepto != null && partida.Concepto.EsAbono())
                        abono += partida.Monto;
                }
                return abono;
            }
            set { SetPropertyValue("Abonos", ref FAbonos, value); }
        }

        private decimal FCargos;
        [Appearance("PCargos", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        public decimal Cargos
        {
            get
            {
                decimal cargos = 0;

                foreach (ConceptoP partida in Partidas)
                {
                    if (partida.Concepto != null && partida.Concepto.EsCargo())
                        cargos += partida.Monto;
                }
                return cargos;
            }
            set { SetPropertyValue("Cargos", ref FCargos, value); }
        }

        private decimal mTotal;
        [DisplayName("Balance")]
        [Appearance("Presupuesto.TotalE", Context = "DetailView", Enabled = false, FontStyle = FontStyle.Italic)]
        [Appearance("Presupuesto.Total", Context = "DetailView", Criteria = "Total < 0", FontColor = "Red")]
        [PersistentAlias("Abonos - Cargos")]
        public decimal Total
        {
            get
            {
                object tempObject = EvaluateAlias("Total");
                if (tempObject != null)
                {
                    return (decimal)tempObject;
                }
                else
                {
                    return 0;
                }
            }
            set { SetPropertyValue("Total", ref mTotal, value); }
        }

        [DisplayName("Conceptos")]
        [Association("Presupuesto-Partidas", typeof(ConceptoP)), Aggregated]
        public XPCollection Partidas
        {
            get { return GetCollection("Partidas"); }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Abonos = 0;
            Cargos = 0;
            Clave = "icpac";
            Descripcion = "Presupuesto mensual";
        }
    }
}
