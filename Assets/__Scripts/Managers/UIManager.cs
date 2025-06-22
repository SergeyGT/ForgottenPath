using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Configuration")]
    [SerializeField] private List<UIContainer> uiContainers;
    private Dictionary<string, CanvasGroup> uiElements;

    [System.Serializable]
    public class UIContainer
    {
        public string key;
        public CanvasGroup element;
    }

    private float _fadeDuration = 1.5f;

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
}