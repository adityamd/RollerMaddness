using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {
	
	public float damageAmountYBPR = 10.0f;
	public float damageAmountTBPR = 5.0f;
	public float damageAmountPBPR = 2.0f;
	public bool Enemy;
	public bool damageOnTrigger = true;
	public bool damageOnCollision = false;
	public bool continuousDamage = false;
	public float continuousTimeBetweenHits = 0;

	public bool destroySelfOnImpact = false;	// variables dealing with exploding on impact (area of effect)
	public float delayBeforeDestroy = 0.0f;
	public GameObject explosionPrefab;

	private float savedTime = 0;

	void OnTriggerEnter(Collider collision)						// used for things like bullets, which are triggers.  
	{
		if (damageOnTrigger) {
			if (this.tag == "PlayerBullet" && collision.gameObject.tag == "Player")	// if the player got hit with it's own bullets, ignore it
				return;
		
			if (collision.gameObject.GetComponent<Health> () != null) {	// if the hit object has the Health script on it, deal damage
				collision.gameObject.GetComponent<Health>().healthPoints=0;

				if (destroySelfOnImpact) {
					Destroy (gameObject, delayBeforeDestroy);	  // destroy the object whenever it hits something
				}
			
				if (explosionPrefab != null) {
					Instantiate (explosionPrefab, transform.position, transform.rotation);
				}
			}
		}
	}


	void OnCollisionEnter(Collision collision) 						// this is used for things that explode on impact and are NOT triggers
	{	if(Enemy){
		collision.gameObject.GetComponent<Health>().healthPoints=0;
	}
		if (damageOnCollision && !collision.gameObject.CompareTag("Enemy")) {
			if (this.tag == "PlayerBullet" && collision.gameObject.tag == "Player")	// if the player got hit with it's own bullets, ignore it
				return;
		
			if (collision.gameObject.GetComponent<Health> () != null) {	// if the hit object has the Health script on it, deal damage
				
				
				if(this.gameObject.CompareTag("PinkBumpr"))
				collision.gameObject.GetComponent<Health> ().ApplyDamage (damageAmountPBPR);	
				else if(this.gameObject.CompareTag("TealBumpr"))
				collision.gameObject.GetComponent<Health> ().ApplyDamage (damageAmountTBPR);
				else if(this.gameObject.CompareTag("YellowBumpr"))
				collision.gameObject.GetComponent<Health> ().ApplyDamage (damageAmountYBPR);
			
				if (destroySelfOnImpact) {
					Destroy (gameObject, delayBeforeDestroy);	  // destroy the object whenever it hits something
				}
			
				if (explosionPrefab != null) {
					Instantiate (explosionPrefab, transform.position, transform.rotation);
				}
			}
		}
	}


	void OnCollisionStay(Collision collision) // this is used for damage over time things
	{
		if (continuousDamage) {
			if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Health> () != null) {	// is only triggered if whatever it hits is the player
				if (Time.time - savedTime >= continuousTimeBetweenHits) {
					savedTime = Time.time;
					if(this.CompareTag("PinkBumpr"))
				collision.gameObject.GetComponent<Health> ().ApplyDamage (damageAmountPBPR);	
				else if(this.CompareTag("TealBumpr"))
				collision.gameObject.GetComponent<Health> ().ApplyDamage (damageAmountTBPR);
				else if(this.CompareTag("YellowBumpr"))
				collision.gameObject.GetComponent<Health> ().ApplyDamage (damageAmountYBPR);
				}
			}
		}
	}
	
}