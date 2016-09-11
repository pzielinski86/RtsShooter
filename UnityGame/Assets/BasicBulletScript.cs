using UnityEngine;
using System.Collections;
using Assets;
using Game.Core.Gun;

public class BasicBulletScript : MonoBehaviour
{

    public AudioClip FireAudioClip;

	// Use this for initialization
	void Start ()
	{

	    AudioSource.PlayClipAtPoint(FireAudioClip, Bullet.Position);
	}
	
	// Update is called once per frame
	void Update ()
	{
        Bullet.Update();
	    transform.position = Bullet.Position;
    }

    void OnCollisionEnter(Collision col)
    {
        var hittable=col.gameObject.GetComponent<IHittable>();

        if(hittable!=null)
            hittable.Hit(Bullet);

        Bullet.Destroy();        
    }

    public BulletBase Bullet { get; set; }
}
