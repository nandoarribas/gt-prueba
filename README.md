# GT-Test: Microservicio de Gestion de Renting

Este microservicio ha sido desarrollado como un ejercicio tecnico enfocado en un sistema de gestion de flota y alquiler de vehiculos bajo .NET 9.

## Arquitectura y Patrones de Diseno

El proyecto se ha estructurado siguiendo los principios de **Clean Architecture** y el patron **CQRS** (Command Query Responsibility Segregation).

* **MediatR:** Utilizado como mediador para desacoplar los controladores de la logica de aplicacion.
* **Patron Handler:** Implementacion de handlers especificos para cada Command y Query.
* **Persistencia en Memoria:** Uso de VehicleDbContext en memoria para permitir la ejecucion inmediata sin dependencias externas.

## Logica de Negocio y Reglas de Dominio

La logica reside en el **RentingManager**, donde se validan los requisitos:
1. **Restriccion por Cliente:** Un cliente no puede reservar mas de un vehiculo simultaneamente.
2. **Antiguedad de la Flota:** No se permiten vehiculos con mas de 5 años de antiguedad.

*Nota:* El ClientId se integra en la entidad Vehicle para simplificar el alcance del ejercicio.

## Decisiones Tecnicas y Refactorizacion

### Gestion de Excepciones
Se ha implementado el **BusinessExceptionFilter** existente. Esto permite lanzar excepciones de dominio sin usar bloques try-catch redundantes, centralizando la respuesta de errores.

### Uso de DTOs y AutoMapper
Se ha refactorizado el codigo para que el controlador no exponga entidades de dominio. Se hace uso de **VehicleDTO** y **AutoMapper** en la capa del Handler para transformar los datos y proteger la integridad del dominio.

### Telemetria y Logging
* **Logging:** Uso de Serilog para registrar eventos en consola.
* **Telemetria:** Registro de eventos de exito y fallo con detalles tecnicos (ej. motivo del fallo).

### Docker y Swagger
* Se ha forzado el modo **Development** en Docker para permitir el acceso a Swagger.
* Se ha optimizado SwashBuckle eliminando parametros de version manuales.

## Estrategia de Pruebas (Testing)

Se ha priorizado la calidad sobre la cantidad, proporcionando una base sólida de pruebas que demuestran el enfoque de verificación:

* **Alcance:** Se han implementado ejemplos representativos de tests unitarios y de infraestructura. Aunque el sistema permitiría una cobertura mucho mayor (específicamente en las reglas de validación de 5 años o duplicidad de alquileres en el `RentingManager`), se ha optado por ofrecer una "pincelada" técnica que demuestre el dominio de diferentes tipos de test.
* **Nomenclatura y Estilo:** Se ha seguido el estándar `Accion_Condicion_ResultadoEsperado` para mejorar la legibilidad. Debido a que el uso de guiones bajos (`_`) en nombres de métodos puede contravenir ciertas reglas de estilo por defecto, se ha incluido una configuración específica en el archivo **GlobalSuppressions** para permitir este formato descriptivo.
* **Tests de Infraestructura:** He tomado la decisión de mover ciertos tests unitarios al proyecto de Infraestructura. El motivo es que estos tests validan la lógica de creación y persistencia en la base de datos (aunque sea en memoria), y arquitectónicamente es el lugar donde debe verificarse que el estado del repositorio y el contexto de datos se comportan como se espera.

## Instrucciones de Ejecucion

Para construir y desplegar el contenedor:

```bash
docker build --no-cache -t gtmotive-app .
docker run -d -p 8080:8080 --name gtmotive-container gtmotive-app
```

Swagger disponible en: http://localhost:8080/swagger

## Autor y Repo

* Fernando Arribas Ramirez - [nandoarribas](https://github.com/nandoarribas/gt-prueba/)