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

using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace Cap.Bancos.BusinessObjects
{
    [NavigationItem("Bancos")]
    [ImageName("Price_Ticket")]
    public partial class ConceptoB
    {
        //#region Properties
        //#region + Gasto (?)
        // Para al dar de alta el movimiento generé el gasto
        private bool FGasto;
        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public bool Gasto
        {
            get { return FGasto; }
            set { SetPropertyValue("Gasto", ref FGasto, value); }
        }
        //#endregion

        //#region + Concepto a generar
        private ConceptoB FGenera;
        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public ConceptoB Genera
        {
            get { return FGenera; }
            set { SetPropertyValue("Genera", ref FGenera, value); }
        }

        //#region + Monto del concepto a generar
        private decimal FMontoG;
        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public decimal MontoG
        {
            get { return FMontoG; }
            set { SetPropertyValue("FMontoG", ref FMontoG, value); }
        }
        //#endregion

        //#region + Tipo del monto % o valor
        private MontoGTipo FMontoGTipo;
        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public MontoGTipo MontoGTipo
        {
            get { return FMontoGTipo; }
            set { SetPropertyValue("MontoGTipo", ref FMontoGTipo, value); }
        }
        //#endregion
        //#endregion


        [RuleFromBoolProperty("ConceptoB.TipoOk", DefaultContexts.Save, "Debe indicar un Tipo de Concepto")]
        protected bool TipoOk
        {
            get { return Tipo != EConceptoTipo.Ninguno; }
        }

        private EConceptoTipo FTipo;
        [Index(2)]
        [VisibleInLookupListView(true)]
        public EConceptoTipo Tipo
        {
            get { return FTipo; }
            set
            {
                SetPropertyValue("Tipo", ref FTipo, value);

                if (IsLoading)
                    TipoA = value;
            }
        }

        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [NonPersistent]
        public EConceptoTipo TipoA { get; set; }



        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Gasto = false;
            Genera = null;
            MontoG = 0;
            MontoGTipo = MontoGTipo.Cantidad;
            Tipo = EConceptoTipo.Ninguno;
        }


        public bool EsAbono()
        {
            return Tipo == EConceptoTipo.Abono;
        }

        public bool EsCargo()
        {
            return Tipo == EConceptoTipo.Cargo;
        }

        public void Abono()
        {
            Tipo = EConceptoTipo.Abono;
        }

        public void Cargo()
        {
            Tipo = EConceptoTipo.Cargo;
        }

        public void Ninguno()
        {
            Tipo = EConceptoTipo.Ninguno;
        }
    }
}
