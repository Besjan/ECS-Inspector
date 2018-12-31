namespace BeeX.ECS
{
    using Entitas;
    using System;
    using UnityEngine;

    public class ReplaceComponent : ComponentBase
    {
        Type[] componentTypes = new Type[0];


        public override void OnEntityCreated(IEntity e)
        {
            componentTypes = GameComponentsLookup.componentTypes;
        }

        public override void OnEntityDestroyed(IEntity e) { }


        public void Add()
        {
            var index = Array.IndexOf(componentTypes, Component.GetType());
            if (index >= 0 && !entity.entity.HasComponent(index))
                entity.entity.AddComponent(index, Component);
        }

        public void Remove()
        {
            var index = Array.IndexOf(componentTypes, Component.GetType());
            if (index >= 0 && entity.entity.HasComponent(index))
                entity.entity.RemoveComponent(index);
        }

        #region Replace
        public void Replace(string value)
        {
            if (ComponentValueType() == typeof(string)) ReplaceValue(value);
        }

        public void Replace(int value)
        {
            if (ComponentValueType() == typeof(int)) ReplaceValue(value);
        }

        public void Replace(float value)
        {
            if (ComponentValueType() == typeof(float)) ReplaceValue(value);
        }

        public void Replace(Color value)
        {
            if (ComponentValueType() == typeof(Color)) ReplaceValue(value);
        }

        public void Replace(Vector2 value)
        {
            if (ComponentValueType() == typeof(Vector2)) ReplaceValue(value);
        }

        public void Replace(Vector3 value)
        {
            if (ComponentValueType() == typeof(Vector3)) ReplaceValue(value);
        }

        public void Replace(Quaternion value)
        {
            if (ComponentValueType() == typeof(Quaternion)) ReplaceValue(value);
        }

        public void Replace(Sprite value)
        {
            if (ComponentValueType() == typeof(Sprite)) ReplaceValue(value);
        }
        #endregion

        void ReplaceValue(object value)
        {
            var index = Array.IndexOf(componentTypes, Component.GetType());
            if (index < 0) return;
            Component.GetType().GetField("value").SetValue(Component, value);
            entity.entity.ReplaceComponent(index, Component);
        }
    }
}
