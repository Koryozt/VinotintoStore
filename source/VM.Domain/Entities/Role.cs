using VM.Domain.Primitives;

namespace VM.Domain.Entities;

public sealed class Role : Enumeration<Role>
{
    public static readonly Role Registered = new(1, "Registered");
    public static readonly Role Admin = new(2, "Administrator");

    public Role(int id, string name)
        : base(id, name)
    {
    }

    public ICollection<Permission> Permissions { get; set; }

    public ICollection<User> Users { get; set; }
}
