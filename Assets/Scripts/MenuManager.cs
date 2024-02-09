using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextAsset scoreData;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }
    
    public void StartGame()
    {
        ReadWriteScore.CreatePersistentCopy(scoreData);
        SceneManager.LoadScene("GameScene");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
