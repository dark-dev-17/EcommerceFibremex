using ConfigurableCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ConfigurableCode.Controllers
{
    public class PartesCrl
    {
        private Configurable configurable;
        public Configurable Configurable { get { return configurable; } }
        public PartesCrl(Configurable configurable_)
        {
            this.configurable = configurable_;
        }

        #region Partes
        public void Delete(int id)
        {
            ValidateConf();
            var Parte = configurable.Partes.Find(a => a.Id == id);
            if (Parte is null)
            {
                throw new Exception(string.Format("No se encontró la parte seleccionada del configurable"));
            }

            if (!configurable.Partes.Remove(Parte))
            {
                throw new Exception(string.Format("Error al eliminar la parte seleccionada del configurable"));
            }
        }

        public void Update(int id, string Tipo, string Descripcion)
        {
            ValidateConf();
            var Parte = configurable.Partes.Find(a => a.Id == id);

            if (Parte is null)
            {
                throw new Exception(string.Format("No se encontró la parte seleccionada del configurable"));
            }
            Parte.Tipo = Tipo;
            Parte.Descripcion = Descripcion;

            if (Tipo.Trim() != "Fijo" && Tipo.Trim() != "Opcional" && Tipo.Trim() != "Open_mask")
            {
                throw new Exception(string.Format("el tipo de parte {0} no es valido", Tipo));
            }
        }

        public void Add(string Tipo, string Descripcion)
        {
            ValidateConf();
            var Parte = new Parte
            {
                Id = configurable.Partes.Count == 0 ? 1 : configurable.Partes.Max(a => a.Id) + 1,
                Tipo = Tipo,
                Descripcion = Descripcion
            };

            if (Tipo.Trim() != "Fijo" && Tipo.Trim() != "Opcional" && Tipo.Trim() != "Open_mask")
            {
                throw new Exception(string.Format("el tipo de parte {0} no es valido", Tipo));
            }
            configurable.Partes.Add(Parte);
        }
        #endregion

        private void ValidateConf()
        {
            if(configurable is null)
            {
                throw new Exception("El configurable es nulo");
            }

            if(configurable.Partes is null)
            {
                throw new Exception("El configurable.Partes es nulo");
            }
        }
    }
}
