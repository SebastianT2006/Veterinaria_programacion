using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class consultasPruebas
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
        private citas? cita = null;
        private consultas? entidad = null;

        public consultasPruebas()
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
            // Persona + Cliente
            this.persona = new personas()
            {
                Nombre = "Persona Consulta " + Guid.NewGuid().ToString().Substring(0, 8),
                Telefono = "3000000000",
                Email = "consulta@test.com",
                Direccion = "Calle 8",
                FechaNacimiento = new DateTime(1980, 3, 3)
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
                Nombre = "Finca Consulta " + Guid.NewGuid().ToString().Substring(0, 8),
                Direccion = "Rionegro",
                AreaHectareas = 8,
                TipoProduccion = "Lechera",
                Estado = "Activa"
            };
            this.conexion.fincas!.Add(this.finca);
            this.conexion.SaveChanges();

            this.especie = new especies()
            {
                Nombre = "Especie Consulta " + Guid.NewGuid().ToString().Substring(0, 8),
                Descripcion = "Especie prueba"
            };
            this.conexion.especies!.Add(this.especie);
            this.conexion.SaveChanges();

            this.raza = new razas()
            {
                Especie = this.especie.Id,
                Nombre = "Raza Consulta " + Guid.NewGuid().ToString().Substring(0, 8),
                TamanoPromedio = 40,
                PesoPromedio = 15,
                EsperanzaVidaAnios = 12,
                Descripcion = "Raza prueba"
            };
            this.conexion.razas!.Add(this.raza);
            this.conexion.SaveChanges();

            this.animal = new animales()
            {
                Raza = this.raza.Id,
                Cliente = this.cliente.Id,
                Finca = this.finca.Id,
                Nombre = "Animal Consulta " + Guid.NewGuid().ToString().Substring(0, 8),
                Sexo = "Macho",
                FechaNacimiento = new DateTime(2022, 6, 6),
                Color = "Negro",
                Peso = 20,
                Estado = "Activo"
            };
            this.conexion.animales!.Add(this.animal);
            this.conexion.SaveChanges();

            this.personaVet = new personas()
            {
                Nombre = "Vet Consulta " + Guid.NewGuid().ToString().Substring(0, 8),
                Telefono = "3002222222",
                Email = "vet2@test.com",
                Direccion = "Calle 9",
                FechaNacimiento = new DateTime(1985, 6, 6)
            };
            this.conexion.personas!.Add(this.personaVet);
            this.conexion.SaveChanges();

            this.veterinario = new veterinarios()
            {
                Persona = this.personaVet.Id,
                Especialidad = "Cirugia",
                RegistroProfesional = "VET-" + Guid.NewGuid().ToString().Substring(0, 8),
                FechaIngreso = DateTime.Now,
                Estado = "Activo",
                TelefonoLaboral = "6041111111"
            };
            this.conexion.veterinarios!.Add(this.veterinario);
            this.conexion.SaveChanges();

            this.cita = new citas()
            {
                Animal = this.animal.Id,
                Veterinario = this.veterinario.Id,
                Fecha = new DateTime(2026, 6, 1),
                Hora = new TimeSpan(9, 0, 0),
                Motivo = "Revision",
                Estado = "Atendida",
                Observaciones = null
            };
            this.conexion.citas!.Add(this.cita);
            this.conexion.SaveChanges();
        }

        public void Insertar()
        {
            this.entidad = new consultas()
            {
                Cita = this.cita!.Id,
                Animal = this.animal!.Id,
                Veterinario = this.veterinario!.Id,
                Fecha = new DateTime(2026, 6, 1),
                Diagnostico = "Animal saludable",
                Observaciones = "Sin novedades",
                PesoActual = 21,
                Temperatura = 38.5m
            };
            this.conexion.consultas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.consultas!
                .Where(x => x.Id == this.entidad!.Id)
                .ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Diagnostico = "Actualizado " + Guid.NewGuid().ToString().Substring(0, 8);

            var entry = this.conexion!.Entry<consultas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.consultas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }

        private void Limpiar()
        {
            this.conexion.citas!.Remove(this.cita!);
            this.conexion.SaveChanges();
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