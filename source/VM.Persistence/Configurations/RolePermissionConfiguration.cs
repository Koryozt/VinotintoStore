using VM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Permission = VM.Domain.Enums.Permission;

namespace VM.Persistence.Configurations;

internal sealed class RolePermissionConfiguration
    : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.HasData(
            Create(Role.Registered, Permission.ReadUser),
            Create(Role.Registered, Permission.UpdateCurrentUser),
            Create(Role.Registered, Permission.ReadOrder),
            Create(Role.Registered, Permission.CreateOrder),
            Create(Role.Registered, Permission.UpdateOrder),
            Create(Role.Registered, Permission.ReadProduct),
            Create(Role.Registered, Permission.AddProduct),
            Create(Role.Registered, Permission.UpdateProduct),
            Create(Role.Registered, Permission.ReadShoppingCart),
            Create(Role.Registered, Permission.UpdateShoppingCart),
            Create(Role.Registered, Permission.ReadCategory),
            Create(Role.Registered, Permission.AddCategory),
            Create(Role.Registered, Permission.UpdateCategory),
            Create(Role.Registered, Permission.ReadPayment),
            Create(Role.Registered, Permission.ReadOrderDetails),
            Create(Role.Registered, Permission.ReadShipping),
            Create(Role.Registered, Permission.ReadRating),
            Create(Role.Registered, Permission.AddRating),
            Create(Role.Registered, Permission.ReadCoupon),

            Create(Role.Admin, Permission.ReadUser),
            Create(Role.Admin, Permission.UpdateUser),
            Create(Role.Admin, Permission.UpdateCurrentUser),
            Create(Role.Admin, Permission.RemoveUser),
            Create(Role.Admin, Permission.ReadOrder),
            Create(Role.Admin, Permission.CreateOrder),
            Create(Role.Admin, Permission.UpdateOrder),
            Create(Role.Admin, Permission.ReadProduct),
            Create(Role.Admin, Permission.AddProduct),
            Create(Role.Admin, Permission.UpdateProduct),
            Create(Role.Admin, Permission.RemoveProduct),
            Create(Role.Admin, Permission.ReadShoppingCart),
            Create(Role.Admin, Permission.UpdateShoppingCart),
            Create(Role.Admin, Permission.ReadCategory),
            Create(Role.Admin, Permission.AddCategory),
            Create(Role.Admin, Permission.UpdateCategory),
            Create(Role.Admin, Permission.ReadPayment),
            Create(Role.Admin, Permission.ReadOrderDetails),
            Create(Role.Admin, Permission.ReadShipping),
            Create(Role.Admin, Permission.ReadRating),
            Create(Role.Admin, Permission.AddRating),
            Create(Role.Admin, Permission.CreateCoupon),
            Create(Role.Admin, Permission.ReadCoupon));
    }

    private static RolePermission Create(
        Role role, Permission permission)
    {
        return new RolePermission
        {
            RoleId = role.Id,
            PermissionId = (int)permission
        };
    }
}
