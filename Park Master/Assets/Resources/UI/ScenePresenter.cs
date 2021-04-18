using System;
using Managment;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI
{
    public class ScenePresenter : UIElementBase
    {
        [SerializeField] private ScoreControl score;
        [SerializeField] private GameResultsPanelPresenter _gameResultsPanelPresenter;

        private const string CarCrashText = "Car crash";
        private const string LevelSucceed = "Level Succeed";
        private const string LevelFailed = "Level Failed";
        
        private IGameStateHolder _gameStateHolder;
        
        [Inject]
        private void SetDependencies(IGameStateHolder gameStateHolder)
        {
            _gameStateHolder = gameStateHolder;
        }
        
        private void Awake()
        {
            score.Initialize();
            _gameStateHolder.CurrentGameState.Skip(1).Subscribe(SubscribeOnStates).AddTo(OnDestroyDisposables);
        }

        private void SubscribeOnStates(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.CarCrashState:
                    _gameResultsPanelPresenter.Show(CarCrashText);
                    break;
                case GameState.LevelSuccess:
                    _gameResultsPanelPresenter.Show(LevelSucceed);
                    break;
                case GameState.LevelFailed:
                    _gameResultsPanelPresenter.Show(LevelFailed);
                    break;
            }
        }
    }
}

