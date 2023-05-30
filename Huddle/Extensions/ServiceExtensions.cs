using Huddle.Domain.Entities;
using Huddle.Domain.Context;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using GoogleApi.Extensions;
using Huddle.Application.GoogleMaps;
using Huddle.Application.EmailService;
using Huddle.Application.UserServices;
using Repositories.UserRepo;
using Repositories.HomeRepo;
using Repositories.GroupRepo;
using Huddle.Application.GroupServices;

namespace Huddle.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<HuddleContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("sqlConnection")));
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User,Role>()
                .AddEntityFrameworkStores<HuddleContext>()
                  .AddUserStore<UserStore<User, Role, HuddleContext, Guid>>()
                  .AddRoleStore<RoleStore<Role, HuddleContext, Guid>>();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration Configuration)
        {
            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:Jwt_Secret"].ToString());
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                };
            });
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllersWithViews();
        }

        public static void ConfigureAppServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGoogleMapsApiService, GoogleMapsApiService>();
            services.AddScoped<ISendEmails, SendEmails>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IHomeRepository, HomeRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IGroupServices,GroupServices>();
            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
        }

        public static void ConfigureGoogleMapsAPIs(this IServiceCollection services)
        {
            services.AddGoogleApiClients();
        }
    }
}
