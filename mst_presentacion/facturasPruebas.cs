using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class facturasPruebas
    {
        private IConexion conexion;
        private personas? persona = null;
        private clientes? cliente = null;
        private facturas? entidad = null;

        public facturasPruebas()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = "server=localhost;database=db_veterinaria;Integrated Security=True;TrustServerCertificate=true;";
        }

        [TestMethod]
        public void Execute()
        {
            PrepararDependencias();
            Insertar();
            Consultar();
            Actualizar();
            Borrar();
            Limpiar();
        }

        private void PrepararDependencias()
        {
            this.persona = new personas()
            {
                Nombre = "Persona Fac " + Guid.NewGuid().ToString().Substring(0, 8),
                Telefono = "3000000000",
                Email = "fac@test.com",
                Direccion = "Calle 14",
                FechaNacimiento = new DateTime(1980, 7, 7)
            };
            this.conexion.personas!.Add(this.persona);
            this.conexion.SaveChanges();

            this.cliente = new clientes()
            {
                Persona = this.persona.Id,
                TipoCliente = "Particular",
                FechaRegistro = DateTime.Now,
                Estado = "Activo",
                Observaciones = "Cliente prueba",
                PreferenciaContacto = "Email"
            };
            this.conexion.clientes!.Add(this.cliente);
            this.conexion.SaveChanges();
        }

        public void Insertar()
        {
            this.entidad = new facturas()
            {
                Cliente = this.cliente!.Id,
                Fecha = DateTime.Now,
                Total = 80000,
                MetodoPago = "Efectivo",
                Estado = "Pagada",
                Observaciones = "Factura prueba"
            };
            this.conexion.facturas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.facturas!
                .Where(x => x.Id == this.entidad!.Id)
                .ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Estado = "Pendiente";

            var entry = this.conexion!.Entry<facturas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.facturas!.Remove(this.entidad!);
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