using Mirror;

namespace Scripts.Services
{
    public interface INetworkMessagesService
    {
        void Subscribe(
            NetworkConnectionToClient connection,
            string messageType);            

        void Unsubscribe(NetworkConnectionToClient connection);

        void SendToSubscribers<T>(T message)
            where T : struct, NetworkMessage;
    }
}
