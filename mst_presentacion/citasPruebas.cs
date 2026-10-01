using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class citasPruebas
    {
        private IConexion conexion;
        private personas? persona = null;
        private clientes? cliente = null;
        private especies? especie = null;
        private razas? raza = null;
        private fincas? finca = null;
        private animales? animal = null;
        private personas? personaVet = null;
        private veterinarios? veterinario = null;
        private citas? entidad = null;

        public citasPruebas()
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
            // Persona + Cliente + Finca + Especie + Raza + Animal
            this.persona = new personas()
            {
                Nombre = "Persona Cita " + Guid.NewGuid().ToString().Substring(0, 8),
                Telefono = "3000000000",
                Email = "cita@test.com",
                Direccion = "Calle 6",
                FechaNacimiento = new DateTime(1980, 1, 1)
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

            this.finca = new fincas()
            {
                Cliente = this.cliente.Id,
                Nombre = "Finca Cita " + Guid.NewGuid().ToString().Substring(0, 8),
                Direccion = "Rionegro",
                AreaHectareas = 5,
                TipoProduccion = "Mixta",
                Estado = "Activa"
            };
            this.conexion.fincas!.Add(this.finca);
            this.conexion.SaveChanges();

            this.especie = new especies()
            {
                Nombre = "Especie Cita " + Guid.NewGuid().ToString().Substring(0, 8),
                Descripcion = "Especie prueba"
            };
            this.conexion.especies!.Add(this.especie);
            this.conexion.SaveChanges();

            this.raza = new razas()
            {
                Especie = this.especie.Id,
                Nombre = "Raza Cita " + Guid.NewGuid().ToString().Substring(0, 8),
                TamanoPromedio = 30,
                PesoPromedio = 10,
                EsperanzaVidaAnios = 10,
                Descripcion = "Raza prueba"
            };
            this.conexion.razas!.Add(this.raza);
            this.conexion.SaveChanges();

            this.animal = new animales()
            {
                Raza = this.raza.Id,
                Cliente = this.cliente.Id,
                Finca = this.finca.Id,
                Nombre = "Animal Cita " + Guid.NewGuid().ToString().Substring(0, 8),
                Sexo = "Hembra",
                FechaNacimiento = new DateTime(2023, 5, 5),
                Color = "Cafe",
                Peso = 5,
                Estado = "Activo"
            };
            this.conexion.animales!.Add(this.animal);
            this.conexion.SaveChanges();

            // Persona + Veterinario
            this.personaVet = new personas()
            {
                Nombre = "Vet Cita " + Guid.NewGuid().ToString().Substring(0, 8),
                Telefono = "3001111111",
                Email = "vet@test.com",
                Direccion = "Calle 7",
                FechaNacimiento = new DateTime(1985, 5, 5)
            };
            this.conexion.personas!.Add(this.personaVet);
            this.conexion.SaveChanges();

            this.veterinario = new veterinarios()
            {
                Persona = this.personaVet.Id,
                Especialidad = "Medicina general",
                RegistroProfesional = "VET-" + Guid.NewGuid().ToString().Substring(0, 8),
                FechaIngreso = DateTime.Now,
                Estado = "Activo",
                TelefonoLaboral = "6040000000"
            };
            this.conexion.veterinarios!.Add(this.veterinario);
            this.conexion.SaveChanges();
        }

        public void Insertar()
        {
            this.entidad = new citas()
            {
                Animal = this.animal!.Id,
                Veterinario = this.veterinario!.Id,
                Fecha = new DateTime(2026, 5, 1),
                Hora = new TimeSpan(10, 0, 0),
                Motivo = "Revision general",
                Estado = "Pendiente",
                Observaciones = "Sin observaciones"
            };
            this.conexion.citas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.citas!
                .Where(x => x.Id == this.entidad!.Id)
                .ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Estado = "Atendida";

            var entry = this.conexion!.Entry<citas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.citas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }

        private void Limpiar()
        {
            this.conexion.veterinarios!.Remove(this.veterinario!);
            this.conexion.SaveChanges();
            this.conexion.personas!.Remove(this.personaVet!);
            this.conexion.SaveChanges();
            this.conexion.animales!.Remove(this.animal!);
            this.conexion.SaveChanges();
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