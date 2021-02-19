using System;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Updating;
using System.Collections.Generic;
using Cap.Bancos.BusinessObjects;

namespace Cap.Bancos.DatabaseUpdate
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppUpdatingModuleUpdatertopic
    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion)
        {
        }
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();
            //string name = "MyName";
            //DomainObject1 theObject = ObjectSpace.FindObject<DomainObject1>(CriteriaOperator.Parse("Name=?", name));
            //if(theObject == null) {
            //    theObject = ObjectSpace.CreateObject<DomainObject1>();
            //    theObject.Name = name;
            //}

            Datos();

            ObjectSpace.CommitChanges(); //This line persists created object(s).
        }
        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
            if (CurrentDBVersion <= new Version("1.0.7305.27014")
                && CurrentDBVersion > new Version("0.0.0.0"))
            {
                if (((DevExpress.ExpressApp.Xpo.XPObjectSpace)this.ObjectSpace).Connection.ConnectionString.Contains("postgres"))
                {
                    ExecuteNonQueryCommand(
                        "alter table \"Bancaria\" alter column \"Url\" Type character varying(50)", true);
                }
                else
                {
                    ExecuteNonQueryCommand(
                        "alter table Bancaria alter column Url nvarchar(50)", false);
                }
            }
        }

        private void Datos()
        {
            // Conceptos Bancarios
            List<string> ceptosB = new List<string>();

            ceptosB.Add("A.AFOREREND|DEPÓSITOS DE AFORE, RENDIMIENTOS|Abono");
            ceptosB.Add("A.AFOREVOL|APORTACIONES VOLUNTARIAS|Abono");
            ceptosB.Add("A.INTRS|INTERESES|Abono");
            ceptosB.Add("A.MENSUAL|MENSUALIDAD|Abono");
            ceptosB.Add("A.PRES|PRESTAMOS|Abono");
            ceptosB.Add("A.SUELD|SUELDO|Abono");
            ceptosB.Add("A.SLDI|SALDO INICIAL|Abono");
            ceptosB.Add("A.TR|TRASPASO ABONO|Abono");
            ceptosB.Add("C.AFORE|COMISIONES|Cargo");
            ceptosB.Add("C.C.A|AGUA|Cargo");
            ceptosB.Add("C.C.L|LUZ|Cargo");
            ceptosB.Add("C.C.T|TELEFONO|Cargo");
            ceptosB.Add("C.COMIDA|COMIDA, REFRESCOS, PASTELES, DULCES|Cargo");
            ceptosB.Add("C.D|DESPENSA|Cargo");
            ceptosB.Add("C.D.V|VIVERES|Cargo");
            ceptosB.Add("C.ENSERES|CAJAS|Cargo");
            ceptosB.Add("C.ESC.COL|COLEGIATURAS|Cargo");
            ceptosB.Add("C.HERRAM|HERRAMIENTAS, EN GENERAL|Cargo");
            ceptosB.Add("C.MANTENIMIENTO|PARA DAR MANTENIMIENTO A LA CASA|Cargo");
            ceptosB.Add("C.MEDICAMENTOS|MEDICINAS EN GENERAL|Cargo");
            ceptosB.Add("C.R|ROPA|Cargo");
            ceptosB.Add("C.RETIROCAJA|RETIRO EN CAJA|Cargo");
            ceptosB.Add("C.T|PAGO DE TARJETA|Cargo");
            ceptosB.Add("C.TR|TRASPASO CARGO|Cargo");
            ceptosB.Add("C.TRANS|TRANSPORTE, PECERO, METRO.|Cargo");

            foreach (string ob in ceptosB)
            {
                ConceptoB cpt;
                string[] arrtoks = ob.Split('|');

                cpt = ObjectSpace.FindObject<ConceptoB>(CriteriaOperator.Parse(string.Format("Clave == '{0}'", arrtoks[0])));
                if (cpt == null)
                {
                    cpt = ObjectSpace.CreateObject<ConceptoB>();
                    cpt.Clave = arrtoks[0];
                    cpt.Descripcion = arrtoks[1];
                    cpt.Tipo = arrtoks[2] == "Abono" ? EConceptoTipo.Abono : EConceptoTipo.Cargo;
                    cpt.Save();
                }
            }

           
            if (ObjectSpace.FindObject<Presupuesto>(null) == null)
            {
                Presupuesto prs = ObjectSpace.CreateObject<Presupuesto>();
                prs.Save();
            }
        }
    }
}
