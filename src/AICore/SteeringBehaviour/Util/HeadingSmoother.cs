using System.Linq;
using System.Numerics;
using AICore.Entity.Contracts;

namespace AICore.SteeringBehaviour.Util
{
    public class HeadingSmoother
    {
        private readonly IMovingEntity _movingEntity;
        private readonly Vector2[] _headingHistory;

        private int _currentSample = 0;

        public HeadingSmoother(IMovingEntity movingEntity, int sampleSize)
        {
            _movingEntity = movingEntity;
            _headingHistory = new Vector2[sampleSize];
        }

        public void Update()
        {
            // Save heading for later use
            _headingHistory[_currentSample] = _movingEntity.Heading;

            // Set next sample index
            _currentSample = (_currentSample + 1) % _headingHistory.Length;

            var headingSum = _headingHistory.Aggregate(
                Vector2.Zero,
                (current, previousHeading) => current + previousHeading
            );

            _movingEntity.SmoothHeading = headingSum / _headingHistory.Length;
        }
    }
}