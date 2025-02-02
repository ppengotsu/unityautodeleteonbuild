using System.Collections.Generic;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using SyskenTLib.STAutoDeleteOnBuild;
using SyskenTLib.STAutoDeleteOnBuildEditor.Internal;
using UnityEditor;


namespace SyskenTLib.STAutoDeleteOnBuildEditor
{
    public class STBuildProcessSceneManager:IProcessSceneWithReport
    {
        public int callbackOrder => -9999;
        
        public void OnProcessScene(Scene scene, BuildReport report)
        {
            #if STAUTODELETE_NO_FORCE_DELETE
            Debug.Log("Cancel Auto Delete");
            return;
            #endif
            
            
            bool isEnableAutoDelete = IsEnableAutoDelete();

            if (isEnableAutoDelete == true)
            {
                Debug.Log("Start Auto Delete Scene "+ scene.name);
                
                //
                // シーン上の設定を拾う
                //
                STAutoDeleteSceneConfigData sceneConfigData = ReadSceneConfig(scene);

                if (sceneConfigData._sceneAutoDeleteType == SceneAutoDeleteType.ThisSceneEnableDelete)
                {
                    //削除が有効なシーン
                    
                    //しーんの設定ファイルの削除
                    if (sceneConfigData._originalSceneConfig != null)
                    {
                        Debug.Log("Delete Scene Config  "+ sceneConfigData._originalSceneConfig.gameObject +"\nOn "+scene.name+" Scene");
                        GameObject.DestroyImmediate(sceneConfigData._originalSceneConfig.gameObject);//対象のオブジェクトを削除
                    }
                    
                    //オブジェクト削除
                    if (EditorApplication.isPlaying == true)
                    {
                        //UnityEditorを再生した時
                        if (sceneConfigData._stTargetBuildType == STTargetBuildType.All
                            || sceneConfigData._stTargetBuildType == STTargetBuildType.UnityEditorOnly)
                        {
                                              
                            Debug.Log("Delete Objects On  "+ scene.name);
                            DeleteAllObjectsOnThisScene(scene);
                        }
                        else
                        {
#if STAUTODELETE_FORCE_DELETE_ON_UNITY_EDITOR
                            //強制削除
                            Debug.Log("Delete Objects On  "+ scene.name);
                            DeleteAllObjectsOnThisScene(scene);
#endif
                        }
                    }
                    else
                    {
                        //アプリをビルドした時
                        if (sceneConfigData._stTargetBuildType == STTargetBuildType.All
                            || sceneConfigData._stTargetBuildType == STTargetBuildType.BuildAppOnly)
                        {
                                              
                            Debug.Log("Delete Objects On  "+ scene.name);
                            DeleteAllObjectsOnThisScene(scene);
                        }
                        else
                        {
                            
                            //
                            #if STAUTODELETE_FORCE_DELETE
                            //強制削除
                            Debug.Log("Delete Objects On  "+ scene.name);
                            DeleteAllObjectsOnThisScene(scene);
                            #endif
                        }
                    }
                    
                   
                }
                
                
                
                Debug.Log("End Auto Delete Scene "+ scene.name);
            }
        }

        private bool IsEnableAutoDelete()
        {
            return true;
        }

        private STAutoDeleteSceneConfigData ReadSceneConfig(Scene scene)
        {
            STAutoDeleteSceneConfigData sceneConfigData = new STAutoDeleteSceneConfigData();
            
            
            //
            //  デフォルト設定
            //
            sceneConfigData._sceneAutoDeleteType = SceneAutoDeleteType.ThisSceneEnableDelete;
            sceneConfigData._stTargetBuildType = STTargetBuildType.All;
            
            
            
            List<STAutoDeleteSceneConfig> sceneConfigList =  scene
                .GetRootGameObjects()
                .Select(nextObject => nextObject.GetComponentsInChildren<STAutoDeleteSceneConfig>().ToList())
                .FirstOrDefault(nextConfigList => (nextConfigList != null && nextConfigList.Count > 0));
            
            
            if (sceneConfigList == null || sceneConfigList.Count == 0)
            {
                Debug.Log("No Scene Config On "+ scene.name);
                return sceneConfigData;
            }
            STAutoDeleteSceneConfig sceneConfig = sceneConfigList[0];

            if (sceneConfig == null)
            {
                Debug.Log("No Scene Config On "+ scene.name);
                return sceneConfigData;
            }

            //
            //  設定上書き
            //
            sceneConfigData._sceneAutoDeleteType = sceneConfig.GetSceneAutoDeleteType;
            sceneConfigData._stTargetBuildType = sceneConfig.GetStTargetBuildType;
            
            sceneConfigData._originalSceneConfig = sceneConfig;
            
            
            return sceneConfigData;
        }

        private void DeleteAllObjectsOnThisScene(Scene scene)
        {
            List<List<STAutoDeleteTarget>> autoDeleteListList = scene
                .GetRootGameObjects()
                .Select(nextObject => nextObject.GetComponentsInChildren<STAutoDeleteTarget>().ToList())
                .ToList();
            
            autoDeleteListList.ForEach(autoDeleteList =>
            {
                autoDeleteList.ForEach(autoDeleteScript =>
                {
                    Debug.Log("Delete  "+ autoDeleteScript.gameObject +"\nOn "+scene.name+" Scene");
                    GameObject.DestroyImmediate(autoDeleteScript.gameObject);//対象のオブジェクトを削除
                });
            }); 

        }
        
        
    }
}