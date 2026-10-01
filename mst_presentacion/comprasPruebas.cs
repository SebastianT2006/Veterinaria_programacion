using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class comprasPruebas
    {
        private IConexion conexion;
        private proveedores? proveedor = null;
        private compras? entidad = null;

        public comprasPruebas()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = "server=localhost;database=db_veterinaria;Integrated Security=True;TrustServerCertificate=true;";
        }

        [TestMethod]
        public void Execute()
        {
            PrepararProveedor();
            Insertar();
            Consultar();
            Actualizar();
            Borrar();
            Limpiar();
        }

        private void PrepararProveedor()
        {
            this.proveedor = new proveedores()
            {
                Nombre = "Proveedor Compra " + Guid.NewGuid().ToString().Substring(0, 8),
                Telefono = "6040000000",
                Email = "pc@test.com",
                Direccion = "Direccion 1",
                Nit = "900000000-" + Guid.NewGuid().ToString().Substring(0, 1)
            };
            this.conexion.proveedores!.Add(this.proveedor);
            this.conexion.SaveChanges();
        }

        public void Insertar()
        {
            this.entidad = new compras()
            {
                Proveedor = this.proveedor!.Id,
                Fecha = DateTime.Now,
                Total = 450000,
                NumeroFactura = "FC-" + Guid.NewGuid().ToString().Substring(0, 8),
                Estado = "Recibida",
                Observaciones = "Compra prueba"
            };
            this.conexion.compras!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.compras!
                .Where(x => x.Id == this.entidad!.Id)
                .ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Estado = "Anulada";

            var entry = this.conexion!.Entry<compras>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.compras!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }

        private void Limpiar()
        {
            this.conexion.proveedores!.Remove(this.proveedor!);
            this.conexion.SaveChanges();
        }
    }
}