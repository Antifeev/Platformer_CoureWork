using UnityEngine;
using UnityEngine.SceneManagement; // Библиотека для управления сценами

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        // Загружаем следующую сцену по списку (это будет наша игра)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Выход из игры!");
        Application.Quit(); // Эта команда закроет игру (работает только в готовом .exe)
    }
    // Эту функцию мы повесим на кнопку "МЕНЮ" в конце игры
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Загружаем сцену по имени
    }
}