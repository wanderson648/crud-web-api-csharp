using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class FillProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("insert into product (Name, Description, Price, ImageUrl, Stock, CreatedAt, CatergoryId)" +
                "values ('Coca-cola diet', 'Refrigerante de Coca 350ml', '5.45', 'cocacola.jpg', '10', now(), 1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("delete from products");
        }
    }
}
