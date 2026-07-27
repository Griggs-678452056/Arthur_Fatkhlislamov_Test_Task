using Mirror;
using System.Collections.Generic;

namespace Scripts.Services
{
    public class NetworkMessagesService : INetworkMessagesService
    {
        private readonly Dictionary<NetworkConnectionToClient, HashSet<string>>
            _subscriptions = new();

        public void Subscribe(
            NetworkConnectionToClient connection,
            string messageType)
        {
            if (_subscriptions.TryGetValue(connection, out var types))
            {
                types = new HashSet<string>();

                _subscriptions.Add(connection, types);
            }

            types.Add(messageType);
        }

        public void Unsubscribe(NetworkConnectionToClient connection)
        {
            _subscriptions.Remove(connection);
        }

        public void SendToSubscribers<T>(T message)
            where T : struct, NetworkMessage
        {
            string messageType = typeof(T).Name;

            foreach (var pair in _subscriptions)
            {
                if (pair.Value.Contains(messageType))
                {
                    pair.Key.Send(message);
                }
            }
        }
    }
}