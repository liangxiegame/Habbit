using System;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.widgets;

namespace HabitApp
{
    public class Task : StatefulWidget
    {
        public TaskData TaskData;

        public Action OnClick;

        public float Width;

        public Task(TaskData taskData, Action onClick, float width)
        {
            TaskData = taskData;
            OnClick = onClick;
            Width = width;
        }

        public override State createState()
        {
            return new TaskState();
        }
    }

    class TaskState : State<Task>
    {
        public override Widget build(BuildContext context)
        {
            var status = widget.TaskData.Status;

            var text = string.Empty;
            var textColor = Colors.white;
            var circleColor = Colors.transparent;
            IconData icon = null;

            bool showIcon = false;

            if (status == TaskStatus.UnSetted)
            {
                text = widget.TaskData.Seq.ToString();

                if (widget.TaskData.IsToday.Value)
                {
                    textColor = Colors.black;
                    circleColor = Colors.white;
                }

            }
            else if (status == TaskStatus.Completed)
            {
                showIcon = true;
                icon = Icons.check;
                circleColor = Colors.green;

            }
            else if (status == TaskStatus.Failed)
            {
                icon = Icons.close;
                circleColor = Colors.red;
                showIcon = true;
            }
            else if (status == TaskStatus.Skipped)
            {
                text = "//";
                circleColor = Colors.green;
            }

            if (widget.TaskData.IsFuture.Value)
            {
                textColor = Colors.white70;
            }

            return new GestureDetector(
                onTapDown: details => { },
                onTapUp: details =>
                {
                    if (widget.TaskData.IsToday.Value || widget.TaskData.IsYesterday.Value)
                    {
                        widget.OnClick();
                    }
                },
                onTapCancel: () => { },
                child: new Container(
                    margin: EdgeInsets.all(16),
                    decoration: new BoxDecoration(
                        color: circleColor,
                        shape: BoxShape.circle
                    ),
                    alignment: Alignment.center,
                    child: !showIcon
                        ? new Text(text, style: new TextStyle(color: textColor,fontSize:widget.Width / 2.5f)) as Widget
                        : new Icon(icon,color:Colors.white,size:widget.Width / 2))
            );
        }
    }
}