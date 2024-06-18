using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;

public class ClickerGame : MonoBehaviour
{
    public Button clickButton;
    public Text scoreText;
    public Button upgradeButton;
    public Text upgradeCostText;

    private BigInteger score = 0;
    private BigInteger clickPower = 1;

    public BigInteger initialUpgradeCost = 10;
    public BigInteger upgradeCostIncrement = 5;
    private BigInteger currentUpgradeCost;

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
    }

    void OnUpgradeButtonClick()
    {
        if (CanUpgrade())
        {
            PurchaseUpgrade();
        }
    }

    bool CanUpgrade()
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
        scoreText.text = FormatBigNumber(score) + " Gold";
    }

    void UpdateUpgradeCostText()
    {
        upgradeCostText.text = "ºñ¿ë: " + FormatBigNumber(currentUpgradeCost) + " Gold";
    }

    IEnumerator AutoClick()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoClickInterval);
            IncrementScore();
        }
    }

    string FormatBigNumber(BigInteger number)
    {
        if (number < 1000)
        {
            return number.ToString();
        }

        string[] units = { "", "K", "M", "B", "T", "Q", "Qi", "Sx", "Sp", "Oc", "No", "Dc" };
        int unitIndex = 0;
        decimal decimalNumber = (decimal)number;

        while (decimalNumber >= 1000)
        {
            decimalNumber /= 1000;
            unitIndex++;
        }

        return decimalNumber.ToString("F2") + units[unitIndex];
    }
}
