using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    public int nextMove;//행동지표를 결정할 변수
    public GameObject prfHpBar;
    public GameObject canvas;
    RectTransform hpBar;

    public float height = 1.7f;

    public string enemyName;
    public int maxHp;
    public int nowHp;
    public int atkDmg;
    public int atkSpeed;
    void Start()
    {
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
        if(name.Equals("Monster"))
        {
            SetEnemyStatus("Monster",100, 10, 1);
        }
        nowHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();
    }
    void Update()
    {
        Vector3 _hpBarPos =
            Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = _hpBarPos;
        nowHpbar.fillAmount = (float)nowHp / (float)maxHp;
        
    }
    private void SetEnemyStatus(string _enemyName, int _maxHp, int _atkDmg, int _atkSpeed)
    {
	    enemyName = _enemyName;
	    maxHp = _maxHp;
	    nowHp = _maxHp;
	    atkDmg = _atkDmg;
	    atkSpeed = _atkSpeed;
    }
    public Heavy_Move heavy_move;
    Image nowHpbar;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Bullet"))
        {
            if(heavy_move.bullet)
            {
                nowHp -= heavy_move.atkDmg;
                Debug.Log(nowHp);
                if(nowHp <=0)
                {
                    Destroy(gameObject);
                    Destroy(hpBar.gameObject);
                }
            }
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        
        Invoke("Think",2);
    }
 
    // Update is called once per frame
    void FixedUpdate()
    {
        //한 방향으로만 알아서 움직이게
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);//왼쪽으로 가니까 -1, y축은 0을 넣으면 큰일남!
 
 
        //플랫폼 체크 
        //몬스터는 앞을 체크해야 
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.2f,rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0)); 
        // 시작,방향 색깔
         
        RaycastHit2D rayHit1 = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("grounded"));
 
        
        if (rayHit1.collider == null)
        {
 
            Turn();
 
        }
    }
    //행동지표를 바꿔줄 함수 생각 --> 랜덤클래스 활용 
 
    void Think()
    {
 
        //set next active
        nextMove = Random.Range(-1, 2); //-1이면 왼쪽, 0이면 멈추기,1이면 오른쪽으로이동
 
        //sprite animation
        anim.SetInteger("WalkSpeed", nextMove);
 
        //방향 바꾸기 (0일 때는 굳이 바꿀 필요없기에 조건문 사용해준거)
        if (nextMove != 0)
        {
            spriteRenderer.flipX = (nextMove == 1); //nextMove가 1이면 방향바꾸기
        }
 
        //재귀 
        float nextThinkTime = Random.Range(2f, 5f);//생각하는 시간도 랜덤으로 
 
        Invoke("Think", nextThinkTime);//재귀
 
 
 
 
    }
 
    void Turn()
    {
        nextMove = nextMove * (-1);
        spriteRenderer.flipX = (nextMove == 1); //nextMove가 1이면 방향바꾸기
 
 
        CancelInvoke();
        Invoke("Think", 2);
    }
}