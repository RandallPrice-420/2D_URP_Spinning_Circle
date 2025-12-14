using UnityEngine;


namespace SpiroGraph1.Scripts
{
    public class PenPoint : MonoBehaviour
    {
        public Vector3 Center;



        private bool           _displaySprite;
        private LineRenderer   _lineRenderer;
        private Vector3[]      _positionArray;
        private SpriteRenderer _spriteRenderer;



        private void Start()
        {
            this._spriteRenderer = GetComponent<SpriteRenderer>();
            this._lineRenderer = GetComponent<LineRenderer>();

        }   // Start()


        public void DrawCurve(int count)
        {
            this._lineRenderer.positionCount = count;
            this._lineRenderer.SetPositions(this._positionArray);

        }   // DrawCurve()


        public void SetCenter(Vector3 center)
        {
            this.Center = center;
            this.transform.position = center;

        }   // SetCenter()


        public void SetPositionArray(int size)
        {
            this._positionArray = new Vector3[size];

        }   // SetPositionArray()


        public void SpriteVisible(bool condition)
        {
            this._spriteRenderer.enabled = condition;

        }   // SpriteVisible()


        public void StorePoint(int index)
        {
            Vector3 newPosition        = this.transform.position;
            this._positionArray[index] = newPosition;

        }   // StorePoint()


        public void ResetCurve()
        {
            this._positionArray = new Vector3[0];
            this.DrawCurve(this._positionArray.Length);

        }   // ResetCurve()


    }   // class PenPoint


}   // namespace SpiroGraph1.Scripts