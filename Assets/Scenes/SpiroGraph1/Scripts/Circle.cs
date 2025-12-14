using UnityEngine;


namespace SpiroGraph1.Scripts
{
    public class Circle : MonoBehaviour
    {
        public float   Radius;
        public Vector3 Center;
        public float   AngleIncrement;



        private SpriteRenderer _spriteRenderer;
        private bool           _displaySprite;



        private void Start()
        {
            this._spriteRenderer = GetComponent<SpriteRenderer>();
            this.SetRadius(this.Radius);
            this.transform.position = this.Center;

        }   // Start()



        public void Iterate()
        {
            this.transform.Rotate(0, 0, this.AngleIncrement);

        }   // Iterate()


        public void SetCenter(Vector3 center)
        {
            this.Center             = center;
            this.transform.position = center;

        }   // SetCenter()


        public void SetRadius(float radius)
        {
            this.Radius               = radius;
            this.transform.localScale = new Vector3(radius, radius, 0);

        }   // SetRadius()


        public void SpriteVisible(bool condition)
        {
            this._spriteRenderer.enabled = condition;

        }   // SpriteVisible()


    }   // class Circle

}   // namespace 2D_URP_Spinning_Circle.SpiroGraph1.Scripts
