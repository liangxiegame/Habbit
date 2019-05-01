using System;
using QFramework;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.scheduler;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace HabitApp
{
    public class TaskItem : StatefulWidget
    {
        public float     ItemWidth { get; }
        public DailyTask Task      { get; }
        public Action    OnClick   { get; }


        public TaskItem(float itemWidth, DailyTask task, Action onClick)
        {
            ItemWidth = itemWidth;
            Task = task;
            OnClick = onClick;
        }

        public override State createState()
        {
            return new TaskItemState();
        }
    }

    class TaskItemState : State<TaskItem>, TickerProvider
    {
        private float widthRatio = 1.0f;

        public override Widget build(BuildContext context)
        {
            var task = widget.Task;
            var itemWidth = widget.ItemWidth;

            var circleColor = Colors.transparent;
            var text = task.Seq.ToString();
            var textColor = Colors.white;
            var borderColor = Colors.transparent;
            var completedIcon = new Icon(
                Icons.done,
                color: Colors.white,
                size: itemWidth / 2
            );
            var failedIcon = new Icon(
                Icons.close,
                color: Colors.white,
                size: itemWidth / 2
            );

            Icon icon = null;

            switch (task.Status)
            {
                case DailyTaskStatus.Completed:
                    circleColor = Colors.green;
                    icon = completedIcon;
                    break;
                case DailyTaskStatus.Failed:
                    circleColor = Colors.red;
                    icon = failedIcon;
                    break;
                case DailyTaskStatus.Skipped:
                    circleColor = Colors.green;
                    text = "//";
                    break;
            }

            if (task.IsSelected == true)
            {
                if (task.Status == null)
                {
                    textColor = Colors.white;
                }

                borderColor = Colors.white;
            }

            if (task.IsToday == true)
            {
                if (task.Status == null)
                {
                    circleColor = Colors.white;
                    textColor = Colors.black;
                }
            }

            if (task.IsFuture == true)
            {
                textColor = Colors.white54;
            }


            return new GestureDetector(
                onTapUp: (e) =>
                {
                    if (task.IsToday == true || task.IsYesterday == true)
                    {
                        widget.OnClick.InvokeGracefully();
                        this.setState(() => { widthRatio = 1.0f; });
                    }
                },
                onTapCancel: () =>
                {
                    if (task.IsToday == true || task.IsYesterday == true)
                    {
                        this.setState(() => { widthRatio = 1.0f; });
                    }
                },
                onTapDown: (e) =>
                {
                    if (task.IsToday == true || task.IsYesterday == true)
                    {
                        this.setState(() => { widthRatio = 1.5f; });
                    }
                },
                child: new Container(
                    margin: EdgeInsets.all(Tasks.padding / 2 * widthRatio),
                    decoration: new BoxDecoration(
                        color: circleColor,
                        shape: BoxShape.circle,
                        border: Border.all(
                            color: borderColor,
                            width: Mathf.Max((itemWidth / 25).ToInt(), 2.0f))
                    ),
                    child: new Center(
                        child: (Widget) icon ?? new Text(text,
                                   style: new TextStyle(color: textColor, fontSize: itemWidth / 2.5f))
                    )
                ));
        }

        public Ticker createTicker(TickerCallback onTick)
        {
            return new Ticker(onTick);
        }
    }
}