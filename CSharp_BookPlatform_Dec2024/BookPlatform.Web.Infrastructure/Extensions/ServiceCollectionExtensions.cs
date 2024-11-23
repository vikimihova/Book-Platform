using System.Reflection;

using BookPlatform.Data.Models;
using BookPlatform.Data.Repository;
using BookPlatform.Data.Repository.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterRepositories(this IServiceCollection services, Assembly modelsAssembly)
        {
            // ignore ApplicationUser
            Type[] typesToExclude = new Type[] { typeof(ApplicationUser) };

            // get all other types
            Type[] modelTypes = modelsAssembly.GetTypes()
                .Where(t => !t.IsAbstract
                        && !t.IsInterface
                        && !t.Name.ToLower().EndsWith("attribute")
                        && !typesToExclude.Contains(t))
                .ToArray();

            if (modelTypes.Any())
            {
                // register repository for each type
                foreach (Type type in modelTypes)
                {
                    // for each type:
                    // get id property info
                    PropertyInfo idPropInfo = type.GetProperty("Id")!;

                    // get type of repository interface and class
                    Type repositoryInterface = typeof(IRepository<,>);
                    Type repositoryInstanceType = typeof(BaseRepository<,>);

                    // define repository interface and class construction arguments
                    Type[] constructArgs = new Type[2];
                    constructArgs[0] = type;

                    if (idPropInfo == null) 
                    {
                        constructArgs[1] = typeof(object); // for example if it's a composite key
                    }
                    else
                    {
                        constructArgs[1] = idPropInfo.PropertyType;
                    }

                    // create repository interface and class instances with defined construction arguments
                    repositoryInterface = repositoryInterface.MakeGenericType(constructArgs);
                    repositoryInstanceType = repositoryInstanceType.MakeGenericType(constructArgs);

                    // add to services
                    services.AddScoped(repositoryInterface, repositoryInstanceType);
                }
            }            
        }

        public static void RegisterUserDefinedServices(this IServiceCollection services, Assembly serviceAssembly)
        {
            // get all interface types
            Type[] serviceInterfaceTypes = serviceAssembly
                .GetTypes()
                .Where(t => t.IsInterface)
                .ToArray();

            // get all service types
            Type[] serviceTypes = serviceAssembly
                .GetTypes()
                .Where(t => !t.IsInterface && !t.IsAbstract &&
                        t.Name.ToLower().EndsWith("service"))
                .ToArray();

            if (serviceInterfaceTypes.Any())
            {
                foreach (Type serviceInterfaceType in serviceInterfaceTypes)
                {
                    // find the service corresponding to the service interface
                    Type? serviceType = serviceTypes
                        .SingleOrDefault(t => "i" + t.Name.ToLower() == serviceInterfaceType.Name.ToLower());

                    // no corresponding service for the service interface
                    if (serviceType == null)
                    {
                        throw new NullReferenceException($"Service type could not be obtained for the service {serviceInterfaceType.Name}");
                    }

                    // register service interface and service
                    services.AddScoped(serviceInterfaceType, serviceType);
                }
            }            
        }
    }
}
