using UnityEngine;

[System.Serializable] public class Weapon
{
    public enum DAMAGE_TYPE
    {
        PHYSICAL = 0,
        MAGICAL = 1,
    }

    [SerializeField] private string name;
    [SerializeField] private DAMAGE_TYPE dmgType;
    [SerializeField] private ELEMENT elem;
    [SerializeField] private Stats bonusStats;

    public Weapon(string name, DAMAGE_TYPE dmgType, ELEMENT elem, Stats bonusStats)
    {
        this.name = name;
        this.dmgType = dmgType;
        this.elem = elem;
        this.bonusStats = bonusStats;
    }

    //GETTER
    public string GetName() => name;
    public DAMAGE_TYPE GetDmgTyp() => dmgType;
    public ELEMENT GetElem() => elem;
    public Stats GetBonusStats() => bonusStats;

    //SETTER
    public void SetName(string name)
    {
        this.name = name;
    }
    public void SetDmgType(DAMAGE_TYPE dmgType)
    {
        this.dmgType = dmgType;
    }
    public void SetElem(ELEMENT elem)
    {
        this.elem = elem;
    }
    public void SetBonusStats(Stats bonusStats)
    {
        this.bonusStats = bonusStats;
    }
}
