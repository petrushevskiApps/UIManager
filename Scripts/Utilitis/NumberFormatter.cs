namespace TwoOneTwoGames.UIManager.Utilities
{
    public enum SuffixThreshold
    {
        None,
        Thousand,
        Million,
        Billion,
        Trillion
    }

    public static class NumberFormatter
    {
        private const double THOUSAND = 1000.0;
        private const double MILLION = 1000000.0;
        private const double BILLION = 1000000000.0;
        private const double TRILLION = 1000000000000.0;

        public static string FormatWithSuffix(double value, bool useTwoDecimals = false, SuffixThreshold threshold = SuffixThreshold.Thousand)
        {
            double minThreshold = GetThresholdValue(threshold);

            if (value < minThreshold)
            {
                return value.ToString("N0");
            }
            else if (value < MILLION)
            {
                return FormatWithSuffix(value / THOUSAND, "K", useTwoDecimals);
            }
            else if (value < BILLION)
            {
                return FormatWithSuffix(value / MILLION, "M", useTwoDecimals);
            }
            else if (value < TRILLION)
            {
                return FormatWithSuffix(value / BILLION, "B", useTwoDecimals);
            }
            else
            {
                return FormatWithSuffix(value / TRILLION, "T", useTwoDecimals);
            }
        }

        private static double GetThresholdValue(SuffixThreshold threshold)
        {
            switch (threshold)
            {
                case SuffixThreshold.None:
                    return double.MaxValue;
                case SuffixThreshold.Thousand:
                    return THOUSAND;
                case SuffixThreshold.Million:
                    return MILLION;
                case SuffixThreshold.Billion:
                    return BILLION;
                case SuffixThreshold.Trillion:
                    return TRILLION;
                default:
                    return THOUSAND;
            }
        }

        private static string FormatWithSuffix(double value, string suffix, bool useTwoDecimals)
        {
            if (useTwoDecimals)
            {
                return $"{value:F2}{suffix}";
            }
            
            if (value >= 100)
            {
                return $"{value:F0}{suffix}";
            }
            else if (value >= 10)
            {
                return $"{value:F1}{suffix}";
            }
            else
            {
                return $"{value:F1}{suffix}";
            }
        }
    }
}
