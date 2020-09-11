#region Copyright (c) 2014-2020 cjlc
/*
{+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++)
{                                                                   }
{     cjlc Cap control administrativo personal                      }
{                                                                   }
{     Copyrigth (c) 2014-2020 cjlc                                  }
{     Todos los derechos reservados                                 }
{                                                                   }
{*******************************************************************}
 */
#endregion

using DevExpress.Persistent.Base;

namespace Cap.Bancos.BusinessObjects
{
    //#region + Concepto tipo enums
    public enum EConceptoTipo
    {
        /*
        Salida = 1,
        Entrada = 2,*/
        [ImageName("Label-Red")]
        Cargo = 3,
        [ImageName("Label-Green")]
        Abono = 4,
        Ninguno = 5
    }
    //#endregion
}
