using UnityEngine;

public static class GameFormulas
{
    public static bool HasElementAdvantage(ELEMENT attackElement, Hero defender)
    {
        return attackElement == defender.GetWeakness()? true : false;
    }

    public static bool HasElementDisadvantage(ELEMENT attackElement, Hero defender)
    {
        return attackElement == defender.GetResistance() ? true : false;
    }

    public static float EvaluateElementalModifier(ELEMENT attackElement, Hero defender)
    {
        if (HasElementAdvantage(attackElement, defender))
            return 1.5f;
        else if (HasElementDisadvantage(attackElement, defender))
            return 0.5f;
        return 1f;
    }

    public static bool HasHit(Stats attacker, Stats defender)
    {
        int hitChance = attacker.aim - defender.eva;
        if (Random.Range(0, 100) > hitChance)
        {
            Debug.Log("MISS");
            return false;
        }
        return true;
    }

    public static bool IsCrit(int critValue)
    {
        if (Random.Range(0, 100) < critValue)
        {
            Debug.Log("CRIT");
            return true;
        }
        return false;
    }

    public static int CalculateDamage(Hero attacker, Hero defender)
    {
        Stats attackerStats = Stats.Sum(attacker.GetBaseStats(), attacker.GetWeapon().GetBonusStats());
        Stats defenderStats = Stats.Sum(defender.GetBaseStats(), defender.GetWeapon().GetBonusStats());

        int defense;
        if (attacker.GetWeapon().GetDmgTyp() == Weapon.DAMAGE_TYPE.PHYSICAL)
            defense = defenderStats.def;
        else
            defense = defenderStats.res;

        int damage = attackerStats.atk - defense;
        damage *= (int)EvaluateElementalModifier(attacker.GetWeapon().GetElem(), defender);
        damage = IsCrit(attackerStats.crt) ? damage * 2 : damage;

        return damage < 0 ? 0 : damage;
    }
}
