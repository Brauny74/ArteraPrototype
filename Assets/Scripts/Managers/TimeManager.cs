using UnityEngine;

namespace TopDownShooter
{
    /// <summary>
    /// Simple Time Manager, that stores time dilation for movement and stuff like that.
    /// Can be used to create slow-mo effect, so far only used to create active pause (without pausing the application)
    /// That allows to create animated menus and UI.
    /// </summary>
    public class TimeManager : Singleton<TimeManager>
    {
        public bool IsPaused
        {
            get { return TimeDilation == 0.0f; }
            set
            {
                if (value)
                {
                    TimeDilation = 0.0f;
                }
                else
                {
                    TimeDilation = 1.0f;
                }
            }
        }

        public float TimeDilation
        { get; private set; }

        private void Start()
        {
            IsPaused = false;
        }
    }
}