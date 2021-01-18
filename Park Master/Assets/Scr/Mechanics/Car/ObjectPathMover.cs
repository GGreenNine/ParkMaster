using System;
using DG.Tweening;
using Scr.Extensions;
using UniRx;
using UnityEngine;

namespace Scr.Mechanics.Car
{
    public interface IPathMover : IDisposable
    {
        void Move(Vector3[] path, float speed, Transform transform, ObjectMoveStrategy objectMoveStrategy, ObjectRotateStrategy objectRotateStrategy);
        void StopMoving();
    }

    public class ObjectPathMover : IPathMover
    {
        private readonly SerialDisposable moveDisposable = new SerialDisposable();
        
        public void Move(Vector3[] path, float speed, Transform transform, ObjectMoveStrategy objectMoveStrategy, ObjectRotateStrategy objectRotateStrategy)
        {
            int currentWaypoint = 0;
            moveDisposable.Disposable = Observable.EveryUpdate().Subscribe(l =>
            {
                if (currentWaypoint < path.Length)
                {
                    objectRotateStrategy.Rotate(transform, path[currentWaypoint], speed);
                    objectMoveStrategy.Move(transform, path[currentWaypoint], speed);
                    
                    if (transform.position.AlmostEqual(path[currentWaypoint], 0.01f))
                    {
                        currentWaypoint++;
                    }
                }
            }); 
        }

        public void StopMoving()
        {
        }

        public void Dispose()
        {
            moveDisposable.Dispose();
        }
    }

    public abstract class ObjectMoveStrategy
    {
        public abstract void Move(Transform current, Vector3 target, float args);
    }

    public class ObjectMoveByXZStrategy : ObjectMoveStrategy
    {
        public override void Move(Transform current, Vector3 target, float args)
        {
            var position = current.position;
            var tunedTargetPosition = new Vector3(target.x, position.y, target.z);
            position = Vector3.MoveTowards(position, tunedTargetPosition,
                    args);
            current.position = position;
        }
    }

    public abstract class ObjectRotateStrategy
    {
        public abstract void Rotate(Transform current, Vector3 target, float args);
    }

    public class ObjectRotateByXZLerp : ObjectRotateStrategy
    {
        public override void Rotate(Transform current, Vector3 target, float args)
        {
            var targetRotation = Quaternion.LookRotation(target - current.position).normalized;
            Debug.Log(targetRotation);
            current.rotation = Quaternion.Slerp(current.rotation, targetRotation, Time.deltaTime * args);
        }
    }
}