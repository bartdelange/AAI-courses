using System;
using AICore.Enum;

namespace AICore
{
    public static class Config
    {
        public static bool Debug = Environment.GetEnvironmentVariable("DEBUG") == "1";
        
        // Game properties
        public const float BallMass = 1f;
        public const float BallMaxSpeed = 25f;
        public const float BallFriction = -0.75f;
        public const int BallRadius = 10;
    }
}