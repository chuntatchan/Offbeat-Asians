using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterFightScene : MonoBehaviour
{
	[SerializeField]
	private CharacterStats prefabStats;
	[SerializeField]
	private int statMaxHealth, statHealth, statArmour, statFinesse, statSpeed;
	[SerializeField]
	private int charaNum;
	private List<int> tempArmour, tempFinesse;
	private List<float> tempDamageMultiplier, tempDamageReductionMultiplier;
	private List<int> tempStatDuration;
	[SerializeField]
	private WeaponStats weaponEquiped;
	[SerializeField]
	private Slider hpSlider;
	[SerializeField]
	private TMP_Text HPText, statText;
	[SerializeField]
	private Animator characterAnimator;
	[SerializeField]
	private GameObject damagePopUpPrefab;
	[SerializeField]
	private Transform damagePopUpLoc;
	[SerializeField]
	private Transform pointerLoc;

	[Header ("Enemy Stuff")]

	[SerializeField]
	private bool isEnemy;
	[SerializeField]
	private GameObject tbox;
	[SerializeField]
	private Text tbox_text;

	private bool _isDead;

	void Start ()
	{
		statMaxHealth = prefabStats.GetMaxHealth ();
		statHealth = prefabStats.GetHealth ();
		statArmour = prefabStats.GetArmour ();
		statFinesse = prefabStats.GetFinesse ();
		statSpeed = prefabStats.GetSpeed ();

		tempArmour = new List<int> ();
		tempFinesse = new List<int> ();
		tempStatDuration = new List<int> ();
		tempDamageMultiplier = new List<float> ();
		tempDamageReductionMultiplier = new List<float> ();


		hpSlider.maxValue = statMaxHealth;
		hpSlider.minValue = 0;
		hpSlider.value = statHealth;
		HPText.text = GetHealthDisplay ();
		if (!isEnemy) {
			statText.text = GetStatsDisplay ();
		}
	}

	public void AddTempStats (int hp, int armour, int finesse, float damageMulti, float damageReductionMulti, int duration)
	{
		takeDamage (-hp);
		tempArmour.Add (armour);
		tempFinesse.Add (finesse);
		tempDamageMultiplier.Add (damageMulti);
		tempDamageReductionMultiplier.Add (damageReductionMulti);
		tempStatDuration.Add (duration);
		SetHealthDisplay ();
		if (statText != null) {
			SetStatsDisplay ();
		}
	}

	public void turnTick ()
	{
		for (int i = 0; i < tempStatDuration.Count; i++) {
			tempStatDuration [i] = tempStatDuration [i] - 1;
			if (tempStatDuration [i] == 0) {
				tempArmour.RemoveAt (i);
				tempFinesse.RemoveAt (i);
				tempDamageMultiplier.RemoveAt (i);
				tempDamageReductionMultiplier.RemoveAt (i);
				tempStatDuration.RemoveAt (i);
			}
		}
	}

	private void OnEnable ()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded (Scene scene, LoadSceneMode mode)
	{
		resetUI ();
	}

	public string GetHealthDisplay ()
	{
		return statHealth + "/" + statMaxHealth;
	}

	public void SetHealthDisplay ()
	{
		HPText.text = statHealth + "/" + statMaxHealth;
		hpSlider.value = statHealth;
	}

	public string GetStatsDisplay ()
	{
		return "Armor: " + statArmour + "  Finesse: " + statFinesse + "%";
	}

	public void SetStatsDisplay ()
	{
		statText.text = "Armor: " + statArmour + "  Finesse: " + statFinesse + "%";
	}

	public void AttackAnim ()
	{
		resetUI ();
		characterAnimator.SetTrigger ("attack");
	}

	public void SetWalkingAnim (bool state)
	{
		resetUI ();
		characterAnimator.SetBool ("isWalking", state);
	}

	public void SetSpeed (int i)
	{
		statSpeed = i;
	}

	public float GetDamageMultiplier ()
	{
		float totalDamageMulti = 1f;
		if (tempDamageMultiplier.Count != 0) {
			for (int j = 0; j < tempDamageReductionMultiplier.Count; j++) {
				totalDamageMulti = totalDamageMulti * tempDamageMultiplier [j];
			}
		}
		return totalDamageMulti;
	}

	public int GetSpeed ()
	{
		return statSpeed;
	}

	public void SetFinesse (int i)
	{
		statFinesse = i;
	}

	public int GetFinesse ()
	{
		return statFinesse;
	}

	public void SetArmour (int i)
	{
		statArmour = i;
	}

	public int GetArmour ()
	{
		return statArmour;
	}

	public int weaponAttack (int i)
	{
		return weaponEquiped.weaponAttack (i);
	}

	public void takeDamage (int i)
	{
		resetUI ();
		float totalDamageReductionMulti = 1f;
		if (tempDamageReductionMultiplier.Count != 0) {
			for (int j = 0; j < tempDamageReductionMultiplier.Count; j++) {
				totalDamageReductionMulti = totalDamageReductionMulti * tempDamageMultiplier [j];
			}
		}
		print ("totalDamageReduct: " + totalDamageReductionMulti.ToString ());
		int totalTempArmour = 0;
		if (tempArmour.Count != 0) {
			for (int j = 0; j < tempArmour.Count; j++) {
				totalTempArmour = totalTempArmour + tempArmour [j];
			}
		}
		print ("totalTempArmour: " + totalTempArmour.ToString ());
		if (i < 0) {
			statHealth = (Mathf.Clamp (statHealth - (Mathf.RoundToInt (i * totalDamageReductionMulti) - (statArmour + totalTempArmour)), 0, 99));
			print ("damageTaken: " + (Mathf.RoundToInt (i * totalDamageReductionMulti) - (statArmour + totalTempArmour)).ToString ());
		} else {
			statHealth = (Mathf.Clamp (statHealth - i, 0, statMaxHealth ));
			print ("damageHeal: " + i);
		}
		if (statHealth > statMaxHealth) {
			statHealth = (statMaxHealth);
		}

		if (hpSlider != null) {
			hpSlider.value = statHealth;
		}
		if (HPText != null) {
			SetHealthDisplay ();
		}
		if (characterAnimator != null && i > 0) {
			characterAnimator.SetTrigger ("takingDamage");
		}
		if (damagePopUpPrefab != null && damagePopUpLoc != null) {
			print ("spawnDamagePopUp");
			GameObject temp = Instantiate (damagePopUpPrefab, damagePopUpLoc.position, damagePopUpLoc.rotation);
			if (i > 0) {
				temp.GetComponent<TMP_Text> ().text = (Mathf.CeilToInt (i * totalDamageReductionMulti) - (statArmour + totalTempArmour)).ToString ();
			} else if (i < 0) {
				temp.GetComponentInChildren<SpriteRenderer> ().color = Color.green;
				string text = (i * -1).ToString ();

				temp.GetComponent<TMP_Text> ().text = text;
			}
		}
		if (statHealth < 1) {
			_isDead = true;
			hpSlider.fillRect.gameObject.SetActive (false);
		}
	}

	public WeaponStats GetWeapon ()
	{
		return weaponEquiped;
	}

	public void SetWeapon (WeaponStats newWeapon)
	{
		weaponEquiped = newWeapon;
	}

	public bool isDead ()
	{
		return _isDead;
	}

	public Transform GetDamagePopUpLoc ()
	{
		return damagePopUpLoc;
	}

	public Transform GetPointerLoc ()
	{
		return pointerLoc;
	}

	public void deathAnimation ()
	{
		characterAnimator.SetBool ("isDead", true);
	}

	public void attackAnimation ()
	{
		characterAnimator.SetTrigger ("attack");
	}

	public void SetActiveTbox (bool state)
	{
		tbox.SetActive (state);
	}

	public Text GetTboxText ()
	{
		return tbox_text;
	}

	public void startWalkingAnimation ()
	{
		characterAnimator.SetBool ("isWalking", true);
	}

	public Slider GetHPSlider ()
	{
		return hpSlider;
	}

	public int GetHealth ()
	{
		return statHealth;
	}

	public int GetMaxHealth ()
	{
		return statMaxHealth;
	}

	public void SetMaxHealth (int n_maxHeath)
	{
		statMaxHealth = (n_maxHeath);
	}

	public void resetUI ()
	{
		if (charaNum != 0) {
			if (characterAnimator == null || hpSlider == null || HPText == null || damagePopUpLoc == null || pointerLoc == null || statText == null) {
				GameObject[] player = GameObject.FindGameObjectsWithTag ("Player" + charaNum);
				for (int j = 0; j < player.Length; j++) {
					if (player [j].GetComponent<Animator> () != null) {
						characterAnimator = player [j].GetComponent<Animator> ();
					} else if (player [j].GetComponent<Slider> () != null) {
						hpSlider = player [j].GetComponent<Slider> ();
						hpSlider.maxValue = statHealth;
						hpSlider.minValue = 0;
						hpSlider.value = statHealth;
						;
					} else if (player [j].GetComponent<TMP_Text> () != null && player [j].name == "HP Text") {
						HPText = player [j].GetComponent<TMP_Text> ();
						HPText.text = GetHealthDisplay ();
					} else if (player [j].GetComponent<TMP_Text> () != null && player [j].name == "Stats" && !isEnemy) {
						statText = player [j].GetComponent<TMP_Text> ();
						statText.text = GetStatsDisplay ();
					} else if (player [j].name == "DamagePopUpLoc") {
						damagePopUpLoc = player [j].transform;
					} else if (player [j].name == "PointerLoc") {
						pointerLoc = player [j].transform;
					}
				}
			}
		}
	}

}
