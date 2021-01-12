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
        bool IsPlaying { get; }
    }

    public class ObjectPathMover : InGameTweener, IPathMover
    {
        private int currentWaypoint = 0;
        
        public void Move(Vector3[] path, float speed, Transform transform, ObjectMoveStrategy objectMoveStrategy, ObjectRotateStrategy objectRotateStrategy)
        {
            Observable.EveryUpdate().Subscribe(l =>
            {
                if (currentWaypoint < path.Length)
                {
                    objectMoveStrategy.Move(transform, path[currentWaypoint], speed);
                    objectRotateStrategy.Rotate(transform, path[currentWaypoint], speed);
                    
                    if (transform.position.AlmostEqual(path[currentWaypoint], 0.01f))
                    {
                        currentWaypoint++;
                    }
                }
            }); // todo addto
        }

        public void StopMoving()
        {
            Stop();
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
            var rotation = current.rotation;
            var tunedTargetRotation = new Vector3(rotation.x, current.rotation.y, rotation.z);
            // current.rotation = Quaternion.Lerp(current.rotation,
            //         new Quaternion(tunedTargetRotation.x, tunedTargetRotation.y, tunedTargetRotation.z,
            //                 current.rotation.w), 0.5f);
            var targetRotation = 
            
            current.rotation = new Quaternion(tunedTargetRotation.x, tunedTargetRotation.y, tunedTargetRotation.z,
                    current.rotation.w);
        }
    }
}