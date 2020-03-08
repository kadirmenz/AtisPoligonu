using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    public Text EnemyHealth;
    int Health = 100;
    public Text hitText;
    void Start()
    {
        EnemyHealth.text = "Enemy Health = " + Health;
        hitText.gameObject.SetActive(false);
    }

    
    void Update()
    {

        lifeFilling();
        if (Health <= 0)
        {
            
            Health = 0;
            EnemyHealth.text = "Enemy Health = " + Health;
            Debug.Log("Hedef Kullanilamaz Hale Geldi.!!! Guncel Can= "+Health);
        }
    }

    private void lifeFilling()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Health = 100;
            EnemyHealth.text = "Enemy Health = " + Health;
            Debug.Log("Can Dolduruldu, Guncel Can = " + Health);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        hitText.gameObject.SetActive(true);
        if (Health <= 0)
        {
            Health = 0;
            EnemyHealth.text = "Enemy Health = " + Health;
            Debug.Log("Hedef Vuruldu, Guncel Can= " + Health);
        }
        else
        {

            if (other.transform.name == "Ak47Bullet")
            {
                
                Health -= 5;
                EnemyHealth.text = "Enemy Health = " + Health;
                Debug.Log("Hedef Vuruldu, Guncel Can= " + Health);

            }
            else if (other.transform.name == "DeagleBullet")
            {
                Health -= 40;
                EnemyHealth.text = "Enemy Health = " + Health;
                Debug.Log("Hedef Vuruldu, Guncel Can= " + Health);

            }
        }
        
        StartCoroutine(Wait(0.1f));
        

    }
    IEnumerator Wait(float value)
    {
        hitText.gameObject.SetActive(true);
        yield return new WaitForSeconds(value);
        hitText.gameObject.SetActive(false);
    }
}
