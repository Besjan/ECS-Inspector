namespace BeeX.ECS
{
    using ByteSheep.Events;
    using Entitas;
    using Sirenix.OdinInspector;

    public class OnComponentAdded : ComponentBase
    {
        [DrawWithUnity]
        public AdvancedEvent OnAdded;


        public override void OnEntityCreated(IEntity e)
        {
            e.OnComponentAdded += OnEntityComponentAdded;
        }

        public override void OnEntityDestroyed(IEntity e)
        {
            e.OnComponentAdded -= OnEntityComponentAdded;
        }

        private void OnEntityComponentAdded(IEntity entity, int index, IComponent component)
        {
            if (component.GetType() == Component.GetType())
                OnAdded.Invoke();
        }
    }
}
