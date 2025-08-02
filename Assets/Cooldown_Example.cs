using UnityEngine;

/**
 * 示例：收到伤害冷却时间的处理案例
 */
public class Cooldown_Example : MonoBehaviour
{
    private SpriteRenderer sr;
    //收到伤害改变状态的时长
    [SerializeField] private float redColorDuration = 1;

    //当前游戏中的时间
    public float currentTimeInGame;
    //记录最后一次收到伤害时的时间值
    public float lastTimeWasDamaged;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        ChanceColorIfNeeded();
    }

    private void ChanceColorIfNeeded()
    {
        currentTimeInGame = Time.time; //一直获取当前游戏的时间

        if (currentTimeInGame > lastTimeWasDamaged + redColorDuration)
        {
            if (sr.color != Color.white)
                sr.color = Color.white;
        }
    }

    public void TakeDamage()
    {
        sr.color = Color.red;
        lastTimeWasDamaged = Time.time; //Time.time类似Java中的System.currentTimeMillis但这里单位是秒
    }  

    private void TurnWihte()
    {
        sr.color = Color.white;
    }
}
