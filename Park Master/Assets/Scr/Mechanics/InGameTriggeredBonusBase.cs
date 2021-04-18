using System;
using System.Collections;
using System.Collections.Generic;
using Managment;
using Scr.Mechanics.Bezier;
using Scr.Mechanics.Car;
using UnityEngine;
using Zenject;

namespace Scr.Mechanics
{
    public enum InGameBonusType
    {
        Coin, 
        Key
    }
    public class InGameTriggeredBonusBase : CollidingElementBase
    {
        public InGameBonusType InGameBonusType => inGameBonusType;
        private CarType _canCollectedBy = CarType.Any;
        protected IGameStateHolder _gameStateHolder;

        [SerializeField]
        protected InGameBonusType inGameBonusType;

        protected IInGameBonusCollector BonusCollector;

        public void SetOptions(InGameBonusInfo info) //todo think about that
        {
            _canCollectedBy = info.canCollectedBy;
            transform.position = info.position;
            transform.rotation = info.rotation;
        }

        [Inject]
        private void SetDependencies(IInGameBonusCollector bonusCollector, IGameStateHolder gameStateHolder)
        {
            _gameStateHolder = gameStateHolder;
            BonusCollector = bonusCollector;
        }
            
        protected override void OnTriggered(Collider other)
        {
            var carController = other.GetComponent<CarController>();
            if (carController != null && (carController.CarType == _canCollectedBy || _canCollectedBy == CarType.Any) && _gameStateHolder.CurrentGameState.Value != GameState.PathRememberingState)
            {
                BonusCollector.Collect(inGameBonusType);
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}