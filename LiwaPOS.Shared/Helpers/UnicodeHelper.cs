using System.Collections.ObjectModel;

namespace LiwaPOS.Shared.Helpers
{
    public static class UnicodeHelper
    {
        public static ObservableCollection<string> GetAllSymbols()
        {
            var symbols = new ObservableCollection<string>();

            // Smiley Emojiler
            for (int i = 0x1F600; i <= 0x1F64F; i++)
            {
                symbols.Add(char.ConvertFromUtf32(i));
            }

            // Sembol ve Objeler
            for (int i = 0x1F300; i <= 0x1F5FF; i++)
            {
                symbols.Add(char.ConvertFromUtf32(i));
            }

            // Çeşitli semboller
            for (int i = 0x2600; i <= 0x26FF; i++)
            {
                symbols.Add(char.ConvertFromUtf32(i));
            }

            // Ekstra semboller
            for (int i = 0x2700; i <= 0x27BF; i++)
            {
                symbols.Add(char.ConvertFromUtf32(i));
            }

            return symbols;
        }
    }
}
