using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kultie.GamePlayModule
{
    public interface IGameMod
    {
        string key { get; }
        void Init();
        void RegisterEvent(GamePlayEventHandler eventHandler, params string[] events);
        void UnRegisterEvent(GamePlayEventHandler eventHandler, params string[] events);
        void Dispose(GamePlayEventHandler eventHandler);
    }
}