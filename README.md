# Veterinaria

Solución organizada siguiendo la arquitectura del proyecto de referencia de horarios:

- `aplicaciones_veterinaria`: entidades, conexión SQL Server, interfaces y operaciones CRUD.
- `servicio_veterinaria`: API REST con controladores para las entidades.
- `presentaciones_veterinaria`: comunicación HTTP y capa de presentación.
- `front_veterinaria`: frontend ASP.NET Core Razor Pages con pantallas CRUD.
- `pruebas_veterinaria`: pruebas existentes del proyecto.

El modelo conserva las 20 tablas y relaciones definidas en `aplicaciones_veterinaria/db_veterinaria.sql`.

## Configuración y ejecución

1. Crear la base de datos ejecutando `aplicaciones_veterinaria/db_veterinaria.sql` en SQL Server.
2. Ajustar la cadena `ConnectionStrings:Veterinaria` en `servicio_veterinaria/appsettings.json` si la instancia SQL Server no es `localhost` o requiere otras credenciales.
3. Abrir `veterinaria.slnx` en Visual Studio o restaurar paquetes con `dotnet restore`.
4. Iniciar `servicio_veterinaria` y `front_veterinaria`. La API escucha en `http://localhost:5209`; el frontend en `http://localhost:5179`.

La URL de la API puede cambiarse mediante la variable de entorno `VETERINARIA_API_URL`.
