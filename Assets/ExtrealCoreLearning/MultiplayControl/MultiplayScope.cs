using VContainer;
using VContainer.Unity;

namespace ExtrealCoreLearning.MultiplayControl
{
    public class MultiplayControlScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<MultiplayRoom>(Lifetime.Singleton);

            builder.RegisterEntryPoint<MultiplayControlPresenter>();
        }
    }
}