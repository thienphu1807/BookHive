using BookHiveApi.Mapper;
using BookHiveApi.Services;
using BookHiveMVC.Repository;
using BookHiveMVC.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add HttpClientFactory
builder.Services.AddHttpClient();

//Register Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

//Register Service
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<BookService>();


//Register AutoMap
builder.Services.AddAutoMapper(
    cfg =>
    {
        cfg.AddProfile<BookHiveMapping>();
    }
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
