using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI CoinTextScore;
    private AudioSource AudioManager;
    private int ScoreCount = 0;
    private void Awake()
    {
        AudioManager = GetComponent<AudioSource>();
    }
    void Start()
    {
        CoinTextScore = GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Coin")
        {
            target.gameObject.SetActive(false);
            ScoreCount++;
            CoinTextScore.text = "x" + ScoreCount;
            AudioManager.Play();
        }
    }
    void Update()
    {
        
    }
}//class
