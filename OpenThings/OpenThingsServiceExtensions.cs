using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace OpenThings
{
    /// <summary>
    /// Add the OpenThings types to the <see cref="IServiceCollection"/>
    /// </summary>
    public static class OpenThingsServiceExtensions
    {
        /// <summary>
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/> to add the</param>
        /// <returns>The <see cref="IServiceCollection"/></returns>
        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddOpenThings(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IParameters, DefaultParameters>();
            serviceCollection.AddSingleton<IOpenThingsDecoder, OpenThingsDecoder>();
            serviceCollection.AddSingleton<IOpenThingsEncoder, OpenThingsEncoder>();

            return serviceCollection;
        }
    }
}
