using KikiCourierApp.BLL.Interfaces;

namespace KikiCourierApp.Infrastructure.InputProviders.PackageInputProviders
{
    public class ConsolePackageInputProvider : IPackageInputProvider
    {
        public string ReadLine()
        {
            string? input = Console.ReadLine() ?? string.Empty;
            return input.Trim();
        }
    }
}
