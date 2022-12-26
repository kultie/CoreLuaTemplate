using Kultie.Lua;
using XLua;
using UnityEngine;
using System;
using System.Collections.Generic;
using Kultie.GamePlayModule.Delegates;

namespace Kultie.GamePlayModule
{
    [CSharpCallLua]
    public delegate void LuaEventCallback(LuaTable subject, object csharpSubject, object context);
    [CreateAssetMenu(fileName = "LuaModuleSO", menuName = "Kultie/GamePlay/LuaModuleSO", order = 1)]
    public class LuaModSO : KLuaScriptableObject, IGameMod
    {
        [ScriptTemplate("ModuleSO", "Assets/GamePlayModules/Scripts/LuaCore/module_template.lua.txt")]
        [SerializeField]
        LuaScriptFile scriptFile;
        [SerializeField]
        string Key;
        LuaTable scriptInstance;

        Dictionary<string, GamePlayEventCallback> funcDic = new Dictionary<string, GamePlayEventCallback>();
        public string key => Key;
        public override LuaScriptFile script => scriptFile;

        public void Init()
        {
            scriptInstance = CreateLuaObject();
            scriptInstance.Get<Action<LuaTable>>("Init")?.Invoke(scriptInstance);
        }
        public void Dispose(GamePlayEventHandler eventHandler)
        {
            scriptInstance.Get<Action<LuaTable>>("Dispose")?.Invoke(scriptInstance);
        }



        public void RegisterEvent(GamePlayEventHandler eventHandler, params string[] events)
        {
            foreach (var k in events)
            {
                eventHandler.RegisterListener(k, GetCallbackFromLuaObject(k));
            }
        }

        public void UnRegisterEvent(GamePlayEventHandler eventHandler, params string[] events)
        {
            foreach (var k in events)
            {
                eventHandler.RemoveListener(k, GetCallbackFromLuaObject(k));
            }
        }

        GamePlayEventCallback GetCallbackFromLuaObject(string evtKey)
        {
            if (funcDic.TryGetValue(evtKey, out var res))
            {
                return res;
            }

            var luaFunc = scriptInstance.Get<LuaEventCallback>(evtKey);
            if (luaFunc != null)
            {
                funcDic[evtKey] = (subject, context) =>
                {
                    luaFunc.Invoke(scriptInstance, subject, context);
                };
                return funcDic[evtKey];
            }
            return null;

        }
    }
}