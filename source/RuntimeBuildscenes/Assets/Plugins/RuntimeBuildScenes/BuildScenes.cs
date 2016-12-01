namespace RuntimeBuildscenes
{
    using System.Linq;
    using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
    using UnityEditor.Callbacks;
#endif

    public static class BuildScenes
    {
        public static BuildSceneRecord[] Records
        {
            get { return SInstance.BuildSceneRecords; }
        }

        private static BuildScenesAsset s_instance;

        private static BuildScenesAsset SInstance
        {
            get
            {
                if (s_instance == null)
                {
                    LoadBuildScenesAsset();
                }

                return s_instance;
            }
        }

        [RuntimeInitializeOnLoadMethod]
        private static void LoadBuildScenesAsset()
        {
            s_instance = Resources.Load<BuildScenesAsset>(typeof(BuildScenesAsset).Name);
            if (s_instance == null)
            {
                Debug.LogWarning("Asset named 'BuildScenes' not found, trying brute force...");
                Resources.LoadAll<BuildScenesAsset>(string.Empty);
                s_instance = Resources.FindObjectsOfTypeAll<BuildScenesAsset>().FirstOrDefault();
            }

#if UNITY_EDITOR
            if (s_instance == null)
            {
                s_instance = CreateBuildScenesAsset();
                Debug.LogWarning("Created asset: " + typeof(BuildScenesAsset).Name);
            }
#endif
        }

#if UNITY_EDITOR
        [PostProcessScene]
        private static void OnPostProcessScene()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode) return;
            SInstance.BuildSceneRecords = GenerateBuildSettingsSceneRecords();
        }

        private static BuildSceneRecord[] GenerateBuildSettingsSceneRecords()
        {
            return EditorBuildSettings.scenes.Select((scene, i) => new BuildSceneRecord(scene.path, i)).ToArray();
        }

        private static BuildScenesAsset CreateBuildScenesAsset()
        {
            if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            {
                Debug.LogWarning("Created 'Resources' folder at 'Assets' root folder");
                AssetDatabase.CreateFolder("Assets", "Resources");
            }
            var buildScenesAsset = ScriptableObject.CreateInstance<BuildScenesAsset>();
            buildScenesAsset.BuildSceneRecords = GenerateBuildSettingsSceneRecords();
            AssetDatabase.CreateAsset(buildScenesAsset, "Assets/Resources/" + typeof(BuildScenesAsset).Name + ".asset");
            AssetDatabase.Refresh();
            return buildScenesAsset;
        }
#endif

    }

}