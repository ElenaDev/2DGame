using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;//necesario para pasar a binario el archivo

//atributo serializable, este atributo nos va a permitir guardar estos datos en un archivo
[System.Serializable]
public class PlayerData
{
    public int level;
    public int health;
    public float[] position;//para guardarme la posición


    //constructor de la clase
    //lo uso para inicializar los valores
    public PlayerData(Player player)
    {
        level = player.level;
        health = player.health;

        position = new float[3];//array de tamaño 3
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}

public static class SaveSystem
{
    public static void SavePlayer(Player player)
    {
        //lo que me va a permitir pasar archivo a binario
        BinaryFormatter formatter = new BinaryFormatter();

        //path para guardar el archivo que va a contener los datos
        string path = Application.persistentDataPath + "/player.data";

        //FileStream proporciona un flujo para un archivo, permite leer y escribir
        //Ese flujo son 3 pasos: crear, pasar info o coger info y cerrar
        FileStream stream = new FileStream(path, FileMode.Create);

        //creo una variable "tipo PlayerData" donde guardo el valor de las variables que tiene la clase Player
        PlayerData data = new PlayerData(player);

        //paso a binario el archivo
        formatter.Serialize(stream, data);

        //cierro el flujo
        stream.Close();

    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.data";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("path not found");
            return null;
        }
    }
}
