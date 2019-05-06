using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Unity.UIWidgets;
using Unity.UIWidgets.engine;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace HabitApp
{
    public class App : UIWidgetsPanel
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            FontManager.instance.addFont(Resources.Load<Font>("MaterialIcons-Regular"), "Material Icons");
        }

        protected override Widget createWidget()
        {
            var jsonContent = PlayerPrefs.GetString("SAVE_MIDDLEWARE",string.Empty);

            AppState initialAppState = null;
            
            if (string.IsNullOrEmpty(jsonContent))
            {
                initialAppState = new AppState()
                {
//                    Habits = new List<HabitData>()
//                    {
//                        new HabitData() { Title = "创建习惯",CreateAt = DateTime.Now - TimeSpan.FromDays(1)},
//                        new HabitData() { Title = "早起",CreateAt = DateTime.Now - TimeSpan.FromDays(7)},
//                        new HabitData() { Title = "早睡",CreateAt = DateTime.Now - TimeSpan.FromDays(30)},
//                        new HabitData() { Title = "运动",CreateAt = DateTime.Now}
//                    }
                };
            }
            else
            {
                initialAppState = JsonConvert.DeserializeObject<AppState>(jsonContent);
            }
            
            var store = new Store<AppState>(AppReducer.Reduce, 
                initialState: initialAppState,
                SaveMiddleware.create<AppState>());

            return new StoreProvider<AppState>(
                child: new MaterialApp(
                    home: new Scaffold(
                        appBar: new AppBar(
                            title: new Title()
                        ),
                        body: new Body()
                    )
                ),
                store: store
            );
        }
    }
}