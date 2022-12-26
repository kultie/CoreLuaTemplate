using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using System;

namespace Kultie.Lua.Editor
{
    public class LuaScriptFileDrawer : OdinValueDrawer<LuaScriptFile>
    {
        private InspectorProperty scriptFile;

        protected override void Initialize()
        {
            scriptFile = Property.Children["scriptFile"];
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            scriptFile.ValueEntry.WeakSmartValue = SirenixEditorFields.UnityObjectField((UnityEngine.Object)scriptFile.ValueEntry.WeakSmartValue, typeof(TextAsset), false);
            if (scriptFile.ValueEntry.ValueState == PropertyValueState.NullReference)
            {
                var attributes = Property.Attributes;
                foreach (var att in attributes)
                {
                    if (att is ScriptTemplateAttribute)
                    {
                        ScriptTemplateAttribute attribute = att as ScriptTemplateAttribute;
                        if (GUILayout.Button("Create from template"))
                        {
                            scriptFile.ValueEntry.WeakSmartValue = Utilities.CreateLuaScriptFromTemplate(attribute.scriptType, attribute.templatePath, null);
                        }
                    }
                }
            }
        }
    }
}