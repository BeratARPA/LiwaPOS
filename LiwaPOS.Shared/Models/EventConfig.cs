using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Models.Entities;

namespace LiwaPOS.Shared.Models
{
    public class EventConfig
    {
        public static List<EventMetadata> GetEventMetadata()
        {
            return new List<EventMetadata>
            {
                new EventMetadata { EventType = EventType.UserLoggedIn, DataObjectType = typeof(UserDTO) },
                new EventMetadata { EventType = EventType.UserLoggedOut, DataObjectType = typeof(UserDTO) },
                new EventMetadata { EventType = EventType.PageOpened, DataObjectType = typeof(PageOpenedDTO) },
                new EventMetadata { EventType = EventType.AutomationCommandExecuted, DataObjectType = typeof(AutomationCommandDTO) },
            };
        }
    }
}
