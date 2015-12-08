using System;

namespace Dacha.PropertyMappings
{

    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class PropertyLink : System.Attribute
    {
        public readonly Type ClassName;

        public PropertyLink(Type className)
        {
            ClassName = className;
        }
    }
}
