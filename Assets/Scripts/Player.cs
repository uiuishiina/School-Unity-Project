using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //インプットシステム
    PlayerInput playerInput;

    //左右用列挙体...あまりやる必要はないがインスペクターからの見やすさのために用意
    enum FlipBool {
        Left = -1,Right = 0
    }

    //設定
    [SerializeField, Header("初期設定が右向きならRightに設定")] private FlipBool FlipSprite_ = FlipBool.Right;
    private FlipBool Flip_cash;
    [SerializeField, Header("アニメーター")] private Animator animator;
    [SerializeField, Header("シールド")] private GameObject shield_;
    [SerializeField, Header("残機")] private const int PlayerLife_ = 5;
    private int Life_ = 5;
    private const float IdleTime_ = 0.2f;
    private float Idle_cool = IdleTime_;
    private bool end_animation = true;

    //外部機能参照
    [SerializeField, Header("GameManager")] private GameManager GameManager_;
    
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        Reset_Game();
    }

    void Reset_Game()
    {
        Life_ = PlayerLife_;
        playerInput.SwitchCurrentActionMap("Player");
        shield_.SetActive(false);
    }

    void Update()
    {
        //左右キー取得
        var move = playerInput.actions["Move"].ReadValue<Vector2>();
        if(move.x != 0)
        {   
            Idle_cool = IdleTime_;
            if (move.x > 0) {
                FlipSprite_ = FlipBool.Right;
            }
            else {
                FlipSprite_ = FlipBool.Left;
            }

            if (FlipSprite_ != Flip_cash) {
                shield_.SetActive(false);
                animator.SetBool("Guard", false);
            }
            
            if (end_animation)
            {
                transform.rotation = Quaternion.Euler(0, 180 * (int)(FlipSprite_), 0);
                animator.SetBool("Guard", true);
                Flip_cash = FlipSprite_;
                end_animation = false;
            }

        }
        else {
            if (Idle_cool > 0) {
                Idle_cool -= Time.deltaTime;
            }
            else {
                shield_.SetActive(false);
                animator.SetBool("Guard", false);
                end_animation = true;
            }
        }

        //デバッグ
        if (playerInput.actions["Jump"].WasPressedThisFrame())
        {
            animator.SetTrigger("Death");
            GameManager_.Death_player(Reset_Player);
        }
    }

    public void ActiveSheld()
    {
        if (!shield_.activeSelf) {
            shield_.SetActive(true);
            ExecuteEvents.Execute<ShiledInterface>(
                target: shield_,
                eventData: null,
                functor: (receiver, eventData) => receiver.OnRecieve(true));
        }
    }

    public void GuardEnd()
    {
        end_animation = true;
        Debug.Log("EndGuard");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("被弾");

            if (Life_ > 0) {
                Life_--;
                animator.SetTrigger("Damage");
            }
            else
            {
                animator.SetTrigger("Death");
                playerInput.SwitchCurrentActionMap("UI");
                GameManager_.Death_player(Reset_Player);
            }
        }
    }

    private void Reset_Player()
    {
        animator.SetTrigger("Reset");
        Reset_Game();
    }
}
