using System;
using com.unity.uiwidgets.Runtime.rendering;
using QFramework;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace HabitApp
{
    public class Tasks : StatelessWidget
    {
        public const float padding           = 16.0f;
        public const int   threeTasksCount   = 3;
        public const int   weekTasksCount    = 9;
        public const int   monthTasksCount   = 25;
        public const int   quarterTasksCount = 81;

        private BuildContext mContext;

        public Tasks(BuildContext context)
        {
            mContext = context;
        }

        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState, AppState>(
                converter: state => state,
                builder: (buildContext, model, dispatcher) =>
                {
                    var tasks = model.Tasks;

                    if (tasks.IsNull())
                    {
                        return new Container();
                    }

                    var columnCount = threeTasksCount;

                    switch (tasks.Count)
                    {
                        case weekTasksCount:
                            columnCount = Mathf.Sqrt(weekTasksCount).ToInt();
                            break;
                        case monthTasksCount:
                            columnCount = Mathf.Sqrt(monthTasksCount).ToInt();
                            break;
                        case quarterTasksCount:
                            columnCount = Mathf.Sqrt(quarterTasksCount).ToInt();
                            break;
                    }

                    var screenWidth = MediaQuery.of(context).size.width;
                    var height = screenWidth;
                    var itemWidth =
                        ((height - padding * 2) - (columnCount - 1) * padding) /
                        columnCount;
                    var extraPaddingTop = 0.0f;
                    if (tasks.Count == threeTasksCount)
                    {
                        extraPaddingTop = (height - padding * 2 - itemWidth) / 2;
                    }

                    return new Container(
                        height: height,
                        width: screenWidth,
                        padding: EdgeInsets.only(
                            left: padding / 2,
                            top: padding / 2 + extraPaddingTop,
                            right: padding / 2,
                            bottom: 0),
                        decoration: new BoxDecoration(
                            color: Theme.of(context).backgroundColor
                        ),
                        child: GridView.builder(
                            physics: new NeverScrollableScrollPhysics(),
                            itemCount: tasks.Count,
                            itemBuilder: (context1, index) => new TaskItem(
                                itemWidth,
                                tasks[index],
                                () => { dispatcher.dispatch(new SelectTaskAction(tasks[index])); }
                            ),
                            gridDelegate: new SliverGridDelegateWithFixedCrossAxisCount(
                                crossAxisCount: columnCount
                            )
                        ));

                });
        }
    }
}