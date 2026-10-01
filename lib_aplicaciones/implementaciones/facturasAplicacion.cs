using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.implementaciones
{
    public class facturasAplicacion : IfacturasAplicacion
    {
        private IConexion conexion;

        public facturas Actualizar(facturas entidad)
        {
            var entry = this.conexion!.Entry<facturas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public facturas Borrar(facturas entidad)
        {
            this.conexion.facturas!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<facturas> Consultar()
        {
            return this.conexion.facturas!.ToList();
        }

        public facturas Insert(facturas entidad)
        {
            this.conexion.facturas!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}