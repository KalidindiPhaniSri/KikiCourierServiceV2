using KikiCourierApp.BLL.Interfaces;
using KikiCourierApp.BLL.Models;
using Microsoft.Extensions.Logging;

namespace KikiCourierApp.BLL.Services
{
    public class PackageReader
    {
        private readonly IPackageInputProvider _input;
        private readonly List<Package> _packages =  [ ];
        private readonly ILogger<PackageReader> _logger;
        public double BaseDeliveryPrice { get; private set; }
        public int MaxWeight { get; private set; }
        public int VehicleCount { get; private set; }
        public int Speed { get; private set; }
        public IReadOnlyList<Package> Packages => _packages;

        public PackageReader(IPackageInputProvider input, ILogger<PackageReader> logger)
        {
            _input = input;
            _logger = logger;
        }

        public void ReadInput()
        {
            _logger.LogInformation("Started to read the input");
            ReadBaseDeliveryPrice();
            ReadPackages();
            _logger.LogInformation(
                "Finished reading input. BaseDeliveryPrice={BaseDeliveryPrice}, PackagesCount={PackagesCount}, VehicleCount={VehicleCount}, MaxWeight={MaxWeight}, Speed={Speed}",
                BaseDeliveryPrice,
                _packages.Count,
                VehicleCount,
                MaxWeight,
                Speed
            );
            ReadVehicleDetails();
        }

        public int GetPackagesCount()
        {
            return _packages.Count;
        }

        private double ReadDouble(string fieldName)
        {
            _logger.LogInformation("Reading {fieldName}", fieldName);
            if (!double.TryParse(_input.ReadLine(), out double value))
            {
                _logger.LogError("Invalid value for {FieldName}. Input={Input}", fieldName, value);

                throw new InvalidDataException($"{fieldName} must be a number");
            }
            return value;
        }

        private int ReadInt(string fieldName)
        {
            _logger.LogInformation("Reading {fieldName}", fieldName);
            if (!int.TryParse(_input.ReadLine(), out int value))
            {
                _logger.LogError("Invalid value for {FieldName}. Input={Input}", fieldName, value);

                throw new InvalidDataException($"{fieldName} must be a number");
            }
            return value;
        }

        private void ReadBaseDeliveryPrice()
        {
            BaseDeliveryPrice = ReadDouble("Base delivery price");
        }

        private void ReadVehicleDetails()
        {
            VehicleCount = ReadInt("Vehicle count");
            Speed = ReadInt("Vehicle speed");
            MaxWeight = ReadInt("Maximum weight");
        }

        public void AddSinglePackage(string id, int weight, int distance, string couponCode)
        {
            _packages.Add(new Package(id, weight, distance, couponCode));
            _logger.LogInformation("Package {id} added", id);
        }

        private void ReadPackages()
        {
            int count = ReadInt("Packages count");
            _logger.LogInformation("Read packages");
            for (int i = 0; i < count; i++)
            {
                _logger.LogInformation("Reading package id");
                string id = _input.ReadLine();
                int weight = ReadInt("Package weight");
                int distance = ReadInt("Delivery distance");
                _logger.LogInformation("Reading package coupon code");
                string couponCode = _input.ReadLine();
                AddSinglePackage(id, weight, distance, couponCode);
            }
            _logger.LogInformation("Packages: {@Packages}", _packages);
        }
    }
}
