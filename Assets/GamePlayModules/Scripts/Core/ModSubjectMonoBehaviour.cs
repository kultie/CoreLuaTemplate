using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kultie.GamePlayModule
{
    public abstract class ModSubjectMonoBehaviour<T> : MonoBehaviour where T : IModSubject
    {
        public abstract string[] EventKeys { get; }

        protected ModContainer<T> ModContainer;
        private void Awake()
        {
            ModContainer = GetComponent<ModContainer<T>>();
            if (ModContainer == null)
            {
                ModContainer = gameObject.AddComponent<ModContainer<T>>();
            }
        }
    }
}