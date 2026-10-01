using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.implementaciones
{
    public class veterinariosAplicacion : IveterinariosAplicacion
    {
        private IConexion conexion;

        public veterinarios Actualizar(veterinarios entidad)
        {
            var entry = this.conexion!.Entry<veterinarios>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public veterinarios Borrar(veterinarios entidad)
        {
            this.conexion.veterinarios!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<veterinarios> Consultar()
        {
            return this.conexion.veterinarios!.ToList();
        }

        public veterinarios Insert(veterinarios entidad)
        {
            this.conexion.veterinarios!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}