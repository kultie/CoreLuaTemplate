using UnityEngine;
using XLua;

namespace Kultie.Lua
{
    public class LuaEnvironment : MonoBehaviour
    {
        static LuaEnvironment instance;
        static LuaEnvironment Instance
        {
            get
            {
                if (instance == null)
                {
                    var go = new GameObject("LuaEnvironment");
                    instance = go.AddComponent<LuaEnvironment>();
                    DontDestroyOnLoad(go);
                }

                return instance;
            }

        }
        private LuaEnv luaEnv;
        public static LuaEnv LuaEnv
        {
            get
            {
                if (Instance.luaEnv == null)
                {
                    Instance.luaEnv = new LuaEnv();
                }

                return Instance.luaEnv;
            }
        }

        private void Update()
        {
            try
            {
                Instance.luaEnv.Tick();
            }
            catch
            {
                Debug.LogError("Something wrong with lua Env");
            }
        }

        public void OnDestroy()
        {
            if (Instance.luaEnv == null)
            {
                Instance.luaEnv.Dispose();
                Instance.luaEnv = null;
            }
        }

        public static void Dispose()
        {
            if (Instance.luaEnv == null)
            {
                Instance.luaEnv.Dispose();
                Instance.luaEnv = null;
            }
        }
    }
}