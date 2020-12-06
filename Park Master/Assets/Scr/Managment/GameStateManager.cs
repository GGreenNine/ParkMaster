using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Asteroids
{
    public class GameStateManager 
    {
        public enum GameState
        {
            Pause,
            Playing,
            End,
            Quit
        }

        bool requestTitleScreen = true;
        public static GameState CurrentGameState;

        public void SetGameState(GameState state)
        {
            switch (state)
            {
                case GameState.Quit:
                    break;
                case GameState.Pause:
                    break;
                case GameState.Playing:
                    break;
                case GameState.End:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }

            CurrentGameState = state;
        }

        private void Start()
        {
            // StartCoroutine(Background_Game_Workflow());
        }

        /// <summary>
        /// Works in backgroud, changing the game Behaviour
        /// for current game state
        /// </summary>
        /// <returns></returns>
        IEnumerator Background_Game_Workflow()
        {
            while (true)
            {
                yield return null;

            }
        }

        IEnumerator ShowingPreGameTitle()
        {
            yield return null;

        }

        IEnumerator StartLevel()
        {
            yield return null;

        }

        IEnumerator PlayLevel()
        {
            yield return null;

        }

        IEnumerator EndLevel()
        {
            yield return null;
        }


    }
}