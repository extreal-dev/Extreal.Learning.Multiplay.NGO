using System;
using Cysharp.Threading.Tasks;
using Extreal.Core.StageNavigation;
using ExtrealCoreLearning.App;
using UniRx;
using VContainer.Unity;

namespace ExtrealCoreLearning.VirtualSpace
{
    public class VirtualSpacePresenter : IInitializable, IDisposable
    {
        private StageNavigator<StageName, SceneName> stageNavigator;
        private VirtualSpaceView virtualSpaceView;

        private CompositeDisposable disposables = new CompositeDisposable();

        public VirtualSpacePresenter(StageNavigator<StageName, SceneName> stageNavigator,
            VirtualSpaceView virtualSpaceView)
        {
            this.stageNavigator = stageNavigator;
            this.virtualSpaceView = virtualSpaceView;
        }

        public void Initialize()
        {
            virtualSpaceView.OnBackButtonClicked.Subscribe(_ =>
            {
                stageNavigator.ReplaceAsync(StageName.TitleStage).Forget();
            }).AddTo(disposables);
        }

        public void Dispose()
        {
            disposables.Dispose();
        }
    }
}