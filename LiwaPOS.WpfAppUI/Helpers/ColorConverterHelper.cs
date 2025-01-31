using System.Globalization;

namespace LiwaPOS.WpfAppUI.Helpers
{
    public static class ColorConverterHelper
    {
        /// <summary>
        /// Color nesnesini HEX string formatına çevirir. (Örn: "#FF6E8E2C")
        /// </summary>
        public static string ColorToHex(System.Windows.Media.Color color)
        {
            return $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        /// <summary>
        /// HEX string formatını Color nesnesine çevirir.
        /// Desteklenen formatlar: "#AARRGGBB", "#RRGGBB"
        /// </summary>
        public static System.Windows.Media.Color HexToColor(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex))
                throw new ArgumentException("Hex değeri boş olamaz!");

            hex = hex.Replace("#", "").Trim();

            if (hex.Length == 8) // "#AARRGGBB"
            {
                return System.Windows.Media.Color.FromArgb(
                    byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber),
                    byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber),
                    byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber),
                    byte.Parse(hex.Substring(6, 2), NumberStyles.HexNumber)
                );
            }
            else if (hex.Length == 6) // "#RRGGBB"
            {
                return System.Windows.Media.Color.FromArgb(255, // Alpha değeri 255 (tam opak)
                    byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber),
                    byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber),
                    byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber)
                );
            }
            else
            {
                throw new ArgumentException("Geçersiz HEX formatı! Doğru format: #RRGGBB veya #AARRGGBB");
            }
        }

        /// <summary>
        /// Color nesnesini ARGB tamsayı formatına çevirir.
        /// </summary>
        public static int ColorToArgbInt(System.Windows.Media.Color color)
        {
            return (color.A << 24) | (color.R << 16) | (color.G << 8) | color.B;
        }

        /// <summary>
        /// ARGB tamsayı formatını Color nesnesine çevirir.
        /// </summary>
        public static System.Windows.Media.Color ArgbIntToColor(int argb)
        {
            return System.Windows.Media.Color.FromArgb(
                (byte)((argb >> 24) & 0xFF),  // Alpha
                (byte)((argb >> 16) & 0xFF),  // Red
                (byte)((argb >> 8) & 0xFF),   // Green
                (byte)(argb & 0xFF)           // Blue
            );
        }

        /// <summary>
        /// Color nesnesini RGB int dizisine çevirir. (R, G, B)
        /// </summary>
        public static int[] ColorToRgbArray(System.Windows.Media.Color color)
        {
            return new int[] { color.R, color.G, color.B };
        }
      
    }
}
