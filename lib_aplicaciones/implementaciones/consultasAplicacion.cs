using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.implementaciones
{
    public class consultasAplicacion : IconsultasAplicacion
    {
        private IConexion conexion;

        public consultas Actualizar(consultas entidad)
        {
            var entry = this.conexion!.Entry<consultas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public consultas Borrar(consultas entidad)
        {
            this.conexion.consultas!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<consultas> Consultar()
        {
            return this.conexion.consultas!.ToList();
        }

        public consultas Insert(consultas entidad)
        {
            this.conexion.consultas!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}