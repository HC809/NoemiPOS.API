using NoemiPOS.Application.Abstractions.Messaging;
using NoemiPOS.Domain.Abstractions;
using NoemiPOS.Infraestructure;
using System.Reflection;

namespace NoemiPOS.ArchitectureTests.Infraestructure;
public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(Entity).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(IBaseCommand).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(ApplicationDbContext).Assembly;
    protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
}
