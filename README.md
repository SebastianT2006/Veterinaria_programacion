# Sistema de Gestión Veterinaria

**Integrantes:**
- Sebastian Torres
- Angelly Rivera Orrego

## Descripción

Aplicación web académica para administrar la información de una veterinaria: personas, clientes, veterinarios, empleados, especies, razas, fincas y animales; además de citas, consultas, historias clínicas, vacunaciones, tratamientos, proveedores, inventario, compras, servicios y facturación.

Incluye inicio de sesión, roles, registro de auditoría y exportación de algunos listados a PDF.

La aplicación está organizada por capas y se comunica con una API REST que accede a una base de datos relacional en SQL Server.

## Tecnologías utilizadas

- **C# y .NET 10 / ASP.NET Core:** desarrollo de la aplicación y de la API REST.
- **Razor Pages, HTML, CSS y Bootstrap:** interfaz web.
- **Microsoft SQL Server:** almacenamiento de datos.
- **Entity Framework Core 10 (proveedor SQL Server):** acceso a datos y operaciones CRUD.
- **HTTP / JSON:** comunicación entre interfaz, presentación y API.
- **QuestPDF:** generación de reportes en PDF.
- **MSTest:** pruebas automatizadas.
- **SHA-256 (System.Security.Cryptography):** hash de contraseñas implementado en la clase Cifrado. Es una implementación académica; para un sistema en producción debe utilizarse un algoritmo específico para contraseñas, como Argon2id o BCrypt.

## Estructura del proyecto

| Proyecto | Función |
| --- | --- |
| aplicaciones_veterinaria | Entidades, conexión a SQL Server, interfaces y lógica de acceso a datos y validaciones. |
| servicio_veterinaria | API REST con los controladores. |
| presentaciones_veterinaria | Comunicación HTTP con la API. |
| front_veterinaria | Páginas web y reportes PDF. |
| pruebas_veterinaria | Pruebas automatizadas. |

## Requisitos para ejecutar

1. Tener instalado **.NET SDK 10** y, opcionalmente, **Visual Studio** compatible con .NET 10.
2. Tener **SQL Server** en ejecución y acceso suficiente para crear una base de datos.
3. Crear la base de datos ejecutando **aplicaciones_veterinaria/db_veterinaria.sql** en SQL Server. Este script crea las tablas y los registros iniciales de roles y usuarios.
4. Revisar ConnectionStrings:Veterinaria en **servicio_veterinaria/appsettings.json**. El proyecto viene configurado para localhost\\SQLEXPRESS, base db_veterinaria y autenticación integrada de Windows; modificarlo si la instancia o forma de autenticación es distinta.
5. Restaurar las dependencias y ejecutar **la API y el frontend**, ambos necesarios para utilizar la aplicación desde el navegador.

Desde la carpeta del proyecto se pueden iniciar en terminales diferentes:

```bash
dotnet run --project servicio_veterinaria --launch-profile http
```

```bash
dotnet run --project front_veterinaria --launch-profile http
```

Con los perfiles incluidos, la API utiliza **http://localhost:5209** y la interfaz **http://localhost:5179**. Si cambia la dirección de la API, configurar la variable de entorno **VETERINARIA_API_URL** para el frontend.

**Importante:** Usuarios_Roles_Auditorias.sql es un script complementario para agregar estas tablas a una base de datos existente que aún no las tenga; no es necesario ejecutarlo después del db_veterinaria.sql completo.

## Condiciones y dependencias para registrar información

Para evitar errores, se deben crear primero los registros de los que dependen otros módulos. Estas condiciones se basan en las relaciones de las tablas y en las validaciones implementadas en el código.

| Módulo | Condiciones necesarias |
| --- | --- |
| **Personas** | Se pueden registrar directamente; requieren nombre, teléfono, correo, dirección y una fecha de nacimiento válida. |
| **Clientes** | Debe existir una **persona** para asociarla al cliente. Una misma persona no se puede registrar dos veces como cliente. |
| **Veterinarios** | Debe existir una **persona**. Una misma persona no se puede registrar dos veces como veterinario. |
| **Empleados** | Debe existir una **persona**. Una misma persona no se puede registrar dos veces como empleado. |
| **Especies** | Se registran antes de las razas. |
| **Razas** | Debe existir una **especie** a la que pertenezca la raza. |
| **Fincas** | Debe existir un **cliente** al que pertenezca la finca. |
| **Animales** | Deben existir una **raza**, un **cliente** y una **finca**. La especie del animal se obtiene indirectamente a través de la raza; no se selecciona como relación independiente en la tabla de animales. |
| **Citas** | Deben existir el **animal** y el **veterinario** que atenderá la cita. |
| **Consultas** | Deben existir una **cita**, un **animal** y un **veterinario**. Una cita solo puede tener **una consulta** registrada. |
| **Historias clínicas** | Debe existir un **animal**. Cada animal puede tener **una sola historia clínica**. |
| **Proveedores** | Se registran directamente; se necesitan para crear compras. |
| **Productos** | Se registran directamente; se utilizan en vacunaciones, tratamientos, detalles de compras y detalles de facturas. |
| **Vacunaciones** | Deben existir el **animal** y el **producto** utilizado. |
| **Tratamientos** | Deben existir el **animal**, la **consulta** y el **producto** relacionado. |
| **Compras** | Debe existir un **proveedor**. |
| **Detalles de compras** | Deben existir una **compra** y un **producto**. |
| **Servicios** | Se registran directamente; pueden asociarse a detalles de facturas. |
| **Facturas** | Debe existir un **cliente**. |
| **Detalles de facturas** | Debe existir una **factura** y se debe indicar **al menos un producto o un servicio** existente. |
| **Roles** | Se pueden registrar directamente; su nombre debe ser único. |
| **Usuarios** | Debe existir un **rol**. El nombre de usuario debe ser único y al crearlo se debe ingresar una contraseña. |
| **Auditorías** | Se generan registros de actividad y acceso. La relación con el usuario puede ser nula, por ejemplo, ante un inicio de sesión fallido. |

### Orden recomendado para registrar datos

1. Crear las **personas**, **especies**, **proveedores**, **productos**, **servicios** y, si se necesitan, **roles**.
2. Crear **clientes, veterinarios y empleados** a partir de personas existentes; crear **razas** a partir de especies.
3. Crear las **fincas** asociadas a clientes.
4. Crear los **animales**, seleccionando raza, cliente y finca.
5. Crear **citas** y, a partir de ellas, **consultas**; después registrar **historias clínicas**, **vacunaciones** y **tratamientos** según corresponda.
6. Para las operaciones comerciales, registrar **compras** asociadas a proveedores y luego sus **detalles**; registrar **facturas** asociadas a clientes y luego sus **detalles**.

## Otras validaciones importantes

- **Datos obligatorios:** el sistema comprueba diversos campos de texto, como nombres, estados, descripciones, motivos, diagnósticos, lotes y otros según el módulo.
- **Fechas:** las fechas de nacimiento de personas y animales no pueden ser posteriores al día actual ni anteriores al 1 de enero de 1900. Varios campos de fecha operativa admiten el intervalo entre el 1 de enero de 1900 y el 6 de junio de 2079.
- **Cantidades positivas:** el peso de un animal, el peso actual en una consulta, las dosis, la duración de tratamientos y servicios, el área de una finca, los valores promedio de raza, la esperanza de vida y las cantidades en detalles de compras y facturas deben ser mayores que cero cuando su validación así lo exige.
- **Valores no negativos:** los precios, el total de compras y facturas, el salario y el stock de productos no pueden ser negativos.
- **Registros únicos:** una persona no puede repetirse dentro de clientes, veterinarios o empleados; un animal no puede tener dos historias clínicas y una cita no puede tener dos consultas. Los nombres de roles y de usuarios tampoco se pueden duplicar.
- **Eliminación de registros relacionados:** las claves foráneas de SQL Server pueden impedir eliminar, por ejemplo, una especie que tenga razas, una raza que tenga animales o un cliente con fincas, animales o facturas. Primero deben resolverse las dependencias correspondientes.
- **Roles:** no se permite eliminar un rol que tenga usuarios asociados.
- **Usuarios y auditoría:** no se permite eliminar un usuario que ya tenga registros de auditoría; en ese caso se debe cambiar su estado a **Inactivo**.
- **Cambio de contraseña:** al editar un usuario, si la contraseña se deja vacía se conserva la anterior; si se ingresa una nueva, se calcula su hash SHA-256.

> **Nota:** las referencias entre registros se comprueban, pero no todas las reglas de coherencia entre relaciones están implementadas. Por ejemplo, que un animal pertenezca a una finca del mismo cliente, o que el animal y veterinario de una consulta coincidan con los de su cita, no debe darse por garantizado únicamente por las claves foráneas. Los datos se deben seleccionar coherentemente.

## Inicio de sesión, permisos y reportes

- El sistema requiere iniciar sesión para acceder a las ventanas protegidas.
- El rol **Administrador** puede acceder a todas las ventanas.
- El rol **Usuario** tiene acceso a las ventanas de personas, clientes, fincas, animales, razas, citas, consultas, historias clínicas, vacunaciones y tratamientos; en esas ventanas **no puede borrar** registros desde los manejadores protegidos.
- El sistema registra eventos de ingreso e intentos fallidos en auditoría.
- Hay **exportación a PDF** implementada para los listados de **Personas, Animales y Auditorías** mediante QuestPDF.
