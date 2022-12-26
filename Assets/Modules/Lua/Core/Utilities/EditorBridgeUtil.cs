using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Kultie.Lua
{
    [XLua.BlackList]
    public static class EditorBridgeUtil
    {
        public static TextAsset CreateScript(string scriptType, string templateDirectory, Dictionary<string, string> mapData)
        {
            System.Reflection.Assembly editorAssembly = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.FullName.StartsWith("Assembly-CSharp-Editor,")); // ',' included to ignore  Assembly-CSharp-Editor-FirstPass
            System.Type utilityType = editorAssembly.GetTypes().FirstOrDefault(t => t.FullName.Contains("Utilities"));
            object[] args = new object[] { scriptType, templateDirectory, mapData };
            var obj = utilityType.GetMethod("CreateLuaScript", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Invoke(obj: null, parameters: args);
            return obj as TextAsset;
        }

        public static TextAsset CreateScriptFromTemplate(string scriptType, string templateFile, Dictionary<string, string> mapData)
        {
            System.Reflection.Assembly editorAssembly = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.FullName.StartsWith("Assembly-CSharp-Editor,")); // ',' included to ignore  Assembly-CSharp-Editor-FirstPass
            System.Type utilityType = editorAssembly.GetTypes().FirstOrDefault(t => t.FullName.Contains("Utilities"));
            object[] args = new object[] { scriptType, templateFile, mapData };
            var obj = utilityType.GetMethod("CreateLuaScriptFromTemplate", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Invoke(obj: null, parameters: args);
            return obj as TextAsset;
        }
    }
}