using Microsoft.EntityFrameworkCore.Migrations;

namespace one_to_many.Migrations
{
    public partial class firstmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    gradeid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    grade_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.gradeid);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    studentid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    student_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    student_age = table.Column<int>(type: "int", nullable: false),
                    student_phone = table.Column<long>(type: "bigint", nullable: false),
                    gradeid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.studentid);
                    table.ForeignKey(
                        name: "FK_Students_Grades_gradeid",
                        column: x => x.gradeid,
                        principalTable: "Grades",
                        principalColumn: "gradeid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_gradeid",
                table: "Students",
                column: "gradeid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Grades");
        }
    }
}
