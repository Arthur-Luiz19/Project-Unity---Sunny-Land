using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BossController : MonoBehaviour
{
    
[Header("Componentes")]
private SpriteRenderer  sprite;
private Animator        PA;
private ControllerGame  _controllerGame;

[Header("Alvos e Referências")]
public  Transform       enemie;
public  Transform       player;
public  GameObject      bee;
public  GameObject      bee2;
public  GameObject      BossHit;
public  GameObject      BossDie;
public  GameObject      flag;

[Header("Movimentação e Status")]
public  float           speed = 5f;
public  bool            facingRight;
private bool            beeActivated = false;
private bool            iFrames;
private bool            isHit = false;
private int             vidas = 3;

[Header("Detecção e Ataque")]
private float           detectionRange = 20f;
private float           attackRange = 2f;
public  bool            isPlayerRange = false;

[Header("Efeitos Sonoros")]
public  AudioSource     JumpFX;

    
    
    // Start is called before the first frame update
    void Start()
    {
        sprite = enemie.gameObject.GetComponent<SpriteRenderer>();
        _controllerGame = FindObjectOfType(typeof(ControllerGame)) as ControllerGame;

        PA = GetComponent<Animator>();
        bee.SetActive(false);
        bee2.SetActive(false);
        flag.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
        
        float distancePlayer = Vector3.Distance(enemie.position, player.position);

        if(distancePlayer <= detectionRange){
            isPlayerRange = true;
        }
        else {
            isPlayerRange = false;
        }

        if(isPlayerRange){

            enemie.position = Vector3.MoveTowards(enemie.position, player.position, speed * Time.deltaTime);
            

            if(player.position.x < enemie.position.x && !facingRight){
                Flip();
            }
            else if(player.position.x > enemie.position.x && facingRight){
                Flip();
            }

            if(distancePlayer <= attackRange){
                isPlayerRange = true;
                StartCoroutine(AttackCoroutine());
            }
            
        }
    }
        public void BossHurt(){
            if(!isHit){

                StartCoroutine(HitCoroutine());
                
                if(vidas < 1)
                {
                    enemie.position = new Vector3(63, -13, Time.deltaTime);
                    StartCoroutine(DieCoroutine());
                    Destroy(gameObject, 1.7f); 
                    flag.SetActive(true);
                }
            }
            
        }
            

         IEnumerator AttackCoroutine(){

            PA.SetTrigger("Attack");
            yield return new WaitForSeconds(1.5f);
         }
         
         IEnumerator HitCoroutine()
        {
            if(!iFrames){

                isHit = true;
                enemie.position = new Vector3(82, -13, Time.deltaTime);
                iFrames = true;
                vidas--;
                PA.SetTrigger("Hit");
                JumpFX.PlayOneShot(_controllerGame.fxExplosão);
                StartCoroutine(SpeedCoroutine());

                if(!beeActivated){

                    bee.SetActive(true);
                    beeActivated = true;
                    Destroy(bee.gameObject, 20f);
                }

                    if(vidas == 1){

                        PA.SetTrigger("Hit");
                        iFrames = true;
                        enemie.position = new Vector3(82, -13, Time.deltaTime);
                        JumpFX.PlayOneShot(_controllerGame.fxExplosão);
                        StartCoroutine(SpeedCoroutine());

                        if(!bee2.activeSelf){

                            bee2.SetActive(true);
                        }

                    }
            }
            


            // Wait for 1 second (length of hit animation)
            yield return new WaitForSeconds(0.5f);
            
            isHit = false;
        }

        IEnumerator SpeedCoroutine(){
            speed = 0;
            yield return new WaitForSeconds(12f);

            speed = 5f;
            iFrames = false;
        }

        IEnumerator DieCoroutine(){

            PA.SetTrigger("Die");
            JumpFX.PlayOneShot(_controllerGame.fxExplosão);
            speed = 0;

            yield return new WaitForSeconds(2.0f);
        }
        void Flip(){

            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
}
