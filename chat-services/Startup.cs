using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using realtime_app.Db;
using realtime_app.Services;
using realtime_app.SignalR.Hubs;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using chat_services.Infrastructure.Settings;
using chatservices.Services;
using chatservices.Infrastructure.Settings;
using chatservices.Db;
using System.Reflection;

namespace realtime_app
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string AllowAnyOrigin = "_allowAnyOrigin";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ChatDbContext>(options => options.UseMySql(connectionString));

            // Add a DbContext to store your Database Keys
            services.AddDbContext<ChatDbContext>(options => options.UseMySql(connectionString));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var identityServerOpts = Configuration.GetSection(nameof(IdentityServerOptions));
                options.Authority = identityServerOpts.GetValue<string>(nameof(IdentityServerOptions.Authority));
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/hub")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiReader", policy => policy.RequireClaim("scope", "api.read"));
                options.AddPolicy("Consumer", policy => policy.RequireClaim("role", "consumer"));
            });

            services.Configure<RedisSettings>(Configuration.GetSection(nameof(RedisSettings)));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<RedisStore>();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IClaimsService, ClaimsService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddSingleton<IPubSub, PubSub>();
            services.AddSingleton(options =>
            {
                return new ChatDbConnection(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy(AllowAnyOrigin, policy =>
                {
                    policy.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                });
            });

            var redisSettings = Configuration.GetSection(nameof(RedisSettings));
            var host = redisSettings.GetValue<string>(nameof(RedisSettings.Host));
            var password = redisSettings.GetValue<string>(nameof(RedisSettings.Password));
            services.AddSignalR().AddStackExchangeRedis($"{host}, port:6379, password={password}");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Chat Service API", Version = "v1" });
            });

            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(AllowAnyOrigin);
            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/hub/chat");
                endpoints.MapHub<NotificationHub>("/hub/notification");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chat service API V1");
            });
        }
    }
}
