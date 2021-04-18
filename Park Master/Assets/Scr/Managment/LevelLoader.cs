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
    private readonly PlayerState _playerState;
    private readonly IFactory<CarModel, CarController> carFactory;
    private readonly IFactory<InGameBonusType, InGameTriggeredBonusBase> bonusFactory;


    public LevelLoader(PlayerState playerState, IFactory<CarModel, CarController> carFactory, IFactory<InGameBonusType, InGameTriggeredBonusBase> bonusFactory)
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
       carFactory.Create(new CarModel(carInfo.CarType, 15, carInfo.rotation, carInfo.position));
    }

    private void LoadBonus(InGameBonusInfo inGameBonusInfo)
    {
        var bonus = bonusFactory.Create(inGameBonusInfo.InGameBonusType);
        bonus.SetOptions(inGameBonusInfo);
    }
}
