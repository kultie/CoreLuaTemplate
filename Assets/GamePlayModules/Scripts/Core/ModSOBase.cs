using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kultie.GamePlayModule
{
    public abstract class ModSOBase : ScriptableObject, IGameMod
    {
        [SerializeField]
        string Key;
        public string key => key;

        public void Init()
        {
            OnInit();
        }
        protected abstract void OnInit();
        public abstract void Dispose(GamePlayEventHandler eventHandler);
        public abstract void RegisterEvent(GamePlayEventHandler eventHandler, params string[] events);

        public void UnRegisterEvent(GamePlayEventHandler eventHandler, params string[] events)
        {
            
        }
    }
}