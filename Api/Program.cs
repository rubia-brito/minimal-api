using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.Dominio.ModelViews;
using MinimalApi.Dominio.Servicos;
using MinimalApi.DTOs;
using MinimalApi.Infraestrutura.Db;
using Microsoft.OpenApi.Models;


        #region Builder

public partial class Program
{
    private static object? adms;

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

#endregion

        #region JWT

        var key = "minimal-api-chave-super-secreta-com-mais-de-32-caracteres-123456";

        builder.Services
            .AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(key)
                    ),

                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        builder.Services.AddAuthorization();

        #endregion

        #region Serviços

        builder.Services.AddScoped<IAdministradorServico, AdministradorServico>();
        builder.Services.AddScoped<IVeiculoServico, VeiculoServico>();

        #endregion

        #region Banco de Dados

         var connectionString =
         builder.Configuration.GetConnectionString("mysql");

         builder.Services.AddDbContext<DbContexto>(options =>
         options.UseMySql(
         connectionString,
         ServerVersion.AutoDetect(connectionString)
       )
        );

        #endregion

#region Swagger

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Minimal API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Digite: Bearer {seu token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },

            Array.Empty<string>()
        }
    });
});

#endregion
        var app = builder.Build();

        #region Middleware

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication();
        app.UseAuthorization();

        #endregion

        #region Home

        app.MapGet("/", () =>
        {
            return Results.Json(new Home());
        })
        .AllowAnonymous().WithTags("Home");

        #endregion

        #region Administradores

        // LOGIN -> público
        app.MapPost("/administradores/login",
            ([FromBody] LoginDTO loginDTO,
            IAdministradorServico administradorServico) =>
        {
            var administrador = administradorServico.Login(loginDTO);

            if (administrador == null)
                return Results.Unauthorized();

            var key = "minimal-api-chave-super-secreta-com-mais-de-32-caracteres-123456";

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key)
            );

            var credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
            );

            var claims = new[]
            {
        new Claim("Email", administrador.Email),
        new Claim("Perfil", administrador.Perfil),
        new Claim(ClaimTypes.Role, administrador.Perfil)

            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return Results.Ok(new AdministradorLogado
            {
                Email = administrador.Email,
                Perfil = administrador.Perfil,
                Token = tokenString
            });
        })
        .AllowAnonymous()
        .WithTags("Administradores");


        // LISTAR -> protegido
        app.MapGet("/administradores",
        async (DbContexto db) =>
       {
         var administradores = await db.Administradores.ToListAsync();

        return Results.Ok(administradores);
        })
         .RequireAuthorization()
         .RequireAuthorization(policy => policy.RequireRole("Adm"))
         .WithTags("Administradores");


        // BUSCAR POR ID -> protegido
        app.MapGet("/administradores/{id}",
            (int id) =>
        {
            return Results.Ok();
        })
        .RequireAuthorization()
        .RequireAuthorization(policy => policy.RequireRole("Adm"))
        .WithTags("Administradores");


        // CRIAR -> protegido
        app.MapPost("/administradores",
            ([FromBody] Administrador administrador,
            DbContexto db) =>
        {
            db.Administradores.Add(administrador);
            db.SaveChanges();

            return Results.Created($"/administradores/{administrador.Id}", administrador);
        })
        .RequireAuthorization()
        .RequireAuthorization(policy => policy.RequireRole("Adm"))
        .WithTags("Administradores");

        #endregion

        #region Veículos

        app.MapPost("/veiculos",
            async ([FromBody] VeiculoDTO veiculoDTO,
            DbContexto db) =>
        {
            var veiculo = new Veiculo
            {
                Nome = veiculoDTO.Nome,
                Marca = veiculoDTO.Marca,
                Ano = veiculoDTO.Ano
            };

            await db.Veiculos.AddAsync(veiculo);
            await db.SaveChangesAsync();

            return Results.Created($"/veiculos/{veiculo.Id}", veiculo);
        })
        .RequireAuthorization()
        .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm,Editor" })
        .WithTags("Veículos");

        app.MapPut("/veiculos/{id}",
        async (int id,
        [FromBody] VeiculoDTO veiculoDTO,
        DbContexto db) =>
    {
        var veiculo = await db.Veiculos.FindAsync(id);

         if (veiculo == null)
        {
          return Results.NotFound();
        }

        veiculo.Nome = veiculoDTO.Nome;
        veiculo.Marca = veiculoDTO.Marca;
        veiculo.Ano = veiculoDTO.Ano;

        await db.SaveChangesAsync();

        return Results.Ok(veiculo);
    })
    .RequireAuthorization()
    .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
    .WithTags("Veículos");

        app.MapGet("/veiculos",
            ([FromQuery] int? pagina,
            IVeiculoServico veiculoServico) =>
        {
            var veiculos = veiculoServico.Todos(pagina);

            return Results.Ok(veiculos);
        })
        .RequireAuthorization()
        .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm,Editor" })
        .WithTags("Veículos");

        app.MapDelete("/veiculos/{id}",
        async (int id,
        DbContexto db) =>
    {
        var veiculo = await db.Veiculos.FindAsync(id);

        if (veiculo == null)
        {
            return Results.NotFound();
        }

        db.Veiculos.Remove(veiculo);

        await db.SaveChangesAsync();

        return Results.NoContent();
    })
     .RequireAuthorization()
     .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
     .WithTags("Veículos");
     #endregion  
       
       #region App
        
       app.UseSwagger();
       app.UseSwaggerUI();

       app.UseAuthentication();
       app.UseAuthorization();

        app.Run();
       
        #endregion

    }
}