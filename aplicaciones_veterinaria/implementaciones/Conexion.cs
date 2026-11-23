using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace lib_aplicaciones.implementaciones;

public class Conexion : DbContext, IConexion
{
    public string? StringConexion
    {
        get; set;
    }

    public Conexion()
    {
    }

    public Conexion(IConfiguration configuration)
    {
        StringConexion = configuration.GetConnectionString("Veterinaria");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        StringConexion ??= "Server=localhost\\SQLEXPRESS;Database=db_veterinaria;Integrated Security=True;TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(StringConexion, p => { });
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    public DbSet<animales>? animales
    {
        get; set;
    }
    public DbSet<citas>? citas
    {
        get; set;
    }
    public DbSet<clientes>? clientes
    {
        get; set;
    }
    public DbSet<compras>? compras
    {
        get; set;
    }
    public DbSet<consultas>? consultas
    {
        get; set;
    }
    public DbSet<detalleCompras>? detalleCompras
    {
        get; set;
    }
    public DbSet<detalleFacturas>? detalleFacturas
    {
        get; set;
    }
    public DbSet<empleados>? empleados
    {
        get; set;
    }
    public DbSet<especies>? especies
    {
        get; set;
    }
    public DbSet<facturas>? facturas
    {
        get; set;
    }
    public DbSet<fincas>? fincas
    {
        get; set;
    }
    public DbSet<historiasClinicas>? historiasClinicas
    {
        get; set;
    }
    public DbSet<personas>? personas
    {
        get; set;
    }
    public DbSet<productos>? productos
    {
        get; set;
    }
    public DbSet<proveedores>? proveedores
    {
        get; set;
    }
    public DbSet<razas>? razas
    {
        get; set;
    }
    public DbSet<servicios>? servicios
    {
        get; set;
    }
    public DbSet<tratamientos>? tratamientos
    {
        get; set;
    }
    public DbSet<vacunaciones>? vacunaciones
    {
        get; set;
    }
    public DbSet<veterinarios>? veterinarios
    {
        get; set;
    }

    public DbSet<roles>? roles { get; set; }
    public DbSet<usuarios>? usuarios { get; set; }
    public DbSet<auditorias>? auditorias { get; set; }

    // Id del usuario que esta usando la aplicacion (la API lo recibe de la pagina)
    public int UsuarioActual { get; set; }

    // Guarda los cambios y, si se sabe quien los hizo, anota una fila por cada cambio en la tabla auditorias
    public override int SaveChanges()
    {
        var cambios = new List<(object Entidad, string Accion)>();
        foreach (var entrada in ChangeTracker.Entries())
        {
            if (entrada.Entity is auditorias)
                continue;
            if (entrada.State == EntityState.Added)
                cambios.Add((entrada.Entity, "CREAR"));
            else if (entrada.State == EntityState.Modified)
                cambios.Add((entrada.Entity, "MODIFICAR"));
            else if (entrada.State == EntityState.Deleted)
                cambios.Add((entrada.Entity, "ELIMINAR"));
        }

        var resultado = base.SaveChanges();

        if (UsuarioActual != 0 && cambios.Count > 0)
        {
            foreach (var cambio in cambios)
            {
                var id = cambio.Entidad.GetType().GetProperty("Id")?.GetValue(cambio.Entidad) as int?;
                auditorias!.Add(new auditorias
                {
                    Usuario = UsuarioActual,
                    Fecha = DateTime.Now,
                    Accion = cambio.Accion,
                    Entidad = cambio.Entidad.GetType().Name,
                    Registro = id
                });
            }
            base.SaveChanges();
        }
        return resultado;
    }
}
