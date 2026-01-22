using KikiCourierApp.BLL.Models;

namespace KikiCourierApp.BLL.Interfaces
{
    public interface IDiscountRules
    {
        DiscountRule? GetRule(string coupon);
    }
}
