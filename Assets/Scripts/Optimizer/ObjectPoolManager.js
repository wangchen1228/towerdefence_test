#pragma strict
#pragma downcast

//~ @script ExecuteInEditMode()

// A general pool object for reusable game objects.
//
// It supports spawning and unspawning game objects that are
// instantiated from a common prefab. Can be used preallocate
// objects to avoid calls to Instantiate during gameplay. Can
// also create objects on demand (which it does if no objects
// are available in the pool).
class GameObjectPool {
	// The prefab that the game objects will be instantiated from.
	private var prefab : GameObject;
	// The list of available game objects (initially empty by default).
	private var available : Stack;
	// The list of all game objects created thus far (used for efficiently
	// unspawning all of them at once, see UnspawnAll).
	private var all : ArrayList;

	// An optional function that will be called whenever a new object is instantiated.
	// The newly instantiated object is passed to it, which allows users of the pool
	// to do custom initialization.
	private var initializationFunction : Function;
	// Indicates whether the pool's game objects should be activated/deactivated
	// recursively (i.e. the game object and all its children) or non-recursively (just the
	// game object).
	private var setActiveRecursively : boolean;
	
	var ID:int;

	// Creates a pool.
	// The initialCapacity is used to initialize the .NET collections, and determines
	// how much space they pre-allocate behind the scenes. It does not pre-populate the
	// collection with game objects. For that, see the PrePopulate function.
	// If an initialCapacity that is <= to zero is provided, the pool uses the default
	// initial capacities of its internal .NET collections.
	function GameObjectPool(prefab : GameObject, initialCapacity : int, initializationFunction : Function, setActiveRecursively : boolean, id){
		ID=id;
		this.prefab = prefab;
		if(initialCapacity > 0){
			this.available = Stack(initialCapacity);
			this.all = ArrayList(initialCapacity);
		} else {
			// Use the .NET defaults
			this.available = Stack();
			this.all = ArrayList();
		}
		this.initializationFunction = initializationFunction;
		this.setActiveRecursively = setActiveRecursively;
	}

	// Spawn a game object with the specified position/rotation.
	function Spawn(position : Vector3, rotation : Quaternion) : GameObject {
		var result : GameObject;

		if(available.Count == 0){
			// Create an object and initialize it.
			result = GameObject.Instantiate(prefab, position, rotation) as GameObject;
			var id:IDTag=result.AddComponent.<IDTag>();
			id.ID=ID;
			//~ result.SendMessage("Start");
			if(initializationFunction != null){
				initializationFunction(result);
			}
			// Keep track of it.
			all.Add(result);
			
			//~ Debug.Log("spawn a new");
			
		} else {
			result = available.Pop() as GameObject;
			// Get the result's transform and reuse for efficiency.
			// Calling gameObject.transform is expensive.
			var resultTrans = result.transform;
			resultTrans.position = position;
			resultTrans.rotation = rotation;

			//~ result.SetActiveRecursively(true);
			this.SetActive(result, true);
			
			//~ Debug.Log("spawn from pool");
		}
		return result;
	}

	// Unspawn the provided game object.
	// The function is idempotent. Calling it more than once for the same game object is
	// safe, since it first checks to see if the provided object is already unspawned.
	// Returns true if the unspawn succeeded, false if the object was already unspawned.
	function Unspawn(obj : GameObject) : boolean {
		if(!available.Contains(obj)){ // Make sure we don't insert it twice.
			available.Push(obj);
			this.SetActive(obj, false);
			return true; // Object inserted back in stack.
		}
		return false; // Object already in stack.
	}

	// Pre-populates the pool with the provided number of game objects.
	function PrePopulate(count : int){
		var array : GameObject[] = new GameObject[count];
		for(var i = 0; i < count; i++){
			array[i] = Spawn(Vector3.zero, Quaternion.identity);
		}
		for(var j = 0; j < count; j++){
			Unspawn(array[j]);
		}
		
		//~ Debug.Log(available.Count);
	}

	// Unspawns all the game objects created by the pool.
	function UnspawnAll(){
		for(var i = 0; i < all.Count; i++){
			var obj : GameObject = all[i] as GameObject;
			if(obj!=null && obj.active)
				Unspawn(obj);
		}
	}

	// Unspawns all the game objects and clears the pool.
	function Clear(){
		UnspawnAll();
		available.Clear();
		all.Clear();
	}
	
	function GetTotalCount() : int {
		return all.Count;
	}

	// Returns the number of active objects.
	function GetActiveCount() : int {
		return all.Count - available.Count;
	}

	// Returns the number of available objects.
	function GetAvailableCount() : int {
		return available.Count;
	}

	// Returns the prefab being used by this pool.
	function GetPrefab() : GameObject {
		return prefab;
	}

	// Applies the provided function to some or all of the pool's game objects.
	function ForEach(func : Function, activeOnly : boolean){
		for(var i = 0; i < all.Count; i++){
			var obj : GameObject = all[i] as GameObject;
			if(!activeOnly || obj.active){
				func(obj);
			}
		}
	}

	// Activates or deactivates the provided game object using the method
	// specified by the setActiveRecursively flag.
	private function SetActive(obj : GameObject, val : boolean){
		if(setActiveRecursively){
			obj.SetActiveRecursively(val);
		}
		else{
			obj.active = val;
		}
	}
}





class ObjectPoolManager extends MonoBehaviour{
	//~ static var ObjectPoolArray:Array=new Array();
	static var ObjectPoolArray:GameObjectPool[]=new GameObjectPool[0];
	static var ObjectList:Array=new Array();

	static var ManagerInstance:ObjectPoolManager;
	
	static function Clear(){
		for(var pool:GameObjectPool in ObjectPoolArray){
			pool.Clear();
		}
		ObjectPoolArray=new GameObjectPool[0];
		ObjectList=new Array();
	}
	
	function UnspawnCoroutine(){
		
	}

	static function Init(){
		for(var pool:GameObjectPool in ObjectPoolArray){
			pool.Clear();
		}
		ObjectPoolArray=new GameObjectPool[0];
		ObjectList=new Array();
	}
	
	static function New(obj:Transform, num:int){
		New(obj.gameObject, num);
	}

	static function New(obj:GameObject, num:int){

		var match:boolean=false;
		var n:int=0;
		var matchID:int=0;
		var ID:int;

		if(ObjectPoolArray.length>0){
			for(var pool:GameObjectPool in ObjectPoolArray){
				if(pool.GetPrefab()==obj){
					matchID=n;
					match=true;
					ID=n;
				}
				n+=1;
			}
		}
		
		if(!match) ID=n;
		
		//~ Debug.Log(match+"  ID: "+ID);
		
		if(!match){
			var temp:GameObjectPool;
			var flag:boolean; //flag indicate weather object have any children so if SetActiveRecursively is needed
			if(obj.transform.childCount>0) flag=true;
			else flag=false;
			temp=GameObjectPool(obj, num, null, flag, ID);
			temp.PrePopulate(num);

			ObjectList.Add(temp);
			
			ObjectPoolArray = ObjectList.ToBuiltin(GameObjectPool);
		}
		else{
			var availableCount:int=ObjectPoolArray[matchID].GetTotalCount();
			ObjectPoolArray[matchID].PrePopulate(num+availableCount);
		}
	}
	
	

	static function Spawn(object:Transform, pos:Vector3, rot:Quaternion):Transform{
		var temp:Transform;
		for(var pool:GameObjectPool in ObjectPoolArray){
			if(pool.GetPrefab()==object.gameObject){
				temp=pool.Spawn(pos, rot).transform;
				break;
			}
		}
		return temp;
	}
	
	static function Spawn(object:GameObject, pos:Vector3, rot:Quaternion):GameObject{
		var temp:GameObject;
		for(var pool:GameObjectPool in ObjectPoolArray){
			if(pool.GetPrefab()==object){
				temp=pool.Spawn(pos, rot);
				break;
			}
		}
		return temp;
	}

	static function Unspawn(obj:GameObject):boolean{
		var flag:boolean=false;
		var ID:int=obj.GetComponent.<IDTag>().ID;
		for(var pool:GameObjectPool in ObjectPoolArray){
			//~ if(pool.GetPrefab().name+"(Clone)"==obj.gameObject.name){
			if(pool.ID==ID){
				pool.Unspawn(obj);
				flag=true;
				break;
			}
		}
		if(!flag) Debug.Log("no match "+ID);
		return flag;
	}
}

class IDTag extends MonoBehaviour{
	var ID:int;
}