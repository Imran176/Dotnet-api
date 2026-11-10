
// Create the WebApplication builder.
// It configures the application, loads settings, and manages services.
var builder = WebApplication.CreateBuilder(args);

// Register controller services so the application can handle API controllers.
builder.Services.AddControllers();

// Register OpenAPI services to generate API documentation.
// http://localhost:5052/openapi/v1.json
builder.Services.AddOpenApi();

// Register CORS (Cross-Origin Resource Sharing) services.
// CORS allows your frontend application to communicate with your backend API
// even when they run on different origins (domains, ports, or protocols).
builder.Services.AddCors(options =>
{
    // Define a CORS policy for the development environment.
    options.AddPolicy("DevCors", corsBuilder =>
    {
        corsBuilder
            // Allow requests only from the local frontend running on port 3000.
            .WithOrigins("http://localhost:3000")

            // Allow all HTTP methods such as GET, POST, PUT, DELETE, and PATCH.
            .AllowAnyMethod()

            // Allow all HTTP request headers, such as Content-Type and Authorization.
            .AllowAnyHeader()

            // Allow the frontend to send credentials such as cookies
            // and receive credential-related responses.
            .AllowCredentials();
    });

    // Define a separate CORS policy for the production environment.
    options.AddPolicy("ProdCors", corsBuilder =>
    {
        corsBuilder
            // Allow requests only from your production frontend domain.
            .WithOrigins("https://site-domain.com")

            // Allow all HTTP methods.
            .AllowAnyMethod()

            // Allow all HTTP request headers.
            .AllowAnyHeader()

            // Allow credentials such as cookies in cross-origin requests.
            .AllowCredentials();
    });
});

// Build the application using the registered services and configuration.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Apply the development CORS policy.
    // Only the frontend at http://localhost:3000 can access this API.
    app.UseCors("DevCors");
    // Enable the OpenAPI JSON document endpoint in the development environment.
    app.MapOpenApi();
}
else
{
    // Apply the production CORS policy.
    // Only the configured production frontend domain can access this API.
    app.UseCors("ProdCors");
    // Redirect HTTP requests to HTTPS in non-development environments.
    app.UseHttpsRedirection();
}

// Map controller routes so incoming HTTP requests reach their respective controllers.
app.MapControllers();

// Start the web application and begin listening for incoming HTTP requests.
app.Run();
