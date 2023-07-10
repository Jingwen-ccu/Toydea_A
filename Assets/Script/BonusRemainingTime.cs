using UnityEngine;

[System.Serializable]
public class BonusRemainingTime {
    #region Variables
    private float remainingTime = 5;
    private float _nextAttackTime;

    #endregion

    public bool IsNotOver => Time.time < _nextAttackTime;
    public void StartTimer() => _nextAttackTime = Time.time + remainingTime;
}