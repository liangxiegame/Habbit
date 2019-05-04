using System.Collections.Generic;
using System.Linq;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
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
        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState,List<HabitData>>(
                converter:state=>state.Habits,
                builder:(buildContext, model, dispatcher) =>
                {
                    return new Column(
                        children: new List<Widget>()
                        {
                            new Container(
                                color: Colors.grey,
                                height: 200,
                                child: new ListView(
                                    children: model.Select(habit => new Habit(habit, () =>
                                        {
//                                    this.setState(() => { mHabits.Remove(habit); });
                                            Navigator.push(context,
                                                new MaterialPageRoute(buildContext1 =>
                                                {
                                                    return new HabitEditor(habit);
                                                }));

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
                                    () =>
                                    {
                                        Navigator.push(context, new MaterialPageRoute(
                                            builder: (context1 => { return new HabitEditor(); })
                                        ));

                                    }
                                )
                            )
                        }
                    );
                }
                );
        }
    }
}