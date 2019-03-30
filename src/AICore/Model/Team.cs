using System;
using System.Collections.Generic;
using AICore.Entity.Contracts;
using AICore.Entity.Static;

namespace AICore.Worlds
{
    public class Team
    {
        private Team _opponent;

        public readonly SoccerGoal Goal;
        public readonly List<IPlayer> Players;

        public Team Opponent
        {
            get
            {
                if (_opponent == null)
                    throw new ArgumentNullException("Opponent is not defined");

                return _opponent;
            }
            set => _opponent = value;
        }

        public Team(SoccerGoal soccerGoal, List<IPlayer> players)
        {
            Goal = soccerGoal;
            Players = players;
        }
    }
}