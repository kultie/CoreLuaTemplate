using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Kultie.GamePlayModule
{
    public abstract class ModContainer<T> : SerializedMonoBehaviour where T : IModSubject
    {
        [SerializeField]
        List<IGameMod> modules;
        T subject;
        GamePlayEventHandler<T> eventHandler;

        private void Start()
        {
            subject = GetComponent<T>();
            eventHandler = new GamePlayEventHandler<T>(subject);
            foreach (var m in modules)
            {
                m.Init();
                m.RegisterEvent(eventHandler, subject.EventKeys);
            }
        }

        private void OnDestroy()
        {
            foreach (var m in modules)
            {
                m.UnRegisterEvent(eventHandler, subject.EventKeys);
                m.Dispose(eventHandler);
            }
        }

        public void AddMod(IGameMod module)
        {
            modules.Add(module);
            module.Init();
            module.RegisterEvent(eventHandler, subject.EventKeys);
        }

        public void RemoveMod(string key)
        {
            var mod = modules.SingleOrDefault(m => m.key == key);
            if (mod != null)
            {
                mod.UnRegisterEvent(eventHandler, subject.EventKeys);
                mod.Dispose(eventHandler);
                modules.Remove(mod);
            }

        }
        public void RemoveMod(IGameMod module)
        {
            module.UnRegisterEvent(eventHandler, subject.EventKeys);
            module.Dispose(eventHandler);
            modules.Remove(module);
        }

        public void DispatchEvent(string key, object context)
        {
            eventHandler.Dispatch(key, context);
        }
    }
}