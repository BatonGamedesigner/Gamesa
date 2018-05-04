using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))] //Необходимый компонент

public class Player2dControl : MonoBehaviour
{

    public enum ProjectAxis { onlyX = 0, xAndY = 1 }; //Енум для выбора осей перемещения (Только по Х, или по обеим)
    public ProjectAxis projectAxis = ProjectAxis.xAndY; //Присвоение оси
    public float speed = 100000; //Базовая скорость перемещения
    public float addForce = 50000; //Скорость ускорения
    public KeyCode leftButton = KeyCode.A;                   //>
    public KeyCode rightButton = KeyCode.D;                 //|
    public KeyCode jumpButton = KeyCode.Space;              //|
    public KeyCode downButton = KeyCode.S;                 //  > Клавиши на клавиатуре
    public KeyCode addForceButton = KeyCode.LeftShift;      //|
    public KeyCode quitMainMenu = KeyCode.Escape;           //| 
    public KeyCode reloadLevel = KeyCode.R;                  //>
    private bool isFacingRight = false;
    public int i = 0; //счетчик, отсчитывающий количество кадров в прыжке, при достижении максимального значения останавливает прыжок
    public int j = 0; //Счетчик отсчитывающий количество кадров в прыжке, при достижении максимального значения перезагружает уровень
    public float maxJ = 600;
    public float maxI = 300;
    private Vector3 direction; //Направление движения
    private float vertical;
    private float horizontal;
    private Rigidbody2D body; // компонент Rigidbody2D
    private float rotationY;
    private bool jump;
    private bool alreadyJumped; //Прыгнул ли уже игрок
    private int jumpCoords=0; 
    public int NextLevel = 0; //Порядковый номер следующего уровня в иерархии
    public int CurrentLevel = 1; //Порядковый номер данного уровня в иерархии

    // Метод начала, запускается при старте проекта, запрещает персонажу вращаться, а так же устанавливает гравитацию
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.fixedAngle = true;

        if (projectAxis == ProjectAxis.xAndY)
        {
            body.gravityScale = 4000;
            body.drag = 10;
        }
    }
    //Метод, обрабатывающий столкновения обьекта "Игрок" с другими обьектами
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy") //При столкновении с врагом, перезагружает уровень
            SceneManager.LoadScene(CurrentLevel);
        if (col.gameObject.tag == "Finish") //При столкновении с концом уровня (Батей), Загружает следующий уровень
            SceneManager.LoadScene(NextLevel);
        if (col.gameObject.tag == "Ground"){ //При столкновлении с поверхностью обнуляет переменные и присваивает оси положительное значение
            alreadyJumped = false;
            i = 0;
            j = 0;
            vertical = 1;
        }
    }
    //Единичное обновление кадров, присваивание значений полям Body
    void FixedUpdate()
    {
        body.AddForce(direction * body.mass * speed);

        if (Mathf.Abs(body.velocity.x) > speed / 100f)
        {
            body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * speed / 100f, body.velocity.y);
        }

        if (projectAxis == ProjectAxis.xAndY)
        {
            if (Mathf.Abs(body.velocity.y) > speed / 100f)
            {
                body.velocity = new Vector2(body.velocity.x, Mathf.Sign(body.velocity.y) * speed / 100f);
            }
        }
        else
        {
            if (Input.GetKey(addForceButton) && jump)
            {
                body.velocity = new Vector2(0, addForce);
            }
        }
    }

    //Метод, разворачивающий спрайт персонажа при ходьбе влево-вправо в нужном направлении
    void Flip()
    {
        if (projectAxis == ProjectAxis.xAndY)
        {
            isFacingRight = !isFacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    void Update()
    {
    	//Перезагрузка уровня при нажатии на клавишу
        if (Input.GetKeyDown(reloadLevel))
        {
            SceneManager.LoadScene(CurrentLevel);
        }
        //Выход в главное меню при нажатии на клавишу
        if(Input.GetKey(quitMainMenu)){
            SceneManager.LoadScene(0);
        }
        //Если направления движения по вертикали отрицательное, j Увеличивается
        if (vertical == -1) j++;
        //Перезагружает уровень, если персонаж пролетает больше максимального значения
        if (j > maxJ) SceneManager.LoadScene(CurrentLevel);
        //Прыжок персонажа
        if (Input.GetKey(jumpButton)) {
            vertical = 1;
           
            if (i >= 0 && i <= maxI)
            {
                vertical = 1;
                i++;
                //Если i равен максимальному, меняет направление движения по вертикали на отрицательное
                if (i == maxI)
                {
                    vertical = -1;
                    alreadyJumped = true;

                    if (i > 0 && alreadyJumped)
                    {
                        i--;
                    }

                }
                
            }
        }
        
        else if (Input.GetKey(downButton)) vertical = -1; else vertical = 0;
       	//Поворот влево
        if (Input.GetKey(leftButton)) horizontal = -1;
        //Поворот вправо, если ничего не нажато то стоит на месте
        else if (Input.GetKey(rightButton)) horizontal = 1; else horizontal = 0;
        
        if(projectAxis == xAndY)
        {
            if (Input.GetKeyDown(addForceButton)) speed += addForce; else if (Input.GetKeyUp(addForceButton)) speed -= addForce;
            direction = new Vector2(horizontal, vertical);
        }
        //Поворот модели
        if (horizontal > 0 && !isFacingRight) Flip(); else if (horizontal < 0 && isFacingRight) Flip();
    }
}