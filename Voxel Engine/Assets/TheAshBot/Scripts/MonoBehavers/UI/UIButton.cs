using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TheAshBot.UI
{
    public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {


        public class Text
        {
            public bool woodWrap = false;
            public int fontSize = 32;
            public string text = "null";
            public string name = "Text (TMP)";
            public Color textColor = Color.black;
            public TextAlignmentOptions alignment = TextAlignmentOptions.Midline;
            public Vector2 position = Vector2.zero;
            public Vector2 size = Vector2.zero;
            public Vector2 scale = Vector2.one;

            public TMP_FontAsset fontAsset = null;
            public Material fontMaterial = null;
        }



        #region Static

        public static UIButton Create(Vector2 position, Vector2 size, Sprite sprite, UIButton baseUIButton, Text text, string name)
        {
            Canvas canvas = FindAnyObjectByType<Canvas>();

            // finding the buttons holder gamobject
            GameObject uiButtonHolderGameObject = GameObject.Find("UIButtonHolder");
            if (uiButtonHolderGameObject != null)
            {
                if (uiButtonHolderGameObject.GetComponent<UIButton>().GetComponentInParent<Canvas>() == null)
                {
                    Make_UIButtonHolder();
                }
            }
            else
            {
                Make_UIButtonHolder();
            }

            // Maing the button GameObject
            GameObject uiButtonGameObject = new GameObject(name);
            uiButtonGameObject.transform.parent = uiButtonHolderGameObject.transform;
            uiButtonGameObject.transform.localScale = Vector2.one;

            RectTransform uiButtonRectTransform = uiButtonGameObject.AddComponent<RectTransform>();
            uiButtonRectTransform.localPosition = position;
            uiButtonRectTransform.sizeDelta = size;

            // Setting up the button
            Image uiButtonImage = uiButtonGameObject.AddComponent<Image>();
            if (sprite == null)
            {
                uiButtonImage.color = Color.HSVToRGB(1f / 3f, 0.7f, 0.7f);
            }
            else
            {
                uiButtonImage.sprite = sprite;
                uiButtonImage.color = Color.white;
            }

            UIButton uiButton = uiButtonGameObject.AddComponent<UIButton>();
            uiButton.renderType = RenderType.Image;
            uiButton.buttonImage = uiButtonImage;

            if (baseUIButton == null)
            {
                uiButton.colorVisualization = ColorVisualization.SetColor;
                uiButton.defualtColor = Color.HSVToRGB(1f / 3f, 0.7f, 0.7f);
                uiButton.mouseOverUIColor = Color.HSVToRGB(1f / 3f, 0.7f, 0.65f);
                uiButton.holdingMouseDownOverUIColor = Color.HSVToRGB(1f / 3f, 0.7f, 0.5f);
            }
            else
            {
                uiButton.OnMouseEnterUI = baseUIButton.OnMouseEnterUI;
                uiButton.OnMouseExitUI = baseUIButton.OnMouseExitUI;
                uiButton.OnMouseStartClickUI = baseUIButton.OnMouseStartClickUI;
                uiButton.OnMouseEndClickUI = baseUIButton.OnMouseEndClickUI;

                uiButton.colorVisualization = baseUIButton.colorVisualization;
                uiButton.defualtColor = baseUIButton.defualtColor;
                uiButton.mouseOverUIColor = baseUIButton.mouseOverUIColor;
                uiButton.holdingMouseDownOverUIColor = baseUIButton.holdingMouseDownOverUIColor;
            }

            // Making the Text
            if (text != null)
            {
                GameObject textGameObject = new GameObject(text.name);
                textGameObject.transform.parent = uiButtonGameObject.transform;

                RectTransform textRectTransfrom = textGameObject.AddComponent<RectTransform>();
                TextMeshProUGUI textTextMeshProGUI = textGameObject.AddComponent<TextMeshProUGUI>();

                textTextMeshProGUI.enableWordWrapping = text.woodWrap;
                textTextMeshProGUI.fontSize = text.fontSize;
                textTextMeshProGUI.text = text.text;
                textTextMeshProGUI.alignment = text.alignment;
                textTextMeshProGUI.color = text.textColor;
                if (text.fontAsset != null)
                {
                    textTextMeshProGUI.font = text.fontAsset;
                }
                if (text.fontMaterial != null)
                {
                    textTextMeshProGUI.fontMaterial = text.fontMaterial;
                }

                textRectTransfrom.localScale = text.scale;
                textRectTransfrom.sizeDelta = text.size;
                textRectTransfrom.localPosition = text.position;
            }

            return uiButton;

            void Make_UIButtonHolder()
            {
                uiButtonHolderGameObject = new GameObject("UIButtonHolder");
                uiButtonHolderGameObject.transform.parent = canvas.transform;
                RectTransform uiButtonHolderRectTransfrom = uiButtonHolderGameObject.AddComponent<RectTransform>();
                uiButtonHolderRectTransfrom.anchoredPosition = new Vector2(0, 0);
                uiButtonHolderRectTransfrom.sizeDelta = new Vector2(0, 0);
                uiButtonHolderRectTransfrom.localScale = Vector2.one;
                uiButtonHolderRectTransfrom.anchorMin = Vector2.zero;
                uiButtonHolderRectTransfrom.anchorMax = Vector2.one;
            }
        }
        #region Create
        public static UIButton Create(Vector2 position, Vector2 size, Sprite sprite, UIButton baseUIButton, Text text)
        {
            return Create(position, size, sprite, baseUIButton, text, "UI Button");
        }
        public static UIButton Create(Vector2 position, Vector2 size, Sprite sprite, UIButton baseUIButton, string name)
        {
            return Create(position, size, sprite, baseUIButton, null, name);
        }
        public static UIButton Create(Vector2 position, Vector2 size, Sprite sprite, UIButton baseUIButton)
        {
            return Create(position, size, sprite, baseUIButton, null, "UI Button");
        } 
        public static UIButton Create(Vector2 position, Vector2 size, Sprite sprite, Text text, string name)
        {
            return Create(position, size, sprite, null, text, name);
        }
        public static UIButton Create(Vector2 position, Vector2 size, Sprite sprite, Text text)
        {
            return Create(position, size, sprite, null, text, "UI Button");
        }
        public static UIButton Create(Vector2 position, Vector2 size, Sprite sprite, string name)
        {
            return Create(position, size, sprite, null, null, name);
        }
        public static UIButton Create(Vector2 position, Vector2 size, Sprite sprite)
        {
            return Create(position, size, sprite, null, null, "UI Button");
        }
        public static UIButton Create(Vector2 position, Vector2 size, UIButton baseUIButton, Text text, string name)
        {
            return Create(position, size, null, baseUIButton, text, name);
        }
        public static UIButton Create(Vector2 position, Vector2 size, UIButton baseUIButton, Text text)
        {
            return Create(position, size, null, baseUIButton, text, "UI Button");
        }
        public static UIButton Create(Vector2 position, Vector2 size, UIButton baseUIButton, string name)
        {
            return Create(position, size, null, baseUIButton, null, name);
        }
        public static UIButton Create(Vector2 position, Vector2 size, UIButton baseUIButton)
        {
            return Create(position, size, null, baseUIButton, null, "UI Button");
        } 
        public static UIButton Create(Vector2 position, Vector2 size, Text text, string name)
        {
            return Create(position, size, null, null, text, name);
        }
        public static UIButton Create(Vector2 position, Vector2 size, Text text)
        {
            return Create(position, size, null, null, text, "UI Button");
        }
        public static UIButton Create(Vector2 position, Vector2 size, string name)
        {
            return Create(position, size, null, null, null, name);
        }
        public static UIButton Create(Vector2 position, Vector2 size)
        {
            return Create(position, size, null, null, null, "UI Button");
        }
        #endregion

        #endregion





        #region Enums

        public enum ColorVisualization
        {
            [InspectorName("None\\Custom")]
            None,
            SetColor,
        }
        public enum RenderType
        {
            [InspectorName("None\\Custom")]
            None,
            Image,
            RawImage,
        }

        #endregion


        #region Variables
        // These are public becouse they I want to be able to get/set them in other scripts, and show them in the inspector
#if ODIN_INSPECTOR
        #region ODIN_INSPECTOR
        
        public event Action OnMouseEnterUI;
        public event Action OnMouseExitUI;
        public event Action OnMouseStartClickUI;
        public event Action OnMouseEndClickUI;


        [Header("Renderer")]
        [EnumToggleButtons()]
        public RenderType renderType = RenderType.None;
        [ShowIf("@renderType == RenderType.Image")]
        public Image buttonImage;
        [ShowIf("@renderType == RenderType.RawImage")]
        public RawImage buttonRawImage;


        [Header("ColorVisualization")]
        [EnumToggleButtons()]
        public ColorVisualization colorVisualization = ColorVisualization.SetColor;
        [HideIf("@colorVisualization == ColorVisualization.None")]
        public Color defualtColor = Color.white;
        [HideIf("@colorVisualization == ColorVisualization.None")]
        public Color mouseOverUIColor = Color.white;
        [HideIf("@colorVisualization == ColorVisualization.None")]
        public Color holdingMouseDownOverUIColor = Color.white;

        [Header("Other")]
        private bool isMouseOverUI;
        #endregion
#else
        #region NOT ODIN_INSPECTOR

        public event Action OnMouseEnterUI;
        public event Action OnMouseExitUI;
        public event Action OnMouseStartClickUI;
        public event Action OnMouseEndClickUI;


        [Header("Renderer")]
        public RenderType renderType;
        public Image buttonImage;
        public RawImage buttonRawImage;


        [Header("ColorVisualization")]
        public ColorVisualization colorVisualization = ColorVisualization.SetColor;
        public Color defualtColor = Color.white;
        public Color mouseOverUIColor = Color.white;
        public Color holdingMouseDownOverUIColor = Color.white;

        [Header("Other")]
        [SerializeField] private TextMeshProUGUI text;
        private bool isMouseOverUI;

        #endregion
#endif

        #endregion


        #region Unity Functions

        private void Awake()
        {
            if (buttonImage == null)
            {
                TryGetComponent(out buttonImage);
            }
            if (buttonRawImage == null)
            {
                TryGetComponent(out buttonRawImage);
            }
            if (text == null)
            {
                if (!TryGetComponent(out text))
                {
                    text = GetComponentInChildren<TextMeshProUGUI>();
                }
            }
        }

        private void Start()
        {
            SetColorTo_DefualtColor();
        }

        #endregion


        #region Interfaces

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("OnPointerEnter", this);

            OnMouseEnterUI?.Invoke();

            isMouseOverUI = true;

            SetColorTo_MouseOverUIColor();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("OnPointerExit", this);

            OnMouseExitUI?.Invoke();

            isMouseOverUI = false;

            SetColorTo_DefualtColor();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("OnPointerDown", this);

            OnMouseStartClickUI?.Invoke();

            SetColorTo_HoldingMouseDownOverUIColor();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("OnPointerUp", this);

            OnMouseEndClickUI?.Invoke();

            SetColorTo_MouseOverUIColor();
        }

        #endregion


        #region Public Helper Functions

        /// <summary>
        /// sets the color of the renderer.
        /// </summary>
        /// <param name="color">is the color that the renderer will have.</param>
        public void SetColor(Color color)
        {
            switch (renderType)
            {
                case RenderType.Image:
                    buttonImage.color = color;
                    break;
                case RenderType.RawImage:
                    buttonRawImage.color = color;
                    break;
            }
        }

        /// <summary>
        /// will set the sprite/Texture of the Image/RawImage.
        /// </summary>
        /// <param name="sprite">is the sprite/texture that the the renderer is going to be set to</param>
        public void SetSprite(Sprite sprite)
        {
            switch (renderType)
            {
                case RenderType.Image:
                    buttonImage.sprite = sprite;
                    break;
                case RenderType.RawImage:
                    buttonRawImage.texture = sprite.texture;
                    break;
            }
        }

        /// <summary>
        /// will make all the events = null. A.K.A. Removing all subscribers.
        /// </summary>
        public void SetAllEventsToNull()
        {
            OnMouseEnterUI = null;
            OnMouseEnterUI = null;
            OnMouseStartClickUI = null;
            OnMouseEndClickUI = null;
        }

        /// <summary>
        /// will return true if the mouse is over the UI and false if not.
        /// </summary>
        /// <returnstrue if the mouse is over the UI and false if not></returns>
        public bool IsMouseOverUI()
        {
            return isMouseOverUI;
        }

        /// <summary>
        /// will get the text if it has any.
        /// </summary>
        /// <param name="text">the text component</param>
        /// <returns>true if has text, else false</returns>
        public bool TryGetText(out TextMeshProUGUI text)
        {
            text = this.text;
            return text != null;
        }

        #endregion


        #region Set Color

        /// <summary>
        /// sets the color of the renderer to be the defualt color.
        /// </summary>
        private void SetColorTo_DefualtColor()
        {
            if (renderType == RenderType.None || colorVisualization == ColorVisualization.None) return;

            SetColor(defualtColor);
        }

        /// <summary>
        /// sets the color of the renderer to be the MouseOverUI color.
        /// </summary>
        private void SetColorTo_MouseOverUIColor()
        {
            if (renderType == RenderType.None || colorVisualization == ColorVisualization.None) return;

            SetColor(mouseOverUIColor);
        }

        /// <summary>
        /// sets the color of the renderer to be the HoldingMouseDownOverUI color.
        /// </summary>
        private void SetColorTo_HoldingMouseDownOverUIColor()
        {
            if (renderType == RenderType.None || colorVisualization == ColorVisualization.None) return;

            SetColor(holdingMouseDownOverUIColor);
        }

        #endregion


    }
}
