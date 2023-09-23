using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayerStates { Normal = 0, Thirsty, Radiation_Exposure, Rest, Limited_View}

public class Player : BasePlayer
{
    private float hp;           // ü��
    private float water;        // ����
    private float fatigue;      // �Ƿ�
    private float radiation;    // ����
    private float hungry;       // �����

    public float timer;         // �������� ���Ҹ� �ϱ����� Ÿ�� ����

    // �÷��̾ ������ �ִ� ��� ����, ���� ����
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

        // Player�� ���� �� �ִ� ���� ������ŭ �޸� �Ҵ�, �� ���¿� Ŭ���� �޸� �Ҵ�
        states = new State[5];
        states[(int)PlayerStates.Normal] = new PlayerOwendState.Normal();
        states[(int)PlayerStates.Thirsty] = new PlayerOwendState.Thirsty();
        states[(int)PlayerStates.Radiation_Exposure] = new PlayerOwendState.Radiation_Exposure();

        // Player �⺻ �� ����
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
        // �ٲٷ��� ���°� null�̸� �������� ����
        if (states[(int)newState] == null) return;
        // ���� ����, ���� �ٲ� ������ Enter() ȣ��
        currentState = states[(int)newState];
        playerState.Add(currentState);
        currentState.Enter(this);
    }
}
