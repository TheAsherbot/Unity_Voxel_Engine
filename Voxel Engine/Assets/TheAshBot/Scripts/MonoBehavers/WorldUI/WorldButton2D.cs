using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using UnityEngine;

using TheAshBot.TwoDimentional;
using TMPro;

namespace TheAshBot.WorldUI
{
    public class WorldButton2D : MonoBehaviour
    {


        public class Text
        {
            public bool woodWrap = false;
            public int fontSize = 10;
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


        public static WorldButton2D Create(Vector2 position, Vector2 size, Sprite sprite, WorldButton2D baseWorldButton2D, Text text, string name)
        {
            // making the Gameobject
            GameObject buttonGameObject = new GameObject(name);
            buttonGameObject.transform.position = (Vector3)position + new Vector3(0, 0, 1);
            buttonGameObject.transform.localScale = size;

            // SpriteRenderer
            SpriteRenderer buttonSpriteRenderer = buttonGameObject.AddComponent<SpriteRenderer>();
            if (sprite == null)
            {
                buttonSpriteRenderer.sprite = AssetReferences.Instance.SinglePixelSprite;
                buttonSpriteRenderer.color = Color.HSVToRGB(1f / 3f, 0.7f, 0.7f);
            }
            else
            {
                buttonSpriteRenderer.sprite = sprite;
                buttonSpriteRenderer.color = Color.white;
            }

            // Button
            WorldButton2D buttonWorldButton2D = buttonGameObject.AddComponent<WorldButton2D>();
            buttonWorldButton2D.buttonSpriteRenderer = buttonSpriteRenderer;
            buttonWorldButton2D.renderType = RenderType.SpriteRenderer;

            if (baseWorldButton2D == null)
            {
                buttonWorldButton2D.colorVisualization = ColorVisualization.SetColor;
                buttonWorldButton2D.defualtColor = Color.HSVToRGB(1f / 3f, 0.7f, 0.7f);
                buttonWorldButton2D.mouseOverUIColor = Color.HSVToRGB(1f / 3f, 0.7f, 0.65f);
                buttonWorldButton2D.holdingMouseDownOverUIColor = Color.HSVToRGB(1f / 3f, 0.7f, 0.5f);
            }
            else
            {
                buttonWorldButton2D.OnMouseEnterUI = baseWorldButton2D.OnMouseEnterUI;
                buttonWorldButton2D.OnMouseExitUI = baseWorldButton2D.OnMouseExitUI;
                buttonWorldButton2D.OnMouseStartClickUI = baseWorldButton2D.OnMouseStartClickUI;
                buttonWorldButton2D.OnMouseEndClickUI = baseWorldButton2D.OnMouseEndClickUI;

                buttonWorldButton2D.colorVisualization = baseWorldButton2D.colorVisualization;
                buttonWorldButton2D.defualtColor = baseWorldButton2D.defualtColor;
                buttonWorldButton2D.mouseOverUIColor = baseWorldButton2D.mouseOverUIColor;
                buttonWorldButton2D.holdingMouseDownOverUIColor = baseWorldButton2D.holdingMouseDownOverUIColor;
            }

            BoxCollider2D buttonBoxCollider2D = buttonGameObject.AddComponent<BoxCollider2D>();
            buttonBoxCollider2D.isTrigger = true;


            // Making the Text
            if (text != null)
            {
                GameObject textGameObject = new GameObject(text.name);
                textGameObject.transform.parent = buttonGameObject.transform;

                RectTransform textRectTransfrom = textGameObject.AddComponent<RectTransform>();
                TextMeshPro textTextMeshPro = textGameObject.AddComponent<TextMeshPro>();

                textTextMeshPro.enableWordWrapping = text.woodWrap;
                textTextMeshPro.fontSize = text.fontSize;
                textTextMeshPro.text = text.text;
                textTextMeshPro.alignment = text.alignment;
                textTextMeshPro.color = text.textColor;
                if (text.fontAsset != null)
                {
                    textTextMeshPro.font = text.fontAsset;
                }
                if (text.fontMaterial != null)
                {
                    textTextMeshPro.fontMaterial = text.fontMaterial;
                }

                textRectTransfrom.SetGlobalScale(text.scale);
                textRectTransfrom.sizeDelta = text.size;
                textRectTransfrom.localPosition = text.position;
            }


            return buttonWorldButton2D;

        }
        #region Create
        public static WorldButton2D Create(Vector2 position, Vector2 size, Sprite sprite, WorldButton2D baseWorldButton2D, Text text)
        {
            return Create(position, size, sprite, baseWorldButton2D, text, "World Button 2D");
        }
        public static WorldButton2D Create(Vector2 position, Vector2 size, Sprite sprite, WorldButton2D baseWorldButton2D, string name)
        {
            return Create(position, size, sprite, baseWorldButton2D, null, name);
        }
        public static WorldButton2D Create(Vector2 position, Vector2 size, Sprite sprite, WorldButton2D baseWorldButton2D)
        {
            return Create(position, size, sprite, baseWorldButton2D, null, "World Button 2D");
        }
        public static WorldButton2D Create(Vector2 position, Vector2 size, Sprite sprite, Text text, string name)
        {
            return Create(position, size, sprite, null, text, name);
        }
        public static WorldButton2D Create(Vector2 position, Vector2 size, Sprite sprite, Text text)
        {
            return Create(position, size, sprite, null, text, "World Button 2D");
        }
        public static WorldButton2D Create(Vector2 position, Vector2 size, Sprite sprite, string name)
        {
            return Create(position, size, sprite, null, null, name);
        }
        public static WorldButton2D Create(Vector2 position, Vector2 size, Sprite sprite)
        {
            return Create(position, size, sprite, null, null, "World Button 2D");
        }
        public static WorldButton2D Create(Vector2 position, Vector2 size, WorldButton2D baseUIButton, Text text, string name)
        {
            return Create(position, size, null, baseUIButton, text, name);
        }
        public static WorldButton2D Create(Vector2 position, Vector2 size, WorldButton2D baseUIButton, Text text)
        {
            return Create(position, size, null, baseUIButton, text, "World Button 2D");
        }
        public static WorldButton2D Create(Vector2 position, Vector2 size, WorldButton2D baseUIButton, string name)
        {
            return Create(position, size, null, baseUIButton, null, name);
        }
        public static WorldButton2D Create(Vector2 position, Vector2 size, WorldButton2D baseUIButton)
        {
            return Create(position, size, null, baseUIButton, null, "World Button 2D");
        }
        public static WorldButton2D Create(Vector2 position, Vector2 size, Text text, string name)
        {
            return Create(position, size, null, null, text, name);
        }
        public static WorldButton2D Create(Vector2 position, Vector2 size, Text text)
        {
            return Create(position, size, null, null, text, "World Button 2D");
        }
        public static WorldButton2D Create(Vector2 position, Vector2 size, string name)
        {
            return Create(position, size, null, null, null, name);
        }
        public static WorldButton2D Create(Vector2 position, Vector2 size)
        {
            return Create(position, size, null, null, null, "World Button 2D");
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
            SpriteRenderer,
            MeshRenderer,
        }

        #endregion


        #region Variables

        [ReadOnlyText(height = 110, yOffset = 40), SerializeField]
        private string textLable1 = "Make sure this GameObject is +1 unit on the Z.";

#if ODIN_INSPECTOR
        #region ODIN_INSPECTOR
        public event Action OnMouseEnterUI;
        public event Action OnMouseExitUI;
        public event Action OnMouseStartClickUI;
        public event Action OnMouseEndClickUI;


        [Space(10), Header("Renderer")]
        public RenderType renderType;
        [ShowIf("@renderType == RenderType.SpriteRenderer")]
        public SpriteRenderer buttonSpriteRenderer;
        [ShowIf("@renderType == RenderType.MeshRenderer")]
        public MeshRenderer buttonMeshRenderer;


        [Header("ColorVisualization")]
        [EnumToggleButtons()]
        public ColorVisualization colorVisualization;
        [HideIf("@colorVisualization == ColorVisualization.None")]
        public Color defualtColor = Color.white;
        [HideIf("@colorVisualization == ColorVisualization.None")]
        public Color mouseOverUIColor = Color.white;
        [HideIf("@colorVisualization == ColorVisualization.None")]
        public Color holdingMouseDownOverUIColor = Color.white;
        #endregion
#else
        #region NOT ODIN_INSPECTOR

        public event Action OnMouseEnterUI;
        public event Action OnMouseExitUI;
        public event Action OnMouseStartClickUI;
        public event Action OnMouseEndClickUI;


        [Space(10), Header("Renderer")]
        public RenderType renderType;
        public SpriteRenderer buttonSpriteRenderer;
        public MeshRenderer buttonMeshRenderer;


        [Header("ColorVisualization")]
        public ColorVisualization colorVisualization;
        public Color defualtColor = Color.white;
        public Color mouseOverUIColor = Color.white;
        public Color holdingMouseDownOverUIColor = Color.white;

        #endregion
#endif

        [Header("Other")]
        [SerializeField] private TextMeshProUGUI text;
        private Material startMaterial;


        #region Mouse State Variables

        private bool isMouseUnobstructedOverButton;
        private bool isMouseUnobstructedOverButtonLate;
        private bool isMouseOverButton;

        #endregion

        #endregion


        #region Unity Functions

        private void Awake()
        {
            if (buttonMeshRenderer == null)
            {
                buttonMeshRenderer = GetComponent<MeshRenderer>();
            }
            if (buttonSpriteRenderer == null)
            {
                buttonSpriteRenderer = GetComponent<SpriteRenderer>();
            }
            if (text == null)
            {
                if (!TryGetComponent(out text))
                {
                    text = GetComponentInChildren<TextMeshProUGUI>();
                }
            }

            if (buttonMeshRenderer != null)
            {
                startMaterial = buttonMeshRenderer.material;
            }
        }

        private void Update()
        {
            if (isMouseOverButton)
            {
                if (Mouse2D.TryGetObjectAtMousePosition(out GameObject hit) && hit == gameObject)
                {
                    isMouseUnobstructedOverButton = true;
                }
                else
                {
                    isMouseUnobstructedOverButton = false;
                }
            }

            if (isMouseUnobstructedOverButton && !isMouseUnobstructedOverButtonLate)
            {
                // Mouse just Entered Button
                isMouseUnobstructedOverButtonLate = true;
                OnMouseEnterUI?.Invoke();

                SetColor_MouseOverUIColor();
            }
            else if (!isMouseUnobstructedOverButton && isMouseUnobstructedOverButtonLate)
            {
                // Mouse just Exited Button
                isMouseUnobstructedOverButtonLate = false;
                OnMouseExitUI?.Invoke();

                SetColor_DefualtColor();
            }
        }

        #endregion


        #region Checking Mouse

        private void OnMouseEnter()
        {
            isMouseOverButton = true;
        }

        private void OnMouseDown()
        {
            if (isMouseUnobstructedOverButton)
            {
                OnMouseStartClickUI?.Invoke();

                SetColor_HoldingMouseDownOverUIColor();
            }
        }

        private void OnMouseUpAsButton()
        {
            if (isMouseUnobstructedOverButton)
            {
                OnMouseEndClickUI?.Invoke();

                SetColor_MouseOverUIColor();
            }
        }

        private void OnMouseExit()
        {
            isMouseOverButton = false;
            isMouseUnobstructedOverButton = false;
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
                case RenderType.SpriteRenderer:
                    buttonSpriteRenderer.color = color;
                    break;
                case RenderType.MeshRenderer:
                    startMaterial.color = color;
                    break;
            }
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
        private void SetColor_DefualtColor()
        {
            if (renderType == RenderType.None || colorVisualization == ColorVisualization.None) return;

            SetColor(defualtColor);
        }

        /// <summary>
        /// sets the color of the renderer to be the MouseOverUI color.
        /// </summary>
        private void SetColor_MouseOverUIColor()
        {
            if (renderType == RenderType.None || colorVisualization == ColorVisualization.None) return;

            SetColor(mouseOverUIColor);
        }

        /// <summary>
        /// sets the color of the renderer to be the HoldingMouseDownOverUI color.
        /// </summary>
        private void SetColor_HoldingMouseDownOverUIColor()
        {
            if (renderType == RenderType.None || colorVisualization == ColorVisualization.None) return;

            SetColor(holdingMouseDownOverUIColor);
        }

        #endregion


    }
}
