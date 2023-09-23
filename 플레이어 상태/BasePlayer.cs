using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayer : MonoBehaviour
{
    // ���� ������ 1���� ����
    private static int playerID = 0;

    // BasePlayer�� ��ӹ޴� ��� ���� ������Ʈ���� ID ��ȣ �ο�
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

    private string playerName; // �÷��̾� �̸�

    // �Ļ� Ŭ�������� base.Setip()���� ȣ��
    public virtual void Setup(string name)
    {
        // ������ȣ ����
        ID = playerID;
        // �̸� ����
        playerName = name;
    }

    //GameController Ŭ�������� ��� �÷��̾��� Udated()�� ȣ���� �÷��̾� ����
    public abstract void Updated();
}
