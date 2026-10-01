using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.implementaciones
{
    public class historiasClinicasAplicacion : IhistoriasClinicasAplicacion
    {
        private IConexion conexion;

        public historiasClinicas Actualizar(historiasClinicas entidad)
        {
            var entry = this.conexion!.Entry<historiasClinicas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public historiasClinicas Borrar(historiasClinicas entidad)
        {
            this.conexion.historiasClinicas!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<historiasClinicas> Consultar()
        {
            return this.conexion.historiasClinicas!.ToList();
        }

        public historiasClinicas Insert(historiasClinicas entidad)
        {
            this.conexion.historiasClinicas!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}