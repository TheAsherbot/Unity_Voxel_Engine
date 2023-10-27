using System;

using TMPro;

using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace TheAshBot.UI
{
    public class TooltopScreenSpaceUI : MonoBehaviour
    {

        public static TooltopScreenSpaceUI Instance
        {
            get;
            private set;
        }



        private bool useGetTooltipFunc;
        private Func<string> getTooltipTextFunc;


        [SerializeField] private RectTransform canvasRectTransfrom;
        [SerializeField] private RectTransform backgroundRectTransfrom;
        [SerializeField] private TextMeshProUGUI textMeshPro;

        private RectTransform rectTransform;


        private void Awake()
        {
            if (Instance != null)
            {
                this.LogError("There is more than one instance of the TooltopScreenSpaceUI Class!!! this should NEVER happen!");
                Destroy(this);
                return;
            }

            Instance = this;

            rectTransform = GetComponent<RectTransform>();

            Hide();
        }

        private void LateUpdate()
        {
            if (useGetTooltipFunc)
            {
                SetText(getTooltipTextFunc());
            }

#if ENABLE_INPUT_SYSTEM
            Vector2 anchoredPosition = Mouse.current.position.ReadValue() / canvasRectTransfrom.localScale.x; // x, y, or z will work here becouse all of them will be the same.
#elif !ENABLE_INPUT_SYSTEM
            Vector2 anchoredPosition = Input.mousePosition / canvasRectTransfrom.localScale.x; // x, y, or z will work here becouse all of them will be the same.
#else
        return;
#endif

            if (anchoredPosition.x + backgroundRectTransfrom.rect.width > canvasRectTransfrom.rect.width)
            {
                // Tooltip has left the screen on right side of the screen
                anchoredPosition.x = canvasRectTransfrom.rect.width - backgroundRectTransfrom.rect.width;
            }
            else if (anchoredPosition.x < 0)
            {
                // Tooltip has left the screen on left side of the screen
                anchoredPosition.x = 0;
            }

            if (anchoredPosition.y + backgroundRectTransfrom.rect.height > canvasRectTransfrom.rect.height)
            {
                // Tooltip has left the screen on top side of the screen
                anchoredPosition.y = canvasRectTransfrom.rect.height - backgroundRectTransfrom.rect.height;
            }
            else if (anchoredPosition.y < 0)
            {
                // Tooltip has left the screen on bottom side of the screen
                anchoredPosition.y = 0;
            }

            Vector2 offset = new Vector2(8, 8);

            rectTransform.anchoredPosition = anchoredPosition + offset;
        }


        private void SetText(string tooltipText)
        {
            textMeshPro.text = tooltipText;
            textMeshPro.ForceMeshUpdate();

            Vector2 textSize = textMeshPro.GetRenderedValues(false);

            backgroundRectTransfrom.sizeDelta = textSize;
        }

        private void Show(string tooltipText)
        {
            if (tooltipText == null || tooltipText == string.Empty || tooltipText == "") return;

            useGetTooltipFunc = false;
            gameObject.SetActive(true);
            SetText(tooltipText);
        }
        private void Show(out Action<string> OnTooltipChanged)
        {
            useGetTooltipFunc = false;
            OnTooltipChanged = Show;
        }
        private void Show(Func<string> getTooltipTextFunc)
        {
            useGetTooltipFunc = true;
            this.getTooltipTextFunc = getTooltipTextFunc;
            gameObject.SetActive(true);
            SetText(getTooltipTextFunc());
        }

        private void Hide()
        {
            useGetTooltipFunc = false;
            gameObject.SetActive(false);
        }


        /// <summary>
        /// will show the toottip
        /// </summary>
        /// <param name="tooltipText"></param>
        public static void ShowTooltip(string tooltipText)
        {
            Instance.Show(tooltipText);
        }
        /// <summary>
        /// will show the toottip
        /// </summary>
        /// <param name="OnTooltipChanged">when triggerd the tooltip will change to the disierd text</param>
        public static void ShowTooltip(out Action<string> OnTooltipChanged)
        {
            Instance.Show(out OnTooltipChanged);
        }
        /// <summary>
        /// will show the toottip
        /// </summary>
        /// <param name="getTooltipTextFunc">Called every frame. it will allow you to chnage the tooltip text to what ever you would like very frame.</param>
        public static void ShowTooltip(Func<string> getTooltipTextFunc)
        {
            Instance.Show(getTooltipTextFunc);
        }

        /// <summary>
        /// will disable the tool tip.
        /// </summary>
        public static void HideTooltip()
        {
            Instance.Hide();
        }


    }
}