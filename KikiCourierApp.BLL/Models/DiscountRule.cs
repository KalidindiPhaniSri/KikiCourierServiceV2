namespace KikiCourierApp.BLL.Models
{
    public class DiscountRule
    {
        public double Discount { get; private set; }
        public int MinWeight { get; private set; }
        public int MaxWeight { get; private set; }
        public int MinDistance { get; private set; }
        public int MaxDistance { get; private set; }

        public DiscountRule(
            double discount,
            int minWeight,
            int maxWeight,
            int minDistance,
            int maxDistance
        )
        {
            if (discount < 0)
            {
                throw new ArgumentException("Discount should not be negative");
            }
            if (minWeight < 0 || maxWeight < 0)
            {
                throw new ArgumentException("Weight should not be less than zero");
            }
            if (minDistance < 0 || maxDistance < 0)
            {
                throw new ArgumentException("Distance should not be negative");
            }
            Discount = discount;
            MinWeight = minWeight;
            MaxWeight = maxWeight;
            MinDistance = minDistance;
            MaxDistance = maxDistance;
        }

        private bool IsApplicable(int weight, int distance)
        {
            return weight >= MinWeight
                && weight <= MaxWeight
                && distance >= MinDistance
                && distance <= MaxDistance;
        }

        public double CalculateDiscountPercentage(int weight, int distance)
        {
            if (weight < 0 || distance < 0)
            {
                return 0;
            }
            return IsApplicable(weight, distance) ? Discount : 0;
        }
    }
}
