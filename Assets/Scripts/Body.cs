using System.Collections.Generic;
using System.Linq;
using com.unity.uiwidgets.Runtime.rendering;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace HabitApp
{
    public class Body : StatefulWidget
    {
        public override State createState()
        {
            return new BodyState();
        }
    }

    class BodyState : State<Body>
    {
        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState, List<HabitData>>(
                converter: state => state.Habits,
                builder: (buildContext, model, dispatcher) =>
                {

                    return new SafeArea(
                        child: new LayoutBuilder(
                            builder: ((context1, constraints) =>
                            {
                                var totalHeight = constraints.maxHeight;
                                var tasksHeight = MediaQuery.of(context).size.width;
                                var habitsHeight = totalHeight - tasksHeight;

                                return new Column(
                                    children: new List<Widget>()
                                    {
                                        new Container(
                                            height: tasksHeight,
                                            color: Colors.green,
                                            child: GridView.builder(
                                                gridDelegate: new SliverGridDelegateWithFixedCrossAxisCount(
                                                    crossAxisCount: 5
                                                ),
                                                itemBuilder: ((context3, index) => { return new Text("item"); }),
                                                itemCount: 25
                                            )
                                        ),
                                        new Container(
                                            color: Colors.grey,
                                            height: habitsHeight,
                                            child: GridView.builder(
                                                gridDelegate: new SliverGridDelegateWithFixedCrossAxisCount(
                                                    crossAxisCount: 2,
                                                    childAspectRatio: 1.5f
                                                ),
                                                itemCount: model.Count,
                                                itemBuilder: (context2, index) =>
                                                {
                                                    return new Habit(model[index], () =>
                                                    {
                    //                                    this.setState(() => { mHabits.Remove(habit); });
                                                        Navigator.push(context,
                                                            new MaterialPageRoute(buildContext1 =>
                                                            {
                                                                return new HabitEditor(model[index]);
                                                            }));

                                                    });
                                                }

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
                                                    Navigator.push(context, new MaterialPageRoute(
                                                        builder: (context2 => { return new HabitEditor(); })
                                                    ));
                                                }
                                            )
                                        )
                                    }
                                );
                            })
                        )
                    );
                }
            );
        }
    }
}