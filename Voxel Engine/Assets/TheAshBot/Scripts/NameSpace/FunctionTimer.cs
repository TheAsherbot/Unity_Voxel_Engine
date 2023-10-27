using System;

using UnityEngine;

namespace TheAshBot
{
    public class FunctionTimer
    {


        #region Static

        /// <summary>
        /// is used to trigger an OnUpdate action
        /// </summary>
        private class DummyTimerClass : MonoBehaviour
        {
            /// <summary>
            /// is triggerd every update
            /// </summary>
            public event Action OnUpdate;

            private void Update()
            {
                OnUpdate?.Invoke();
            }
        }


        /// <summary>
        /// is the curent dummy timer class. there should only be one of these.
        /// </summary>
        private static DummyTimerClass dummyTimer;


        /// <summary>
        /// will create a timer and when the timer is equal to wait time it will trigger the OnTimerFinished action
        /// </summary>
        /// <param name="waitTime">is the time that will pass before the OnTimerFinished is tiggered</param>
        /// <param name="disposeAfter_Cycles">is the number of cycles before the timer will be disposed. Put 0 if you do not want it to be automaticly be disposed of. Defualts to 1</param>
        /// <param name="useUnscaledDeltaTime">if true then it will use unscaled delta time for the timer. Else it will used scaled delta time. Defualts to false</param>
        /// <param name="OnTimerFinished">is the function that will be called when the timer is complete. this has a function timer that you can dispose manualy of if you want <code>FunctionTimer.Dispose();</code></param>
        /// <returns>a function timer that you can dispose of when ever</returns>
        public static FunctionTimer Create(float waitTime, int disposeAfter_Cycles, bool useUnscaledDeltaTime, Action<FunctionTimer> OnTimerFinished)
        {
            InitIfNeeded();

            return new FunctionTimer(waitTime, disposeAfter_Cycles, useUnscaledDeltaTime, OnTimerFinished);
        }
        /// <summary>
        /// will create a timer and when the timer is equal to wait time it will trigger the OnTimerFinished action
        /// </summary>
        /// <param name="waitTime">is the time that will pass before the OnTimerFinished is tiggered</param>
        /// <param name="disposeAfter_Cycles">is the number of cycles before the timer will be disposed. Put 0 if you do not want it to be automaticly be disposed of. Defualts to 1</param>
        /// <param name="OnTimerFinished">is the function that will be called when the timer is complete. this has a function timer that you can dispose manualy of if you want <code>FunctionTimer.Dispose();</code></param>
        /// <returns>a function timer that you can dispose of when ever</returns>
        public static FunctionTimer Create(float waitTime, int disposeAfter_Cycles, Action<FunctionTimer> OnTimerFinished)
        {
            return Create(waitTime, disposeAfter_Cycles, false, OnTimerFinished);
        }
        /// <summary>
        /// will create a timer and when the timer is equal to wait time it will trigger the OnTimerFinished action
        /// </summary>
        /// <param name="waitTime">is the time that will pass before the OnTimerFinished is tiggered</param>
        /// <param name="OnTimerFinished">is the function that will be called when the timer is complete. this has a function timer that you can dispose manualy of if you want <code>FunctionTimer.Dispose();</code></param>
        /// <returns>a function timer that you can dispose of when ever</returns>
        public static FunctionTimer Create(float waitTime, Action<FunctionTimer> OnTimerFinished)
        {
            return Create(waitTime, 1, false, OnTimerFinished);
        }


        /// <summary>
        /// This will create the dummy timer if it needs to.
        /// </summary>
        private static void InitIfNeeded()
        {
            if (dummyTimer == null)
            {
                dummyTimer = new GameObject("DO NOT DELETE!!! This is a Dummy Timer", typeof(DummyTimerClass)).GetComponent<DummyTimerClass>();
                GameObject.DontDestroyOnLoad(dummyTimer);
            }
        }


        #endregion




        #region Instance

        private float waitTime;
        private int disposeAfter_Cycles;
        private bool useUnscaledDeltaTime;
        private Action<FunctionTimer> OnTimerFinished;

        private float timer;
        private int cycles;


        private FunctionTimer(float waitTime, int disposeAfter_Cycles, bool useUnscaledDeltaTime, Action<FunctionTimer> OnTimerFinished)
        {
            this.waitTime = waitTime;
            this.disposeAfter_Cycles = disposeAfter_Cycles;
            this.useUnscaledDeltaTime = useUnscaledDeltaTime;
            this.OnTimerFinished = OnTimerFinished;

            timer = 0;

            dummyTimer.OnUpdate += UpdateTimer;
        }

        /// <summary>
        /// will update the timer, and check if the timer is finished, and if it should be destoryed.
        /// </summary>
        private void UpdateTimer()
        {
            timer += useUnscaledDeltaTime ? Time.unscaledDeltaTime : Time.deltaTime;

            if (timer >= waitTime)
            {
                timer = 0;
                cycles++;

                OnTimerFinished?.Invoke(this);

                if (cycles >= disposeAfter_Cycles && disposeAfter_Cycles > 0)
                {
                    Dispose();
                }
            }
        }

        /// <summary>
        /// This will stop the timer from running any more.
        /// </summary>
        public void Dispose()
        {
            dummyTimer.OnUpdate -= UpdateTimer;
        }

        #endregion


    }
}
