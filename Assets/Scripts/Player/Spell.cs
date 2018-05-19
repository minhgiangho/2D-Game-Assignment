using UnityEngine;

namespace Player
{
    
    public abstract class Spell : ScriptableObject
    {
        public string Name = "new Spell";
        public Sprite Sprite;
        public AudioClip Sound;
        public float BaseCoolDown = 1f;
        public abstract void Initialize(GameObject obj);
        public abstract void TriggerSpell();
    }


}