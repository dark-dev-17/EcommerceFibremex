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
        public DarkSpecialMySQL<HomeSlide> HomeSlide { get; set; }
        public DarkSpecialMSSQL<DireccionPedido> DireccionPedido { get; set; }
        public DarkManagerMySQL<CFDI_Usos> CFDI_Usos { get; set; }
        public DarkManagerMySQL<ViewPedidoB2C_> ViewPedidoB2C_ { get; set; }
        public DarkManagerMySQL<LogErrorsOpenPay> LogErrorsOpenPay { get; set; }
        public DarkManagerMySQL<SplittelContacto> SplittelContacto { get; set; }
        public DarkManagerMySQL<ViewCategorias> ViewCategorias { get; set; }
        public DarkManagerMySQL<ProductoComent> ProductoComent { get; set; }
        public DarkSpecialMySQL<ProductoComentProm> ProductoComentProm { get; set; }
        public DarkManagerMySQL<Configu_nombre> Configu_nombre { get; set; }
        #endregion

        #region Sap bussines one
        public DarkManagerMSSQL<TipoCambio> TipoCambio { get; set; }
        public SocioNegocio SocioNegocio { get; set; }
        #endregion

        #region Propiedades extras
        public TokenValidationAction tokenValidationAction;
        public FtpFiles FtpFiles;
        /// <summary>
        /// Url de servicios web de Grupo Splittel
        /// </summary>
        public string EndPointWs { get; internal set; }
        #endregion

        #region Constructores
        public DarkDev(IConfiguration configuration, DarkMode darkMode) : base(configuration, darkMode)
        {
            tokenValidationAction = new TokenValidationAction(configuration);

            bool ModeProduction = configuration.GetSection("ModeProduction").Value == "true" ? true : false;
            string FTPS = ModeProduction ? "Ftp" : "FtpTest";
            string FtpServer = configuration.GetSection(FTPS).GetSection("server").Value;
            string FtpUser = configuration.GetSection(FTPS).GetSection("User").Value;
            string FtpPassword = configuration.GetSection(FTPS).GetSection("Password").Value;
            string FtpDomain = configuration.GetSection(FTPS).GetSection("Domain").Value;
            string FtpSiterute = configuration.GetSection(FTPS).GetSection("Siterute").Value;
            string FtpSitebase = configuration.GetSection(FTPS).GetSection("Sitebase").Value;
            string EndPointWs = configuration.GetSection($"WsSplittel{(ModeProduction ? "Test":"")}").Value;

            FtpFiles = new FtpFiles(FtpServer, FtpUser, FtpPassword, FtpDomain, FtpSiterute, FtpSitebase);
            
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
            else if (mysqlObject == MysqlObject.HomeSlide)
            {
                HomeSlide = new DarkSpecialMySQL<HomeSlide>(this.ConnectionMySQL);
            }
            else if (mysqlObject == MysqlObject.CFDI_Usos)
            {
                CFDI_Usos = new DarkManagerMySQL<CFDI_Usos>(this.ConnectionMySQL);
            }
            else if (mysqlObject == MysqlObject.ViewPedidoB2C_)
            {
                ViewPedidoB2C_ = new DarkManagerMySQL<ViewPedidoB2C_>(this.ConnectionMySQL);
            }
            else if (mysqlObject == MysqlObject.LogErrorsOpenPay)
            {
                LogErrorsOpenPay = new DarkManagerMySQL<LogErrorsOpenPay>(this.ConnectionMySQL);
            }
            else if (mysqlObject == MysqlObject.SplittelContacto)
            {
                SplittelContacto = new DarkManagerMySQL<SplittelContacto>(this.ConnectionMySQL);
            }
            else if (mysqlObject == MysqlObject.ViewCategorias)
            {
                ViewCategorias = new DarkManagerMySQL<ViewCategorias>(this.ConnectionMySQL);
            }
            else if (mysqlObject == MysqlObject.ProductoComent)
            {
                ProductoComent = new DarkManagerMySQL<ProductoComent>(this.ConnectionMySQL);
            }
            else if (mysqlObject == MysqlObject.ProductoComentProm)
            {
                ProductoComentProm = new DarkSpecialMySQL<ProductoComentProm>(this.ConnectionMySQL);
            }
            else if (mysqlObject == MysqlObject.Configu_nombre)
            {
                Configu_nombre = new DarkManagerMySQL<Configu_nombre>(this.ConnectionMySQL);
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
            if (sSQLObject == SSQLObject.DireccionPedido)
            {
                DireccionPedido = new DarkSpecialMSSQL<DireccionPedido>(this.ConnectionSqlSever);
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
        HomeSlide = 18,
        CFDI_Usos = 19,
        ViewPedidoB2C_ = 20,
        LogErrorsOpenPay = 21,
        SplittelContacto = 22,
        ViewCategorias = 23,
        ProductoComent = 24,
        ProductoComentProm = 25,
        Configu_nombre = 26,
    }

    public enum SSQLObject
    {
        TipoCambio = 1,
        SocioNegocio = 2,
        DireccionPedido = 3,
    }
}
