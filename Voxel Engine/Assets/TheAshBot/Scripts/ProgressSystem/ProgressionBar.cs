using System;

using TheAshBot.MonoBehaviours;

using UnityEngine;

namespace TheAshBot.ProgressionSystem
{
    public class ProgressionBar : MonoBehaviour
    {


        public class Border
        {
            public float thickness;
            public Color color;
        }


        #region Create

        public static ProgressionSystem Create(int maxProgress, Transform fallow, Vector3 offset, Vector2 size, Color barColor, Color backgroundColor,
            Border border = null, bool isCountingUp = true, bool isVertical = false, int layer = 0, params Component[] addComponents)
        {
            //                                             This is backwards becouse I am going to roatate it 90 degres.
            Vector2 rotatedSize = isVertical ? new Vector2(size.y, size.x) : size;

            // Main Health Bar
            GameObject healthBarGameObject = new GameObject("ProgressBar");
            healthBarGameObject.transform.localPosition = offset;

            if (border != null)
            {
                // Border
                GameObject borderGameObject = new GameObject("Border", typeof(SpriteRenderer));
                borderGameObject.transform.SetParent(healthBarGameObject.transform);
                borderGameObject.transform.localPosition = Vector3.zero;
                borderGameObject.transform.localScale = rotatedSize + Vector2.one * border.thickness;
                borderGameObject.GetComponent<SpriteRenderer>().color = border.color;
                borderGameObject.GetComponent<SpriteRenderer>().sprite = AssetReferences.Instance.SinglePixelSprite;
                RendererSortingOrderSorter borderRendererSortingOrderSorter = borderGameObject.AddComponent<RendererSortingOrderSorter>();
                borderGameObject.gameObject.layer = layer;
                borderRendererSortingOrderSorter.offset = -40;
            }

            // Background
            GameObject backgroundGameObject = new GameObject("Background", typeof(SpriteRenderer));
            backgroundGameObject.transform.SetParent(healthBarGameObject.transform);
            backgroundGameObject.transform.localPosition = Vector3.zero;
            backgroundGameObject.transform.localScale = rotatedSize;
            backgroundGameObject.GetComponent<SpriteRenderer>().color = backgroundColor;
            backgroundGameObject.GetComponent<SpriteRenderer>().sprite = AssetReferences.Instance.SinglePixelSprite;
            RendererSortingOrderSorter backgroundRendererSortingOrderSorter = backgroundGameObject.AddComponent<RendererSortingOrderSorter>();
            backgroundGameObject.gameObject.layer = layer;
            backgroundRendererSortingOrderSorter.offset = -50;

            // Bar
            GameObject barGameObject = new GameObject("Bar");
            barGameObject.transform.SetParent(healthBarGameObject.transform);
            barGameObject.transform.localPosition = new Vector3(-rotatedSize.x / 2f, 0f);
            barGameObject.transform.localScale = new Vector3(isCountingUp ? 0 : 1, 1);

            // Bar Sprite
            GameObject barSpriteGameObject = new GameObject("barSprite", typeof(SpriteRenderer));
            barSpriteGameObject.transform.SetParent(barGameObject.transform);
            barSpriteGameObject.transform.localPosition = new Vector3(rotatedSize.x / 2f, 0f);
            barSpriteGameObject.transform.localScale = rotatedSize;
            barSpriteGameObject.GetComponent<SpriteRenderer>().color = barColor;
            barSpriteGameObject.GetComponent<SpriteRenderer>().sprite = AssetReferences.Instance.SinglePixelSprite;
            RendererSortingOrderSorter barSpriteRendererSortingOrderSorter = barSpriteGameObject.AddComponent<RendererSortingOrderSorter>();
            barSpriteGameObject.gameObject.layer = layer;
            barSpriteRendererSortingOrderSorter.offset = -60;

            

            if (isVertical)
            {
                healthBarGameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
            }


            // Added more Compents
            foreach (Component component in addComponents)
            {
                healthBarGameObject.CopyComponent(component);
            }

            // Setting up the progression bar
            ProgressionBar progressBar = healthBarGameObject.AddComponent<ProgressionBar>();
            ProgressionSystem healthSystem = new ProgressionSystem(maxProgress, isCountingUp);
            progressBar.SetUp(healthSystem, offset, fallow, barGameObject.transform, healthBarGameObject);
            return healthSystem;
        }

        public static ProgressionSystem Create(int maxProgress, Vector3 position, Vector2 size, Color barColor, Color backgroundColor,
            Border border = null, bool isCountingUp = true, bool isVertical = false, int layer = 0, params Component[] addComponents)
        {
            return Create(maxProgress, null, position, size, barColor, backgroundColor, border, isCountingUp, isVertical, layer, addComponents);
        }

        #endregion


        #region Variables

        private Vector3 offset;
        private Transform bar;
        private Transform fallow;
        private GameObject healthBarGameObject;

        private ProgressionSystem progressSystem;

        #endregion


        #region Unity Functions

        private void LateUpdate()
        {
            if (fallow != null)
            {
                transform.position = fallow.position + offset;
            }
        }

        #endregion


        private void SetUp(ProgressionSystem progressSystem, Vector3 offset, Transform fallow, Transform bar, GameObject healthBarGameObject)
        {
            this.offset = offset;
            this.fallow = fallow;
            this.bar = bar;
            this.healthBarGameObject = healthBarGameObject;

            this.progressSystem = progressSystem;

            if (fallow != null)
            {
                transform.position = fallow.position + offset;
            }
            else
            {
                transform.position = offset;
            }

            progressSystem.OnProgressChanged += ProgressSystem_OnProgressChanged;
            progressSystem.OnProgressFinished += ProgressSystem_OnProgressFinished;
        }

        private void OnDestroy()
        {
            progressSystem.OnProgressChanged -= ProgressSystem_OnProgressChanged;
            progressSystem.OnProgressFinished -= ProgressSystem_OnProgressFinished;
        }


        #region Events (Subscriptions)

        private void ProgressSystem_OnProgressChanged(object sender, ProgressionSystem.OnProgressChangedEventArgs e)
        {
            bar.localScale = new Vector3(progressSystem.GetProgressPercent(), 1);
        }

        private void ProgressSystem_OnProgressFinished(object sender, EventArgs e)
        {
            DestroyImmediate(gameObject);
        }

        #endregion



    }
}
