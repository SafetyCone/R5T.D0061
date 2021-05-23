using System;
using System.Threading.Tasks;


namespace R5T.D0061
{
    public static class IRazorViewStringRendererExtensions
    {
        public static Task<string> Render(this IRazorViewStringRenderer razorViewStringRenderer, string viewName)
        {
            var rendering = razorViewStringRenderer.Render<object>(viewName, null);
            return rendering;
        }
    }
}
