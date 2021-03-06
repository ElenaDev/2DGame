using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject ghostPrefab;//prefab del fantasma (el proyectil del player)
    public GameObject duckPrefab;
    public Transform posRight;
    public Transform posLeft;
    public float timeBetweenProjectiles;//tiempo entre fantasmas
    public float timeBetweenProjectilesDuck;//tiempo entre patos

    SpriteRenderer spriteRenderer;
    float timer;
    float timerDuck;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        //timer = timer + Time.deltaTime;
        timer += Time.deltaTime;
        timerDuck += Time.deltaTime;
        //si pulso el botón izquierdo del ratón
        if(Input.GetMouseButtonDown(0) && timer >= timeBetweenProjectiles)
        {
            CreateProjectileGhostOrDuck(ghostPrefab);//llamada a la función
        }
        else if (Input.GetMouseButtonDown(1) && timer >= timeBetweenProjectilesDuck)
        {
            CreateProjectileGhostOrDuck(duckPrefab);//llamada a la función
        }
    }
    //declaración de la función
    void CreateProjectileGhostOrDuck(GameObject _object)
    {
        if (_object == ghostPrefab) timer = 0;
        else timerDuck = 0;

        GameObject clonePrefab;

        if (spriteRenderer.flipX == false)//significa que el player está mirando a la derecha
        {
            //Instantiate lo usamos para crear clones de un prefab
            //newQuaternion significa que no le doy rotación
            clonePrefab = Instantiate(_object, posRight.position, new Quaternion());
            if (_object == ghostPrefab) clonePrefab.GetComponent<Projectile>().direction = 1;
            else clonePrefab.GetComponent<ProjectileDuck>().direction = 1;
            clonePrefab.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            clonePrefab = Instantiate(_object, posLeft.position, new Quaternion());
            if (_object == ghostPrefab) clonePrefab.GetComponent<Projectile>().direction = -1;
            else clonePrefab.GetComponent<ProjectileDuck>().direction = -1;
            clonePrefab.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    //OTRA FORMA DE HACERLO MENOS ÓPTIMA
    void CreateProjectile()
    {
        timer = 0;
        GameObject clonePrefab;//variable local para guardarme el clone del prefab que estoy instanciando

        if(spriteRenderer.flipX == false)//significa que el player está mirando a la derecha
        {
            //Instantiate lo usamos para crear clones de un prefab
            //newQuaternion significa que no le doy rotación
            clonePrefab = Instantiate(ghostPrefab, posRight.position, new Quaternion());
            clonePrefab.GetComponent<Projectile>().direction = 1;
            clonePrefab.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            clonePrefab = Instantiate(ghostPrefab, posLeft.position, new Quaternion());
            clonePrefab.GetComponent<Projectile>().direction = -1;
            clonePrefab.GetComponent<SpriteRenderer>().flipX = false;
        }

    }
    void ProjectileDuck()
    {
        timerDuck = 0;
        GameObject clonePrefab;//variable local para guardarme el clone del prefab que estoy instanciando

        if (spriteRenderer.flipX == false)//significa que el player está mirando a la derecha
        {
            //Instantiate lo usamos para crear clones de un prefab
            //newQuaternion significa que no le doy rotación
            clonePrefab = Instantiate(duckPrefab, posRight.position, new Quaternion());
            clonePrefab.GetComponent<ProjectileDuck>().direction = 1;
            clonePrefab.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            clonePrefab = Instantiate(duckPrefab, posLeft.position, new Quaternion());
            clonePrefab.GetComponent<ProjectileDuck>().direction = -1;
            clonePrefab.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    
}
