using Scr.Mechanics;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class ScoreControl : UIElementBase
    {
        [SerializeField] private Text BonusesCollected;
        [SerializeField] private Text KeysCollected;

        private InGameBonusCollector _bonusCollector;
        private PlayerState _playerState;
        private LevelInfo _levelInfo;

        private readonly CompositeDisposable TextUpdateDisposable = new CompositeDisposable();
        
        [Inject]
        private void SetDependencies(InGameBonusCollector bonusCollector, PlayerState playerState)
        {
            _bonusCollector = bonusCollector;
            _playerState = playerState;
            TextUpdateDisposable.AddTo(OnDestroyDisposables);
        }

        public void Initialize()
        {
            TextUpdateDisposable.Clear();
            _levelInfo = _playerState.CurrentLevelInfo;
            _bonusCollector.CoinsCollected.Subscribe(OnCoinsCollected).AddTo(TextUpdateDisposable);
            _bonusCollector.KeysCollected.Subscribe(OnKeysCollected).AddTo(TextUpdateDisposable);
        }

        private void OnCoinsCollected(int count)
        {
            BonusesCollected.text = $"{count} / {_levelInfo.GetBonusesCount(InGameBonusType.Coin)}";
        }
        
        private void OnKeysCollected(int count)
        {
            KeysCollected.text = $"{count} / {_levelInfo.GetBonusesCount(InGameBonusType.Key)}";
        }
    }
}