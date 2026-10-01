using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.implementaciones
{
    public class animalesAplicacion : IanimalesAplicacion
    {
        private IConexion conexion;

        public animales Actualizar(animales entidad)
        {
            var entry = this.conexion!.Entry<animales>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public animales Borrar(animales entidad)
        {
            this.conexion.animales!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<animales> Consultar()
        {
            return this.conexion.animales!.ToList();
        }

        public animales Insert(animales entidad)
        {
            this.conexion.animales!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}