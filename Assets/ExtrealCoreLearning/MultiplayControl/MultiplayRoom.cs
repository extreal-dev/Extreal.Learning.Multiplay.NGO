using Cysharp.Threading.Tasks;
// highlight-start
using Extreal.Core.Common.System;
// highlight-end
using Extreal.Core.Logging;
using Extreal.Integration.Multiplay.NGO;
// highlight-start
using ExtrealCoreLearning.MultiplayCommon;
using UniRx;
using Unity.Collections;
using Unity.Netcode;
// highlight-end

namespace ExtrealCoreLearning.MultiplayControl
{
    // highlight-start
    public class MultiplayRoom : DisposableBase
    // highlight-end
    {
        private static readonly ELogger Logger = LoggingManager.GetLogger(nameof(MultiplayRoom));
        private readonly NgoClient ngoClient;
        // highlight-start
        private readonly CompositeDisposable disposables = new CompositeDisposable();
        // highlight-end

        public MultiplayRoom(NgoClient ngoClient)
        {
            this.ngoClient = ngoClient;

            // highlight-start
            ngoClient.OnConnected
                .Subscribe(_ => SendPlayerSpawnMessage(ngoClient))
                .AddTo(disposables);
            // highlight-end
        }

        // highlight-start
        private static void SendPlayerSpawnMessage(NgoClient ngoClient)
        {
            var messageStream
                = new FastBufferWriter(FixedString64Bytes.UTF8MaxLengthInBytes, Allocator.Temp);
            ngoClient.SendMessage(MessageName.PlayerSpawn.ToString(), messageStream);
        }
        // highlight-end

        public async UniTask JoinAsync()
        {
            await ngoClient.ConnectAsync(new NgoConfig());
            if (Logger.IsDebug())
            {
                Logger.LogDebug("Joined");
            }
        }

        public async UniTask LeaveAsync()
        {
            await ngoClient.DisconnectAsync();
            if (Logger.IsDebug())
            {
                Logger.LogDebug("Left");
            }
        }

        // highlight-start
        protected override void ReleaseManagedResources() => disposables.Dispose();
        // highlight-end
    }
}