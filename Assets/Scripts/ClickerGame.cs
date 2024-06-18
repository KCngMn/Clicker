using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClickerGame : MonoBehaviour
{
    [Header("UI Elements")]
    public Button clickButton;
    public Text scoreText;
    public Button upgradeButton;
    public Text upgradeCostText;

    private int score = 0;
    private int clickPower = 1;

    [Header("Upgrade Settings")]
    public int initialUpgradeCost = 10;
    public int upgradeCostIncrement = 5;
    private int currentUpgradeCost;

    [Header("Auto Click Settings")]
    public float autoClickInterval = 1.0f;

    private Animator clickButtonAnimator;

    void Start()
    {
        currentUpgradeCost = initialUpgradeCost;
       
        clickButton.onClick.AddListener(OnButtonClick);
        upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
    
        clickButtonAnimator = clickButton.GetComponent<Animator>();

        UpdateScoreText();
        UpdateUpgradeCostText();

        StartCoroutine(AutoClick());
    }

    void OnButtonClick()
    {
        IncrementScore();
        PlayClickAnimation();
    }

    void PlayClickAnimation()
    {
        if (clickButtonAnimator != null)
        {
            clickButtonAnimator.SetTrigger("Click");
        }
    }

    void IncrementScore()
    {
        score += clickPower;
        UpdateScoreText();
        Debug.Log("Score incremented. Current score: " + score);
    }

    void OnUpgradeButtonClick()
    {
        if (CanAffordUpgrade())
        {
            PurchaseUpgrade();
        }
    }

    bool CanAffordUpgrade()
    {
        return score >= currentUpgradeCost;
    }

    void PurchaseUpgrade()
    {
        score -= currentUpgradeCost;
        clickPower++;
        currentUpgradeCost += upgradeCostIncrement;

        UpdateScoreText();
        UpdateUpgradeCostText();

      
    }

    void UpdateScoreText()
    {
        scoreText.text = $"{score} Gold";
    }

    void UpdateUpgradeCostText()
    {
        upgradeCostText.text = $"ºñ¿ë: {currentUpgradeCost} Gold";
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
