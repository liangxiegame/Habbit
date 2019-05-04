using System.Collections.Generic;
using System.Linq;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.widgets;

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
        List<string> mHabits = new List<string>();

        public override Widget build(BuildContext context)
        {
            return
                new Column(
                    children: new List<Widget>()
                    {
                        new Container(
                            color: Colors.grey,
                            height: 200,
                            child: new ListView(
                                children: mHabits.Select(habit => new Habit(habit, () =>
                                {
                                    this.setState(() => { mHabits.Remove(habit); });
                                    
                                }) as Widget)
                                    .ToList()
                            )
                        ),
                        new Container(
                            alignment: Alignment.center,
                            child: new FlatButton(
                                color: Colors.blue,
                                child: new Text("创建习惯", style: new TextStyle(color: Colors.white)),
                                onPressed:
                                () => { this.setState(() => { mHabits.Add("新的习惯"); }); }
                            )
                        )
                    }

                );
        }
    }
}