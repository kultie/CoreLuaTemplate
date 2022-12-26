using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

#if UNITY_EDITOR
using UnityEditor;
using System.Linq;
#endif

namespace Kultie.Lua
{
    public abstract class KLuaScriptableObject : ScriptableObject
    {   
        public abstract LuaScriptFile script { get; }
        public LuaVariables variables;
        protected virtual string templatePathDirectory => "Assets/Kultie/xLua/Templates";

        public virtual LuaTable CreateLuaObject()
        {
            var luaEnv = LuaEnvironment.LuaEnv;
            string scriptText = script.text;
            var scriptEnv = luaEnv.NewTable();

            LuaTable meta = luaEnv.NewTable();
            meta.Set("__index", luaEnv.Global);
            scriptEnv.SetMetaTable(meta);
            meta.Dispose();

            object[] result = luaEnv.DoString(scriptText, string.Format("{0}({1})", "LuaBehaviour", this.name), scriptEnv);
            if (result.Length != 1 || !(result[0] is LuaTable))
            {
                Debug.LogError("Object is not a Lua Table");
                return null;
            }
            var obj = result[0] as LuaTable;
            if (variables != null && variables.Variables != null)
            {
                foreach (var variable in variables.Variables)
                {
                    var name = variable.Name.Trim();
                    if (string.IsNullOrEmpty(name))
                        continue;

                    obj.Set(name, variable.GetValue());
                }
            }
            return obj;
        }
    }
}