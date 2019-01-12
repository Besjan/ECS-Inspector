namespace Cuku.ECS
{
    using ByteSheep.Events;
    using Entitas;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class OnComponentReplaced : ComponentBase
    {
        #region Events
        [DrawWithUnity]
        [ShowIf("IsString")]
        public AdvancedStringEvent OnStringReplaced;

        [DrawWithUnity]
        [ShowIf("IsInt")]
        public AdvancedIntEvent OnIntReplaced;

        [DrawWithUnity]
        [ShowIf("IsFloat")]
        public AdvancedFloatEvent OnFloatReplaced;
        
        [DrawWithUnity]
        [ShowIf("IsVector2")]
        public AdvancedVector2Event OnVector2Replaced;

        [DrawWithUnity]
        [ShowIf("IsVector3")]
        public AdvancedVector3Event OnVector3Replaced;
        #endregion


        public override void OnEntityCreated(IEntity e)
        {
            e.OnComponentReplaced += OnEntityComponentReplaced;
        }

        public override void OnEntityDestroyed(IEntity e)
        {
            e.OnComponentReplaced -= OnEntityComponentReplaced;
        }

        private void OnEntityComponentReplaced(IEntity entity, int index, IComponent previousComponent, IComponent newComponent)
        {
            if (newComponent.GetType() != Component.GetType()) return;

            var value = newComponent.GetType().GetField("value").GetValue(newComponent);

            if (IsString()) OnStringReplaced.Invoke((string)value);
            else if (IsInt()) OnIntReplaced.Invoke((int)value);
            else if (IsFloat()) OnFloatReplaced.Invoke((float)value);
            else if (IsVector2()) OnVector2Replaced.Invoke((Vector2)value);
            else if (IsVector3()) OnVector3Replaced.Invoke((Vector3)value);
        }


        #region Validation
        bool IsString()
        {
            return ComponentValueType() == typeof(string);
        }

        bool IsInt()
        {
            return ComponentValueType() == typeof(int);
        }

        bool IsFloat()
        {
            return ComponentValueType() == typeof(float);
        }

        bool IsVector2()
        {
            return ComponentValueType() == typeof(Vector2);
        }

        bool IsVector3()
        {
            return ComponentValueType() == typeof(Vector3);
        }
        #endregion
    }
}
