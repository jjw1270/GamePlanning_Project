using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Start_TEXT_FadeInOut : MonoBehaviour
{
    public Text text1, text2, text3, text4, text5;
    int textCount = 0;
    void Start()
    {
        text1.color = new Color(text1.color.r, text1.color.g, text1.color.b, 0);
        text2.color = new Color(text2.color.r, text2.color.g, text2.color.b, 0);
        text3.color = new Color(text3.color.r, text3.color.g, text3.color.b, 0);
        text4.color = new Color(text4.color.r, text4.color.g, text4.color.b, 0);
        text5.color = new Color(text5.color.r, text5.color.g, text5.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(textCount == 0){
            textCount++;
            StartCoroutine(FadeInText(text1));
        }
        else if(Input.GetMouseButtonDown(0) && textCount == 1){
            textCount++;
            StartCoroutine(FadeOutandInText(text1, text2));
        }
        else if(Input.GetMouseButtonDown(0) && textCount == 2){
            textCount++;
            StartCoroutine(FadeOutandInText(text2, text3));
        }
        else if(Input.GetMouseButtonDown(0) && textCount == 3){
            textCount++;
            StartCoroutine(FadeOutandInText(text3, text4));
        }
        else if(Input.GetMouseButtonDown(0) && textCount == 4){
            textCount++;
            StartCoroutine(FadeOutandInText(text4, text5));
        }
        else if(Input.GetMouseButtonDown(0) && textCount == 5){
            SceneManager.LoadScene("MainScene1");
        }
    }

    IEnumerator FadeInText(Text text){
        while(text.color.a < 1.0f){
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / 2.0f));
            yield return null;
        }
    }
    IEnumerator FadeOutandInText(Text prevText, Text curText){
        while(prevText.color.a > 0.0f){
            prevText.color = new Color(prevText.color.r, prevText.color.g, prevText.color.b, prevText.color.a - (Time.deltaTime / 4.0f));
            yield return null;
        }
        StartCoroutine(FadeInText(curText));
    }
}
