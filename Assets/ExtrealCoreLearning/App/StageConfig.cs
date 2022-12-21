using Extreal.Core.StageNavigation;
using UnityEngine;

namespace ExtrealCoreLearning.App
{
    [CreateAssetMenu(
        menuName = "Config/" + nameof(StageConfig),
        fileName = nameof(StageConfig))]
    public class StageConfig : StageConfigBase<StageName, SceneName>
    {
    }
}