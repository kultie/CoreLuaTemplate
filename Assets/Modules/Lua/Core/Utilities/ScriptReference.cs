using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kultie.Lua
{
    public enum ScriptReferenceType
    {
        TextAsset,
        Filename
    }

    [System.Serializable]
    public class ScriptReference : ISerializationCallbackReceiver
    {
#if UNITY_EDITOR
        [SerializeField]
        private Object cachedAsset;
#endif
        [SerializeField]
        protected TextAsset text;

        [SerializeField]
        protected string filename;

        [SerializeField]
        protected ScriptReferenceType type = ScriptReferenceType.TextAsset;

        public virtual ScriptReferenceType Type
        {
            get { return this.type; }
        }

        public virtual TextAsset Text
        {
            get { return this.text; }
        }

        public virtual string Filename
        {
            get { return this.filename; }
        }

        public void OnAfterDeserialize()
        {
            Clear();
        }

        public void OnBeforeSerialize()
        {
            Clear();
        }

        public bool IsNull()
        {
            switch (type)
            {
                case ScriptReferenceType.TextAsset:
                    return text == null;

                case ScriptReferenceType.Filename:
                    return string.IsNullOrEmpty(filename);
            }
            return false;
        }

        public void SetTextAsset(TextAsset asset) {
            text = asset;            
        }

        protected virtual void Clear()
        {
#if !UNITY_EDITOR
            switch (type)
            {
                case ScriptReferenceType.TextAsset:
                    this.filename = null;
                    break;
                case ScriptReferenceType.Filename:
                    this.text = null;
                    break;
            }
#endif
        }
    }
}