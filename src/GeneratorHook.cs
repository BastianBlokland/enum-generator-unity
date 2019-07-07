#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;

namespace EnumGenerator.Editor
{
    /// <summary>
    /// Class for hooking into Unity events / menus.
    /// </summary>
    public class GeneratorHook : UnityEditor.AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            var generators = FindGenerators().ToArray();
            foreach (var assetPath in importedAssets)
            {
                foreach (var generator in generators)
                    generator.GenerateForFile(assetPath);
            }
        }

        [UnityEditor.MenuItem("Assets/EnumGenerator/RegenerateAll")]
        private static void RegenerateAll()
        {
            foreach (var generator in FindGenerators())
                generator.GenerateAll();
        }

        private static IEnumerable<Generator> FindGenerators()
        {
            foreach (var generator in UnityEditor.AssetDatabase.
               FindAssets($"t:{nameof(Generator)}").
               Select(g => UnityEditor.AssetDatabase.GUIDToAssetPath(g)).
               Select(p => UnityEditor.AssetDatabase.LoadAssetAtPath<Generator>(p)))
            {
                if (generator != null)
                    yield return generator;
            }
        }
    }
}
#endif
