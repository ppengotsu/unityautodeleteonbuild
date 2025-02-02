using SyskenTLib.STAutoDeleteOnBuild;

namespace SyskenTLib.STAutoDeleteOnBuildEditor.Internal
{
    public class STAutoDeleteSceneConfigData
    {
        public SceneAutoDeleteType _sceneAutoDeleteType = SceneAutoDeleteType.ThisSceneEnableDelete;
        public STTargetBuildType _stTargetBuildType = STTargetBuildType.All;
        
        public STAutoDeleteSceneConfig _originalSceneConfig = null;
    }
}