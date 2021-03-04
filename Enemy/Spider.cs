using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public GameObject acidEffectPrefab;
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }
    public void Damage()
    {
        if (isDead == false)
        {
            Health--;

            if (Health <= 0 && isDead == false)
            {
                isDead = true;
                anim.SetTrigger("Death");
                GameObject diamond = (GameObject)Instantiate(diamondPrefab, transform.position, Quaternion.identity);
                diamond.GetComponent<Diamond>().value = base.diamondValue;
            }
        }
    }
    public override void Update()
    {
    }

    public override void Movement()
    {
    }

    public void Attack()
    {
        Instantiate(acidEffectPrefab, transform.position, Quaternion.identity);
    }
}
