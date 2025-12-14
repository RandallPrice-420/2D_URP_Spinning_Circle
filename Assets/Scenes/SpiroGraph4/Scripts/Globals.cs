using UnityEngine;


namespace Assets.Scenes.SpiroGraph4.Scripts
{
    public class Globals : MonoBehaviour
    {
        // -------------------------------------------------------------------------
        // Public Enums:
        // -------------
        //   ButtonType
        // -------------------------------------------------------------------------

        #region . ButtonType  .
        [System.Serializable]
        public enum ButtonType
        {
            Start,
            Stop,
            Pause,
            Resume,
            Reset,
            Quit
        }
        #endregion


    }   // class Globals

}   // namespace Assets.Scenes.SpiroGraph4.Scripts
