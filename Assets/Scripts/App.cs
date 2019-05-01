using LearnUIWidgets;
using Unity.UIWidgets;
using Unity.UIWidgets.engine;
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
            var store = new Store<AppState>(
                reducer: AppReducer.Reduce, 
                initialState: AppState.Load(),
                ReduxPersistMiddleware.create<AppState>());

            return new StoreProvider<AppState>(store: store,
                child: new MaterialApp(
                    title: "Habbit",
                    theme: new ThemeData(
                        backgroundColor: new Color(0xFF212121),
                        scaffoldBackgroundColor: new Color(0xFF212121),
                        primaryTextTheme: new TextTheme(title: new TextStyle(color: Colors.white))
                    ),
                    home: new Scaffold(
                        backgroundColor: new Color(0xFF111111),
                        appBar: new AppBar(
                            centerTitle: true,
                            brightness: Brightness.dark,
                            title: new Title(),
                            elevation: 0,
                            backgroundColor: new Color(0xFF212121)
                        ),
                        body: new Body(),
                        resizeToAvoidBottomPadding: false
                    )
                )
            );
        }
    }
}