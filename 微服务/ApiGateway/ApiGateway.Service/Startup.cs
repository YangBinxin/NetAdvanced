using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Cache.CacheManager;
using Ocelot.Provider.Polly;
using Swashbuckle.AspNetCore.Swagger;
using IdentityServer4.AccessTokenValidation;
using Ocelot.Administration;

namespace ApiGateway.Service
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //Identity Server Bearer Tokens 
            //验证机制IdentityServer
            Action<IdentityServerAuthenticationOptions> isaOpt = option =>
            {
                option.Authority = Configuration["IdentityService:Uri"];
                option.RequireHttpsMetadata = Convert.ToBoolean(Configuration["IdentityService:UseHttps"]);
                option.ApiName = Configuration["IdentityService:ApiName"];
                option.ApiSecret = Configuration["IdentityService:ApiSecret"];
                option.SupportedTokens = SupportedTokens.Both;
            };
            //添加服务器身份验证
            services.AddAuthentication().AddIdentityServerAuthentication(Configuration["IdentityService:DefaultScheme"], isaOpt);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services
                .AddOcelot() //Ocelot 网关
                .AddConsul() //Consul 网络发现
                .AddCacheManager(x => { x.WithDictionaryHandle(); })
                .AddPolly(); //Polly是一个被.NET基金会认可的弹性和瞬态故障处理库
                             //.AddAdministration("/administration", isaOpt);

            //为Swagger兼容API动态生成优雅文档
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(Configuration["Swagger:Name"], new Info { Title = Configuration["Swagger:Title"], Version = Configuration["Swagger:Version"] });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var apis = Configuration["Apis:SwaggerNames"].Split(";").ToList();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc()
              .UseSwagger()  //动态生成接口文档
              .UseSwaggerUI(options =>
              {
                  apis.ToList().ForEach(key =>
                  {
                      options.SwaggerEndpoint($"/{key}/swagger.json", key);
                  });
                  options.DocumentTitle = "网关";
              });
            app.UseOcelot().Wait(); //启用网关
        }
    }
}
