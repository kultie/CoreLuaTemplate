using System;
using UnityEngine;
using UnityEngine.Events;

namespace Kultie.Lua
{

    public class KLuaPhysic2DBehaviour : KLuaBehaviourBase
    {
        [ScriptTemplate("LuaBehaviour", "Assets/Modules/Lua/Core/Templates/lua_behaviour_template_physic2D.txt")]
        public LuaScriptFile scriptFile;
        public override LuaScriptFile script => scriptFile;

        protected Action<MonoBehaviour, Collider2D> onTriggerEnter2D;
        protected Action<MonoBehaviour, Collider2D> onTriggerStay2D;
        protected Action<MonoBehaviour, Collider2D> onTriggerExit2D;

        protected Action<MonoBehaviour, Collision2D> onCollisionEnter2D;
        protected Action<MonoBehaviour, Collision2D> onCollisionStay2D;
        protected Action<MonoBehaviour, Collision2D> onCollisionExit2D;

        protected override void OnInit()
        {
            onTriggerEnter2D = metatable.Get<Action<MonoBehaviour, Collider2D>>("OnTriggerEnter2D");
            onTriggerStay2D = metatable.Get<Action<MonoBehaviour, Collider2D>>("OnTriggerStay2D");
            onTriggerExit2D = metatable.Get<Action<MonoBehaviour, Collider2D>>("OnTriggerExit2D");

            onCollisionEnter2D = metatable.Get<Action<MonoBehaviour, Collision2D>>("OnCollisionEnter2D");
            onCollisionStay2D = metatable.Get<Action<MonoBehaviour, Collision2D>>("OnCollisionStay2D");
            onCollisionExit2D = metatable.Get<Action<MonoBehaviour, Collision2D>>("OnCollisionExit2D");
        }

        protected override void OnDispose()
        {
            onTriggerEnter2D = null;
            onTriggerExit2D = null;
            onTriggerStay2D = null;

            onCollisionEnter2D = null;
            onCollisionStay2D = null;
            onCollisionExit2D = null;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            onTriggerEnter2D?.Invoke(this, collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            onTriggerStay2D?.Invoke(this, collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            onTriggerExit2D?.Invoke(this, collision);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            onCollisionEnter2D?.Invoke(this, collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            onCollisionStay2D?.Invoke(this, collision);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            onCollisionExit2D?.Invoke(this, collision);
        }

    }
}