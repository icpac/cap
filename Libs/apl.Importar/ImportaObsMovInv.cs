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
    #region + Importa Observaciones Movimientos al inv
    public class ImportaObsMovInv : ImportaObs
    {
        #region + Constructor
        public ImportaObsMovInv(string path, string numEmp)
            : base(path, numEmp)
        {
            NombreFile = "OMIN.D";
        }
        #endregion
    }
    #endregion
}
