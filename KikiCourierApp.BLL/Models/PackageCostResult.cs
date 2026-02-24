namespace KikiCourierApp.BLL.Models
{
    public class PackageCostResult
    {
        public string PackageId { get; private set; }
        public double DiscountAmount { get; private set; }
        public double DeliveryCost { get; private set; }
        public double DeliveryTime { get; private set; }

        public PackageCostResult(
            string packageId,
            double discountAmount,
            double deliveryCost,
            double deliveryTime
        )
        {
            PackageId = packageId;
            DiscountAmount = discountAmount;
            DeliveryCost = deliveryCost;
            DeliveryTime = deliveryTime;
        }
    }
}
