using System.Text.Json;
using KikiCourierApp.BLL.Interfaces;
using KikiCourierApp.BLL.Models;
using Microsoft.Extensions.Logging;

namespace KikiCourierApp.Infrastructure.InputProviders.DiscountInputProviders
{
    public class JsonDiscountInputProvider : IDiscountRules
    {
        private readonly Dictionary<string, DiscountRule> _discountRulesData =  [ ];

        // private readonly ILogger<JsonDiscountInputProvider> _logger;
        public IReadOnlyDictionary<string, DiscountRule> DiscountRulesData => _discountRulesData;

        public JsonDiscountInputProvider(string filePath, ILogger<JsonDiscountInputProvider> logger)
        {
            logger.LogInformation(
                "Reading discount rules from file. FilePath={FilePath}",
                filePath
            );
            try
            {
                var json = File.ReadAllText(filePath);
                var rules = JsonSerializer.Deserialize<Dictionary<string, DiscountRule>>(json);
                _discountRulesData = rules ?? [ ];
                logger.LogInformation("Loaded {count} discount rules", _discountRulesData.Count);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to load discount rules from file.");
            }
        }

        public DiscountRule? GetRule(string coupon)
        {
            if (_discountRulesData.TryGetValue(coupon, out var rule))
            {
                return rule;
            }
            return null;
        }
    }
}
