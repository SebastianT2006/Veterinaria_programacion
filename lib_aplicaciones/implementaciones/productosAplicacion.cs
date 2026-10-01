using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.implementaciones
{
    public class productosAplicacion : IproductosAplicacion
    {
        private IConexion conexion;

        public productos Actualizar(productos entidad)
        {
            var entry = this.conexion!.Entry<productos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public productos Borrar(productos entidad)
        {
            this.conexion.productos!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<productos> Consultar()
        {
            return this.conexion.productos!.ToList();
        }

        public productos Insert(productos entidad)
        {
            this.conexion.productos!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}