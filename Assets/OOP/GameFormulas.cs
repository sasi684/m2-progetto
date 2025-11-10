using UnityEngine;

// CLASSE STATICA CONTENENTE LE LOGICHE DI CALCOLO DEI DANNI DI UN ATTACCO
public static class GameFormulas
{
    // VERIFICHIAMO CHE L'ELEMENTO DI ATTACCO SIA UGUALE ALLA DEBOLEZZA DEL DIFENSORE (IN CASO DI NONE, RESTITUISCE FALSE DI DEFAULT)
    public static bool HasElementAdvantage(ELEMENT attackElement, Hero defender)
    {
        if (attackElement == ELEMENT.NONE) return false;
        return attackElement == defender.GetWeakness()? true : false;
    }

    // VERIFICHIAMO CHE L'ELEMENTO DI ATTACCO SIA UGUALE ALLA RESISTENZA DEL DIFENSORE (IN CASO DI NONE, RESTITUISCE FALSE DI DEFAULT)
    public static bool HasElementDisadvantage(ELEMENT attackElement, Hero defender)
    {
        if(attackElement == ELEMENT.NONE) return false;
        return attackElement == defender.GetResistance() ? true : false;
    }

    // SETTIAMO QUELLO CHE SARA' IL MOLTIPLICATORE PER IL DANNO
    public static float EvaluateElementalModifier(ELEMENT attackElement, Hero defender)
    {
        if (HasElementAdvantage(attackElement, defender))
        {
            Debug.Log("Hai colpito una debolezza");
            return 1.5f;
        }
        else if (HasElementDisadvantage(attackElement, defender))
            return 0.5f;
        return 1f;
    }

    // SCRIPT PER CALCOLARE LA MESSA A SEGNO DELL'ATTACCO FACENDO LOGICA SULLE STATS DI MIRA DELL'ATTACCANTE E LE STATS DI SCHIVATA DEL DIFENSORE
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

    // VERIFICHIAMO SE L'ATTACCO E' UN COLPO CRITICO (VERRA' POI PASSATO COME ARGOMENTO LA STATS CRT DELL'ATTACCANTE)
    public static bool IsCrit(int critValue)
    {
        if (Random.Range(0, 100) < critValue)
        {
            Debug.Log("CRIT");
            return true;
        }
        return false;
    }

    // FUNZIONE GENERALE PER IL CALCOLO DEI DANNI
    public static int CalculateDamage(Hero attacker, Hero defender)
    {
        Stats attackerStats = Stats.Sum(attacker.GetBaseStats(), attacker.GetWeapon().GetBonusStats());
        Stats defenderStats = Stats.Sum(defender.GetBaseStats(), defender.GetWeapon().GetBonusStats());

        int defense;
        if (attacker.GetWeapon().GetDmgTyp() == Weapon.DAMAGE_TYPE.PHYSICAL)
            defense = defenderStats.def;
        else
            defense = defenderStats.res;

        float damage = attackerStats.atk - defense;

        //Debug.Log($"Danno crudo: {damage}"); DEBUG PER TESTARE IL DANNO PRIMA E DOPO IL CALCOLO CON IL MOLTIPLICATORE

        damage *= EvaluateElementalModifier(attacker.GetWeapon().GetElem(), defender);
        damage = IsCrit(attackerStats.crt) ? damage * 2 : damage;

        return (int)damage < 0 ? 0 : (int)damage;
    }
}
