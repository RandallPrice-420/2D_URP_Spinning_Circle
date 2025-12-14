using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scenes.SpiroGraph3.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        // ---------------------------------------------------------------------
        // Serialized Fields:
        // ------------------
        //   _buttonQuit
        //   _buttonStart
        //   _buttonStop
        //   _sliderRotationSpeed
        //   _textRotationSpeedValue
        // ---------------------------------------------------------------------

        #region .  Serialized Fields  .

        [SerializeField] private Button   _buttonQuit;
        [SerializeField] private Button   _buttonStart;
        [SerializeField] private Button   _buttonStop;
        [SerializeField] private Slider   _sliderRotationSpeed;
        [SerializeField] private TMP_Text _textRotationSpeedValue;

        #endregion



        // ---------------------------------------------------------------------
        // Public Methods:
        // ---------------
        //   OnButtonQuitClicked()  --  EMPTY
        //   OnButtonStartClicked()
        //   OnBttonStopClicked()
        //   OnSliderValueChanged()
        // ---------------------------------------------------------------------

        #region .  OnButtonQuitClicked()  --  EMPTY  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnButtonQuitClicked()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void OnButtonQuitClicked()
        {
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
            _buttonStart.interactable = false;
            _buttonStop .interactable = true;

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
            _buttonStart.interactable = true;
            _buttonStop .interactable = false;

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
            //UpdateRotationSpeedTextValue(_sliderRotationSpeed.value);
            _textRotationSpeedValue.text = _sliderRotationSpeed.value.ToString();

        }   // OnSliderValueChanged()
        #endregion



        // ---------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   UpdateRotationSpeedTextValue()
        // ---------------------------------------------------------------------

        #region .  UpdateRotationSpeedTextValue()  .
        // ---------------------------------------------------------------------
        //   Method.......:  UpdateRotationSpeedTextValue()
        //   Description..:  
        //   Parameters...:  int
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void UpdateRotationSpeedTextValue(System.Single value)
        {
            _textRotationSpeedValue.text = value.ToString();

        }   // UpdateRotationSpeedTextValue()
        #endregion


    }   // class UIManager

}   // namespace Assets.Scenes.SpiroGraph3.Scripts.Managers
