using KikiCourierApp.BLL.Models;

namespace KikiCourierApp.BLL.Interfaces
{
    public interface IShipmentBuilder
    {
        List<Shipment> BuildShipments(IReadOnlyList<Package> packages, int maxWeight);
    }
}
