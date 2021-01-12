using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Scr.Mechanics.Bezier;
using UnityEngine;
using Zenject;

namespace Scr.Mechanics.Car
{
    public class CarController : SelectableGameObject
    {
        private IPathBuilder _pathBuilder;
        private CarModel _carModel;
        private Sequence movingSequence;
        private IPathMover _pathMover;

        [Inject]
        public void SetDependencies(IPathBuilder pathBuilder, IPathMover pathMover, CarModel carModel)
        {
            _pathBuilder = pathBuilder;
            _pathMover = pathMover;
            _carModel = carModel;
        }

        protected override void OnSelected()
        {
            _pathBuilder.StartBuilding(_carModel.CarType);
        }

        protected override void Awake()
        {
            base.Awake();

            if (_pathBuilder is LineRendererPathBuilder rendererPathBuilder)
            {
                rendererPathBuilder.transform.localPosition = new Vector3(-0.02f, -2.53f, 0);
            }
        }

        protected override void OnDeselect()
        {
            base.OnDeselect();
            var path = _pathBuilder.StopBuilding(_carModel.CarType);
            Move(path);
        }

        private void Move(IEnumerable<Vector3> path)
        {
            _pathMover.Move(path.ToArray(), _carModel.CarSpeed, transform, new ObjectMoveByXZStrategy(), new ObjectRotateByXZLerp());
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _pathMover?.Dispose();
        }
    }
}