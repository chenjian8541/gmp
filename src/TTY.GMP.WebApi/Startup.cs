using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TTY.GMP.IOC;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using TTY.GMP.ServiceBus;
using TTY.GMP.Entity.Config;
using Microsoft.AspNetCore.Mvc.Filters;
using TTY.GMP.WebApi.FilterAttribute;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using TTY.Api.Throttle;
using TTY.GMP.WebApi.Serializer;
using TTY.GMP.Http;
using TTY.GMP.ICache;
using TTY.GMP.WebApi.Extensions;
using TTY.GMP.IEventProvider;

namespace TTY.GMP.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddSession();
            services.AddResponseCompression();
            services.AddResponseCaching();
            services.AddJwtAuthentication();
            services.AddRedis();
            services.AddApiThrottle();
            services.AddMvc(options =>
            {
                RegisterGlobalFilters(options.Filters);
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                options.SerializerSettings.ContractResolver = new CustomContractResolver();
            });
            services.AddOptions();
            services.AddCors(options =>
            {
                options.AddPolicy("GmpDomainLimit", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IHttpClient, StandardHttpClient>();
            return Bootstrapper.Bootstrap((p) =>
            {
                p.Populate(services);
                InitCustomIoc(p);
                InitRabbitMq(p);
            });
        }

        /// <summary>
        /// 注册全局过滤器
        /// </summary>
        /// <param name="filters"></param>
        private void RegisterGlobalFilters(FilterCollection filters)
        {
            //filters.Add(new GmpHandleLogAttribute());
            //filters.Add(new GmpRoleLimitAttribute());  
            filters.Add(typeof(ApiThrottleActionFilter));
            filters.Add(new GmpValidateRequestAttribute());
            filters.Add(new GmpUserBehaviorAttribute());
            filters.Add(new GmpResponseCacheAttribute());
            filters.Add(new GmpExceptionFilterAttribute());
        }

        /// <summary>
        /// 自定义一些注入规则
        /// </summary>
        /// <param name="container"></param>
        private void InitCustomIoc(ContainerBuilder container)
        {
        }

        /// <summary>
        /// 初始化rabbitmq
        /// </summary>
        /// <param name="container"></param>
        private void InitRabbitMq(ContainerBuilder container)
        {
            var config = Configuration.GetSection("AppSettings").Get<AppSettings>().RabbitMqConfig;
            if (config.IsOpen)
            {
                var busControl = new SubscriptionAdapt().PublishAt(config.Host, "TestConsumerQueue", config.UserName, config.Password);
                var publisher = new EventPublisher(busControl);
                container.RegisterInstance(publisher).As<IEventPublisher>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSession();
            app.UseAuthentication();
            app.UseResponseCompression();
            app.UseResponseCaching();
            app.UseCors("GmpDomainLimit");
            app.UseApiThrottle();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
        }
    }
}
