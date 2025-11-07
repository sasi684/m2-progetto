using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1ProjectTest : MonoBehaviour
{

    [SerializeField] private Hero a;
    [SerializeField] private Hero b;

    // Update is called once per frame
    void Update()
    {
        if (!a.IsAlive() || !b.IsAlive())
            return;

        Hero attacker;
        Hero defender;

        Stats aStats = Stats.Sum(a.GetBaseStats(), a.GetWeapon().GetBonusStats());
        Stats bStats = Stats.Sum(b.GetBaseStats(), b.GetWeapon().GetBonusStats());

        if(aStats.spd > bStats.spd)
        {
            attacker = a;
            defender = b;
        }
        else
        {
            attacker = b;
            defender = a;
        }
        Attack(attacker, defender);

        if (attacker == a)
        {
            attacker = b;
            defender = a;
        }
        else
        {
            attacker = a;
            defender = b;
        }
        Attack(attacker, defender);
    }

    public void Attack(Hero attacker, Hero defender)
    {
        if (attacker.GetHP() <= 0)
            return;
        Debug.Log($"Attaca {attacker.GetName()} e difende {defender.GetName()}");

        if (GameFormulas.HasHit(attacker.GetBaseStats(), defender.GetBaseStats()))
        {
            if (GameFormulas.HasElementAdvantage(attacker.GetWeapon().GetElem(), defender))
                Debug.Log("WEAKNESS");
            if (GameFormulas.HasElementDisadvantage(attacker.GetWeapon().GetElem(), defender))
                Debug.Log("RESIST");

            int damage = GameFormulas.CalculateDamage(attacker, defender);
            Debug.Log($"Sono stati effettuati {damage} danni!");
            defender.TakeDamage(damage);

            if (!defender.IsAlive())
            {
                Debug.Log($"Il vincitore è {attacker.GetName()}");
                return;
            }
        }
    }
}
