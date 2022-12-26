using System;
using UnityEngine;
using XLua;
namespace Kultie.Lua
{
    public abstract class KLuaBehaviourBase : MonoBehaviour
    {
        public abstract LuaScriptFile script { get; }
        public LuaVariables variables;

        protected LuaTable scriptEnv;
        protected LuaTable metatable;
        protected Action<MonoBehaviour> onAwake;
        protected Action<MonoBehaviour> onEnable;
        protected Action<MonoBehaviour> onDisable;
        protected Action<MonoBehaviour> onStart;
        protected Action<MonoBehaviour> onUpdate;
        protected Action<MonoBehaviour> onFixedUpdate;
        protected Action<MonoBehaviour> onLateUpdate;
        protected Action<MonoBehaviour> onDestroy;

        public virtual LuaTable GetMetatable()
        {
            return metatable;
        }

        public KLuaBehaviour GetLuaBehaviour(string luaScriptName)
        {
            var behaviours = GetComponents<KLuaBehaviour>();
            foreach (var b in behaviours)
            {
                if (b.script.name.Equals(luaScriptName + ".lua"))
                {
                    return b;
                }
            }
            Debug.LogError("No " + luaScriptName + " found in " + name);
            return null;
        }

        protected virtual void Initialize()
        {
            var luaEnv = LuaEnvironment.LuaEnv;
            scriptEnv = luaEnv.NewTable();

            LuaTable meta = luaEnv.NewTable();
            meta.Set("__index", luaEnv.Global);
            scriptEnv.SetMetaTable(meta);
            meta.Dispose();

            scriptEnv.Set("target", this);

            string scriptText = script.text;
            object[] result = luaEnv.DoString(scriptText, string.Format("{0}({1})", "LuaBehaviour", this.name), scriptEnv);

            if (result.Length != 1 || !(result[0] is LuaTable))
                throw new Exception("Object is not a Lua Table");

            metatable = (LuaTable)result[0];
            if (variables != null && variables.Variables != null)
            {
                foreach (var variable in variables.Variables)
                {
                    var name = variable.Name.Trim();
                    if (string.IsNullOrEmpty(name))
                        continue;

                    metatable.Set(name, variable.GetValue());
                }
            }

            onAwake = metatable.Get<Action<MonoBehaviour>>("Awake");
            onEnable = metatable.Get<Action<MonoBehaviour>>("Enable");
            onStart = metatable.Get<Action<MonoBehaviour>>("Start");
            onUpdate = metatable.Get<Action<MonoBehaviour>>("Update");
            onFixedUpdate = metatable.Get<Action<MonoBehaviour>>("FixedUpdate");
            onLateUpdate = metatable.Get<Action<MonoBehaviour>>("LateUpdate");
            onDisable = metatable.Get<Action<MonoBehaviour>>("Disable");
            onDestroy = metatable.Get<Action<MonoBehaviour>>("Destroy");
            OnInit();
        }

        protected virtual void Awake()
        {
            this.Initialize();
            onAwake?.Invoke(this);
        }

        protected virtual void OnEnable()
        {
            onEnable?.Invoke(this);
        }

        protected virtual void OnDisable()
        {
            onDisable?.Invoke(this);
        }

        protected virtual void Start()
        {
            onStart?.Invoke(this);

        }

        protected virtual void Update()
        {
            onUpdate?.Invoke(this);
        }

        protected virtual void FixedUpdate()
        {
            onFixedUpdate?.Invoke(this);
        }

        protected virtual void LateUpdate()
        {
            onLateUpdate?.Invoke(this);
        }

        protected virtual void OnDestroy()
        {
            onDestroy?.Invoke(this);
            onDestroy = null;
            onUpdate = null;
            onFixedUpdate = null;
            onLateUpdate = null;
            onStart = null;
            onEnable = null;
            onDisable = null;
            onAwake = null;

            OnDispose();
            if (metatable != null)
            {
                metatable.Dispose();
                metatable = null;
            }

            if (scriptEnv != null)
            {
                scriptEnv.Dispose();
                scriptEnv = null;
            }
        }

        protected abstract void OnInit();
        protected abstract void OnDispose();
    }
}