using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject gameUI; // Reference to the "UI" canvas
    [SerializeField] private string mainMenuSceneName = "Start Menu";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        if (gameUI != null) gameUI.SetActive(true);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        if (gameUI != null) gameUI.SetActive(false);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void OnResumeButton()
    {
        Resume();
    }

    public void OnOptionsButton()
    {
        Debug.Log("Options clicked - not implemented yet");
    }

    public void OnQuitButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
