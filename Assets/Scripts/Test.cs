using Kultie.Lua;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kultie.Test
{
    public class Test : MonoBehaviour
    {
        [ScriptTemplate("Test","Assets/Modules/Lua/Core/Templates")]
        [SerializeField]
        LuaScriptFile textScipt;

        [Button]
        void PrintValue() {
            Debug.Log(textScipt.text);
        }
    }
}