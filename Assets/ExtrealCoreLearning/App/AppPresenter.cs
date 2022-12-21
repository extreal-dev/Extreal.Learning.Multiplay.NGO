using System.Threading;
using Cysharp.Threading.Tasks;
using Extreal.Core.StageNavigation;
using VContainer.Unity;

namespace ExtrealCoreLearning.App
{
    public class AppPresenter : IAsyncStartable
    {
        private StageNavigator<StageName, SceneName> stageNavigator;

        public AppPresenter(StageNavigator<StageName, SceneName> stageNavigator)
        {
            this.stageNavigator = stageNavigator;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            await stageNavigator.ReplaceAsync(StageName.TitleStage);
        }
    }
}