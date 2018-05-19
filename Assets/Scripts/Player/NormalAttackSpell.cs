using UnityEngine;

namespace Player
{
    /// <summary>
    /// Scriptable Object, to set up properties of cooldown and triggerable script 
    /// like Flame Burst, Frostbolt
    /// </summary>
    [CreateAssetMenu(menuName = "Spells/NormalAttackSpell")]
    public class NormalAttackSpell : Spell
    {
        // cooldown?
        public Talent Talent = Talent.Fire;
        public int Damage = 2;
        [TextArea]
        public string Description;
        private NormalAttackTriggerable normalAttack;

        // obj ought to refer to player
        public override void Initialize(GameObject obj)
        {
            normalAttack = obj.GetComponent<NormalAttackTriggerable>();
            normalAttack.Initialize();
            normalAttack.Damage = Damage;
            normalAttack.Description = Description;
        }

        public override void TriggerSpell()
        {
            normalAttack.Trigger();
        }
    }
}