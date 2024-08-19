using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
   
[Header("Configurações da Câmera")]
public float cameraSpeed = 10f;
public float offsetX = 3f;
public float smooth = 0.1f;
public float limitedUp = 2f;
public float limitedDown = 0f;
public float limitedLeft = 0f;
public float limitedRight = 100f;

[Header("Referências")]
public Transform player;

[Header("Dados de Estágio")]
private int               IdTarget;
private int               IdStage;
private bool[]            stageComplete;


     
    
    // Start is called before the first frame update
    void Awake()
    {
        
    }
    
    
    void FixedUpdate()
    {
        

        float moveX = Input.GetAxisRaw("Horizontal");
        float movey = Input.GetAxisRaw("Vertical");

        Vector3 Direction = new Vector3(moveX, movey, 0f);

        transform.position += Direction * cameraSpeed * Time.deltaTime;

        moveX = Mathf.Clamp(player.position.x + offsetX, limitedLeft, limitedRight);
        movey = Mathf.Clamp(player.position.y, limitedDown, limitedUp);

            transform.position = Vector3.Lerp(transform.position, new Vector3(moveX, movey, transform.position.z), smooth);

    }
    
    
        
        
            

        
    

    // Update is called once per frame
    

   
}
