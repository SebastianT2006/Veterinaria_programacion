using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.implementaciones
{
    public class personasAplicacion : IpersonasAplicacion
    {
        private IConexion conexion;

        public personas Actualizar(personas entidad)
        {
            var entry = this.conexion!.Entry<personas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public personas Borrar(personas entidad)
        {
            this.conexion.personas!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<personas> Consultar()
        {
            return this.conexion.personas!.ToList();
        }

        public personas Insert(personas entidad)
        {
            this.conexion.personas!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}