using Microsoft.EntityFrameworkCore;
using SqlGuide.Interface;
using SqlGuide.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//var str = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddTransient<ITurmaProfessorRepository, TurmaProfessorRepository>();
builder.Services.AddTransient<IAulaRepository, AulaRepository>();
builder.Services.AddTransient<IChamadaRepository, ChamadaRepository>();
builder.Services.AddTransient<IPessoaRepository, PessoaRepository>();
builder.Services.AddTransient<ITurmaRepository, TurmaRepository>();
builder.Services.AddTransient<IDisciplinaRepository, DisciplinaRepository>();
builder.Services.AddTransient<IAproveitamentoRepository, AproveitamentoRepository>();
builder.Services.AddTransient<INotaRepository, NotaRepository>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
