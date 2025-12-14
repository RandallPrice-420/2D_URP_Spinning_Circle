using UnityEngine;


namespace Assets.Scenes.SpiroGraph3.Scripts
{
    public class Utils : Singleton<Utils>
    {
        public Component[] GetAllObjectComponents(GameObject gameObject)
        {
            Component[] components = gameObject.GetComponents(typeof(Component));

            foreach (Component component in components)
            {
                Debug.Log(component.GetType().ToString());
            }

            return components;

        }   // GetAllObjectComponents()


    }   // class Utils

}   // Assets.Scenes.SpiroGraph3.Scripts
