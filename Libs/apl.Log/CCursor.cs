#region Copyright (c) 2000-2013 cjlc
/*
{+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++)
{                                                                   }
{     cjlc Cap control administrativo personal                      }
{                                                                   }
{     Copyrigth (c) 2000-2013 cjlc                                  }
{     Todos los derechos reservados                                 }
{                                                                   }
{*******************************************************************}
 */
/*
 * CCursor.cs   Durante un proceso cambia el cursor actual al cursor de espera.
 * 
 */
#endregion

using System;
using System.Windows.Forms;

namespace apl.Log
{
	/// <summary>
	/// </summary>
	public class CCursor : IDisposable
	{
        private readonly Cursor saved = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newCursor"></param>
		public CCursor(Cursor newCursor)
		{
			saved = Cursor.Current;

			Cursor.Current = newCursor;		
		}

        /// <summary>
        /// 
        /// </summary>
		public void Dispose()
		{
			Cursor.Current = saved;
            if (saved != null)
                saved.Dispose();
		}
    }
}
