using Cysharp.Threading.Tasks;
// highlight-start
using Extreal.Core.Common.System;
using Extreal.Core.Logging;
// highlight-end
using Extreal.Integration.Multiplay.NGO;
// highlight-start
using ExtrealCoreLearning.MultiplayCommon;
using UniRx;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AddressableAssets;
// highlight-end

namespace ExtrealCoreLearning.MultiplayServer
{
    // highlight-start
    public class MultiplayServer : DisposableBase
    // highlight-end
    {
        // highlight-start
        private static readonly ELogger Logger = LoggingManager.GetLogger(nameof(MultiplayServer));
        private readonly CompositeDisposable disposables = new CompositeDisposable();
        // highlight-end
        private readonly NgoServer ngoServer;

        public MultiplayServer(NgoServer ngoServer)
        {
            this.ngoServer = ngoServer;

            // highlight-start
            this.ngoServer.OnServerStarted.Subscribe(_ =>
            {
                ngoServer.RegisterMessageHandler(
                    MessageName.PlayerSpawn.ToString(), PlayerSpawnMessageHandler);
            }).AddTo(disposables);

            this.ngoServer.OnServerStopping.Subscribe(_ =>
            {
                ngoServer.UnregisterMessageHandler(MessageName.PlayerSpawn.ToString());
            }).AddTo(disposables);
            // highlight-end
        }

        // highlight-start
        private async void PlayerSpawnMessageHandler(ulong clientId, FastBufferReader messageStream)
        {
            if (Logger.IsDebug())
            {
                Logger.LogDebug($"{MessageName.PlayerSpawn}: {clientId}");
            }

            ngoServer.SpawnAsPlayerObject(clientId, await LoadPlayerPrefab());
        }
        // highlight-end

        // highlight-start
        private static async UniTask<GameObject> LoadPlayerPrefab()
        {
            var result = Addressables.LoadAssetAsync<GameObject>("PlayerPrefab");
            return await result.Task;
        }
        // highlight-end

        public UniTask StartAsync()
        {
            return ngoServer.StartServerAsync();
        }

        // highlight-start
        protected override void ReleaseManagedResources()
        {
            disposables.Dispose();
        }
        // highlight-end
    }
}