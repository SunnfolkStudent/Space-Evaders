using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;
    [SerializeField] private GameObject newHighScore;
    [SerializeField] private TextMeshProUGUI displayScore;
    private bool _newHighScoreFlag;


    private void Awake()
    {
        endScreen.SetActive(false);
        newHighScore.SetActive(false);
    }

    public void NewHighScore()
    {
        newHighScore.SetActive(true);
    }
    
    public void GameOver(int score)
    {
        displayScore.text = "Score: " + score;
        endScreen.SetActive(true);
    }
    
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
