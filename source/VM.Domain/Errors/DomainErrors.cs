using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Shared;

namespace VM.Domain.Errors;

# pragma warning disable

public static class DomainErrors
{
    public static class Email
    {
        public static readonly Error Empty = new(
            "Email.Empty",
            "Email is empty");

        public static readonly Error TooLong = new(
            "Email.TooLong",
            "Email is too long, the limit is 255 characters");

        public static readonly Error InvalidFormat = new(
            "Email.InvalidFormat",
            "Email format is invalid");
    }

    public static class Password
    {
        public static readonly Error Empty = new(
            "Password.Empty",
            "Password is empty");

        public static readonly Error TooShort = new(
            "Password.TooShort",
            "Password too short. The minimum permitted are 8 characters");

        public static readonly Error TooLong = new(
            "Password.TooLong",
            "Password too long. The maximum permitted are 32 characters");

        public static readonly Error Invalid = new(
            "Password.Invalid",
            "The password must contain uppercase, lowercase, numbers and special characters");
    }

    public static class Name
    {
        public static readonly Error Empty = new(
            "Name.Empty",
            "Name is empty");

        public static readonly Error TooLong = new(
            "Name.TooLong",
            "Name is too long, the limit is 50 characters");
    }

    public static class LongText
    {
        public static readonly Error Empty = new(
            "LongText.Empty",
            "Long text is empty");

        public static readonly Error TooLong = new(
            "LongText.TooLong",
            "Long text exceeded the limit of 5000 characters");
    }

    public static class Numeric
    {
        public static readonly Error NegativeValue = new(
            "Numeric.NegativeValue",
            "The value provided can not be negative");

        public static readonly Error TooLong = new(
            "Numeric.TooLong",
            "The value exceeded the max value of 100000000.00");
    }

    public static class Quantity
    {
        public static readonly Error NegativeValue = new(
            "Quantity.NegativeValue",
            "The value provided can not be negative");

        public static readonly Error TooLong = new(
            "Quantity.TooLong",
            "The value exceeded the max value of 1000000");
    }

    public static class Code
    {
        public static readonly Error Empty = new(
            "Code.Empty",
            "Code is empty");

        public static readonly Error TooShort = new(
            "Code.TooShort",
            "Code too short. The minimum permitted are 5 characters");

        public static readonly Error TooLong = new(
            "Code.TooLong",
            "Code exceeded the limit of 15 characters");

        public static readonly Error InvalidFormat = new(
            "Code.InvalidFormat",
            "Code format is invalid");
    }
}
