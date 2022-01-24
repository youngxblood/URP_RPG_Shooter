using UnityEngine;
using System.Collections;

namespace Pathfinding
{
    /// <summary>
    /// Simple patrol behavior.
    /// This will set the destination on the agent so that it moves through the sequence of objects in the <see cref="targets"/> array.
    /// Upon reaching a target it will wait for <see cref="delay"/> seconds.
    ///
    /// See: <see cref="Pathfinding.AIDestinationSetter"/>
    /// See: <see cref="Pathfinding.AIPath"/>
    /// See: <see cref="Pathfinding.RichAI"/>
    /// See: <see cref="Pathfinding.AILerp"/>
    /// </summary>
    [UniqueComponent(tag = "ai.destination")]
    [HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_patrol.php")]
    public class Patrol : VersionedMonoBehaviour
    {
        /// <summary>Target points to move to in order</summary>
        public Transform[] targets;
        public Vector3[] randomTargets;
        public Vector3 currentTarget;
        public int numberOfTargets = 3;
        public float radiusOfPatrol = 1f;
        private Vector3 spawnLocation;

        /// <summary>Time in seconds to wait at each target</summary>
        public float delay = 0;

        /// <summary>Current target index</summary>
        int index;

        IAstarAI agent;
        float switchTime = float.PositiveInfinity;

        protected override void Awake()
        {
            base.Awake();
            agent = transform.root.GetComponent<IAstarAI>();
            spawnLocation = transform.position;

            randomTargets = new Vector3[numberOfTargets];
            for (int i = 0; i < randomTargets.Length; i++)
            {
                // Vector3 test = Random.insideUnitCircle * radiusOfPatrol;
                // test += spawnLocation;
                randomTargets[i] = PickRandomPoint();
            }

        }

        Vector3 PickRandomPoint()
        {
            var point = Random.insideUnitSphere * radiusOfPatrol;

            // point.y = 0;
            point += transform.position;
            return point;
        }

        public void StartPatrol()
        {
            if (randomTargets.Length == 0) return;

            bool search = false;

            // Note: using reachedEndOfPath and pathPending instead of reachedDestination here because
            // if the destination cannot be reached by the agent, we don't want it to get stuck, we just want it to get as close as possible and then move on.
            if (agent.reachedEndOfPath && !agent.pathPending && float.IsPositiveInfinity(switchTime))
            {
                switchTime = Time.time + delay;
            }

            if (Time.time >= switchTime)
            {
                index = index + 1;
                search = true;
                switchTime = float.PositiveInfinity;
            }

            index = index % randomTargets.Length;
            agent.destination = randomTargets[index];
            currentTarget = randomTargets[index];

            if (search) agent.SearchPath();
        }
    }
}