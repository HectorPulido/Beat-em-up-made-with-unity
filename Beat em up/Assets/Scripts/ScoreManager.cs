using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text textPot;
    [SerializeField]
    GameObject pause;

    int _score = 0;
    public int score
    {
        get { return _score; }
        set
        {
            _score = value;
            UpdateScore();
        }
    }

    int _pots = 1;
    public int pots
    {
        get { return _pots; }
        set
        {
            _pots = value;
            UpdatePot();
        }
    }

    void UpdateScore()
    {
        scoreText.text = _score.ToString();
    }
    void UpdatePot()
    {
        textPot.text = _pots.ToString();
    }

    public void BuyPots()
    {
        if (score < 100)
            return;
        score -= 100;
        pots++;
    }

    public static ScoreManager singleton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            pause.SetActive(!pause.activeInHierarchy);

    }
	void Start ()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        singleton = this;
        UpdateScore();
        UpdatePot();
    }

}
