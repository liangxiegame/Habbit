using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using Color = Unity.UIWidgets.ui.Color;

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
            return new Container(
                decoration:new BoxDecoration(
                    color:new Color(0xFF111111)),
                child:new StoreConnector<AppState, AppState>(
                converter: state => state,
                builder: (buildContext, model, dispatcher) =>
                {
                    return new SafeArea(
                        child: new LayoutBuilder(
                            builder: (context1, constraints) =>
                            {
                                var totalHeight = constraints.maxHeight;
                                var tasksHeight = MediaQuery.of(context).size.width;
                                var habitsHeight = totalHeight - tasksHeight;

                                var itemWidth = tasksHeight / 5;

                                var habitsCount = model.Habits.Count;

                                if (habitsCount == 0)
                                {
                                    return new CustomScrollView(
                                        physics:new NeverScrollableScrollPhysics(),
                                        slivers:new List<Widget>()
                                        {
                                            new SliverToBoxAdapter(child:
                                                new Container(
                                                    decoration:new BoxDecoration(
                                                        color:Theme.of(context).backgroundColor
                                                        ),
                                                    height:tasksHeight,
                                                    alignment:Alignment.center,
                                                    child:new Text(
                                                        data:"WE BECOME \nWHAT WE \nREPEATEDLY DO.",
                                                        textAlign:TextAlign.center,
                                                        style:new TextStyle(
                                                            fontSize:24,
                                                            color:Colors.white,
                                                            height:1.2f
                                                        )
                                                        )
                                                    )
                                                ),
                                            new SliverToBoxAdapter(
                                                child: new Habits(habitsHeight)),
                                        }
                                    );
                                }
                                else
                                {
                                    return new CustomScrollView(
                                        physics:new NeverScrollableScrollPhysics(),
                                        slivers:new List<Widget>()
                                        {
                                            new SliverToBoxAdapter(
                                                child:new Tasks()
                                                ),
                                            new SliverToBoxAdapter(
                                                child: new Habits(habitsHeight)
                                            )
                                        }
                                    );
                                }
                            }
                        )
                    );
                }
            ));
        }
    }
}