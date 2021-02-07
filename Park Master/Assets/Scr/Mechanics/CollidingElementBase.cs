using Scr.Extensions;
using UnityEngine;

namespace Scr.Mechanics
{
    [RequireComponent(typeof(Collider))]
    public abstract class CollidingElementBase : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        private void OnTriggerEnter(Collider other)
        {
            if (_layerMask.Contains(other.gameObject.layer))
            {
                Collide();
            }
        }

        protected abstract void Collide();
    }
}