using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClickerGame : MonoBehaviour
{
    public Button clickButton;
    public Text scoreText;
    private int score = 0;
    public float autoClickInterval = 1.0f;
    public Button upgradeButton;
    private int clickPower = 1;
    private Animator clickButtonAnimator;

    void Start()
    {
        clickButton.onClick.AddListener(OnButtonClick);
        upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
        clickButtonAnimator = clickButton.GetComponent<Animator>();
        UpdateScoreText();
        StartCoroutine(AutoClick());
    }

    void OnButtonClick()
    {
        IncrementScore();
        if (clickButtonAnimator != null)
        {
            clickButtonAnimator.SetTrigger("Click");
        }
    }

    void IncrementScore()
    {
        score += clickPower;
        UpdateScoreText();
        Debug.Log("Score incremented. Current score: " + score); // ����� �α� �߰�
    }

    void OnUpgradeButtonClick()
    {
        if (score >= 10) // ���׷��̵� ���
        {
            score -= 10;
            clickPower++;
            UpdateScoreText();
            Debug.Log("Upgrade purchased. New click power: " + clickPower); // ����� �α� �߰�
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString() + " Gold";
    }

    IEnumerator AutoClick()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoClickInterval);
         
            IncrementScore();
        }
    }
}
