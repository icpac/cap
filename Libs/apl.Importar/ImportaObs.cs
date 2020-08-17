#region Copyright (c) 2000-2012 cjlc
/*
{+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++)
{                                                                   }
{     cjlc Cap control administrativo personal                      }
{                                                                   }
{     Copyrigth (c) 2000-2012 cjlc                                  }
{     Todos los derechos reservados                                 }
{                                                                   }
{*******************************************************************}
 */
#endregion

using System;
using System.Text;

namespace apl.Importar
{
    using System.IO;

    #region + Importa Observaciones - Aspel
    public class ImportaObs : ImportarAspel
    {
        #region + Constructor
        public ImportaObs(string path, string numEmp)
            : base(path, numEmp)
        {
            Ruta = path;
            NumeroEmpresa = numEmp;

            LongitudFile = 30;
        }
        #endregion

        #region + Observacion, obtiene del binario
        public string Observacion(UInt32 pos)
        {
            string observa = string.Empty;

            if (pos > 0)
            {
                ASCIIEncoding enc = new ASCIIEncoding();

                /* Estaba descomentado pero parece que eran prueba !
                 * 13 Oct 11 los comenté 
                BinReader.BaseStream.Seek((pos-1) * 30, SeekOrigin.Begin);
                LeeRegistro();
                observa = enc.GetString(Bytes);

                BinReader.BaseStream.Seek((pos+1) * 30, SeekOrigin.Begin);
                LeeRegistro();
                observa = enc.GetString(Bytes);*/

                try
                {
                    BinReader.BaseStream.Seek(pos * 30, SeekOrigin.Begin);
                    LeeRegistro();
                    observa = enc.GetString(Bytes);
                    while (observa.EndsWith("@"))
                    {
                        LeeRegistro();
                        observa = observa.Remove(observa.Length - 1, 1);
                        observa += enc.GetString(Bytes);
                    }
                }
                catch (Exception ex)
                {
                    apl.Log.LogDebug.Escribe(ex.Message);
                }
            }
            return observa;
        }
        #endregion
    }
    #endregion
}
