using System;

namespace AICore
{
    public static class Config
    {
        public static bool Debug = Environment.GetEnvironmentVariable("DEBUG") == "1";

        // Ball properties
        public const float BallMass = 5f;
        public const float BallMaxSpeed = 15f;
        public const float BallFriction = -0.75f;
        public const float BallBuildupRatio = 3;
        public const int BallRadius = 8;
        
        // Player properties
        public const float MaxSpeed = 25;
        public const float RestSpeed = 10f;
        
        // Timeout in ms
        public const double KickTimeout = 1000;
    }
}