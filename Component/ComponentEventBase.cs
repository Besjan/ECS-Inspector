namespace BeeX.ECS
{
    using Sirenix.OdinInspector;
    using Entitas;
    using System;

    [HideMonoScript]
    public abstract class ComponentEventBase : SerializedMonoBehaviour
    {
        #region Properties
        [ValidateInput("IsValidEntity", "Entity reference is missing!")]
        public Entity entity;

        public IComponent Component;
        #endregion


        protected virtual void OnEnable()
        {
            if (!entity) entity = GetComponent<Entity>();
            if (!entity) return;

            entity.OnEntityCreated.AddListener(OnEntityCreated);
            entity.OnEntityDestroyed.AddListener(OnEntityDestroyed);

            if (entity.entity != null)
                OnEntityCreated(entity.entity);
        }

        protected virtual void OnDisable()
        {
            if (entity == null || entity.entity == null) return;

            OnEntityDestroyed(entity.entity);

            entity.OnEntityCreated.RemoveListener(OnEntityCreated);
            entity.OnEntityDestroyed.RemoveListener(OnEntityDestroyed);
        }

        abstract public void OnEntityCreated(IEntity e);

        abstract public void OnEntityDestroyed(IEntity e);


        #region Validation
        bool IsValidEntity(Entity entityReference)
        {
            return entityReference != null || GetComponent<Entity>();
        }

        public Type ComponentValueType()
        {
            if (Component == null || Component.GetType().GetField("value") == null)
                return null;
            
            return Component.GetType().GetField("value").FieldType;
        }
        #endregion
    }
}
