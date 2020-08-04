/*
 * Autor: Tlacaelel Icpac
 * Date: Mié Jun 30 2004
 * Time: 03:51 p.m.
 * 
 * Correo:		tlacaelel.icpac@gmail.com
 * 
 * mySQLBD.cs		Acceso a la base de datos MySQL.
 */

using System;
using System.Data;

// using ByteFX.Data.MySqlClient;
/*
using MySql.Data.MySqlClient;*/

namespace apl.Data
{
	#region mySQLBD
	/// <summary>
	/// Description of mySQLBD.	
	/// </summary>
	public class mySQLBD : GenericBD
    {
        #region + Constructores
        public mySQLBD()
		{
		}
		#endregion
		
		#region + override obten conexion sin parametros
		public override IDbConnection obtenConexion()
		{
			return new MySqlConnection();
		}
        #endregion

        #region + override obten coneccion con cadena de coneccion
        public override IDbConnection obtenConexion(string cadenaConexion)
		{
			return new MySqlConnection(cadenaConexion);
		}
		#endregion

		#region + obtenemos adapter sin parametros
		public override IDbDataAdapter obtenAdapter()
		{
			return new MySqlDataAdapter();
		}
		#endregion

		#region Blob
		//TODO: Verificar esto.
		protected override IDataParameter GetBlobParameter(IDbConnection connection, IDataParameter p)
		{
			// do nothing special for BLOBs...as far as we know now.
			return p;
		}
		#endregion

		#region Parametros
		public override IDataParameter obtenParametro()
		{
			return new MySqlParameter(); 
		}

		/// <summary>
		/// Calls the CommandBuilder.DeriveParameters method for the specified provider, doing any setup and cleanup necessary
		/// </summary>
		/// <param name="cmd">The IDbCommand referencing the stored procedure from which the parameter information is to be derived. The derived parameters are added to the Parameters collection of the IDbCommand. </param>
		public override void DeriveParameters( IDbCommand cmd )
		{
			/*TODO: Revisar Feb 2005
			bool mustCloseConnection = false;

			if( !( cmd is SqlCommand ) )
				throw new ArgumentException( "The command provided is not a SqlCommand instance.", "cmd" );
			
			if (cmd.Connection.State != ConnectionState.Open) 
			{
				cmd.Connection.Open();
				mustCloseConnection = true;
			}
			
			SqlDeriveParameters.DeriveParameters((SqlCommand)cmd );
			
			if (mustCloseConnection)
			{
				cmd.Connection.Close();
			}*/
		}
		#endregion

		#region + obten command sin parametros
		public override IDbCommand obtenCommand()
		{
		   return new MySqlCommand();
		}
		#endregion

        #region no se paque
        /*
		public override DataSet openSQL(string sql)
		{
			DataSet result = new DataSet();

			string myConnectionString = "Database=icpac;Data Source=localhost;User Id=root;Password=";
			// If the connection string is null, use a default.
			if(myConnectionString == "")
				myConnectionString = "Database=Test;Data Source=localhost;User Id=username;Password=pass";

			MySqlConnection myConnection = new MySqlConnection(myConnectionString);
			MySqlDataAdapter  myAdapter = new MySqlDataAdapter();

			myAdapter.SelectCommand = new MySqlCommand(sql, myConnection);
			myAdapter.Fill(result);
			myConnection.Close();

			return result;
		}*/

        /*
        public override bool execSQL(string sql)
        {
            DataSet result = new DataSet();

            string myConnectionString = "Database=precios_web2;Data Source=localhost;User Id=root;Password=password";
            // If the connection string is null, use a default.
            if(myConnectionString == "")
                myConnectionString = "Database=Test;Data Source=localhost;User Id=username;Password=pass";

            MySqlConnection myConnection = new MySqlConnection(myConnectionString);
            string mySql = "SELECT * FROM proveedor";
            MySqlDataAdapter  myAdapter = new MySqlDataAdapter();

            myAdapter.SelectCommand = new MySqlCommand(mySql, myConnection);
            myAdapter.Fill(result);
            myConnection.Close();

            return true;
        }*/
        #endregion
    }
	#endregion
}
