using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var passwordParameter = builder.AddParameter("sqlPassword", "Password1");

var sqlServer = builder
    .AddSqlServer("appointme-sql", passwordParameter)
    .WithImage("mssql/server:2025-CU1-ubuntu-24.04")
    .WithHostPort(60740)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var database = sqlServer.AddDatabase("AppointMeSql", "AppointMe");


var keycloak = builder.AddKeycloak("keycloak", 8082,
        builder.AddParameter("username", "admin"),
        builder.AddParameter("password", "admin"))
    .WithDataVolume("AppointMe-Keycloak")
    .WithRealmImport("appointme-realm.json")
    .WithLifetime(ContainerLifetime.Persistent);

var mailpit = builder.AddContainer("Mailpit", "axllent/mailpit")
    .WithEndpoint(8026, 8025, name: "web-ui", scheme: "http")
    .WithEndpoint(1026, 1025, name: "smtp-server")
    .WithLifetime(ContainerLifetime.Persistent);

var appointmeApi = builder.AddProject<AppointMe_Api>("appointme-api")
    .WithReference(database)
    .WithReference(keycloak)
    .WaitFor(database)
    .WaitFor(keycloak)
    .WaitFor(mailpit)
    .WithExternalHttpEndpoints();

builder.AddViteApp(name: "appointme-frontend", workingDirectory: "../AppointMe.Frontend", packageManager: "npm")
    .WithNpmPackageInstallation()
    .WithHttpsEndpoint(port: 5173, env: "PORT")
    .WithReference(appointmeApi)
    .WaitFor(appointmeApi);

await builder.Build().RunAsync();
