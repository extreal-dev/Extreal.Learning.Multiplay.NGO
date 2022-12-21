using Unity.Netcode.Components;

namespace ExtrealCoreLearning.MultiplayCommon
{
    public class ClientNetworkTransform : NetworkTransform
    {
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }
}