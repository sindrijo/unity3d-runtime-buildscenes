using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RuntimeBuildscenes
{

    [Serializable]
    public class BuildSceneRecord
    {
        [SerializeField] private string name;
        [SerializeField] private int buildIndex;
        [SerializeField] private string path;

        public BuildSceneRecord(string path, int buildIndex)
        {
            this.path = path;
            var nameStart = path.LastIndexOf('/') + 1;
            var nameLength = path.LastIndexOf('.') - nameStart;
            name = path.Substring(nameStart, nameLength);
            this.buildIndex = buildIndex;
        }

        public int BuildIndex
        {
            get { return buildIndex; }
        }

        public string Path
        {
            get { return path; }
        }

        public string Name
        {
            get { return name; }
        }

        public bool IsLoaded
        {
            get
            {
#if UNITY_5_3_OR_NEWER
                return SceneManager.GetSceneByBuildIndex(buildIndex).isLoaded;
#else
                return Application.loadedLevel == buildIndex;
#endif
            }
        }

        public override string ToString()
        {
            return string.Format(@"[{0}] ""{1}"" ({2})", buildIndex, name, path);
        }
    }

}