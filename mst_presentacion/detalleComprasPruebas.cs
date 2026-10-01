using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class detalleComprasPruebas
    {
        private IConexion conexion;
        private proveedores? proveedor = null;
        private compras? compra = null;
        private productos? producto = null;
        private detalleCompras? entidad = null;

        public detalleComprasPruebas()
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
            this.proveedor = new proveedores()
            {
                Nombre = "Proveedor DC " + Guid.NewGuid().ToString().Substring(0, 8),
                Telefono = "6040000000",
                Email = "dc@test.com",
                Direccion = "Direccion 2",
                Nit = "900000000-" + Guid.NewGuid().ToString().Substring(0, 1)
            };
            this.conexion.proveedores!.Add(this.proveedor);
            this.conexion.SaveChanges();

            this.compra = new compras()
            {
                Proveedor = this.proveedor.Id,
                Fecha = DateTime.Now,
                Total = 350000,
                NumeroFactura = "FC-" + Guid.NewGuid().ToString().Substring(0, 8),
                Estado = "Recibida",
                Observaciones = "Compra DC"
            };
            this.conexion.compras!.Add(this.compra);
            this.conexion.SaveChanges();

            this.producto = new productos()
            {
                Nombre = "Producto DC " + Guid.NewGuid().ToString().Substring(0, 8),
                Tipo = "Medicamento",
                Precio = 35000,
                Stock = 80,
                Descripcion = "Producto prueba"
            };
            this.conexion.productos!.Add(this.producto);
            this.conexion.SaveChanges();
        }

        public void Insertar()
        {
            this.entidad = new detalleCompras()
            {
                Compra = this.compra!.Id,
                Producto = this.producto!.Id,
                Cantidad = 10,
                Precio = 35000
            };
            this.conexion.detalleCompras!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.detalleCompras!
                .Where(x => x.Id == this.entidad!.Id)
                .ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Cantidad = 20;

            var entry = this.conexion!.Entry<detalleCompras>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.detalleCompras!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }

        private void Limpiar()
        {
            this.conexion.productos!.Remove(this.producto!);
            this.conexion.SaveChanges();
            this.conexion.compras!.Remove(this.compra!);
            this.conexion.SaveChanges();
            this.conexion.proveedores!.Remove(this.proveedor!);
            this.conexion.SaveChanges();
        }
    }
}