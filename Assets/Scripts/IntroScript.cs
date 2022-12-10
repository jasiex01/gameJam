using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class IntroScript : MonoBehaviour
{
    public int messageNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupDesc(int descNum)
    {
        Debug.Log("Nasz numer:" + descNum);
    }

    public void IncreaseNum()
    {
        messageNum += 1;
        Debug.Log("Number = " + messageNum);
    }

    public void ToIntro1()
    {
        Debug.Log("To Intro1");
        SceneManager.LoadScene("Intro1");  
    }

    public void ToIntro2()
    {
        Debug.Log("To Intro2");
        SceneManager.LoadScene("Intro2");  
    }

    public void ToIntro3()
    {
        SceneManager.LoadScene("Intro3");  
    }


}
