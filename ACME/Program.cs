using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using ACME.Data;
using ACME.DTO;
using ACME.Validator;
using ACME.Repository;
using ACME.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add validators
builder.Services.AddScoped<IValidator<CustomerRequest>, CustomerRequestValidator>();
builder.Services.AddScoped<IValidator<ProductRequest>, ProductRequestValidator>();
builder.Services.AddScoped<IValidator<OrderRequest>, OrderRequestValidator>();

// Add data access
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddDbContext<AcmeContext>(options => 
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ACME")));

// Add fluent validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// Add auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add controlers
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Add the Api Key to Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "Api Key to access the API",
        Type = SecuritySchemeType.ApiKey,
        Name = "x-Api-Key",
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });
    var scheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header
    };
    var requirement = new OpenApiSecurityRequirement
    {
        { scheme, new List<string>() }
    };
    c.AddSecurityRequirement(requirement);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyAuthMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
