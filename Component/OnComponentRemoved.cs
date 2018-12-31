namespace BeeX.ECS
{
    using ByteSheep.Events;
    using Entitas;
    using Sirenix.OdinInspector;

    public class OnComponentRemoved : ComponentBase
    {
        [DrawWithUnity]
        public AdvancedEvent OnRemoved;


        public override void OnEntityCreated(IEntity e)
        {
            e.OnComponentRemoved += OnEntityComponentRemoved;
        }

        public override void OnEntityDestroyed(IEntity e)
        {
            e.OnComponentRemoved -= OnEntityComponentRemoved;
        }

        private void OnEntityComponentRemoved(IEntity entity, int index, IComponent component)
        {
            if (component.GetType() == Component.GetType())
                OnRemoved.Invoke();
        }
    }
}
