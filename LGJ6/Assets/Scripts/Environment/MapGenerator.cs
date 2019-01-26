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
            frontMap.transform.SetParent(frontMap.transform.parent);
            //Set Material
            int i = Random.Range(0, mapColor.Count - UseMaterial.Count);
            frontMap.GetComponent<MeshRenderer>().material = mapColor[i];
            UseMaterial.Add(mapColor[i]);
            mapColor.RemoveAt(i);
            // Spawn First Portal
            PortalScript portal = Instantiate(Portal, frontMap.transform).GetComponent<PortalScript>();
            portal.Direction = Direction.Up;
            portal.transform.localPosition = new Vector3();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

