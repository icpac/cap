using System;
using DevExpress.ExpressApp;
using Cap.Bancos.BusinessObjects;
using System.Collections;
using System.Collections.Generic;
using DevExpress.Data.Filtering;
using System.Linq;

namespace Cap.Bancos.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class VCPresupuesto : ViewController
    {
        public VCPresupuesto()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(Presupuesto);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void VCPresupuesto_ViewControlsCreated(object sender, EventArgs e)
        {
            if (View != null)
            {
                Presupuesto obj = View.CurrentObject as Presupuesto;

                if (obj != null)
                {
                    obj.Partidas.ListChanged += Partidas_ListChanged;
                }
            }
        }

        void Partidas_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            if (View != null && View.ObjectSpace != null && !View.ObjectSpace.IsCommitting)
            {
                Presupuesto obj = View.CurrentObject as Presupuesto;

                obj.Total = obj.Partidas.Count;
            }
        }

        private void popupWindowShowActionPresupuestoReal_CustomizePopupWindowParams(object sender, DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            PresupuestoFiltro pf = objectSpace.CreateObject<PresupuestoFiltro>();

            e.View = Application.CreateDetailView(objectSpace, "PresupuestoFiltro_DetailView", true, pf);
        }

        private void popupWindowShowActionPresupuestoReal_Execute(object sender, DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventArgs e)
        {
            PresupuestoFiltro obj = e.PopupWindowViewCurrentObject as PresupuestoFiltro;

            if (obj != null)
            {
                IObjectSpace objectSpace = Application.CreateObjectSpace();
                List<MovimientoB> lista;
                GroupOperator fil = new GroupOperator();
                Presupuesto prsu = objectSpace.FindObject<Presupuesto>(null);

                if (prsu != null)
                {
                    decimal suma;
                    foreach (ConceptoP cp in prsu.Partidas)
                    {
                        if (cp.Concepto != null)
                        {
                            fil.Operands.Clear();
                            fil.Operands.Add(CriteriaOperator.And(new BinaryOperator("FechaAplicacion", obj.FchIncl, BinaryOperatorType.GreaterOrEqual),
                                new BinaryOperator("FechaAplicacion", obj.FchFnl, BinaryOperatorType.LessOrEqual)));
                            fil.Operands.Add(new BinaryOperator("Concepto", cp.Concepto));

                            IList col = objectSpace.CreateCollection(typeof(MovimientoB), fil);
                            lista = col.Cast<MovimientoB>().ToList();
                            if (lista != null && lista.Count > 0)
                            {
                                suma = lista.Sum(item => item.Monto);
                                cp.MntRl = suma;
                            }
                            else
                                cp.MntRl = 0;
                        }
                    }

                    IList lis = objectSpace.CreateCollection(typeof(ConceptoB));
                    foreach(ConceptoB cpt in lis)
                    {
                        prsu.Partidas.Filter = new BinaryOperator("Concepto", cpt);
                        if (prsu.Partidas.Count == 0)
                        {
                            fil.Operands.Clear();
                            fil.Operands.Add(CriteriaOperator.And(new BinaryOperator("FechaAplicacion", obj.FchIncl, BinaryOperatorType.GreaterOrEqual),
                                new BinaryOperator("FechaAplicacion", obj.FchFnl, BinaryOperatorType.LessOrEqual)));
                            fil.Operands.Add(new BinaryOperator("Concepto", cpt));

                            IList col = objectSpace.CreateCollection(typeof(MovimientoB), fil);
                            lista = col.Cast<MovimientoB>().ToList();
                            if (lista != null && lista.Count > 0)
                            {
                                suma = lista.Sum(item => item.Monto);

                                ConceptoP cp = objectSpace.CreateObject<ConceptoP>();
                                cp.Concepto = cpt;
                                cp.MntRl = suma;

                                prsu.Partidas.Add(cp);
                            }
                        }
                    }

                    objectSpace.CommitChanges();
                    View.Refresh(true);
                }
            }
        }

        private void simpleActionACrs_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            if (View != null && View.CurrentObject != null)
            {
                Presupuesto prsu = View.CurrentObject as Presupuesto;

                if (prsu != null)
                {
                    foreach (ConceptoP cp in prsu.Partidas)
                    {
                        cp.MntRl = 0;
                    }
                    View.ObjectSpace.CommitChanges();
                    View.Refresh(true);
                }
            }
        }
    }
}
