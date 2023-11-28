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
            foreach (GameObject next in current.gameObject.GetComponent<GridTile>().getSreach(1))  //ÁÖº¯ ÀÌ¿ô  Gird Å½»ö 
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




    private static float Cost(GameObject current, GameObject Next)
    {
        return Vector2.Distance(current.transform.position, Next.transform.position);
    }


    private static float heuristic(GameObject End, GameObject Next)
    {
        return Vector2.Distance(End.transform.position, Next.transform.position);
    }



}
