using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Configuration")]
    [SerializeField] private List<UIContainer> uiContainers;
    private Dictionary<string, CanvasGroup> uiElements;

    [Header("Eyelids Configuration")]
    [SerializeField] private float eyelidsMoveDuration = 0.5f;
    [SerializeField] private float eyelidsStayClosedDuration = 1f;

    private float _fadeDuration = 1.5f;
    private bool _eyesClosing = false;
    private string _currentMessage;
    private RectTransform _upperEyelid;
    private RectTransform _lowerEyelid;
    private Vector2 _upperEyelidOpenPos;
    private Vector2 _lowerEyelidOpenPos;

    [System.Serializable]
    public class UIContainer
    {
        public string key;
        public CanvasGroup element;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeDictionary();
        InitializeEyelids();
    }

    private void InitializeEyelids()
    {
        if (uiElements.TryGetValue("UpperEyelid", out CanvasGroup upperEyelidGroup) &&
            uiElements.TryGetValue("LowerEyelid", out CanvasGroup lowerEyelidGroup))
        {
            _upperEyelid = upperEyelidGroup.GetComponent<RectTransform>();
            _lowerEyelid = lowerEyelidGroup.GetComponent<RectTransform>();

            if (_upperEyelid != null && _lowerEyelid != null)
            {
                _upperEyelidOpenPos = _upperEyelid.anchoredPosition;
                _lowerEyelidOpenPos = _lowerEyelid.anchoredPosition;

                upperEyelidGroup.gameObject.SetActive(false);
                lowerEyelidGroup.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("Eyelids not found in UI containers. Please add 'UpperEyelid' and 'LowerEyelid' to uiContainers.");
        }
    }

    private void InitializeDictionary()
    {
        uiElements = new Dictionary<string, CanvasGroup>();
        foreach (var container in uiContainers)
        {
            if (!uiElements.ContainsKey(container.key))
            {
                uiElements.Add(container.key, container.element);
            }
            else
            {
                Debug.LogError($"Duplicate UI key found: {container.key}");
            }
        }
    }

    public void ShowElement(string elementKey)
    {
        if (uiElements.TryGetValue(elementKey, out CanvasGroup element))
        {
            element.alpha = 0.3f;
            element.gameObject.SetActive(true);
            LeanTween.alphaCanvas(element, 1f, _fadeDuration);
        }
        else
        {
            Debug.LogError($"UI element with key {elementKey} not found!");
        }
    }

    public void HideElement(string elementKey)
    {
        if (uiElements.TryGetValue(elementKey, out CanvasGroup element))
        {
            LeanTween.alphaCanvas(element, 0f, _fadeDuration)
                .setOnComplete(() => element.gameObject.SetActive(false));
        }
    }

    public void StartClosingEyes()
    {
        if (_eyesClosing) return;

        _eyesClosing = true;

        // Активируем веки
        if (uiElements.TryGetValue("UpperEyelid", out CanvasGroup upperEyelidGroup))
            upperEyelidGroup.gameObject.SetActive(true);
        if (uiElements.TryGetValue("LowerEyelid", out CanvasGroup lowerEyelidGroup))
            lowerEyelidGroup.gameObject.SetActive(true);

        // Рассчитываем позиции для закрытых век
        float closeOffset = Screen.height * 0.5f;
        Vector2 upperClosedPos = _upperEyelidOpenPos + new Vector2(0, -closeOffset);
        Vector2 lowerClosedPos = _lowerEyelidOpenPos + new Vector2(0, closeOffset);

        // Анимация закрытия
        LeanTween.move(_upperEyelid, upperClosedPos, eyelidsMoveDuration)
            .setEase(LeanTweenType.easeInOutQuad);

        LeanTween.move(_lowerEyelid, lowerClosedPos, eyelidsMoveDuration)
            .setEase(LeanTweenType.easeInOutQuad)
            .setOnComplete(() => {
             ShowElement("inventory");

                //LeanTween.delayedCall(eyelidsStayClosedDuration, () => {
                //    if (!Input.GetKey(KeyCode.Tab))
                //    {
                //        StartOpeningEyes();
                //    }
                //});
            });
    }

    public void StartOpeningEyes()
    {
        if (!_eyesClosing) return;

        HideElement("inventory");

        LeanTween.move(_upperEyelid, _upperEyelidOpenPos, eyelidsMoveDuration)
            .setEase(LeanTweenType.easeInOutQuad);

        LeanTween.move(_lowerEyelid, _lowerEyelidOpenPos, eyelidsMoveDuration)
            .setEase(LeanTweenType.easeInOutQuad)
            .setOnComplete(() => {
                if (uiElements.TryGetValue("UpperEyelid", out CanvasGroup upperEyelidGroup))
                    upperEyelidGroup.gameObject.SetActive(false);
                if (uiElements.TryGetValue("LowerEyelid", out CanvasGroup lowerEyelidGroup))
                    lowerEyelidGroup.gameObject.SetActive(false);
                _eyesClosing = false;
            });

    }
}