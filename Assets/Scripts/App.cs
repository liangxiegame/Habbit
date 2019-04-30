using Unity.UIWidgets;
using Unity.UIWidgets.engine;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.service;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace HabitApp
{
    public class App : UIWidgetsPanel
    {
        protected override Widget createWidget()
        {
            var store = new Store<AppState>(reducer: AppReducer.Reduce, initialState: new AppState());

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