using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //[SerializeField] private GameObject optionsMenu;

    public void OnStartButton()
    {
        SceneManager.LoadScene("Game Scene"); 
    }


    //public void OnOptionsButton()
    //{
    //    optionsMenu.SetActive(true);
    //    gameObject.SetActive(false); // Hide main menu when options open
    //}

    public void OnQuitButton()
    {
        Application.Quit();
        Debug.Log("Quit Game"); // Will only show in Editor
    }
}
