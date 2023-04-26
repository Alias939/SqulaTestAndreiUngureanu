using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerWriter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject inputField;
    public TMPro.TextMeshProUGUI inputText;

    public string answer;
    public GameObject GameController;


    public void Write()
    {
        //When button is pressed add to the input text the number of the button. 

        answer = inputText.text;
        if (answer.Length < 3)
        {
            answer += gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text;
            inputText.text = answer;
        }
       
        
    }

    public void Delete()
    {
        //Delete the last character of the input field

        answer = inputText.text;
        if (answer!="")
        {
            answer = answer.Substring(0, answer.Length - 1);
            inputText.text = answer;
        }
    }

    public void Check()
    {
        //Send over the input field text to the gamecontroller to check if the answer is correct, reset the input field

        answer = inputText.text;
        GameController.GetComponent<GameController>().checkAnswer(answer);
        inputText.text = "";
    }

    void Start()
    {
        inputText = inputField.GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
