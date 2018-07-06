using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{

    [SerializeField]
    private int health, armour, resistance, speed;
    [SerializeField]
    private WeaponStats weaponEquiped;
    [SerializeField]
    private Image characterImage;
    [SerializeField]
    private Slider hpSlider;
    [SerializeField]
    private Animator characterAnimator;

    private bool _isDead;

    void Start()
    {
        hpSlider.maxValue = health;
        hpSlider.minValue = 0;
        hpSlider.value = health;
    }

    public void AttackAnim()
    {
        characterAnimator.SetTrigger("attack");
    }

    public void SetWalkingAnim(bool state)
    {
        characterAnimator.SetBool("isWalking", state);
    }

    public void SetSpeed(int i)
    {
        speed = i;
    }

    public int GetSpeed()
    {
        return speed;
    }

    public int weaponAttack(int i)
    {
        return weaponEquiped.weaponAttack(i);
    }

    public void takeDamage(int i)
    {
        health -= i;
        hpSlider.value = health;
        characterAnimator.SetTrigger("takingDamage");
        if (health < 1)
        {
            _isDead = true;
			hpSlider.fillRect.gameObject.SetActive (false);
        }
    }

    public WeaponStats GetWeapon()
    {
        return weaponEquiped;
    }

    public bool isDead()
    {
        return _isDead;
    }

    public void deathAnimation()
    {
        characterAnimator.SetBool("isDead", true);
    }

    public void attackAnimation()
    {
        characterAnimator.SetTrigger("attack");
    }

    public void startWalkingAnimation()
    {
        characterAnimator.SetBool("isWalking", true);
    }

	public GameObject GetHPSlider() {
		return hpSlider.gameObject;
	}

}
