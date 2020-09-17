using DbManagerDark.Exceptions;
using EcommerceApiLogic.Models;
using EcommerceApiLogic.ViewModels;
using Newtonsoft.Json;
using Openpay;
using Openpay.Entities;
using Openpay.Entities.Request;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EcommerceApiLogic.Rules
{
    public class OpenPayRule
    {
        private OpenpayAPI openpayAPI;
        private DarkDev darkDev;
        private Cliente Cliente;
        private ViewPedido Pedido;

        public OpenPayRule(DarkDev darkDev, int IdCliente, int IdPedido)
        {
            this.darkDev = darkDev;

            darkDev.LoadObject(MysqlObject.Cliente);
            darkDev.LoadObject(MysqlObject.OpenPayKeys);
            darkDev.LoadObject(MysqlObject.ViewDetallePedido);
            darkDev.LoadObject(MysqlObject.ViewPedido);
            darkDev.LoadObject(MysqlObject.Pedido);
            darkDev.LoadObject(MysqlObject.SeguimientB2B);
            darkDev.LoadObject(MysqlObject.SeguimientB2C);
            darkDev.LoadObject(MysqlObject.OpenPayLog);

            Cliente = darkDev.Cliente.Get(IdCliente);
            Pedido = darkDev.ViewPedido.Get(IdPedido);

            
        }

        public void CheateChargeCard(RequestPedido requestPedido)
        {
            try
            {
                darkDev.StartTransaction();

                var OpenPayKeys_re = darkDev.OpenPayKeys.Get("1");

                if (Cliente == null)
                    throw new DarkExceptionUser("Cliente no encontrado");

                if (Pedido == null)
                    throw new DarkExceptionUser("Pedido no encontrado");

                if (Pedido.IdCliente != Cliente.IdCliente)
                    throw new DarkExceptionUser("Este pedido no puede ser adquirido por tu cuenta");

                openpayAPI = new OpenpayAPI(OpenPayKeys_re.PrivateKey, OpenPayKeys_re.IdKey);
                openpayAPI.Production = OpenPayKeys_re.ProductionMode.Trim() == "true" ? true : false;

                Customer customer = new Customer
                {
                    Name = Cliente.Nombre,
                    LastName = Cliente.Apellidos,
                    Email = Cliente.Email,
                    PhoneNumber = Cliente.Telefono
                };

                float AmountPurchase;
                if (requestPedido.MonedaPago == "USD")
                    AmountPurchase = Pedido.Total;
                else
                    AmountPurchase = Pedido.TotalMXN;

                if (AmountPurchase <= 0)
                    throw new DarkExceptionUser(string.Format("No se puede hacer un pago por: ${0} {1}", AmountPurchase, Pedido.MonedaPago));


                ChargeRequest request = new ChargeRequest
                {
                    Method = "card",
                    SourceId = requestPedido.TokenId, // token tarjeta
                    Amount = Math.Round(new Decimal(AmountPurchase), 2),
                    Currency = requestPedido.MonedaPago == "USD" ? "USD" : "MXN",
                    Description = "Pago con tarjeta",
                    OrderId = Pedido.IdPedido + "",
                    DeviceSessionId = requestPedido.SessionIdDevice,
                    Customer = customer
                };

                Charge charge = openpayAPI.ChargeService.Create(request);
                ProcessCharge(charge, requestPedido);

                darkDev.Commit();
            }
            catch (OpenpayException ex)
            {
                darkDev.RolBack();
                darkDev.OpenConnection();
                
                HttpStatusCode Code = ex.StatusCode;
                int codee = (int)Code;


                darkDev.OpenPayLog.Element = new OpenPayLog();
                darkDev.OpenPayLog.Element.IdPedido = requestPedido.IdPedido;
                darkDev.OpenPayLog.Element.ErroCode = ex.ErrorCode + "";
                darkDev.OpenPayLog.Element.ErrorHttp = codee + "";
                darkDev.OpenPayLog.Element.Descripcion = ex.Description;
                darkDev.OpenPayLog.Element.DetailsJson = JsonConvert.SerializeObject(new {
                    description = ex.Description,
                    error_code = codee,
                    category = ex.Category,
                    http_code = ex.StatusCode.ToString(),
                    request_id = ex.RequestId

                }); 
                darkDev.OpenPayLog.Element.Update = DateTime.Now;
                darkDev.OpenPayLog.Element.Created = DateTime.Now;

                darkDev.OpenPayLog.Add();

                darkDev.CloseConnection();
                throw new DarkExceptionUser(string.Format("Error al pagar el pedido E-commerce No. {0}", requestPedido.IdPedido));
            }
        }

        private void ProcessCharge(Charge charge, RequestPedido requestPedido)
        {
            if(charge.Status == "completed")
            {
                //update pedido
                var Pedido_re = darkDev.Pedido.Get(Pedido.IdPedido);

                if(Pedido_re == null)
                {
                    throw new DarkExceptionUser(string.Format("No se encontró el pedido E-commerce No. {0}", requestPedido.IdPedido));
                }

                Pedido_re.MetodoPago = "03";
                Pedido_re.MonedaPago = requestPedido.MonedaPago;
                Pedido_re.DatosEnvio = requestPedido.DatosEnvio;
                Pedido_re.DatosFacturacion = requestPedido.DatosFacturacion;
                Pedido_re.Paqueteria = requestPedido.ReferenciasPaqueteria;
                Pedido_re.Estatus = "P";
                darkDev.Pedido.Element = Pedido_re;

                if (!darkDev.Pedido.Update())
                {
                    throw new DarkExceptionUser(string.Format("No se pudo guardar la información acerca de tu pedido E-commerce No. {0}", requestPedido.IdPedido));
                }

                //
                if (Cliente.TipoCliente.Trim() == "B2B")
                {
                    //guardar 
                    var SeguimientoB2B = new SeguimientB2B
                    {
                        IdPedido = requestPedido.IdPedido + "",
                        EstatusPedido = 50,
                        Intentos = 1,
                        OpenPayReferencia = charge.Id,
                        ReferenciaFactura = requestPedido.B2B.ReferenciaDoc,
                        ContactoNombre = requestPedido.B2B.ContactoNombre,
                        ContactoTelefono = requestPedido.B2B.ContactoTelefono,
                        ContactoCorreo = requestPedido.B2B.ContactoCorreo
                    };
                    SeguimientoB2B.ContactoCorreo = requestPedido.B2B.ContactoCorreo;

                    darkDev.SeguimientB2B.Element = SeguimientoB2B;

                    if (!darkDev.SeguimientB2B.Add())
                    {
                        throw new DarkExceptionUser(string.Format("No se pudo guardar la información acerca de tu pedido E-commerce No. {0}", requestPedido.IdPedido));
                    }

                    // enviar correo de confirmacion "Pedido creado"


                }
                else if (Cliente.TipoCliente.Trim() == "B2C")
                {
                    var SeguimientoB2C = new SeguimientB2C
                    {
                        IdPedido = requestPedido.IdPedido + "",
                        OpenPayReferencia = charge.Id,
                        RequiereFactura = requestPedido.B2C.RequiereFactura ? "si" : "no",
                        Estatus = "50",
                        ReferenciaFactura = requestPedido.B2C.ReferenciaDoc,
                        Intentos = 1
                    };

                    darkDev.SeguimientB2C.Element = SeguimientoB2C;

                    if (!darkDev.SeguimientB2C.Add())
                    {
                        throw new DarkExceptionUser(string.Format("No se pudo guardar la información acerca de tu pedido E-commerce No. {0}", requestPedido.IdPedido));
                    }

                    // enviar correo de confirmacion "Pedido creado"
                }
                else
                {
                    throw new DarkExceptionUser(string.Format("Tipo de cliente invalido"));
                }
            }
            else
            {

            }
        }
    }
}
