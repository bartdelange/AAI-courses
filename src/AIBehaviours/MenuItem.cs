using System;

namespace AIBehaviours
{
    public struct MenuItem
    {
        public readonly string Name;
        public readonly Type Value;

        public MenuItem(string name, Type value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
