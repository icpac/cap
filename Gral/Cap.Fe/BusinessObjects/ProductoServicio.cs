using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using Cap.Generales.BusinessObjects.Object;

namespace Cap.Fe.BusinessObjects
{
    [DefaultProperty("DisplayLook")]
    [NavigationItem(("Configuración"))]
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class ProductoServicio : PObject // , IPServicio Very slowww
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public ProductoServicio(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue("PersistentProperty", ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}



        private string mClv;
        [RuleRequiredField("RuleRequiredField for ProductoServicio.Clv", DefaultContexts.Save, "Debe capturar la Clave", SkipNullOrEmptyValues = false)]
        [Indexed]
        // TI Jul 2017 cfdi 3.3 ahora también la leyenda lleva clave parece que el motivo desaparece
        // [Appearance("Pago.Clv", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Criteria = "Tipo != 'FormaPago'")]
        // [Appearance("Pago.Clv", AppearanceItemType = "LayoutItem", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Criteria = "Tipo == 'Motivo'")]
        [Size(10)]
        [XafDisplayName("Clave")]
        public string Clv
        {
            get
            {
                /*
                char []aux = mClv.ToCharArray();
                if (aux.Length == 8 && aux[6] == '0' && aux[7] == '0')*/
                    return mClv;/*
                else
                    return string.Format(" -{0}", mClv);*/
            }
            set { SetPropertyValue("Clv", ref mClv, value); }
        }

        private string mDscrpcn;
        [XafDisplayName("Descripción")]
        [Size(150)]
        public string Dscrpcn
        {
            get
            {
                if (string.IsNullOrEmpty(mClv))
                    return mDscrpcn;
                else
                {
                    char[] aux = mClv.ToCharArray();
                    if (aux.Length == 8 && aux[6] == '0' && aux[7] == '0')
                        return mDscrpcn;
                    else
                        return string.Format("       -{0}", mDscrpcn);
                }
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.TrimStart()[0] == '-')
                    SetPropertyValue("Dscrpcn", ref mDscrpcn, value.TrimStart().Substring(1));
                else
                    SetPropertyValue("Dscrpcn", ref mDscrpcn, value);
            }
        }

        private ETipoProductoServicioFe mTp;
        [XafDisplayName("Tipo")]
        public ETipoProductoServicioFe Tp
        {
            get { return mTp; }
            set { SetPropertyValue("Tp", ref mTp, value); }
        }

        private string mPlbrsSmlrs;
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        [Size(SizeAttribute.Unlimited)]
        [XafDisplayName("Palabras Similares")]
        public string PlbrsSmlrs
        {
            get { return mPlbrsSmlrs; }
            set { SetPropertyValue("PlbrsSmlrs", ref mPlbrsSmlrs, 
                    value != null ? value.Trim() : string.Empty); }
        }

        [VisibleInLookupListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [NonPersistent]
        public string DisplayLook
        {
            get { return string.Format("{0} - {1}", mClv, mDscrpcn); }
        }

        /* TIT Very Slow 
        private ProductoServicio parent;

        [Association("ProductoServicioParent-ProductoServicioChild")]
        public XPCollection<ProductoServicio> Children
        {
            get { return GetCollection<ProductoServicio>("Children"); }
        }

        [Persistent, Association("ProductoServicioParent-ProductoServicioChild")]
        public ProductoServicio Parent
        {
            get { return parent; }
            set { SetPropertyValue("Parent", ref parent, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetPropertyValue("Name", ref name, value); }
        }
        IBindingList ITreeNode.Children
        {
            get { return Children as IBindingList; }
        }
        ITreeNode IPServicio.Parent
        {
            get { return Parent as IPServicio; }
            set { Parent = value as ProductoServicio; }
        }
        ITreeNode ITreeNode.Parent
        {
            get { return Parent as ITreeNode; }
        }*/
    }

    /*
    public interface IPServicio : ITreeNode
    {
        new string Name { get; set; }
        new ITreeNode Parent { get; set; }
    }*/

    public enum ETipoProductoServicioFe
    {
        Producto, 
        Servicio
    }
}