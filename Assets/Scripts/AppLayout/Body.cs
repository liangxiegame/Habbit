using System.Collections.Generic;
using QFramework;
using QFramework.UIWidgets;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace HabitApp
{
    public class Body : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return QF.Container
                .Decoration(new BoxDecoration(color: new Color(0xFF111111)))
                .Child(new StoreConnector<AppState, AppState>(
                    converter: state => state,
                    builder: (buildContext, model, dispatcher) =>
                    {
                        var habitCount = model.Habits.Count;
                        return new SafeArea(
                            child: new LayoutBuilder(
                                builder: (context1, constraints) =>
                                {
                                    var totalHeight = constraints.maxHeight;
                                    var tasksHeight = MediaQuery.of(context1).size.width;
                                    var habitsHeight = (totalHeight - tasksHeight).ToInt();

                                    if (habitCount == 0)
                                    {
                                        // show welcome
                                        return new CustomScrollView(
                                            physics: new NeverScrollableScrollPhysics(),
                                            slivers: new List<Widget>()
                                            {
                                                new SliverToBoxAdapter(
                                                    child: new Container(
                                                        decoration: new BoxDecoration(
                                                            color: Theme.of(context1).backgroundColor),
                                                        height: totalHeight - habitsHeight,
                                                        child: new Center(
                                                            child: QF.Text
                                                                .Data("WE BECOME \nWHAT WE \nREPEATEDLY DO.")
                                                                .TextAlignCenter()
                                                                .FontSize(24)
                                                                .FontColor(Colors.white)
                                                                .Height(1.2f)
                                                                .EndText()
                                                        )
                                                    )
                                                ),
                                                new SliverToBoxAdapter(
                                                    child: new Container(
                                                        child: new Habits(habitsHeight)
                                                    )
                                                )
                                            }
                                        );
                                    }
                                    else
                                    {
                                        return new Text("待处理");
                                    }
                                })
                        );
                    }))
                .EndContainer();
        }
    }
}