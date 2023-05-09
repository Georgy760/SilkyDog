using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI place_text;
    [SerializeField] private TextMeshProUGUI nick_name_text;
    [SerializeField] private TextMeshProUGUI score_text;

    private int place;
    private string nick_name;
    private int score;

    public int Place 
    {
        get { return place; }
        set 
        {
            place = value;
            place_text.text = value.ToString(); 
        }
    }
    public string NickName 
    {
        get { return nick_name; }
        set
        {
            nick_name = value;
            nick_name_text.text = value;
        }
    }
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            score_text.text = value.ToString();
        }
    }
}
