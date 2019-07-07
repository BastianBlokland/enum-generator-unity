﻿using System;
using System.IO;
using Microsoft.Extensions.Logging;

using EnumGenerator.Core.Utilities;
using EnumGenerator.Core.Exporter;
using EnumGenerator.Core.Mapping;
using EnumGenerator.Core.Definition;
using EnumGenerator.Core.Mapping.Exceptions;

namespace EnumGenerator.Editor
{
    /// <summary>
    /// Public api into the enum-generator.
    /// </summary>
    public static class GeneratorApi
    {
        /// <summary>
        /// Generate enum file.
        /// </summary>
        /// <param name="options">Configuration options</param>
        /// <param name="logContext">Unity object to use as context for logs</param>
        public static void GenerateEnumToFile(Options options, UnityEngine.Object logContext)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            // Create logger.
            var logger = new UnityLogger(options.VerboseLogging, logContext);
            if (string.IsNullOrEmpty(options.InputText))
            {
                logger.LogCritical($"Failed to generate: Input json file missing");
                return;
            }

            // Generate.
            GenerateEnumToFile(
                options.InputText,
                options.OutputPath,
                options.CollectionJPath,
                options.EntryNameJPath,
                options.EntryValueJPath,
                options.EntryCommentJPath,
                options.EnumComment,
                options.EnumNamespace,
                options.IndentMode,
                options.IndentSize,
                options.NewlineMode,
                options.StorageType,
                options.CurlyBracketMode,
                logger);
        }

        /// <summary>
        /// Generate enum file.
        /// </summary>
        /// <param name="inputJsonText">Input json text to generate the enum from</param>
        /// <param name="outputPath">Path where to save the generated enum to</param>
        /// <param name="collectionJPath">JPath to the collection in the input file</param>
        /// <param name="entryNameJPath">
        /// JPath to the name field in an entry in the input file</param>
        /// <param name="entryValueJPath">
        /// Optional JPath to the value field in an entry in the input file.
        /// </param>
        /// <param name="entryCommentJPath">
        /// Optional JPath to the comment field in an entry in the input file.
        /// </param>
        /// <param name="enumComment">
        /// Optional comment to add to the generated enum.
        /// </param>
        /// <param name="enumNamespace">
        /// Optional namespace to add the generated enum to.
        /// </param>
        /// <param name="indentMode">Mode to use when indenting text</param>
        /// <param name="indentSize">When indenting with spaces this controls how many</param>
        /// <param name="newlineMode">Mode to use when adding newlines to text</param>
        /// <param name="storageType">Storage type for the exported enum</param>
        /// <param name="curlyBracketMode">Mode to use when writing curly-brackets</param>
        /// <param name="logger">Optional logger for diagnostic output</param>
        public static void GenerateEnumToFile(
            string inputJsonText,
            string outputPath,
            string collectionJPath,
            string entryNameJPath,
            string entryValueJPath,
            string entryCommentJPath,
            string enumComment,
            string enumNamespace,
            CodeBuilder.IndentMode indentMode,
            int indentSize,
            CodeBuilder.NewlineMode newlineMode,
            StorageType storageType,
            CurlyBracketMode curlyBracketMode,
            ILogger logger = null)
        {
            // Generate enum name.
            var enumName = GetEnumName(outputPath, logger);
            if (enumName == null)
                return;

            // Create mapping context.
            var context = Context.Create(
                collectionJPath,
                entryNameJPath,
                entryValueJPath,
                entryCommentJPath,
                logger);

            // Map enum.
            EnumDefinition enumDefinition = null;
            try
            {
                enumDefinition = context.MapEnum(inputJsonText, enumName, enumComment);
            }
            catch (JsonParsingFailureException)
            {
                logger?.LogCritical("Failed to parse input file: invalid json");
                return;
            }
            catch (MappingFailureException e)
            {
                logger?.LogCritical($"Failed to map enum: {e.InnerException.Message}");
                return;
            }

            // Export to c#.
            string csharp = null;
            try
            {
                csharp = enumDefinition.Export(
                    enumNamespace,
                    indentMode,
                    indentSize,
                    newlineMode,
                    storageType,
                    curlyBracketMode);
            }
            catch (Exception e)
            {
                logger?.LogCritical($"Failed to generate c#: {e.Message}");
                return;
            }

            // Write the file.
            try
            {
                var fullPath = Path.Combine(UnityEngine.Application.dataPath, outputPath);
                var outputDir = Path.GetDirectoryName(fullPath);
                if (!Directory.Exists(outputDir))
                {
                    logger?.LogDebug($"Creating output directory: '{outputDir}'");
                    Directory.CreateDirectory(outputDir);
                }

                File.WriteAllText(fullPath, csharp);
                logger?.LogInformation($"Saved enum: '{outputPath}'");
            }
            catch (Exception e)
            {
                logger?.LogCritical($"Failed to save enum: {e.Message}");
            }
        }

        private static string GetEnumName(string outputPath, ILogger logger = null)
        {
            // Determine file-name.
            string fileName = Path.GetFileNameWithoutExtension(outputPath);
            if (string.IsNullOrEmpty(fileName))
            {
                logger?.LogCritical($"Unable to get file-name from path: '{outputPath}'");
                return null;
            }

            // Strip .g from the filename.
            if (fileName.EndsWith(".g", StringComparison.OrdinalIgnoreCase))
                fileName = fileName.Substring(0, fileName.Length - 2);

            // Convert file-name into valid identifier.
            if (IdentifierCreator.TryCreateIdentifier(fileName, out var nameId))
            {
                logger?.LogDebug($"Generated enum-name: '{nameId}' from file-name: '{fileName}'");
                return nameId;
            }

            logger?.LogCritical($"Unable to create valid identifier from file-name: '{fileName}'");
            return null;
        }
    }
}
