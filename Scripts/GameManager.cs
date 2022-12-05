using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private PlayerTypeLeaderboard playerTypeLeaderboard;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private TMP_Text killScoreText;
    private GameObject player;
    private int waveNumber = 0;
    [SerializeField] private TMP_Text waveText;
    private int killScore = 0;
    private float previousTimeScale;
    [SerializeField] private int waveDuration;
    private int timeRemaining;
    [SerializeField] private UnityEvent OnWaveEnd;
    [SerializeField] TMP_Text timerText;
    [SerializeField] GameObject deathMenu;
    [SerializeField] Transform spawners;
    [SerializeField] TMP_Text waveAnnouncer;
    private AudioManager audioManager;

    public enum GameType { Regular, Endless }
    private GameType gameType;
    private bool canPlayMainTheme = true;


    public int WaveNumber
    {
        get { return waveNumber; }
        set 
        { 
            waveNumber = value;
            waveAnnouncer.text = $"Wave {waveNumber}";
            waveText.text = $"Wave {waveNumber}";
        }
    }
    public int KillScore
    { 
        get { return killScore; } 
        set 
        {
            killScore = value; 
            killScoreText.text = killScore.ToString();
            TenToOne();
        } 
    }

    public int TimeRemaining
    {
        get { return timeRemaining; }
        set 
        { 
            timeRemaining = value; 
            timerText.text = timeRemaining.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTypeLeaderboard = FindObjectOfType<PlayerTypeLeaderboard>();
        audioManager = FindObjectOfType<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        previousTimeScale = Time.timeScale;
        OnWaveStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pauseMenu.activeSelf)
                {
                    pauseMenu.SetActive(false);
                    ResumeGame();
                }
                else
                {
                    pauseMenu.SetActive(true);
                    PauseGame();
                }
            }
        }
        if (spawners.gameObject.activeSelf && canPlayMainTheme)
        {
            StartCoroutine(LoopMainTheme());
        }
    }

    public void PauseGame()
    {
        previousTimeScale = Time.timeScale;
        Time.timeScale = 0f;
    }
    
    public void ResumeGame()
    {
        Time.timeScale = previousTimeScale;
    }

    public void EndGame()
    {
        OnWaveEnd?.Invoke();
        deathMenu.SetActive(true);
        playerTypeLeaderboard.UploadScore(killScore);
    }

    private void TenToOne()
    {
        player.TryGetComponent<TenToOne>(out TenToOne component);
        if (component != null)
        {
            component.CheckKillCount();
        }
    }

    public void OnWaveStart()
    {
        audioManager.Stop("ShopTheme");
        WaveNumber++;
        CheckWaveState();
        TimeRemaining = waveDuration;
        ApplySpawnBuff();
        StartCoroutine(StartTimer());
        waveAnnouncer.GetComponent<Animator>().Play("WaveAnnouncement");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        ResumeGame();
    }

    public void QuitGame()
    {
        ResumeGame();
        Application.Quit();
        LoadMainMenu();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        killScore = 0;
        waveNumber = 0;
    }

    private void CheckWaveState()
    {
        if (waveNumber == 1)
        {
            spawners.GetChild(2).gameObject.SetActive(true);
        }

        else if (waveNumber == 2)
        {
            spawners.GetChild(3).gameObject.SetActive(true);
        }
        else if (waveNumber == 3)
        {
            spawners.GetChild(4).gameObject.SetActive(true);
        }
        else if (waveNumber == 4)
        {
            spawners.GetChild(5).gameObject.SetActive(true);
        }
    }

    private void ApplySpawnBuff()
    {
        foreach (Transform spawn in spawners)
        {
            spawn.GetComponent<MobSpawner>().SpawnDelay *= SpawnRateBuff(waveNumber);
            player.TryGetComponent<BabyBoom>(out BabyBoom component);
            if (component != null)
            {
                spawn.GetComponent<MobSpawner>().SpawnDelay *= component.ApplySpawnRateBuff();
            }
        }
    }

    private float SpawnRateBuff(int level)
    {
        return 4 * Mathf.Exp(- (level + 3.5f) / 3) ;
    }

    public void SetGameType(GameManager.GameType type)
    {
        gameType = type;
    }

    IEnumerator StartTimer()
    {
        if (TimeRemaining > 0)
        {
            yield return new WaitForSeconds(1);
            TimeRemaining -= 1;
            StartCoroutine(StartTimer());
        }
        else
        {
            timerText.text = "Shopping time !";
            OnWaveEnd?.Invoke();
            audioManager.Play("ShopTheme");
            audioManager.Stop("MainTheme");
            yield return null;
        }
    }

    IEnumerator LoopMainTheme()
    {
        canPlayMainTheme = false;
        audioManager.Play("MainTheme");
        yield return new WaitForSecondsRealtime(35.5f);
        canPlayMainTheme = true;
    }
}
