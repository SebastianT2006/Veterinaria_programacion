using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class clientesPruebas
    {
        private IConexion conexion;
        private personas? persona = null;
        private clientes? entidad = null;

        public clientesPruebas()
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
                Nombre = "Persona Cliente " + Guid.NewGuid().ToString().Substring(0, 8),
                Telefono = "3000000000",
                Email = "cli@test.com",
                Direccion = "Calle 1",
                FechaNacimiento = new DateTime(1990, 1, 1)
            };
            this.conexion.personas!.Add(this.persona);
            this.conexion.SaveChanges();
        }

        public void Insertar()
        {
            this.entidad = new clientes()
            {
                Persona = this.persona!.Id,
                TipoCliente = "Particular",
                FechaRegistro = DateTime.Now,
                Estado = "Activo",
                Observaciones = "Cliente prueba",
                PreferenciaContacto = "Email"
            };
            this.conexion.clientes!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.clientes!
                .Where(x => x.Id == this.entidad!.Id)
                .ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Estado = "Inactivo";

            var entry = this.conexion!.Entry<clientes>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.clientes!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }

        private void LimpiarPersona()
        {
            this.conexion.personas!.Remove(this.persona!);
            this.conexion.SaveChanges();
        }
    }
}