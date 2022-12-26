using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kultie.GamePlayModule.Test
{
    [CreateAssetMenu(fileName = "LogModule", menuName = "Kultie/GamePlay/LogModuleSO", order = 1)]
    public class LogModule : ModSOBase
    {
        public override void Dispose(GamePlayEventHandler eventHandler)
        {
            eventHandler.RemoveListener("Key_Press", OnKeyPress);
        }

        public override void RegisterEvent(GamePlayEventHandler eventHandler, params string[] events)
        {
            eventHandler.RegisterListener("Key_Press", OnKeyPress);
        }


        protected override void OnInit()
        {

        }
        private void OnKeyPress(object subject, object context)
        {
            Debug.Log(((TestSubject)subject).subjectValue + ":" + ((TestContext)context).contextValue);
        }
    }
}