using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;  //物理特性组件

    [Header("Attack details")]  //攻击相关信息
    [SerializeField] private float attackRadius; //攻击圆型区域半径
    [SerializeField] private Transform attackPoint; //攻击的圆心位置
    [SerializeField] private LayerMask whatIsEnemy; //攻击的是敌人的物体

    [Header("Movement details")]
    [SerializeField] private float moveSpeed = 3.5f; // 移动速度
    [SerializeField] private float jumpForce = 8;  //跳跃高度
    [SerializeField] private float xInput; //x轴的水平方向

    [Header("Collision details")]
    [SerializeField] private float groundCheckDistance;  //Player的中心点离地面的距离
    [SerializeField] private bool isGrounded; //是否在地面上
    [SerializeField] private LayerMask whatIsGround; //地面层级


    [SerializeField] private bool facingRight = true; //面朝右边

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
        //检测攻击区域，攻击半径，是不是敌人的物体层级 ：获取攻击的敌人集合
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius,whatIsEnemy);
        foreach (Collider2D enemy in enemyColliders)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.TakeDamage();
            Debug.Log("I damage enemy : " + enemyScript.GetEnemyName());
        }
    }

    //执行动画
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

    //攻击
    private void TryToAttack()
    {
        if (isGrounded)
            anim.SetTrigger("attack");
    }
   

    //跳跃
    private void TryToJump()
    {
        //在地面上才能跳跃
        if (isGrounded && canJump) 
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }


    //执行移动
    private void HandleMovement()
    {
        if(canMove)
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
    }

    //发射射线检测是否离开地面
    private void HandleCollision()
    {
        //发射坐标，方向，距离，物体
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }


    //执行翻转逻辑
    private void HandleFilp()
    {
        //若是往右并且脸朝左或者往左脸朝右，那么需要翻转
        if (rb.linearVelocity.x > 0 && facingRight == false)
            Filp();
        else if(rb.linearVelocity.x < 0 && facingRight == true)
            Filp();
    }

    [ContextMenu("Flip")]
    //水平翻转180度
    private void Filp()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }

    /**
     * 通过Unity提供的Gizmos类绘制各种图形（如线框、球体、立方体等），帮助开发者直观查看：
     * 碰撞体范围
     * 视野范围
     * 路径点
     * 向量方向
     * 自定义调试信息
     * 仅在Unity编辑器中执行，不会在游戏运行时调用。用于开发阶段的调试和可视化
     * */
    private void OnDrawGizmos()
    {
        //绘制射线距离地面的高度，用于开发模式查看
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0,-groundCheckDistance));
        //绘制圆形攻击区域查看，用于开发模式查看
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
