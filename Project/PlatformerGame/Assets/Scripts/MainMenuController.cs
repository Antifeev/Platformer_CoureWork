using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        // Загружаем следующую сцену по списку
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Выход из игры!");
        Application.Quit(); // Эта команда закроет игру
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Загружаем сцену по имени
    }
}