using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace SpiroGraph2.Scripts
{
	public class SpiroGraph : MonoBehaviour
	{
		private bool          shouldDraw = false;
		private float         lastDrawTime;
		private List<Vector3> graphPoints;

		[SerializeField] private LineRenderer lineRenderer;
		[SerializeField] private LineRenderer lineRendererVisuals;

		[Space]
		[SerializeField] private Transform outerCircle;
		[SerializeField] private Transform innerCircle;
		[SerializeField] private Transform drawPoint;

		[Space]
		[Header("SpiroGraph Controls")]
		public float outerRadius = 5;
		public float innerRadius = 3;
		public float drawPointRadius = 1;
		[Range(10f, 1000f)] public float outerCircleRotationSpeed = 5;
		private float innerCircleRotationSpeed;

		[Space]
		[Header("SpiroGraph Optimisation")]
		public float maxGraphPoints = 5000;
		[Range(0.01f,  1f)] public float graphPointDistanceThreshold = .05f;
		[Range(0.005f, 1f)] public float drawInterval = .009f;


		public void ClearLineVisuals()
		{
			shouldDraw = true;
			lineRendererVisuals.positionCount = 0;

			outerCircle.gameObject.SetActive(false);

		}



		private void AddPointToGraph(Vector3 pointToDraw)
		{
			if (Time.time - lastDrawTime >= drawInterval)
			{
				lastDrawTime = Time.time;
				if (!graphPoints.Contains(pointToDraw))
				{
					if (graphPoints.Count > 0 && Vector3.Distance(pointToDraw, graphPoints[graphPoints.Count - 1]) > graphPointDistanceThreshold)
					{
						graphPoints.Add(pointToDraw);
					}
					else
					{
						graphPoints.Add(pointToDraw);
					}
					if (graphPoints.Count > maxGraphPoints)
					{
						ClearLineVisuals();
					}
				}

				DrawSpiroGraph();
			}

		}


		private void Awake()
		{
			graphPoints = new List<Vector3>();
			InitialisePoints();

		}


		private void ClearGraph()
		{
			graphPoints?.Clear();
			lineRenderer.positionCount = 0;

		}


		private void DrawSpiroGraph()
		{
			lineRenderer.positionCount = graphPoints.Count;

			for (int i = 0; i < graphPoints.Count; i++)
			{
				lineRenderer.SetPosition(i, graphPoints[i]);
			}

		}


		private void InitialisePoints()
		{
			innerCircleRotationSpeed  = outerCircleRotationSpeed * outerRadius / innerRadius;
			innerCircle.localPosition = new Vector3(outerRadius - innerRadius, 0, 0);
			drawPoint  .localPosition = new Vector3(drawPointRadius,           0, 0);

			UpdateLine();

		}


		private void LateUpdate()
		{
			if (shouldDraw) return;

			UpdateLine();

		}


		private void OnDrawGizmos()
		{
			Gizmos.color = Color.white;
			Gizmos.DrawWireSphere(transform.position, outerRadius);

			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(innerCircle.position, innerRadius);

			float drawSphereRadius = 0.1f;

			Gizmos.color = Color.magenta;
			Gizmos.DrawSphere(drawPoint.position, drawSphereRadius);

		}


		private void OnValidate()
		{
			shouldDraw = false;
			outerCircle.gameObject.SetActive(true);

			InitialisePoints();
			ClearGraph();

		}


		private void RotatePoints()
		{
			outerCircle.Rotate(transform.forward,  outerCircleRotationSpeed * Time.deltaTime);
			innerCircle.Rotate(-transform.forward, innerCircleRotationSpeed * Time.deltaTime);

		}


		private void Update()
		{
			if (shouldDraw)
				return;

			RotatePoints();
			AddPointToGraph(drawPoint.position);

		}


		private void UpdateLine()
		{
			lineRendererVisuals.positionCount = 3;
			lineRendererVisuals.SetPosition(0, transform  .position);
			lineRendererVisuals.SetPosition(1, innerCircle.position);
			lineRendererVisuals.SetPosition(2, drawPoint  .position);

		}


	}   // class SpiroGraph


	[CanEditMultipleObjects, CustomEditor(typeof(SpiroGraph))]
	public class SpiroGraphEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			SpiroGraph controller = target as SpiroGraph;
			EditorGUILayout.Space(5);
			GUILayout.BeginHorizontal();

			if (GUILayout.Button("Stop"))
			{
				controller.ClearLineVisuals();
			}

			GUILayout.EndHorizontal();

		}


	}   // class SpiroGraphEditor

}   // namespace SpiroGraph2.Scripts
