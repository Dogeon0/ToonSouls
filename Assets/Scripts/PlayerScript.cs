using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Movement=======================================
    public CharacterController controller;
    [Header("=======================Character Movement")]

    public Transform cam;
    public float speed = 6f;
    private Vector3 moveDir;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    //Dash======================================

    [Header("=========================Dash Settings")]

    
    public float dashSpeed;
    public float dashTime;
    bool invincible = false;
    
    //Jump===================================
    [Header("===========================Gravity Settings")]
    
    public float gravity = -9.81f; 
    public float jumpForce = 3f;  
    private Vector3 gVelocity;

    void Start()
    {
        //Movement===============================

        //Dash========================================
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 

        //Jump=======================================

    }

    void Update(){
        //Movement==================================
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal,0f,vertical).normalized;
        if(direction.magnitude >= 0.1f){

            float targetAngle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle,0f);
            moveDir = Quaternion.Euler(0f, targetAngle,0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        //Dash======================================
        if(Input.GetMouseButtonDown(0)){
            StartCoroutine(Dash());
        }

        //Jump========================================
        gVelocity.y += gravity * Time.deltaTime;
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            gVelocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
        controller.Move(gVelocity * Time.deltaTime);
    }

    IEnumerator Dash(){
        float startTime = Time.time;

        while(Time.time < startTime + dashTime){
            controller.Move(moveDir * dashSpeed * Time.deltaTime);
            invincible = true;
            yield return null;
        }
        invincible = false;
    }
}
