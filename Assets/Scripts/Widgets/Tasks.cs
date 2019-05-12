using System;
using System.Linq;
using com.unity.uiwidgets.Runtime.rendering;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace HabitApp
{
    public class Tasks :StatelessWidget
    {

        private const float PADDING = 16.0f;

        private const int MONTH_COUNT = 25;

        private const int SEASON_COUNT = 81;
        

        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState, AppState>(
                converter: state => state,
                builder: (buildContext, model, dispatcher) =>
                {
                    var tasks = model.Tasks;
                    
                    if (tasks.isEmpty())
                    {
                        return new Container();
                    }
                    else
                    {

                        int columCount = 0;
                        
                        if (tasks.Count == MONTH_COUNT)
                        {
                            columCount = (int)Math.Sqrt(MONTH_COUNT);
                        } else if (tasks.Count == SEASON_COUNT)
                        {
                            columCount = (int) Math.Sqrt(SEASON_COUNT);
                        }
                        

                        var screenWidth = MediaQuery.of(context).size.width;
                        var height = screenWidth;
                        var itemWidth = ((screenWidth - PADDING * 2) - (columCount - 1) * PADDING) /
                                        columCount;
                        
                        

                        
                        return new Container(
                            height: height,
                            width:screenWidth,
                            padding:EdgeInsets.only(
                                left:PADDING / 2,
                                top:PADDING / 2,
                                right:PADDING / 2,
                                bottom:0
                                ),
                            decoration: new BoxDecoration(
                                color: Theme.of(context).backgroundColor
                            ),
                            child: GridView.builder(
                                gridDelegate: new SliverGridDelegateWithFixedCrossAxisCount(
                                    crossAxisCount: columCount
                                ),
                                itemBuilder: (context3, index) =>
                                {
                                    var task = tasks[index];

                                    return new Task(task, () =>
                                    {
                                        Debug.Log("Task Clicked");

                                        dispatcher.dispatch(new ChangeTaskStatusAction(task));
                                    }, itemWidth);
                                },
                                itemCount: 25));
                    }
                }
            );
        }
    }
}