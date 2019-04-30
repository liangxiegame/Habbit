using System.Linq;
using QFramework.UIWidgets;
using Unity.UIWidgets.material;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;

namespace HabitApp
{
    public class Title : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState, AppState>(
                converter: state => state,
                builder: (buildContext, model, dispatcher) =>
                {
                    var title = "Habbit";

                    if (model.Habits.Any())
                    {
                        var habit = model.Habits.FirstOrDefault(_habit => _habit.Selected);

                        if (habit != null)
                        {
                            title = habit.Title;
                        }
                    }

                    return QF.Text
                        .Data(title)
                        .FontColor(Colors.white)
                        .EndText();
                });
        }
    }
}