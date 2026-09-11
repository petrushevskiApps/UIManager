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

        // The suffix ladder past T, indexed by exponent/3. Kept identical to BigNumber.ToDisplayString
        // so a long/double routed through here reads the same as a BigNumber balance — a currency
        // that outgrew long still shows "6.5Qi", not "6500000T". Index 0-4 ("", K, M, B, T) are
        // handled by the fast paths below; the array carries them too so the generic rung maths lines
        // up with its own index.
        private static readonly string[] Suffixes =
        {
            "", "K", "M", "B", "T",
            "Qa", "Qi", "Sx", "Sp", "Oc", "No", "Dc",
            "Ud", "Dd", "Td", "Qad", "Qid", "Sxd", "Spd", "Ocd", "Nod", "Vg"
        };

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
                // T and beyond: climb the ladder instead of dead-ending at T. Past the ladder's end
                // (~1e66, far beyond anything a run reaches) fall back to scientific notation.
                int rung = (int)System.Math.Floor(System.Math.Log10(value) / 3.0);
                if (rung >= Suffixes.Length)
                {
                    int exponent = (int)System.Math.Floor(System.Math.Log10(value));
                    double mantissa = value / System.Math.Pow(10.0, exponent);
                    return mantissa.ToString("0.##") + "e" + exponent;
                }
                return FormatWithSuffix(value / System.Math.Pow(10.0, rung * 3), Suffixes[rung], useTwoDecimals);
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
