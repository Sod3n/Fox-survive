using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class StateMachine : ITickable, IStatesContainer, IInitializable
    {
        private DiContainer _container;

        public StateMachine(DiContainer container)
        {
            _container = container;
        }
        public void Initialize()
        {
            EnterIn(ResolveIState<GameWorldState>());
        }

        private IState _currentState;

        private ITickableState _tickableState;
        private IState _nextState;
        public void Tick()
        {
            _tickableState = _currentState as ITickableState;
            _tickableState?.Tick();

            _nextState = _currentState.GetNextState();

            if (_nextState is not null)
                EnterIn(_nextState);
        }

        private void EnterIn(IState state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public T ResolveIState<T>() where T : class, IState
        { 
            return _container.TryResolve<T>();
        }
    }
}
