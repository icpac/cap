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

    #region + Importa Observaciones Cxp
    public class ImportaObsCxp : ImportaObs
    {
        public ImportaObsCxp(string path, string numEmp)
            : base(path, numEmp)
        {
            NombreFile = "OCXP.D";
        }
    }
    #endregion
}
