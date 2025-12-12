using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogue.Migrations
{
    /// <inheritdoc />
    public partial class FillCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Categories (Name, ImageUrl) VALUES ('Drinks', 'drinks.jpg')");
            mb.Sql("INSERT INTO Categories (Name, ImageUrl) VALUES ('Snacks', 'snacks.jpg')");
            mb.Sql("INSERT INTO Categories (Name, ImageUrl) VALUES ('Desserts', 'desserts.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Categories");
        }
    }
}
