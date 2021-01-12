using UnityEngine;

namespace Scr.Input
{
    public interface IRaycastingSystem
    {
        T TryToGetGameObjectOfType<T>(int layer) where T : Component;
        GameObject TryToGetGameObject(LayerMask layer);
        Vector3? TryToGetPoint(LayerMask layer);
    }
    
    public class RaycastingSystem : IRaycastingSystem
    {
        private readonly Camera _raycastCamera;
        private float rayMaxRange = 1000;
        private readonly IInputMaster _inputMaster;


        public RaycastingSystem(Camera raycastCamera, IInputMaster inputMaster)
        {
            _raycastCamera = raycastCamera;
            _inputMaster = inputMaster;
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
        
        public GameObject TryToGetGameObject(LayerMask layer)
        {
            Debug.Log("trying to get gameobject");
            var ray = _raycastCamera.ScreenPointToRay(_inputMaster.MousePosition);
            
            if (Physics.Raycast(ray, out var hit, rayMaxRange, layer))
            {
                var objectHit = hit.transform.gameObject;
                Debug.Log($"Gameobject finded {objectHit}");

                return objectHit;
            }

            return null;
        }

        public Vector3? TryToGetPoint(LayerMask layer)
        {
            var ray = _raycastCamera.ScreenPointToRay(_inputMaster.MousePosition);

            if (Physics.Raycast(ray, out var hit, rayMaxRange, layer))
            {
                return hit.point;
            }

            return null;
        }
        


    }
}