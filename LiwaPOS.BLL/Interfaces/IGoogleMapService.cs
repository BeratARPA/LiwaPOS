using LiwaPOS.Shared.Models;

namespace LiwaPOS.BLL.Interfaces
{
    public interface IGoogleMapService
    {
        Task<string> GetDirectionAsync(ShowGoogleMapsDirectionDTO showGoogleMapsDirectionDto);
    }
}
