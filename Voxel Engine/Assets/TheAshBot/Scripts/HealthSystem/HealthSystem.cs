using System;

namespace TheAshBot.HealthBarSystem
{
    public class HealthSystem
    {

        public event EventHandler OnHealthDepleted;
        public event EventHandler<OnHealthChangedEventArgs> OnHealthChanged;
        public class OnHealthChangedEventArgs : EventArgs
        {
            public int value;
            public int amount;
        }



        private int maxHealth;
        private int health;


        public HealthSystem(int maxHealth)
        {
            if (maxHealth < 0)
            {
                return;
            }
            this.maxHealth = maxHealth;
            health = maxHealth;
        }

        public int GetHealth()
        {
            return health;
        }

        public int GetMaxHealth()
        {
            return maxHealth;
        }

        public void AddMaxHealth(int amount)
        {
            maxHealth += amount;
            OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs
            {
                value = health,
                amount = 0,
            });
        }

        public void SetMaxHealth(int maxHealth)
        {
            this.maxHealth = maxHealth;
            OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs
            {
                value = health,
                amount = 0,
            });
        }

        public void SetHealthToMaxHealth()
        {
            health = maxHealth;
            OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs
            {
                value = health,
                amount = 0,
            });
        }

        public void SetHealth(int health)
        {
            this.health = health;

            if (this.health > maxHealth)
            {
                this.health = maxHealth;
                return;
            }

            if (this.health <= 0)
            {
                this.health = 0;
                OnHealthDepleted?.Invoke(this, EventArgs.Empty);
                return;
            }

            OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs
            {
                value = health,
                amount = 0,
            });
        }

        public float GetHealthPercent()
        {
            return (float)health / maxHealth;
        }

        public void Damage(int damageAmount)
        {
            health -= damageAmount;

            if (health > maxHealth)
            {
                health = maxHealth;
            }

            if (health <= 0)
            {
                health = 0;
                OnHealthDepleted?.Invoke(this, EventArgs.Empty);
                return;
            }

            OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs
            {
                value = health,
                amount = -damageAmount,
            });
        }

        public void Heal(int healAmount)
        {
            health += healAmount;

            if (health > maxHealth)
            {
                health = maxHealth;
            }

            if (health <= 0)
            {
                health = 0;
                OnHealthDepleted?.Invoke(this, EventArgs.Empty);
                return;
            }

            OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs
            {
                value = health,
                amount = healAmount,
            });
        }


    }
}
