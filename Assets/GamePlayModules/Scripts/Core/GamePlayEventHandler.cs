using Kultie.GamePlayModule.Delegates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kultie.GamePlayModule
{
    public abstract class GamePlayEventHandler
    {
        protected Dictionary<string, GamePlayEventCallback> funcDict = new Dictionary<string, GamePlayEventCallback>();
        public void RegisterListener(string evtKey, GamePlayEventCallback func)
        {
            if (!funcDict.ContainsKey(evtKey))
            {
                funcDict[evtKey] = null;
            }
            funcDict[evtKey] += func;
        }
        public void RemoveListener(string evtKey, GamePlayEventCallback func)
        {
            if (!funcDict.ContainsKey(evtKey))
            {
                funcDict[evtKey] = null;
            }
            funcDict[evtKey] -= func;
        }

        public abstract void Dispatch(string evtKey, object context);
    }

    public class GamePlayEventHandler<T> : GamePlayEventHandler where T : IModSubject
    {
        T subject;
        public GamePlayEventHandler(T subject)
        {
            this.subject = subject;
        }

        public void SetSubject(T subject)
        {
            this.subject = subject;
        }

        public override void Dispatch(string evtKey, object context)
        {
            if (funcDict.TryGetValue(evtKey, out var func))
            {
                func?.Invoke(subject, context);
            }
        }
    }
}