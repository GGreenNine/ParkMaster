using UnityEngine;

namespace Scr.Input
{
    public interface IRaycastingSystem
    {
        T TryToGetGameObjectOfType<T>(int layer) where T : Component;
        GameObject TryToGetGameObject(int layer);
        Vector3? TryToGetPoint(int layer);
    }
    
    public class RaycastingSystem : IRaycastingSystem
    {
        private readonly Camera _raycastCamera;
        private float rayMaxRange = 1000;

        public RaycastingSystem(Camera raycastCamera)
        {
            _raycastCamera = raycastCamera;
        }

        public T TryToGetGameObjectOfType<T>(int layer) where T : Component
        {
            int layerMask = 1 << layer;
            var ray = _raycastCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, rayMaxRange, layerMask))
            {
                var objectHit = hit.transform.gameObject;
                return objectHit.GetComponent<T>();
            }

            return null;
        }
        
        public GameObject TryToGetGameObject(int layer)
        {
            int layerMask = 1 << layer;
            var ray = _raycastCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, rayMaxRange, layerMask))
            {
                var objectHit = hit.transform.gameObject;
                return objectHit;
            }

            return null;
        }

        public Vector3? TryToGetPoint(int layer)
        {
            int layerMask = 1 << layer;
            
            var ray = _raycastCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, rayMaxRange, layerMask))
            {
                return hit.point;
            }

            return null;
        }
        
        public static bool LayerInLayerMask(int layer, LayerMask layerMask)
        {
            return ((1 << layer) & layerMask) != 0;
        }

        
    }
}