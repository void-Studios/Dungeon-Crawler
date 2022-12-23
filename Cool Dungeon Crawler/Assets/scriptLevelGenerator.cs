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
    #region BetterVariables
    private List<TrailPoint> Trail=new List<TrailPoint>();


    #endregion

    private GameObject Apostle;
    private scriptApostle scriptApostle;

    public GameObject playerRoot;
    private StatsHandler PlayerStats;

    public GameObject dungeonHolder;
    public int dungeonLength;
    public GameObject enemyPrefab;
    private int enemyCount;
    public GameObject fingPrefab;
    public GameObject doorPrefab;
    private int doorCount;

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
    
    public BoundingListStorage StorageList;
    public TilesListStorage TilesList;
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
    private void Awake()
    {
        Apostle = FindObjectOfType<scriptApostle>().transform.gameObject;

        playerRoot = FindObjectOfType<StatsHandler>().transform.gameObject;
        PlayerStats = playerRoot.GetComponent<StatsHandler>();
        Apostle.GetComponent<scriptApostle>().SetPlayerObject(playerRoot);
        dungeonHolder = GameObject.FindGameObjectWithTag("dungeonHolder");
    }


    private void Start() 
    {

        //Defaulting
        Defaulting();
       
       //Declaring tile boundaries[NSEW]
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
            GodEye.SetHasInitialized(false);
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

    private bool BetterStart()
    {
        #region Variable declaration for BetterStart() Usage
        //Individual Bounding of each Tile, to be further changed with 
        //JSON->LIST->Object
        TileBoundings boundingSNDeadend = new TileBoundings( 0, 0, 1, 0 );
        TileBoundings boundingNSDeadend = new TileBoundings(1, 0, 0, 0 );
        TileBoundings boundingVertical = new TileBoundings(1, 0, 1, 0 );
        TileBoundings boundingHorizontal = new TileBoundings(0, 1, 0, 1 );
        TileBoundings boundingElbowRight = new TileBoundings(0, 1, 1, 0 );
        TileBoundings boundingElbowRightInv = new TileBoundings(1, 1, 0, 0 );
        TileBoundings boundingElbowLeft = new TileBoundings(0, 0, 1, 1 );
        TileBoundings boundingElbowLeftInv = new TileBoundings(1, 0, 0, 1 );
        TileBoundings boundingHorizontalT = new TileBoundings(0, 1, 1, 1 );
        TileBoundings boundingHorizontalTInv = new TileBoundings(1, 1, 0, 1 );
        TileBoundings boundingVerticalT = new TileBoundings(1, 1, 1, 0 );
        TileBoundings boundingVerticalTInv = new TileBoundings(1, 0, 1, 1 );
        TileBoundings boundingWEDeadend = new TileBoundings(0, 0, 0, 1 );
        TileBoundings boundingbEWDeadend = new TileBoundings(0, 1, 0, 0 );

        //ListStorage
        BoundingList boundingsN = new BoundingList(boundingSNDeadend, boundingVertical, boundingElbowRight, boundingElbowLeft, boundingHorizontalT, boundingVerticalT, boundingVerticalTInv);
        BoundingList boundingsE = new BoundingList(boundingWEDeadend, boundingHorizontal, boundingElbowLeft, boundingElbowLeftInv, boundingHorizontalT, boundingHorizontalTInv, boundingVerticalTInv);
        BoundingList boundingsS = new BoundingList(boundingNSDeadend, boundingVertical, boundingElbowRightInv, boundingElbowLeftInv, boundingHorizontalTInv, boundingVerticalT, boundingVerticalTInv);
        BoundingList boundingsW = new BoundingList(boundingbEWDeadend, boundingHorizontal, boundingElbowRight, boundingElbowRightInv, boundingHorizontalT, boundingHorizontalTInv, boundingVerticalT);

        //Storage for the Lists of Lists
       

        StorageList = new BoundingListStorage(boundingsN, boundingsE, boundingsS, boundingsW);
        TilesList = new TilesListStorage(tilesUp, tilesRight, tilesDown, tilesLeft);
        #endregion
        return true;
    }
    private bool BetterDefault()
    {
        GodEye.SetDefaultPlayer();
        PlayerStats.BetterDefault();
        //Calculate new weapon and send info
        int v = Random.Range(0, scriptApostle.WeaponList.Length);
        int tempRandom = v;
        GodEye.SetWeaponCurrentActive(scriptApostle.WeaponList[tempRandom]);
        scriptApostle.SetWeaponCurrent();

        return true;
    }
    private bool BetterDrawLevel()
    {
        //Variables for the Randomizer
        BoundingList currentBoundingsList;
        int RandomTile = Random.Range(1, 7);
        Direction RandomDirection = new Direction(Random.Range(0, 4));
        Vector2Int trailPosition = new Vector2Int(0, 0);

        //TrailPoint for DrawTile
        currentBoundingsList = StorageList.Get(RandomDirection.GetDirection());
        TileBoundings _tempTileBoundings = currentBoundingsList.Get(RandomTile);
        TileBoundings _currentBounding = new TileBoundings(1, 1, 1, 1);
        TrailPoint trailPoint = new TrailPoint(trailPosition, RandomDirection, RandomTile, false, _tempTileBoundings);

        //GetTileBoundings(X,Y) or from PreviousTrailPoint From Trail

        //Level Difficulty drawing loop 


        for (int i = 0; i < (GodEye.GetWorldDifficulty()*5); i++)
        {
            if (Trail.Count != 0)
            {
                RandomTile = Random.Range(1, 7);
                RandomDirection = new Direction(Random.Range(0, 4));

                TrailPoint _lastTrailpoint = Trail[Trail.Count - 1];
                _currentBounding = _lastTrailpoint.boundings;
                currentBoundingsList = StorageList.Get(RandomDirection.GetDirection());
                _tempTileBoundings = currentBoundingsList.Get(RandomTile);

                //Prior to here POSX and POSY is still current drawn tile
                Vector2Int value = new Vector2Int(_lastTrailpoint.trailPosition.x, _lastTrailpoint.trailPosition.y);
                trailPosition = RandomDirection.UpdateTestVector2(value, RandomDirection);
                
                trailPoint = new TrailPoint(trailPosition, RandomDirection, RandomTile, false, _tempTileBoundings);
            }
            //Loops starting from Direction to attempt all possible exits to CurrentTile
            ScanDirection(trailPoint, _currentBounding);
        }
        return true;
    }

    private void ScanDirection(TrailPoint trailPoint, TileBoundings _currentBounding)
    {
        for (int i = 0; i < 3; i++)
        {
            if (_currentBounding.Get(trailPoint.direction.direction) == 0)
            {
                trailPoint.Next();
                continue;
            }

            if (DrawTile(trailPoint))
            {
                break;
            }
        }
        //it did not work at all so this should be false

    }

    private bool DrawTile(TrailPoint trailPoint)
    {
        //for loop to check if trailinfo matches an entry from trail[]
        Vector2Int _testPosition = trailPoint.direction.UpdateTestVector2(trailPoint.trailPosition, trailPoint.direction);
        Vector3Int _testTileAt = new Vector3Int(_testPosition.x, _testPosition.y, 0);


        if (tilemap.GetTile(_testTileAt) != null)
        {
            trailPoint.Next();



            return false;
        }
        else
        {
            tilemap.SetTile(_testTileAt, TilesList.Get(trailPoint.direction.direction)[trailPoint.tile]);
            trailPoint.trailPosition = new Vector2Int(_testTileAt.x, _testTileAt.y);
            
            trailPoint.isSuccess = true;
            BetterTrailAdd(trailPoint);
            return true;
        }
    }

    private void BetterTrailAdd(TrailPoint trailPoint)
    {
        Trail.Add(trailPoint);
    }

    private bool DrawTrail(List<TrailPoint> Trail)
    {
        for (int i = 0; i < Trail.Count-1; i++)
        {
            DrawTile(Trail[i]);
        }

        return true;
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
        PlayerStats.DefaultPlayer();
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

        //Level has been generated and we proceed to confirm if it's a valid level. 

        if (trailInfo.Count<dungeonLength)//Invalid level therefore scrap and retry
        {
            Defaulting();
            StartGame();
           
        }
        else //Successful level creation and therefore final cleanup and player spawn.
        {
            RecursiveClean();
            playerRoot.transform.position = tilemap.CellToWorld(new Vector3Int(trailInfo[0].XPos,trailInfo[0].YPos,1));
            GodEye.SetHasInitialized(true);
            Debug.Log("Initialized...");
            //Game has finished initializing. no further lines below please. :c
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