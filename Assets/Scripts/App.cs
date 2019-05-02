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

    public class Body : StatefulWidget
    {
        public override State createState()
        {
            return new BodyState();
        }
    }

    class BodyState : State<Body>
    {
        List<string> mHabits = new List<string>();

        public override Widget build(BuildContext context)
        {
            return
                new Column(
                    children: new List<Widget>()
                    {
                        new Container(
                            color:Colors.grey,
                            height:200,
                            child: new ListView(
                                children: mHabits.Select(habit => new Text(habit) as Widget).ToList()
                            )
                        ),
                        new Container(
                            alignment: Alignment.center,
                            child: new FlatButton(
                                color: Colors.blue,
                                child: new Text("创建习惯", style: new TextStyle(color: Colors.white)),
                                onPressed:
                                () =>
                                {
                                    this.setState(() => { mHabits.Add("新的习惯"); });
                                }
                            )
                        )
                    }

                );
        }
    }
}