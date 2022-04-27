using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

using R5T.T0064;


namespace R5T.D0061
{
    // Source: https://github.com/aspnet/Entropy/blob/master/samples/Mvc.RenderViewToString/RazorViewToStringRenderer.cs
    [ServiceImplementationMarker]
    public class RazorViewStringRenderer : IRazorViewStringRenderer, IServiceImplementation
    {
        private IRazorViewEngine RazorViewEngine { get; }
        private IServiceProvider ServiceProvider { get; }
        private ITempDataProvider TempDataProvider { get; }


        public RazorViewStringRenderer(
            IRazorViewEngine razorViewEngine,
            IServiceProvider serviceProvider,
            ITempDataProvider tempDataProvider)
        {
            this.RazorViewEngine = razorViewEngine;
            this.ServiceProvider = serviceProvider;
            this.TempDataProvider = tempDataProvider;
        }


        public async Task<string> Render<TModel>(string viewName, TModel model)
        {
            var actionContext = this.GetActionContext();
            var view = this.FindView(actionContext, viewName);

            using var output = new StringWriter();

            var viewContext = new ViewContext(
                actionContext,
                view,
                new ViewDataDictionary<TModel>(
                    metadataProvider: new EmptyModelMetadataProvider(),
                    modelState: new ModelStateDictionary())
                {
                    Model = model
                },
                new TempDataDictionary(
                    actionContext.HttpContext,
                    this.TempDataProvider),
                output,
                new HtmlHelperOptions());

            await view.RenderAsync(viewContext);

            return output.ToString();
        }

        private IView FindView(ActionContext actionContext, string viewName)
        {
            var getViewResult = this.RazorViewEngine.GetView(executingFilePath: null, viewPath: viewName, isMainPage: true);
            if (getViewResult.Success)
            {
                return getViewResult.View;
            }

            var findViewResult = this.RazorViewEngine.FindView(actionContext, viewName, isMainPage: true);
            if (findViewResult.Success)
            {
                return findViewResult.View;
            }

            var searchedLocations = getViewResult.SearchedLocations.Concat(findViewResult.SearchedLocations);
            var errorMessage = String.Join(
                Environment.NewLine,
                new[] { $"Unable to find view '{viewName}'. The following locations were searched:" }.Concat(searchedLocations)); ;

            throw new InvalidOperationException(errorMessage);
        }

        private ActionContext GetActionContext()
        {
            var httpContext = new DefaultHttpContext
            {
                RequestServices = this.ServiceProvider
            };

            var actionContext = new ActionContext(
                httpContext,
                new RouteData(),
                new ActionDescriptor());

            return actionContext;
        }
    }
}
