using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Client
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfigureOptions<ClientOptions>, ClientOptions>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Equals("/config.json"))
                {
                    var config = context.RequestServices.GetService<IOptions<ClientOptions>>();
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync($"{{ \"authority\": \"{config?.Value?.Authority}\", \"api\": \"{config?.Value?.Api}\", \"scopes\":\"{config?.Value?.Scopes}\" }}");
                }
                else
                {
                    await next();
                }
            });
        }
    }
}
