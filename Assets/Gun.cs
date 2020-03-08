using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Gun(string gunName,bool isAuto,int magazineAmount,int Damage,int ReloadingTime,GameObject bulletOut)
    {
        this.gunName = gunName;
        this.isAuto = isAuto;
        this.magazineAmount = magazineAmount;
        this.Damage = Damage;
        this.ReloadingTime = ReloadingTime;
        this.bulletOut = bulletOut;

    }
    public string gunName { get; set; }
    public bool isAuto { get; set; }
    public int magazineAmount { get; set; }
    public int Damage { get; set; }
    public int ReloadingTime { get; set; }
    public GameObject bulletOut { get; set; }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
