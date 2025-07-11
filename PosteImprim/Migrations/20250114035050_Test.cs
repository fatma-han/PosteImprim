﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosteImprim.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Texts",
                table: "Imprimers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            // Commentez ou supprimez les modifications suivantes
            // migrationBuilder.AlterColumn<string>(
            //     name: "Name",
            //     table: "AspNetUserTokens",
            //     type: "nvarchar(128)",
            //     maxLength: 128,
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(450)");

            // migrationBuilder.AlterColumn<string>(
            //     name: "LoginProvider",
            //     table: "AspNetUserTokens",
            //     type: "nvarchar(128)",
            //     maxLength: 128,
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(450)");

            // migrationBuilder.AlterColumn<string>(
            //     name: "ProviderKey",
            //     table: "AspNetUserLogins",
            //     type: "nvarchar(128)",
            //     maxLength: 128,
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(450)");

            // migrationBuilder.AlterColumn<string>(
            //     name: "LoginProvider",
            //     table: "AspNetUserLogins",
            //     type: "nvarchar(128)",
            //     maxLength: 128,
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Texts",
                table: "Imprimers");

            // Commentez ou supprimez les modifications suivantes
            // migrationBuilder.AlterColumn<string>(
            //     name: "Name",
            //     table: "AspNetUserTokens",
            //     type: "nvarchar(450)",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(128)",
            //     oldMaxLength: 128);

            // migrationBuilder.AlterColumn<string>(
            //     name: "LoginProvider",
            //     table: "AspNetUserTokens",
            //     type: "nvarchar(450)",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(128)",
            //     oldMaxLength: 128);

            // migrationBuilder.AlterColumn<string>(
            //     name: "ProviderKey",
            //     table: "AspNetUserLogins",
            //     type: "nvarchar(450)",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(128)",
            //     oldMaxLength: 128);

            // migrationBuilder.AlterColumn<string>(
            //     name: "LoginProvider",
            //     table: "AspNetUserLogins",
            //     type: "nvarchar(450)",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(128)",
            //     oldMaxLength: 128);
        }
    }
}
