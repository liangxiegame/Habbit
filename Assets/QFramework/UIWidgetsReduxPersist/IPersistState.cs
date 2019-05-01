using Newtonsoft.Json;
using UnityEngine;

namespace QFramework.UIWidgets.ReduxPersist
{
    public abstract class AbstractPersistState<T> where T: AbstractPersistState<T>,new()
    {
        private const string KEY = "REDUX_PERISIST";

        public static T Load()
        {
            var jsonContent = PlayerPrefs.GetString(KEY);

            if (string.IsNullOrWhiteSpace(jsonContent))
            {
                return new T();
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(jsonContent);
            }
            
        }

        public void Save()
        {
            PlayerPrefs.SetString(KEY,JsonConvert.SerializeObject(this));    
        }
    }
}