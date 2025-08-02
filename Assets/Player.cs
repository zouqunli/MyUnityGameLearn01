using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;  //�����������

    [Header("Attack details")]  //���������Ϣ
    [SerializeField] private float attackRadius; //����Բ������뾶
    [SerializeField] private Transform attackPoint; //������Բ��λ��
    [SerializeField] private LayerMask whatIsEnemy; //�������ǵ��˵�����

    [Header("Movement details")]
    [SerializeField] private float moveSpeed = 3.5f; // �ƶ��ٶ�
    [SerializeField] private float jumpForce = 8;  //��Ծ�߶�
    [SerializeField] private float xInput; //x���ˮƽ����

    [Header("Collision details")]
    [SerializeField] private float groundCheckDistance;  //Player�����ĵ������ľ���
    [SerializeField] private bool isGrounded; //�Ƿ��ڵ�����
    [SerializeField] private LayerMask whatIsGround; //����㼶


    [SerializeField] private bool facingRight = true; //�泯�ұ�

    private bool canMove = true;
    private bool canJump = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    

    void Start()
    {
        Debug.Log("this  is  start");
    }

    
    void Update()
    {
        HandleCollision();
        HandleInput();
        HandleMovement();
        HandleAnimations();
        HandleFilp();
    }


    public void DamageEnemies()
    {
        //��⹥�����򣬹����뾶���ǲ��ǵ��˵�����㼶 ����ȡ�����ĵ��˼���
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius,whatIsEnemy);
        foreach (Collider2D enemy in enemyColliders)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.TakeDamage();
            Debug.Log("I damage enemy : " + enemyScript.GetEnemyName());
        }
    }

    //ִ�ж���
    private void HandleAnimations()
    {
        anim.SetBool("isGrounded",isGrounded);
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetFloat("yVelocity",rb.linearVelocity.y);
    }

    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if( Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.K))
        {
            TryToJump();
        }
        if(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.J))
        {
            TryToAttack();
        }
    }

    public void EnableMovementAndJump(bool enable)
    {
        canMove = enable;
        canJump = enable;
    }

    //����
    private void TryToAttack()
    {
        if (isGrounded)
            anim.SetTrigger("attack");
    }
   

    //��Ծ
    private void TryToJump()
    {
        //�ڵ����ϲ�����Ծ
        if (isGrounded && canJump) 
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }


    //ִ���ƶ�
    private void HandleMovement()
    {
        if(canMove)
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
    }

    //�������߼���Ƿ��뿪����
    private void HandleCollision()
    {
        //�������꣬���򣬾��룬����
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }


    //ִ�з�ת�߼�
    private void HandleFilp()
    {
        //�������Ҳ���������������������ң���ô��Ҫ��ת
        if (rb.linearVelocity.x > 0 && facingRight == false)
            Filp();
        else if(rb.linearVelocity.x < 0 && facingRight == true)
            Filp();
    }

    [ContextMenu("Flip")]
    //ˮƽ��ת180��
    private void Filp()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }

    /**
     * ͨ��Unity�ṩ��Gizmos����Ƹ���ͼ�Σ����߿����塢������ȣ�������������ֱ�۲鿴��
     * ��ײ�巶Χ
     * ��Ұ��Χ
     * ·����
     * ��������
     * �Զ��������Ϣ
     * ����Unity�༭����ִ�У���������Ϸ����ʱ���á����ڿ����׶εĵ��ԺͿ��ӻ�
     * */
    private void OnDrawGizmos()
    {
        //�������߾������ĸ߶ȣ����ڿ���ģʽ�鿴
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0,-groundCheckDistance));
        //����Բ�ι�������鿴�����ڿ���ģʽ�鿴
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
