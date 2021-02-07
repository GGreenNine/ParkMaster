using System;
using System.Collections;
using System.Collections.Generic;
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
        
        [SerializeField]
        protected InGameBonusType inGameBonusType;

        protected IInGameBonusCollector BonusCollector;

        [Inject]
        private void SetDependencies(IInGameBonusCollector bonusCollector)
        {
            BonusCollector = bonusCollector;
        }
            
        protected override void Collide()
        {
            BonusCollector.Collect(inGameBonusType);
        }
    }
}