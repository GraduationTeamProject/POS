public abstract class State
{
    // �ش� ���� ������ �� 1ȸ ȣ��
    public abstract void Enter(Player player);

    // �ش� ���� ������Ʈ�� �� �� ������ ȣ��
    public abstract void Execute(Player player);

    //�ش� ���� ������ �� 1ȸ ȣ��
    public abstract void Exit(Player player);
}
