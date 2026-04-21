using DefaultNamespace;
using Ecommerce.Api.Features.Products.GetAll;
using Ecommerce.Api.Features.Products.GetById;
using Ecommerce.Api.Infrastructure.Repositories.ProductRepository;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AngularSimplifiedEcommerceCorsPolicy");

app.MapGetAllProducts();
app.MapGetByIdProduct();

app.UseHttpsRedirection();

app.Run();