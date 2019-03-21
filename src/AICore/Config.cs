using System;
using AICore.Enum;

namespace AICore
{
    public static class Config
    {
        public static readonly bool Debug = Environment.GetEnvironmentVariable(EnvironmentVariableName.Debug) == "1";
    }
}