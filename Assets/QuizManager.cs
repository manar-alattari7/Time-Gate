using UnityEngine;
using TMPro;
using RTLTMPro;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class QuestionData
    {
        public string questionText;
        public string[] answers;
        public int correctAnswerIndex;
    }

    [Header("UI Elements")]
    //public TextMeshProUGUI questionTextUI;
    public RTLTextMeshPro questionTextUI;
    public Button[] answerButtons;
    public GameObject quizPanel;

    [Header("Quiz Data")]
    public QuestionData[] questions;

    private int currentQuestionIndex = 0;
    private int correctAnswersCount = 0;

    void Start()
    {
        StartQuiz();
        // إخفاء لوحة الأسئلة عند بداية اللعبة
       // quizPanel.SetActive(false);
    }

    // استدعاء هذه الدالة لبدء الاختبار
    public void StartQuiz()
    {
        currentQuestionIndex = 0;
        correctAnswersCount = 0;

        quizPanel.SetActive(true);

        if (questions.Length > 0)
        {
            DisplayQuestion();
        }
    }

    void DisplayQuestion()
    {
        QuestionData currentQuestion = questions[currentQuestionIndex];

        questionTextUI.text = currentQuestion.questionText;
        
      
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < currentQuestion.answers.Length)
            {
                answerButtons[i].gameObject.SetActive(true);

                 answerButtons[i]
                 .GetComponentInChildren<TextMeshProUGUI>()
                 .text = currentQuestion.answers[i];
                
               
                
                int index = i;

                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void OnAnswerSelected(int selectedIndex)
    {
        if (selectedIndex == questions[currentQuestionIndex].correctAnswerIndex)
        {
            Debug.Log("إجابة صحيحة!");
            correctAnswersCount++;
        }
        else
        {
            Debug.Log("إجابة خاطئة!");
        }

        currentQuestionIndex++;

        if (currentQuestionIndex < questions.Length)
        {
            DisplayQuestion();
        }
        else
        {
            EndQuiz();
        }
    }

    void EndQuiz()
    {
        quizPanel.SetActive(false);

        Debug.Log("انتهى الاختبار!");
        Debug.Log("عدد الإجابات الصحيحة: " + correctAnswersCount + " / " + questions.Length);
    }
}