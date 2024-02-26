using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace ExtrealCoreLearning.MultiplayServer
{
    public class MultiplayServerPresenter : IStartable
    {
        private readonly MultiplayServer multiplayServer;

        public MultiplayServerPresenter(MultiplayServer multiplayServer)
        {
            this.multiplayServer = multiplayServer;
        }

        public void Start()
        {
            multiplayServer.StartAsync().Forget();
        }
    }
}