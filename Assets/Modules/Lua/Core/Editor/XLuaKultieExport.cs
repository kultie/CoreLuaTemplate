using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XLua;
namespace Kultie.Lua
{   
    public static class XLuaKultieExport
    {
        [LuaCallCSharp]
        public static List<Type> _l1
        {
            get
            {
                return (from type in Assembly.Load("Assembly-CSharp").GetTypes()
                        where type.Namespace == "Kultie.Utilities"
                        select type).ToList();
            }
        }

        [LuaCallCSharp]
        public static List<Type> _l2
        {
            get
            {
                return (from type in Assembly.Load("Assembly-CSharp").GetTypes()
                        where type.Namespace == "Kultie.Lua"
                        select type).ToList();
            }
        }
    }
}