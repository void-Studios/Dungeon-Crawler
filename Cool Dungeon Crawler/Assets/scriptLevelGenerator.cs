using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Trail
{
    public int XPos{get;set;}
    public int YPos{get;set;}
    public int[] BoundingBox{get;set;}
}
public class scriptLevelGenerator : MonoBehaviour
{
    public GameObject dungeonHolder;
    public int dungeonLength;
    public GameObject enemyPrefab;
    int enemyCount;
    public GameObject fingPrefab;
    public GameObject doorPrefab;
    int doorCount;

    public Tilemap tilemap;
    public TileBase[] tilesUp;
    public TileBase[] tilesDown;
    public TileBase[] tilesRight;
    public TileBase[] tilesLeft;  
    public TileBase[] testTiles;
   
    public List<int[]> boundingUp = new List<int[]>();
    public List<int[]> boundingDown = new List<int[]>();
    public List<int[]> boundingRight = new List<int[]>();
    public List<int[]> boundingLeft = new List<int[]>();

    public List<Trail> trailInfo = new List<Trail>();
    public List<GameObject> entityList = new List<GameObject>();
 
    public GameObject Player;
    public GameObject healthBooster;
    public GameObject damageBooster;

    public Vector3Int position;
    public Vector3Int testPosition;
    public int posX;
    public int posY;
    public int prevX;
    public int prevY;
    public int[] currentBounding;
   
    public int facing;

    public bool drewSuccess;
    
    public bool isRetry;

    //0,1,2,3 order for sides
    //UP,DOWN,RIGHT,LEFT
   private void Start() {
        //Defaulting
        dungeonHolder = GameObject.FindGameObjectWithTag("dungeonHolder");

        Defaulting();
       
       //Declaring tile boundaries
       int[] bSNDead = new int[4]{0,1,0,0};
       int[] bNSDead = new int[4]{1,0,0,0};
       int[] bVertical = new int[4]{1,1,0,0};
       int[] bHorizontal = new int[4]{0,0,1,1};
       int[] bR = new int[4]{0,1,1,0};
       int[] bRInv = new int[4]{1,0,1,0};
       int[] bL = new int[4]{0,1,0,1};
       int[] bLInv = new int[4]{1,0,0,1};
       int[] bT = new int[4]{0,1,1,1};
       int[] bTInv = new int[4]{1,0,1,1};
       int[] bTVertical = new int[4]{1,1,1,0};
       int[] bTVerticalInv = new int[4]{1,1,0,1};
       int[] bWEDead = new int[4]{0,0,0,1};
       int[] bEWDead = new int[4]{0,0,1,0};
       
       //Lists for storage
       boundingUp = new List<int[]>(){bSNDead,bVertical,bR,bL,bT,bTVertical,bTVerticalInv};
       boundingDown = new List<int[]>() {bNSDead,bVertical,bRInv,bLInv,bTInv,bTVertical,bTVerticalInv};
       boundingRight = new List<int[]>(){bWEDead,bHorizontal,bL,bLInv,bT,bTInv,bTVerticalInv};
       boundingLeft = new List<int[]>(){bEWDead,bHorizontal,bR,bRInv,bT,bTInv,bTVertical};

        StartGame();
   }

   private void Update() {
       if (Input.GetKeyDown(KeyCode.R)){
            Defaulting();
            StartGame();
       }
   }

    public void StartGame()
    {
        int fail=0;
        
        while (trailInfo.Count<dungeonLength && fail<100)
        {           
            drewSuccess = DrawLevel();
            if (drewSuccess)
            {
                fail=0;
            }
            else if(!drewSuccess)
            {
                fail+=1;
            }
        }
        if (trailInfo.Count<dungeonLength)
        {
            //Debug.Log("Attempting Force. Current count is "+trailInfo.Count.ToString());
            bool wasForced = ForceDraw();
            if (!wasForced)
            {
                //Debug.Log("Force failed.");
                Cleanup();
            }
            else if (wasForced)
            {
                StartGame();    
            }
        }
        else
        {
            Cleanup();
        }
    }

    public bool DrawLevel()
        {
            //random thingy
            int tileRng = Random.Range(1,7);
            int directionRng = Random.Range(0,4);
            int testY;
            int testX;

            if (directionRng == 0 && currentBounding[0]==1)
            {
                testY = posY+1;
                testPosition = new Vector3Int(posX,testY,0);
                if (prevY==testY || tilemap.GetTile(testPosition)!=null)
                {
                    return false;
                
                }
                else
                {
                    prevY=posY;
                    prevX=posX;
                    posY+=1;
                    position = new Vector3Int(posX,posY,0);
                
                    tilemap.SetTile(position,tilesUp[tileRng]);
                    currentBounding = boundingUp[tileRng];
                    facing=directionRng;
                    TrailAdd();
                    return true;
                }
            }
            else if (directionRng ==1 && currentBounding[1]==1)
            {
                testY = posY-1;
                testPosition = new Vector3Int(posX,testY,0);
                if (prevY==testY || tilemap.GetTile(testPosition)!=null)
                {
                    return false;
                }
                else
                {
                    prevY=posY;
                    prevX=posX;
                    posY-=1;
                    position = new Vector3Int(posX,posY,0);
                
                    tilemap.SetTile(position,tilesDown[tileRng]);
                    currentBounding = boundingDown[tileRng];
                    facing=directionRng;
                    TrailAdd();
                    return true;
                }
            }
            else if (directionRng==2 && currentBounding[2]==1)
            {
                testX = posX+1;
                testPosition = new Vector3Int(testX,posY,0);
                if (prevX==testX || tilemap.GetTile(testPosition)!=null)
                {
                    return false;
                }
                else
                {
                    prevY=posY;
                    prevX=posX;
                    posX+=1;
                    position = new Vector3Int(posX,posY,0);
                
                    tilemap.SetTile(position,tilesRight[tileRng]);
                    currentBounding = boundingRight[tileRng];
                    facing=directionRng;
                    TrailAdd();
                    return true;
                }
            }
            else if (directionRng==3 && currentBounding[3]==1)
            { 
                testX = posX-1;
                testPosition = new Vector3Int(testX,posY,0);
                if (prevX==testX || tilemap.GetTile(testPosition)!=null)
                {
                    return false;
                }
                else
                {
                    prevY=posY;
                    prevX=posX;
                    posX-=1;
                    position = new Vector3Int(posX,posY,0);
                
                    tilemap.SetTile(position,tilesLeft[tileRng]);
                    currentBounding = boundingLeft[tileRng];
                    facing=directionRng;
                    TrailAdd();
                    return true; 
                }
            }
            else
            {
                return false;
            }
        }

    public bool ForceDraw()
    {   
        if (!isRetry)
        {
            int efef = trailInfo.Count;
            efef-=1;
            //Resetting last point for re attempt.
            tilemap.SetTile(position,null);
            trailInfo.RemoveAt(efef);
            prevX=trailInfo[efef-2].XPos;
            prevY=trailInfo[efef-2].YPos;

            posX=trailInfo[efef-1].XPos;
            posY=trailInfo[efef-1].YPos;
            currentBounding=trailInfo[efef-1].BoundingBox;
            isRetry=true;
            return true;
        }
        else
        {
            return false;
        } 
    }

    public void Defaulting()
    {
        for(int i = 0; i <entityList.Count; i++)
        {
            Destroy(entityList[i]);
        }

        entityList.Clear();
        trailInfo.Clear();
        isRetry=false;
        tilemap.ClearAllTiles();
        posX=0;
        posY=0;
        prevY=-1;
        prevX=-1;
        position = new Vector3Int(0,0,0);
        testPosition = new Vector3Int(0,0,0);
        currentBounding=new int[4]{1,1,1,1};
        enemyCount=0;
    
    
    }

    public void Cleanup()
    {
    
        tilemap.SetTile(position,testTiles[1]);
        Vector3 boosterSpawn = tilemap.CellToWorld(position);
        boosterSpawn = new Vector3(boosterSpawn.x+.3f,boosterSpawn.y,boosterSpawn.z);
        GameObject tempObj = Instantiate(healthBooster,boosterSpawn,healthBooster.transform.rotation, dungeonHolder.transform);
        entityList.Add(tempObj);
        boosterSpawn = new Vector3(boosterSpawn.x-.6f,boosterSpawn.y,boosterSpawn.z);
        tempObj = Instantiate(damageBooster,boosterSpawn,healthBooster.transform.rotation, dungeonHolder.transform);
        entityList.Add(tempObj);

        if (trailInfo.Count<dungeonLength)
        {
            Defaulting();
            StartGame();
           //Not enough length.
        }
        else
        {
            RecursiveClean();
            Player.transform.position = tilemap.CellToWorld(new Vector3Int(trailInfo[0].XPos,trailInfo[0].YPos,1));
        }
    

    }

    public void TrailAdd()
    {   
        trailInfo.Add(new Trail{XPos=posX,YPos=posY,BoundingBox=currentBounding});
    }

    public void SpawnDoor()
    {
        Vector3 doorSpawn = tilemap.CellToWorld(position);
        doorSpawn = new Vector3(doorSpawn.x, doorSpawn.y, doorSpawn.z);
        GameObject tempDoor = Instantiate(doorPrefab, doorSpawn, doorPrefab.transform.rotation, dungeonHolder.transform);
        doorCount += 1;
        entityList.Add(tempDoor);
    }

    public void RecursiveClean()
    {
        prevX=-2135;
        prevY=-2135;
        for (int i = 0; i < trailInfo.Count; i++)
        {
            int recursiveX=trailInfo[i].XPos;
            int recursiveY=trailInfo[i].YPos;
            int[] recursiveBounds=trailInfo[i].BoundingBox;
            Vector3Int recursivePosition = new Vector3Int(recursiveX,recursiveY,0);
            int testX;
            int testY;

            for (int ii = 0; ii < recursiveBounds.Length; ii++)
            {
                if (recursiveBounds[ii]==1)
                {
                    if (ii==0)
                    {
                        //UP
                        //tilemap.SetTile(recursivePosition,tilesUp[ii]);
                        testY = recursiveY+1;
                        testPosition = new Vector3Int(recursiveX,testY,0);
                        if (prevY==testY || tilemap.GetTile(testPosition)!=null)
                        {
                            //can't do anything
                        }
                        else
                        {
                            prevY=recursiveY;
                            prevX=recursiveX;
                        
                            position = new Vector3Int(recursiveX,testY,0);
                        
                            tilemap.SetTile(position,tilesUp[0]);
                            SpawnDoor();
                        }
                    


                    }else if (ii==1)
                    {
                        //DOWN
                        testY = recursiveY-1;
                        testPosition = new Vector3Int(recursiveX,testY,0);
                        if (prevY==testY || tilemap.GetTile(testPosition)!=null)
                        {
                            //can't do anything
                        }
                        else
                        {
                            prevY=recursiveY;
                            prevX=recursiveX;
                        
                            position = new Vector3Int(recursiveX,testY,0);
                        
                            tilemap.SetTile(position,tilesDown[0]);
                            SpawnDoor();
                        }
                    }else if (ii==2)
                    {
                        //RIGHT
                        testX = recursiveX+1;
                        testPosition = new Vector3Int(testX,recursiveY,0);
                        if (prevX==testX || tilemap.GetTile(testPosition)!=null)
                        {
                            //can't do anything
                        }
                        else
                        {
                            prevY=recursiveY;
                            prevX=recursiveX;
                        
                            position = new Vector3Int(testX,recursiveY,0);
                        
                            tilemap.SetTile(position,tilesRight[0]);
                            SpawnDoor();
                        }
                    }else if (ii==3)
                    {
                        //LEFT
                        testX = recursiveX-1;
                        testPosition = new Vector3Int(testX,recursiveY,0);
                        if (prevX==testX || tilemap.GetTile(testPosition)!=null)
                        {
                            //can't do anything
                        }
                        else
                        {
                            prevY=recursiveY;
                            prevX=recursiveX;
                        
                            position = new Vector3Int(testX,recursiveY,0);
                        
                            tilemap.SetTile(position,tilesLeft[0]);
                            SpawnDoor();
                        }
                    }
                }
            }
        }
    }
}
//420 note cuz reasons.