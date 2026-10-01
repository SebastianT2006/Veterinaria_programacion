using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class historiasClinicasPruebas
    {
        private IConexion conexion;
        private personas? persona = null;
        private clientes? cliente = null;
        private especies? especie = null;
        private razas? raza = null;
        private fincas? finca = null;
        private animales? animal = null;
        private historiasClinicas? entidad = null;

        public historiasClinicasPruebas()
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
                Nombre = "Persona HC " + Guid.NewGuid().ToString().Substring(0, 8),
                Telefono = "3000000000",
                Email = "hc@test.com",
                Direccion = "Calle 10",
                FechaNacimiento = new DateTime(1980, 4, 4)
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
                Nombre = "Finca HC " + Guid.NewGuid().ToString().Substring(0, 8),
                Direccion = "Rionegro",
                AreaHectareas = 12,
                TipoProduccion = "Mixta",
                Estado = "Activa"
            };
            this.conexion.fincas!.Add(this.finca);
            this.conexion.SaveChanges();

            this.especie = new especies()
            {
                Nombre = "Especie HC " + Guid.NewGuid().ToString().Substring(0, 8),
                Descripcion = "Especie prueba"
            };
            this.conexion.especies!.Add(this.especie);
            this.conexion.SaveChanges();

            this.raza = new razas()
            {
                Especie = this.especie.Id,
                Nombre = "Raza HC " + Guid.NewGuid().ToString().Substring(0, 8),
                TamanoPromedio = 30,
                PesoPromedio = 8,
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
                Nombre = "Animal HC " + Guid.NewGuid().ToString().Substring(0, 8),
                Sexo = "Hembra",
                FechaNacimiento = new DateTime(2023, 7, 7),
                Color = "Blanco",
                Peso = 4,
                Estado = "Activo"
            };
            this.conexion.animales!.Add(this.animal);
            this.conexion.SaveChanges();
        }

        public void Insertar()
        {
            this.entidad = new historiasClinicas()
            {
                Animal = this.animal!.Id,
                Antecedentes = "Ninguno",
                Alergias = "Ninguna conocida",
                Observaciones = "Animal saludable"
            };
            this.conexion.historiasClinicas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.historiasClinicas!
                .Where(x => x.Id == this.entidad!.Id)
                .ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Observaciones = "Actualizado " + Guid.NewGuid().ToString().Substring(0, 8);

            var entry = this.conexion!.Entry<historiasClinicas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.historiasClinicas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }

        private void Limpiar()
        {
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