using System;
using System.Collections;
using System.Collections.Generic;
using Scr.Mechanics;
using UniRx;
using UnityEngine;

public interface IInGameCollector<T>
{
    void Collect(T item);
}

public interface IInGameCollector
{
    void Collect();
}

public interface IInGamePathCollector : IInGameCollector, GameStateExecutor
{
    void CarCrash();
}

public interface IInGameCarMovesCollector : IInGameCollector, GameStateExecutor{}

public interface IInGameBonusCollector : IInGameCollector<InGameBonusType>, GameStateExecutor
{
    IObservable<int> CoinsCollected { get; }
    IObservable<int> KeysCollected { get; }
}

public interface GameStateExecutor //todo naming
{
    IReadOnlyReactiveProperty<bool> Triggered { get; }
}

public class InGameCarMovesCollector : IInGameCarMovesCollector
{
    public IReadOnlyReactiveProperty<bool> Triggered => _trigger;
    private readonly ReactiveProperty<bool> _trigger = new ReactiveProperty<bool>(false);

    private int MovedCarsCount;
    private readonly PlayerState _playerState;

    public InGameCarMovesCollector(PlayerState playerState)
    {
        _playerState = playerState;
    }
    
    public void Collect()
    {
        MovedCarsCount++;

        if (MovedCarsCount >= _playerState.CurrentLevelInfo.Cars.Count)
        {
            _trigger.Value = true;
        }
    }
}

public class InGamePathCollector :  IInGamePathCollector
{
    public IReadOnlyReactiveProperty<bool> Triggered => _trigger;
    private readonly ReactiveProperty<bool> _trigger = new ReactiveProperty<bool>(false);

    private int _pathCreated;
    private readonly PlayerState _playerState;
    
    public InGamePathCollector(PlayerState playerState)
    {
        _playerState = playerState;
    }

    public void CarCrash() // maybe move to another collector
    {
        _trigger.Value = false;
    }
    
    public void Collect()
    {
        _pathCreated++;
        if (_pathCreated >= _playerState.CurrentLevelInfo.Cars.Count)
        {
            _trigger.Value = true;
        }
    }
}

public class InGameBonusCollector : IDisposable, IInGameBonusCollector
{
    public IReadOnlyReactiveProperty<bool> Triggered => _trigger;
    private readonly ReactiveProperty<bool> _trigger = new ReactiveProperty<bool>(false);

    private CompositeDisposable _compositeDisposable = new CompositeDisposable();

    private readonly Dictionary<InGameBonusType, ReactiveProperty<int>> _inGameBonusesDictionary =
        new Dictionary<InGameBonusType, ReactiveProperty<int>>();

    public IObservable<int> CoinsCollected => _coinsCollected;
    public IObservable<int> KeysCollected => _keysCollected;


    private readonly ReactiveProperty<int> _coinsCollected = new ReactiveProperty<int>();
    private readonly ReactiveProperty<int> _keysCollected = new ReactiveProperty<int>();

    public InGameBonusCollector(PlayerState playerState)
    {
        _inGameBonusesDictionary.Add(InGameBonusType.Coin, _coinsCollected);
        _inGameBonusesDictionary.Add(InGameBonusType.Key, _keysCollected);
        _coinsCollected.AddTo(_compositeDisposable);
        _keysCollected.AddTo(_compositeDisposable);
        _trigger.AddTo(_compositeDisposable);
        
        _keysCollected.Skip(1).Subscribe(i =>
        {
            _trigger.Value = playerState.CurrentLevelInfo.GetBonusesCount(InGameBonusType.Key) >= i;
        }).AddTo(_compositeDisposable);
    }

    public void Collect(InGameBonusType bonusType)
    {
        _inGameBonusesDictionary[bonusType].Value += 1;
    }

    public void Dispose()
    {
        _compositeDisposable?.Dispose();
    }

}
