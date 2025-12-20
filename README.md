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
Se elimina de swaggerExtensions el uso de OpenApi