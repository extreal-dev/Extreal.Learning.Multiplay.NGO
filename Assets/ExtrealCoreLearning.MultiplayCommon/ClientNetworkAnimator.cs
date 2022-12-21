using Unity.Netcode.Components;

namespace ExtrealCoreLearning.MultiplayCommon
{
    public class ClientNetworkAnimator : NetworkAnimator
    {
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }
}