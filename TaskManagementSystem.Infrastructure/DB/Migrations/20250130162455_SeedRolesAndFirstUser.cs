using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem.Infrastructure.DB.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesAndFirstUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // معرف الدور "admin"
            var adminRoleId = Guid.NewGuid().ToString();
            // معرف المستخدم "Admin"
            var adminUserId = Guid.NewGuid().ToString();

            // إضافة الدور "admin"
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[]
                {
                    adminRoleId,             // Id للدور
                    "admin",                 // اسم الدور
                    "ADMIN",                 // اسم الدور بأحرف كبيرة
                    Guid.NewGuid().ToString()// ConcurrencyStamp
                }
            );

            // إعداد بيانات المستخدم "Admin"
            var hasher = new PasswordHasher<IdentityUser>();
            var hashedPassword = hasher.HashPassword(null, "admin@T5ear2024");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled", "AccessFailedCount" },
                values: new object[]
                {
                    adminUserId,              // Id للمستخدم
                    "Admin",                  // اسم المستخدم
                    "ADMIN",                  // اسم المستخدم بأحرف كبيرة
                    "admin@admin.info",        // الإيميل
                    "ADMIN@ADMIN.INFO",        // الإيميل بأحرف كبيرة
                    true,                     // تأكيد الإيميل
                    hashedPassword,           // كلمة المرور المشفرة
                    Guid.NewGuid().ToString(),// SecurityStamp
                    Guid.NewGuid().ToString(),// ConcurrencyStamp
                    false,                    // تأكيد رقم الهاتف
                    false,                    // Two-Factor Authentication
                    true,                     // Lockout Enabled
                    0                         // AccessFailedCount
                }
            );

            // ربط المستخدم بالدور "admin"
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[]
                {
                    adminUserId,  // Id المستخدم
                    adminRoleId   // Id الدور
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // حذف المستخدم
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "Admin"
            );

            // حذف الدور
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin"
            );
        }
    }
}
