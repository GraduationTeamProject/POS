public abstract class State
{
    // 해당 상태 시작할 때 1회 호출
    public abstract void Enter(Player player);

    // 해당 상태 업데이트할 때 매 프레임 호출
    public abstract void Execute(Player player);

    //해당 상태 종료할 때 1회 호출
    public abstract void Exit(Player player);
}
