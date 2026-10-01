using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class razasPruebas
    {
        private IConexion conexion;
        private especies? especie = null;
        private razas? entidad = null;

        public razasPruebas()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = "server=localhost;database=db_veterinaria;Integrated Security=True;TrustServerCertificate=true;";
        }

        [TestMethod]
        public void Execute()
        {
            PrepararEspecie();
            Insertar();
            Consultar();
            Actualizar();
            Borrar();
            LimpiarEspecie();
        }

        private void PrepararEspecie()
        {
            this.especie = new especies()
            {
                Nombre = "Especie Raza " + Guid.NewGuid().ToString().Substring(0, 8),
                Descripcion = "Especie de prueba"
            };
            this.conexion.especies!.Add(this.especie);
            this.conexion.SaveChanges();
        }

        public void Insertar()
        {
            this.entidad = new razas()
            {
                Especie = this.especie!.Id,
                Nombre = "Raza Prueba " + Guid.NewGuid().ToString().Substring(0, 8),
                TamanoPromedio = 50,
                PesoPromedio = 25,
                EsperanzaVidaAnios = 12,
                Descripcion = "Descripcion prueba"
            };
            this.conexion.razas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.razas!
                .Where(x => x.Id == this.entidad!.Id)
                .ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Descripcion = "Actualizado " + Guid.NewGuid().ToString().Substring(0, 8);

            var entry = this.conexion!.Entry<razas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.razas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }

        private void LimpiarEspecie()
        {
            this.conexion.especies!.Remove(this.especie!);
            this.conexion.SaveChanges();
        }
    }
}