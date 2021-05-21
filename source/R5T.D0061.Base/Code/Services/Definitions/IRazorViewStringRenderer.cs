using System;
using System.Threading.Tasks;


namespace R5T.D0061
{
    /// <summary>
    /// Renders a Razor view to a string.
    /// </summary>
    public interface IRazorViewStringRenderer
    {
        Task<string> Render<TModel>(string viewName, TModel model);
    }
}
