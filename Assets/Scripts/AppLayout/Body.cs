using System;
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
            return new StoreConnector<AppState, AppState>(
                converter: state => state,
                builder: (buildContext, model, dispatcher) =>
                {

                    return new SafeArea(
                        child: new LayoutBuilder(
                            builder: ((context1, constraints) =>
                            {
                                var totalHeight = constraints.maxHeight;
                                var tasksHeight = MediaQuery.of(context).size.width;
                                var habitsHeight = totalHeight - tasksHeight;

                                var itemWidth = tasksHeight / 5;
                                
                                return new Column(
                                    children: new List<Widget>()
                                    {
                                        new Container(
                                            height: tasksHeight,
                                            color: Colors.black,
                                            // TODO: 封装成 Tasks
                                            child: new Tasks(itemWidth)
                                        ),
                                        new Container(
                                            padding:EdgeInsets.symmetric(horizontal:Habits.Padding / 2,vertical:Habits.Padding / 2),
                                            color: Colors.black,
                                            height: habitsHeight,
                                            child: new Habits())
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