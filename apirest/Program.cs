var builder = WebApplication.CreateBuilder(args);

// Registrar servicios y configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()     // Permitir cualquier origen
              .AllowAnyMethod()     // Permitir cualquier método (GET, POST, etc.)
              .AllowAnyHeader();    // Permitir cualquier encabezado
    });
});

// Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();      

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {   
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API REST v1");
        c.RoutePrefix = string.Empty;

    });
}

app.UseHttpsRedirection();

// Habilitar la política de CORS antes de los controladores
app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

app.Run();
