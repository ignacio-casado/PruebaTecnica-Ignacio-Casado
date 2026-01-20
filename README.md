# Sistema de Gestión Académica - Prueba Técnica

Este proyecto es una Web API desarrollada en .NET 8 que gestiona Usuarios, Roles e Inscripciones a Cursos, siguiendo una arquitectura de capas (Controllers, Services, Repositories).

---

## 🛠️ Configuración de Persistencia (Entity Framework Code First)

El proyecto utiliza el enfoque **Code First**, lo que significa que la base de datos se genera y actualiza a partir del código C#.

### Pasos para replicar la Base de Datos:

1. **Configurar la cadena de conexión**: 
   Verifica el archivo `appsettings.json` y asegúrate de que la propiedad `DefaultConnection` apunte a tu servidor local de SQL Server. Debes ingresar el sevidor donde lo quieras levantar en Server= y en Database= tu base donde quieras los datos. 
<img width="1275" height="77" alt="image" src="https://github.com/user-attachments/assets/74b33cb8-29fb-48a7-a207-a20586e27eaa" />

2. **Ejecutar Migraciones**:
   Abre la "Consola de Administrador de Paquetes" en Visual Studio (Herramientas > Administrador de Paquetes NuGet) y ejecuta:
   
   > Add-Migration InitialCreate
   > Update-Database

Esto creará automáticamente las tablas, relaciones e índices únicos definidos en el `AppDbContext`.

---

## 🔐 Lógica de Roles y Permisos

El sistema implementa un control de acceso basado en el `RolId` enviado en los encabezados (`Headers`) de las peticiones HTTP.

| Rol | ID | Permisos Clave |
| :--- | :--- | :--- |
| **Director** | 1 | Alta de usuarios, creación de cursos, creación de roles, inscripciones. |
| **Profesor** | 2 | Consulta de alumnos e inscripción de alumnos a cursos. |
| **Alumno** | 3 | Solo lectura de sus propios datos (Sujeto a inscripciones). |

### Reglas de Negocio Implementadas:
- **Seguridad en Servicios**: Los métodos de creación validan que el `idRol` del ejecutor sea el autorizado, de lo contrario lanzan una `UnauthorizedAccessException` (403 Forbidden).
- **Validación de Inscripción**: Solo se permite inscribir a usuarios cuyo `RolId` sea exactamente **3 (Alumno)**.
- **Identificación por Documento**: Al crear un curso, el sistema solicita el **Número de Documento del Profesor** en lugar de su ID interno, validando que el documento exista y pertenezca efectivamente a un docente.

---

## 🚀 Estructura del Proyecto

- **Controllers**: Gestionan las peticiones HTTP y traducen excepciones en códigos de estado (200, 201, 400, 403, 404, 500).
- **Services**: Contienen toda la lógica de negocio y validaciones de seguridad.
- **Repositories**: Encargados de la comunicación con la base de datos utilizando LINQ y Eager Loading (`.Include()`) para cargar relaciones Muchos a Muchos.
- **DTOs**: Objetos de transferencia de datos para desacoplar las entidades de la base de datos de las respuestas de la API.

---

## 📌 Notas de Uso en Swagger

Para probar los endpoints protegidos:
1. Localiza el campo **idRol** en el Header de la petición.
2. Ingresa `1` para simular acciones de Director o `2` para Profesor.
3. El sistema validará automáticamente si tienes el permiso para realizar dicha acción.
