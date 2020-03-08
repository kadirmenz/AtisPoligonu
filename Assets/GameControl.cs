using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public GameObject Ak47Object;
    public GameObject DesertEagleObject;
    public GameObject deagleBulletOut;
    public GameObject ak47BulletOut;
    public GameObject Bullet;
    public Camera camera;
    public LayerMask layerMask;
    public Text MagazineText;
    public Text ReloadingText;
    LineRenderer lineRenderer;
    float shotDuration = 0.3f;
    bool isDeagleNeedReload=false;
    bool isAk47NeedReload = false;
    bool ReloadisEnable = false;
    int range = 1000;
    int fireSpeed = 130;
    int shotedAmountDeagle = 0;
    int shotedAmountAk47 = 0;
    float nextFire;
    float ReloadCounter = 0;
    Gun Deagle;
    Gun Ak47;


    string currentGun;
    void Start()
    {
        Ak47 = new Gun("Ak47", true, 30, 5,5,ak47BulletOut);
        Deagle = new Gun("Deagle", false, 10, 40,4,deagleBulletOut);
        ReloadingText.gameObject.SetActive(false);
    }

    
    void Update()
    {
        magazineControl();
        
        ChangeGun();
        if (DesertEagleObject.activeSelf)
        {
            Fire(Deagle);
            currentGun = Deagle.gunName;
            MagazineText.text = Deagle.magazineAmount - shotedAmountDeagle + " / " + Deagle.magazineAmount;
            StartCoroutine(ReloadControl(Deagle));
        }
        else if (Ak47Object.activeSelf)
        {
            Fire(Ak47);
            currentGun = Ak47.gunName;
            MagazineText.text = Ak47.magazineAmount - shotedAmountAk47 + " / " + Ak47.magazineAmount;
            StartCoroutine(ReloadControl(Ak47));
        }
        //DrawLines();

        Debug.Log(currentGun);

    }

    private void magazineControl()
    {
        if (Deagle.magazineAmount - shotedAmountDeagle <= 0 )
        {
            isDeagleNeedReload = true;
            if (DesertEagleObject.activeSelf)
            {
                Debug.Log(Deagle.gunName + " Silahinin Sarjoru Bitti.");

            }
            //shotedAmountDeagle = 0;

        }
        if (Ak47.magazineAmount - shotedAmountAk47 <= 0 )
        {
            isAk47NeedReload = true;
            if (Ak47Object.activeSelf)
            {
                Debug.Log(Ak47.gunName + " Silahinin Sarjoru Bitti.");

            }
            
            //shotedAmountAk47 = 0;

        }
    }
    
    IEnumerator ReloadControl(Gun gun)
    {
        
        if (ReloadisEnable)
        {
            ReloadingText.gameObject.SetActive(true);
            ReloadCounter -= Time.deltaTime;
            if (ReloadCounter <= 0) { ReloadCounter = 0; }
            ReloadingText.text = "Reloading..." + ReloadCounter;
        }
        else
        {
            ReloadingText.gameObject.SetActive(false);
            if (gun.gunName == Deagle.gunName && Deagle.magazineAmount != Deagle.magazineAmount - shotedAmountDeagle)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    ReloadCounter = Deagle.ReloadingTime;
                    ReloadisEnable = true;
                    yield return StartCoroutine(Wait(Deagle.ReloadingTime));
                    ReloadisEnable = false;
                    isDeagleNeedReload = false;
                    shotedAmountDeagle = 0;
                }
            }
            else if (gun.gunName == Ak47.gunName && Ak47.magazineAmount != Ak47.magazineAmount - shotedAmountAk47)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    ReloadCounter = Ak47.ReloadingTime;
                    ReloadisEnable = true;
                    yield return StartCoroutine(Wait(Ak47.ReloadingTime));
                    ReloadisEnable = false;
                    isAk47NeedReload = false;
                    shotedAmountAk47 = 0;
                }
            }
        }
        
        
    }

    private void DrawLines()
    {
        Vector3 rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Deagle.bulletOut.GetComponent<LineRenderer>().SetPosition(0, Deagle.bulletOut.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, camera.transform.forward, out hit, range))
        {
            Deagle.bulletOut.GetComponent<LineRenderer>().SetPosition(1, hit.point);
        }
        else
        {
            Deagle.bulletOut.GetComponent<LineRenderer>().SetPosition(1, rayOrigin + (camera.transform.forward * range));
        }
        Debug.DrawRay(rayOrigin, camera.transform.forward * range);
        Debug.DrawLine(Deagle.bulletOut.GetComponent<LineRenderer>().GetPosition(0), Deagle.bulletOut.GetComponent<LineRenderer>().GetPosition(0));
    }

    private void Fire(Gun currentGun)
    {
        if (currentGun.gunName == Deagle.gunName)
        {
            shotDuration = 0.2f;
            if (Input.GetKeyDown(KeyCode.S) && !isDeagleNeedReload && !ReloadisEnable)
            {
                shotedAmountDeagle++;
                Debug.Log("Ates Edildi..");
                Deagle.bulletOut.GetComponent<LineRenderer>().SetPosition(0, Deagle.bulletOut.transform.position);
                Vector3 rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hit;
                /*if (Physics.Raycast(rayOrigin, camera.transform.forward, out hit, range))
                {
                    Deagle.bulletOut.GetComponent<LineRenderer>().SetPosition(1, hit.transform.position);
                    GameObject CreatedBullet=Instantiate(Bullet, Deagle.bulletOut.transform.position, Quaternion.identity);
                    
                    CreatedBullet.GetComponent<Rigidbody>().AddForce((hit.point-CreatedBullet.transform.position)*Time.deltaTime * fireSpeed, ForceMode.Force);
                }
                else
                {}*/
                Deagle.bulletOut.GetComponent<LineRenderer>().SetPosition(1, rayOrigin + (camera.transform.forward * range));
                GameObject CreatedBullet = Instantiate(Bullet, Deagle.bulletOut.transform.position, Quaternion.identity);
                CreatedBullet.name = "DeagleBullet";
                CreatedBullet.GetComponent<Rigidbody>().AddForce(((camera.transform.forward * range) - CreatedBullet.transform.position) * Time.deltaTime * fireSpeed, ForceMode.Force);
                
            }
        } else if (currentGun.gunName == Ak47.gunName )
        {
            shotDuration += Time.deltaTime;
            if (Input.GetKey(KeyCode.S) && shotDuration >= 0.2f && !isAk47NeedReload && !ReloadisEnable)
            {
                shotedAmountAk47++;
                Debug.Log("Ates Edildi..");
                Ak47.bulletOut.GetComponent<LineRenderer>().SetPosition(0, Ak47.bulletOut.transform.position);
                Vector3 rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hit;
                    /*if (Physics.Raycast(rayOrigin, camera.transform.forward, out hit, range))
                    {
                        Deagle.bulletOut.GetComponent<LineRenderer>().SetPosition(1, hit.transform.position);
                        GameObject CreatedBullet=Instantiate(Bullet, Deagle.bulletOut.transform.position, Quaternion.identity);

                        CreatedBullet.GetComponent<Rigidbody>().AddForce((hit.point-CreatedBullet.transform.position)*Time.deltaTime * fireSpeed, ForceMode.Force);
                    }
                    else
                    {}*/
                Ak47.bulletOut.GetComponent<LineRenderer>().SetPosition(1, rayOrigin + (camera.transform.forward * range));
                GameObject CreatedBullet = Instantiate(Bullet, Ak47.bulletOut.transform.position, Quaternion.identity);
                CreatedBullet.name = "Ak47Bullet";
                CreatedBullet.GetComponent<Rigidbody>().AddForce(((camera.transform.forward * range) - CreatedBullet.transform.position) * Time.deltaTime * fireSpeed, ForceMode.Force);
                shotDuration = 0;
                
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                shotDuration = 0.2f;
            }
        }
        
    }

    private void ChangeGun()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !ReloadisEnable)
        {
            if (DesertEagleObject.activeSelf)
            {
                DesertEagleObject.SetActive(false);
                Ak47Object.SetActive(true);

            }else if (Ak47Object.activeSelf)
            {
                Ak47Object.SetActive(false);
                DesertEagleObject.SetActive(true);
                
            }
        }
    }
    IEnumerator Wait(int waitSecond)
    {
        yield return new WaitForSeconds(waitSecond);
    }
    public string GetCurrentGun()
    {
        return currentGun;
    }
}
