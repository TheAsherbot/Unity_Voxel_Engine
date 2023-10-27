namespace TheAshBot
{
    public static class TimeTickSystem
    {


        /// <summary>
        /// is the delages used for all the on tick events
        /// </summary>
        /// <param name="tick">this is the number of ticks that have happened</param>
        public delegate void OnTickEventArgs(int tick);

        /// <summary>
        /// is fired every tick.
        /// </summary>
        public static event OnTickEventArgs OnTick;
        /// <summary>
        /// is fired every 5 ticks.
        /// </summary>
        public static event OnTickEventArgs OnTick_5;
        /// <summary>
        /// is fired every 10 ticks.
        /// </summary>
        public static event OnTickEventArgs OnTick_10;
        /// <summary>
        /// is fired every 50 ticks.
        /// </summary>
        public static event OnTickEventArgs OnTick_50;
        /// <summary>
        /// is fired every 100 ticks.
        /// </summary>
        public static event OnTickEventArgs OnTick_100;


        /// <summary>
        /// is the number of ticks that will happen per second. even if you were to change this constant in run time; you would not change how fast the ticks go becouse it is caculated once at the bigining.
        /// </summary>
        private const float TICKS_PER_SECOND = 5f;



        private static int tick;
        private static bool isTicking;


        /// <summary>
        /// will test to see if it is counting ticks; if it is not then it start counting ticks
        /// </summary>
        public static void CreateIfNeeded()
        {
            // if it is already ticking then stop the function right here.
            if (isTicking) return;

            // if it is not yet ticking then it will make a timer loop that will make the tick go up by one. 
            FunctionTimer.Create(1 / TICKS_PER_SECOND, 0, (FunctionTimer functionTimer) =>
            {
                isTicking = true;
                tick++;
                OnTick?.Invoke(tick);

                if ((tick % 5) == 0)
                {
                    OnTick_5?.Invoke(tick);

                    if ((tick % 10) == 0)
                    {
                        OnTick_10?.Invoke(tick);

                        if ((tick % 50) == 0)
                        {
                            OnTick_50?.Invoke(tick);

                            if ((tick % 100) == 0)
                            {
                                OnTick_100?.Invoke(tick);
                            }
                        }
                    }
                }
            });
        }

        /// <summary>
        /// will get the ticks. if it is not yet counting the ticks then it will start to.
        /// </summary>
        /// <returns>the number if ticks</returns>
        public static int GetTick()
        {
            return tick;
        }


    }
}
