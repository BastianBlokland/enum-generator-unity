using System;
using UnityEngine;

using EnumGenerator.Core.Utilities;
using EnumGenerator.Core.Exporter;

namespace EnumGenerator.Editor
{
    /// <summary>
    /// Configuration options for the enum generator.
    /// </summary>
    [Serializable]
    public sealed class Options
    {
#pragma warning disable CS0649
        [Header("Mapping")]
        [Tooltip("Input json file to generate the enum from")]
        [SerializeField] private TextAsset jsonFile;

        [Tooltip("JPath to the collection in the input file")]
        [SerializeField] private string collectionJPath = "[*]";

        [Tooltip("JPath to the name field of a entry in the input file")]
        [SerializeField] private string entryNameJPath = "name";

        [Tooltip("(Optional) JPath to a value field of a entry in the input file")]
        [SerializeField] private string entryValueJPath = "id";

        [Tooltip("(Optional) JPath to a comment field of a entry in the input file")]
        [SerializeField] private string entryCommentJPath = "";

        [Header("Generation")]
        [Tooltip("(Optional) Namespace to add the generated enum to")]
        [SerializeField] private string enumNamespace = "";

        [Tooltip("(Optional) Comment to add to the generated enum")]
        [SerializeField] private string enumComment = "";

        [Tooltip("Indentation mode to use for the generated enum")]
        [SerializeField] private CodeBuilder.IndentMode indentMode = CodeBuilder.IndentMode.Spaces;

        [Tooltip("How many spaces to use when indenting with spaces")]
        [SerializeField] private int indentSize = 4;

        [Tooltip("Which newline mode to use for the generated enum")]
        [SerializeField] private CodeBuilder.NewlineMode newlineMode = CodeBuilder.NewlineMode.Unix;

        [Tooltip("Underlying storage type for the exported enum")]
        [SerializeField] private StorageType storageType = StorageType.Implicit;

        [Tooltip("Which curlybracket-mode to use")]
        [SerializeField] private CurlyBracketMode curlyBracketMode = CurlyBracketMode.NewLine;

        [Header("Output")]
        [Tooltip("Auto-generate the enum when the json file is changed")]
        [SerializeField] private bool autoGenerate = true;

        [Tooltip("Path to save the enum to relative to the Assets directory")]
        [SerializeField] private string outputPath = "Scripts/NewEnum.g.cs";

        [Header("Diagnostics")]
        [Tooltip("Should verbose diagnostic logging be enabled")]
        [SerializeField] private bool verboseLogging;
#pragma warning restore CS0649

        /// <summary>
        /// Input json text to generate the enum from.
        /// </summary>
        public string InputText => this.jsonFile == null ? null : this.jsonFile.text;

#if UNITY_EDITOR
        /// <summary>
        /// Path relative to the project folder where the input asset is stored.
        /// </summary>
        public string InputFilePath =>
            this.jsonFile == null ? null : UnityEditor.AssetDatabase.GetAssetPath(this.jsonFile);
#endif

        /// <summary>
        /// JPath to the collection in the input file.
        /// </summary>
        public string CollectionJPath => this.collectionJPath;

        /// <summary>
        /// JPath to the name field of a entry in the input file.
        /// </summary>
        public string EntryNameJPath => this.entryNameJPath;

        /// <summary>
        /// (Optional) JPath to a value field of a entry in the input file.
        /// </summary>
        public string EntryValueJPath => this.entryValueJPath;

        /// <summary>
        /// (Optional) JPath to a comment field of a entry in the input file.
        /// </summary>
        public string EntryCommentJPath => this.entryCommentJPath;

        /// <summary>
        /// (Optional) Namespace to add the generated enum to.
        /// </summary>
        public string EnumNamespace => this.enumNamespace;

        /// <summary>
        /// (Optional) Comment to add to the generated enum.
        /// </summary>
        public string EnumComment => this.enumComment;

        /// <summary>
        /// Indentation mode to use for the generated enum.
        /// </summary>
        public CodeBuilder.IndentMode IndentMode => this.indentMode;

        /// <summary>
        /// How many spaces to use when indenting with spaces.
        /// </summary>
        public int IndentSize => this.indentSize;

        /// <summary>
        /// Which newline mode to use for the generated enum.
        /// </summary>
        public CodeBuilder.NewlineMode NewlineMode => this.newlineMode;

        /// <summary>
        /// Underlying storage type for the exported enum.
        /// </summary>
        public StorageType StorageType => this.storageType;

        /// <summary>
        /// Which curlybracket-mode to use.
        /// </summary>
        public CurlyBracketMode CurlyBracketMode => this.curlyBracketMode;

        /// <summary>
        /// Auto-generate the enum when the json file is changed.
        /// </summary>
        public bool AutoGenerate => this.autoGenerate;

        /// <summary>
        /// Path to save the enum to relative to the Assets directory.
        /// </summary>
        public string OutputPath => this.outputPath;

        /// <summary>
        /// Should verbose diagnostic logging be enabled during the mapping.
        /// </summary>
        public bool VerboseLogging => this.verboseLogging;
    }
}
