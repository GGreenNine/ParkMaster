using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Managment;
using Scr.Mechanics.Bezier;
using UniRx;
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
        private IGameStateHolder _gameStateHolder;
        private IInGamePathCollector _inGamePathCollector;
        private IInGameCarMovesCollector _carMovesCollector;
        
        [SerializeField] private Rigidbody _rigidbody;

        public CarType CarType => _carModel.CarType;

        private bool pathCollected = false;
        private bool movesCollected = false;
        private IEnumerable<Vector3> lastPath;

        [Inject]
        public void SetDependencies(IPathBuilder pathBuilder, IPathMover pathMover, CarModel carModel, IGameStateHolder gameStateHolder,
            IInGamePathCollector inGamePathCollector, IInGameCarMovesCollector carMovesCollector)
        {
            _gameStateHolder = gameStateHolder;
            _pathBuilder = pathBuilder;
            _pathMover = pathMover;
            _carModel = carModel;
            _inGamePathCollector = inGamePathCollector;
            _carMovesCollector = carMovesCollector;
        }

        protected override void OnSelected()
        {
            if (_gameStateHolder.CurrentGameState.Value == GameState.PathRememberingState)
            {
                _pathBuilder.StartBuilding(_carModel.CarType);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            ResetPosition();

            _gameStateHolder.CurrentGameState.Subscribe(OnStateChanged).AddTo(OnDestroyDisposable);
        }

        private void OnStateChanged(GameState state)
        {
            if (state == GameState.PathExecutionState)
            {
                Move(lastPath);
            }
        }

        private void ResetPosition()
        {
            transform.position = _carModel.InitialPos;
            transform.rotation = _carModel.InitialRotation;
            
            if (_pathBuilder is LineRendererPathBuilder rendererPathBuilder)
            {
                rendererPathBuilder.transform.localPosition = new Vector3(-0.02f, -2.53f, 0);
            }
        }

        protected override void OnDeselect()
        {
            base.OnDeselect();
            lastPath = _pathBuilder.StopBuilding(_carModel.CarType);

            if (!pathCollected)
            {
                _inGamePathCollector.Collect();
                pathCollected = true;
            }
            
            Move(lastPath);
        }

        private void Move(IEnumerable<Vector3> path)
        {
            _pathMover.Move(path.ToArray(), _carModel.CarSpeed, transform, new ObjectMoveByXZStrategy(), new ObjectRotateByXZLerp()).Subscribe(OnPathComplite).AddTo(OnDestroyDisposable);
        }

        private void OnPathComplite(Unit obj)
        {
            switch (_gameStateHolder.CurrentGameState.Value)
            {
                case GameState.PathRememberingState:
                    ResetPosition();
                    break;
                case GameState.PathExecutionState:
                    _carMovesCollector.Collect();
                    break;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _pathMover?.Dispose();
        }

        protected override void OnTriggered(Collider other)
        {
            Explode();
        }

        private void Explode()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.AddExplosionForce(5, transform.position, 5, 3); // todo move to constants / serialized fields
            _inGamePathCollector.CarCrash();
            _pathMover.StopMoving();
        }
    }
}