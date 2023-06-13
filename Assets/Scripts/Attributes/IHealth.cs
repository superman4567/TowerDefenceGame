namespace Scripts.Attributes
{
    public delegate void OnDeath();
    public delegate void OnDamage();
    public delegate void OnCure();
    public delegate void OnHeal();

    public interface IHealth
    {
        event OnDeath OnDeath;
        event OnDamage OnDamage;
        event OnCure OnCure;
        event OnHeal OnHeal;

        void TakeDamage(int damage, EDamageType type);
        void Cure(EDamageType type);
        void Heal(int amount);
    }
}
