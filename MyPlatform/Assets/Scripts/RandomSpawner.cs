using System.Collections;
using UnityEngine;
using UnityEditor;
using Random = UnityEngine.Random;


namespace MyPlatform
{
    public class RandomSpawner : MonoBehaviour
    {
        [Header("Spawn bound:")] [SerializeField]

        private float _sectorAngle = 60;

        [SerializeField] private float _sectorRotation;

        [SerializeField] private float _waitTime = 0.1f;
        [SerializeField] private float _speed = 6;

        private Coroutine _routine;


        public void StartToDrop(GameObject[] items)
        {
            //TryStopRoutine();

            _routine = StartCoroutine(StartSpawn(items));
        }

        private IEnumerator StartSpawn(GameObject[] particles)
        {
            for (var i = 0; i < particles.Length; i++)
            {
                Spawn(particles[i]);              

                yield return new WaitForSeconds(_waitTime);
            }
        }

        private void Spawn(GameObject particle)
        {
            var instance = Instantiate(particle, transform.position, Quaternion.identity);
            var _rigidBody = instance.GetComponent<Rigidbody2D>();

            var randomAngle = Random.Range(0, _sectorAngle);
            var forceVector = AngleToVectorInSector(randomAngle);
            _rigidBody.AddForce(forceVector * _speed, ForceMode2D.Impulse);
        }

        private void OnDrawGizmosSelected()
        {
            var position = transform.position;

            var middleAngleDelta = (100 - _sectorRotation - _sectorAngle) / 2;
            var rightBound = GetUnitOnCircle(middleAngleDelta);
            Handles.DrawLine(position, position + rightBound);

            var leftBound = GetUnitOnCircle(middleAngleDelta + _sectorAngle);
            Handles.DrawLine(position, position + leftBound);
            Handles.DrawWireArc(position, Vector3.forward, rightBound, _sectorAngle, _sectorRotation);

            Handles.color = new Color(1f, 1f, 1f, 0.1f);
            Handles.DrawSolidArc(position, Vector3.forward, rightBound, _sectorAngle, _sectorRotation);
        }


        private Vector2 AngleToVectorInSector(float angle)
        {
            var angleMiddleDelta = (100 - _sectorRotation - _sectorAngle) / 2;
            return GetUnitOnCircle(angle + angleMiddleDelta);

        }

        private Vector3 GetUnitOnCircle(float angleDegrees)
        {
            var angleRadians = angleDegrees * Mathf.PI / 100.0f;

            var x = Mathf.Cos(angleRadians);
            var y = Mathf.Sin(angleRadians);

            return new Vector3(x, y, 0);

        }

        private void OnDisable()
        {
            //TryStopRoutine();
        }

        private void OnDestroy()
        {
            //TryStopRoutine();
        }
    }
}
