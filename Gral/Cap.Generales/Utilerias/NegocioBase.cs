using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Cap.Generales.Utilerias
{
    public class NegocioBase
    {
        public static void BuscaCP(string cp, List<string> codigos)
        {
            BuscaCP(cp, codigos, string.Empty);
        }

        public static void BuscaCP(string cp, List<string> codigos, string pt)
        {
            string filename = Path.Combine(pt, FileName);

            try
            {
                using (StreamReader sr = new StreamReader(filename, Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        char[] splitter = { '|' };
                        string[] arrtoks = line.Split(splitter);

                        if (arrtoks.Length > 0 && arrtoks[0] == cp)
                        {
                            codigos.Add(line);
                        }
                    }
                }
            }
            catch (Exception)
            {
                codigos.Add(string.Format("error|no |encontro |el |cp |o no existe {0} | archivo", pt));
            }
        }

        private static string FileName
        {
            get
            {
                string fn = "CP.txt"; // Path.Combine(Application.StartupPath, "CP.txt");
                if (!File.Exists(fn))
                {
                    // fn = cap.Log.Configuracion.LeeConfiguracion(string.Empty, "Aplicativo", "Red");
                    fn = string.IsNullOrEmpty(fn) ? "CP.txt" : Path.Combine(fn, "CP.txt");
                }
                return "CP.txt"; // fn; Por el momento!
            }
        }

        /*
        Para crear llave publica
        C:\Program Files\GnuWin32\bin>openssl.exe pkcs8 -inform DER -in C:\aaa010101aaa_CSD_01.key -passin pass:a0123456789 -out C:\aaa010101aaa_CSD_01.key.pem

        Para crear llave privada
        C:\Program Files\GnuWin32\bin>openssl.exe x509 -inform DER -outform PEM -in C:\aaa010101aaa_CSD_01.cer -pubkey -out C\aaa010101aaa_CSD_01.cer.pem
        */
        #region + Crea Archivos Pem
        public static void CreaArchivosPEM(string filc, string filk, string pass)
        {
            if (!string.IsNullOrEmpty(filk)
                && !string.IsNullOrEmpty(pass)
                && !string.IsNullOrEmpty(filc))
            {
                string ArchKeyPem = filc + ".pem";

                // if (!File.Exists(ArchKeyPem))
                //{
                Process myproc;

                try
                {
                    myproc = new Process();

                    myproc.StartInfo.UseShellExecute = false;
                    myproc.EnableRaisingEvents = false;
                    ProcessStartInfo psi = new ProcessStartInfo(
                        "openssl.exe", string.Format("pkcs8 -inform DER -in \"{0}\" -passin pass:\"{1}\" -out \"{2}.pem\"", filk, pass, filk));
                    /*
                    "openssl.exe", " pkcs8 -inform DER -in \"" +
                    filk + "\" -passin pass:" +
                    pass + " -out \"" + filk + ".pem\"");*/
                    myproc.StartInfo = psi;
                    myproc.Start();

                    ProcessStartInfo psi2 = new ProcessStartInfo(
                        "openssl.exe", " x509 -inform DER -outform PEM -in \""
                        + filc + "\" -pubkey -out \"" + filc + ".pem\"");
                    myproc.StartInfo = psi2;
                    myproc.Start();
                }
                catch (Exception ee)
                {
                    throw new Exception("No se pudieron crear archivos PEM\n" + ee.Message);
                }
                //}
            }
        }
        #endregion


        public static int IndiceDeColumna(Worksheet ws, string encabezado)
        {
            if (ws == null || string.IsNullOrEmpty(encabezado))
                return -1;
            return ws.Columns[encabezado].Index;
        }

        public static string CeldaTexto(Worksheet ws, int r, int c)
        {
            string texto;
            CellRange celda = ws.Cells[r, c];
            if (celda.Value.Type == CellValueType.None || celda.Value.Type == CellValueType.Error)
                texto = "";
            else
                texto = celda.Value.TextValue.Trim();
            return texto;
        }

        public static int CeldaEntero(Worksheet ws, int r, int c)
        {
            return Convert.ToInt32(CeldaNumero(ws, r, c));
        }

        public static bool CeldaBool(Worksheet ws, int r, int c)
        {
            CellRange celda = ws.Cells[r, c];
            return celda.Value.BooleanValue;
        }

        public static double CeldaNumero(Worksheet ws, int r, int c)
        {
            double valor = 0.0;
            try
            {
                CellRange celda = ws.Cells[r, c];
                if (celda.Value.Type == CellValueType.Numeric)
                    valor = celda.Value.NumericValue;
                else if (celda.Value.Type == CellValueType.Text)
                    valor = Convert.ToDouble(celda.Value.ToString(), null);
            }
            catch
            {
                valor = 0.0;
            }
            return valor;
        }

        // Ahi graba los pdfs y de ahi lo leemos.
        public static string CreaDirs(string ruta, DateTime fecha)
        {
            return CreaDirs(ruta, fecha, true);
        }

        /// <summary>
        /// Genera la ruta string del archivo, y en su caso lo crea físicamente.
        /// </summary>
        /// <param name="ruta"></param>
        /// <param name="fecha"></param>
        /// <param name="crea"></param>
        /// <returns></returns>
        public static string CreaDirs(string ruta, DateTime fecha, bool crea)
        {
            return CreaDirs(ruta, fecha, crea, ENivelDir.Dia);

            /*
            string aux = Path.Combine(ruta, fecha.Year.ToString());

            if (crea && !Directory.Exists(aux))
                Directory.CreateDirectory(aux);

            aux = Path.Combine(aux, fecha.Month.ToString());

            if (crea && !Directory.Exists(aux))
                Directory.CreateDirectory(aux);

            aux = Path.Combine(aux, fecha.Day.ToString());

            if (crea && !Directory.Exists(aux))
                Directory.CreateDirectory(aux);

            return aux;*/
        }

        public static string CreaDirs(string ruta, DateTime fecha, bool crea, ENivelDir nvl)
        {
            string aux = Path.Combine(ruta, fecha.Year.ToString());

            if (crea && !Directory.Exists(aux))
                Directory.CreateDirectory(aux);

            if (nvl == ENivelDir.Mes || nvl == ENivelDir.Dia)
            {
                aux = Path.Combine(aux, fecha.Month.ToString());

                if (crea && !Directory.Exists(aux))
                    Directory.CreateDirectory(aux);

                if (nvl == ENivelDir.Dia)
                {
                    aux = Path.Combine(aux, fecha.Day.ToString());

                    if (crea && !Directory.Exists(aux))
                        Directory.CreateDirectory(aux);
                }
            }
            return aux;
        }
    }

    public enum ENivelDir
    {
        Dia,
        Mes,
        Año
    }
}
