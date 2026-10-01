using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class tratamientosPruebas
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
        private consultas? consulta = null;
        private productos? producto = null;
        private tratamientos? entidad = null;

        public tratamientosPruebas()
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
            this.persona = new personas()
            {
                Nombre = "Persona Trat " + Guid.NewGuid().ToString().Substring(0, 8),
                Telefono = "3000000000",
                Email = "trat@test.com",
                Direccion = "Calle 12",
                FechaNacimiento = new DateTime(1980, 6, 6)
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
                Nombre = "Finca Trat " + Guid.NewGuid().ToString().Substring(0, 8),
                Direccion = "Rionegro",
                AreaHectareas = 7,
                TipoProduccion = "Lechera",
                Estado = "Activa"
            };
            this.conexion.fincas!.Add(this.finca);
            this.conexion.SaveChanges();

            this.especie = new especies()
            {
                Nombre = "Especie Trat " + Guid.NewGuid().ToString().Substring(0, 8),
                Descripcion = "Especie prueba"
            };
            this.conexion.especies!.Add(this.especie);
            this.conexion.SaveChanges();

            this.raza = new razas()
            {
                Especie = this.especie.Id,
                Nombre = "Raza Trat " + Guid.NewGuid().ToString().Substring(0, 8),
                TamanoPromedio = 30,
                PesoPromedio = 12,
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
                Nombre = "Animal Trat " + Guid.NewGuid().ToString().Substring(0, 8),
                Sexo = "Macho",
                FechaNacimiento = new DateTime(2022, 9, 9),
                Color = "Negro",
                Peso = 18,
                Estado = "Activo"
            };
            this.conexion.animales!.Add(this.animal);
            this.conexion.SaveChanges();

            this.personaVet = new personas()
            {
                Nombre = "Vet Trat " + Guid.NewGuid().ToString().Substring(0, 8),
                Telefono = "3003333333",
                Email = "vet3@test.com",
                Direccion = "Calle 13",
                FechaNacimiento = new DateTime(1985, 7, 7)
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
                TelefonoLaboral = "6042222222"
            };
            this.conexion.veterinarios!.Add(this.veterinario);
            this.conexion.SaveChanges();

            this.cita = new citas()
            {
                Animal = this.animal.Id,
                Veterinario = this.veterinario.Id,
                Fecha = new DateTime(2026, 7, 1),
                Hora = new TimeSpan(11, 0, 0),
                Motivo = "Control",
                Estado = "Atendida",
                Observaciones = null
            };
            this.conexion.citas!.Add(this.cita);
            this.conexion.SaveChanges();

            this.consulta = new consultas()
            {
                Cita = this.cita.Id,
                Animal = this.animal.Id,
                Veterinario = this.veterinario.Id,
                Fecha = new DateTime(2026, 7, 1),
                Diagnostico = "Gastritis leve",
                Observaciones = null,
                PesoActual = 18.5m,
                Temperatura = 39.0m
            };
            this.conexion.consultas!.Add(this.consulta);
            this.conexion.SaveChanges();

            this.producto = new productos()
            {
                Nombre = "Medicamento Trat " + Guid.NewGuid().ToString().Substring(0, 8),
                Tipo = "Medicamento",
                Precio = 75000,
                Stock = 50,
                Descripcion = "Medicamento prueba"
            };
            this.conexion.productos!.Add(this.producto);
            this.conexion.SaveChanges();
        }

        public void Insertar()
        {
            this.entidad = new tratamientos()
            {
                Animal = this.animal!.Id,
                Consulta = this.consulta!.Id,
                Producto = this.producto!.Id,
                Dosis = 1.0m,
                Frecuencia = "Cada 12 horas",
                Duracion = 7,
                Indicaciones = "Administrar despues de comer",
                FechaInicio = new DateTime(2026, 7, 1)
            };
            this.conexion.tratamientos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.tratamientos!
                .Where(x => x.Id == this.entidad!.Id)
                .ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Indicaciones = "Actualizado " + Guid.NewGuid().ToString().Substring(0, 8);

            var entry = this.conexion!.Entry<tratamientos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.tratamientos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }

        private void Limpiar()
        {
            this.conexion.productos!.Remove(this.producto!);
            this.conexion.SaveChanges();
            this.conexion.consultas!.Remove(this.consulta!);
            this.conexion.SaveChanges();
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