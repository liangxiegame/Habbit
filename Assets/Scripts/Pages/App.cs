using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Unity.UIWidgets;
using Unity.UIWidgets.engine;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.service;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using Color = Unity.UIWidgets.ui.Color;

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
            var jsonContent = PlayerPrefs.GetString("HABBIT",string.Empty);
            
            AppState initialAppState = null;

            if (string.IsNullOrEmpty(jsonContent))
            {
                initialAppState = new AppState()
                {
                    Habits = new List<HabitData>()
                    {
//                        new HabitData() { Title = "创建习惯",CreateAt = DateTime.Now - TimeSpan.FromDays(1)},
//                        new HabitData() { Title = "早起",CreateAt = DateTime.Now - TimeSpan.FromDays(7)},
//                        new HabitData() { Title = "早睡",CreateAt = DateTime.Now - TimeSpan.FromDays(30)},
//                        new HabitData() { Title = "运动",CreateAt = DateTime.Now}
                    }
                };
            }
            else
            {
                initialAppState = JsonConvert.DeserializeObject<AppState>(jsonContent);
            }

            if (initialAppState.SelectedHabit != null)
            {
                var previousSelectedHabit =
                    initialAppState.Habits.Find(habit => habit.Id == initialAppState.SelectedHabit.Id);
                if (previousSelectedHabit != null)
                {
                    previousSelectedHabit.Selected = false;
                }

                initialAppState.SelectedHabit = null;
            }

            if (initialAppState.Habits.Count > 0)
            {
                var selectedHabit = initialAppState.Habits.first();

                initialAppState.Tasks.Clear();
                initialAppState.Tasks.AddRange(AppReducer.FormatTasks(selectedHabit));

                initialAppState.SelectedHabit = selectedHabit;

                selectedHabit.Selected = true;
            }

            var store = new Store<AppState>(AppReducer.Reduce, 
                initialState: initialAppState,
                ReduxLogging.create<AppState>(),
                SaveMiddleware.create<AppState>()
                );

            return new StoreProvider<AppState>(
                child: new MaterialApp(
                    title:"Habbit",
                    theme:new ThemeData(
                        backgroundColor:new Color(0xFF212121),
                        scaffoldBackgroundColor:new Color(0xFF212121),
                        primaryTextTheme:new TextTheme(title:new TextStyle(color:Colors.white))
                        ),
                    home: new Scaffold(
                        backgroundColor:new Color(0xFF111111),
                        appBar: new AppBar(
                            centerTitle:true,
                            brightness:Brightness.dark,
                            title: new Title(),
                            elevation:0,
                            backgroundColor:new Color(0xFF212121)
                        ),
                        body: new Body()
                    )
                ),
                store: store
            );
        }
    }
}