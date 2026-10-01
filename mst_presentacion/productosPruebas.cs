using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class productosPruebas
    {
        private IConexion conexion;
        private productos? entidad = null;

        public productosPruebas()
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
            this.entidad = new productos()
            {
                Nombre = "Producto Prueba " + Guid.NewGuid().ToString().Substring(0, 8),
                Tipo = "Medicamento",
                Precio = 10000,
                Stock = 50,
                Descripcion = "Descripcion prueba"
            };
            this.conexion.productos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.productos!
                .Where(x => x.Id == this.entidad!.Id)
                .ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Stock = 99;

            var entry = this.conexion!.Entry<productos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.productos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}