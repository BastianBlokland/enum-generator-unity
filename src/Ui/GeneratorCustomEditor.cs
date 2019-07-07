#if UNITY_EDITOR
using UnityEngine;

namespace EnumGenerator.Editor.Ui
{
    /// <summary>
    /// Custom editor ui for <see cref="Generator"/>.
    /// </summary>
    [UnityEditor.CustomEditor(typeof(Generator))]
    public class GeneratorCustomEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var generator = target as Generator;
            if (generator == null)
                return;

            if (GUILayout.Button("Generate"))
                generator.GenerateAll();
        }
    }
}
#endif
