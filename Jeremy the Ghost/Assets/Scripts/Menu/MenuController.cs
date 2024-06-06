using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles all actions done in the menu.
/// </summary>
public class MenuController : MonoBehaviour
{
    /// <summary>
    /// Gets called when the player presses the Play button in the menu.
    /// </summary>
    public void Play()
    {
        SceneManager.LoadScene("IntroLevel");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
