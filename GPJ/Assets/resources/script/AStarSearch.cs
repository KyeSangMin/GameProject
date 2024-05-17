using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AStarSearch
{

    public static List<GameObject> Search(GameObject StartObject, GameObject EndObject)
    {
        Queue<GameObject> frontier = new Queue<GameObject>();
        List<GameObject> path = new List<GameObject>(); 
        GameObject Start = StartObject;
        GameObject End = EndObject;
        frontier.Enqueue(Start);
        Dictionary<GameObject, GameObject> came_from = new Dictionary<GameObject, GameObject>();
        Dictionary<GameObject, int> cost_so_far = new Dictionary<GameObject, int>();
        came_from[Start] = null;
        cost_so_far[Start] = 0;

        while (frontier.Count != 0)
        {
            GameObject current = frontier.Dequeue();
            if (current == End)
            {
                break;
            }
            foreach (GameObject next in current.gameObject.GetComponent<GridTile>().getSreach(1))  //주변 이웃  Gird 탐색 
            {
                int new_cost = cost_so_far[current] + (int)Cost(current, next);
                if (!cost_so_far.ContainsKey(next) || new_cost < cost_so_far[next])
                {
                    cost_so_far[next] = new_cost;
                    int priority = new_cost + (int)heuristic(End, next);
                    frontier.Enqueue(next);                     
                    //frontier.Put(next, priority);
                    came_from[next] = current;
                }
            }
        }

       
        GameObject currentObject = End;
        while (currentObject != Start)
        {
            path.Add(currentObject);
            currentObject = came_from[currentObject];
        }
        path.Add(Start);
        path.Reverse();
        return path;
    }

    public static List<GameObject> bfsSearch(GameObject StartObject, int range)
    {
        List<GameObject> aroundTile = new List<GameObject>();
        Queue<GameObject> frontier = new Queue<GameObject>();
        Dictionary<GameObject, int> distances = new Dictionary<GameObject, int>();
        GameObject start = StartObject;
        frontier.Enqueue(start);
        aroundTile.Add(start);
        distances[start] = 0;

        while (frontier.Count != 0)
        {
            GameObject current = frontier.Dequeue();
            int currentDistance = distances[current];

            foreach (GameObject next in current.gameObject.GetComponent<GridTile>().getSreach(1)) 
            {
                if(!aroundTile.Contains(next) && currentDistance + 1<= range)
                {
                    frontier.Enqueue(next);
                    aroundTile.Add(next);
                    distances[next] = currentDistance + 1;
                }
            }
        }
        return aroundTile;
    }
    public static GameObject bfsDistanceSearch(GameObject StartObject, int range)
    {
        GameObject targetObejct = null;
        GameObject AllyObejct = null;
        List<GameObject> aroundTile = new List<GameObject>();
        Queue<GameObject> frontier = new Queue<GameObject>();
        Dictionary<GameObject, int> distances = new Dictionary<GameObject, int>();
        GameObject start = StartObject;
        float targetDistance = 10000f;
        frontier.Enqueue(start);
        aroundTile.Add(start);
        distances[start] = 0;

        while (frontier.Count != 0)
        {
            GameObject current = frontier.Dequeue();
            int currentDistance = distances[current];

            foreach (GameObject next in current.gameObject.GetComponent<GridTile>().getSreach(1))
            {
                
                if(!aroundTile.Contains(next) && next.GetComponent<GridTile>().getState() == GridTile.TileState.Allypos && targetDistance > heuristic(start,next) && currentDistance <= range) // 범위 내 아군 캐릭터 있을경우
                {
                    AllyObejct = next;
                    targetObejct = next;
                    targetDistance = heuristic(start, next);
                    frontier.Enqueue(next);
                    aroundTile.Add(next);
                    distances[next] = currentDistance + 1;
                }
                
                else if (!aroundTile.Contains(next) && AllyObejct == null && next.GetComponent<GridTile>().getState() != GridTile.TileState.Enemypos && currentDistance <= range-1) // 범위 내 아군 캐릭터 없을경우 
                {
             
                    if (targetDistance > heuristicforbfsNearbySearch(bfsNearbySearch(start), next))
                    {
                        targetObejct = next;
                        targetDistance = heuristicforbfsNearbySearch(bfsNearbySearch(start), next);
                        
                    }
           
                    frontier.Enqueue(next);
                    aroundTile.Add(next);
                    distances[next] = currentDistance + 1;

                }
            }
        }
        return targetObejct;
    }

    public static GameObject bfsNearbySearch(GameObject StartObject)
    {
        GameObject targetObejct = null;
        List<GameObject> aroundTile = new List<GameObject>();
        Queue<GameObject> frontier = new Queue<GameObject>();
        Dictionary<GameObject, int> distances = new Dictionary<GameObject, int>();
        GameObject start = StartObject;
        frontier.Enqueue(start);
        aroundTile.Add(start);
        distances[start] = 0;

        while (frontier.Count != 0)
        {
            GameObject current = frontier.Dequeue();
            int currentDistance = distances[current];

            foreach (GameObject next in current.gameObject.GetComponent<GridTile>().getSreach(1))
            {
                if (!aroundTile.Contains(next) && currentDistance + 1 <= 13)
                {
                    if(next.GetComponent<GridTile>().getState() == GridTile.TileState.Allypos)
                    {
                        targetObejct = next;
                        break;
                    }
                    frontier.Enqueue(next);
                    aroundTile.Add(next);
                    distances[next] = currentDistance + 1;
                }
            }
            if (targetObejct != null) // 원하는 타일을 찾았을 때 루프 종료
            {
                break;
            }
        }

        //Debug.Log("bfsNearbySearch");
        //Debug.Log(targetObejct);
        return targetObejct;
    }

    public static GameObject bfsFarawaySearch(GameObject StartObject, int range)
    {
        GameObject targetObejct = null;
        GameObject AllyObejct = null;
        List<GameObject> aroundTile = new List<GameObject>();
        Queue<GameObject> frontier = new Queue<GameObject>();
        Dictionary<GameObject, int> distances = new Dictionary<GameObject, int>();
        GameObject start = StartObject;
        float targetDistance = 0f;
        frontier.Enqueue(start);
        aroundTile.Add(start);
        distances[start] = 0;

        while (frontier.Count != 0)
        {
            GameObject current = frontier.Dequeue();
            int currentDistance = distances[current];

            foreach (GameObject next in current.gameObject.GetComponent<GridTile>().getSreach(1))
            {

                if (!aroundTile.Contains(next) && next.GetComponent<GridTile>().getState() == GridTile.TileState.Allypos && targetDistance < heuristic(start, next) && currentDistance <= range) // 범위 내 아군 캐릭터 있을경우
                {
                    AllyObejct = next;
                    targetObejct = next;
                    targetDistance = heuristic(start, next);
                    frontier.Enqueue(next);
                    aroundTile.Add(next);
                    distances[next] = currentDistance + 1;
                }

                else if (!aroundTile.Contains(next) && AllyObejct == null && next.GetComponent<GridTile>().getState() != GridTile.TileState.Enemypos && currentDistance <= range - 1) // 범위 내 아군 캐릭터 없을경우 
                {
                    if (targetDistance < heuristic(bfsNearbySearch(start), next))
                    {
                        targetObejct = next;
                        targetDistance = heuristic(bfsNearbySearch(start), next);

                    }
                    frontier.Enqueue(next);
                    aroundTile.Add(next);
                    distances[next] = currentDistance + 1;

                }
            }
        }
        return targetObejct;


    }
    private static float Cost(GameObject current, GameObject Next)
    {
        return Vector2.Distance(current.transform.position, Next.transform.position);
    }


    private static float heuristic(GameObject End, GameObject Next)
    {

        return Vector2.Distance(End.transform.position, Next.transform.position);
    }

    private static float heuristicforbfsNearbySearch(GameObject End, GameObject Next)
    {
        
        if(End == null)
        {
            return Vector2.Distance(Next.transform.position, Next.transform.position);
        }
        
        return Vector2.Distance(End.transform.position, Next.transform.position);
    }

}
