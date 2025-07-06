using UnityEngine;

[CreateAssetMenu(fileName = "ClickerConfig", menuName = "Configs/ClickerConfig")]
public class ClickerConfig : ScriptableObject
{
    [Header("Energy")]
    public int maxEnergy = 1000;
    public int regenAmount = 10;
    public float regenInterval = 10f;
    public int costEnergy = 1;

    [Header("Currency")]
    public int coinsPerClick = 1;

    [Header("AutoGrind")]
    public float autoGrindInterval = 3f;

    [Header("UI Animation")]
    public float coinFlyDuration = 0.15f;
    public float buttonClickMoveAmount = 10f;
    public float buttonClickDuration = 0.05f;
}