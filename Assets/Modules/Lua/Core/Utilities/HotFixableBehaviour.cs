using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

#if UNITY_EDITOR
using UnityEditor;
using System.Linq;
#endif

namespace Kultie.Lua
{
    [TypeInfoBox("Must have [XLua.Hotfix] attribute")]
    public abstract class HotFixableBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {       
        [SerializeField]
        HotFixTime hotFixTime;
        
        [SerializeField]
        TextAsset luaScripts;
        protected virtual string templatePathDirectory => "Assets/Kultie/xLua/Templates";

        private void Awake()
        {
            if (hotFixTime == HotFixTime.Awake)
            {
                ApplyFix();
            }

        }

        private void Start()
        {
            if (hotFixTime == HotFixTime.Start)
            {
                ApplyFix();
            }

        }

        private void OnEnable()
        {
            if (hotFixTime == HotFixTime.Enable)
            {
                ApplyFix();
            }

        }

        public void ApplyFix()
        {
            var luaEnv = LuaEnvironment.LuaEnv;
            luaEnv.DoString(luaScripts.text);
        }

        public void ApplyFix(string code)
        {
            var luaEnv = LuaEnvironment.LuaEnv;
            luaEnv.DoString(code);
        }

#if UNITY_EDITOR
        [BlackList]
        [ShowIf("@this.luaScripts == null")]
        [Button]
        public virtual void CreateScript()
        {

            var obj = EditorBridgeUtil.CreateScript("Hot Fix Behaviour", "Assets/Kultie/xLua/Templates", null);
            if (obj != null)
            {
                luaScripts = obj;
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssets();
            }
        }
#endif

        private enum HotFixTime { Awake, Start, Enable, Custom }
    }
}