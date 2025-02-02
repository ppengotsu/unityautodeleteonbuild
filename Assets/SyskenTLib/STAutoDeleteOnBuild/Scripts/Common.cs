namespace SyskenTLib.STAutoDeleteOnBuild
{
    public enum SceneAutoDeleteType : int
    {
        ThisSceneEnableDelete = 0
        ,NoTargetScene =10
    }
    
    public enum STTargetBuildType : int
    {
        All = 0
        ,BuildAppOnly =10
        ,UnityEditorOnly =20
    }
}