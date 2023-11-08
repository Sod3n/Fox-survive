using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GameController
{
    public class PauseHandler : IInitializable
    {
        private readonly Settings _settings;

        public PauseHandler(Settings settings)
        {
            _settings = settings;
        }

        public void Initialize()
        {
            //lets think that this handler will be live
            //longer than player so dont need unsubscribe
            //_settings.Player.OnDie += PauseGame; 
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
        }

        [Serializable]
        public class Settings
        {
            public Player.Facade Player;
        }
    }
}
