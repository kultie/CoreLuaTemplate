using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XLua;

namespace Kultie.GamePlayModule.EditorUtilities
{
    public static class LuaGamePlayModuleExporter
    {
        [LuaCallCSharp]
        public static List<Type> _l1
        {
            get
            {
                return (from type in Assembly.Load("Assembly-CSharp").GetTypes()
                        where type.Namespace == "Kultie.GamePlayModule.Delegates"
                        select type).ToList();
            }
        }
    }
}