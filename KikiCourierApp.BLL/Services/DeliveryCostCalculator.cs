namespace KikiCourierApp.BLL.Services
{
    public class DeliveryCostCalculator
    {
        public static double CalculateDeliveryCost(double basePrice, int weight, int distance)
        {
            if (basePrice < 0 || weight < 0 || distance < 0)
            {
                return 0;
            }
            return basePrice + weight * 10 + distance * 5;
        }
    }
}
