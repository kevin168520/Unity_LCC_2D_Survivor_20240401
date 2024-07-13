using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameManager : MonoBehaviour
{
    private Button btnStart, btnQuit;
    private string gameSceneName = "遊戲場景";

    private void Awake()
    {
        btnStart = GameObject.Find("按鈕開始遊戲").GetComponent<Button>();
        btnQuit  = GameObject.Find("按鈕離開遊戲").GetComponent <Button>();

        btnStart.onClick.AddListener(StartGame);
        btnQuit.onClick.AddListener(QuitGame);
    }

    private void QuitGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    private void StartGame()
    {
        Application.Quit();
    }
}
