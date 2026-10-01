using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.implementaciones
{
    public class especiesAplicacion : IespeciesAplicacion
    {
        private IConexion conexion;

        public especies Actualizar(especies entidad)
        {
            var entry = this.conexion!.Entry<especies>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public especies Borrar(especies entidad)
        {
            this.conexion.especies!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<especies> Consultar()
        {
            return this.conexion.especies!.ToList();
        }

        public especies Insert(especies entidad)
        {
            this.conexion.especies!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}