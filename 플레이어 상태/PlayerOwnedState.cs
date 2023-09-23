using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerOwendState
{
    // �⺻ ����
    public class Normal : State
    {
        public override void Enter(Player player)
        {
            Debug.Log("���� �����Դϴ�");
        }

        public override void Execute(Player player)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.Water += 50f;
            }

            // �������� ���� ����
            if (player.timer > 1f)
            {
                player.Water -= 15f;
                Debug.Log(player.Water);
                player.timer = 0f;
            }

            // ������ 20 ����, playerState �ȿ� Thirty ���°� ���� ��
            if(player.Water <= 20f && !player.playerState.Contains(player.states[(int)PlayerStates.Thirsty]))
            {
                // Thirsty ���� �߰�
                player.AddState(PlayerStates.Thirsty);
            }
        }

        public override void Exit(Player player)
        {
            
        }
    }
    // Ż�� ����
    public class Thirsty : State
    {
        public override void Enter(Player player)
        {
            Debug.Log("Ż�� �����Դϴ�.");
        }

        public override void Execute(Player player)
        {
            // �ൿ ����

            Debug.Log("Ż�� ������");  

            // ������ 20 �ʰ� �� ��
            if(player.Water > 20f)
            {
                // playerState �� Thirsty ���� ���� �� Exit ȣ��
                player.playerState.Remove(player.states[(int)PlayerStates.Thirsty]);
                Exit(player); 
            }
        }

        public override void Exit(Player player)
        {
            // ļ �ϴ� ���� ���
        }
    }

    // ���� ���� ����
    public class Radiation_Exposure : State
    {
        public override void Enter(Player player)
        {
            Debug.Log("���� ���� �����Դϴ�.");
        }

        public override void Execute(Player player)
        {
            //���� ���� ������ �� �߻��ϴ� �̺�Ʈ
            Debug.Log("���� ���� ������");
            if(player.Radiation < 80)
            {
                //���� ���� ���� ����
            }
        }

        public override void Exit(Player player)
        {
            Debug.Log("���� ���� ���� ����");
        }
    }
}