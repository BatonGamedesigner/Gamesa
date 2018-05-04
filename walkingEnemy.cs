using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
//Класс, реализующий ходьбу врага
public class walkingEnemy : MonoBehaviour { 
    public Rigidbody2D sc = new Rigidbody2D();

    public float i = 0;
    public float speed = 7;
    public float direction = -1;
    //При столкновении с обьектом с тегом Swap, разворачивает напарвление движения
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Swap")
            direction = direction*-1;
    }
    //Перемещение тела
    void Update()
    {
        sc.velocity = new Vector2(speed * direction, sc.velocity.y);
        i++;
    }
}
