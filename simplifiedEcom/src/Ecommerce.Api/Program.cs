using System.Text;
using DefaultNamespace;
using Ecommerce.Api.Features.Authentication;
using Ecommerce.Api.Features.Products.GetAll;
using Ecommerce.Api.Features.Products.GetAllBrandsWithCount;
using Ecommerce.Api.Features.Products.GetAllCategories;
using Ecommerce.Api.Features.Products.GetAllSubcategories;
using Ecommerce.Api.Features.Products.GetById;
using Ecommerce.Api.Infrastructure.Repositories.FilterRepository;
using Ecommerce.Api.Infrastructure.Repositories.ProductRepository;
using Ecommerce.Api.Infrastructure.Repositories.UserRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularSimplifiedEcommerceCorsPolicy",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddScoped<Db>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<GetAllProductsHandler>();
builder.Services.AddScoped<GetByIdProductHandler>();

// filtering
builder.Services.AddScoped<IFilterRepository, FilterRepository>();
builder.Services.AddScoped<GetAllCategoriesHandler>();
builder.Services.AddScoped<GetAllSubcategoriesHandler>();
builder.Services.AddScoped<GetAllBrandsWithCountHandler>();

// auth
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<AuthenticationHandler>();

// JWT AUTH
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AngularSimplifiedEcommerceCorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapGetAllProducts();
app.MapGetByIdProduct();

// filtering
app.MapGetAllCategories();
app.MapGetAllSubcategories();
app.MapGetAllBrandsWithCount();

app.MapAuthEndpoints();

app.UseHttpsRedirection();

app.Run();