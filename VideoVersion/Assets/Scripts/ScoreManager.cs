using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    int _score = 100;
    public int score
    {
        get { return _score; }
        set
        {
            _score = value;
            whenScoreChange.Invoke(_score.ToString());
        }
    }
    [SerializeField]
    protected MyStringEvent whenScoreChange;

    public static ScoreManager singleton;

    void Awake()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        singleton = this;

        whenScoreChange.Invoke(_score.ToString());
    }

}
