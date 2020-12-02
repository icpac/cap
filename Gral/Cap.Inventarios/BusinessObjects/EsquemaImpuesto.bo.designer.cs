using System;
using System.Collections.Generic;

using System.Linq;

using System.Text;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace Cap.Inventarios.BusinessObjects
{
    [DefaultClassOptions]
    public partial class EsquemaImpuesto // : DevExpress.Persistent.BaseImpl.BaseObject
    {
        public EsquemaImpuesto(DevExpress.Xpo.Session session)
            : base(session)
        {

        }
    }
}
