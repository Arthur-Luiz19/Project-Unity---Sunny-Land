using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    public  Transform       enemie;
    public  Transform []    Position;
    public  float           Speed = 6f;
    private SpriteRenderer  batSprite;
    private int             IdTarget;
    public  bool            facingRight;



    
    // Start is called before the first frame update
    void Start()
    {
        batSprite = enemie.gameObject.GetComponent<SpriteRenderer>();
        enemie.position = Position[0].position;
        IdTarget = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(batSprite != null){
            enemie.position = Vector3.MoveTowards(enemie.position, Position[IdTarget].position, Speed * Time.deltaTime);

            if(enemie.position == Position[IdTarget].position){
                IdTarget +=1;
                if(IdTarget == Position.Length){
                    IdTarget = 0;
                }
            
            if(Position[IdTarget].position.x < enemie.position.x && !facingRight){
                Flip();
            }
            else if(Position[IdTarget].position.x > enemie.position.x && facingRight){
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
