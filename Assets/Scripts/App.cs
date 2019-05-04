using System.Collections.Generic;
using System.Linq;
using Unity.UIWidgets.engine;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace HabitApp
{
    public class App : UIWidgetsPanel
    {
        protected override Widget createWidget()
        {
            return new MaterialApp(
                home: new Scaffold(
                    appBar: new AppBar(
                        title: new Text("Title")
                    ),
                    body: new Body()
                )
            );
        }
    }
}