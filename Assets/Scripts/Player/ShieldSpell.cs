using UnityEngine;

namespace Player
{
    /// <summary>
    /// ShieldSpell ScriptObject, can edit value on inspector easily
    /// </summary>
    [CreateAssetMenu(menuName = "Spells/ShieldSpell")]
    public class ShieldSpell : Spell
    {
        // cooldown?
        public Talent Talent = Talent.Fire;
        public int MaxHealth = 2;
        public float Duration = 1;
        [TextArea]
        public string Description;

        private ShieldTriggerable shield;

        public override void Initialize(GameObject obj)
        {
            shield = obj.GetComponent<ShieldTriggerable>();
            shield.Initialize();
            shield.Duration = Duration;
            shield.MaxHealth = MaxHealth;
            shield.Description = Description;
        }

        public override void TriggerSpell()
        {
            shield.Trigger();
        }
    }
}