using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Core;
using Game.Core.Gun;

public class BulletsScript : MonoBehaviour
{

    public GameObject BasicBullet;
    private WorldMap _worldMap;

    private readonly Dictionary<BulletBase, GameObject> _bulletComponents = new Dictionary<BulletBase, GameObject>();

    // Use this for initialization
    void Start()
    {
        _worldMap = GetComponent<WorldMapScript>().WorldMap;
        _worldMap.BulletsHandler.Bullets.ItemRemoved += Bullets_ItemRemoved;
        _worldMap.BulletsHandler.Bullets.ItemAdded += Bullets_ItemAdded;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Bullets_ItemRemoved(object sender, Game.Core.Collections.ObservableCollectionEventArgs<Game.Core.Gun.BulletBase> e)
    {
        Destroy(_bulletComponents[e.Item]);
        _bulletComponents.Remove(e.Item);
    }

    private void Bullets_ItemAdded(object sender, Game.Core.Collections.ObservableCollectionEventArgs<Game.Core.Gun.BulletBase> e)
    {
        var bulletComponent = (GameObject)Instantiate(BasicBullet, e.Item.Position, Quaternion.identity);
        bulletComponent.transform.localScale = new Vector3(e.Item.Gun.BulletRadius, e.Item.Gun.BulletRadius, e.Item.Gun.BulletRadius);
        _bulletComponents.Add(e.Item, bulletComponent);
        bulletComponent.GetComponent<BasicBulletScript>().Bullet = e.Item;
    }

}
