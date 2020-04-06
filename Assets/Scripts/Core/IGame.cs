using System;

namespace Core
{
    public interface IGame
    {
        event Action EndGameEvent;
        event Action<int> ScoreChangedEvent;

        void NewGame();
    }
}