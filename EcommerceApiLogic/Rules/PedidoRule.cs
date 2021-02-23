using DbManagerDark.Exceptions;
using DbManagerDark.Managers;
using EcommerceApiLogic.Models;
using EcommerceApiLogic.ModelsSap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EcommerceApiLogic.Rules
{
    public class PedidoRule
    {
        private DarkDev darkDev;
        private Cliente Cliente;
        private SocioNegocio SocioNegocio;

        public PedidoRule(DarkDev darkDev, int IdCliente)
        {
            this.darkDev = darkDev;

            darkDev.LoadObject(MysqlObject.Cliente);
            darkDev.LoadObject(MysqlObject.Pedido);
            darkDev.LoadObject(MysqlObject.DetallePedido);
            darkDev.LoadObject(MysqlObject.ProductoFijo);

            darkDev.LoadObject(SSQLObject.SocioNegocio);
            darkDev.LoadObject(SSQLObject.TipoCambio);

            Cliente = darkDev.Cliente.Get(IdCliente);
            SocioNegocio = darkDev.SocioNegocio.Get(Cliente.CodigoCliente);
        }
        /// <summary>
        /// Agregar a carrito, creando nuevo carrito
        /// </summary>
        /// <param name="idPedido"></param>
        /// <param name="CodigoProducto"></param>
        /// <param name="cantidad"></param>
        /// <returns></returns>
        public int AddCarrito(int idPedido, string CodigoProducto, int cantidad)
        {
            try
            {
                darkDev.StartTransaction();

                if(string.IsNullOrEmpty(CodigoProducto))
                    throw new DarkExceptionUser(string.Format("Por favor selecciona un producto"));
                if (cantidad == 0)
                    throw new DarkExceptionUser(string.Format("Por favor selecciona una cantidad mayor a cero"));

                var TC_result = darkDev.TipoCambio.Get(
                        darkDev.TipoCambio.ColumName(nameof(darkDev.TipoCambio.Element.Currency)), "USD",
                        darkDev.TipoCambio.ColumName(nameof(darkDev.TipoCambio.Element.RateDate)), DateTime.Now.ToString("yyyy-MM-dd")
                    );

                if (TC_result == null)
                    throw new DarkExceptionUser(string.Format("El tipo de cambio no ha sido definido"));

                if (TC_result.Rate <= 0)
                    throw new DarkExceptionUser(string.Format("El tipo de cambio no puede ser cero"));

                if (idPedido == 0)
                    idPedido = AddNuevaCotizacion(TC_result.Rate);

                var Result = darkDev.Pedido.Get(idPedido);

                if(Result.IdCliente != Cliente.IdCliente)
                    throw new DarkExceptionUser(string.Format("No puedes agregar más productos, restriccion de datos"));


                if (Result == null)
                    throw new DarkExceptionUser(string.Format("El carrito número {0} no fue encontrado", idPedido));
                //agregar articulo al carrito
                AddNewLine(CodigoProducto, cantidad, Result.IdPedido);
                //actualizar carrito
                UpdateCarrito(Result.IdPedido);
                darkDev.Commit();
                return Result.IdPedido;
            }
            catch (DarkExceptionSystem ex)
            {
                darkDev.RolBack();
                throw ex;
            }
            catch (DarkExceptionUser ex)
            {
                darkDev.RolBack();
                throw ex;
            }
            catch (Exception ex)
            {
                darkDev.RolBack();
                throw new DarkExceptionUser(ex.Message);
            }
            finally
            {
                
                darkDev.CloseConnection();
            }

        }
        /// <summary>
        /// Crear nueva cotizacione
        /// </summary>
        /// <param name="TipoCambio"></param>
        /// <returns></returns>
        private int AddNuevaCotizacion(float TipoCambio)
        {
            darkDev.Pedido.Element = new Pedido();
            darkDev.Pedido.Element.IdPedido = 0;
            darkDev.Pedido.Element.IdCliente = Cliente.IdCliente;
            darkDev.Pedido.Element.SubTotal = 0; // float
            darkDev.Pedido.Element.Iva = 0; // float
            darkDev.Pedido.Element.Total = 0; // float
            darkDev.Pedido.Element.Fecha = DateTime.Now;
            darkDev.Pedido.Element.Activo = "si";
            darkDev.Pedido.Element.Estatus = "c";
            darkDev.Pedido.Element.MetodoPago = "";
            darkDev.Pedido.Element.MonedaPago = "";
            darkDev.Pedido.Element.DatosEnvio = "";
            darkDev.Pedido.Element.DatosFacturacion = "";
            darkDev.Pedido.Element.NumeroGuia = "";
            darkDev.Pedido.Element.Paqueteria = "";
            darkDev.Pedido.Element.TipoCambio = TipoCambio; // float

            if(Cliente.TipoCliente.Trim() == "B2B")
            {
                darkDev.Pedido.Element.DiasCreditoExtra = Int32.Parse(SocioNegocio.ExtraDays);
            }
            else
            {
                darkDev.Pedido.Element.DiasCreditoExtra = 0;
            }
            
            darkDev.Pedido.Element.UsoCFDI = "";
            darkDev.Pedido.Element.Aprobada = 0;
            darkDev.Pedido.Element.NumGuiaEstatus = "";
            darkDev.Pedido.Element.PaqueteriaDescripcion = "";
            darkDev.Pedido.Element.FechaReciboPaquete = "";
            darkDev.Pedido.Element.NombreRecibio = "";
            darkDev.Pedido.Element.IpSource = "";
            darkDev.Pedido.Element.TipoPedido = "Normal";
            darkDev.Pedido.Element.EstatusPuntos = 0;

            if (!darkDev.Pedido.Add())
            {
                throw new DarkExceptionUser(string.Format("Error al crear el nuevo carrito, {0}", darkDev.Pedido.GetLastMessage()));
            }

            return darkDev.Pedido.GetLastId(darkDev.Pedido.ColumName(nameof(darkDev.Pedido.Element.IdCliente)), Cliente.IdCliente + "");
        }
        /// <summary>
        /// Agregar nuevo producto a carrito
        /// </summary>
        /// <param name="ProductoCode"></param>
        /// <param name="cantidad"></param>
        /// <param name="IdPedido"></param>
        private void AddNewLine(string ProductoCode,  int cantidad, int IdPedido)
        {
            var Art_result = darkDev.ProductoFijo.Get(ProductoCode);

            if(Art_result == null)
            {
                throw new DarkExceptionUser(string.Format("Error, no fue encontrado el articulo: {0}", ProductoCode));
            }
            //precio real del articulo con descuento
            float precioReal = 0;
            if(Art_result.Descuento > 0)
            {
                precioReal = Art_result.Precio - (Art_result.Precio * (Art_result.Descuento / 100));
            }
            else
            {
                Art_result.Descuento = 0;
                precioReal = Art_result.Precio;
            }

            //verificar si ya existe el producto en el carrito
            var DetalleResult = darkDev.DetallePedido.Get(
                        darkDev.DetallePedido.ColumName(nameof(darkDev.DetallePedido.Element.IdPedido)), IdPedido + "",
                        darkDev.DetallePedido.ColumName(nameof(darkDev.DetallePedido.Element.CodigoProducto)), ProductoCode
                    );

            if (DetalleResult != null)
            {
                darkDev.DetallePedido.Element = DetalleResult;

                if (darkDev.DetallePedido.Element.Activo == "no")
                {
                    darkDev.DetallePedido.Element.Cantidad = 0;
                }

                darkDev.DetallePedido.Element.Cantidad += cantidad;
                darkDev.DetallePedido.Element.Descuento = Art_result.Descuento;
                darkDev.DetallePedido.Element.SubTotal = darkDev.DetallePedido.Element.Cantidad * precioReal;
                darkDev.DetallePedido.Element.SubTotalSinDesc = Art_result.Precio * darkDev.DetallePedido.Element.Cantidad;
                darkDev.DetallePedido.Element.Iva = 0.16f * darkDev.DetallePedido.Element.SubTotal;
                darkDev.DetallePedido.Element.Total = darkDev.DetallePedido.Element.SubTotal + darkDev.DetallePedido.Element.Iva;
                darkDev.DetallePedido.Element.FechaRegistro = DateTime.Now;
                darkDev.DetallePedido.Element.Activo = "si";
                darkDev.DetallePedido.Element.CodigoConfigurable = Art_result.CodigoConfigurable;
                if (!darkDev.DetallePedido.Update())
                {
                    throw new DarkExceptionUser(string.Format("Error, no fue agregado a tu carrito el articulo: {0}", ProductoCode));
                }
            }
            else
            {
                darkDev.DetallePedido.Element = new DetallePedido();
                darkDev.DetallePedido.Element.IdDetalle = 0;
                darkDev.DetallePedido.Element.IdPedido = IdPedido;
                darkDev.DetallePedido.Element.CodigoProducto = ProductoCode;
                darkDev.DetallePedido.Element.Cantidad = cantidad;
                darkDev.DetallePedido.Element.Descuento = Art_result.Descuento;
                darkDev.DetallePedido.Element.SubTotal = darkDev.DetallePedido.Element.Cantidad * precioReal;
                darkDev.DetallePedido.Element.SubTotalSinDesc = Art_result.Precio * darkDev.DetallePedido.Element.Cantidad;
                darkDev.DetallePedido.Element.Iva = 0.16f * darkDev.DetallePedido.Element.SubTotal;
                darkDev.DetallePedido.Element.Total = darkDev.DetallePedido.Element.SubTotal + darkDev.DetallePedido.Element.Iva;
                darkDev.DetallePedido.Element.CodigoConfigurable = Art_result.CodigoConfigurable;
                darkDev.DetallePedido.Element.Activo = "si";
                //darkDev.DetallePedido.Element.FechaRegistro = DateTime.Now.TimeOfDay;
                darkDev.DetallePedido.Element.FechaRegistro = DateTime.Now;

                if (!darkDev.DetallePedido.Add())
                {
                    throw new DarkExceptionUser(string.Format("Error, no fue agregado a tu carrito el articulo: {0}", ProductoCode));
                }
            }
        }
        /// <summary>
        /// Actualizar carrito
        /// </summary>
        /// <param name="IdPedido"></param>
        private void UpdateCarrito(int IdPedido)
        {
            var Carrito_re = darkDev.Pedido.Get(IdPedido);
            if(Carrito_re.Estatus.Trim() != "c")
            {
                throw new DarkExceptionUser(string.Format("No se puede actualizar este pedido, estatus actual: pedido confirmado", IdPedido));
            }

            var CarritoDetalle_re = darkDev.DetallePedido.Get(
                        IdPedido + "", darkDev.DetallePedido.ColumName(nameof(darkDev.DetallePedido.Element.IdPedido))
                    ).Where(a => a.Activo.Trim() == "si");
            Carrito_re.SubTotal = CarritoDetalle_re.Sum(a => a.SubTotal);
            Carrito_re.Iva = CarritoDetalle_re.Sum(a => a.Iva);
            Carrito_re.Total = CarritoDetalle_re.Sum(a => a.Total);

            darkDev.Pedido.Element = Carrito_re;

            if (!darkDev.Pedido.Update())
            {
                throw new DarkExceptionUser(string.Format("Error, al actualizar el carrito: {0}", IdPedido));
            }

        }
        /// <summary>
        /// Listar pedidos
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public List<Pedido> GetPedidos(string tipo)
        {
            var TC_result = darkDev.Pedido.GetList(
                        darkDev.Pedido.ColumName(nameof(darkDev.Pedido.Element.IdCliente)), Cliente.IdCliente+"",
                        darkDev.Pedido.ColumName(nameof(darkDev.Pedido.Element.Estatus)), tipo
                    );

            return TC_result.OrderByDescending(a => a.Fecha).ToList();
        }
        /// <summary>
        /// Obtener pedido
        /// </summary>
        /// <param name="IdPedido"></param>
        /// <returns></returns>
        public Pedido GetPedido(int IdPedido)
        {
            var DetalleResult = darkDev.Pedido.Get(
                        darkDev.Pedido.ColumName(nameof(darkDev.Pedido.Element.IdPedido)), IdPedido + "",
                        darkDev.Pedido.ColumName(nameof(darkDev.Pedido.Element.IdCliente)), Cliente.IdCliente + ""
                    );
            if(DetalleResult == null)
            {
                throw new DarkExceptionUser(string.Format("Error, no fue encontrada  la orden: {0}", IdPedido));
            }
            DetalleResult.DetallePedidos = darkDev.DetallePedido.Get(
                         DetalleResult.IdPedido + "", darkDev.DetallePedido.ColumName(nameof(darkDev.DetallePedido.Element.IdPedido))
                    ).Where(a => a.Activo == "si").ToList();
            return DetalleResult;
        }
        /// <summary>
        /// elimar producto del carrito seleccionado
        /// </summary>
        /// <param name="ProductoCode"></param>
        /// <param name="IdPedido"></param>
        public void EliminarProducto(string ProductoCode, int IdPedido)
        {
            try
            {
                darkDev.StartTransaction();

                var Pedido_re = darkDev.Pedido.Get(IdPedido);
                if (Pedido_re.IdCliente != Cliente.IdCliente)
                    throw new DarkExceptionUser(string.Format("No puedes modificar, restriccion de datos, el pedido no te corresonde"));

                var CarritoDetalle_re = darkDev.DetallePedido.Get(
                            darkDev.DetallePedido.ColumName(nameof(darkDev.DetallePedido.Element.IdPedido)), IdPedido + "",
                            darkDev.DetallePedido.ColumName(nameof(darkDev.DetallePedido.Element.CodigoProducto)), ProductoCode + ""
                        );
                if (CarritoDetalle_re == null)
                    throw new DarkExceptionUser(string.Format("No fue encontrado en el carrito el articulo: {0}", ProductoCode));

                darkDev.DetallePedido.Element = CarritoDetalle_re;
                darkDev.DetallePedido.Element.Activo = "no";

                if (!darkDev.DetallePedido.Update())
                {
                    throw new DarkExceptionUser(string.Format("Error, no fue removido el articulo: {0}", ProductoCode));
                }

                UpdateCarrito(IdPedido);

                darkDev.Commit();
            }
            catch (DarkExceptionSystem ex)
            {
                darkDev.RolBack();
                throw ex;
            }
            catch (DarkExceptionUser ex)
            {
                darkDev.RolBack();
                throw ex;
            }
            catch (Exception ex)
            {
                darkDev.RolBack();
                throw new DarkExceptionUser(ex.Message);
            }
            finally
            {

                darkDev.CloseConnection();
            }
        }
        /// <summary>
        /// cambiar cantidad de carrito
        /// </summary>
        /// <param name="ProductoCode">Codigo del producto</param>
        /// <param name="cantidad">Nueva cantidad</param>
        /// <param name="IdPedido">Numero de pedido e-commerce</param>
        public void CambiarCantidad(string ProductoCode, int cantidad, int IdPedido)
        {
            try
            {
                darkDev.StartTransaction();

                var Pedido_re = darkDev.Pedido.Get(IdPedido);
                if (Pedido_re.IdCliente != Cliente.IdCliente)
                    throw new DarkExceptionUser(string.Format("No puedes modificar, restriccion de datos, el pedido no te corresonde"));

                var CarritoDetalle_re = darkDev.DetallePedido.Get(
                            darkDev.DetallePedido.ColumName(nameof(darkDev.DetallePedido.Element.IdPedido)), IdPedido + "",
                            darkDev.DetallePedido.ColumName(nameof(darkDev.DetallePedido.Element.CodigoProducto)), ProductoCode + ""
                        );
                if (CarritoDetalle_re == null)
                    throw new DarkExceptionUser(string.Format("No fue encontrado en el carrito el articulo: {0}", ProductoCode));

                if (CarritoDetalle_re.Activo == "no")
                    throw new DarkExceptionUser(string.Format("El articulo: {0} fue eliminado anteriormente", ProductoCode));

                var Art_result = darkDev.ProductoFijo.Get(ProductoCode);

                if (Art_result == null)
                    throw new DarkExceptionUser(string.Format("Error, no fue encontrado el articulo: {0}", ProductoCode));

                //precio real del articulo con descuento
                float precioReal = 0;
                if (Art_result.Descuento > 0)
                {
                    precioReal = Art_result.Precio - (Art_result.Precio * (Art_result.Descuento / 100));
                }
                else
                {
                    Art_result.Descuento = 0;
                    precioReal = Art_result.Precio;
                }

                darkDev.DetallePedido.Element = CarritoDetalle_re;
                darkDev.DetallePedido.Element.Cantidad = cantidad;
                darkDev.DetallePedido.Element.Descuento = Art_result.Descuento;
                darkDev.DetallePedido.Element.SubTotal = darkDev.DetallePedido.Element.Cantidad * precioReal;
                darkDev.DetallePedido.Element.SubTotalSinDesc = Art_result.Precio * darkDev.DetallePedido.Element.Cantidad;
                darkDev.DetallePedido.Element.Iva = 0.16f * darkDev.DetallePedido.Element.SubTotal;
                darkDev.DetallePedido.Element.Total = darkDev.DetallePedido.Element.SubTotal + darkDev.DetallePedido.Element.Iva;
                darkDev.DetallePedido.Element.FechaRegistro = DateTime.Now;
                darkDev.DetallePedido.Element.Activo = "si";
                darkDev.DetallePedido.Element.CodigoConfigurable = Art_result.CodigoConfigurable;

                if (!darkDev.DetallePedido.Update())
                {
                    throw new DarkExceptionUser(string.Format("Error, no fue actualizado el articulo: {0}", ProductoCode));
                }

                UpdateCarrito(IdPedido);

                darkDev.Commit();
            }
            catch (DarkExceptionSystem ex)
            {
                darkDev.RolBack();
                throw ex;
            }
            catch (DarkExceptionUser ex)
            {
                darkDev.RolBack();
                throw ex;
            }
            catch (Exception ex)
            {
                darkDev.RolBack();
                throw new DarkExceptionUser(ex.Message);
            }
            finally
            {

                darkDev.CloseConnection();
            }
        }
    }
}
