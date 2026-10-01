using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.implementaciones
{
    public class razasAplicacion : IrazasAplicacion
    {
        private IConexion conexion;

        public razas Actualizar(razas entidad)
        {
            var entry = this.conexion!.Entry<razas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public razas Borrar(razas entidad)
        {
            this.conexion.razas!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<razas> Consultar()
        {
            return this.conexion.razas!.ToList();
        }

        public razas Insert(razas entidad)
        {
            this.conexion.razas!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}