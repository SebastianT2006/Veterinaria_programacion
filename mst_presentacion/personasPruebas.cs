using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class personasPruebas
    {
        private IConexion conexion;
        private personas? entidad = null;

        public personasPruebas()
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
            this.entidad = new personas()
            {
                Nombre = "Persona Prueba " + Guid.NewGuid().ToString().Substring(0, 8),
                Telefono = "3000000000",
                Email = "prueba@test.com",
                Direccion = "Calle 1",
                FechaNacimiento = new DateTime(1990, 1, 1)
            };
            this.conexion.personas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.personas!
                .Where(x => x.Id == this.entidad!.Id)
                .ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Nombre = "Actualizado " + Guid.NewGuid().ToString().Substring(0, 8);

            var entry = this.conexion!.Entry<personas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.personas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}