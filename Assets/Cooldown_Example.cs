using UnityEngine;

/**
 * ʾ�����յ��˺���ȴʱ��Ĵ�����
 */
public class Cooldown_Example : MonoBehaviour
{
    private SpriteRenderer sr;
    //�յ��˺��ı�״̬��ʱ��
    [SerializeField] private float redColorDuration = 1;

    //��ǰ��Ϸ�е�ʱ��
    public float currentTimeInGame;
    //��¼���һ���յ��˺�ʱ��ʱ��ֵ
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
        currentTimeInGame = Time.time; //һֱ��ȡ��ǰ��Ϸ��ʱ��

        if (currentTimeInGame > lastTimeWasDamaged + redColorDuration)
        {
            if (sr.color != Color.white)
                sr.color = Color.white;
        }
    }

    public void TakeDamage()
    {
        sr.color = Color.red;
        lastTimeWasDamaged = Time.time; //Time.time����Java�е�System.currentTimeMillis�����ﵥλ����
    }  

    private void TurnWihte()
    {
        sr.color = Color.white;
    }
}
