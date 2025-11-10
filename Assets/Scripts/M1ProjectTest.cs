using UnityEngine;

// SCRIPT COMPONENTE
public class M1ProjectTest : MonoBehaviour
{
    // DICHIARAMO GLI EROI CHE VERRANNO POI DICHIARATI NELL'INSPECTOR
    [SerializeField] private Hero a;
    [SerializeField] private Hero b;

    void Update()
    {
        if (!a.IsAlive() || !b.IsAlive()) // SE UNO DEI DUE EROI E' MORTO, INTERROMPI L'UPDATE
            return;

        Hero attacker;
        Hero defender;

        Stats aStats = Stats.Sum(a.GetBaseStats(), a.GetWeapon().GetBonusStats());
        Stats bStats = Stats.Sum(b.GetBaseStats(), b.GetWeapon().GetBonusStats());

        if(aStats.spd > bStats.spd) // ATTACCA PER PRIMO L'EROE CON VELOCITA' PIU' ALTA
        {
            attacker = a;
            defender = b;
        }
        else
        {
            attacker = b;
            defender = a;
        }
        Attack(attacker, defender); // RICHIAMA LA FUNZIONE DI ATTACCO

        if (attacker == a) // CAMBIA I RUOLI DI ATTACCANTE E DIFENSORE
        {
            attacker = b;
            defender = a;
        }
        else
        {
            attacker = a;
            defender = b;
        }
        Attack(attacker, defender); // VIENE SFERRATO IL SECONDO ATTACCO
    }

    // FUNZIONE CHE DESCRIVE GLI STEP DELL'ATTACCO
    public void Attack(Hero attacker, Hero defender)
    {
        if (!attacker.IsAlive()) // SE L'ATTACCANTE E' MORTO, SI ESCE DALLA FUNZIONE
            return;
        Debug.Log($"Attaca {attacker.GetName()} e difende {defender.GetName()}");

        if (GameFormulas.HasHit(attacker.GetBaseStats(), defender.GetBaseStats())) // SE L'ATTACCO VA A BUON FINE, SI PROCEDE CON IL CALCOLO DEI DANNI
        {
            if (GameFormulas.HasElementAdvantage(attacker.GetWeapon().GetElem(), defender)) // ESPLICITIAMO SE VIENE COLPITA LA DEBOLEZZA DEL DIFENSORE
                Debug.Log("WEAKNESS");
            if (GameFormulas.HasElementDisadvantage(attacker.GetWeapon().GetElem(), defender)) // ESPLICITIAMO SE VIENE COLPITA LA RESISTENZA DEL DIFENSORE
                Debug.Log("RESIST");

            int damage = GameFormulas.CalculateDamage(attacker, defender); // CALCOLIAMO I DANNI DA INFERIRE, LI ESPLICITIAMO E SOTTRAIAMO HP AL DIFENSORE PARI AI DANNI
            Debug.Log($"Sono stati effettuati {damage} danni!");
            defender.TakeDamage(damage);

            if (!defender.IsAlive()) // SE IL DIFENSORE NON SOPRAVVIVE ALL'ATTACCO, STAMPIAMO CHE L'ATTACCANTE E' IL VINCITORE
            {
                Debug.Log($"Il vincitore è {attacker.GetName()}");
                return;
            }
        }
    }
}
