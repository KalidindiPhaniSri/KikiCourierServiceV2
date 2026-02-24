namespace KikiCourierApp.BLL.Models
{
    public class Shipment
    {
        public List<Package> Packages { get; set; } = [ ];
        public int MaxDistance { get; set; }
        public int TotalWeight { get; set; }
    }
}
