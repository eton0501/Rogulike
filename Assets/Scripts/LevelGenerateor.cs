using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerateor : MonoBehaviour
{
    public GameObject generatorRoom;
    public Transform generatorPoint;
    public Color starColor,endColor,shopColor,gunRoomColor;
    public bool includeShop,includeGunRoom;
    public int minDistanceToShop,maxDistanceToShop,minDistanceToGunRoom,maxDistanceToGunRoom;
    public int distanceToEnd;
    public enum Direction{up,down,left,right};
    public Direction slectedDirecion;
    public float xOffset=18f,yOffset=10f;
    public LayerMask whatIsRoom;
    private GameObject endRoom,shopRoom,gunRoom;
    private List<GameObject> layoutRooms=new List<GameObject>();
    public RoomPrefabs room;
    private List<GameObject> generatedOutlines=new List<GameObject>();
    public RoomCenter centerStart,centerEnd,centerShop,centerGunRoom;
    public RoomCenter[] potentialCenter;
    void Start()
    {
        Instantiate(generatorRoom,generatorPoint.position,generatorPoint.rotation).GetComponent<SpriteRenderer>().color=starColor;
        slectedDirecion=(Direction)Random.Range(0,4);
        MoveGeneratorPoint();
        for(int i = 0; i < distanceToEnd; i++)
        {
            GameObject newRoom=Instantiate(generatorRoom,generatorPoint.position,generatorPoint.rotation);
            layoutRooms.Add(newRoom);
            if (i + 1 == distanceToEnd)
            {
                newRoom.GetComponent<SpriteRenderer>().color=endColor;
                layoutRooms.RemoveAt(layoutRooms.Count-1);
                endRoom=newRoom;
            }
            slectedDirecion=(Direction)Random.Range(0,4);
            MoveGeneratorPoint();
            while (Physics2D.OverlapCircle(generatorPoint.position, 0.2f, whatIsRoom))
            {
                MoveGeneratorPoint();
            }
        }

        if (includeShop)
        {
            int shopSelector=Random.Range(minDistanceToShop,maxDistanceToShop+1);
            shopRoom=layoutRooms[shopSelector];
            layoutRooms.RemoveAt(shopSelector);
            shopRoom.GetComponent<SpriteRenderer>().color=shopColor;
        }

        if (includeGunRoom)
        {
            int gunRoomSelector=Random.Range(minDistanceToGunRoom,maxDistanceToGunRoom+1);
            gunRoom=layoutRooms[gunRoomSelector];
            layoutRooms.RemoveAt(gunRoomSelector);
            gunRoom.GetComponent<SpriteRenderer>().color=gunRoomColor;
        }

        CreateRoomOutlined(Vector3.zero);
        foreach (GameObject room in layoutRooms)
        {
            CreateRoomOutlined(room.transform.position);
        }
        CreateRoomOutlined(endRoom.transform.position);
        CreateRoomOutlined(shopRoom.transform.position);
        CreateRoomOutlined(gunRoom.transform.position);

        foreach(GameObject outlines in generatedOutlines)
        {
            bool generateCenter=true;
            if (outlines.transform.position == Vector3.zero)
            {
                Instantiate(centerStart,outlines.transform.position,transform.rotation).theRoom=outlines.GetComponent<Room>();
                generateCenter=false;
            }
            if (outlines.transform.position == endRoom.transform.position)
            {
                Instantiate(centerEnd,outlines.transform.position,transform.rotation).theRoom=outlines.GetComponent<Room>();
                generateCenter=false;
            }
            if (outlines.transform.position == shopRoom.transform.position)
            {
                Instantiate(centerShop,outlines.transform.position,transform.rotation).theRoom=outlines.GetComponent<Room>();
                generateCenter=false;
            }
            if (outlines.transform.position == gunRoom.transform.position)
            {
                Instantiate(centerGunRoom,outlines.transform.position,transform.rotation).theRoom=outlines.GetComponent<Room>();
                generateCenter=false;
            }
            if (generateCenter)
            {
                int centerSlect=Random.Range(0,potentialCenter.Length);
                Instantiate(potentialCenter[centerSlect],outlines.transform.position,transform.rotation).theRoom=outlines.GetComponent<Room>();
            }
            
        }
    }

    
    void Update()
    {
        
    }

    public void MoveGeneratorPoint()
    {
        switch (slectedDirecion)
        {
            case Direction.up:
                generatorPoint.position+=new Vector3(0f,yOffset,0f);
                break;
            case Direction.down:
                generatorPoint.position+=new Vector3(0f,-yOffset,0f);
                break;
            case Direction.left:
                generatorPoint.position+=new Vector3(-xOffset,0f,0f);
                break;
            case Direction.right:
                generatorPoint.position+=new Vector3(xOffset,0f,0f);
                break;            
        }
    }

    public void CreateRoomOutlined(Vector3 roomPosition)
    {
        bool roomAbove=Physics2D.OverlapCircle(roomPosition+new Vector3(0f,yOffset,0f),0.2f,whatIsRoom);
        bool roomBelow=Physics2D.OverlapCircle(roomPosition+new Vector3(0f,-yOffset,0f),0.2f,whatIsRoom);
        bool roomLeft=Physics2D.OverlapCircle(roomPosition+new Vector3(-xOffset,0f,0f),0.2f,whatIsRoom);
        bool roomRight=Physics2D.OverlapCircle(roomPosition+new Vector3(xOffset,0f,0f),0.2f,whatIsRoom);
        int directionCount=0;
        if (roomAbove)
        {
            directionCount++;
        }
        if (roomBelow)
        {
            directionCount++;
        }
        if (roomLeft)
        {
            directionCount++;
        }
        if (roomRight)
        {
            directionCount++;
        }
        switch (directionCount)
        {
            case 0:
                break;
            case 1:
                if (roomAbove)
                {
                    generatedOutlines.Add(Instantiate(room.singleUp,roomPosition,transform.rotation));
                }
                if (roomBelow)
                {
                    generatedOutlines.Add(Instantiate(room.singleDown,roomPosition,transform.rotation));
                }
                if (roomLeft)
                {
                    generatedOutlines.Add(Instantiate(room.singleLeft,roomPosition,transform.rotation));
                } 
                if (roomRight)
                {
                    generatedOutlines.Add(Instantiate(room.singleRight,roomPosition,transform.rotation));
                }
                break;
            case 2:
                if(roomAbove && roomBelow)
                {
                    generatedOutlines.Add(Instantiate(room.doubleUpDown,roomPosition,transform.rotation));
                }
                if(roomRight && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(room.doubleLeftRight,roomPosition,transform.rotation));
                }
                if(roomAbove && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(room.doubleUpLeft,roomPosition,transform.rotation));
                }
                if(roomLeft && roomBelow)
                {
                    generatedOutlines.Add(Instantiate(room.doubleDownLeft,roomPosition,transform.rotation));
                }
                if(roomRight && roomBelow)
                {
                    generatedOutlines.Add(Instantiate(room.doubleDownRight,roomPosition,transform.rotation));
                }
                if(roomAbove && roomRight)
                {
                    generatedOutlines.Add(Instantiate(room.doubleUpRight,roomPosition,transform.rotation));
                }
                break;
            case 3:
                if(roomAbove && roomBelow && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(room.tripleDownLeftUp,roomPosition,transform.rotation));
                }
                if(roomAbove && roomBelow && roomRight)
                {
                    generatedOutlines.Add(Instantiate(room.tripleUpRightDown,roomPosition,transform.rotation));
                }
                if(roomAbove && roomRight && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(room.tripleLeftUpRight,roomPosition,transform.rotation));
                }
                if(roomRight && roomBelow && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(room.tripleRightDownLeft,roomPosition,transform.rotation));
                }
                break;
            case 4:
                generatedOutlines.Add(Instantiate(room.fourway,roomPosition,transform.rotation));  
                break;                
        }
    }
}

[System.Serializable]
public class RoomPrefabs
{
    public GameObject singleUp,singleDown,singleRight,singleLeft,
    doubleUpDown,doubleLeftRight,doubleUpLeft,doubleDownLeft,doubleUpRight,doubleDownRight,
    tripleUpRightDown,tripleRightDownLeft,tripleDownLeftUp,tripleLeftUpRight,
    fourway;
}