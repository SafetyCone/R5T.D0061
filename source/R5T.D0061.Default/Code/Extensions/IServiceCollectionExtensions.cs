using System;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using R5T.Dacia;


namespace R5T.D0061
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="RazorViewStringRenderer"/> implementation of <see cref="IRazorViewStringRenderer"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddRazorViewStringRenderer(this IServiceCollection services,
            IServiceAction<IRazorViewEngine> razorViewEngineAction,
            IServiceAction<ITempDataProvider> tempDataProviderAction)
        {
            services
                .AddSingleton<IRazorViewStringRenderer, RazorViewStringRenderer>()
                .Run(razorViewEngineAction)
                .Run(tempDataProviderAction)
                ;

            return services;
        }

        /// <summary>
        /// Adds the <see cref="RazorViewStringRenderer"/> implementation of <see cref="IRazorViewStringRenderer"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IRazorViewStringRenderer> AddRazorViewStringRendererAction(this IServiceCollection services,
            IServiceAction<IRazorViewEngine> razorViewEngineAction,
            IServiceAction<ITempDataProvider> tempDataProviderAction)
        {
            var serviceAction = ServiceAction.New<IRazorViewStringRenderer>(() => services.AddRazorViewStringRenderer(
                razorViewEngineAction,
                tempDataProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Assumes that the required <see cref="IRazorViewEngine"/> and <see cref="ITempDataProvider"/> service dependencies are added elsewhere, perhaps via an ASP.NET Core AddRazorPages() call.
        /// Adds the <see cref="RazorViewStringRenderer"/> implementation of <see cref="IRazorViewStringRenderer"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddRazorViewStringRenderer(this IServiceCollection services)
        {
            var razorViewEngineAction = ServiceAction<IRazorViewEngine>.AddedElsewhere;
            var tempDataProviderAction = ServiceAction<ITempDataProvider>.AddedElsewhere;

            services.AddRazorViewStringRenderer(
                razorViewEngineAction,
                tempDataProviderAction);

            return services;
        }

        /// <summary>
        /// Assumes that the required <see cref="IRazorViewEngine"/> and <see cref="ITempDataProvider"/> service dependencies are added elsewhere, perhaps via an ASP.NET Core AddRazorPages() call.
        /// Adds the <see cref="RazorViewStringRenderer"/> implementation of <see cref="IRazorViewStringRenderer"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IRazorViewStringRenderer> AddRazorViewStringRendererAction(this IServiceCollection services)
        {
            var serviceAction = ServiceAction.New<IRazorViewStringRenderer>(() => services.AddRazorViewStringRenderer());
            return serviceAction;
        }
    }
}
