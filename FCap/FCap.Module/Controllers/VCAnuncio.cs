using System;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.ExpressApp.Actions;
using FCap.Module.Utilerias;
using Cap.Generales.BusinessObjects.Empresa;
using System.IO;
using Cap.Generales.BusinessObjects.Object;
using Cap.Generales.BusinessObjects.General;
using Cap.Clientes.BusinessObjects.Clientes;
using Cap.Clientes.BusinessObjects.Generales;

namespace FCap.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class VCAnuncio : ViewController
    {
        public VCAnuncio()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.

            TargetObjectType = typeof(Anuncio);
            TargetViewType = ViewType.ListView;
            
            /* TIT Jun 2020 Para qué sirve ?
            TargetViewNesting = Nesting.Root;*/

        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            SendAction.TargetObjectType = typeof(Anuncio);
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

        string ParamValue;
        private void SendAction_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {
            ParamValue = e.ParameterCurrentValue as string;
            /*
            Thread HiloImpar = new Thread(EnviaC);
            HiloImpar.IsBackground = true;
            HiloImpar.Start();
            HiloImpar.Join();*/
            EnviaC();

            /*
            Anuncio obj = View.CurrentObject as Anuncio;
            Correo cfg = obj.Session.FindObject<Correo>(null); // (new BinaryOperator("Clave", "icpac"));
            if (obj != null && cfg != null)
            {
                CorreoBase objCorreo = View.ObjectSpace.CreateObject<CorreoBase>(); // View.ObjectSpace.FindObject<Correo>(null); // (CriteriaOperator.Parse("Clave = 'anuncio'"));

                / *
                if (objCorreo == null)
                {
                    objCorreo = View.ObjectSpace.CreateObject<Correo>();
                    objCorreo.Clave = "anuncio";
                }* /
                objCorreo.Cuenta = cfg.Cuenta;
                objCorreo.Identificdr = cfg.Identificdr;
                objCorreo.Passw = cfg.Passw;
                objCorreo.Puerto = cfg.Puerto;
                objCorreo.SegurdSSL = cfg.SegurdSSL;
                objCorreo.ServidorSMTP = cfg.ServidorSMTP;
                objCorreo.Usuario = cfg.Usuario;

                objCorreo.Asunto = string.Format("Anuncio iCPAC");
                objCorreo.Mensaje1 = obj.Mensaje;

                //CorreoSend.MandaCorreo(cfg, "tlacaelel.icpac@gmail.com", obj.Mensaje, "iCPAC");

                string paramValue = e.ParameterCurrentValue as string;
                if (!string.IsNullOrEmpty(paramValue))
                {
                    IObjectSpace objectSpace = Application.CreateObjectSpace();
                    Cliente cli = objectSpace.FindObject<Cliente>(new BinaryOperator("Clave", Cliente.ClaveFto(paramValue)));

                    if (cli != null && cli.Compania != null && cli.Compania.Direccion != null &&
                        !string.IsNullOrEmpty(cli.Compania.Direccion.Email))
                        CorreoSend.MandaCorreo(objCorreo, cli.Compania.Direccion.Email, objCorreo.Asunto, "iCPAC");
                }
                else
                {
                    IObjectSpace objectSpace = Application.CreateObjectSpace();
                    IList<Cliente> clientes = objectSpace.GetObjects<Cliente>(new BinaryOperator("Status", StatusTipo.Activo));

                    foreach (Cliente cli in clientes)
                    {
                        if (cli != null && cli.Compania != null && cli.Compania.Direccion != null &&
                            !string.IsNullOrEmpty(cli.Compania.Direccion.Email))
                        {
                            if (cli.Status != StatusTipo.Suspendido)
                                CorreoSend.MandaCorreo(objCorreo, cli.Compania.Direccion.Email, objCorreo.Asunto, "iCPAC");
                        }
                    }
                }
                objCorreo.Mensaje1 =
                objCorreo.Mensaje2 = string.Empty;
            }*/
        }

        private void EnviaC()
        {
            Anuncio obj = View.CurrentObject as Anuncio;
            Correo cfg = obj.Session.FindObject<Correo>(null);
            Empresa emp = obj.Session.FindObject<Empresa>(null);

            if (obj != null && cfg != null)
            {
                CorreoBase objCorreo = View.ObjectSpace.CreateObject<CorreoBase>(); 
                
                objCorreo.Cuenta = cfg.Cuenta;
                objCorreo.Identificdr = cfg.Identificdr;
                objCorreo.Passw = cfg.Passw;
                objCorreo.Puerto = cfg.Puerto;
                objCorreo.SegurdSSL = cfg.SegurdSSL;
                objCorreo.ServidorSMTP = cfg.ServidorSMTP;
                objCorreo.Usuario = cfg.Usuario;

                objCorreo.Asunto = "Anuncio iCPAC";
                objCorreo.Mensaje1 = obj.Mensaje;

                //CorreoSend.MandaCorreo(cfg, "tlacaelel.icpac@gmail.com", obj.Mensaje, "iCPAC");
                MemoryStream mLogo = null;
                if (emp != null && emp.Logo != null)
                {
                    JpegStorageConverter jpeg = new JpegStorageConverter();

                    Byte[] arr = jpeg.ConvertToStorageType(emp.Logo) as Byte[];
                    mLogo = new MemoryStream(arr);
                }

                // string paramValue = e.ParameterCurrentValue as string;
                CorreoSend.Client(objCorreo);
                // CorreoSend.MailMssg(objCorreo, objCorreo.Asunto, "iCPAC",  mLogo);
                if (!string.IsNullOrEmpty(ParamValue))
                {
                    IObjectSpace objectSpace = Application.CreateObjectSpace();
                    Cliente cli = objectSpace.FindObject<Cliente>(new BinaryOperator("Clave", Cliente.ClaveFto(ParamValue)));

                    List<string> arRcpt = new List<string>();
                    if (cli != null && cli.Compania != null && cli.Compania.Direccion != null &&
                        !string.IsNullOrEmpty(cli.Compania.Direccion.Email))
                    {
                        objCorreo.Mensaje1 = string.Format("{0}{1} <br/> <br />{2}",
                            !string.IsNullOrEmpty(cli.Als) ? cli.Als : string.Empty, Environment.NewLine, obj.Mensaje);

                        CorreoSend.MailMssg(objCorreo, objCorreo.Asunto, "iCPAC", mLogo);
                        arRcpt.Add(cli.Compania.Direccion.Email);
                        CorreoSend.MandaCorreo2(objCorreo, arRcpt);
                    }
                }
                else
                {
                    IObjectSpace objectSpace = Application.CreateObjectSpace();
                    IList<Cliente> clientes = objectSpace.GetObjects<Cliente>
                        (new BinaryOperator("Status", EStatusPrvdClnt/*StatusTipo*/.Activo));


                    List<string> arRcpt = new List<string>();
                    foreach (Cliente cli in clientes)
                    {
                        if (cli != null && cli.Compania != null && cli.Compania.Direccion != null &&
                            !string.IsNullOrEmpty(cli.Compania.Direccion.Email))
                        {
                            if (cli.Status != Cap.Clientes.BusinessObjects.Generales.EStatusPrvdClnt.Suspendido /* Cap.Clientes.BusinessObjects.Generales.StatusTipo.Suspendido*/)
                            {
                                // CorreoSend.MandaCorreo2(objCorreo, cli.Compania.Direccion.Email);
                                if (!string.IsNullOrEmpty(cli.Als))
                                    objCorreo.Mensaje1 = string.Format("{0}{1} <br/> <br />{2}", cli.Als, Environment.NewLine, obj.Mensaje);
                                else
                                    objCorreo.Mensaje1 = obj.Mensaje;

                                arRcpt.Add(cli.Compania.Direccion.Email);

                                CorreoSend.MsgAltClear();
                                CorreoSend.MailMssg(objCorreo, objCorreo.Asunto, "iCPAC", mLogo);
                                CorreoSend.MandaCorreo2(objCorreo, /*cli.Compania.Direccion.Email*/arRcpt);

                                arRcpt.Clear();
                            }
                        }
                    }

                    /*
                    if (arRcpt.Count > 0)
                        CorreoSend.MandaCorreo2(objCorreo, / *cli.Compania.Direccion.Email* /arRcpt);*/

                }
                objCorreo.Mensaje1 =
                objCorreo.Mensaje2 = string.Empty;

                if (mLogo != null)
                    mLogo.Dispose();
            }
        }

        private void popupWindowShowActionSnd_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            AnuncioFiltro fil = objectSpace.FindObject<AnuncioFiltro>(null);

            if (fil == null)
                fil = objectSpace.CreateObject<AnuncioFiltro>();

            e.View = Application.CreateDetailView(objectSpace, "AnuncioFiltro_DetailView", true, fil);
        }

        private void popupWindowShowActionSnd_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            AnuncioFiltro fil = (AnuncioFiltro)e.PopupWindowViewCurrentObject;


            if (fil != null)
            {
                IObjectSpace objectSpace = Application.CreateObjectSpace();
                IList<Cliente> clientes;
                
                
                GroupOperator gfil = new GroupOperator();

                gfil.Operands.Add(new BinaryOperator("Compania.Direccion.Email", string.Empty, BinaryOperatorType.NotEqual));
                if (!string.IsNullOrEmpty(fil.Clv))
                    gfil.Operands.Add(new BinaryOperator("Clave", fil.Clv));
                else
                {
                    if (fil.ClntPrspct != null)
                    {
                        CatalogoCliente cp = objectSpace.FindObject<CatalogoCliente>
                            (new BinaryOperator("Oid", fil.ClntPrspct.Oid));
                        gfil.Operands.Add(new BinaryOperator("ClntPrspct", cp));
                    }

                    if (fil.Clsfccn != null)
                    {
                        Clasificacion cl = objectSpace.FindObject<Clasificacion>
                            (new BinaryOperator("Oid", fil.Clsfccn.Oid));
                        gfil.Operands.Add(new BinaryOperator("Clasifica", cl));
                    }

                    if (fil.Stts != null)
                        gfil.Operands.Add(new BinaryOperator("Status", fil.Stts));
                }
                clientes = objectSpace.GetObjects<Cliente>(gfil);

                EnviaC2(clientes, fil.Idntfcdr, fil.Asnt);
            }
        }


        private void EnviaC2(IList<Cliente> clientes, string idntfcdr, string asnt)
        {
            Anuncio obj = View.CurrentObject as Anuncio;
            Correo cfg = obj.Session.FindObject<Correo>(null);
            Empresa emp = obj.Session.FindObject<Empresa>(null);

            if (obj != null && cfg != null)
            {
                CorreoBase objCorreo = View.ObjectSpace.CreateObject<CorreoBase>();

                objCorreo.Cuenta = cfg.Cuenta;
                objCorreo.Identificdr = cfg.Identificdr;
                objCorreo.Passw = cfg.Passw;
                objCorreo.Puerto = cfg.Puerto;
                objCorreo.SegurdSSL = cfg.SegurdSSL;
                objCorreo.ServidorSMTP = cfg.ServidorSMTP;
                objCorreo.Usuario = cfg.Usuario;

                objCorreo.Asunto = idntfcdr; //  "Anuncio iCPAC";
                objCorreo.Mensaje1 = obj.Mensaje;

                //CorreoSend.MandaCorreo(cfg, "tlacaelel.icpac@gmail.com", obj.Mensaje, "iCPAC");
                MemoryStream mLogo = null;
                if (emp != null && emp.Logo != null)
                {
                    JpegStorageConverter jpeg = new JpegStorageConverter();

                    Byte[] arr = jpeg.ConvertToStorageType(emp.Logo) as Byte[];
                    mLogo = new MemoryStream(arr);
                }

                // string paramValue = e.ParameterCurrentValue as string;
                CorreoSend.Client(objCorreo);
                // CorreoSend.MailMssg(objCorreo, objCorreo.Asunto, "iCPAC",  mLogo);
                /*
                if (!string.IsNullOrEmpty(ParamValue))
                {
                    IObjectSpace objectSpace = Application.CreateObjectSpace();
                    Cliente cli = objectSpace.FindObject<Cliente>(new BinaryOperator("Clave", Cliente.ClaveFto(ParamValue)));

                    List<string> arRcpt = new List<string>();
                    if (cli != null && cli.Compania != null && cli.Compania.Direccion != null &&
                        !string.IsNullOrEmpty(cli.Compania.Direccion.Email))
                    {
                        objCorreo.Mensaje1 = string.Format("{0}{1} <br/> <br />{2}",
                            !string.IsNullOrEmpty(cli.Als) ? cli.Als : string.Empty, Environment.NewLine, obj.Mensaje);

                        CorreoSend.MailMssg(objCorreo, objCorreo.Asunto, "iCPAC", mLogo);
                        arRcpt.Add(cli.Compania.Direccion.Email);
                        CorreoSend.MandaCorreo2(objCorreo, arRcpt);
                    }
                }
                else
                {*/
                    IObjectSpace objectSpace = Application.CreateObjectSpace();
                /*
                    IList<Cliente> clientes = objectSpace.GetObjects<Cliente>
                        (new BinaryOperator("Status", EStatusPrvdClnt/ *StatusTipo* /.Activo));*/


                    List<string> arRcpt = new List<string>();
                    foreach (Cliente cli in clientes)
                    {
                        if (cli != null && cli.Compania != null && cli.Compania.Direccion != null &&
                            !string.IsNullOrEmpty(cli.Compania.Direccion.Email))
                        {
                            if (cli.Status != Cap.Clientes.BusinessObjects.Generales.EStatusPrvdClnt.Suspendido /* Cap.Clientes.BusinessObjects.Generales.StatusTipo.Suspendido*/)
                            {
                                // CorreoSend.MandaCorreo2(objCorreo, cli.Compania.Direccion.Email);
                                if (!string.IsNullOrEmpty(cli.Als))
                                    objCorreo.Mensaje1 = string.Format("{0}{1} <br/> <br />{2}", cli.Als, Environment.NewLine, obj.Mensaje);
                                else
                                    objCorreo.Mensaje1 = obj.Mensaje;

                                arRcpt.Add(cli.Compania.Direccion.Email);

                                CorreoSend.MsgAltClear();
                                CorreoSend.MailMssg(objCorreo, objCorreo.Asunto, 
                                    /*"iCPAC"*/asnt, mLogo);
                                CorreoSend.MandaCorreo2(objCorreo, /*cli.Compania.Direccion.Email*/arRcpt);

                                arRcpt.Clear();
                            }
                        }
                    }

                    /*
                    if (arRcpt.Count > 0)
                        CorreoSend.MandaCorreo2(objCorreo, / *cli.Compania.Direccion.Email* /arRcpt);*/

                //}
                objCorreo.Mensaje1 =
                objCorreo.Mensaje2 = string.Empty;

                if (mLogo != null)
                    mLogo.Dispose();
            }
        }

        /* TIT Jun No se puede porque no lo ejecuta
         * cuando le doy clic a la action, lo hace antes 
        private void SendAction_CustomGetFormattedConfirmationMessage(object sender, CustomGetFormattedConfirmationMessageEventArgs e)
        {
            ParametrizedAction act = sender as ParametrizedAction;

            if (act != null)
                e.ConfirmationMessage = $"Está seguro de mandar el Anuncio a:";
        }*/
    }
}
