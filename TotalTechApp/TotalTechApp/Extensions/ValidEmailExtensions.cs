using System.ComponentModel.DataAnnotations;

namespace TotalTechApp.Extensions
{
    public static class ValidEmailExtensions
    {
        public static bool IsValidEmailAddress(this string address) => address != null && new EmailAddressAttribute().IsValid(address);
    }
}
