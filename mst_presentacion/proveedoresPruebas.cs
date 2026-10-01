using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class proveedoresPruebas
    {
        private IConexion conexion;
        private proveedores? entidad = null;

        public proveedoresPruebas()
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
            this.entidad = new proveedores()
            {
                Nombre = "Proveedor Prueba " + Guid.NewGuid().ToString().Substring(0, 8),
                Telefono = "6040000000",
                Email = "prov@test.com",
                Direccion = "Direccion 1",
                Nit = "900000000-" + Guid.NewGuid().ToString().Substring(0, 1)
            };
            this.conexion.proveedores!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.proveedores!
                .Where(x => x.Id == this.entidad!.Id)
                .ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Direccion = "Actualizado " + Guid.NewGuid().ToString().Substring(0, 8);

            var entry = this.conexion!.Entry<proveedores>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.proveedores!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}