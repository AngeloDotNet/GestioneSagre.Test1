namespace GestioneSagre.Web.Server;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
#pragma warning disable CS0618 // Il tipo o il membro � obsoleto
        services.AddControllersWithViews()
            .AddJsonOptions(options =>
            {
                // Info su: https://github.com/marcominerva/MyWebApiToolbox
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
            })
            .AddFluentValidation(options =>
            {
                options.AutomaticValidationEnabled = false;
                //GestioneSagre.Categorie.Validators
                options.RegisterValidatorsFromAssemblyContaining<CategoriaCreateValidator>();
                // GestioneSagre.Versioni.Validators
                options.RegisterValidatorsFromAssemblyContaining<VersioneCreateValidator>();
                // GestioneSagre.Internal.Validators
                options.RegisterValidatorsFromAssemblyContaining<MailSupportoSenderValidator>();
            });
#pragma warning restore CS0618 // Il tipo o il membro � obsoleto

        services.AddRazorPages();
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });
        });

        services.AddDbContextPool<GestioneSagreDbContext>(optionBuilder =>
        {
            var connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");

            optionBuilder.UseSqlite(connectionString, options =>
            {
                // Info su: https://docs.microsoft.com/it-it/ef/core/managing-schemas/migrations/projects?tabs=dotnet-core-cli

                // To perform a new migration you need:
                // 1. Open the Package Manager Console panel
                // 2. In the Default Project drop-down menu make sure that the selected project is GestioneSagre.Web.Server.
                // 3. Run the command Add-Migration NAME-MIGRATION -Project GestioneSagre.Web.Migrations where NAME-MIGRATION represents the name of the migration to create (example: InitialMigration)
                // 4. Finally run the command Update-Database -Project GestioneSagre.Web.Migrations
                options.MigrationsAssembly("GestioneSagre.Web.Migrations");
            });
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        services.AddSwaggerServices(Configuration, xmlPath);

        // Services TRANSIENT - GestioneSagre.XXX.CommandStack
        services.AddTransient<ICategoriaCommandStackService, CategoriaCommandStackService>();
        services.AddTransient<VersioneCommandStackService>();

        // Services TRANSIENT - GestioneSagre.XXX.QueryStack
        services.AddTransient<ICategoriaQueryStackService, CategoriaQueryStackService>();
        services.AddTransient<VersioneQueryStackService>();

        // Services SINGLETON
        services.AddSingleton<IInternalQueryStackService, InternalQueryStackService>();
        services.AddSingleton<IImagePersister, MagickNetImagePersister>();

        // Options
        services.Configure<KestrelServerOptions>(Configuration.GetSection("Kestrel"));
        services.Configure<SmtpOptions>(Configuration.GetSection("Smtp"));
    }

    public void Configure(WebApplication app)
    {
        IWebHostEnvironment env = app.Environment;

        if (env.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }

        var enableSwagger = Configuration.GetSection("Swagger").GetValue<bool>("enabled");

        if (enableSwagger)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestione Sagre v1"));
        }

        app.UseHttpsRedirection();
        app.UseBlazorFrameworkFiles();

        app.UseStaticFiles();
        app.UseRouting();

        app.UseCors();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapControllers();
            endpoints.MapFallbackToFile("index.html");
        });
    }
}