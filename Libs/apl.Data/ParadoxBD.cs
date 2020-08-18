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
using System.Collections.Generic;

namespace apl.Data
{
    using System.Data;
    using System.Data.Odbc;
    using System.Collections.Specialized;

    public class ParadoxBD : GenericBD
    {
        #region + override Get Blob parameter
        protected override IDataParameter GetBlobParameter(IDbConnection connection, IDataParameter p)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        #endregion

        #region + override obten coneccion con cadena de coneccion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cadenaConexion"></param>
        /// <returns></returns>
        public override IDbConnection obtenConexion(string cadenaConexion)
        {
            return new OdbcConnection(cadenaConexion);
        }
        #endregion

        #region # Create data adapter
        protected override IDbDataAdapter CreateDataAdapter()
        {
            return new OdbcDataAdapter();
        }
        #endregion

        #region + override obten parametro
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IDataParameter obtenParametro()
        {
            return new OdbcParameter();
        }
        #endregion

        #region + override Borra tabla
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameTable"></param>
        public override void BorraTabla(string nameTable)
        {
            /*
            if (ExisteTabla(nameTable))
            {
                string sqlTabla;
                IDataParameter[] parames = null;

                sqlTabla = "DROP TABLE ";
                sqlTabla += nameTable;

                ejecutaSentencia(sqlTabla, parames);
            }*/
        }
        #endregion

        #region + Crea tabla
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameTable"></param>
        /// <param name="campos"></param>
        /// <param name="tipos"></param>
        /// <param name="tamanos"></param>
        /// <param name="keys"></param>
        /// <param name="indexs"></param>
        /// <param name="uniques"></param>
        /// <returns></returns>
        public override bool CreaTabla(string nameTable, StringCollection campos, StringCollection tipos,
            StringCollection tamanos, StringCollection keys, StringCollection indexs, StringCollection uniques)
        {
            /*
            bool ok = false;

            if (!ExisteTabla(nameTable))
            {
                string sqlTabla;
                IDataParameter[] parames = null;

                sqlTabla = SentenciaTabla(nameTable, campos, tipos, tamanos, keys, indexs, uniques);
                ejecutaSentencia(sqlTabla, parames);

                if (indexs.Count > 0)
                    foreach (string campo in indexs)
                    {
                        sqlTabla = "CREATE INDEX Indice" + campo;
                        sqlTabla += " ON " + nameTable + " ( " + campo + " )";
                        ejecutaSentencia(sqlTabla, parames);
                    }

                ok = true;
            }
            else
            {
                VerificaTabla(nameTable, campos, tipos, tamanos);
            }
            return ok;*/
            return false;
        }
        #endregion
    }
}
