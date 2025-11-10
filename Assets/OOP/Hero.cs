using UnityEngine;

// CLASSE HERO USATA PER ISTANZIARE
[System.Serializable] public class Hero
{
    [SerializeField] private string name;
    [SerializeField] private int hp;
    [SerializeField] private Stats baseStats;
    [SerializeField] private ELEMENT resistance;
    [SerializeField] private ELEMENT weakness;
    [SerializeField] private Weapon weapon;

    // METODO COSTRUTTORE
    public Hero(string name, int hp, Stats baseStats, ELEMENT resistance, ELEMENT weakness, Weapon weapon)
    {
        this.name = name;
        this.hp = hp;
        this.baseStats = baseStats;
        this.resistance = resistance;
        this.weakness = weakness;
        this.weapon = weapon;
    }

    // GETTER
    public string GetName() => name;
    public int GetHP() => hp;
    public Stats GetBaseStats() => baseStats;
    public ELEMENT GetResistance() => resistance;
    public ELEMENT GetWeakness() => weakness;
    public Weapon GetWeapon() => weapon;

    // SETTER
    public void SetName(string name)
    {
        this.name = name;
    }
    public void SetHP(int hp)
    {
        this.hp = hp;
    }
    public void SetBaseStats(Stats baseStats)
    {
        this.baseStats = baseStats;
    }
    public void SetResistance(ELEMENT resistance)
    {
        this.resistance = resistance;
    }
    public void SetWeakness(ELEMENT weakness)
    {
        this.weakness = weakness;
    }
    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }

    // FUNZIONI PER INCREMENTO/DECREMENTO DELLA SALUTE
    public void AddHP(int amount)
    {
        SetHP(hp + amount);
    }

    public void TakeDamage(int damage)
    {
        AddHP(-damage);
    }

    // FUNZIONE CONTROLLA SE L'EROE E' VIVO (HP > 0)
    public bool IsAlive()
    {
        return hp > 0 ? true : false;
    }
}
