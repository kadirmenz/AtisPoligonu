using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{

    Rigidbody rigidbody;
    public int speed = 5;
    float xAxisLimit = 0;
    

    [SerializeField]
    float mouseSensitivity=1;
    [SerializeField]
    Transform gunSpawn, camera;
    void Start()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody>();

    }

    
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //movement();
        mouseLookingAround();
    }

    private void mouseLookingAround()
    {
        float mouseX=Input.GetAxis("Mouse X");
        float mouseY=Input.GetAxis("Mouse Y");

        float mouseXAmount = mouseX * mouseSensitivity;
        float mouseYAmount = mouseY * mouseSensitivity;

        xAxisLimit += mouseYAmount;
       
        Vector3 rotateGun = gunSpawn.transform.rotation.eulerAngles;
        Vector3 rotatePlayer = transform.rotation.eulerAngles;
        Vector3 rotateCamera = camera.transform.rotation.eulerAngles;
        
        rotatePlayer.y += mouseXAmount;
        rotatePlayer.z = 0;

        
        rotateGun.y += mouseXAmount;

        
        rotateCamera.y += mouseXAmount;
        rotateCamera.z = 0;

        if (xAxisLimit <= 80 && xAxisLimit >= -90)
        {
            rotateCamera.x -= mouseYAmount;
            rotateGun.x -= mouseYAmount;
        }
        else
        {
            if (xAxisLimit > 0)
            {
                xAxisLimit = 80;
            }
            else
            {
                xAxisLimit= -90;
            }
        }
        
        
        


        
        

        
        

        transform.rotation = Quaternion.Euler(rotatePlayer);
        gunSpawn.rotation = Quaternion.Euler(rotateGun);
        camera.rotation = Quaternion.Euler(rotateCamera);
    }

    void movement()
    {
        float Vertical = Input.GetAxisRaw("Vertical");
        float Horizontal = Input.GetAxisRaw("Horizontal");


        if (Horizontal > 0)
        {
            //transform.forward += new Vector3(Horizontal, rigidbody.velocity.y, rigidbody.velocity.z) * Time.deltaTime * speed;
            rigidbody.velocity = transform.right * speed;
        }
        else if (Horizontal < 0)
        {
            //transform.forward += new Vector3(Horizontal, rigidbody.velocity.y, rigidbody.velocity.z) * Time.deltaTime * speed;
            rigidbody.velocity = -transform.right * speed;
        }
        else if (Vertical > 0)
        {

            //transform.position += new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, Vertical) * Time.deltaTime * speed;
            rigidbody.velocity = transform.forward * speed;
        }
        else if (Vertical < 0)
        {
            //transform.position += new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, Vertical) * Time.deltaTime * speed;
            rigidbody.velocity = -transform.forward * speed;
        }
        else
        {
            rigidbody.velocity = new Vector3(0,0,0);
        }
    }
}
