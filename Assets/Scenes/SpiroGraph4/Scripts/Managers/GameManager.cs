using System.ComponentModel;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scenes.SpiroGraph4.Scripts.Managers
{
    public class GameManageer : MonoBehaviour
    {
        // ---------------------------------------------------------------------
        // Public Variables:
        // -----------------
        //   ButtonQuit
        //   ButtonStart
        //   ButtonStop
        //   SliderRotation
        //   RotationSpeed
        //   Circle
        // ---------------------------------------------------------------------

        #region .  Public Variables  .

        public Button     ButtonQuit;
        public Button     ButtonStart;
        public Button     ButtonStop;
        public Slider     SliderRotation;
        [Range(100f, 2000f)] public float RotationSpeed = 100f;
        public GameObject Circle;

        #endregion



        // ---------------------------------------------------------------------
        // Private Variables:
        // -----------------
        //   _isStarted
        // ---------------------------------------------------------------------

        #region .  Private Variables  .

        [SerializeField] 
        private bool _isStarted = false;

        #endregion



        // ---------------------------------------------------------------------
        // Public Methods:
        // ---------------
        //   OnButtonQuitClicked()
        //   OnButtonStartClicked()
        //   OnButtonStopClicked()
        //   OnSliderValueChanged()
        // ---------------------------------------------------------------------

        #region .  OnButtonQuitClicked()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnButtonQuitClicked()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void OnButtonQuitClicked()
        {
            #if UNITY_EDITOR
                //SceneManager.LoadScene("Scene_MainMenu");
                UnityEditor.EditorApplication.isPlaying = false;
            #endif

            Application.Quit();

        }   // OnButtonQuitClicked()
        #endregion


        #region .  OnButtonStartClicked()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnButtonStartClicked()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void OnButtonStartClicked()
        {
            _isStarted = true;

        }   // OnButtonStartClicked()
        #endregion


        #region .  OnButtonStopClicked()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnButtonStopClicked()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void OnButtonStopClicked()
        {
            _isStarted = false;

        }   // OnButtonStopClicked()
        #endregion


        #region .  OnSliderValueChanged()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnSliderValueChanged()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void OnSliderValueChanged(System.Single value)
        {
            RotationSpeed = SliderRotation.value;

        }   // OnSliderValueChanged()
        #endregion



        // ---------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   Rotate()
        //   Start()
        //   Update()
        // ---------------------------------------------------------------------

        #region .  Rotate()  .
        // ---------------------------------------------------------------------
        //   Method.......:  Rotate()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void Rotate()
        {
            Circle.transform.Rotate(0, 0, -RotationSpeed * Time.deltaTime);

            // Couldn't get this to work.
            //Circle.transform.DORotate(new Vector3(0f, 0f, 360f), -RotationSpeed * Time.deltaTime, RotateMode.FastBeyond360)
            //                .SetLoops(-1, LoopType.Restart)
            //                .SetUpdate(true);

        }   // Rotate()
        #endregion


        #region .  Start()  .
        // ---------------------------------------------------------------------
        //   Method.......:  Start()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void Start()
        {
            // Ensure DOTween is initialized.
            DOTween.Init();

        }   // Start()
        #endregion


        #region .  Update()  .
        // ---------------------------------------------------------------------
        //   Method.......:  Update()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void Update()
        {
            if (_isStarted)
            {
                Rotate();
            }

        }   // Update()
        #endregion


    }   // class GameMaager

}  // namespace SAssets.Scenes.SpiroGraph4.Scripts.Managers