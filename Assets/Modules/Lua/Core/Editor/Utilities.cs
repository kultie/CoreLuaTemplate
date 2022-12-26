using Kultie.EditorUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Kultie.Lua.Editor
{
    public static class Utilities
    {
        public static TextAsset CreateLuaScript(string scriptType, string templateDirectory, Dictionary<string, string> mapData)
        {
            try
            {
                string behaviourName = EditorInputDialog.Show("Creating " + scriptType, "Input your " + scriptType, "");
                string templatePath = EditorUtility.OpenFilePanel("Template to create", templateDirectory, "txt");
                string relativePath = "Assets" + templatePath.Substring(Application.dataPath.Length);
                TextAsset templateFile = AssetDatabase.LoadAssetAtPath<TextAsset>(relativePath);
                string templateContent = templateFile.text;
                var content = templateContent;
                content = content.Replace("[BEHAVIOR_NAME]", behaviourName);
                if (mapData != null)
                {
                    foreach (var kv in mapData)
                    {
                        content = content.Replace(kv.Key, kv.Value);
                    }
                }

                string path = EditorUtility.OpenFolderPanel("Target Directory", "Assets", "");
                string newFilePath = Path.Combine(path, behaviourName + ".lua.txt");
                return CreateTextFile(newFilePath, content);
            }
            catch
            {
                return null;
            }
        }

        public static TextAsset CreateLuaScriptFromTemplate(string scriptType, string templateFile, Dictionary<string, string> mapData)
        {
            try
            {
                string behaviourName = EditorInputDialog.Show("Creating " + scriptType, "Input your " + scriptType, "");
                try
                {
                    string templateContent = AssetDatabase.LoadAssetAtPath<TextAsset>(templateFile).text;
                    var content = templateContent;
                    content = content.Replace("[BEHAVIOR_NAME]", behaviourName);
                    if (mapData != null)
                    {
                        foreach (var kv in mapData)
                        {
                            content = content.Replace(kv.Key, kv.Value);
                        }
                    }

                    string path = EditorUtility.OpenFolderPanel("Target Directory", "Assets", "");
                    string newFilePath = Path.Combine(path, behaviourName + ".lua.txt");
                    return CreateTextFile(newFilePath, content);
                }
                catch (Exception e) {
                    EditorUtility.DisplayDialog("Error create lua script from template", "Please check template path", "OK");
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public static TextAsset CreateTextFile(string path, string content)
        {
            File.WriteAllText(path, content);
            AssetDatabase.Refresh();
            string relativePath = "Assets" + path.Substring(Application.dataPath.Length);
            return AssetDatabase.LoadAssetAtPath<TextAsset>(relativePath);
        }
    }
}