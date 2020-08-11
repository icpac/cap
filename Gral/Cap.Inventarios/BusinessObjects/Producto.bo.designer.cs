using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using Cap.Generales.BusinessObjects.Object;

namespace Cap.Inventarios.BusinessObjects
{
    [DefaultClassOptions]
    public partial class Producto : PObject
    {
        public Producto(DevExpress.Xpo.Session session)
            : base(session)
        {
        }
    }
}
