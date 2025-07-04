using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Configuration")]
    [SerializeField] private List<UIContainer> uiContainers;
    private Dictionary<string, CanvasGroup> uiElements;

    [Header("Eyelids Configuration")]
    [SerializeField] private float eyelidsMoveDuration = 0.5f;
    [SerializeField] private float infoDisplayDelay = 0.1f; // Задержка перед показом информации после закрытия
    [SerializeField] private float infoDisplayDuration = 3f; // Время отображения информации

    [Header("InventoryMenu Configuration")]
    [SerializeField] private TextMeshProUGUI batteriesCount;

    private float _fadeDuration = 2f;
    private bool _eyesClosing = false;
    private bool _eyesClosed = false;
    private RectTransform _upperEyelid;
    private RectTransform _lowerEyelid;
    private Vector2 _upperEyelidOpenPos;
    private Vector2 _lowerEyelidOpenPos;
    private Coroutine _eyesClosedCoroutine;
    private LTDescr _upperEyelidTween;
    private LTDescr _lowerEyelidTween;

    private const string TEXT_COUNT_BATTERIES = "Count batteries: ";

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

    private void Start()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        batteriesCount.SetText(TEXT_COUNT_BATTERIES + "0");
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
            element.alpha = 0f;
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

        // Отменяем все текущие анимации
        if (_upperEyelidTween != null) LeanTween.cancel(_upperEyelid.gameObject);
        if (_lowerEyelidTween != null) LeanTween.cancel(_lowerEyelid.gameObject);
        if (_eyesClosedCoroutine != null) StopCoroutine(_eyesClosedCoroutine);

        _eyesClosing = true;
        _eyesClosed = false;

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
        _upperEyelidTween = LeanTween.move(_upperEyelid, upperClosedPos, eyelidsMoveDuration)
            .setEase(LeanTweenType.easeInOutQuad);

        _lowerEyelidTween = LeanTween.move(_lowerEyelid, lowerClosedPos, eyelidsMoveDuration)
            .setEase(LeanTweenType.easeInOutQuad)
            .setOnComplete(() => {
                _eyesClosed = true;
                // Запускаем корутину для отображения информации
                _eyesClosedCoroutine = StartCoroutine(EyesClosedRoutine());
            });
    }

    public void StartOpeningEyes()
    {
        if (!_eyesClosing) return;

        // Отменяем все текущие анимации и корутины
        if (_upperEyelidTween != null) LeanTween.cancel(_upperEyelid.gameObject);
        if (_lowerEyelidTween != null) LeanTween.cancel(_lowerEyelid.gameObject);
        if (_eyesClosedCoroutine != null) StopCoroutine(_eyesClosedCoroutine);

        // Скрываем информацию сразу при начале открытия глаз
        HideElement("inventory");

        // Анимация открытия
        _upperEyelidTween = LeanTween.move(_upperEyelid, _upperEyelidOpenPos, eyelidsMoveDuration)
            .setEase(LeanTweenType.easeInOutQuad);

        _lowerEyelidTween = LeanTween.move(_lowerEyelid, _lowerEyelidOpenPos, eyelidsMoveDuration)
            .setEase(LeanTweenType.easeInOutQuad)
            .setOnComplete(() => {
                if (uiElements.TryGetValue("UpperEyelid", out CanvasGroup upperEyelidGroup))
                    upperEyelidGroup.gameObject.SetActive(false);
                if (uiElements.TryGetValue("LowerEyelid", out CanvasGroup lowerEyelidGroup))
                    lowerEyelidGroup.gameObject.SetActive(false);
                _eyesClosing = false;
                _eyesClosed = false;
            });
    }

    private IEnumerator EyesClosedRoutine()
    {
        // Ждем полного закрытия глаз + небольшую задержку
        yield return new WaitForSeconds(infoDisplayDelay);

        // Показываем информацию только если глаза остаются закрытыми
        if (_eyesClosed)
        {
            ShowElement("inventory");

            // Ждем указанное время перед скрытием информации
            yield return new WaitForSeconds(infoDisplayDuration);

            // Скрываем информацию только если глаза остаются закрытыми
            if (_eyesClosed)
            {
                HideElement("inventory");
            }
        }
    }

    public void UpdateInventory()
    {
        string countBatteries = Inventory.Instance.GetBatteries().ToString();
        batteriesCount.SetText(TEXT_COUNT_BATTERIES + countBatteries);
    }

    public void UpdateInventory(int keyUpdate)
    {
        //Key key = Inventory.Instance.GetKey(keyUpdate);
    }
}