using Cysharp.Threading.Tasks;
using Extreal.Core.Common.System;
using Extreal.Core.StageNavigation;
using ExtrealCoreLearning.App;
using UniRx;
using VContainer.Unity;

namespace ExtrealCoreLearning.MultiplayControl
{
    public class MultiplayControlPresenter : DisposableBase, IInitializable
    {
        private readonly StageNavigator<StageName, SceneName> stageNavigator;
        private readonly MultiplayRoom multiplayRoom;
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        public MultiplayControlPresenter(StageNavigator<StageName, SceneName> stageNavigator,
            MultiplayRoom multiplayRoom)
        {
            this.stageNavigator = stageNavigator;
            this.multiplayRoom = multiplayRoom;
        }

        public void Initialize()
        {
            stageNavigator.OnStageTransitioned
                .Subscribe(_ => multiplayRoom.JoinAsync().Forget())
                .AddTo(disposables);

            stageNavigator.OnStageTransitioning
                .Subscribe(_ => multiplayRoom.LeaveAsync().Forget())
                .AddTo(disposables);
        }

        protected override void ReleaseManagedResources()
        {
            disposables.Dispose();
        }
    }
}