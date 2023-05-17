using Common.GameManager.Scripts;
using Common.Scripts.ManagerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

namespace Assets.Common.Scripts.ManagerService
{
    public class ResstartSession : MonoBehaviour
    {
        private SessionService _session;
        [SerializeField] private Animation _mainMenuAnimator;
        [SerializeField] private AnimationClip _fadeOutAnimation;
        [SerializeField] private AnimationClip _fadeInAnimation;
        [Inject]
        void Construct(ISessionService service, IGameManager manager)
        {
            service.record = 0;
            service.money = 0;
            manager.OnGameStateChanged += HandleGameStateChanged;
            _session = (SessionService)service;
        }
        private void HandleGameStateChanged(GameState currentState, GameState previousState)
        {
            if (currentState == GameState.STARTRUNNING && (previousState == GameState.RESULT || GameState.PAUSED == previousState)) 
            {
                _session.EndRun();
                _session.RestartGame(); 
            }
        }

        
    }
}
