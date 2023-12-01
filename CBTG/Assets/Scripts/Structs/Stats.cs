[System.Serializable]
public struct Stats
{
    public enum ChangeType { Add, Subtract, Overwrite, Multiply /*, IntoAFly*/ }
    public float speed, fireRate, bonusDamage, accuracy, chargeMult, cooldownMult, bonusShield,
        shieldDelayMult, shieldChargeMult, bonusHp;

    //This struct is an eyesore but it's the most intuitive way to handle stats I could think of
    public Stats(float s, float fr, float dm, float a, float chm, float com, float bs, float sdm, float scm, float bhp)
    {
        speed = s;
        fireRate = fr;
        bonusDamage = dm;
        accuracy = a;
        chargeMult = chm;
        cooldownMult = com;
        bonusShield = bs;
        shieldDelayMult = sdm;
        shieldChargeMult = scm;
        bonusHp = bhp;
    }
    static Stats DoOperation(Stats x, Stats y, float n)
    {
        return new Stats(x.speed + n * y.speed, x.fireRate + n * y.fireRate, x.bonusDamage + n * y.bonusDamage, x.accuracy + n * y.accuracy,
            x.chargeMult + n * y.chargeMult, x.cooldownMult + n * y.cooldownMult, x.bonusShield + n * y.bonusShield, x.shieldDelayMult + n * y.shieldDelayMult,
            x.shieldChargeMult * y.shieldChargeMult, x.bonusHp + n * y.bonusHp);
    }
    //+ and - operators reference the same operation, but - makes y negative.
    public static Stats operator +(Stats x, Stats y)
    {
        return DoOperation(x, y, 1f);
    }
    public static Stats operator -(Stats x, Stats y)
    {
        return DoOperation(x, y, -1f);
    }
    public static Stats operator *(Stats x, float m)
    {
        return new Stats(x.speed * m, x.fireRate * m, x.bonusDamage * m, x.accuracy * m, x.chargeMult * m, x.cooldownMult * m, x.bonusShield * m,
            x.shieldDelayMult * m, x.shieldChargeMult * m, x.bonusHp * m);
    }

}
