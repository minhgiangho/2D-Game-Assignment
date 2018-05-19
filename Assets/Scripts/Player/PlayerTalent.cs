using UnityEngine;

namespace Player
{
    public enum Talent
    {
        Fire,
        Ice
    }

    [CreateAssetMenu(menuName = "Talents/PlayerTalent")]
    public class PlayerTalent : ScriptableObject
    {
        public Talent Talent = Talent.Fire;
        public NormalAttackSpell NormalAttackSpell;
        // TODO: Flame Burst and Frost Nova
        public ShieldSpell ShieldSpell;
        private SelectedTalent selectedTalent;

        public void Initialize(GameObject obj)
        {
            selectedTalent = obj.GetComponent<SelectedTalent>();
            selectedTalent.Talent = Talent;
            selectedTalent.NormalAttackSpell = NormalAttackSpell;
            selectedTalent.ShieldSpell = ShieldSpell;
        }
    }

    public class SelectedTalent
    {
        [HideInInspector]public Talent Talent;
        [HideInInspector]public NormalAttackSpell NormalAttackSpell;
        [HideInInspector]public ShieldSpell ShieldSpell;
    }
    
}