using UnityEngine;


namespace Kultie.Lua
{
    public class KLuaBehaviour : KLuaBehaviourBase
    {
        [ScriptTemplate("LuaBehaviour", "Assets/Modules/Lua/Core/Templates/lua_behaviour_template.txt")]
        public LuaScriptFile scriptFile;
        public override LuaScriptFile script => scriptFile;

        protected override void OnDispose()
        {

        }

        protected override void OnInit()
        {
           
        }
    }
}