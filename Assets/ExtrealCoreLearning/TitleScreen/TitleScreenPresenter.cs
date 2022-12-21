using System;
using Cysharp.Threading.Tasks;
using Extreal.Core.StageNavigation;
using ExtrealCoreLearning.App;
using UniRx;
using VContainer.Unity;

namespace ExtrealCoreLearning.TitleScreen
{
    public class TitleScreenPresenter : IInitializable, IDisposable
    {
        private StageNavigator<StageName, SceneName> stageNavigator;

        private TitleScreenView titleScreenView;

        private CompositeDisposable compositeDisposable = new CompositeDisposable();

        public TitleScreenPresenter(StageNavigator<StageName, SceneName> stageNavigator,
            TitleScreenView titleScreenView)
        {
            this.stageNavigator = stageNavigator;
            this.titleScreenView = titleScreenView;
        }

        public void Initialize()
        {
            titleScreenView.OnGoButtonClicked.Subscribe(_ =>
            {
                stageNavigator.ReplaceAsync(StageName.VirtualStage).Forget();
            }).AddTo(compositeDisposable);
        }

        public void Dispose()
        {
            compositeDisposable?.Dispose();
        }
    }
}