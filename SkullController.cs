using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullController : MonoBehaviour
{
   
   public   Transform         enemie;
   private  SpriteRenderer    Skull;
   public   Transform         Player;
   public   float             Speed = 2f;
   public   bool              facingRight;
   public   float             detectionRange = 10f;
   private  bool              isPlayerRange = false;
   
   
   
    // Start is called before the first frame update
    void Start()
    {
        Skull = enemie.gameObject.GetComponent<SpriteRenderer>();
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
            enemie.position = Vector3.MoveTowards(enemie.position, Player.position, Speed * Time.deltaTime);

            if(Player.position.x < enemie.position.x && !facingRight){
                Flip();
            }
            else if(Player.position.x > enemie.position.x && facingRight){
                Flip();
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
