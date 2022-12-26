using System;

namespace Kultie
{
    public class ScriptTemplateAttribute : Attribute
    {
        public string templatePath;
        public string dicKeys;
        public string scriptType;
        public ScriptTemplateAttribute(string scriptType, string templateFolder)
        {
            templatePath = templateFolder;
            this.scriptType = scriptType;
        }
    }
}