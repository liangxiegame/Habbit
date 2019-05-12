using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.UIWidgets;
using Unity.UIWidgets.Redux;
using UnityEngine;

namespace HabitApp
{
    public class SaveMiddleware
    {
        public static Middleware<State> create<State>()
        {
            return (store) => (next) => new DispatcherImpl((action) =>
            {
                var retValue = next.dispatch(action);
                var state= store.getState();
                
                var jsonContent = JsonConvert.SerializeObject(state);
                PlayerPrefs.SetString("HABBIT",jsonContent);
                
                return retValue;
            });
        }
    }
}