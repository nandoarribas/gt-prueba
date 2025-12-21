// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Maintainability", "CA1515:Consider making public types internal", Justification = "For avoid xUnit1027.", Scope = "type", Target = "~T:GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure.TestServerCollectionFixture")]
[assembly: SuppressMessage("Maintainability", "CA1515:Considere la posibilidad de hacer que los tipos públicos sean internos", Justification = "<pendiente>", Scope = "type", Target = "~T:GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure.InfrastructureTestBase")]
[assembly: SuppressMessage("Maintainability", "CA1515:Considere la posibilidad de hacer que los tipos públicos sean internos", Justification = "<pendiente>", Scope = "type", Target = "~T:GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure.GenericInfrastructureTestServerFixture")]
[assembly: SuppressMessage("Naming", "CA1707:Los identificadores no deben contener caracteres de subrayado", Justification = "<pendiente>", Scope = "member", Target = "~M:GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure.VehicleRepositoryTests.GetByIdAsync_ShouldReturnNull_WhenVehicleDoesNotExist~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("Naming", "CA1707:Los identificadores no deben contener caracteres de subrayado", Justification = "<pendiente>", Scope = "member", Target = "~M:GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure.VehicleRepositoryTests.CreateAsync_ShouldPersistVehicleInDatabase~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("Naming", "CA1707:Los identificadores no deben contener caracteres de subrayado", Justification = "<pendiente>", Scope = "member", Target = "~M:GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure.VehicleRepositoryTests.UpdateAsync_ShouldPersistInDatabase~System.Threading.Tasks.Task")]
