namespace KikiCourierApp.BLL.Models
{
    public class PackageCostResult
    {
        public string PackageId { get; private set; }
        public double DiscountAmount { get; private set; }
        public double DeliveryCost { get; private set; }

        public PackageCostResult(string packageId, double discountAmount, double deliveryCost)
        {
            PackageId = packageId;
            DiscountAmount = discountAmount;
            DeliveryCost = deliveryCost;
        }
    }
}
