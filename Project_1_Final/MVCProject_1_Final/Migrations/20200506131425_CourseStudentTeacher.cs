using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_1_Final.Migrations
{
    public partial class CourseStudentTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Teacher_TeacherId",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_Teacher_TeacherId1",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_TeacherId",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_TeacherId1",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "TeacherId1",
                table: "Course");

            migrationBuilder.CreateIndex(
                name: "IX_Course_FirstTeacherId",
                table: "Course",
                column: "FirstTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_SecondTeacherId",
                table: "Course",
                column: "SecondTeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Teacher_FirstTeacherId",
                table: "Course",
                column: "FirstTeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Teacher_SecondTeacherId",
                table: "Course",
                column: "SecondTeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Teacher_FirstTeacherId",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_Teacher_SecondTeacherId",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_FirstTeacherId",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_SecondTeacherId",
                table: "Course");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Course",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId1",
                table: "Course",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Course_TeacherId",
                table: "Course",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_TeacherId1",
                table: "Course",
                column: "TeacherId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Teacher_TeacherId",
                table: "Course",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Teacher_TeacherId1",
                table: "Course",
                column: "TeacherId1",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
