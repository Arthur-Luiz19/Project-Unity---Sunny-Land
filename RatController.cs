using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
    public  Transform       enemie;
    public  Transform       Player;
    public  Transform[]     Posicao;
    private int             IdTarget;
    private SpriteRenderer  ratSprite;
    public  float           speed = 3f;
    public  bool            facingRight;
    private bool            isPlayerRange = false;
    public  float           detectionRange = 10f; 
    
    // Start is called before the first frame update
    void Start()
    {
        ratSprite = enemie.gameObject.GetComponent<SpriteRenderer>();
        enemie.position = Posicao[0].position;
        IdTarget = 1;

    }

    // Update is called once per frame
    void Update()
    {
        float distancePlayer = Vector3.Distance(enemie.position, Player.position);

        if(distancePlayer <= detectionRange){
            isPlayerRange = true;
        }
        else{
            isPlayerRange = false;
        }

        if(isPlayerRange){
            enemie.position = Vector3.MoveTowards(enemie.position, Player.position, speed * Time.deltaTime);

            if(Player.position.x < enemie.position.x && !facingRight){
                Flip();
            }
            else if(Player.position.x > enemie.position.x && facingRight){
                Flip();
            }
        }
        
        if(ratSprite != null){
            enemie.position = Vector3.MoveTowards(enemie.position, Posicao[IdTarget].position, speed * Time.deltaTime);

            if(enemie.position == Posicao[IdTarget].position){
                IdTarget += 1;
                if(IdTarget == Posicao.Length){
                    IdTarget = 0;
                }

            if(Posicao[IdTarget].position.x < enemie.position.x && !facingRight){
                Flip();
            }
            else if(Posicao[IdTarget].position.x > enemie.position.x && facingRight){
                Flip();
            }
            }
        }
    }

    void Flip(){
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
