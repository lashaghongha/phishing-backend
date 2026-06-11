using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApiSolution.Migrations
{
    /// <inheritdoc />
    public partial class vigaca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS ""Categories"" (
                    ""Id"" serial PRIMARY KEY,
                    ""Name"" text NOT NULL,
                    ""Description"" text NOT NULL,
                    ""CreatedAt"" timestamp with time zone NOT NULL
                );
            ");

            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS ""Products"" (
                    ""Id"" serial PRIMARY KEY,
                    ""Name"" text NOT NULL,
                    ""Price"" numeric(18,2) NOT NULL,
                    ""CategoryId"" integer NOT NULL,
                    ""Description"" text NOT NULL,
                    ""CreatedAt"" timestamp with time zone NOT NULL,
                    ""UpdatedAt"" timestamp with time zone NOT NULL
                );
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Categories");
            migrationBuilder.DropTable(name: "Products");
        }
    }
}
