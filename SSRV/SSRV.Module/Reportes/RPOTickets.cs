using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ReportsV2;
using SSRV.Module.BusinessObjects.Servicio;
using DevExpress.ExpressApp.Model;

namespace SSRV.Module.Reportes
{
    [DomainComponent]
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113594.aspx.
    public class RPOTickets : ReportParametersObjectBase, INotifyPropertyChanged
    {
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public RPOTickets(IObjectSpaceCreator provider) : base(provider)
        {
            Status = StsSrvc.TODOS;
            FchIncl = apl.Log.Fecha.FechaInicial(DateTime.Today.Month);
            FchFnl = apl.Log.Fecha.FechaFinal(DateTime.Today.Month);
        }
        protected override IObjectSpace CreateObjectSpace()
        {
            //return objectSpaceCreator.CreateObjectSpace(null);
            return objectSpaceCreator.CreateObjectSpace(typeof(Incidencia));
        }
        public override CriteriaOperator GetCriteria()
        {
            /*
            CriteriaOperator criteria = new BinaryOperator("MyPropertyName", "MyValue");
            return criteria;*/
            GroupOperator fil = new GroupOperator();

            if (Status != StsSrvc.TODOS)
                fil.Operands.Add(new BinaryOperator("Status", Status));
            if (!string.IsNullOrEmpty(Folio))
                fil.Operands.Add(new BinaryOperator("Folio", Incidencia.FolioFto(Folio)));
            fil.Operands.Add(new BinaryOperator("FchAtncn", apl.Log.Fecha.FechaInicial(FchIncl), BinaryOperatorType.GreaterOrEqual));
            fil.Operands.Add(new BinaryOperator("FchAtncn", apl.Log.Fecha.FechaFinal(FchFnl), BinaryOperatorType.LessOrEqual));
             
            /*
            fil.Operands.Add(new BinaryOperator("FchSolctd", apl.Log.Fecha.FechaInicial(FchIncl), BinaryOperatorType.GreaterOrEqual));
            fil.Operands.Add(new BinaryOperator("FchSolctd", apl.Log.Fecha.FechaFinal(FchFnl), BinaryOperatorType.LessOrEqual));*/

            return fil;
        }
        public override SortProperty[] GetSorting()
        {
            /*
            SortProperty[] sorting = { new SortProperty("MyPropertyName", SortingDirection.Descending) };
            return sorting;*/
            return null;
        }

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public string Folio { get; set; }

        public StsSrvc Status { get; set; }

        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        [XafDisplayName("Fecha Inicial")]
        public DateTime FchIncl { get; set; }

        [ModelDefault("DisplayFormat", "{0:dd MMM yyyy}")]
        [XafDisplayName("Fecha Final")]
        public DateTime FchFnl { get; set; }
    }
}
