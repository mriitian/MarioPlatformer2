using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI LifeText;
    private int LifeScoreCount;

    private bool CanDamage;

    private void Awake()
    {
        LifeText = GameObject.Find("LifeText").GetComponent<TextMeshProUGUI>();
        LifeScoreCount = 3;
        LifeText.text = "x" + LifeScoreCount;

        CanDamage = true;
    }

    public void DealDamage()
    {
        if (CanDamage)
        {
            LifeScoreCount--;
            if (LifeScoreCount >= 0)
            {
                LifeText.text = "x" + LifeScoreCount;
            }

            if (LifeScoreCount < 0)
            {
                //Restart the Game
                Time.timeScale = 0f;
                StartCoroutine(RestarttheGame());
            }
            CanDamage = false;
            StartCoroutine(WaitForDamage());
        }
    }
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        CanDamage = true;
    }
    IEnumerator RestarttheGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("SampleScene");

    }
}
