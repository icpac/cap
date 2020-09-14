#region 2014-2020 tit
/*
{+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++)
{                                                                   }
{     Cap control administrativo personal                           }
{                                                                   }
{     2014-2020 tit                                                 }
{     tlacaelel.icpac@gmail.com                                     }
{                                                                   }
{*******************************************************************}
 */
#endregion

using DevExpress.ExpressApp.DC;

namespace Cap.Bancos.BusinessObjects
{
    public enum EMovimientoTipo
    {
        Normal = 1,
        Cheque = 2,
        Gasto = 3,
        [XafDisplayName("Transf. Electrónica F.")]
        TEF = 4,
        Ninguno = 5
    }
}
