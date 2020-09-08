﻿using DbManagerDark;
using DbManagerDark.Managers;
using EcommerceApiLogic.Models;
using EcommerceApiLogic.ModelsSap;
using EcommerceApiLogic.Validators;
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
        #endregion

        #region Sap bussines one
        public DarkManagerMSSQL<TipoCambio> TipoCambio { get; set; }
        public SocioNegocio SocioNegocio { get; set; }
        #endregion

        #region Propiedades extras
        public TokenValidationAction tokenValidationAction;
        #endregion

        #region Constructores
        public DarkDev(IConfiguration configuration, DarkMode darkMode) : base(configuration, darkMode)
        {
            tokenValidationAction = new TokenValidationAction(configuration);
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
    }

    public enum SSQLObject
    {
        TipoCambio = 1,
        SocioNegocio = 2,
    }
}
