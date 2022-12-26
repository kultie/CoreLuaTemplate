using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kultie.Lua
{
    [CreateAssetMenu(fileName = "GenericSO", menuName = "Kultie/Lua/GenericSO", order = 1)]
    public class KLuaGenericScriptableObject : KLuaScriptableObject
    {
        [ScriptTemplate("LuaBehaviour", "Assets/Modules/Lua/Core/Templates/lua_scriptable_object_template.txt")]
        public LuaScriptFile scriptFile;
        public override LuaScriptFile script => scriptFile;
    }
}