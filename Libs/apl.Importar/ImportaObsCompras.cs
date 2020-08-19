#region 
/*
{+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++)
{                                                                   }
{     tlacaelel.icpac                                               }
{     Cap control administrativo personal                           }
{                                                                   }
{     2000-2012                                                     }
{                                                                   }
{*******************************************************************}
 */
#endregion

using System;

namespace apl.Importar
{
    #region + Importa Observaciones Compras
    public class ImportaObsCompras : ImportaObs
    {
        #region + Constructor
        public ImportaObsCompras(string path, string numEmp)
            : base(path, numEmp)
        {
            NombreFile = "OCOM.D";
        }
        #endregion
    }
    #endregion
}
