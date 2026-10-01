using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.implementaciones
{
    public class clientesAplicacion : IclientesAplicacion
    {
        private IConexion conexion;

        public clientes Actualizar(clientes entidad)
        {
            var entry = this.conexion!.Entry<clientes>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public clientes Borrar(clientes entidad)
        {
            this.conexion.clientes!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<clientes> Consultar()
        {
            return this.conexion.clientes!.ToList();
        }

        public clientes Insert(clientes entidad)
        {
            this.conexion.clientes!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}