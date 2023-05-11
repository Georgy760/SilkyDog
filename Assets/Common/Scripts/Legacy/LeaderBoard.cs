using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private Transform player_info_prefab;

    public void OnClick() //test method
    {
        AddPlayer(1, "dog", 100);
    }

    public void AddPlayer(int place, string nick_name, int score)
    {
        Transform player = Instantiate(player_info_prefab);
        player.SetParent(content);

        PlayerInfo player_info = player.GetComponent<PlayerInfo>();
        player_info.Place = place;
        player_info.NickName = nick_name;
        player_info.Score = score;
    }
}
