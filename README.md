# Hotel Reservation

## Prerrequisitos

Antes de ejecutar el proyecto, asegúrate de tener instalados los siguientes requisitos:

- **SDK de .NET 9**: [Descargar .NET 9.0](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) (Linux, macOS y Windows)
- **Visual Studio 2022**: [Descargar Visual Studio](https://visualstudio.microsoft.com/downloads/) (Windows, Mac, Linux)

## Ejecución en Local (Windows)

Sigue estos pasos para ejecutar el proyecto en tu entorno local:

1. Clonar el repositorio de GitHub:
   ```sh
   git clone https://github.com/ingjuang/HotelReservation
   ```
2. Entrar a la carpeta del proyecto clonado y abrir el archivo `HotelReservation.sln` en Visual Studio.
3. Hacer clic en **Ejecutar por HTTPS** o presionar la tecla `F5` para iniciar la aplicación.

## Respuesta a Especificaciones

### Patrones de Diseño Implementados

En el desarrollo del proyecto, se han utilizado diversos patrones de diseño para mejorar la mantenibilidad, escalabilidad y modularidad del sistema:

- **CQRS (Command Query Responsibility Segregation)**: Separa las operaciones de lectura y escritura, optimizando el rendimiento de las consultas y transacciones en la base de datos.
- **Repository Pattern**: Separa la lógica de acceso a datos de la lógica de negocio, proporcionando una abstracción limpia para interactuar con la base de datos.
- **Unit of Work**: Coordina múltiples repositorios asegurando que las operaciones sean atómicas, manteniendo la consistencia de los datos.
- **Dependency Injection**: Facilita la gestión de instancias de servicios y repositorios, promoviendo un bajo acoplamiento y mejorando las pruebas unitarias.
- **Factory Pattern**: Simplifica la creación de objetos complejos, asegurando coherencia en la instanciación y encapsulando la lógica de creación.

### Arquitectura Orientada al Dominio (DDD)

El proyecto sigue los principios de **Domain-Driven Design (DDD)** y se estructura en diferentes capas:

- **Domain Layer**: Contiene las entidades, objetos de valor y reglas de negocio fundamentales, sin depender de la infraestructura.
- **Application Layer**: Implementa los casos de uso y actúa como intermediario entre la lógica de dominio y las interfaces externas. Aquí se aplica CQRS.
- **Infrastructure Layer**: Maneja la persistencia de datos y la comunicación con sistemas externos, como MongoDB.
- **API Layer**: Expone los endpoints para interactuar con el sistema.

---

📌 **Autor**: [ingjuang](https://github.com/ingjuang)

