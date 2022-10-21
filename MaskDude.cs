using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskDude : MonoBehaviour
{


    private Rigidbody2D rig;
    private Animator anim;

    public float speed;
    float teste;

    public Transform rightCol;
    public Transform leftCol;

    private bool colliding1;
    private bool colliding2;

    [SerializeField]
    private LayerMask layer1;

    [SerializeField]
    private LayerMask layer2;

    //[SerializeField]
    //private LayerMask layer3;

    public int VidaAtual;

    //parar quando levar hit
    private float dazedTime;
    public float startDazedTime;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        

        rig.velocity = new Vector2(speed, rig.velocity.y);

        
        if(dazedTime > 0)
        {
            teste = speed;
            speed = 0;
            rig.velocity = new Vector2(speed, rig.velocity.y);
            dazedTime -= Time.deltaTime;
            speed = teste;

        }
        
        
        colliding1 = Physics2D.Linecast(rightCol.position, leftCol.position, layer1);
        colliding2 = Physics2D.Linecast(rightCol.position, leftCol.position, layer2);
        //colliding3 = Physics2D.Linecast(rightCol.position, leftCol.position, layer3);

        if (colliding1 || colliding2)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed = - speed;
        }

        if (VidaAtual <= 0)
        {
            anim.SetTrigger("Die");
            Destroy(this.gameObject,0.2f);

        }

    }

    public void DanoInimigo(int dano)
    {
        dazedTime = startDazedTime;
        VidaAtual -= dano;
        anim.SetTrigger("Die");

    }

}
