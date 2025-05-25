using TMPro;
using UnityEngine;
public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] portalGates;
    [SerializeField] private TMP_Text ScoreText;
    
    public static ScoreSystem Instance;
    private int Score = 0;
    public int totalScore = 10;
    
    private void Awake()
    {
        HidePortalGate();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void AddScore(int amount)
    {
        Score += amount;
        Debug.Log("Score: " + Score);
        ScoreText.text = Score + " / " + totalScore;
        
        if (Score >= totalScore)
        {
            ShowPortalGate();
        }
    }

    public void ShowPortalGate()
    {
        for (var i = 0; i < portalGates.Length; i++)
        {
            portalGates[i].SetActive(true);
        }
    }

    public void HidePortalGate()
    {
        for (var i = 0; i < portalGates.Length; i++)
        {
            portalGates[i].SetActive(false);
        }
    }
}
