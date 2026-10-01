using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.implementaciones
{
    public class serviciosAplicacion : IserviciosAplicacion
    {
        private IConexion conexion;

        public servicios Actualizar(servicios entidad)
        {
            var entry = this.conexion!.Entry<servicios>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public servicios Borrar(servicios entidad)
        {
            this.conexion.servicios!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<servicios> Consultar()
        {
            return this.conexion.servicios!.ToList();
        }

        public servicios Insert(servicios entidad)
        {
            this.conexion.servicios!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}