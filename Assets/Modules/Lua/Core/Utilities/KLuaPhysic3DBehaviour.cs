using System;
using UnityEngine;
namespace Kultie.Lua
{
    public class KLuaPhysic3DBehaviour : KLuaBehaviourBase
    {
        [ScriptTemplate("LuaBehaviour", "Assets/Modules/Lua/Core/Templates/lua_behaviour_template_physic3D.txt")]
        public LuaScriptFile scriptFile;
        public override LuaScriptFile script => scriptFile;

        protected override void OnInit()
        {
            onTriggerEnter = metatable.Get<Action<MonoBehaviour, Collider>>("OnTriggerEnter");
            onTriggerStay = metatable.Get<Action<MonoBehaviour, Collider>>("OnTriggerStay");
            onTriggerExit = metatable.Get<Action<MonoBehaviour, Collider>>("OnTriggerExit");

            onCollisionEnter = metatable.Get<Action<MonoBehaviour, Collision>>("OnCollisionEnter");
            onCollisionStay = metatable.Get<Action<MonoBehaviour, Collision>>("OnCollisionStay");
            onCollisionExit = metatable.Get<Action<MonoBehaviour, Collision>>("OnCollisionExit");
        }

        protected override void OnDispose()
        {
            onTriggerEnter = null;
            onTriggerStay = null;
            onTriggerExit = null;

            onCollisionEnter = null;
            onCollisionStay = null;
            onCollisionExit = null;
        }


        protected Action<MonoBehaviour, Collider> onTriggerEnter;
        protected Action<MonoBehaviour, Collider> onTriggerStay;
        protected Action<MonoBehaviour, Collider> onTriggerExit;

        protected Action<MonoBehaviour, Collision> onCollisionEnter;
        protected Action<MonoBehaviour, Collision> onCollisionStay;
        protected Action<MonoBehaviour, Collision> onCollisionExit;

        private void OnTriggerEnter(Collider collision)
        {
            onTriggerEnter?.Invoke(this, collision);
        }

        private void OnTriggerExit(Collider collision)
        {
            onTriggerStay?.Invoke(this, collision);
        }

        private void OnTriggerStay(Collider collision)
        {
            onTriggerExit?.Invoke(this, collision);
        }

        private void OnCollisionEnter(Collision collision)
        {
            onCollisionEnter?.Invoke(this, collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            onCollisionStay?.Invoke(this, collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            onCollisionExit?.Invoke(this, collision);
        }
    }
}
