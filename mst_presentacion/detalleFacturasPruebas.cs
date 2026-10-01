using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class detalleFacturasPruebas
    {
        private IConexion conexion;
        private personas? persona = null;
        private clientes? cliente = null;
        private facturas? factura = null;
        private productos? producto = null;
        private servicios? servicio = null;
        private detalleFacturas? entidad = null;

        public detalleFacturasPruebas()
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
                Nombre = "Persona DF " + Guid.NewGuid().ToString().Substring(0, 8),
                Telefono = "3000000000",
                Email = "df@test.com",
                Direccion = "Calle 15",
                FechaNacimiento = new DateTime(1980, 8, 8)
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

            this.factura = new facturas()
            {
                Cliente = this.cliente.Id,
                Fecha = DateTime.Now,
                Total = 125000,
                MetodoPago = "Tarjeta",
                Estado = "Pagada",
                Observaciones = "Factura DF"
            };
            this.conexion.facturas!.Add(this.factura);
            this.conexion.SaveChanges();

            this.producto = new productos()
            {
                Nombre = "Producto DF " + Guid.NewGuid().ToString().Substring(0, 8),
                Tipo = "Medicamento",
                Precio = 75000,
                Stock = 50,
                Descripcion = "Producto prueba"
            };
            this.conexion.productos!.Add(this.producto);
            this.conexion.SaveChanges();

            this.servicio = new servicios()
            {
                Nombre = "Servicio DF " + Guid.NewGuid().ToString().Substring(0, 8),
                Descripcion = "Servicio prueba",
                Precio = 80000,
                Duracion = 30,
                Categoria = "Consulta"
            };
            this.conexion.servicios!.Add(this.servicio);
            this.conexion.SaveChanges();
        }

        public void Insertar()
        {
            this.entidad = new detalleFacturas()
            {
                Factura = this.factura!.Id,
                Producto = this.producto!.Id,
                Servicio = null,
                Cantidad = 1,
                Precio = 75000
            };
            this.conexion.detalleFacturas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.detalleFacturas!
                .Where(x => x.Id == this.entidad!.Id)
                .ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Cantidad = 2;

            var entry = this.conexion!.Entry<detalleFacturas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.detalleFacturas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }

        private void Limpiar()
        {
            this.conexion.servicios!.Remove(this.servicio!);
            this.conexion.SaveChanges();
            this.conexion.productos!.Remove(this.producto!);
            this.conexion.SaveChanges();
            this.conexion.facturas!.Remove(this.factura!);
            this.conexion.SaveChanges();
            this.conexion.clientes!.Remove(this.cliente!);
            this.conexion.SaveChanges();
            this.conexion.personas!.Remove(this.persona!);
            this.conexion.SaveChanges();
        }
    }
}