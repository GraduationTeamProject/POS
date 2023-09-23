using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayerStates { Normal = 0, Thirsty, Radiation_Exposure, Rest, Limited_View}

public class Player : BasePlayer
{
    private float hp;           // 체력
    private float water;        // 수분
    private float fatigue;      // 피로
    private float radiation;    // 방사능
    private float hungry;       // 배고픔

    public float timer;         // 지속적인 감소를 하기위한 타임 저장

    // 플레이어가 가지고 있는 모든 상태, 현재 상태
    public State[] states;
    public State currentState;
    public List<State> playerState = new List<State> { };

    public float Hp
    {
        set => hp = Mathf.Max(0, value);
        get => hp;
    }
    public float Water
    {
        set => water = Mathf.Max(0, value);
        get => water;
    }
    public float Fatigue
    {
        set => fatigue = Mathf.Max(0, value);
        get => fatigue;
    }
    public float Radiation
    {
        set => radiation = Mathf.Max(0, value);
        get => radiation;
    }
    public float Hungry
    {
        set => hungry = Mathf.Max(0, value);
        get => hungry;
    }

    public override void Setup(string name)
    {
        base.Setup(name);

        gameObject.name = $"{ID:D2}_Playere_{name}";

        // Player가 가질 수 있는 상태 개수만큼 메모리 할당, 각 상태에 클래스 메모리 할당
        states = new State[5];
        states[(int)PlayerStates.Normal] = new PlayerOwendState.Normal();
        states[(int)PlayerStates.Thirsty] = new PlayerOwendState.Thirsty();
        states[(int)PlayerStates.Radiation_Exposure] = new PlayerOwendState.Radiation_Exposure();

        // Player 기본 값 세팅
        hp = 100;
        water = 100;
        fatigue = 0;
        radiation = 0;
        hungry = 100;

        AddState(PlayerStates.Normal);
    }

    public override void Updated()
    {
        timer += Time.deltaTime;

        if(playerState != null)
        {
            for (int i = 0; i < playerState.Count; i++)
            {
                playerState[i].Execute(this);
            }
        }
    }

    public void AddState(PlayerStates newState)
    {
        // 바꾸려는 상태가 null이면 변경하지 않음
        if (states[(int)newState] == null) return;
        // 상태 변경, 새로 바뀐 상태의 Enter() 호출
        currentState = states[(int)newState];
        playerState.Add(currentState);
        currentState.Enter(this);
    }
}
