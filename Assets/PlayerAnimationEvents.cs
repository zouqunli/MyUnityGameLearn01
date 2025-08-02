using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void DamageEnemies() => player.DamageEnemies();

    //禁用移动和跳跃
    private void DisableMovementAndJump() => player.EnableMovementAndJump(false);

    //启用移动和跳跃
    private void EnableMovementAndJump() => player.EnableMovementAndJump(true);
}
