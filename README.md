# gt-test
Technical exercise for GT
Cosas hechas

Describir el modelo, el repository, el controlador y las dependencias

Describir el uso del telemetry y como poder probarlo
Se ha declarado el telemetry para registrar los resultados de las llamadas a los endpoints
Podríamos haber usado una bbdd y guardar las transacciones, pero hemos dejado por simplicidad el uso con consola
Ejemplo, si hacemos una creación de vehiculo a la flota, y falla porque son mas de 5 años
[TELEMETRY EVENT]: AddVehicleToFleetFailed
  Prop: Reason = VehicleTooOld
  Prop: FabricationYear = 2000
  Prop: CurrentYear = 2025
[11:16:41 Information] Microsoft.AspNetCore.Mvc.Infrastructure.ObjectResultExecutor
Si el proceso es OK
[TELEMETRY EVENT]: AddVehicleToFleetSuccess
  Prop: VehicleId = C0001
[11:19:04 Information] Microsoft.AspNetCore.Mvc.StatusCodeResult
Executing StatusCodeResult, setting HTTP status code 200

Describir cómo declarar el swagger

Incluir el logging para complementar el telemetry

Se ha incluido logging en los controladores para registrar eventos importantes y errores.

Se va a hacer uso de DomainException en el RentingManager para gestionar errores de negocio y proporcionar mensajes claros. 
Se modifica con ello el controller también para hacer más sencillo de leer la lógica en caso de excepciones controladas
Se podría haber hecho uso de excepciones personalizadas del estilo VehicleTooOld o ClientRentalException pero al no ser tan complejo el proyecto creo que es mejor no meterse tan profundo

Uso del filtro deBusinessExceptionFilter  para dejar mas limipio
el controlador de try catch

Refactorizamos el controller para eliminar los logger de warning al ser redundantes y usarse en el filter otros logger

Uso del patron mediatR
    Se ha implementado el patrón MediatR para manejar las solicitudes y comandos dentro de la aplicación.
    con esto lo que hacemos es quitar el uso desde el controller del manager directamente, y sea mediante el mediatR que usemos los
    handlers para gestionar las peticiones.
    Como se puede observar hemos dividido las llamadas de consulta (Queries) de las acciones post (Commands) para mantener el patrón CQRS, del principio de segregación 
    de responsabilidad entre consultas y comandos

Para el logger, podríamos haber hecho uso de Azure applicationInsights, pero para hacerlo correr rápido y sin dependencias lo dejamos tal y como lo hemos puesto, tampoco tenemos suscripción de azure para probarlo
Como inicialmente tomamos la decisión también de cambiar el telemetry para pintar por logs, conseguimos que el proyecto sea independiente y se pueda ejecutar sin dependencias externas

Decisiones técnicas
Uso de Development al crear la imagen de docker: 
Se realiza este cambio para evitar que el código de errores en el futuro y para que sea más fácil llevar a cabo la trazabilidad (logs y derivados...)
Se elimina de swagger el uso de OpenApiVersion porque SwashBuckle en la versión usada no necesita indicar
de manera explícita ese parámetro, ya que lo maneja internamente.
Por falta de tiempo se me pasó la creación de un DTO para la extracción de información del vehiculo de Repositorio, para evitar pasar información sensible 
en caso de que la tuviera desde la MemoryDB al usuario (imagina que hay in VIN o cosas asi que no serían necesarias para mostrar...)

Tests
Se podrían haber creado muchos más tests (ejemplo en rentingMAnager las reglas citadas del enunciado de 5 años de antiguedad o vehiculo ya alquilado), pero 
he dejado una pincelada de ejemplos de tests, tanto unitarios como infra
En el caso de infra, se han movido tests unitarios a infra debido a que se utiliza lógica de creación y guardado en bbdd y en esta parte es donde se debería probarlo

Para el test funcional se utiliza el tipo Unit para permitir que los comandos que no devuelven datos sean compatibles con la infraestructura genérica de MediatR y de los tests. 
En vez de void, Unit es un tipo que permite todas las peticiones de forma homogénea bajo la interfaz IRequestHandler<TRequest, TResponse>, facilitando la automatización de los Scopes y la inyección de dependencias.

