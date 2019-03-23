using System;
using System.Drawing;

namespace AIBehaviours
{
    public struct MenuItem
    {
        public readonly string Name;
        public readonly Size Size;
        public readonly Type Value;

        public MenuItem(string name, Size size, Type value)
        {
            Name = name;
            Size = size;
            Value = value;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
