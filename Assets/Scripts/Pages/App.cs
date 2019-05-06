using System.Collections.Generic;
using System.Linq;
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
            var store = new Store<AppState>(AppReducer.Reduce, initialState: new AppState());

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