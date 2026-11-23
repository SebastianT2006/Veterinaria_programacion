using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<IConexion, Conexion>();
builder.Services.AddScoped<IanimalesAplicacion, animalesAplicacion>();
builder.Services.AddScoped<IcitasAplicacion, citasAplicacion>();
builder.Services.AddScoped<IclientesAplicacion, clientesAplicacion>();
builder.Services.AddScoped<IcomprasAplicacion, comprasAplicacion>();
builder.Services.AddScoped<IconsultasAplicacion, consultasAplicacion>();
builder.Services.AddScoped<IdetalleComprasAplicacion, detalleComprasAplicacion>();
builder.Services.AddScoped<IdetalleFacturasAplicacion, detalleFacturasAplicacion>();
builder.Services.AddScoped<IempleadosAplicacion, empleadosAplicacion>();
builder.Services.AddScoped<IespeciesAplicacion, especiesAplicacion>();
builder.Services.AddScoped<IfacturasAplicacion, facturasAplicacion>();
builder.Services.AddScoped<IfincasAplicacion, fincasAplicacion>();
builder.Services.AddScoped<IhistoriasClinicasAplicacion, historiasClinicasAplicacion>();
builder.Services.AddScoped<IpersonasAplicacion, personasAplicacion>();
builder.Services.AddScoped<IproductosAplicacion, productosAplicacion>();
builder.Services.AddScoped<IproveedoresAplicacion, proveedoresAplicacion>();
builder.Services.AddScoped<IrazasAplicacion, razasAplicacion>();
builder.Services.AddScoped<IserviciosAplicacion, serviciosAplicacion>();
builder.Services.AddScoped<ItratamientosAplicacion, tratamientosAplicacion>();
builder.Services.AddScoped<IvacunacionesAplicacion, vacunacionesAplicacion>();
builder.Services.AddScoped<IveterinariosAplicacion, veterinariosAplicacion>();
builder.Services.AddScoped<IrolesAplicacion, rolesAplicacion>();
builder.Services.AddScoped<IusuariosAplicacion, usuariosAplicacion>();
builder.Services.AddScoped<IauditoriasAplicacion, auditoriasAplicacion>();
var app = builder.Build();
// La pagina envia en X-Usuario el id de quien esta conectado; se guarda para la auditoria
app.Use(async (context, next) =>
{
    if (int.TryParse(context.Request.Headers["X-Usuario"], out var idUsuario))
        context.RequestServices.GetRequiredService<IConexion>().UsuarioActual = idUsuario;
    await next();
});
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (ValidacionException ex)
    {
        context.Response.StatusCode = 422;
        context.Response.ContentType = "text/plain; charset=utf-8";
        await context.Response.WriteAsync(ex.Message);
    }
});
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => "Veterinaria API");
app.Run();
