using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class empleadosPruebas
    {
        private IConexion conexion;
        private personas? persona = null;
        private empleados? entidad = null;

        public empleadosPruebas()
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
                Nombre = "Persona Emp " + Guid.NewGuid().ToString().Substring(0, 8),
                Telefono = "3000000000",
                Email = "emp@test.com",
                Direccion = "Calle 3",
                FechaNacimiento = new DateTime(1988, 3, 3)
            };
            this.conexion.personas!.Add(this.persona);
            this.conexion.SaveChanges();
        }

        public void Insertar()
        {
            this.entidad = new empleados()
            {
                Persona = this.persona!.Id,
                Cargo = "Auxiliar",
                FechaIngreso = DateTime.Now,
                Salario = 1700000,
                Estado = "Activo",
                Turno = "Manana"
            };
            this.conexion.empleados!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.empleados!
                .Where(x => x.Id == this.entidad!.Id)
                .ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Turno = "Tarde";

            var entry = this.conexion!.Entry<empleados>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.empleados!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }

        private void LimpiarPersona()
        {
            this.conexion.personas!.Remove(this.persona!);
            this.conexion.SaveChanges();
        }
    }
}