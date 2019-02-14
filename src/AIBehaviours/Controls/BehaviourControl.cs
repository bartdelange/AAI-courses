using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AIBehaviours.Entity;
using AIBehaviours.Behaviour;
using System.Linq;

namespace AIBehaviours
{
    public partial class BehaviourControl : UserControl
    {
        public readonly Type BehaviourType;

        private readonly World _world;
        private readonly ListBox _entityList;
        private readonly Random _random = new Random();

        public BehaviourControl(BehaviourItem behaviour, ListBox entityList, World world)
        {
            InitializeComponent();

            behaviourNameBox.Text = behaviour.Name;
            BehaviourType = behaviour.Type;

            _entityList = entityList;
            _world = world;
        }

        private MovingEntity GetRandomTarget(MovingEntity currentEntity)
        {
            Func<MovingEntity> getEntity = () => _world.Entities[_random.Next(0, _world.Entities.Count)];

            var randomTarget = getEntity();

            // Make sure we don't assign current entity as our target
            while (randomTarget == currentEntity)
                randomTarget = getEntity();

            return randomTarget;
        }

        public void UpdateEntities()
        {
            foreach(MovingEntity movingEntity in _entityList.SelectedItems)
            {
                // Remove instance of current steering behaviour
                movingEntity
                    .SteeringBehaviours
                    .RemoveAll(sb => sb.GetType() == BehaviourType);

                // Terminate current iteration when value is 0 (no need to apply the behaviour when it doesn't affect the agent)
                if (weightInput.Value == 0) return;

                var randomTarget = GetRandomTarget(movingEntity);

                var steeringBehaviour = (SteeringBehaviour)Activator.CreateInstance(
                    BehaviourType,
                    movingEntity,
                    randomTarget,
                    (double)weightInput.Value
                );

                movingEntity.SteeringBehaviours.Add(steeringBehaviour);
            }
        }

        #region event handlers

        private void WeightInput_ValueChanged(object sender, EventArgs e)
        {
            UpdateEntities();
        }

        #endregion
    }
}
