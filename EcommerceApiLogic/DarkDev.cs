using DbManagerDark;
using DbManagerDark.Managers;
using EcommerceApiLogic.Herramientas;
using EcommerceApiLogic.Models;
using EcommerceApiLogic.ModelsSap;
using EcommerceApiLogic.Validators;
using EcommerceApiLogic.ViewModels;
using Microsoft.Extensions.Configuration;
using System;

namespace EcommerceApiLogic
{
    public class DarkDev : DbManagerDark.DarkManager
    {
        #region Ecommerce web
        public DarkManagerMySQL<Pedido> Pedido { get; set; }
        public DarkManagerMySQL<Usuario> Usuario { get; set; }
        public DarkManagerMySQL<Categoria> Categoria { get; set; }
        public DarkManagerMySQL<SubCategoria> SubCategoria { get; set; }
        public DarkManagerMySQL<ProductoFijo> ProductoFijo { get; set; }
        public DarkManagerMySQL<Cliente> Cliente { get; set; }
        public DarkManagerMySQL<DetallePedido> DetallePedido { get; set; }
        public DarkManagerMySQL<DireccionFacturacion> DireccionFacturacion { get; set; }
        public DarkManagerMySQL<DireccionEnvio> DireccionEnvio { get; set; }
        public DarkManagerMySQL<PedidoB2C> PedidoB2C { get; set; }
        public DarkManagerMySQL<ViewDetallePedido> ViewDetallePedido { get; set; }
        public DarkManagerMySQL<PedidoB2B> PedidoB2B { get; set; }
        public DarkManagerMySQL<ViewPedido> ViewPedido { get; set; }
        public DarkManagerMySQL<OpenPayKeys> OpenPayKeys { get; set; }
        public DarkManagerMySQL<SeguimientB2C> SeguimientB2C { get; set; }
        public DarkManagerMySQL<SeguimientB2B> SeguimientB2B { get; set; }
        public DarkManagerMySQL<OpenPayLog> OpenPayLog { get; set; }
        #endregion

        #region Sap bussines one
        public DarkManagerMSSQL<TipoCambio> TipoCambio { get; set; }
        public SocioNegocio SocioNegocio { get; set; }
        #endregion

        #region Propiedades extras
        public TokenValidationAction tokenValidationAction;
        public FtpFiles FtpFiles;
        #endregion

        #region Constructores
        public DarkDev(IConfiguration configuration, DarkMode darkMode) : base(configuration, darkMode)
        {
            tokenValidationAction = new TokenValidationAction(configuration);
            FtpFiles = new FtpFiles(
                configuration.GetSection("Ftp").GetSection("Server").Value, 
                configuration.GetSection("Ftp").GetSection("User").Value, 
                configuration.GetSection("Ftp").GetSection("Password").Value
            );
        }
        #endregion

        #region Metodos
        public void LoadObject(MysqlObject mysqlObject)
        {
            if (mysqlObject == MysqlObject.Pedido)
            {
                Pedido = new DarkManagerMySQL<Pedido>(this.ConnectionMySQL);
            }
            else if (mysqlObject == MysqlObject.Usuario)
            {
                Usuario = new DarkManagerMySQL<Usuario>(this.ConnectionMySQL);
            }
            else if (mysqlObject == MysqlObject.Categoria)
            {
                Categoria = new DarkManagerMySQL<Categoria>(this.ConnectionMySQL);
            }
            else if (mysqlObject == MysqlObject.SubCategoria)
            {
                SubCategoria = new DarkManagerMySQL<SubCategoria>(this.ConnectionMySQL);
            }
            else if (mysqlObject == MysqlObject.ProductoFijo)
            {
                ProductoFijo = new DarkManagerMySQL<ProductoFijo>(this.ConnectionMySQL);
            }
            else if (mysqlObject == MysqlObject.Cliente)
            {
                Cliente = new DarkManagerMySQL<Cliente>(this.ConnectionMySQL);
            }
            else if (mysqlObject == MysqlObject.DetallePedido)
            {
                DetallePedido = new DarkManagerMySQL<DetallePedido>(this.ConnectionMySQL);
            }
            else if (mysqlObject == MysqlObject.DireccionFacturacion)
            {
                DireccionFacturacion = new DarkManagerMySQL<DireccionFacturacion>(this.ConnectionMySQL);
            }
            else if (mysqlObject == MysqlObject.DireccionEnvio)
            {
                DireccionEnvio = new DarkManagerMySQL<DireccionEnvio>(this.ConnectionMySQL);
            }
            else if (mysqlObject == MysqlObject.PedidoB2C)
            {
                PedidoB2C = new DarkManagerMySQL<PedidoB2C>(this.ConnectionMySQL);
            }
            else if (mysqlObject == MysqlObject.ViewDetallePedido)
            {
                ViewDetallePedido = new DarkManagerMySQL<ViewDetallePedido>(this.ConnectionMySQL);
            }
            else if (mysqlObject == MysqlObject.PedidoB2B)
            {
                PedidoB2B = new DarkManagerMySQL<PedidoB2B>(this.ConnectionMySQL);
            }
            else if (mysqlObject == MysqlObject.ViewPedido)
            {
                ViewPedido = new DarkManagerMySQL<ViewPedido>(this.ConnectionMySQL);
            }
            else if (mysqlObject == MysqlObject.OpenPayKeys)
            {
                OpenPayKeys = new DarkManagerMySQL<OpenPayKeys>(this.ConnectionMySQL);
            }
            else if (mysqlObject == MysqlObject.SeguimientB2B)
            {
                SeguimientB2B = new DarkManagerMySQL<SeguimientB2B>(this.ConnectionMySQL);
            }
            else if (mysqlObject == MysqlObject.SeguimientB2C)
            {
                SeguimientB2C = new DarkManagerMySQL<SeguimientB2C>(this.ConnectionMySQL);
            }
            else if (mysqlObject == MysqlObject.OpenPayLog)
            {
                OpenPayLog = new DarkManagerMySQL<OpenPayLog>(this.ConnectionMySQL);
            }
        }
        public void LoadObject(SSQLObject sSQLObject)
        {
            if (sSQLObject == SSQLObject.TipoCambio)
            {
                TipoCambio = new DarkManagerMSSQL<TipoCambio>(this.ConnectionSqlSever);
            }
            if (sSQLObject == SSQLObject.SocioNegocio)
            {
                SocioNegocio = new SocioNegocio(this.ConnectionSqlSever);
            }
        }
        #endregion

    }
    public enum MysqlObject
    {
        Pedido = 1,
        Usuario = 2,
        Categoria = 3,
        SubCategoria = 4,
        ProductoFijo = 5,
        Cliente = 6,
        DetallePedido = 7,
        DireccionFacturacion = 8,
        DireccionEnvio = 9,
        PedidoB2C = 10,
        ViewDetallePedido = 11,
        PedidoB2B = 12,
        ViewPedido = 13,
        OpenPayKeys = 14,
        SeguimientB2B = 15,
        SeguimientB2C = 16,
        OpenPayLog = 17,
    }

    public enum SSQLObject
    {
        TipoCambio = 1,
        SocioNegocio = 2,
    }
}
