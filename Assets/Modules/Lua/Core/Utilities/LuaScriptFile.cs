using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kultie.Lua
{
    [Serializable]
    public class LuaScriptFile
    {   
        [SerializeField]
        TextAsset scriptFile;

        public string text => scriptFile == null ? string.Empty : scriptFile.text;
        public string name => scriptFile == null ? string.Empty : scriptFile.name;
    }
}