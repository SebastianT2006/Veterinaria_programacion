using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class vacunacionesPruebas
    {
        private IConexion conexion;
        private personas? persona = null;
        private clientes? cliente = null;
        private especies? especie = null;
        private razas? raza = null;
        private fincas? finca = null;
        private animales? animal = null;
        private productos? producto = null;
        private vacunaciones? entidad = null;

        public vacunacionesPruebas()
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
                Nombre = "Persona Vac " + Guid.NewGuid().ToString().Substring(0, 8),
                Telefono = "3000000000",
                Email = "vac@test.com",
                Direccion = "Calle 11",
                FechaNacimiento = new DateTime(1980, 5, 5)
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
                Nombre = "Finca Vac " + Guid.NewGuid().ToString().Substring(0, 8),
                Direccion = "Rionegro",
                AreaHectareas = 6,
                TipoProduccion = "Ganadera",
                Estado = "Activa"
            };
            this.conexion.fincas!.Add(this.finca);
            this.conexion.SaveChanges();

            this.especie = new especies()
            {
                Nombre = "Especie Vac " + Guid.NewGuid().ToString().Substring(0, 8),
                Descripcion = "Especie prueba"
            };
            this.conexion.especies!.Add(this.especie);
            this.conexion.SaveChanges();

            this.raza = new razas()
            {
                Especie = this.especie.Id,
                Nombre = "Raza Vac " + Guid.NewGuid().ToString().Substring(0, 8),
                TamanoPromedio = 30,
                PesoPromedio = 10,
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
                Nombre = "Animal Vac " + Guid.NewGuid().ToString().Substring(0, 8),
                Sexo = "Macho",
                FechaNacimiento = new DateTime(2022, 8, 8),
                Color = "Cafe",
                Peso = 15,
                Estado = "Activo"
            };
            this.conexion.animales!.Add(this.animal);
            this.conexion.SaveChanges();

            this.producto = new productos()
            {
                Nombre = "Vacuna Prueba " + Guid.NewGuid().ToString().Substring(0, 8),
                Tipo = "Vacuna",
                Precio = 45000,
                Stock = 100,
                Descripcion = "Vacuna de prueba"
            };
            this.conexion.productos!.Add(this.producto);
            this.conexion.SaveChanges();
        }

        public void Insertar()
        {
            this.entidad = new vacunaciones()
            {
                Animal = this.animal!.Id,
                Producto = this.producto!.Id,
                Fecha = new DateTime(2026, 1, 10),
                Observacion = "Vacuna aplicada",
                ProximaFecha = new DateTime(2027, 1, 10),
                Lote = "LOT-" + Guid.NewGuid().ToString().Substring(0, 8),
                Dosis = 1.0m
            };
            this.conexion.vacunaciones!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.vacunaciones!
                .Where(x => x.Id == this.entidad!.Id)
                .ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Observacion = "Actualizado " + Guid.NewGuid().ToString().Substring(0, 8);

            var entry = this.conexion!.Entry<vacunaciones>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.vacunaciones!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }

        private void Limpiar()
        {
            this.conexion.productos!.Remove(this.producto!);
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