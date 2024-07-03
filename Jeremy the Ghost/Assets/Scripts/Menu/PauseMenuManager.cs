using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public bool Paused { get; private set; } = false;
    public static PauseMenuManager Instance { get; private set; }
    [SerializeField] private GameObject _pauseMenu;
    
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
    }

    private void Update()
    {
        TryPauseOrUnpause();
        
    }

    /// <summary>
    /// Checks if the escape key has been pressed and (un)pauses based on that.
    /// </summary>
    private void TryPauseOrUnpause()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (Paused)
        {
            Unpause();
        }
        else
        {
            Pause();
        }
    }

    private void Pause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
        Paused = true;
    }

    private void Unpause()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Paused = false;
    }

    /// <summary>
    /// OnClick function for RestartLevel button in the Pause Menu.
    /// </summary>
    public void RestartLevel()
    {
        Unpause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// OnClick function for MainMenu button in the Pause Menu.
    /// </summary>
    public void ReturnToMainMenu()
    {
        Unpause();
        SceneManager.LoadScene("Menu");
    }
    
    /// <summary>
    /// OnClick function for Close button in the Pause Menu.
    /// </summary>
    public void Close()
    {
        Unpause();
    }
}
