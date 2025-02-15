using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int playerScore = 0;
    [SerializeField] private TextMeshProUGUI tmpScore;
    [SerializeField] private GameObject[] health;
    private int currHealthInd;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }
        currHealthInd = health.Length - 1;
    }
    
    public void AddScore(int score)
    {
        playerScore += score;
        tmpScore.text = "Score: " + playerScore;
    }

    public bool PlayerTookDamage()
    {
        health[currHealthInd--].SetActive(false);

        if (currHealthInd < 0) {
            return true;
        }
        
        return false;
    }
}