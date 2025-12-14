using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip gameOverSFX;
    public AudioClip pauseSFX;
    public AudioClip quitSFX;

    [Header("Button References")]
    public Button startButton;
    public Button settingsButton;
    public Button quitButton;
    public Button backSettingsButton;
    public Button resumeButton;
    public Button returnToMenuButton;

    [Header("HUD References")]
    public TMP_Text livesText;

    [Header("Menu References")]
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject pauseMenu;
    public GameObject gameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();

        if (startButton)
            startButton.onClick.AddListener(() =>
            {
                GameManager.Instance.lives = 5;
                SceneManager.LoadScene(1);
                });

        if (settingsButton)
            settingsButton.onClick.AddListener(() => SetMenus(settingsMenu, mainMenu));

        if (backSettingsButton)
            backSettingsButton.onClick.AddListener(() => SetMenus(mainMenu, settingsMenu));

        if (resumeButton)
            resumeButton.onClick.AddListener(() =>
            {
                SetMenus(null, pauseMenu);
                Time.timeScale = 1;
            });

        if (returnToMenuButton)
            returnToMenuButton.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(0);
            });


        if (quitButton)
            quitButton.onClick.AddListener(QuitGame);

        if (livesText)
        {
            livesText.text = $"Lives: {GameManager.Instance.lives}";
            GameManager.Instance.OnLifeValueChanged += (int newLives) => livesText.text = $"Lives: {newLives}";
        }
    }

    private void SetMenus(GameObject menuToEnable, GameObject menuToDisable)
    {
        if (menuToEnable) menuToEnable.SetActive(true);
        if (menuToDisable) menuToDisable.SetActive(false);
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
source.PlayOneShot(quitSFX);
            Application.Quit();
#endif
    }

    void OnEnable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnGameOver += showGameOver;
    }

    void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnGameOver -= showGameOver;
    }
    public void showGameOver()
    {
        source.PlayOneShot(gameOverSFX);
        SetMenus(gameOver, null);
        Time.timeScale = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu) return;

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pauseMenu.activeSelf)
            {
                SetMenus(null, pauseMenu);
                Time.timeScale = 1f;
                return;
            }
            source.PlayOneShot(pauseSFX);
            SetMenus(pauseMenu, null);
            Time.timeScale = 0f;
        }

    }
}
