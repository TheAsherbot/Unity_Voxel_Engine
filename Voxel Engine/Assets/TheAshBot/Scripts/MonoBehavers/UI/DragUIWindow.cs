using UnityEngine;
using UnityEngine.EventSystems;

namespace TheAshBot.UI
{
    public class DragUIWindow : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler
    {


        #region Variables

        [Header("If these are null then it will atomaticly try to find them")]
        [SerializeField] private RectTransform dragRectTransfrom;
        [SerializeField] private Canvas canvas;


        private Vector2 mouseOffset;

        #endregion


        #region Unity Functions

        private void Awake()
        {
            if (dragRectTransfrom == null)
            {
                dragRectTransfrom = transform.parent.GetComponent<RectTransform>();
            }

            if (canvas == null)
            {
                // Cycling though all parents untill it finds one with a canvus
                Transform textCanvusTransfrom = transform.parent;
                while (textCanvusTransfrom != null)
                {
                    if (textCanvusTransfrom.TryGetComponent(out canvas))
                    {
                        break;
                    }

                    textCanvusTransfrom = textCanvusTransfrom.parent;
                }
            }
        }

        #endregion


        #region Interfaces

        public void OnDrag(PointerEventData eventData)
        {
            RectTransform canvasRectTransfrom = canvas.GetComponent<RectTransform>();

            Vector2 anchoredMouse = eventData.position / canvas.scaleFactor;

            Vector2 anchoredPosition = anchoredMouse;
            float padding = 64;

            // making sure it does not go to far off screen
            if (anchoredPosition.x + padding > canvasRectTransfrom.rect.width)
            {
                // Tooltip has left the screen on right side of the screen
                anchoredPosition.x = canvasRectTransfrom.rect.width - padding;
            }
            else if (anchoredPosition.x - padding < 0)
            {
                // Tooltip has left the screen on left side of the screen
                anchoredPosition.x = padding;
            }
            
            if (anchoredPosition.y + padding > canvasRectTransfrom.rect.height)
            {
                // Tooltip has left the screen on top side of the screen
                anchoredPosition.y = canvasRectTransfrom.rect.height - padding;
            }
            else if (anchoredPosition.y - padding < 0)
            {
                // Tooltip has left the screen on bottom side of the screen
                anchoredPosition.y = padding;
            }


            dragRectTransfrom.anchoredPosition = anchoredPosition - mouseOffset;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // Setting this on top
            dragRectTransfrom.SetAsLastSibling();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // getting the mouse position relitive to the position of the window.
            Vector2 anchoredMouse = eventData.position / canvas.scaleFactor;

            mouseOffset = anchoredMouse - dragRectTransfrom.anchoredPosition;
        }

        #endregion


    }
}
