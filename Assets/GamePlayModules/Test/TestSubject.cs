using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kultie.GamePlayModule.Test
{
    public class TestSubject : ModSubjectMonoBehaviour<TestSubject>, IModSubject
    {
        [SerializeField]
        LuaModSO luaMod;
        public string subjectValue;
        public override string[] EventKeys => new string[] {
            "Key_Press",
        };

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ModContainer.DispatchEvent("Key_Press", new TestContext()
                {
                    contextValue = subjectValue + "From context"
                });
            }
        }

        [Button]
        void AddMod()
        {
            ModContainer.AddMod(luaMod);
        }
        [Button]
        void RemoveMod()
        {
            ModContainer.RemoveMod(luaMod);
        }
        [Button]
        void RemoveMod(string key)
        {
            ModContainer.RemoveMod(key);
        }
    }

    public class TestContext
    {
        public string contextValue;
    }
}