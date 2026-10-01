using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class serviciosPruebas
    {
        private IConexion conexion;
        private servicios? entidad = null;

        public serviciosPruebas()
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
            this.entidad = new servicios()
            {
                Nombre = "Servicio Prueba " + Guid.NewGuid().ToString().Substring(0, 8),
                Descripcion = "Descripcion prueba",
                Precio = 50000,
                Duracion = 30,
                Categoria = "Consulta"
            };
            this.conexion.servicios!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.servicios!
                .Where(x => x.Id == this.entidad!.Id)
                .ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Precio = 75000;

            var entry = this.conexion!.Entry<servicios>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.servicios!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}