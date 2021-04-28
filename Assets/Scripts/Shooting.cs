using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shooting : MonoBehaviour
{
    //public int gunDamage = 1; //amount of damage when shot
    //public float fireRate = .25f; // rate player can shoot
    public float weaponRange = 50f; // how far our raycast will work
    //public float hitForce = 100f; //amount of force applied when shot
    public Transform gunEnd; // position of the end of the gun
    private Camera Camera; // our camera in the scene
    private WaitForSeconds shotDuration = new WaitForSeconds(1f); //to determine how we want the laser visible once shot
    //private AudioSource audioSource; // sound for the gunshot
    private LineRenderer laserLine; //laser line
    //private float nextFire; //hold the time at which the player will be able to shoot again
    //public int ammo;
    //public bool isFiring;
    //public Text ammoDisplay;
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        laserLine = GetComponent<LineRenderer>();
        Camera = GetComponentInChildren<Camera>();
        laserLine.enabled = true;
    }
    void Update()
    {
        //ammoDisplay.text = ammo.ToString();
        // if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        // if (Input.GetMouseButtonDown("Fire1") && !isFiring && ammo > 0)
        if (Input.GetButtonDown("Fire1"))
        {
            //isFiring = true;
            //ammo--;
            //isFiring = false;
            //nextFire = Time.time + fireRate;
            //audioSource.Play();
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = Camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            laserLine.SetPosition(0, gunEnd.position);
            if (Physics.Raycast(rayOrigin, Camera.transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);
                
                //ShootableItem health = hit.collider.GetComponent<ShootableItem>();
                //if (health != null)
                //{
                //health.Damage(gunDamage);
                //}
                if (hit.rigidbody != null)
                {
                    //hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (Camera.transform.forward * weaponRange));
                
            }
        
        
        
        
        }
    }
    private IEnumerator ShotEffect()
    {
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}