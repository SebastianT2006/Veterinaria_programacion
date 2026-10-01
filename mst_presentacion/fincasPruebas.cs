using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class fincasPruebas
    {
        private IConexion conexion;
        private personas? persona = null;
        private clientes? cliente = null;
        private fincas? entidad = null;

        public fincasPruebas()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = "server=localhost;database=db_veterinaria;Integrated Security=True;TrustServerCertificate=true;";
        }

        [TestMethod]
        public void Execute()
        {
            PrepararPersonaYCliente();
            Insertar();
            Consultar();
            Actualizar();
            Borrar();
            Limpiar();
        }

        private void PrepararPersonaYCliente()
        {
            this.persona = new personas()
            {
                Nombre = "Persona Finca " + Guid.NewGuid().ToString().Substring(0, 8),
                Telefono = "3000000000",
                Email = "finca@test.com",
                Direccion = "Calle 4",
                FechaNacimiento = new DateTime(1980, 1, 1)
            };
            this.conexion.personas!.Add(this.persona);
            this.conexion.SaveChanges();

            this.cliente = new clientes()
            {
                Persona = this.persona.Id,
                TipoCliente = "Finca",
                FechaRegistro = DateTime.Now,
                Estado = "Activo",
                Observaciones = "Cliente prueba",
                PreferenciaContacto = "Telefono"
            };
            this.conexion.clientes!.Add(this.cliente);
            this.conexion.SaveChanges();
        }

        public void Insertar()
        {
            this.entidad = new fincas()
            {
                Cliente = this.cliente!.Id,
                Nombre = "Finca Prueba " + Guid.NewGuid().ToString().Substring(0, 8),
                Direccion = "Rionegro",
                AreaHectareas = 25.50m,
                TipoProduccion = "Lechera",
                Estado = "Activa"
            };
            this.conexion.fincas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.fincas!
                .Where(x => x.Id == this.entidad!.Id)
                .ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Estado = "Inactiva";

            var entry = this.conexion!.Entry<fincas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.fincas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }

        private void Limpiar()
        {
            this.conexion.clientes!.Remove(this.cliente!);
            this.conexion.SaveChanges();
            this.conexion.personas!.Remove(this.persona!);
            this.conexion.SaveChanges();
        }
    }
}