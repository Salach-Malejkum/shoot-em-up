using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int playerScore = 0;
    [SerializeField] private TextMeshProUGUI tmpScore;
    [SerializeField] private GameObject[] healthList;
    private int currHealthInd;
    [SerializeField] private TextMeshProUGUI lostGame;
    [SerializeField] private bool isLost = false;

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
        currHealthInd = healthList.Length - 1;
        lostGame.enabled = false;
    }

    private void Update()
    {
        if (isLost)
        {
            Color textColor = lostGame.color;
            textColor.a = Mathf.PingPong(Time.time, 1f);
            lostGame.color = textColor;
        }
    }

    public void AddScore(int score)
    {
        playerScore += score;
        tmpScore.text = "Score: " + playerScore;
    }

    public bool PlayerTookDamage()
    {
        healthList[currHealthInd--].SetActive(false);

        if (currHealthInd < 0)
        {
            isLost = true;
            lostGame.enabled = true;
            return true;
        }
        
        return false;
    }

    private void OnRestart() 
    {
        if (isLost)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }
}