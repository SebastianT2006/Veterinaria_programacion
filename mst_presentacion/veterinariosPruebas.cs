using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class veterinariosPruebas
    {
        private IConexion conexion;
        private personas? persona = null;
        private veterinarios? entidad = null;

        public veterinariosPruebas()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = "server=localhost;database=db_veterinaria;Integrated Security=True;TrustServerCertificate=true;";
        }

        [TestMethod]
        public void Execute()
        {
            PrepararPersona();
            Insertar();
            Consultar();
            Actualizar();
            Borrar();
            LimpiarPersona();
        }

        private void PrepararPersona()
        {
            this.persona = new personas()
            {
                Nombre = "Persona Vet " + Guid.NewGuid().ToString().Substring(0, 8),
                Telefono = "3000000000",
                Email = "vet@test.com",
                Direccion = "Calle 2",
                FechaNacimiento = new DateTime(1985, 5, 5)
            };
            this.conexion.personas!.Add(this.persona);
            this.conexion.SaveChanges();
        }

        public void Insertar()
        {
            this.entidad = new veterinarios()
            {
                Persona = this.persona!.Id,
                Especialidad = "Medicina general",
                RegistroProfesional = "VET-" + Guid.NewGuid().ToString().Substring(0, 8),
                FechaIngreso = DateTime.Now,
                Estado = "Activo",
                TelefonoLaboral = "6040000000"
            };
            this.conexion.veterinarios!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.veterinarios!
                .Where(x => x.Id == this.entidad!.Id)
                .ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Estado = "Inactivo";

            var entry = this.conexion!.Entry<veterinarios>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.veterinarios!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }

        private void LimpiarPersona()
        {
            this.conexion.personas!.Remove(this.persona!);
            this.conexion.SaveChanges();
        }
    }
}