using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Text;

namespace WebApiWithClientCert
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<ClientCertValidation>();

            services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
                .AddCertificate(options => {
                    options.AllowedCertificateTypes = CertificateTypes.Chained;
                    options.Events = new CertificateAuthenticationEvents
                    {
                        OnCertificateValidated = context =>
                        {
                            ClientCertValidation validation = context.HttpContext.RequestServices.GetService<ClientCertValidation>();
                            if (validation.ValidateClientCert(context.ClientCertificate))
                            {
                                Console.WriteLine("[Startup:ConfigureServices:AddAuthentication:OnCertificateValidated]: Success!");
                                context.Success();
                            }
                            else
                            {
                                string errmsg = "Ohhh Noooo !!! You have failed to present a club member cert.";
                                Console.WriteLine($"[Startup:ConfigureServices:AddAuthentication:OnCertificateValidated]: Failure! {errmsg}");
                                context.Fail(errmsg);

                                context.Response.StatusCode = 401;
                                CancellationToken cancellationToken = context.HttpContext.RequestAborted;
                                string failureMessage = $"[Startup:ConfigureServices:AddAuthentication:OnCertificateValidated]: Failure! {errmsg}";
                                context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(failureMessage), 0, failureMessage.Length, cancellationToken);
                            }

                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            string errmsg = "Not a good spy... failed to authenticate.";
                            Console.WriteLine($"[Startup:ConfigureServices:AddAuthentication:OnAuthenticationFailed]: {errmsg}");
                            context.Fail(errmsg);

                            context.Response.StatusCode = 401;
                            CancellationToken cancellationToken = context.HttpContext.RequestAborted;
                            string failureMessage = $"[Startup:ConfigureServices:AddAuthentication:OnAuthenticationFailed]: {errmsg}";
                            context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(failureMessage), 0, failureMessage.Length, cancellationToken);

                            return Task.CompletedTask;
                        }
                    };
                })
                // Adding an ICertificateValidationCache results in certificate auth caching the results.
                // The default implementation uses a memory cache.
                //.AddCertificateCache()
                ;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiWithClientCert", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiWithClientCert v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
