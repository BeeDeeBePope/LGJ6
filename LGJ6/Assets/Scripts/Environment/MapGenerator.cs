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
            TailMapGeneration(frontMap);


            List<GameObject> portals = new List<GameObject>();
            foreach (Transform gameobject in frontMap.GetComponentsInChildren<Transform>())
            {
                if (gameobject.CompareTag("Portal")) portals.Add(gameobject.gameObject);
            }
            int startowyPortal = Random.Range(0, 4);

            

            switch (portals[startowyPortal].GetComponent<PortalScript>().Direction)
            {
                case Direction.Up:
                    GameManager.Instance.Player.Movement.SetDirection(Vector2.down);
                    break;
                case Direction.Down:
                    GameManager.Instance.Player.Movement.SetDirection(Vector2.up);
                    break;
                case Direction.Left:
                    GameManager.Instance.Player.Movement.SetDirection(Vector2.right);
                    break;
                case Direction.Right:
                    GameManager.Instance.Player.Movement.SetDirection(Vector2.left);
                    break;
            }
            portals[startowyPortal].GetComponent<PortalScript>().ActiveMap();
            portals[startowyPortal].GetComponent<PortalScript>().PreVisuals.SetActive(false);
            portals[startowyPortal].GetComponent<PortalScript>().PostVisuals.SetActive(true);
            portals[startowyPortal].GetComponent<PlayerKiller>().DisableOverride = false;
            Destroy(portals[startowyPortal].GetComponent<PortalScript>());

            GameManager.Instance.Player.transform.position = portals[startowyPortal].transform.position + GameManager.Instance.Player.Movement.CurrentDirection;

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
            TailMapGeneration(border);

            PortalScript nextportal = null;
            foreach(PortalScript game in border.GetComponentsInChildren<PortalScript>())
            {
                if (nextportal == null) nextportal = game;
                if(game.transform.position.y > nextportal.transform.position.y)
                {
                    nextportal = game;
                }
            }
            lastPortal.Destination = nextportal;
        }

        public void ChangeMainBorder(GameObject nextMainBorder)
        {
            GameObject temp = frontMap;
            temp.transform.SetParent(null);
            temp.transform.position = new Vector3(100, 100, 1000);
            StartCoroutine(WaitForDestroy(temp));
            frontMap = nextMainBorder;
            nextMainBorder.transform.SetParent(frontLocationMap);

            int i = 0;
            for (int j = 0; j < mapColor.Count; j++)
            {
                if (mapColor[j].color == nextMainBorder.GetComponent<MeshRenderer>().materials[0].color)
                {
                    i = j;
                    break;
                }
            }

            UseMaterial.Add(mapColor[i]);
            mapColor.RemoveAt(i);
            if (UseMaterial.Count == 3)
            {
                mapColor.Add(UseMaterial[0]);
                UseMaterial.RemoveAt(0);
            }

            if (leftMap == nextMainBorder) leftMap = null;
            else if (rightMap == nextMainBorder) rightMap = null;
            else if (upMap == nextMainBorder) upMap = null;
            else if (downMap == nextMainBorder) downMap = null;
            ClearBorder();
            BorderGenereting();
        }

        IEnumerator WaitForDestroy(GameObject game)
        {
            yield return new WaitForSeconds(5f);
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

        void TailMapGeneration(GameObject border)
        {
            if (MapBuilder.Instance == null) return;

            int i = MapBuilder.Instance.SearchByColor(border.GetComponent<MeshRenderer>().materials[0].color);

            if (i == -1) i=0;

            int which = Random.Range(0, MapBuilder.Instance.blockColors.Length / 576);
                
            for (int j = 0; j < 24; j++)
            {
                for (int k = 0; k < 24; k++)
                {
                    GameObject tile = Instantiate(MapBuilder.Instance.bioms[i].sprites[MapBuilder.Instance.blockColors[which, j, k]], border.transform);
                    tile.transform.localPosition = new Vector3(-11.5f + k, 0, 11.5f + -j);
                    if(MapBuilder.Instance.blockColors[which, j, k] == 2)
                    {
                        if(j == 23)tile.GetComponentInChildren<PortalScript>().Direction = Direction.Down;
                        else if (j == 0) tile.GetComponentInChildren<PortalScript>().Direction = Direction.Up;
                        else if (k == 23) tile.GetComponentInChildren<PortalScript>().Direction = Direction.Right;
                        else if (k == 0) tile.GetComponentInChildren<PortalScript>().Direction = Direction.Left;
                    }

                }
            }
        }

    }
}

