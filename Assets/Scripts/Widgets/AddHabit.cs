using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace HabitApp
{
    public class AddHabit : StatelessWidget
    {
        private float mHabitWidth;

        public AddHabit(float habitWidth)
        {
            mHabitWidth = habitWidth;
        }

        public override Widget build(BuildContext context)
        {

            return new GestureDetector(
                onTap: () =>
                {
                    Navigator.push(context, new MaterialPageRoute(
                        builder: (context3 => { return new HabitEditor(); })
                    ));
                },
                child: new Container(
                    width: mHabitWidth,
                    padding: EdgeInsets.only(left: 5, top: 3, bottom: 2, right: 5),
                    margin: EdgeInsets.symmetric(horizontal: Habits.Padding / 2, vertical: Habits.Padding / 2),
                    decoration: new BoxDecoration(
                        color: Colors.white10,
                        borderRadius: BorderRadius.all(Radius.circular(4))
                    ),
                    alignment: Alignment.center,
                    child: new Row(
                        mainAxisAlignment: MainAxisAlignment.center,
                        children: new List<Widget>()
                        {
                            new Text("创建习惯",
                                style: new TextStyle(color: Colors.white, fontSize: 16))
                        }
                    )
                )
            );
        }
    }
}