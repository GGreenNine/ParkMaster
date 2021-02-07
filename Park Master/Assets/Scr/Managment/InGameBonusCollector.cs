using System;
using System.Collections;
using System.Collections.Generic;
using Scr.Mechanics;
using UniRx;
using UnityEngine;

public interface IInGameBonusCollector
{
    IObservable<int> CoinsCollected { get; }
    IObservable<int> KeysCollected { get; }
    void Collect(InGameBonusType bonusType);
}

public class InGameBonusCollector : IDisposable, IInGameBonusCollector
{
    private Dictionary<InGameBonusType, ReactiveProperty<int>> _inGameBonusesDictionary =
        new Dictionary<InGameBonusType, ReactiveProperty<int>>();

    public IObservable<int> CoinsCollected => _coinsCollected;
    public IObservable<int> KeysCollected => _keysCollected;

    private readonly ReactiveProperty<int> _coinsCollected = new ReactiveProperty<int>();
    private readonly ReactiveProperty<int> _keysCollected = new ReactiveProperty<int>();

    public InGameBonusCollector()
    {
        _inGameBonusesDictionary.Add(InGameBonusType.Coin, _coinsCollected);
        _inGameBonusesDictionary.Add(InGameBonusType.Key, _keysCollected);
    }

    public void Collect(InGameBonusType bonusType)
    {
        _inGameBonusesDictionary[bonusType].Value += 1;
    }

    public void Dispose()
    {
        _coinsCollected?.Dispose();
        _keysCollected?.Dispose();
    }
}
