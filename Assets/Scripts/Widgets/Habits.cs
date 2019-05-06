using com.unity.uiwidgets.Runtime.rendering;
using Unity.UIWidgets.material;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;

namespace HabitApp
{
    public class Habits : StatelessWidget
    {
        public const float Padding = 16.0f;
        public const float ItemHeight = 50.0f;
        

        public Habits()
        {
        }


        public override Widget build(BuildContext context)
        {
            var screenWidth = MediaQuery.of(context).size.width;
            var itemWidth = (screenWidth - 3 * Padding) / 2;
            var aspect = itemWidth / ItemHeight;
            
            
            return new StoreConnector<AppState, AppState>(
                converter: state => state,
                builder: (buildContext, model, dispatcher) =>
                {
                    return GridView.builder(
                        gridDelegate: new SliverGridDelegateWithFixedCrossAxisCount(
                            crossAxisCount: 2,
                            childAspectRatio: aspect
                        ),
                        itemCount: model.Habits.Count,
                        itemBuilder: (context2, index) =>
                        {
                            var habit = model.Habits[index];
                            var selected = model.SelectedHabitIndex == index;

                            return new Habit(habit, selected,
                                () => { dispatcher.dispatch(new SelectHabitAction(index)); }, () =>
                                {
                                    Navigator.push(context,
                                        new MaterialPageRoute(buildContext1 => { return new HabitEditor(habit); }));
                                },itemWidth);
                        }

                    );
                }
            );
        }
    }
}