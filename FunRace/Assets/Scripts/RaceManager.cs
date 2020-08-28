using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

public class RaceManager : MonoBehaviour
{
    public float timeReady = 2.0f;
    public float timeStart = 3.0f;
    public float timeGo = 1.0f;
    public float timeEnd = 3.0f;
    public PlayerControls player;
    public TextMeshProUGUI raceText;

    private WaitForEndOfFrame waitForFrame;

    // Start is called before the first frame update
    void Start()
    {
        waitForFrame = new WaitForEndOfFrame();

        StartCoroutine(BeginRace());
    }

    // Update is called once per frame
    void Update()
    {
        if (player.t >= 0.99 && player.last == true)
        {
            StartCoroutine(EndRace());
        }

        if (player.collided)
        {
            StartCoroutine(RestartRace());
        }
    }

    private IEnumerator MyWaitForSeconds(float s)
    {
        float time = 0.0f;
        while (time <= s)
        {
            time += Time.unscaledDeltaTime;
            yield return waitForFrame;
        }
    }


    private IEnumerator BeginRace()
    {
        Time.timeScale = 1.0f;

        yield return MyWaitForSeconds(timeReady);

        float time = 0.0f;
        while (time <= timeStart)
        {
            if (time >= 2.0f)
            {
                raceText.text = ((int)(timeStart - time + 1)).ToString();
            }

            time += Time.unscaledDeltaTime;
            yield return waitForFrame;
        }

        raceText.text = "Go!";
        player.SetPlayerEnabled(true);

        yield return MyWaitForSeconds(timeGo);
        raceText.enabled = false;
    }

    private IEnumerator RestartRace()
    {
        Time.timeScale = 0.0f;
        raceText.enabled = true;
        raceText.text = "TryAgain!";
        player.SetPlayerEnabled(false);
        yield return MyWaitForSeconds(timeEnd);
        SceneManager.LoadScene(0);
    }

    private IEnumerator EndRace()
    {
        raceText.enabled = true;
        raceText.text = "You Win!";
        player.SetPlayerEnabled(false);
        yield return MyWaitForSeconds(timeEnd);
        SceneManager.LoadScene(0);
    }
}
