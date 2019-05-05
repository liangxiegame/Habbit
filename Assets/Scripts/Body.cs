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

                                var selectedHabit = model.Habits[model.SelectedHabitIndex];
                                var tasks = selectedHabit.Tasks;


                                var totalDays = (DateTime.Now - selectedHabit.CreateAt).TotalDays;
                                var dayth = (int) totalDays + 1;

                                var itemWidth = tasksHeight / 5;
                                
                                return new Column(
                                    children: new List<Widget>()
                                    {
                                        new Container(
                                            height: tasksHeight,
                                            color: Colors.black,
                                            child: GridView.builder(
                                                gridDelegate: new SliverGridDelegateWithFixedCrossAxisCount(
                                                    crossAxisCount: 5
                                                ),
                                                itemBuilder: ((context3, index) =>
                                                {
                                                    var task = tasks[index];
                                                    var isToday = dayth == task.Seq;
                                                    
                                                    return new Task(task, () =>
                                                    {
                                                        Debug.Log("Task Clicked");

                                                        dispatcher.dispatch(new ChangeTaskStatusAction(task));

                                                    },itemWidth,isToday); 
                               
                                                }),
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
                                                itemCount: model.Habits.Count,
                                                itemBuilder: (context2, index) =>
                                                {
                                                    var habit = model.Habits[index];
                                                    var selected = model.SelectedHabitIndex == index;
                                                    
                                                    return new Habit(habit,selected,  () =>
                                                    {
                                                       dispatcher.dispatch(new SelectHabitAction(index));
                                                    }, () =>
                                                    {
                                                        Navigator.push(context,
                                                            new MaterialPageRoute(buildContext1 =>
                                                            {
                                                                return new HabitEditor(habit);
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