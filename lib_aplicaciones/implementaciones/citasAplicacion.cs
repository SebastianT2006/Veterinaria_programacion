using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.implementaciones
{
    public class citasAplicacion : IcitasAplicacion
    {
        private IConexion conexion;

        public citas Actualizar(citas entidad)
        {
            var entry = this.conexion!.Entry<citas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public citas Borrar(citas entidad)
        {
            this.conexion.citas!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<citas> Consultar()
        {
            return this.conexion.citas!.ToList();
        }

        public citas Insert(citas entidad)
        {
            this.conexion.citas!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}