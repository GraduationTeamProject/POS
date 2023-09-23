using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private string[] arrayPlayers; // Player들의 이름 배열
    [SerializeField]
    private GameObject playerPrefab;

    private List<BasePlayer> players;

    private void Awake()
    {
        players = new List<BasePlayer>();

        for (int i = 0; i < arrayPlayers.Length; i++)
        {
            // 플레이어 생성, 초기화 메소드 호출
            GameObject clone = Instantiate(playerPrefab);
            Player player = clone.GetComponent<Player>();
            player.Setup(arrayPlayers[i]);

            // 플레이어들의 재생 제어를 위해 리스트에 저장
            players.Add(player);
        }
    }

    private void Update()
    {
        // 모든 플레이어의 Updated()를 호출해 플레이어 구동
        for (int i = 0; i < players.Count; i++)
        {
            players[i].Updated();
        }
    }
}
