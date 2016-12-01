namespace RuntimeBuildscenes
{
    using System;
    using UnityEngine;

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
            var nameEnd = (path.Length - 1) - (path.LastIndexOf('.') + 1);
            name = path.Substring(nameStart, nameEnd);
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

        public override string ToString()
        {
            return string.Format(@"[{0}] ""{1}"" ({2})", buildIndex, name, path);
        }
    }

}