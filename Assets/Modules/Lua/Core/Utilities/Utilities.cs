using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Kultie.Lua
{
    public static class Utilities
    {
        public static GameObject SpawnObject(GameObject prefab, Vector3 position, Transform parent)
        {
            return GameObject.Instantiate(prefab, position, Quaternion.identity, parent);
        }

        public static KLuaBehaviour GetLuaBehaviour(this MonoBehaviour target, string luaScriptName)
        {
            var behaviours = target.GetComponents<KLuaBehaviour>();
            foreach (var b in behaviours)
            {
                if (b.script.name.Equals(luaScriptName + ".lua"))
                {
                    return b;
                }
            }
            Debug.LogError("No " + luaScriptName + " found in " + target.name);
            return null;
        }

        public static string CurrentPlatform()
        {
            return Application.platform.ToString();
        }

        public static T ToEnum<T>(this string value) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public static bool TryParseToEnum<T>(this string value, out T output) where T : struct
        {
            return Enum.TryParse(value, out output);
        }

    }
}