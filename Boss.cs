using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rig;

    private Transform PosicaoPlayer;
    private HeroKnight player;
    public float speed = 2;

    private int Vida = 600;
    private int VidaAtual;

    private float dazedTime;
    public float startDazedTime;

    public bool isFlipped = false;
    
    public float at;
    public float at2;
    public Detector detector;
    private float distance = 2;

    public Transform healthBar;//barra verde
    public GameObject healthBarObject; // pai das barras
    private Vector3 healthBarScale;// tamanho da barra
    private float healthPercent; // percentual de vida para o calculo


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        PosicaoPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        at2 = 0;
        detector = FindObjectOfType<Detector>();
        VidaAtual = Vida;
        healthBarScale = healthBar.localScale;
        healthPercent = healthBarScale.x / VidaAtual;
    }

    void UpdateHealthBar()
    {
        healthBarScale.x = healthPercent * VidaAtual;
        healthBar.localScale = healthBarScale;
    }

    // Update is called once per frame
    void Update()
    {
        SeguirJogador();
        LookAtPlayer();

        at = Time.time;


        if (dazedTime > 0)
        {
            speed = 0;
            rig.velocity = new Vector2(speed, rig.velocity.y);
            dazedTime -= Time.deltaTime;

        }
        if (VidaAtual <= 0)
        {
            anim.SetTrigger("Die");
            Destroy(this.gameObject, 0.2f);
            GameController.instance.ShowGameComplete();
        }

        //Attack

        /*if (detector.pd == true && (at - at2 > 2))
        {
            anim.SetTrigger("attack");
            at2 = Time.time;
        }

        if(detector.pd == true)
        {
            speed = 0;
        }
        else
        {
            speed = 2;
        }*/
        
        


    }

    void SeguirJogador()
    {
       

        if (Vector2.Distance(transform.position, PosicaoPlayer.position) > distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, PosicaoPlayer.position, speed * Time.deltaTime);
        }
        else
        {
            anim.SetTrigger("attack");
        }
        
    }

    //Player attack Boss
    public void DanoBoss(int dano)
    {
        dazedTime = startDazedTime;
        VidaAtual -= dano;
        UpdateHealthBar();

    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x > PosicaoPlayer.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
            healthBarObject.transform.localScale = new Vector3(healthBarObject.transform.localScale.x * -1, healthBarObject.transform.localScale.y, healthBarObject.transform.localScale.z);
        }
        else if(transform.position.x < PosicaoPlayer.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
            healthBarObject.transform.localScale = new Vector3(healthBarObject.transform.localScale.x * -1, healthBarObject.transform.localScale.y, healthBarObject.transform.localScale.z);
        }
    }
}
