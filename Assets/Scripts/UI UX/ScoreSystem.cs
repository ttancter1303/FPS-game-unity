using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private GameObject portalGate;
    [SerializeField] private TMP_Text ScoreText;
    
    public static ScoreSystem Instance;
    private int Score = 0;
    public int totalScore = 10;
    
    private void Awake()
    {
        portalGate.SetActive(false);
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
        portalGate.SetActive(true);
    }
}
