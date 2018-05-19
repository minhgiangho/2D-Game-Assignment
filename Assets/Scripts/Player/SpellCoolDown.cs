using System;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    /// <summary>
    /// Cool down of a specific spell, need to be placed on a button
    /// </summary>
    [RequireComponent(typeof(Image), typeof(AudioSource))]
    public class SpellCoolDown : MonoBehaviour
    {
        public string SpellButtonAxisName = "Fire1";
        public Image DarkMask;
        public Text CoolDownTextDisplay;

//        [SerializeField] private PlayerTalent playerTalent;
        [SerializeField] private Spell spell;
        [SerializeField] private GameObject playerPos;
        private Image myButtonImage;
        private AudioSource spellSource;
        // TODO: write a global cooldown script, cause hero can only use one spell at the meantime
        private float coolDownDuration;
        private float nextReadyTime;
        private float coolDownTimeLeft;

        private void Start()
        {
//            foreach (Spell playerSpell in playerTalent.Spells)
//            {
//            }
            Initialize(spell, playerPos);
        }

        private void Update()
        {
            bool cooldownComplete = Time.time > nextReadyTime;
            if (cooldownComplete)
            {
                SpellReady();
                if (Input.GetButtonDown(SpellButtonAxisName))
                {
                    ButtonTriggered();
                }
            }
            else
            {
                CoolDown();
            }
        }

        public void Initialize(Spell selectedSpell, GameObject player)
        {
            spell = selectedSpell;
            myButtonImage = GetComponent<Image>();
            spellSource = GetComponent<AudioSource>();
            myButtonImage.sprite = spell.Sprite;
            DarkMask.sprite = spell.Sprite;
            coolDownDuration = spell.BaseCoolDown;
            spell.Initialize(player);
        }

        private void SpellReady()
        {
            CoolDownTextDisplay.enabled = false;
            DarkMask.enabled = false;
        }

        private void CoolDown()
        {
            coolDownTimeLeft -= Time.deltaTime;
            float roundedCd = Mathf.Round(coolDownTimeLeft);
            CoolDownTextDisplay.text = roundedCd.ToString();
            DarkMask.fillAmount = coolDownTimeLeft / coolDownDuration;
        }

        private void ButtonTriggered()
        {
            // record by real world time
            nextReadyTime = coolDownDuration + Time.time;
            coolDownTimeLeft = coolDownDuration;
            DarkMask.enabled = true;
            CoolDownTextDisplay.enabled = true;
            // TODO: change cool down text
            spellSource.clip = spell.Sound;
            spellSource.Play();
            spell.TriggerSpell();
        }
    }
}