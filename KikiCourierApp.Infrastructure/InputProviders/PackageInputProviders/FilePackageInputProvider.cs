using KikiCourierApp.BLL.Interfaces;
using Microsoft.Extensions.Logging;

namespace KikiCourierApp.Infrastructure.InputProviders.PackageInputProviders
{
    public class FilePackageInputProvider : IPackageInputProvider
    {
        private StreamReader _reader;
        private readonly ILogger<FilePackageInputProvider> _logger;

        public FilePackageInputProvider(string filePath, ILogger<FilePackageInputProvider> logger)
        {
            _logger = logger;
            _logger.LogInformation(
                "Opening the file to read the package input. FilePath={FilePath}",
                filePath
            );
            try
            {
                _reader = new(filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to open the package input file. FilePath={FilePath}",
                    filePath
                );
                throw;
            }
        }

        public string ReadLine()
        {
            string? value = _reader.ReadLine();
            if (value == null)
            {
                _logger.LogError("Reached to the end of the file");
                return _reader.ReadLine() ?? throw new EndOfStreamException("End of file reached");
            }
            else if (value == "-")
            {
                return string.Empty;
            }
            return value.Trim();
        }

        public void Dispose()
        {
            _logger.LogDebug("Closing package input file");
            _reader.Dispose();
        }
    }
}
