using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    [SerializeField]
    private float countdownDuration = 3f;
    [SerializeField]
    private float gameDuration = 30f;
    [SerializeField]
    private Text countdownText;
    [SerializeField]
    private Text timerText;
    [SerializeField]
    private Text winnerText;
    [SerializeField]
    private GameObject winnerPanel;
    [SerializeField]
    private Image winnerFade;

    private GridManager grid;
    private float timeRemaining;
    private bool paused = true;

    void Awake()
    {
        grid = FindObjectOfType<GridManager>();
        countdownText.enabled = false;
        timerText.enabled = false;
        winnerText.enabled = false;
        winnerPanel.SetActive(false);
        winnerFade.enabled = false;
    }

    private void Start()
    {
        StartCountdown();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: check if game is online or local
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // TODO: pause menu, block player input
            TogglePause();
        }

        if (!paused)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0f)
            {
                paused = true;
                DisplayWinners();
                // TODO: pause player input, go back to lobby
            }
        }

        timerText.text = Mathf.Ceil(timeRemaining).ToString();
    }

    public void StartCountdown()
    {
        // TODO: disable player input until after countdown
        StartCoroutine(CountdownRoutine());
    }

    IEnumerator CountdownRoutine()
    {
        countdownText.enabled = true;

        float countdownTime = countdownDuration;
        while (countdownTime > 0f)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime -= 1f;
        }
        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);

        countdownText.enabled = false;
        StartTimer();
    }

    public void StartTimer()
    {
        timeRemaining = gameDuration;
        paused = false;
        timerText.text = timeRemaining.ToString();
        timerText.enabled = true;
    }

    public void TogglePause()
    {
        paused = !paused;
    }

    Dictionary<int, string> positions = new Dictionary<int, string>
    {
        {0, "1st"},
        {1, "2nd"},
        {2, "3rd"},
        {3, "4th"}
    };

    public void DisplayWinners()
    {
        List<int> winners = grid.GetWinnerOrder();

        winnerPanel.SetActive(true);
        for (int ii = 0; ii < 4; ii++)
        {
            Text txt = winnerPanel.transform.GetChild(ii).GetComponent<Text>();
            string position = positions[ii];
            txt.text = position + " - Player " + winners[ii];
            txt.color = PlayerColorManager.GetRGBAColor(winners[ii]);
        }

        winnerText.text = "Player " + winners[0] + " Wins!";
        winnerText.color = PlayerColorManager.GetRGBAColor(winners[0]);

        winnerText.enabled = true;
        winnerFade.enabled = true;
    }
}
