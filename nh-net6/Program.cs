// See https://aka.ms/new-console-template for more information

using nh_migration;
using nh_shared;

var connectionString = "Data Source=(localdb)\\MSSQLLocalDB";
MigrationRunner.RunMigrations(connectionString);
var factory = new AppSessionFactory(connectionString);
FailingQuery.DoQuery(factory.SessionFactory);
Console.WriteLine("It Works!");