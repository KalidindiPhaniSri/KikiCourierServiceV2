namespace KikiCourierApp.BLL.Models
{
    public class Package
    {
        public string Id { get; private set; }
        public int Weight { get; private set; }
        public int Distance { get; private set; }
        public string CouponCode { get; private set; }

        public Package(string id, int weight, int distance, string couponCode)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Package id is required");
            }
            if (weight < 0)
            {
                throw new ArgumentException("Package weight must be positive");
            }
            if (distance < 0)
            {
                throw new ArgumentException("Delivery distance should be positive");
            }
            Id = id.Trim();
            Weight = weight;
            Distance = distance;
            CouponCode = couponCode?.Trim() ?? string.Empty;
        }
    }
}
