using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteriodCollision : MonoBehaviour
{
    public List<GameObject> ResourcesAfterCollision;

    public List<GameObject> DecoratorsForAsteriod;

    public ParticleSystem ImpactPrefab;

    public ParticleSystem Explosion;

    public Fling Fling;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider.gameObject.tag == "Asteriod")
        {
            float impactForce = GetImpactForce(collision);
            if(impactForce > 20)
            {

                TextPopup.Create(new Vector3(collision.GetContact(0).point.x, collision.GetContact(0).point.y, -8f), "Direct Hit!", true);

                foreach (GameObject resource in ResourcesAfterCollision)
                {
                    GameObject newResource = Instantiate(resource, new Vector3(this.transform.position.x - .3f, this.transform.position.y, this.transform.position.z - 1f), Quaternion.identity) as GameObject;
                    newResource.GetComponent<Rigidbody2D>().velocity = GetPointOnUnitCircleCircumference();
                }

                ParticleSystem Impact_PS = Instantiate(ImpactPrefab, new Vector3(collision.GetContact(0).point.x, collision.GetContact(0).point.y, this.transform.position.z - 3f), Quaternion.LookRotation(collision.GetContact(0).normal)) as ParticleSystem;
                ParticleSystem SelfExplosion_PS = Instantiate(Explosion, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 3f), Quaternion.identity) as ParticleSystem;

                float totalDuration = Impact_PS.main.duration;
                Destroy(Impact_PS, totalDuration);

                float totalDuration_2 = SelfExplosion_PS.main.duration;
                Destroy(SelfExplosion_PS, totalDuration_2);

                Destroy(this.gameObject);
            }
            else
            {
                TextPopup.Create(new Vector3(collision.GetContact(0).point.x, collision.GetContact(0).point.y, -8f), "Glanced");
            }
        }

        if (Fling != null)
            Fling.FlingTrail.gameObject.SetActive(false);
    }

    public Vector2 GetPointOnUnitCircleCircumference()
    {
        float randomAngle = Random.Range(0f, Mathf.PI * 2f);
        return new Vector2(Mathf.Sin(randomAngle), Mathf.Cos(randomAngle)).normalized;
    }

    public float GetImpactForce(Collision2D collision)
    {
        float impulse = 0F;

        foreach (ContactPoint2D point in collision.contacts)
        {
            impulse += point.normalImpulse;
        }

        return impulse / Time.fixedDeltaTime;
    }
}
