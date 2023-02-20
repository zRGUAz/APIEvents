using APIEvents.Filter;
using APIEvents.Infra.Data.Repository;
using APIEvents.Service.Dto;
using APIEvents.Service.Entity;
using APIEvents.Service.Interface;
using APIEvents.Service.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace APIEvents
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExcecaoGeralFilter));
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var chaveCripto = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY"));

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options =>
                        {
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = new SymmetricSecurityKey(chaveCripto),
                                ValidateIssuer = true,
                                ValidIssuer = "APIClientes",
                                ValidateAudience = true,
                                ValidAudience = "APIEvents.com"
                            };
                        });

            builder.Services.AddScoped<IEventoRepository, EventoRepository>();
            builder.Services.AddScoped<IReservaRepository, ReservaRepository>();
            builder.Services.AddScoped<IEventoService, EventoService>();
            builder.Services.AddScoped<IReservaService, ReservaService>();

            MapperConfiguration mapperConfig = new(mc =>
            {
                mc.CreateMap<EventoEntity, EventoDto>().ReverseMap();
                mc.CreateMap<ReservaEntity, ReservaDto>().ReverseMap();
            });

            IMapper mapper = mapperConfig.CreateMapper();

            builder.Services.AddSingleton(mapper);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}