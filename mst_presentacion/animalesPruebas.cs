using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class animalesPruebas
    {
        private IConexion conexion;
        private personas? persona = null;
        private clientes? cliente = null;
        private especies? especie = null;
        private razas? raza = null;
        private fincas? finca = null;
        private animales? entidad = null;

        public animalesPruebas()
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
            // 1. Persona
            this.persona = new personas()
            {
                Nombre = "Persona Animal " + Guid.NewGuid().ToString().Substring(0, 8),
                Telefono = "3000000000",
                Email = "animal@test.com",
                Direccion = "Calle 5",
                FechaNacimiento = new DateTime(1980, 2, 2)
            };
            this.conexion.personas!.Add(this.persona);
            this.conexion.SaveChanges();

            // 2. Cliente
            this.cliente = new clientes()
            {
                Persona = this.persona.Id,
                TipoCliente = "Finca",
                FechaRegistro = DateTime.Now,
                Estado = "Activo",
                Observaciones = "Cliente prueba",
                PreferenciaContacto = "Email"
            };
            this.conexion.clientes!.Add(this.cliente);
            this.conexion.SaveChanges();

            // 3. Finca
            this.finca = new fincas()
            {
                Cliente = this.cliente.Id,
                Nombre = "Finca Animal " + Guid.NewGuid().ToString().Substring(0, 8),
                Direccion = "Rionegro",
                AreaHectareas = 10,
                TipoProduccion = "Ganadera",
                Estado = "Activa"
            };
            this.conexion.fincas!.Add(this.finca);
            this.conexion.SaveChanges();

            // 4. Especie
            this.especie = new especies()
            {
                Nombre = "Especie Animal " + Guid.NewGuid().ToString().Substring(0, 8),
                Descripcion = "Especie prueba"
            };
            this.conexion.especies!.Add(this.especie);
            this.conexion.SaveChanges();

            // 5. Raza
            this.raza = new razas()
            {
                Especie = this.especie.Id,
                Nombre = "Raza Animal " + Guid.NewGuid().ToString().Substring(0, 8),
                TamanoPromedio = 50,
                PesoPromedio = 30,
                EsperanzaVidaAnios = 12,
                Descripcion = "Raza prueba"
            };
            this.conexion.razas!.Add(this.raza);
            this.conexion.SaveChanges();
        }

        public void Insertar()
        {
            this.entidad = new animales()
            {
                Raza = this.raza!.Id,
                Cliente = this.cliente!.Id,
                Finca = this.finca!.Id,
                Nombre = "Animal Prueba " + Guid.NewGuid().ToString().Substring(0, 8),
                Sexo = "Macho",
                FechaNacimiento = new DateTime(2022, 1, 1),
                Color = "Negro",
                Peso = 25,
                Estado = "Activo"
            };
            this.conexion.animales!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.animales!
                .Where(x => x.Id == this.entidad!.Id)
                .ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Peso = 30;

            var entry = this.conexion!.Entry<animales>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.animales!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }

        private void Limpiar()
        {
            this.conexion.razas!.Remove(this.raza!);
            this.conexion.SaveChanges();
            this.conexion.especies!.Remove(this.especie!);
            this.conexion.SaveChanges();
            this.conexion.fincas!.Remove(this.finca!);
            this.conexion.SaveChanges();
            this.conexion.clientes!.Remove(this.cliente!);
            this.conexion.SaveChanges();
            this.conexion.personas!.Remove(this.persona!);
            this.conexion.SaveChanges();
        }
    }
}