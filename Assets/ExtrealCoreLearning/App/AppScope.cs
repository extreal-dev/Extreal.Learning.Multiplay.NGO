// highlight-start
using Extreal.Core.Common.Retry;
// highlight-end
using Extreal.Core.Logging;
using Extreal.Core.StageNavigation;
// highlight-start
using Extreal.Integration.Multiplay.NGO;
using Unity.Netcode;
// highlight-end
using UnityEngine;
using VContainer;
using VContainer.Unity;
// highlight-start
using LogLevel = Extreal.Core.Logging.LogLevel;
// highlight-end

namespace ExtrealCoreLearning.App
{
    public class AppScope : LifetimeScope
    {
        [SerializeField] private StageConfig stageConfig;

        // highlight-start
        [SerializeField] private NetworkManager networkManager;
        // highlight-end

        private static void InitializeApp()
        {
            const LogLevel logLevel = LogLevel.Debug;
            LoggingManager.Initialize(logLevel: logLevel);

            var logger = LoggingManager.GetLogger(nameof(AppScope));
            if (logger.IsDebug())
            {
                logger.LogDebug("Hello, world!");
            }
        }

        protected override void Awake()
        {
            InitializeApp();
            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(stageConfig).AsImplementedInterfaces();
            builder.Register<StageNavigator<StageName, SceneName>>(Lifetime.Singleton);

            // highlight-start
            builder.RegisterComponent(networkManager);
            builder.Register<NgoClient>(Lifetime.Singleton)
                .WithParameter(typeof(IRetryStrategy), NoRetryStrategy.Instance);
            // highlight-end

            builder.RegisterEntryPoint<AppPresenter>();
        }
    }
}