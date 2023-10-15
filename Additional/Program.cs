using Microsoft.EntityFrameworkCore;
using WebApplicationKinoAPI0510;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<KinoDb0410Context>(options =>
{
    options.UseSqlServer("Server=DESKTOP-T5P3GVP;Database=KinoDb0410;Trusted_Connection=True;TrustServerCertificate=True;"); // Замените на вашу строку подключения
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

app.UseHttpsRedirection();




//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//{
//    optionsBuilder
//        .UseLazyLoadingProxies()        // подключение lazy loading
//        .UseSqlServer("Server=DESKTOP-T5P3GVP;Database=KinoDb0410;Trusted_Connection=True;TrustServerCertificate=True;");


//}






app.Run();

