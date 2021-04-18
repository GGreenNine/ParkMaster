using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Managment
{
    public enum GameState
    {
        PathRememberingState,
        PathExecutionState,
        CarCrashState,
        LevelSuccess,
        LevelFailed
    }

    public interface IGameStateHolder
    {
        IReadOnlyReactiveProperty<GameState> CurrentGameState { get; }
    }

    public class GameStateHolder : IInitializable, IDisposable, IGameStateHolder
    {
        public IReadOnlyReactiveProperty<GameState> CurrentGameState => _currentGameState;
        private readonly ReactiveProperty<GameState> _currentGameState = new ReactiveProperty<GameState>(GameState.PathRememberingState);

        private readonly CompositeDisposable gameStateDisposable = new CompositeDisposable();

        private readonly IInGameBonusCollector _inGameBonusCollector;
        private readonly IInGamePathCollector _inGamePathCollector;
        private readonly IInGameCarMovesCollector _inGameCarMovesCollector;

        public GameStateHolder(IInGameBonusCollector inGameBonusCollector, IInGamePathCollector inGamePathCollector, IInGameCarMovesCollector inGameCarMovesCollector)
        {
            _inGameBonusCollector = inGameBonusCollector;
            _inGamePathCollector = inGamePathCollector;
            _inGameCarMovesCollector = inGameCarMovesCollector;
        }

        public void Initialize()
        {
            SubscribeOnExecutors();
        }

        private void SubscribeOnExecutors()
        {
            _inGameBonusCollector.Triggered.Skip(1).Subscribe(BonusCollectedStateHandler).AddTo(gameStateDisposable);
            _inGamePathCollector.Triggered.Skip(1).Subscribe(PathCollectedStateHandler).AddTo(gameStateDisposable);
            _inGameCarMovesCollector.Triggered.Skip(1).Subscribe(CarMovesCollectedStateHandler).AddTo(gameStateDisposable);
        }

        private void CarMovesCollectedStateHandler(bool succeed)
        {
            if (succeed && _currentGameState.Value != GameState.LevelSuccess)
            {
                _currentGameState.Value = GameState.LevelFailed;
            }
        }

        private void BonusCollectedStateHandler(bool succeed)
        {
            if (_currentGameState.Value == GameState.PathExecutionState)
            {
                _currentGameState.Value = succeed ? GameState.LevelSuccess : GameState.LevelFailed;
            }
        }
        
        private void PathCollectedStateHandler(bool succeed)
        {
            if (_currentGameState.Value == GameState.PathRememberingState || _currentGameState.Value == GameState.PathExecutionState)
            {
                _currentGameState.Value = succeed ? GameState.PathExecutionState : GameState.CarCrashState;
            }
        }

        public void Dispose()
        {
            _currentGameState?.Dispose();
            gameStateDisposable?.Dispose();
        }
    }
}