using Mirror;
using Scripts.Messages;
using Scripts.Services;
using UnityEngine;
using Zenject;

namespace Scripts.Network
{
    public class TestNetworkManager : NetworkManager
    {
        private INetworkMessagesService _networkMessagesService;

        [Inject]
        public void Construct(INetworkMessagesService networkMessagesService)
        {
            _networkMessagesService = networkMessagesService;
        }

        public override void Awake()
        {
            Debug.Log("NetworkManager Awake");
        }

        public override void OnStartServer()
        {
            base.OnStartServer();

            NetworkServer.RegisterHandler<SubscribeMessage>(
                OnSubscribeMessageReceived);

            Debug.Log("Server started.");
        }

        public override void OnServerDisconnect(
            NetworkConnectionToClient conn)
        {
            _networkMessagesService.Unsubscribe(conn);

            base.OnServerDisconnect(conn);
        }

        private void OnSubscribeMessageReceived(
            NetworkConnectionToClient connection,
            SubscribeMessage message)
        {
            _networkMessagesService.Subscribe(
                connection,
                message.MessageType);

            Debug.Log($"Client {connection.connectionId} subscribed to {message.MessageType}.");

            _networkMessagesService.SendToSubscribers(
                new HelloMessage
                {
                    Text = $"Hello Client!"
                });
        }
    }
}