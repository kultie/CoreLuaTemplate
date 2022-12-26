using UnityEngine;
namespace Kultie.Lua
{
    [DefaultExecutionOrder(-999)]
    public class ScriptLoader : MonoBehaviour
    {
        [SerializeField]
        ScriptFileDictionary scripts;
        private void Awake()
        {
            var luaEnv = LuaEnvironment.LuaEnv;
            luaEnv.AddLoader(MyLoader);
        }

        private byte[] MyLoader(ref string filepath)
        {
            TextAsset _out = null;
            if (scripts.TryGetValue(filepath, out _out))
            {
                return _out.bytes;
            }
            return null;
        }
    }

    [System.Serializable]
    public class ScriptFileDictionary : SerializableDictionary<string, TextAsset>
    {

    }
}