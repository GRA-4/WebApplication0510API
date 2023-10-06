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








app.MapGet("/api/users/{id}", async (int id, CommonOperations commonOperations) =>
{
    var user = await commonOperations.GetByIdAsync<User>(id);
    if (user != null)
    {
        return Results.Ok(user);
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapPost("/api/users", async (User user, CommonOperations commonOperations) =>
{
    var addedUser = await commonOperations.AddEntityAsync(user);
    if (addedUser != null)
    {
        return Results.Created($"/api/users/{addedUser.Id}", addedUser);
    }
    else
    {
        return Results.BadRequest();
    }
});

app.MapPut("/api/users/{id}", async (int id, User user, CommonOperations commonOperations) =>
{
    if (id != user.Id)
    {
        return Results.BadRequest();
    }

    var updatedUser = await commonOperations.UpdateEntityAsync(user);
    if (updatedUser != null)
    {
        return Results.Ok(updatedUser);
    }
    else
    {
        return Results.BadRequest();
    }
});

app.MapDelete("/api/users/{id}", async (int id, CommonOperations commonOperations) =>
{
    var user = await commonOperations.GetByIdAsync<User>(id);
    if (user != null)
    {
        var result = await commonOperations.RemoveEntityAsync(user);
        if (result == null)
        {
            return Results.NoContent();
        }
    }

    return Results.NotFound();
});

app.MapGet("/api/users", async (CommonOperations commonOperations) =>
{
    var users = await commonOperations.GetAllAsync<User>();
    if (users != null)
    {
        return Results.Ok(users);
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapGet("/api/users/search", async (string fieldName, string value, CommonOperations commonOperations) =>
{
    if (!string.IsNullOrEmpty(fieldName) && !string.IsNullOrEmpty(value))
    {
        if (fieldName == "UserName")
        {
            var users = await commonOperations.GetAllByFieldContainsAsync<User>(u => u.UserName, value);
            if (users != null)
            {
                return Results.Ok(users);
            }
        }
        else if (fieldName == "Email")
        {
            var users = await commonOperations.GetAllByFieldContainsAsync<User>(u => u.Email, value);
            if (users != null)
            {
                return Results.Ok(users);
            }
        }
        // Добавьте другие поля, если необходимо
    }

    return Results.BadRequest();
});




//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//var forecast = Enumerable.Range(1, 5).Select(index =>
//    new WeatherForecast
//    (
//        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//        Random.Shared.Next(-20, 55),
//        summaries[Random.Shared.Next(summaries.Length)]
//    ))
//    .ToArray();
//return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();

//app.MapPut("/api/user/create", async (KinoDb0410Context dbContext, HttpContext context) =>
//    await CommonOperations.CreateEntityAsync<User>(context, dbContext));

//app.MapDelete("/api/user/deletebyid/{Id}", async (KinoDb0410Context dbContext, HttpContext context) =>
//    await APIHandler.DeleteEntityByIDAsync<User>(context, dbContext));

//app.MapGet("/api/user/getbyid/{Id}", async (KinoDb0410Context dbContext, HttpContext context) =>
//    await APIHandler.GetEntityAsync<User>(context, dbContext));

//app.MapPost("/api/user/updatebyid/{Id}", async (KinoDb0410Context dbContext, HttpContext context) =>
//    await APIHandler.UpdateEntityAsync<User>(context, dbContext));

//app.MapGet("/api/user/getall", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.GetAllEntitiesAsync<User>(dbContext, context));

//app.MapPost("/api/user/getbyfields", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.SearchEntitiesByJsonAsync<User>(context, dbContext));



//// Products
//app.MapPut("/api/product/create", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.CreateEntityAsync<Product>(context, dbContext));

//app.MapDelete("/api/product/deletebyid/{Id}", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.DeleteEntityByIDAsync<Product>(context, dbContext));

//app.MapGet("/api/product/getbyid/{Id}", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.GetEntityAsync<Product>(context, dbContext));

//app.MapPost("/api/product/updatebyid/{Id}", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.UpdateEntityAsync<Product>(context, dbContext));

//app.MapGet("/api/product/getall", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.GetAllEntitiesAsync<Product>(dbContext, context));


//// Orders
//app.MapPut("/api/order/create", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.CreateEntityAsync<Order>(context, dbContext));

//app.MapDelete("/api/order/deletebyid/{Id}", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.DeleteEntityByIDAsync<Order>(context, dbContext));

//app.MapGet("/api/order/getbyid/{Id}", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.GetEntityAsync<Order>(context, dbContext));

//app.MapPost("/api/order/updatebyid/{Id}", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.UpdateEntityAsync<Order>(context, dbContext));

//app.MapGet("/api/order/getall", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.GetAllEntitiesAsync<Order>(dbContext, context));


//// OrderItems
//app.MapPut("/api/orderitem/create", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.CreateEntityAsync<OrderItem>(context, dbContext));

//app.MapDelete("/api/orderitem/deletebyid/{Id}", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.DeleteEntityByIDAsync<OrderItem>(context, dbContext));

//app.MapGet("/api/orderitem/getbyid/{Id}", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.GetEntityAsync<OrderItem>(context, dbContext));

//app.MapPost("/api/orderitem/updatebyid/{Id}", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.UpdateEntityAsync<OrderItem>(context, dbContext));

//app.MapGet("/api/orderitem/getall", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.GetAllEntitiesAsync<OrderItem>(dbContext, context));


//// Reviews
//app.MapPut("/api/review/create", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.CreateEntityAsync<Review>(context, dbContext));

//app.MapDelete("/api/review/deletebyid/{Id}", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.DeleteEntityByIDAsync<Review>(context, dbContext));

//app.MapGet("/api/review/getbyid/{Id}", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.GetEntityAsync<Review>(context, dbContext));

//app.MapPost("/api/review/updatebyid/{Id}", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.UpdateEntityAsync<Review>(context, dbContext));

//app.MapGet("/api/review/getall", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.GetAllEntitiesAsync<Review>(dbContext, context));


//// Categories
//app.MapPut("/api/category/create", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.CreateEntityAsync<Category>(context, dbContext));

//app.MapDelete("/api/category/getbyid/{Id}", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.DeleteEntityByIDAsync<Category>(context, dbContext));

//app.MapPost("/api/category/updatebyid/{Id}", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.UpdateEntityAsync<Category>(context, dbContext));

//app.MapGet("/api/category/deletebyid/{Id}", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.GetEntityAsync<Category>(context, dbContext));

//app.MapGet("/api/category/getall", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.GetAllEntitiesAsync<Category>(dbContext, context));


//// Wishlists
//app.MapPut("/api/wishlist/create", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.CreateEntityAsync<Wishlist>(context, dbContext));

//app.MapDelete("/api/wishlist/getbyid/{Id}", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.DeleteEntityByIDAsync<Wishlist>(context, dbContext));

//app.MapPost("/api/wishlist/updatebyid/{Id}", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.UpdateEntityAsync<Wishlist>(context, dbContext));

//app.MapGet("/api/wishlist/deletebyid/{Id}", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.GetEntityAsync<Wishlist>(context, dbContext));

//app.MapGet("/api/wishlist/getall", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.GetAllEntitiesAsync<Wishlist>(dbContext, context));


//// Roles
//app.MapPut("/api/role/create", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.CreateEntityAsync<Role>(context, dbContext));

//app.MapDelete("/api/role/getbyid/{Id}", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.DeleteEntityByIDAsync<Role>(context, dbContext));

//app.MapPost("/api/role/updatebyid/{Id}", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.UpdateEntityAsync<Role>(context, dbContext));

//app.MapGet("/api/role/deletebyid/{Id}", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.GetEntityAsync<Role>(context, dbContext));

//app.MapGet("/api/role/getall", async (MarketplaceDbContext dbContext, HttpContext context) =>
//    await APIHandler.GetAllEntitiesAsync<Role>(dbContext, context));



//internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}






app.Run();

