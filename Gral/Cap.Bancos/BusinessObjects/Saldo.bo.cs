﻿#region Copyright (c) 2015-2020 cjlc
/*
{+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++)
{                                                                   }
{     cjlc Cap control administrativo personal                      }
{                                                                   }
{     Copyrigth (c) 2015-2020 cjlc                                  }
{     Todos los derechos reservados                                 }
{                                                                   }
{*******************************************************************}
 */
#endregion

using DevExpress.Xpo;

namespace Cap.Bancos.BusinessObjects
{
    public partial class Saldo
    {
        //#region Public Properties
        // Con esto se cayo el crear la tabla
        //[Association("Bancaria-Saldos")]
        [Indexed("Cuenta; Periodo", Unique = true)]
        private Bancaria FCuenta;
        public Bancaria Cuenta
        {
            get { return FCuenta; }
            set { SetPropertyValue("Cuenta", ref FCuenta, value); }
        }

        //#region + Periodo
        // Por qué le pusí NonP... ? Dic 2019 
        [NonPersistent]
        public string periodo;
        [Size(6)]
        public string Periodo
        {
            get { return periodo; }
            set
            {
                if (value != null && value.Length > 6)
                    SetPropertyValue("Periodo", ref periodo, value.Substring(6, 4) + value.Substring(3, 2));
                else
                    SetPropertyValue("Periodo", ref periodo, value);
            }
        }
        //#endregion

        //#region + Monto
        private decimal saldo;
        public decimal Monto
        {
            get { return saldo; }
            set { SetPropertyValue("Monto", ref saldo, value); }
        }
        //#endregion
        //#endregion

        //#region + After construction
        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Monto = 0.0m;
            Periodo = string.Empty;
            Cuenta = null;
        }
    }
}
