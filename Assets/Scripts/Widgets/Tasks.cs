using System;
using com.unity.uiwidgets.Runtime.rendering;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace HabitApp
{
    public class Tasks :StatelessWidget
    {
        private float mItemWidth;

        public Tasks(float itemWidth)
        {
            mItemWidth = itemWidth;
        }

        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState, AppState>(
                converter: state => state,
                builder: (buildContext, model, dispatcher) =>
                {

                    if (model.SelectedHabit != null)
                    {

                        var tasks = model.SelectedHabit.Tasks;


                        var totalDays = (DateTime.Now - model.SelectedHabit.CreateAt).TotalDays;
                        var dayth = (int) totalDays + 1;


                        return GridView.builder(
                            gridDelegate: new SliverGridDelegateWithFixedCrossAxisCount(
                                crossAxisCount: 5
                            ),
                            itemBuilder: (context3, index) =>
                            {
                                var task = tasks[index];
                                var isToday = dayth == task.Seq;
                                var editable = dayth == task.Seq || dayth == task.Seq + 1;

                                return new Task(task, () =>
                                {
                                    Debug.Log("Task Clicked");

                                    dispatcher.dispatch(new ChangeTaskStatusAction(task));

                                }, mItemWidth, isToday, editable);
                            },
                            itemCount: 25);
                    }
                    else
                    {
                        return new Container();
                    }
                }
            );
        }
    }
}