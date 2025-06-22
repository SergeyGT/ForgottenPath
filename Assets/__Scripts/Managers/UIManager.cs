using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Configuration")]
    [SerializeField] private Dictionary<string, GameObject> uiElements;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("Attempt to create second UIManager");
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        uiElements = new Dictionary<string, GameObject>();
    }

    
    private void Update()
    {
        
    }

    public void NoBatteriesInfo()
    {
        uiElements["batteryEnded"].SetActive(true);
    }
}
