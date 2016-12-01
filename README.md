# unity3d-runtime-buildscenes
The Unity 3d API for scene management is not perfect, it relies on names and paths at run-time yet provides no way to get information about the scenes that were added to the build settings. This project aims to alleviate that by automatically creating/updating an asset with this information when a build happens and also provides a easy way to access the information at run-time in a build.
# Example
```cs

BuildSceneRecord[] buildSettingsSceneRecords = BuildScenes.Records;
foreach(var buildSceneRecord in buildSettingsSceneRecords)
{
    Debug.Log(buildSceneRecord.BuildIndex);
    Debug.Log(buildSceneRecord.Name);
    Debug.Log(buildSceneRecord.Path);
}

```
