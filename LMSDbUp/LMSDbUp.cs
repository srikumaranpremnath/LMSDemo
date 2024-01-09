using BCrypt.Net;
using DbUp;
using DbUp.Engine;
using DbUp.Helpers;
using DbUp.ScriptProviders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LMS.DBUP
{
    class LMSDbUp
    {
        private const string DropTables = "DropTables";
        private const string Migrations = "Migrations";
        private const string MockDataInsert = "MockDataInsert";
        private const bool IsNullJournal = true;

        static int Main()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();
            string connectionString = configuration.GetConnectionString("DevConnection");
            bool deleteTables = configuration.GetSection("ExecutionParameter:DeleteTables").Value.Equals("true", StringComparison.OrdinalIgnoreCase);

            var x = BCrypt.Net.BCrypt.HashPassword("admin");
            var y = BCrypt.Net.BCrypt.HashPassword("User");
            Console.WriteLine(x+"\n\n"+y);
            EnsureDatabase.For.SqlDatabase(connectionString);

            DatabaseUpgradeResult executionResponse;
            if (deleteTables)
            {
                Console.WriteLine("Start executing Drop Tables...");
                executionResponse = ExecuteMigration(connectionString, DropTables, IsNullJournal).PerformUpgrade();
                if (!executionResponse.Successful)
                    return ReturnError(executionResponse.Error.ToString());
            }

            Console.WriteLine("Start executing Migratrions...");
            executionResponse = ExecuteMigration(connectionString, Migrations).PerformUpgrade();
            if (!executionResponse.Successful)
                return ReturnError(executionResponse.Error.ToString());

            Console.WriteLine("Start executing Mock Data Insert...");
            executionResponse = ExecuteMigration(connectionString, MockDataInsert).PerformUpgrade();
            if (!executionResponse.Successful)
                return ReturnError(executionResponse.Error.ToString());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;

        }

        private static UpgradeEngine ExecuteMigration(string connectionString, string folderName, bool isNullJournal = false)
        {
            var fileDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts", folderName);
            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithVariablesDisabled()
                    .WithScriptsFromFileSystem(fileDirectory, new FileSystemScriptOptions
                    {
                        IncludeSubDirectories = true
                    })
                    .LogToConsole();
            if (isNullJournal)
                upgrader.JournalTo(new NullJournal());
            return upgrader.Build();
        }
        private static int ReturnError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
            return 1;
        }
    }
}
