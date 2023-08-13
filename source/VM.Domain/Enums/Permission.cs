using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Domain.Enums;

public enum Permission
{
    ReadUser = 1,
    UpdateUser,
    UpdateCurrentUser,
    RemoveUser,
    ReadOrder,
    CreateOrder,
    UpdateOrder,
    ReadProduct,
    AddProduct,
    UpdateProduct,
    RemoveProduct,
    ReadShoppingCart,
    UpdateShoppingCart,
    ReadCategory,
    AddCategory,
    UpdateCategory,
    ReadPayment,
    ReadOrderDetails,
    ReadShipping,
    ReadRating,
    AddRating, 
    CreateCoupon,
    ReadCoupon
}
