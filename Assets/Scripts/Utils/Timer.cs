using System;

namespace Cyberball.Utils
{

    /// <summary>
    /// Wait a certain time then calls an Event to notify that the time has passed
    /// </summary>
    public class Timer
    {
        public float RemainingSeconds { get; private set; }

        public Timer(float duration)
        {
            RemainingSeconds = duration;
        }

        public event Action ontimerEnd;

        public void Tick(float deltaTime)
        {
            if (RemainingSeconds == 0) return;
            RemainingSeconds -= deltaTime;

            CheckForTimeEnd();
        }
        private void CheckForTimeEnd()
        {
            if (RemainingSeconds > 0) return;

            RemainingSeconds = 0;

            ontimerEnd?.Invoke();
        }
    }
}