using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsGenerator : MonoBehaviour
{
    public GameObject[] AllCars = new GameObject[7];
    public GameObject bus;
    int randCar;
    int otherRandCar;
    int manyCars;
    int otherManyCars;
    float secondsToWait;
    public Transform Spawnpoint;
    public Transform otherSpawnpoint;
    bool busbool;
    TIme Tcode;
    bool canSpawnBus = false;
    GameObject thisCar;
    CarsMoving CarsMovingCode;
    Vector3 streetX = new Vector3(4.65f, 0, 0);
    Vector3 streetZ = new Vector3(0, 0, 4.65f);
    string carTag;

    public ObjectPooler objectPooler;

    void Start()
    {
        Tcode = GameObject.Find("Sun&Moon").GetComponent<TIme>();
        StartCoroutine(Generator());
        StartCoroutine(Generator2());
    }

    string IntToTag(int tipoCarro)
    {
        switch (tipoCarro)
        {
            case 0:
                carTag = "CamBlanco";
                break;
            case 1:
                carTag = "CamNaranja";
                break;
            case 2:
                carTag = "CamVerde";
                break;
            case 3:
                carTag = "CarAzul";
                break;
            case 4:
                carTag = "CarBlanco";
                break;
            case 5:
                carTag = "CarRojo";
                break;
            case 6:
                carTag = "Taxi";
                break;
        }

        return carTag;
    }

    IEnumerator Generator()
    {
        
        otherManyCars = Random.Range(1, 5);

        for (int i = 0; i < otherManyCars; i++)
        {
            otherRandCar = Random.Range(0, 7);        //int
            secondsToWait = Random.Range(0.6f, 2.3f);   //float
            //thisCar=Instantiate(AllCars[otherRandCar], Spawnpoint); // INSTANCIA ANTIGUA
            thisCar = objectPooler.SpawnFromPool(IntToTag(otherRandCar), Spawnpoint.position, Spawnpoint.rotation);
            CarsMovingCode = thisCar.GetComponent<CarsMoving>();
            if(gameObject.CompareTag("GenX"))
            {
                CarsMovingCode.actualVector = streetX;
                CarsMovingCode.semToFollowX = true;
            }
            else
            {
                CarsMovingCode.actualVector = streetZ;
                CarsMovingCode.semToFollowX = false;
            }
            canSpawnBus = false;
            yield return new WaitForSeconds(secondsToWait);
            canSpawnBus = true;
        }
        StartCoroutine(waitingToWave());
    }

    IEnumerator Generator2()
    {
        manyCars = Random.Range(1, 5);

        for (int i = 0; i < manyCars; i++)
        {
            randCar = Random.Range(0, 7);
            secondsToWait = Random.Range(0.6f, 2.3f);
            //thisCar = Instantiate(AllCars[randCar], otherSpawnpoint);//  INSTANCIA ANTIGUA
            thisCar = objectPooler.SpawnFromPool(IntToTag(randCar), otherSpawnpoint.position, otherSpawnpoint.rotation);
            CarsMovingCode = thisCar.GetComponent<CarsMoving>();
            if (gameObject.CompareTag("GenX"))
            {
                CarsMovingCode.actualVector = streetX;
                CarsMovingCode.semToFollowX = true;
            }
            else
            {
                CarsMovingCode.actualVector = streetZ;
                CarsMovingCode.semToFollowX = false;

            }
            canSpawnBus = false;
            yield return new WaitForSeconds(secondsToWait);
            canSpawnBus = true;
        }
        StartCoroutine(OtherwaitingToWave());
    }

    IEnumerator waitingToWave()
    {
        float secsTowait=Random.Range(3f,7f);
        yield return new WaitForSeconds(secsTowait);
        StartCoroutine(Generator());
    }

    IEnumerator OtherwaitingToWave()
    {
        float secsTowait = Random.Range(3.6f, 7.3f);
        yield return new WaitForSeconds(secsTowait);
        StartCoroutine(Generator2());
    }


    public void BusGenerator()
    {
        while(canSpawnBus)
        {
            Instantiate(bus, Spawnpoint);
            canSpawnBus = false;
        }
    }
}
