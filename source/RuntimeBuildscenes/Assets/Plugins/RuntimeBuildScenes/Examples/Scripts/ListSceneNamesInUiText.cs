namespace RuntimeBuildscenes
{
    using UnityEngine ;

    using System.Collections;
    using System.Linq;
    using UnityEngine.UI;

    public class ListSceneNamesInUiText : MonoBehaviour
    {
        private void Awake()
        {
            var textComponent = GetComponent<Text>();
            textComponent.text = new string(BuildScenes.Records.SelectMany(sr => sr.ToString() + "\n").ToArray());
        }
    }

}