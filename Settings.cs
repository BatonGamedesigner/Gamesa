using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Класс, отвечающий за настройки игры
public class Settings : MonoBehaviour {

    private bool isFullScreen = true; //Фулскрин
    Resolution[] rsl; //массив разрешений
    List<string> resolutions; //лист разрешений
    public Dropdown dropdown; //выпадающие списки

    //заполнение списка разрешений
    public void Awake()
    {
        resolutions = new List<string>();
        rsl = Screen.resolutions;
        foreach (var i in rsl)
        {
            resolutions.Add(i.width + "x" + i.height);
        }
        dropdown.ClearOptions();
        dropdown.AddOptions(resolutions);
    }
    //Смена разрешения экрана
    public void Resolution(int r)
    {
        Screen.SetResolution(rsl[r].width, rsl[r].height, isFullScreen);
    }
    //Качество изображения
    public void Quality(int q)
    {
        QualitySettings.SetQualityLevel(q);
    }
    //Изменения оконного режима
    public void FulllScreenToggle()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

}
