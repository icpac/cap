#region 2014-2020 
/*
{+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++)
{                                                                   }
{     Cap control administrativo personal                           }
{     tlacaelel.icapc@gmail.com                                     }
{     2014-2020 TIT                                                 }
{                                                                   }
{*******************************************************************}
 */
#endregion

using System;

namespace apl.Log
{
    public class Periodicidad
    {
        public static DateTime CalculaFechas(EPeriodicidad _periodo, DateTime _foriginal)
        {
            DateTime fnueva = DateTime.Today;

            if (_periodo == EPeriodicidad.Diaria)
                fnueva = _foriginal.AddDays(1);
            else if (_periodo == EPeriodicidad.Semanal)
                fnueva = _foriginal.AddDays(7);
            else if (_periodo == EPeriodicidad.Quincenal)
                fnueva = _foriginal.AddDays(14);
            else if (_periodo == EPeriodicidad.Mensual)
                fnueva = _foriginal.AddMonths(1);
            else if (_periodo == EPeriodicidad.Bimestral)
                fnueva = _foriginal.AddMonths(2);

            return fnueva;
        }
    }
}
