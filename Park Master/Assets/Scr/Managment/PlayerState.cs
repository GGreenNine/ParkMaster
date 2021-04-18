using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PlayerState
{
    public int CurrentLevel = 1;

    public List<LevelInfo> LevelInfos => _levelInfos;

    private List<LevelInfo> _levelInfos;
    private readonly LevelLoadParser _levelLoadParser;

    public PlayerState(LevelLoadParser levelLoadParser)
    {
        _levelLoadParser = levelLoadParser;
    }
    
    public LevelInfo CurrentLevelInfo
    {
        get
        {
            if (_levelInfos == null)
            {
                _levelInfos = _levelLoadParser.Load();
            }
            if (_levelInfos.Any(info => info.level == CurrentLevel))
            {
                return _levelInfos.First(info => info.level == CurrentLevel);
            }

            throw new KeyNotFoundException($"Can't find level {CurrentLevel} in levels");
        }
    }

    public void MutateLevelInfos(IEnumerable<LevelInfo> levelInfos)
    {
        _levelInfos.Clear();
        _levelInfos.AddRange(levelInfos);
    }


}
