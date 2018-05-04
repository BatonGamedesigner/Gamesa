using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Класс отвечающий за иттерацию кнопок меню
public class MenuControl : MonoBehaviour {

	//Запуск первого уровня, если нажата кнопка игры
    public void PlayPressed()
    {
        SceneManager.LoadScene(1);
    }
    //Выход из игры, если нажата кнопка выхода
    public void ExitPressed()
    {
        Application.Quit();
    }
	// Use this for initialization
}
