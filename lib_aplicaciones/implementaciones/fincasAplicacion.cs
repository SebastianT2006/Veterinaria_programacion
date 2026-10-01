using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.implementaciones
{
    public class fincasAplicacion : IfincasAplicacion
    {
        private IConexion conexion;

        public fincas Actualizar(fincas entidad)
        {
            var entry = this.conexion!.Entry<fincas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public fincas Borrar(fincas entidad)
        {
            this.conexion.fincas!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<fincas> Consultar()
        {
            return this.conexion.fincas!.ToList();
        }

        public fincas Insert(fincas entidad)
        {
            this.conexion.fincas!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}