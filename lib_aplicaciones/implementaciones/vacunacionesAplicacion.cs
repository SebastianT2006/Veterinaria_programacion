using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.implementaciones
{
    public class vacunacionesAplicacion : IvacunacionesAplicacion
    {
        private IConexion conexion;

        public vacunaciones Actualizar(vacunaciones entidad)
        {
            var entry = this.conexion!.Entry<vacunaciones>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public vacunaciones Borrar(vacunaciones entidad)
        {
            this.conexion.vacunaciones!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<vacunaciones> Consultar()
        {
            return this.conexion.vacunaciones!.ToList();
        }

        public vacunaciones Insert(vacunaciones entidad)
        {
            this.conexion.vacunaciones!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}