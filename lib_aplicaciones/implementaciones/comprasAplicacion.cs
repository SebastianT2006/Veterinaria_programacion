using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.implementaciones
{
    public class comprasAplicacion : IcomprasAplicacion
    {
        private IConexion conexion;

        public compras Actualizar(compras entidad)
        {
            var entry = this.conexion!.Entry<compras>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public compras Borrar(compras entidad)
        {
            this.conexion.compras!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<compras> Consultar()
        {
            return this.conexion.compras!.ToList();
        }

        public compras Insert(compras entidad)
        {
            this.conexion.compras!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}