using System;

namespace TheAshBot.ProgressionSystem
{
    public class ProgressionSystem
    {


        public event EventHandler OnProgressFinished;
        public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
        public class OnProgressChangedEventArgs : EventArgs
        {
            public int value;
            public int amount;
        }



        private bool isCountingUp;
        private int maxProgress;
        private int progress;

        /// <summary>
        /// This will create the Pregress system
        /// </summary>
        /// <param name="maxProgress">This is the maxium amout of progress</param>
        /// <param name="isCountingUp">Thid determains if the progress bar gose from 0 to Max progress, or Max progress to 0</param>
        public ProgressionSystem(int maxProgress, bool isCountingUp = true)
        {
            this.isCountingUp = isCountingUp;
            this.maxProgress = maxProgress;
            int starProgresstion = isCountingUp ? 0 : maxProgress;
            progress = starProgresstion;
        }


        #region Max Progress

        public int GetMaxProgress()
        {
            return maxProgress;
        }

        public void AddMaxProgress(int amount)
        {
            maxProgress += amount;
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                value = progress,
                amount = 0,
            });
        }

        public void SetMaxProgress(int maxProgress)
        {
            this.maxProgress = maxProgress;
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                value = progress,
                amount = 0,
            });
        }

        #endregion


        #region Progress

        public int GetProgress()
        {
            return progress;
        }

        public float GetProgressPercent()
        {
            return (float)progress / maxProgress;
        }

        public void SetProgress(int progress)
        {
            this.progress = progress;

            // At start progress
            if (isCountingUp ? this.progress <= 0 : this.progress >= maxProgress)
            {
                this.progress = isCountingUp ? 0 : maxProgress;
            }

            // At end progress
            if (isCountingUp ? this.progress >= maxProgress : this.progress <= 0)
            {
                this.progress = isCountingUp ? maxProgress : 0;
                OnProgressFinished?.Invoke(this, EventArgs.Empty);
            }

            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                value = progress,
                amount = 0,
            });

        }

        public void AddProgress(int progress)
        {
            this.progress += progress;

            // At start progress
            if (isCountingUp ? this.progress <= 0 : this.progress >= maxProgress)
            {
                this.progress = isCountingUp ? 0 : maxProgress;
            }

            // At end progress
            if (isCountingUp ? this.progress >= maxProgress : this.progress <= 0)
            {
                this.progress = isCountingUp ? maxProgress : 0;
                OnProgressFinished?.Invoke(this, EventArgs.Empty);
            }

            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                value = progress,
                amount = 0,
            });

        }

        public void SetProgressToMaxProgress()
        {
            progress = maxProgress;

            // At start progress
            if (isCountingUp ? progress <= 0 : progress >= maxProgress)
            {
                progress = isCountingUp ? 0 : maxProgress;
            }

            // At end progress
            if (isCountingUp ? progress >= maxProgress : progress <= 0)
            {
                progress = isCountingUp ? maxProgress : 0;
                OnProgressFinished?.Invoke(this, EventArgs.Empty);
                return;
            }

            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                value = progress,
                amount = 0,
            });

        }

        #endregion


        #region Is Counting Up

        public bool GetIsCountingUp()
        {
            return isCountingUp;
        }

        public void SetIsCountingUp(bool isCountingUp)
        {
            this.isCountingUp = isCountingUp;
        }

        #endregion


    }
}
