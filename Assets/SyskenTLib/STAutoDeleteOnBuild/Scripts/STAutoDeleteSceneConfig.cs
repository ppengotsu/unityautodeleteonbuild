using UnityEngine;
using UnityEngine.Serialization;

namespace SyskenTLib.STAutoDeleteOnBuild
{
    public class STAutoDeleteSceneConfig: MonoBehaviour
    {
        [SerializeField] private SceneAutoDeleteType _sceneAutoDeleteType = SceneAutoDeleteType.ThisSceneEnableDelete;
        public SceneAutoDeleteType GetSceneAutoDeleteType => _sceneAutoDeleteType;
        
        
        [SerializeField] private STTargetBuildType _stTargetBuildType = STTargetBuildType.All;
        public STTargetBuildType GetStTargetBuildType => _stTargetBuildType;
    }
}