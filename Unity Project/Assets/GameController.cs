using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject questionPrefab;

    public List<GameObject> questionList = new();

    public float fallSpeed;   //the speed at which the questions fall
    public float spawnTime;      //the interval of question spawining


    public int questionA, questionB;    //the two numbers of one question

    public GameObject canvas;     

    public int realAnswer;     

    public GameObject redLine;

    public GameObject scorePanel;
    public int score;
    public TMPro.TextMeshProUGUI scorePanelText;

    // Start is called before the first frame update
    void Start()
    {
       scorePanelText = scorePanel.GetComponent<TMPro.TextMeshProUGUI>();

        StartCoroutine(spawnQuestion(spawnTime));   //initiate spawning of the questions at the beginning of the game 
    }

    // Update is called once per frame
    void Update()
    {
        //every element in the question list is shifted downwards by the fallSpeed

        for (int i = 0; i < questionList.Count; i++)
        { 
            questionList[i].transform.position -= new Vector3(0, Screen.height*fallSpeed/100, 0);

            
        }


        //if the list is not empty and the question reaches the red line reset the game
        if (questionList.Count != 0)
        {
            if (questionList[0].transform.position.y < redLine.transform.position.y + 20f)
            {
               
                resetGame();
            }

        }

        //update the score panel
        scorePanelText.text = "Score\n" + score;

    }

    public void resetGame()
    {
        //destroy all of the questions on screen, reset score and reset list to empty
        for (int i = 0; i < questionList.Count; i++)
        {
            Destroy(questionList[i]);
            score = 0;
        }

        questionList = new();
    }


    public void checkAnswer(string answer)
    {
        //if there is a question on screen, get the two numbers, do the addition and check against the answer from the input screen
        if (questionList.Count != 0)
        {
            int a = questionList[0].GetComponent<TMPro.TextMeshProUGUI>().text[0] - '0';
            int b = questionList[0].GetComponent<TMPro.TextMeshProUGUI>().text[2] - '0';

            realAnswer = a + b;

            //if answer correct, remove the oldest element, shift list upwards and increment score
            if (realAnswer.ToString() == answer)
            {

                Destroy(questionList[0]);
                questionList.RemoveAt(0);
                score += 1;
            }
        }
      
    }


    IEnumerator spawnQuestion(float spawnTime)
    {
        while (true)
        {
            //after spawnTime instantiate a questionPrefab, add the refference to list to be able to manipulate later

            GameObject instantiatedQuestion = Instantiate(questionPrefab, new Vector3(164, 640, 0), Quaternion.identity);

            questionA = Random.Range(1, 10);
            questionB = Random.Range(1, 10);

            instantiatedQuestion.GetComponent<TMPro.TextMeshProUGUI>().text = questionA + "+" + questionB;

            instantiatedQuestion.transform.SetParent(canvas.transform);

            questionList.Add(instantiatedQuestion);

            yield return new WaitForSeconds(spawnTime);
        }
    }





}
