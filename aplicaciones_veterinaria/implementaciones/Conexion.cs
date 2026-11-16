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
}
