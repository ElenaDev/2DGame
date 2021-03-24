using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int level = 2;
    public int health = 30;

    private void Awake()
    {
        if(PlayerPrefs.HasKey("HealthPlayer"))//si existe la key "HealthPlayer"
        {
            LoadData();
        }
       
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            health++;
            Debug.Log(health);
            SaveData();
        }
    }
    void SaveData()
    {
         //PlayerPrefs.SetInt("HealthPlayer", health);//guardar datos
        SaveSystem.SavePlayer(this);
    }
    void LoadData()
    {
        //health = PlayerPrefs.GetInt("HealthPlayer");//cojo el valor asociado a la key HealthPlayer

        PlayerData data = SaveSystem.LoadPlayer();
        level = data.level;
        health = data.health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;
    }
}
