using UnityEngine;

namespace TheAshBot.MonoBehaviours
{
    [RequireComponent(typeof(Renderer))]
    public class RendererSortingOrderSorter : MonoBehaviour
    {

        [Tooltip("This will destory the script after it changes the sorting order.")]
        public bool runOnlyOnce = false;
        [Tooltip("This is the Base sorting order. the heigher it is the more it is going to be on top.")]
        public int sortingOrderBase = 5000;
        [Tooltip("This is the y ofset of the sprite. make sure it lines of woth the buttom if the sprite.")]
        public int offset = 0;


        private float timer;
        private float timerMax = 0.1f;

        private new Renderer renderer;


        private void Awake()
        {
            renderer = gameObject.GetComponent<SpriteRenderer>();
        }

        private void LateUpdate()
        {
            // When the timer is less than 0 sort.
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = timerMax;
                renderer.sortingOrder = Mathf.RoundToInt(sortingOrderBase - transform.position.y) - offset;
                if (runOnlyOnce)
                {
                    // Desotry this component if only run once.
                    Destroy(this);
                }
            }
        }


    }
}
