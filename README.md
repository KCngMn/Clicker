# Clicker Game

Clicker Game은 사용자가 버튼을 클릭하여 점수를 얻고 업그레이드를 구매하여 점수를 더 많이 얻을 수 있는 간단한 2D 클릭커 게임입니다. 이 프로젝트는 Unity 엔진을 사용하여 개발되었으며, 큰 숫자를 처리하기 위해 `BigInteger`를 사용합니다.

## 주요 기능

1. **클릭 이벤트 처리**
    - 사용자가 화면을 클릭할 때마다 점수가 증가합니다.

2. **자동 클릭 기능**
    - 일정 시간마다 자동으로 클릭이 발생하여 점수가 증가합니다.

3. **점수 시스템**
    - 클릭이나 자동 클릭을 통해 얻은 점수가 화면에 표시됩니다.

4. **아이템 및 업그레이드 시스템**
    - 사용자가 점수를 사용하여 클릭 파워를 증가시키는 업그레이드를 구매할 수 있습니다.
    - 업그레이드 비용은 업그레이드를 구매할 때마다 증가합니다.

5. **큰 숫자 처리**
    - 매우 큰 숫자를 처리하기 위해 `BigInteger`를 사용합니다.
    - 큰 숫자는 "1K", "1M" 등의 형식으로 표시되며, 소수점 두 자리까지 표시됩니다.

## 코드 설명

### ClickerGame.cs

```csharp
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
        scoreText.text = FormatBigNumber(score) + " Gold";
    }

    void UpdateUpgradeCostText()
    {
        upgradeCostText.text = "비용: " + FormatBigNumber(currentUpgradeCost) + " Gold";
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
