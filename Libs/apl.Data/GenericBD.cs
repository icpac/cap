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
/*
 * myBd.cs		Libreria para manejo de Base de Datos, los accesos particulares se derivan de esta.
 *              Por ejemplo Access.
 *              Parece que la coneccion esta quedando abierta durante toda la ejecución del programa.
 *              Por el momento estas funciones nos han sido suficientes.
 *              Falta hacer el formato XML, Paradox
 *              Sobreescribir crear base de datos
 *              Sobre escribir crear tablas (con Access, y Sql server se necesita conocer el schema y lo hacen
 *                  de manera distinta
 *              Sobre escribir actualizar tablas.
 */
#endregion

using System;
using System.Data;

namespace apl.Data
{
    #region using 
    using System.Collections.Specialized;
    #endregion

    #region GenericBD
    /// <summary>
	/// Clase abstracta que implementa lo necesario para accesar
	/// la base de datos
	/// </summary>
    public abstract class GenericBD : IDisposable
	{
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        ~GenericBD()
        {
            Dispose(false);
        }

        #region + abstract Conexion
        /// <summary>
		/// Para obtener el objeto conexion, de acuerdo al proveedor. 
		/// La clase derivada de GenericBD regresa su conexión particular.
        /// Igual la clase para Access regresa su conexion particular.
		/// </summary>
		/// <returns></returns>
		public abstract IDbConnection obtenConexion(string conexion);

        private IDbConnection FConnection;
        virtual protected IDbConnection Coneccion
        {
            get 
            {
                if (FConnection == null)
                {
                    if (CadenaConeccion == null || CadenaConeccion.Trim() == string.Empty)
                        throw new Exception("Falta la cadena de conexion");

                    FConnection = obtenConexion(CadenaConeccion);
                }

                return FConnection;
            }
        }
		#endregion

        #region + Cadena de coneccion
        public string CadenaConeccion { get; set; }
        #endregion

		#region Parámetros
		protected abstract IDataParameter GetBlobParameter(IDbConnection connection, IDataParameter p);

		public abstract IDataParameter obtenParametro();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
		public virtual  IDataParameter obtenParametro(string name, DbType dbType, int size, ParameterDirection direction)
		{
			IDataParameter parametro = obtenParametro();
			
			parametro.DbType		= dbType;
			parametro.Direction		= direction;
			parametro.ParameterName = name;

			if (size > 0 && parametro is IDbDataParameter) 
			{
				IDbDataParameter dbParametro = (IDbDataParameter)parametro;
				dbParametro.Size = size;
			}
			return parametro;
		}
		#endregion

		#region Asocia los parametros
		protected virtual void AttachParameters(IDbCommand command, IDataParameter[] commandParameters)
		{
			if (commandParameters != null)
			{
				foreach (IDataParameter p in commandParameters)
				{
					if (p != null)
					{
						// Check for derived output value with no value assigned
						if (   (p.Direction == ParameterDirection.InputOutput 
							|| 	p.Direction == ParameterDirection.Input) 
							&& (p.Value == null))
						{
							p.Value = DBNull.Value;
						}

						if (p.DbType == DbType.Binary) 
						{
							// special handling for BLOBs
							command.Parameters.Add(GetBlobParameter(command.Connection, p));
						}
						else
						{
							command.Parameters.Add(p);
						}
					}
				}
			}
		}
		#endregion update

		#region Prepara el command, para ejecutarse
		/// <summary>
		/// This method opens (if necessary) and assigns a connection, transaction, command type and parameters 
		/// to the provided command
		/// </summary>
		/// <param name="command">The IDbCommand to be prepared</param>
		/// <param name="connection">A valid IDbConnection, on which to execute this command</param>
		/// <param name="transaction">A valid IDbTransaction, or 'null'</param>
		/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">The stored procedure name or SQL command</param>
		/// <param name="commandParameters">An array of IDataParameters to be associated with the command or 'null' if no parameters are required</param>
		/// <param name="mustCloseConnection"><c>true</c> if the connection was opened by the method, otherwose is false.</param>
		private void PrepareCommand(IDbCommand command, IDbConnection connection, IDbTransaction transaction, CommandType commandType, 
            string commandText, IDataParameter[] commandParameters, out bool mustCloseConnection)
		{
			if(commandText == null || commandText.Length == 0) throw new ArgumentNullException( "commandText" );

			// If the provided connection is not open, we will open it
			if (connection.State != ConnectionState.Open)
			{
				mustCloseConnection = true;
				connection.Open();
			}
			else
			{
				mustCloseConnection = false;
			}

			// Associate the connection with the command
			command.Connection = connection;

			// Set the command text (stored procedure name or SQL statement)
			command.CommandText = commandText;

			// If we were provided a transaction, assign it
			if (transaction != null)
			{
				if( transaction.Connection == null ) throw new ArgumentException( "The transaction was rollbacked or commited, please provide an open transaction.", "transaction" );
				command.Transaction = transaction;
			}

			// Set the command type
			command.CommandType = commandType;

			// Attach the command parameters if they are provided
			if (commandParameters != null)
			{
				AttachParameters(command, commandParameters);
			}
			return;
		}
		#endregion

        #region # Execute reader
        /// <summary>
        /// Executes the query and returns a reader object
        /// </summary>
        /// <param name="command">Command to execute </param>
        /// <returns>Data Reader</returns>
        private IDataReader ExecuteReader(IDbCommand command)
        {
            IDataReader reader = null;

            try
            {
                reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                apl.Log.LogDebug.Escribe(String.Format("{0}, {1}", command.CommandText, ex.Message));
            }
            return reader;
        }
        #endregion

        #region Ejecuta sentencia
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlSentencia"></param>
        /// <param name="arrParam"></param>
        /// <returns></returns>
		public int ejecutaSentencia(string sqlSentencia, params IDataParameter[] arrParam)
		{
            IDbConnection conn = Coneccion;
            IDbCommand cmd = conn.CreateCommand();
            bool mustCloseConnection = false;

            PrepareCommand(cmd, conn, (IDbTransaction)null, CommandType.Text, sqlSentencia, arrParam, out mustCloseConnection);

            int retval = cmd.ExecuteNonQuery();

            // Detach the IDataParameters from the command object, so they can be used again
            // don't do this...screws up output parameters -- cjbreisch
            // cmd.Parameters.Clear();
            if (mustCloseConnection)
                conn.Close();

            return retval;
        }
		#endregion

        #region + Lee datos 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlSentencia"></param>
        /// <param name="arrParams"></param>
        /// <returns></returns>
        public IDataReader LeeDatos(string sqlSentencia, params IDataParameter[] arrParams)
        {
            IDbConnection conn = Coneccion;
            IDbCommand cmd = conn.CreateCommand();
            IDataReader reader = null;
            bool mustCloseConnection = false;

            PrepareCommand(cmd, conn, (IDbTransaction)null, CommandType.Text, sqlSentencia, arrParams, out mustCloseConnection);
            reader = ExecuteReader(cmd);
            
            return reader;
        }
        #endregion

        #region # Crea data adapter
        protected abstract IDbDataAdapter CreateDataAdapter();
        #endregion

        #region + Lee data set
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlSentencia"></param>
        /// <param name="arrParams"></param>
        /// <returns></returns>
        public DataSet LeeDataSet(string sqlSentencia, params IDataParameter[] arrParams)
        {
            IDbConnection conn = Coneccion;
            IDbCommand cmd = conn.CreateCommand();
            IDbDataAdapter adapter = CreateDataAdapter(); 
            DataSet datos = new DataSet();
            bool mustCloseConnection = false;

            cmd.CommandText = sqlSentencia;
            PrepareCommand(cmd, conn, (IDbTransaction)null, CommandType.Text, sqlSentencia, arrParams, out mustCloseConnection);

            adapter.SelectCommand = cmd;
            adapter.Fill(datos);
            return datos;
        }
        #endregion

        #region + Crea tabla
        /// <summary>
        /// Suponemos que cada base de datos tienes sus particulares en sus sentencias
        /// para crear las tablas. True si puedo crear la tabla 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cmps"></param>
        /// <param name="tips"></param>
        /// <param name="tams"></param>
        /// <param name="keys"></param>
        /// <param name="indexs"></param>
        public abstract bool CreaTabla(string name, StringCollection cmps, StringCollection tips,
            StringCollection tams, StringCollection keys, StringCollection indexs, StringCollection uniques);
        #endregion

        #region + abstract Borra tabla
        public abstract void BorraTabla(string name);
        #endregion

        #region + Crea base de datos
        /// <summary>
        /// Creo que jala en MySQL, pero en Access se hace de otra manera.
        /// </summary>
        public virtual void CreaBaseDatos()
        {
            ejecutaSentencia("CREATE DATABASE icpac", null);
        }
        #endregion
    }
	#endregion
}
