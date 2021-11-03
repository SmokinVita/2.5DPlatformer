using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField]
    private TMP_Text _coinCount;
    [SerializeField]
    private TMP_Text _livesCount;
    
    public void UpdateCoinAmount(int coinAmount)
    {
        _coinCount.SetText($"Coins: {coinAmount}");
    }

    public void UpdateLivesCount(int livesAmount)
    {
        _livesCount.SetText($"Lives: {livesAmount}");
    }
}
