using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Main game class
/// </summary>
public class GameManager : MonoBehaviour, IObserver
{
    public DamageReceiver finalPoint;

    public ScriptableHitPoints playerScriptableHitPoints;
    private HitPoints playerHitPoints;
    public BaseImplementer[] baseImplementers;

    private void Start()
    {
        // centipede (or other enemies) deal damage to finalPoint, which give this damage to playerHitPoints

        playerHitPoints = playerScriptableHitPoints.CreateHitPointsClass();

        finalPoint.RegisterObserver(playerHitPoints);

        foreach (BaseImplementer item in baseImplementers)
        {
            item.Calculate(playerHitPoints.GetHitPoints());
            playerHitPoints.RegisterObserver(item);
        }

        playerHitPoints.RegisterObserver(this);
    }

    public void UpdateState(ISubject s)
    {
        // when playerHitPoints expired, just load GameOverScene
        if ((int)s.GetData() == 0)
            SceneManager.LoadScene("GameOverScene");
    }
}
