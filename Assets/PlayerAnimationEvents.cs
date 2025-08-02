using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void DamageEnemies() => player.DamageEnemies();

    //�����ƶ�����Ծ
    private void DisableMovementAndJump() => player.EnableMovementAndJump(false);

    //�����ƶ�����Ծ
    private void EnableMovementAndJump() => player.EnableMovementAndJump(true);
}
