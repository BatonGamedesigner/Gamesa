using UnityEngine;
using System.Collections;
//Класс, отвечающий за вращение модели пилы
public class saw : MonoBehaviour
{
    public float speed = -150;
    public bool isMovable = false;
    public float distance = 0;
    public float direction = 1;

    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, speed * Time.deltaTime));

    }
}