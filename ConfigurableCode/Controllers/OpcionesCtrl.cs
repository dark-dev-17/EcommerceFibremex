using ConfigurableCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace ConfigurableCode.Controllers
{
    public class OpcionesCtrl
    {
        private Configurable configurable;
        public Configurable Configurable { get { return configurable; } }
        public OpcionesCtrl(Configurable configurable_)
        {
            this.configurable = configurable_;
        }

        public void UpdOpcion(int Id, int IdOpcion, string clave, string Title)
        {
            var parte_re = Get(Id);

            if (parte_re.Opciones.Count == 0)
            {
                throw new Exception(string.Format("Error, sin opciones en la parte         '{0}'", parte_re.Descripcion));
            }
            var opcion_re = GetOpcion(IdOpcion, parte_re);

            opcion_re.Clave = clave;
            opcion_re.Descripcion = Title;
        }

        public void DelOpcion(int Id, int IdOpcion)
        {
            var parte_re = Get(Id);
            
            if(parte_re.Opciones.Count ==0)
            {
                throw new Exception(string.Format("Error, sin opciones en la parte         '{0}'", parte_re.Descripcion));
            }
            var opcion_re = GetOpcion(IdOpcion, parte_re);
            if (!parte_re.Opciones.Remove(opcion_re))
            {
                throw new Exception(string.Format("Error al eliminar la opcion en la parte '{0}'",parte_re.Descripcion));
            }
        }

        public void AddOpcion(int Id, string clave, string Title)
        {
            var parte_re = Get(Id);
            if(parte_re.Tipo == "Open_mask")
            {
                throw new Exception("no se puede agregar opciones a una parte tipo 'Open_mask'");
            }
            
            if(parte_re.Opciones == null)
            {
                parte_re.Opciones = new List<Opcion>();
            }

            if (parte_re.Tipo == "Fijo" && parte_re.Opciones.Count == 1)
            {
                throw new Exception("Solo se puede agrear una opcion a una parte tipo 'Fija'");
            }

            parte_re.Opciones.Add(new Opcion
            {
                Id = parte_re.Opciones.Count == 0 ? 1 : parte_re.Opciones.Max(a => a.Id) + 1,
                Clave = clave,
                Descripcion = Title,
                Posicion = 1
            });
        }

        private Parte Get(int Id)
        {
            var parte_re = configurable.Partes.Find(a => a.Id == Id);
            if (parte_re is null)
            {
                throw new Exception("parte no encontrada");
            }
            return parte_re;
        }

        private Opcion GetOpcion(int Id, Parte parte)
        {
            var Opcion_re = parte.Opciones.Find(a => a.Id == Id);

            if (Opcion_re is null)
            {
                throw new Exception("opcion no encontrada");
            }
            return Opcion_re;
        }
    }
}
