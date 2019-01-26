using Environment.MapElements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    public class MapGenerator : MonoBehaviour
    {
        public Transform frontLocationMap;
        public Transform leftLocationMap;
        public Transform rightLocationMap;
        public Transform upLocationMap;
        public Transform downLocationMap;

        public GameObject mapPrefab;
        public GameObject Portal;

        public GameObject frontMap;
        public GameObject leftMap;
        public GameObject rightMap;
        public GameObject upMap;
        public GameObject downMap;

        public List<Material> mapColor;
        public List<Material> UseMaterial;

        // Start is called before the first frame update
        void Start()
        {
            UseMaterial = new List<Material>();

            //Create Map
            frontMap = Instantiate(mapPrefab, frontLocationMap);
            //Set Material
            int i = Random.Range(0, mapColor.Count - UseMaterial.Count);
            frontMap.GetComponent<MeshRenderer>().material = mapColor[i];
            UseMaterial.Add(mapColor[i]);
            mapColor.RemoveAt(i);
            // Spawn First Portal
            PortalScript portal = Instantiate(Portal, frontMap.transform).GetComponent<PortalScript>();
            portal.Direction = Direction.Up;
            portal.transform.localPosition = new Vector3(Random.Range(-4.5f, 4.5f), 0, 4.75f);

            portal = Instantiate(Portal, frontMap.transform).GetComponent<PortalScript>();
            portal.Direction = Direction.Down;
            portal.transform.localPosition = new Vector3(Random.Range(-4.5f, 4.5f), 0, -4.75f);

            portal = Instantiate(Portal, frontMap.transform).GetComponent<PortalScript>();
            portal.Direction = Direction.Right;
            portal.transform.localPosition = new Vector3(4.75f, 0, Random.Range(-4.5f, 4.5f));

            BorderGenereting();
        }

        void BorderGenereting()
        {
            List<GameObject> portals = new List<GameObject>();
            foreach (Transform gameobject in frontMap.GetComponentsInChildren<Transform>())
            {
                if (gameobject.CompareTag("Portal")) portals.Add(gameobject.gameObject);
            }

            foreach (GameObject portal in portals)
            {
                switch (portal.GetComponent<PortalScript>().Direction)
                {
                    case Direction.Up:
                        upMap = Instantiate(mapPrefab, upLocationMap);
                        SingleGeneration(upMap, portal.GetComponent<PortalScript>(), Direction.Up);
                        break;
                    case Direction.Down:
                        downMap = Instantiate(mapPrefab, downLocationMap);
                        SingleGeneration(downMap, portal.GetComponent<PortalScript>(), Direction.Down);
                        break;
                    case Direction.Left:
                        leftMap = Instantiate(mapPrefab, leftLocationMap);
                        SingleGeneration(leftMap, portal.GetComponent<PortalScript>(), Direction.Left);
                        break;
                    case Direction.Right:
                        rightMap = Instantiate(mapPrefab, rightLocationMap);
                        SingleGeneration(rightMap, portal.GetComponent<PortalScript>(), Direction.Right);
                        break;
                }
            }
        }

        void SingleGeneration(GameObject border, PortalScript lastPortal, Direction direction)
        {
            //Set Material
            int i = Random.Range(0, mapColor.Count - UseMaterial.Count);
            border.GetComponent<MeshRenderer>().material = mapColor[i];
            // Spawn First Portal
            PortalScript portal = Instantiate(Portal, border.transform).GetComponent<PortalScript>();
            portal.Direction = Direction.Up;
            portal.transform.localPosition = new Vector3(Random.Range(-4.5f, 4.5f), 0, 4.75f);
            if (direction == Direction.Down) lastPortal.Destination = portal;

            portal = Instantiate(Portal, border.transform).GetComponent<PortalScript>();
            portal.Direction = Direction.Down;
            portal.transform.localPosition = new Vector3(Random.Range(-4.5f, 4.5f), 0, -4.75f);
            if (direction == Direction.Up) lastPortal.Destination = portal;

            portal = Instantiate(Portal, border.transform).GetComponent<PortalScript>();
            portal.Direction = Direction.Right;
            portal.transform.localPosition = new Vector3(4.75f, 0, Random.Range(-4.5f, 4.5f));
            if (direction == Direction.Left) lastPortal.Destination = portal;

            portal = Instantiate(Portal, border.transform).GetComponent<PortalScript>();
            portal.Direction = Direction.Left;
            portal.transform.localPosition = new Vector3(-4.75f, 0, Random.Range(-4.5f, 4.5f));
            if (direction == Direction.Right) lastPortal.Destination = portal;
        }

        public void ChangeMainBorder(GameObject nextMainBorder)
        {
            GameObject temp = frontMap;
            temp.transform.SetParent(null);
            StartCoroutine(WaitForDestroy(temp));
            frontMap = nextMainBorder;
            nextMainBorder.transform.SetParent(frontLocationMap);

            if (leftMap == nextMainBorder) leftMap = null;
            else if (rightMap == nextMainBorder) rightMap = null;
            else if (upMap == nextMainBorder) upMap = null;
            else if (downMap == nextMainBorder) downMap = null;
            ClearBorder();
            BorderGenereting();
        }

        IEnumerator WaitForDestroy(GameObject game)
        {
            yield return new WaitForSeconds(.1f);
            Destroy(game);
        }

        void ClearBorder()
        {
            Destroy(leftMap);
            leftMap = null;

            Destroy(rightMap);
            rightMap = null;

            Destroy(upMap);
            upMap = null;

            Destroy(downMap);
            downMap = null;
        }

    }
}

