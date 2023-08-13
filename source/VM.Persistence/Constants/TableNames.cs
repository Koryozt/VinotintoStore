using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Persistence.Constants;

public static class TableNames
{
    public const string CartItem = "CartItem";

    public const string Category = "Category";

    public const string Coupon = "Coupon";

    public const string Order = "Order";

    public const string OrderDetail = "OrderDetail";

    public const string Payment = "Payment";

    public const string Product = "Product";

    public const string Rating = "Rating";

    public const string Shipping = "Shipping";

    public const string ShoppingCart = "Cart";

    public const string User = "User";

    public const string OutboxMessages = "OutboxMessages";

    public const string OutboxMessageConsumers = "OutboxMessageConsumers";

    public const string Role = "Role";

    public const string RolePermission = "RolePermission";
    public const string Permissions = "Permissions";
}
