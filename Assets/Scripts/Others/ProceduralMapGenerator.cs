using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMapGenerator : MonoBehaviour
{
    public static ProceduralMapGenerator instance;

    [SerializeField] Transform staringRoom;
    [SerializeField] Vector3 nextRoomSpawnPos = new Vector3(0, 0, 60);

    public int noOfBuildings = 0;


    [SerializeField] GameObject[] buildingsPrefabs;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        SpawnBuilding(staringRoom.position);
    }


    public void SpawnBuilding(Vector3 _endPoint)
    {
        int _randomBuilding = Random.Range(0, buildingsPrefabs.Length);

        switch (_randomBuilding)
        {
            case 0:
                SpawnAt(new Vector3(0f, 0f, 0f), _randomBuilding, _endPoint);
                break;
            case 1:
                SpawnAt(new Vector3(0f, 0f, 0f), _randomBuilding, _endPoint);
                break;
            default:
                SpawnAt(new Vector3(0f, 0f, 0f), _randomBuilding, _endPoint);
                break;
        }

        noOfBuildings++;

        if(noOfBuildings >= 4)
        {
            Destroy(transform.GetChild(0).gameObject);
            noOfBuildings--;
        }
    }

    void SpawnAt(Vector3 _pos,int _building,Vector3 _endPoint)
    {
        Instantiate(buildingsPrefabs[_building], _endPoint + _pos, Quaternion.identity, this.transform);
    }


}
