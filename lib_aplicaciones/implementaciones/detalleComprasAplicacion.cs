using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.implementaciones
{
    public class detalleComprasAplicacion : IdetalleComprasAplicacion
    {
        private IConexion conexion;

        public detalleCompras Actualizar(detalleCompras entidad)
        {
            var entry = this.conexion!.Entry<detalleCompras>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public detalleCompras Borrar(detalleCompras entidad)
        {
            this.conexion.detalleCompras!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<detalleCompras> Consultar()
        {
            return this.conexion.detalleCompras!.ToList();
        }

        public detalleCompras Insert(detalleCompras entidad)
        {
            this.conexion.detalleCompras!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}