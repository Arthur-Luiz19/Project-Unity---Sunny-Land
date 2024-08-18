using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    
[Header("Componentes")]
private Animator        PA;
private Rigidbody2D     rig;
private SpriteRenderer  srP;
private ControllerGame  _controlGame;
private ControllerFade  _instanciaFade;
private BossController  _bossController;
private StageSelect     _stageSelect;
private FimFase         _fimFase;

[Header("Movimentos")]
public  float           Speed = 5f;
private float           moveX;
private float           moveY;
private Vector2         direction = Vector2.right;
private float           jumpForce = 10f;
public  bool            Jump = false;
private int             numeroPulos = 0;
private int             maxPulos = 2;
public  bool            isGrounded;
public  Transform       GroundCheck;
public  bool            facingRight;
private bool            iFrames;
private bool            win;
public  bool            isClimbing;
private float           velClimb = 3f;

[Header("Checkpoint e Vida")]
private int             vidas = 3;
public  GameObject      playerDie;
public  Transform       checkPoint;
private bool            isCheckPoint;
private Vector2         checkPointPos;

[Header("Efeitos Visuais e Sonoros")]
public  ParticleSystem  poeira;
public  AudioSource     JumpFX;
public  AudioClip       fxPulo;
public  Color           hitColor;
public  Color           noHitColor;

    
    void Awake(){
        
    }
    
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        PA = GetComponent<Animator>();
        srP = GetComponent<SpriteRenderer>();
        win = false;
        

        _controlGame = FindObjectOfType(typeof(ControllerGame)) as ControllerGame;
        _instanciaFade = FindObjectOfType(typeof(ControllerFade)) as ControllerFade;
        _stageSelect = FindObjectOfType(typeof(StageSelect)) as StageSelect;
        _bossController = FindObjectOfType(typeof(BossController)) as BossController;
        _fimFase = FindObjectOfType(typeof(FimFase)) as FimFase;
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        isGrounded = Physics2D.Linecast(transform.position, GroundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        PA.SetBool("isGrounded", isGrounded);
        Debug.Log(isGrounded);
        
        if(Input.GetButtonDown("Jump")){
            Jump = true;
        }
        Moves();
        WinGame();

         if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Fecha o jogo
            Application.Quit();

            // Para garantir que funcione no editor da Unity
            
        }

    }

    void FixedUpdate(){
        if(Jump){
            jumpPLayer();
        }
        MovePlayer();
        if(moveX < 0 && !facingRight || moveX > 0 && facingRight){
            Flip();
        }

    }
    

    void MovePlayer(){
        

        if(isClimbing){
            rig.velocity = new Vector2(moveX * Speed, moveY * velClimb);
            rig.gravityScale = 0f;
        }
        else {
            rig.velocity = new Vector2(moveX * Speed, rig.velocity.y);
            rig.gravityScale = 3f;
        }
        
    }

    void Moves(){
        if (isClimbing)
        {
            PA.SetBool("Walk", Mathf.Abs(moveY) > 0.1f); // Suavizando a transição de animação
            PA.SetBool("Jump", false);
            PA.SetBool("Climb", true);
        }
        else
        {
            PA.SetBool("Climb", false);
            PA.SetBool("Walk", rig.velocity.x != 0 && isGrounded);
            PA.SetBool("Jump", !isGrounded);
        }
        

        
        

    }

    void jumpPLayer(){
        if(isGrounded || isClimbing){
            numeroPulos = 0;
            CriarPoeira();
            
        }
        if(isGrounded || isClimbing || numeroPulos < maxPulos){
            rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            isClimbing = false;
            numeroPulos++;

            JumpFX.PlayOneShot(fxPulo);
            CriarPoeira();

        }
        Jump = false;

    }
        void Flip(){
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            CriarPoeira();

    }


        void OnTriggerEnter2D(Collider2D coll){
            switch(coll.gameObject.tag){

                case "Colecionaveis":

                _controlGame.Pontuacao(1);
                Destroy(coll.gameObject);
                _controlGame.LifeUp();
                break;

                case "Key":

                _controlGame.GanhaChave(1);
                Destroy(coll.gameObject);
                break;

                case "Life":

                if(vidas < 3){

                    Vida();
                    JumpFX.PlayOneShot(_controlGame.fxChave);
                    Destroy(coll.gameObject);

                }
                
                break;

                case "Inimigos":

                GameObject tempExplosão = Instantiate(_controlGame.hitExplosão, transform.position, transform.localRotation);
                Destroy(tempExplosão, 0.5f);

                Rigidbody2D rig = GetComponentInParent<Rigidbody2D>();
                rig.velocity = new Vector2(rig.velocity.x, 0);
                rig.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);
                Destroy(coll.gameObject);
                JumpFX.PlayOneShot(_controlGame.fxExplosão);
                break;

                
                case "Escadas":

                    isClimbing = true;  
                break;

                case "Damage":

                    Hurt();
                break;

                case "Win":

                    win = true;
                    
                    StartCoroutine(_instanciaFade.FinalFade());
                break;

                case "Boss":

                    _bossController.BossHurt();
                break;

                case "Flag":

                    win = true;
                    Speed = 0;
                    StartCoroutine(_instanciaFade.FinalFade());
                    _fimFase.VoltarSelecao();
                break;

            }
        }

        void OnTriggerExit2D(Collider2D coll){

            if(coll.gameObject.tag == "Escadas"){
                
                isClimbing = false;
                
                
            }
        }

        void OnCollisionEnter2D(Collision2D colision){
            switch(colision.gameObject.tag){

                case "Plataforma":
                this.transform.parent = colision.transform;
                break;
                
                case "Inimigos":
                Hurt();
                break;

                case "Morte":
                Morte();
                break;

                case "Boss":
                Hurt();
                break;
            }
        }

        void OnCollisionExit2D(Collision2D collision){

            switch(collision.gameObject.tag){
                case "Plataforma":
                if (this.transform != null && collision.transform != null)
            {
                // Certifique-se de que o objeto ainda está ativo antes de modificar sua hierarquia
                if (gameObject.activeInHierarchy && collision.gameObject.activeInHierarchy)
                {
                    this.transform.parent = null;
                }
            }
                break;
            }
        }

        void Vida(){

        vidas++;
        _controlGame.BarrasVida(vidas);

        if(vidas > 3){
            vidas = 3;
        }
        }

        void Morte(){
            vidas = 0;
            _controlGame.BarrasVida(vidas);
            GameObject pDieTemp = Instantiate(playerDie, transform.position, Quaternion.identity);
            Rigidbody2D rgDie = pDieTemp.GetComponent<Rigidbody2D>();
            rgDie.AddForce(new Vector2(150f, 400f));
            _controlGame.JumpFX.PlayOneShot(_controlGame.fxDie);
            Invoke("CarregaJogo", 3f);
            gameObject.SetActive(false);
        }

        void Hurt(){

            if(!iFrames){
            vidas--;
            _controlGame.JumpFX.PlayOneShot(_controlGame.fxDie);
            iFrames = true;
            _controlGame.BarrasVida(vidas);
            StartCoroutine("Dano");
            Debug.Log("Perdeu uma vida");

            if(vidas < 1){

                GameObject pDieTemp = Instantiate(playerDie, transform.position, Quaternion.identity);
                Rigidbody2D rgDie = pDieTemp.GetComponent<Rigidbody2D>();
                rgDie.AddForce(new Vector2(150f, 400f));
                _controlGame.JumpFX.PlayOneShot(_controlGame.fxDie);
                Invoke("CarregaJogo", 3f);
                gameObject.SetActive(false);
            }

            }
        }

        void CarregaJogo(){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            _instanciaFade.inicioFade();
        }
        IEnumerator Dano(){
            
            
            srP.color = noHitColor;
            yield return new WaitForSeconds(0.1f);


            for(float i = 0; i < 1; i += 0.1f){
                srP.enabled = false;
                yield return new WaitForSeconds(0.1f);
                srP.enabled = true;
                yield return new WaitForSeconds(0.1f);

            }

            srP.color = Color.white;
            iFrames = false;
        }

        void CriarPoeira(){
            poeira.Play();
        }

        void WinGame(){
            if(win){
                transform.Translate(direction * Speed * Time.deltaTime);
                

                if(PA != null){
                    PA.SetBool("Walk", true);
                }
            }
        }

        
        }


