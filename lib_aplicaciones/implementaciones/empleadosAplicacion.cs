using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.implementaciones
{
    public class empleadosAplicacion : IempleadosAplicacion
    {
        private IConexion conexion;

        public empleados Actualizar(empleados entidad)
        {
            var entry = this.conexion!.Entry<empleados>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public empleados Borrar(empleados entidad)
        {
            this.conexion.empleados!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<empleados> Consultar()
        {
            return this.conexion.empleados!.ToList();
        }

        public empleados Insert(empleados entidad)
        {
            this.conexion.empleados!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}