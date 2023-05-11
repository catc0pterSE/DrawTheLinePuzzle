using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Gameplay.Player
{
    public class RouteFollower : MonoBehaviour
    {
        [SerializeField] private float _travelTime = 5f;

        private Coroutine _movingRoutine;

        public bool IsReached { get; private set; }

        public event Action Reached;

        public void StartMoving(Vector3[] route)
        {
            if (_movingRoutine != null)
                StopCoroutine(_movingRoutine);

            _movingRoutine = StartCoroutine(Move(route));
        }

        private IEnumerator Move(Vector3[] route)
        {
            /*WaitForSeconds delay = new WaitForSeconds(_travelTime / route.Length);

            foreach (var point in route)
            {
                transform.position = point;
                yield return delay;
            }*/

            float speed = CalculateRouteLength(route);

            int i = 0;
            while (i < route.Length-1)
            {
                Vector3 currentPosition = transform.position;
                Vector3 target = route[i];

                if (Vector3.Distance(currentPosition, target) > Mathf.Epsilon)
                    transform.position = Vector3.MoveTowards(currentPosition, target, speed*Time.deltaTime);
                else
                    i++;

                yield return null;
            }


            IsReached = true;
            Reached?.Invoke();
        }

        private float CalculateRouteLength(Vector3[] route)
        {
            float length = 0;

            for (int i = 1; i < route.Length; i++)
            {
                length += Vector3.Distance(route[i], route[i - 1]);
            }

            return length;
        }
    }
}