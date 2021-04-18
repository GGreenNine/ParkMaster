using System;
using Scr.Extensions;
using Scr.Mechanics.Car;
using UnityEngine;

namespace Scr.Mechanics
{
    [RequireComponent(typeof(Collider))]
    public abstract class CollidingElementBase : GameObjectBase
    {
        [SerializeField] private LayerMask _layerMask;
        private void OnTriggerEnter(Collider other)
        {
            if (_layerMask.Contains(other.gameObject.layer))
            {
                OnTriggered(other);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (_layerMask.Contains(other.gameObject.layer))
            {
                OnCollisioned(other);
            }
        }

        protected virtual void OnTriggered(Collider other)
        {
        }

        protected virtual void OnCollisioned(Collision other)
        {
        }
    }
}