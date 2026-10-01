using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.implementaciones
{
    public class proveedoresAplicacion : IproveedoresAplicacion
    {
        private IConexion conexion;

        public proveedores Actualizar(proveedores entidad)
        {
            var entry = this.conexion!.Entry<proveedores>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public proveedores Borrar(proveedores entidad)
        {
            this.conexion.proveedores!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<proveedores> Consultar()
        {
            return this.conexion.proveedores!.ToList();
        }

        public proveedores Insert(proveedores entidad)
        {
            this.conexion.proveedores!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}