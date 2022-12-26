using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kultie.Lua
{
    public static class LuaScriptTemplateConst
    {
        public const string PHYSIC_2D_TEMPLATE = @"
-- 2D Physics Remove any method if not needed --

function M:OnTriggerEnter2D(other)
end

function M:OnTriggerStay2D(other)
end

function M:OnTriggerExit2D(other)
end

function M:OnCollisionEnter2D(other)
end

function M:OnCollisionStay2D(other)
end

function M:OnCollisionExit2D(other)
end

-- End of 2D Phyiscs --";

        public const string PHYSIC_TEMPLATE = @"
-- 3D Physics Remove any method if not needed --

function M:OnTriggerEnter(other)
end

function M:OnTriggerStay(other)
end

function M:OnTriggerExit(other)
end

function M:OnCollisionEnter(other)
end

function M:OnCollisionStay(other)
end

function M:OnCollisionExit(other)
end
-- End of 3D Physics --";
    }
}