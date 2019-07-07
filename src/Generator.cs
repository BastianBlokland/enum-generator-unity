using UnityEngine;

namespace EnumGenerator.Editor
{
    /// <summary>
    /// ScriptableObject for configuring and running the enum-generator.
    /// </summary>
    [CreateAssetMenu(fileName = "EnumGenerator", menuName = "EnumGenerator/Generator")]
    public sealed class Generator : ScriptableObject
    {
#pragma warning disable CS0649
        [SerializeField] private Options[] enums = new[] { new Options() };
#pragma warning restore CS0649

        /// <summary>
        /// Generate all enums configured on this generator.
        /// </summary>
        public void GenerateAll()
        {
            foreach (var options in enums)
            {
                if (options == null)
                    continue;
                GeneratorApi.GenerateEnumToFile(options, logContext: this);
            }
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }

#if UNITY_EDITOR
        public void GenerateForFile(string inputFilePath)
        {
            foreach (var options in enums)
            {
                if (options == null)
                    continue;
                if (options.AutoGenerate && options.InputFilePath == inputFilePath)
                    GeneratorApi.GenerateEnumToFile(options, logContext: this);
            }
            UnityEditor.AssetDatabase.Refresh();
        }
#endif
    }
}
