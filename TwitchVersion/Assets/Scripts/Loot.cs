using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField]
    int scoreToAdd = 100;
    [SerializeField]
    FloatingText floatingNumberPrefab;

    public void AddScore()
    {
        FloatingText floating = Instantiate(floatingNumberPrefab, transform.position, floatingNumberPrefab.transform.rotation);
        floating.Init(string.Format("<b>{0}</b>", scoreToAdd.ToString()));
        ScoreManager.singleton.score += scoreToAdd;
    }
}
