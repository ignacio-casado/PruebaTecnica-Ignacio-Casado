📘 Documentación del Sistema: Roles, Persistencia Y Despliegue
Este proyecto utiliza una arquitectura basada en Servicios y Repositorios, gestionando la persistencia de datos mediante Entity Framework Core con el enfoque Code First.

1. Gestión de Roles y Permisos
El sistema implementa un control de acceso basado en lógica de negocio (RBAC simple) donde las acciones están restringidas según el RolId del usuario que ejecuta la petición:

Director (RolId: 1): Es el administrador del sistema. Único usuario con permisos para:

Dar de alta nuevos usuarios (Profesores y Alumnos).

Crear nuevos Cursos.

Crear nuevos Roles.

Inscribir alumnos en cursos.

Profesor (RolId: 2): Tiene permisos operativos. Puede:

Consultar listados de alumnos.

Inscribir alumnos en sus respectivos cursos.

Alumno (RolId: 3): Es el sujeto de las inscripciones. No posee permisos de edición o creación sobre otros recursos.

Nota Técnica: Las validaciones de seguridad se realizan a nivel de Capa de Servicio, lanzando excepciones de tipo UnauthorizedAccessException o retornando objetos DefaultResponse con estado 403 Forbidden cuando el idRol proporcionado en los encabezados no cumple con los requisitos.

2. Configuración de Entity Framework (Code First)
La base de datos se genera automáticamente a partir de las clases del modelo. La configuración se centraliza en el AppDbContext.

Relaciones Implementadas:
Muchos a Muchos (Alumnos ↔ Cursos): Implementada mediante una propiedad de colección en ambas entidades. Entity Framework genera automáticamente la tabla intermedia de inscripciones.

Uno a Muchos (Rol ↔ Usuarios): Cada usuario posee un único rol, mientras que un rol puede pertenecer a múltiples usuarios.

Uno a Muchos (Profesor ↔ Cursos): Un curso tiene un profesor titular asignado mediante su ProfesorId.

Pasos para la Configuración Inicial:
Si acabas de clonar el repositorio, sigue estos pasos en la Consola de Administrador de Paquetes (NuGet):

Crear la Migración: Genera el código necesario para crear las tablas basadas en los modelos actuales.

PowerShell
Add-Migration InitialCreate
Actualizar la Base de Datos: Aplica las migraciones a tu instancia local de SQL Server.

PowerShell
Update-Database
Restricciones de Integridad:
En el método OnModelCreating, se han configurado reglas de validación adicionales para asegurar la consistencia de los datos:

Índices Únicos: El nombre de los Roles y el Email de los Usuarios están marcados como únicos para evitar duplicados.

Borrado en Cascada: Configurado para proteger la integridad referencial entre cursos e inscripciones.

3. Ejecución y Pruebas
Una vez aplicada la migración, puedes utilizar Swagger para probar los endpoints:

Asegúrate de enviar el idRol correcto en el Header de las peticiones protegidas.

Para crear un curso, primero asegúrate de tener un usuario con Rol 2 (Profesor) creado en la base de datos.
