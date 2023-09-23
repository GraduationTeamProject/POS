using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private string[] arrayPlayers; // Player���� �̸� �迭
    [SerializeField]
    private GameObject playerPrefab;

    private List<BasePlayer> players;

    private void Awake()
    {
        players = new List<BasePlayer>();

        for (int i = 0; i < arrayPlayers.Length; i++)
        {
            // �÷��̾� ����, �ʱ�ȭ �޼ҵ� ȣ��
            GameObject clone = Instantiate(playerPrefab);
            Player player = clone.GetComponent<Player>();
            player.Setup(arrayPlayers[i]);

            // �÷��̾���� ��� ��� ���� ����Ʈ�� ����
            players.Add(player);
        }
    }

    private void Update()
    {
        // ��� �÷��̾��� Updated()�� ȣ���� �÷��̾� ����
        for (int i = 0; i < players.Count; i++)
        {
            players[i].Updated();
        }
    }
}
