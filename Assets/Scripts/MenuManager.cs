using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextAsset scoreData;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
