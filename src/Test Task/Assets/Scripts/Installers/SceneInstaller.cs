using Scripts.Network;
using Scripts.Services;
using UnityEngine;
using Zenject;

namespace Scripts.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private TestNetworkManager _networkManager;

        public override void InstallBindings()
        {
            Container.Bind<INetworkMessagesService>()
                .To<NetworkMessagesService>()
                .AsSingle();

            Container.Bind<TestNetworkManager>()
                .FromInstance(_networkManager)
                .AsSingle();

            Debug.Log("SceneInstaller initialized");
        }
    }
}