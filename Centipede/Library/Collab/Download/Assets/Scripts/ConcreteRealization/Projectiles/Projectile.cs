using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool hit { get; private set; }
    private bool shoot;

    private int damage;
    private int damageModifier;

    private float speed;
    private float speedMultiplier;
    private Vector3 direction;

    public void SetParams(Color color, float baseSpeed, int baseDamage)
    {
        transform.GetComponent<MeshRenderer>().material.color = color;
        speed = baseSpeed;
        damage = baseDamage;
    }

    public void Shoot(Vector3 position, Vector3 direction, float speedMultiplier = 1, int damageModifier = 0)
    {
        shoot = true;
        hit = false;
        gameObject.SetActive(true);
        transform.position = position;
        this.direction = direction;

        this.speedMultiplier = speedMultiplier;
        this.damageModifier = damageModifier;
    }

    public int Hit()
    {
        hit = true;
        shoot = false;
        gameObject.SetActive(false);

        return damage + damageModifier;
    }

    private void Update()
    {
        if (shoot && !hit)
        {
            transform.position += direction * speed * speedMultiplier * Time.deltaTime;
        }
    }
}
