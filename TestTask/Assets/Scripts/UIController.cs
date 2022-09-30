using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject touchBlock;
    [SerializeField] private GameObject slider;
    [SerializeField] private GameObject loseTxt;
    [SerializeField] private GameObject winTxt;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private Image sliderFill;
    private GameObject currentGO;

    public static Action BarOn;
	public static Action BarOff;
	public static Action<GameObject> SetNewGO;

    private int objectCount = 3;
    
    private void Awake()
    {
		BarOn += PlusingSliderValue; 
		BarOff += MinusingSliderValue;
        SetNewGO += NewGo;
    }

    public void PlusingSliderValue()
    {
        if (sliderFill.fillAmount <= 0.01f)
        {
            touchBlock.SetActive(true);
            slider.SetActive(true);
        }

        sliderFill.fillAmount += 0.01f;

        if(sliderFill.fillAmount >= 0.99f)
        {
            touchBlock.SetActive(false);
            slider.SetActive(false);
            currentGO.GetComponent<Figures>().MoveAbilityOff();
            objectCount--;
            if(objectCount == 0) { WinLevel(); }
        }
    }
    public void MinusingSliderValue()
    {
        Debug.Log(0);
        sliderFill.fillAmount -= 0.01f;
    }

    private void NewGo(GameObject go)
    {
        currentGO = go;
        sliderFill.fillAmount = 0f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void LoseLevel()
    {
        loseTxt.SetActive(true);
        slider.SetActive(false);
    }
    public void WinLevel()
    {
        winTxt.SetActive(true);
    }

    private void OnDisable()
    {
        BarOn -= PlusingSliderValue;
        BarOff -= MinusingSliderValue;
        SetNewGO -= NewGo;
    }
}
