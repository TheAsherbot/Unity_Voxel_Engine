using TheAshBot.MonoBehaviours;

using UnityEngine;

namespace TheAshBot.HealthBarSystem
{
    public class HealthBar : MonoBehaviour
    {

        public class Border
        {
            public float thickness;
            public Color color;
        }


        public static HealthSystem Create(int maxHealth, Transform fallow, Vector3 offset, Vector2 size, Color barColor, 
            Color backgroundColor, Border border = null, bool hideWhenFull = false, int layer = 0, params Component[] addComponents)
        {
            if (maxHealth <= 0)
            {
                return null;
            }

            // Main Health Bar
            GameObject healthBarGameObject = new GameObject("HealthBar");
            healthBarGameObject.transform.localPosition = offset;

            // Placeholder
            GameObject contentGameObject = new GameObject("Content");
            contentGameObject.transform.SetParent(healthBarGameObject.transform);
            contentGameObject.transform.localPosition = Vector3.zero;

            if (border != null)
            {
                // Border
                GameObject borderGameObject = new GameObject("Border", typeof(SpriteRenderer));
                borderGameObject.transform.SetParent(contentGameObject.transform);
                borderGameObject.transform.localPosition = Vector3.zero;
                borderGameObject.transform.localScale = size + Vector2.one * border.thickness;
                borderGameObject.GetComponent<SpriteRenderer>().color = border.color;
                borderGameObject.GetComponent<SpriteRenderer>().sprite = AssetReferences.Instance.SinglePixelSprite;
                RendererSortingOrderSorter borderRendererSortingOrderSorter = borderGameObject.AddComponent<RendererSortingOrderSorter>();
                borderGameObject.gameObject.layer = layer;
                borderRendererSortingOrderSorter.offset = -40;
            }

            // Background
            GameObject backgroundGameObject = new GameObject("Background", typeof(SpriteRenderer));
            backgroundGameObject.transform.SetParent(contentGameObject.transform);
            backgroundGameObject.transform.localPosition = Vector3.zero;
            backgroundGameObject.transform.localScale = size;
            backgroundGameObject.GetComponent<SpriteRenderer>().color = backgroundColor;
            backgroundGameObject.GetComponent<SpriteRenderer>().sprite = AssetReferences.Instance.SinglePixelSprite;
            RendererSortingOrderSorter backgroundRendererSortingOrderSorter = backgroundGameObject.AddComponent<RendererSortingOrderSorter>();
            backgroundGameObject.gameObject.layer = layer;
            backgroundRendererSortingOrderSorter.offset = -50;

            // Bar
            GameObject barGameObject = new GameObject("Bar");
            barGameObject.transform.SetParent(contentGameObject.transform);
            barGameObject.transform.localPosition = new Vector3(-size.x / 2f, 0f);

            // Bar Sprite
            GameObject barSpriteGameObject = new GameObject("barSprite", typeof(SpriteRenderer));
            barSpriteGameObject.transform.SetParent(barGameObject.transform);
            barSpriteGameObject.transform.localPosition = new Vector3(size.x / 2f, 0f);
            barSpriteGameObject.transform.localScale = size;
            barSpriteGameObject.GetComponent<SpriteRenderer>().color = barColor;
            barSpriteGameObject.GetComponent<SpriteRenderer>().sprite = AssetReferences.Instance.SinglePixelSprite;
            RendererSortingOrderSorter barSpriteRendererSortingOrderSorter = barSpriteGameObject.AddComponent<RendererSortingOrderSorter>();
            barSpriteGameObject.gameObject.layer = layer;
            barSpriteRendererSortingOrderSorter.offset = -60;


            // Added more Compents
            foreach (Component component in addComponents)
            {
                healthBarGameObject.CopyComponent(component);
            }

            // Adding components to the new gameobject 
            HealthBar healthBar = healthBarGameObject.AddComponent<HealthBar>();
            HealthSystem healthSystem = new HealthSystem(maxHealth);
            healthBar.SetUp(healthSystem, offset, fallow, barGameObject.transform, healthBarGameObject, contentGameObject, hideWhenFull, fallow != null);
            return healthSystem;
        }

        public static HealthSystem Create(int maxHealth, Vector3 position, Vector2 size, Color barColor, Color backgroundColor, 
            Border border = null, bool hideWhenFull = false, int layer = 0, params Component[] addComponents)
        {
            return Create(maxHealth, null, position, size, barColor, backgroundColor, border, hideWhenFull, layer, addComponents);
        }






        private bool hideWhenFull;
        private bool useFallowTransfrom;
        private Vector3 offset;
        private Transform bar;
        private Transform fallow;
        private GameObject healthBarGameObject;
        private GameObject contentGameObject;

        private HealthSystem healthSystem;


        private void LateUpdate()
        {
            if (!useFallowTransfrom || fallow == null) return;

            transform.position = fallow.position + offset;
        }

        private void SetUp(HealthSystem healthSystem, Vector3 offset, Transform fallow, Transform bar, 
            GameObject healthBarGameObject, GameObject contentGameObject, bool hideWhenFull, bool useFallowTransfrom)
        {
            this.hideWhenFull = hideWhenFull;
            this.useFallowTransfrom = useFallowTransfrom;
            this.offset = offset;
            this.fallow = fallow;
            this.bar = bar;
            this.healthBarGameObject = healthBarGameObject;
            this.contentGameObject = contentGameObject;

            this.healthSystem = healthSystem;

            if (hideWhenFull)
            {
                contentGameObject.SetActive(false);
            }

            healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
            healthSystem.OnHealthDepleted += HealthSystem_OnHealthDepleted;
        }

        private void HealthSystem_OnHealthDepleted(object sender, System.EventArgs e)
        {
            Destroy(healthBarGameObject);
        }

        private void HealthSystem_OnHealthChanged(object sender, HealthSystem.OnHealthChangedEventArgs e)
        {
            if (hideWhenFull)
            {
                bool hide = e.value == healthSystem.GetMaxHealth();
                contentGameObject.SetActive(!
                    
                    hide);
            }

            bar.localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
        }


    }
}
