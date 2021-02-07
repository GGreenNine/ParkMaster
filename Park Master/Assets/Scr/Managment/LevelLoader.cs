using System.Collections;
using System.Collections.Generic;
using Scr.Mechanics;
using Scr.Mechanics.Bezier;
using Scr.Mechanics.Car;
using UnityEngine;
using Zenject;

public interface ILevelLoader
{
    void LoadLevel(int level);
    void LoadCurrentLevel();
    void LoadLevel(LevelInfo levelInfo);
}

public class LevelLoader : IInitializable, ILevelLoader
{
    private PlayerState _playerState;
    private IFactory<CarType, CarController> carFactory;
    private IFactory<InGameBonusType, InGameTriggeredBonusBase> bonusFactory;


    public LevelLoader(PlayerState playerState, IFactory<CarType, CarController> carFactory, IFactory<InGameBonusType, InGameTriggeredBonusBase> bonusFactory)
    {
        _playerState = playerState;
        this.carFactory = carFactory;
        this.bonusFactory = bonusFactory;
    }

    public void Initialize()
    {
        LoadCurrentLevel();
    }

    public void LoadLevel(int level)
    {
        var info = _playerState.LevelInfos[level];
        LoadLevel(info);
    }

    public void LoadCurrentLevel()
    {
        LoadLevel(_playerState.CurrentLevelInfo);
    }

    public void LoadLevel(LevelInfo levelInfo)
    {
        foreach (var car in levelInfo.Cars)
        {
            LoadCar(car);
        }

        foreach (var bonusInfo in levelInfo.InGameBonusList)
        {
            LoadBonus(bonusInfo);
        }
    }

    private void LoadCar(CarInfo carInfo)
    {
       var car = carFactory.Create(carInfo.CarType).transform;
       car.position = carInfo.position;
       car.rotation = carInfo.rotation;
    }

    private void LoadBonus(InGameBonusInfo inGameBonusInfo)
    {
        var bonus = bonusFactory.Create(inGameBonusInfo.InGameBonusType).transform;
        bonus.position = inGameBonusInfo.position;
        bonus.rotation = inGameBonusInfo.rotation;
    }
}
