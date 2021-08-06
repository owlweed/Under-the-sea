using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Ready,
        Run,
        Pause,
        GameOver,
        MissionClear
    }

    public GameState gState;

    public Text stateLabel;

    GameObject player;
    GameObject Score;

    PlayerMove playerM;

    ScoreManager scoreM;

    public static GameManager gm;

    public GameObject optionUI;

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
    }
    void Start()
    {
        gState = GameState.Ready;

        StartCoroutine(GameStart());

        player = GameObject.Find("Player");
        Score = GameObject.Find("ScoreManager");

        playerM = player.GetComponent<PlayerMove>();
        scoreM = Score.GetComponent<ScoreManager>();

        CloseOptionWindow();
    }

    IEnumerator GameStart()
    {
        stateLabel.text = "Ready...";

        stateLabel.color = new Color32(233, 182, 12, 255);

        yield return new WaitForSeconds(2.0f);

        stateLabel.text = "Go!";

        yield return new WaitForSeconds(0.5f);

        stateLabel.text = "";

        gState = GameState.Run;
    }

    void Update()
    {
        if (playerM.hp <= 0)
        {
            stateLabel.text = "Game Over";

            stateLabel.color = new Color32(255, 0, 0, 255);

            gState = GameState.GameOver;

            SceneManager.LoadScene("BadEndingScene");
        }

        if (scoreM.Score == 150) // 보석 수집 15개 이상
        {
            stateLabel.text = "Mission Clear";

            stateLabel.color = new Color32(36, 70, 231, 255);

            gState = GameState.MissionClear;

            SceneManager.LoadScene("HappyEndingScene");
        }
    }

    public void OpenOptionWindow()
    {
        gState = GameState.Pause;

        Time.timeScale = 0;

        optionUI.SetActive(true);
    }

    public void CloseOptionWindow()
    {
        gState = GameState.Run;

        Time.timeScale = 1.0f;

        optionUI.SetActive(false);
    }

    public void GameRestart()
    {
        Time.timeScale = 1.0f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameQuit()
    {
        SceneManager.LoadScene("GameTitle");
    }
}
