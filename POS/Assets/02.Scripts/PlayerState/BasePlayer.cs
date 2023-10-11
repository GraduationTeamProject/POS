using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayer : MonoBehaviour
{
    // 정적 변수로 1개만 존재
    private static int playerID = 0;

    // BasePlayer를 상속받는 모든 게임 오브젝트에게 ID 번호 부여
    private int id;
    public int ID
    {
        set
        {
            id = value;
            playerID++;
        }
        get => id;
    }

    private string playerName; // 플레이어 이름

    // 파생 클래스에서 base.Setip()으로 호출
    public virtual void Setup(string name)
    {
        // 고유번호 설정
        ID = playerID;
        // 이름 설정
        playerName = name;
    }

    //GameController 클랫스에서 모든 플레이어의 Udated()를 호출해 플레이어 구동
    public abstract void Updated();
}
