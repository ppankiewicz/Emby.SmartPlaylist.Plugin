using System.Threading.Tasks;

namespace SmartPlaylist.Infrastructure.MesssageBus
{
    public interface IMessageHandlerAsync<in T> where T : IMessage
    {
        Task HandleAsync(T message);
    }
}