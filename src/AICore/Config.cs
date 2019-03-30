using System;
using AICore.Enum;

namespace AICore
{
    public static class Config
    {
        public static bool Debug = Environment.GetEnvironmentVariable("DEBUG") == "1";
        
        // Game properties
        public const float BallMass = 5f;
        public const float BallMaxSpeed = 15f;
        public const float BallFriction = -0.75f;
        
        /// <summary>
        /// When kickig ball as a player this will reduce the time necessary to kick
        /// </summary>
        public const float BallBuildupRatio = 3;
        public const int BallRadius = 8;
    }
}