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

namespace apl.Log
{
    /// <summary>
    /// 
    /// </summary>
    public class Fecha
    {
        /// <summary>
        /// Regresa la fecha inicial del mes: 01 / mes / añoActual
        /// </summary>
        /// <param name="_mes">Regresa la fecha de este _mes</param>
        /// <returns></returns>
        public static DateTime FechaInicial(int _mes)
        {
            return FechaInicial(_mes, DateTime.Today.Year);
        }

        /// <summary>
        /// Regresa la fecha inicial del mes, año: 01 / mes / año Hora: 0:0:0 
        /// </summary>
        /// <param name="_mes"></param>
        /// <param name="_año"></param>
        /// <returns></returns>
        public static DateTime FechaInicial(int _mes, int _año)
        {
            return new DateTime(_año, _mes, 1, 0, 0, 0);
        }

        /// <summary>
        /// Da la fecha inicial, considerando el tiempo 0
        /// </summary>
        /// <param name="dia"></param>
        /// <param name="_mes"></param>
        /// <param name="_año"></param>
        /// <returns></returns>
        public static DateTime FechaInicial(int dia, int _mes, int _año)
        {
            return new DateTime(_año, _mes, dia, 0, 0, 0);
        }

        /// <summary>
        /// Regresa Fecha inicial, la misma fecha con hora, 0, 0, 0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime FechaInicial(DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, 0, 0, 0);
        }
        
        /// <summary>
        /// Fecha final con tiempo: hora 23 min 59, seg 59
        /// </summary>
        /// <param name="dia"></param>
        /// <param name="_mes"></param>
        /// <param name="_año"></param>
        /// <returns></returns>
        public static DateTime FechaFinal(int dia, int _mes, int _año)
        {
            return new DateTime(_año, _mes, dia, 23, 59, 59);
        }

        /// <summary>
        /// Fecha del último día del mes, año: Ult día / mes / año Hora 23:59:59
        /// </summary>
        /// <param name="_mes"></param>
        /// <param name="_año"></param>
        /// <returns></returns>
        public static DateTime FechaFinal(int _mes, int _año)
        {
            DateTime aux;

            if (_mes < 12)
                aux = new DateTime(_año, _mes + 1, 1, 23, 59, 59);
            else
                aux = new DateTime(_año + 1, 1, 1, 23, 59, 59);
            aux = aux.AddDays(-1);

            return aux;
        }

        /// <summary>
        /// Fecha final de este mes: Ult dia / mes / Este año
        /// </summary>
        /// <param name="_mes"></param>
        /// <returns></returns>
        public static DateTime FechaFinal(int _mes)
        {
            return FechaFinal(_mes, DateTime.Today.Year);
        }

        /// <summary>
        /// Regresa una fecha casi igual que la del parámetro, con hora 23 59 59
        /// </summary>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        public static DateTime FechaFinal(DateTime fechaFinal)
        {
            return new DateTime(fechaFinal.Year, fechaFinal.Month, fechaFinal.Day, 23, 59, 59);
        }
    }
}
