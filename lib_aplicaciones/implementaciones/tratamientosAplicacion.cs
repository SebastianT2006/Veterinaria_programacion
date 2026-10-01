using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.implementaciones
{
    public class tratamientosAplicacion : ItratamientosAplicacion
    {
        private IConexion conexion;

        public tratamientos Actualizar(tratamientos entidad)
        {
            var entry = this.conexion!.Entry<tratamientos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public tratamientos Borrar(tratamientos entidad)
        {
            this.conexion.tratamientos!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<tratamientos> Consultar()
        {
            return this.conexion.tratamientos!.ToList();
        }

        public tratamientos Insert(tratamientos entidad)
        {
            this.conexion.tratamientos!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}