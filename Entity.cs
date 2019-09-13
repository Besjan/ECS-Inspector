namespace Cuku.ECS
{
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using UnityEngine;
    using Entitas;
    using System;
    using Entitas.Unity;

    [HideMonoScript]
    public class Entity : SerializedMonoBehaviour
    {
        #region Properties
        [Space]
        public List<IComponent> components = new List<IComponent>();

        [HideInInspector]
        public IEntity entity;
        #endregion

        #region Events
        [HideInInspector]
        public EntityEvent OnEntityCreated;

        [HideInInspector]
        public EntityEvent OnEntityDestroyed;
        #endregion

        [HideInInspector]
        [Serializable]
        public class EntityEvent : UnityEngine.Events.UnityEvent<IEntity> { }


        void OnEnable()
        {
            Type[] componentTypes = new Type[0];

            // Create entity
            entity = Contexts.sharedInstance.game.CreateEntity();
            componentTypes = GameComponentsLookup.componentTypes;
#if UNITY_EDITOR && !ENTITAS_DISABLE_VISUAL_DEBUGGING
            gameObject.Link(entity);
#endif

            // Add components
            foreach (var property in components)
            {
                var index = Array.IndexOf(componentTypes, property.GetType());
                if (index >= 0 && !entity.HasComponent(index)) entity.AddComponent(index, property);
            }

            OnEntityCreated.Invoke(entity);
        }

        void OnDisable()
        {
            OnEntityDestroyed.Invoke(entity);

#if UNITY_EDITOR && !ENTITAS_DISABLE_VISUAL_DEBUGGING
            var link = gameObject.GetEntityLink();

            if (!link) return;
            link.Unlink();
            Destroy(link);
#endif
            entity.Destroy();
        }
    }
}
