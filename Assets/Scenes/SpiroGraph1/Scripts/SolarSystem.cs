using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace SpiroGraph1.Scripts
{
    public class SolarSystem : MonoBehaviour
    {
        [Range(_minNumCircles, _maxNumCircles)] public int   numberOfCircles = 4;
        [Range(_minRatio,      _maxRatio)]      public float smallerCircleRadiusRatio;



        [SerializeField] private GameObject _circlePrefab;
        [SerializeField] private GameObject _penPointPrefab;
        [SerializeField] private Slider     _ratioSlider;
        [SerializeField] private Slider     _penPointPosSlider;
        [SerializeField] private Dropdown   _numCircDropdown;
        [SerializeField] private Button     _drawButton;
        [SerializeField] private Material   _largeCircleMat;
        [SerializeField] private Material   _smallCirclesMat;
        [SerializeField] private Sprite     _largeCircleSprite;
        [SerializeField] private Sprite     _smallCircleSprite;

        private const int    _maxNumCircles = 4;
        private const int    _minNumCircles = 2;
        private const float  _maxRatio      = 0.9f;
        private const float  _minRatio      = 0.1f;

        private float        _angleIncr     = 1.1f;
        private int          _batchSize;
        private int          _batchSizeMax  = 30;
        private int          _batchSizeMin  = 8;
        private GameObject[] _circles;
        private int          _currentIteration;
        private bool         _drawing;
        private int          _indexOfLastCircle;
        private float        _largeCircleRadius;
        private int          _numIterations = 8000;
        private GameObject   _penPoint;



        private void Start()
        {
            smallerCircleRadiusRatio = _ratioSlider.value;

            // Construct Circle hierarchy
            _circles    = new GameObject[numberOfCircles];
            _circles[0] = Instantiate(_circlePrefab);

            GameObject first = _circles[0];

            first.GetComponent<Circle>()        .Center           = transform.position;
            first.GetComponent<Circle>()        .AngleIncrement   = _angleIncr;
            first.GetComponent<SpriteRenderer>().material         = _largeCircleMat;
            first.GetComponent<SpriteRenderer>().sprite           = _largeCircleSprite;
            first.GetComponent<SpriteRenderer>().sortingLayerName = "Sun";

            _indexOfLastCircle = numberOfCircles - 1;

            for (int i = 1; i < numberOfCircles; i++)
            {
                _circles[i] = Instantiate(_circlePrefab);

                GameObject current = _circles[i];
                current.transform.SetParent(_circles[i - 1].transform, false);
                current.GetComponent<SpriteRenderer>().material = _smallCirclesMat;
                current.GetComponent<SpriteRenderer>().sprite   = _smallCircleSprite;
                current.GetComponent<SpriteRenderer>().sortingOrder = i;

                float sign = 1 - (i % 2) * 2; // Mathf.Pow(-1, i);

                current.GetComponent<Circle>().AngleIncrement = sign * (_angleIncr / Mathf.Pow(smallerCircleRadiusRatio, i));
            }

            for (int i = 1; i < numberOfCircles; i++)
            {
                float centerY     = 4 - 4*Mathf.Pow(smallerCircleRadiusRatio, i);
                Circle circleComp = _circles[i].GetComponent<Circle>();

                circleComp.SetRadius(smallerCircleRadiusRatio);
                circleComp.SetCenter(new Vector3(0, centerY, 0) + transform.position);
            }

            _penPoint = Instantiate(_penPointPrefab);
            _penPoint.transform.SetParent(_circles[_circles.Length - 1].transform, false);

            _numCircDropdown.value = 1;

            SetPenPointPosition();
            OnNumberOfCirclesValueChanged();

        }   // Start()


        public void RunDrawing()
        {
            SetInteractable(false);

            _currentIteration = 0;
            _drawing = true;

            _penPoint.GetComponent<PenPoint>().SetPositionArray(_numIterations);

            StartCoroutine(Draw());

        }   // RunDrawing()


        private void SetInteractable(bool condition)
        {
            _drawButton       .interactable = condition;
            _ratioSlider      .interactable = condition;
            _penPointPosSlider.interactable = condition;
            _numCircDropdown  .interactable = condition;

        }   // SetInteractable()


        public void SetPenPointPosition()
        {
            PenPoint penComp        = _penPoint.GetComponent<PenPoint>();
            Circle   lastCircleComp = _circles[_indexOfLastCircle].GetComponent<Circle>();
            float    penPointPosY   = _penPointPosSlider.value * Mathf.Pow(smallerCircleRadiusRatio, _indexOfLastCircle) + _circles[_indexOfLastCircle].transform.position.y;

            penComp.SetCenter(new Vector3(0, penPointPosY, 0) + transform.position);

        }   //  SetPenPointPosition()


        public void Reset()
        {
            _drawing = false;
            _penPoint.GetComponent<PenPoint>().ResetCurve();

            SetPenPointPosition();
            ToggleSpritesVisible(true);
            OnSmallerCircleRatioChanged();
            SetInteractable(true);

        }   //  Reset()


        private protected IEnumerator Draw()
        {
            while (_currentIteration < _numIterations && _drawing)
            {
                int max = (_currentIteration + _batchSize < _numIterations ? _currentIteration + _batchSize : _numIterations);

                for (int i = _currentIteration; i < max; i++)
                {
                    for (int j = _circles.Length - 1; j >= 0; j--)
                    {
                        Circle circleComp = _circles[j].GetComponent<Circle>();
                        circleComp.Iterate();
                    }

                    _penPoint.GetComponent<PenPoint>().StorePoint(i);
                }

                // Speed up drawing by increasing batchsize
                _currentIteration += _batchSize;
                _currentIteration = (_currentIteration < _numIterations)
                                  ?  _currentIteration
                                  :  _numIterations;

                _penPoint.GetComponent<PenPoint>().DrawCurve(_currentIteration);

                int batchSizeDelta = Mathf.RoundToInt(Mathf.Sqrt(_currentIteration * 0.1f)); //Mathf.RoundToInt(Mathf.Log10(10 + currentIteration));

                _batchSize = (_batchSizeMin + batchSizeDelta < _batchSizeMax)
                           ?  _batchSizeMin + batchSizeDelta
                           :  _batchSizeMax;

                yield return new WaitForSeconds(0.04f);
            }

            ToggleSpritesVisible(!_drawing);

        }   //  Draw()


        private void ToggleSpritesVisible(bool condition)
        {
            for (int i = 1; i < _circles.Length; i++)
            {
                _circles[i].GetComponent<SpriteRenderer>().enabled = condition;
            }

            _penPoint.GetComponent<PenPoint>().SpriteVisible(condition);

        }   // ToggleSpritesVisible()


        public void OnNumberOfCirclesValueChanged()
        {
            int index = _numCircDropdown.value + 2;

            for (int i = 0; i < _circles.Length; i++)
            {
                _circles[i].SetActive(i < index);
            }

            _penPoint.transform.SetParent(_circles[index - 1].transform, false);
            _indexOfLastCircle = index - 1;

            SetPenPointPosition();

        }   // OnNumberOfCirclesValueChanged()


        public void OnSmallerCircleRatioChanged()
        {
            smallerCircleRadiusRatio = _ratioSlider.value;

            for (int i = 1; i < numberOfCircles; i++)
            {
                Circle circleComp = _circles[i].GetComponent<Circle>();
                circleComp.SetRadius(smallerCircleRadiusRatio);

                float centerY = 4 - 4 * Mathf.Pow(smallerCircleRadiusRatio, i);
                circleComp.SetCenter(new Vector3(0, centerY, 0) + transform.position);
                float sign = Mathf.Pow(-1, i);

                _circles[i].GetComponent<Circle>().AngleIncrement = sign * (_angleIncr / Mathf.Pow(smallerCircleRadiusRatio, i));
            }

            SetPenPointPosition();

        }   // OnSmallerCircleRatioChanged()


    }   // class SolarSystem

}   // namespace SpiroGraph1.Scripts
