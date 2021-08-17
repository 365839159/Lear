using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core
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
            #region
            services.AddSwaggerGen(swaggerGenOpions =>
            {
                swaggerGenOpions.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "Blog.core API",
                    //TermsOfService = new Uri("None"),
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Blog.Core@xxx.com",
                        Url = new Uri("https://www.jianshu.com/u/123")
                    }
                });
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "Blog.Core.xml");//������Ǹո����õ�xml�ļ���
                swaggerGenOpions.IncludeXmlComments(xmlPath,true);

                var xmlModelPath = Path.Combine(basePath, "Blog.Core.Model.xml");//�������Model���xml�ļ���
                swaggerGenOpions.IncludeXmlComments(xmlModelPath);
            });
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            #region 
            app.UseSwagger();
            app.UseSwaggerUI(option => { 
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                option.RoutePrefix = "";//·�����ã�����Ϊ�գ�ȥ��launchSetting.json�е�launchUrl ��ʾֱ���ڸ��������ʣ�http://localhost:5000��

            }) ;

            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
