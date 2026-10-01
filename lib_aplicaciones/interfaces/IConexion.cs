using lib_aplicaciones.entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace lib_aplicaciones.interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }

        DbSet<personas>? personas { get; set; }
        DbSet<clientes>? clientes { get; set; }
        DbSet<veterinarios>? veterinarios { get; set; }
        DbSet<empleados>? empleados { get; set; }
        DbSet<especies>? especies { get; set; }
        DbSet<razas>? razas { get; set; }
        DbSet<fincas>? fincas { get; set; }
        DbSet<animales>? animales { get; set; }
        DbSet<citas>? citas { get; set; }
        DbSet<consultas>? consultas { get; set; }
        DbSet<historiasClinicas>? historiasClinicas { get; set; }
        DbSet<proveedores>? proveedores { get; set; }
        DbSet<productos>? productos { get; set; }
        DbSet<vacunaciones>? vacunaciones { get; set; }
        DbSet<tratamientos>? tratamientos { get; set; }
        DbSet<compras>? compras { get; set; }
        DbSet<detalleCompras>? detalleCompras { get; set; }
        DbSet<servicios>? servicios { get; set; }
        DbSet<facturas>? facturas { get; set; }
        DbSet<detalleFacturas>? detalleFacturas { get; set; }

        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}