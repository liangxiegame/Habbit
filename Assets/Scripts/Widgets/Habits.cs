using com.unity.uiwidgets.Runtime.rendering;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace HabitApp
{
    public class Habits : StatelessWidget
    {
        public const float Padding    = 16.0f;
        public const float ItemHeight = 50.0f;
        public static float VerticalPadding = 16.0f;

        private float mHabitsHeight;

        public Habits(float habitsHeight)
        {
            mHabitsHeight = habitsHeight;
        }


        public override Widget build(BuildContext context)
        {
            var screenWidth = MediaQuery.of(context).size.width;
            var itemWidth = (screenWidth - 3 * Padding) / 2;
            var aspect = (itemWidth + Padding) / (ItemHeight + Padding);

            return new Container(
                padding: EdgeInsets.symmetric(horizontal: Habits.Padding / 2, vertical: Habits.Padding / 2),
                color:new Color(0xFF111111),
                height: mHabitsHeight,
                child: new StoreConnector<AppState, AppState>(
                    converter: state => state,
                    builder: (buildContext, model, dispatcher) =>
                    {
                        if (model.SelectedHabit != null)
                        {

                            return GridView.builder(
                                gridDelegate: new SliverGridDelegateWithFixedCrossAxisCount(
                                    crossAxisCount: 2,
                                    childAspectRatio: aspect
                                ),
                                itemCount: model.Habits.Count + 1,
                                itemBuilder: (context2, index) =>
                                {

                                    if (index == model.Habits.Count)
                                    {
                                        return new AddHabit(itemWidth);
                                    }
                                    else
                                    {
                                        var habit = model.Habits[index];
                                        
                                        return new Habit(habit,
                                            () => { dispatcher.dispatch(new SelectHabitAction(habit)); }, () =>
                                            {
                                                Navigator.push(context,
                                                    new MaterialPageRoute(buildContext1 =>
                                                    {
                                                        return new HabitEditor(habit);
                                                    }));
                                            }, itemWidth);
                                    }
                                }

                            );
                        }
                        else
                        {
                            return new Container(
                                padding:EdgeInsets.only(right:Padding,bottom:VerticalPadding),
                                alignment: Alignment.center,
                                child: new AddHabit(itemWidth)
                            );
                        }
                    }
                )
            );
        }
    }
}