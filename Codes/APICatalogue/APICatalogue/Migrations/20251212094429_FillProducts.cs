using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogue.Migrations
{
    /// <inheritdoc />
    public partial class FillProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Products (Name, Description, Price, ImageUrl, Stock, CreatedAt, CategoryId) VALUES ('Cocal-Cola Diet', 'Refrigerant cola 350ml', 5.45, 'cocacola.jpg', 50, CURRENT_TIMESTAMP(), 1)");

            mb.Sql("INSERT INTO Products (Name, Description, Price, ImageUrl, Stock, CreatedAt, CategoryId) VALUES ('Tuna snack', 'Tuna sandwich with mayonnaise', 8.50, 'tuna.jpg', 10, CURRENT_TIMESTAMP(), 2)");

            mb.Sql("INSERT INTO Products (Name, Description, Price, ImageUrl, Stock, CreatedAt, CategoryId) VALUES ('Pudding 100g', 'Condensed milk pudding', 6.75, 'pudding.jpg', 20, CURRENT_TIMESTAMP(), 3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Products");
        }
    }
}
