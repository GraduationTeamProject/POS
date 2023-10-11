using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerOwendState
{
    // 기본 상태
    public class Normal : State
    {
        public override void Enter(Player player)
        {
            Debug.Log("정상 상태입니다");
        }

        public override void Execute(Player player)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.Water += 50f;
            }

            // 지속적인 수분 감소
            if (player.timer > 1f)
            {
                player.Water -= 15f;
                Debug.Log(player.Water);
                player.timer = 0f;
            }

            // 수분이 20 이하, playerState 안에 Thirty 상태가 없을 때
            if(player.Water <= 20f && !player.playerState.Contains(player.states[(int)PlayerStates.Thirsty]))
            {
                // Thirsty 상태 추가
                player.AddState(PlayerStates.Thirsty);
            }
        }

        public override void Exit(Player player)
        {
            
        }
    }
    // 탈수 상태
    public class Thirsty : State
    {
        public override void Enter(Player player)
        {
            Debug.Log("탈수 상태입니다.");
        }

        public override void Execute(Player player)
        {
            // 행동 제한

            Debug.Log("탈수 진행중");  

            // 수분이 20 초과 일 때
            if(player.Water > 20f)
            {
                // playerState 에 Thirsty 상태 제거 후 Exit 호출
                player.playerState.Remove(player.states[(int)PlayerStates.Thirsty]);
                Exit(player); 
            }
        }

        public override void Exit(Player player)
        {
            // 캬 하는 사운드 재생
        }
    }

    // 방사능 피폭 상태
    public class Radiation_Exposure : State
    {
        public override void Enter(Player player)
        {
            Debug.Log("방사능 피폭 상태입니다.");
        }

        public override void Execute(Player player)
        {
            //방사능 피폭 상태일 때 발생하는 이벤트
            Debug.Log("방사능 피폭 진행중");
            if(player.Radiation < 80)
            {
                //방사능 피폭 상태 제거
            }
        }

        public override void Exit(Player player)
        {
            Debug.Log("방사능 피폭 상태 해제");
        }
    }
}