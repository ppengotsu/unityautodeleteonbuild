
# 使い方

1. 各シーンに、Prefabs/STAutoDeleteSceneConfigオブジェクトを追加する
2. 削除したいオブジェクトにSTAutoDeleteTargetスクリプトを追加する
3. 通常通りビルドするだけ



# パラメータ

## STAutoDeleteSceneConfigスクリプト

シーンに追加したSTAutoDeleteSceneConfigオブジェクトについてます

* sceneAutoDeleteType
  * このシーンのオブジェクト削除を行うかどうか
* stTargetBuildType
  * UnityEditor再生時、ビルド時、両方のどれで削除を実行するか

## マクロ

マクロは、プロジェクト設定などに追加してください

* STAUTODELETE_NO_FORCE_DELETE
  * どの削除もしない
* STAUTODELETE_FORCE_DELETE
  * ビルド時に、強制的にオブジェクトの削除を行います
  * STAutoDeleteSceneConfigのsceneAutoDeleteTypeで、そのシーンでは削除しないにしている場合は、削除されません
* STAUTODELETE_FORCE_DELETE_ON_UNITY_EDITOR
  * UnityEditor再生時に、強制的にオブジェクトの削除を行います
  * STAutoDeleteSceneConfigのsceneAutoDeleteTypeで、そのシーンでは削除しないにしている場合は、削除されません