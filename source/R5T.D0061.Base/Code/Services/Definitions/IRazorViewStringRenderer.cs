using System;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.D0061
{
    /// <summary>
    /// Renders a Razor view to a string.
    /// </summary>
    [ServiceDefinitionMarker]
    public interface IRazorViewStringRenderer : IServiceDefinition
    {
        Task<string> Render<TModel>(string viewName, TModel model);
    }
}
