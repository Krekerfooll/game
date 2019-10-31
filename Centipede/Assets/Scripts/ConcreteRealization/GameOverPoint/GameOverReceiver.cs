using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverReceiver : DamageReceiver
{
    private void OnCollisionEnter(Collision collision)
    {
        GameOverComponent gameOverComponent = collision.gameObject.GetComponent<GameOverComponent>();

        if (gameOverComponent != null)
            SetDamage(gameOverComponent.Contact());
    }
}
