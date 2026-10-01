using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.implementaciones
{
    public class detalleFacturasAplicacion : IdetalleFacturasAplicacion
    {
        private IConexion conexion;

        public detalleFacturas Actualizar(detalleFacturas entidad)
        {
            var entry = this.conexion!.Entry<detalleFacturas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public detalleFacturas Borrar(detalleFacturas entidad)
        {
            this.conexion.detalleFacturas!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<detalleFacturas> Consultar()
        {
            return this.conexion.detalleFacturas!.ToList();
        }

        public detalleFacturas Insert(detalleFacturas entidad)
        {
            this.conexion.detalleFacturas!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}