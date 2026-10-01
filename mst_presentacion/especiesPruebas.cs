using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class especiesPruebas
    {
        private IConexion conexion;
        private especies? entidad = null;

        public especiesPruebas()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = "server=localhost;database=db_veterinaria;Integrated Security=True;TrustServerCertificate=true;";
        }

        [TestMethod]
        public void Execute()
        {
            Insertar();
            Consultar();
            Actualizar();
            Borrar();
        }

        public void Insertar()
        {
            this.entidad = new especies()
            {
                Nombre = "Especie Prueba " + Guid.NewGuid().ToString().Substring(0, 8),
                Descripcion = "Descripcion de prueba"
            };
            this.conexion.especies!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.especies!
                .Where(x => x.Id == this.entidad!.Id)
                .ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Descripcion = "Actualizado " + Guid.NewGuid().ToString().Substring(0, 8);

            var entry = this.conexion!.Entry<especies>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.especies!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}