using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.IdentityModel.Tokens;
using Application.Core.Utilities.Security.Jwt;
using Application.Core.Utilities.Security.Encyption;
using Application.Bussiness.Abstract;
using Application.Bussiness.Concrete;
using Application.DataAccess.Abstract;
using Application.DataAccess.Concrete;

/*
using Newtonsoft.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Core.Extensions;
using System.IO;
using Microsoft.Extensions.FileProviders;
*/


namespace WebApi
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

            //services.AddScoped<IUserService, UserService>();
            // services.AddScoped<IAuthService, AuthService>();

          

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserDal,EfUserDal > ();


            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryDal, EfCategoryDal>();

            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICommentDal, EfCommentDal>();

            services.AddScoped<IProductTypeService, ProductTypeService>();
            services.AddScoped<IProductTypeDal, EfProductTypeDal>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductDal, EfProductDal>();

            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IProductCategoryDal, EfProductCategoryDal>();

            services.AddScoped<IProductImageService, ProductImageService>();
            services.AddScoped<IProductImageDal, EfProductImageDal>();

            services.AddScoped<ICommentLikeServive, CommentLikeService>();
            services.AddScoped<ICommentLikeDal, EfCommentLikeDal>();

            services.AddScoped<ILikeProductService, LikeProductService>();
            services.AddScoped<ILikeProductDal, EfLikeProductDal>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenHelper, JwtHelper>();


            services.AddControllers();

            //******************CORS setting(Api'ye dýþarýdan request(istek) geldiðinde izin vermek için)*************
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://localhost:3000"));//reactjs url normaled domain verilir.
            });

            //******************TOKEN SETTÝNGS*************
            //JWT için authentication servisinin sisteme eklemesi autharazation , hem authentication, autharazation middlewarelarý ekledik.
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });

            



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //CORS için middleware verdik.
            app.UseCors(builder => builder.WithOrigins("http://localhost:3000").AllowAnyHeader()); //yayýna çýktýðýmýzda kendi domainimizi vereceðiz.

            app.UseHttpsRedirection();

            app.UseRouting();

    
            app.UseAuthentication();//anahtar Doðrulamadýr

            app.UseAuthorization();//ne yapabilir.  YETKÝdir.

            app.UseStaticFiles(); // For the wwwroot folder (url ile static dosyalara eriþmek için eklemnmesi gereken middleware)
       
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
