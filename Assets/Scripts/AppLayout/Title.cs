using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;

namespace HabitApp
{
    public class Title : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState,AppState>(
                converter:state => state,
                builder: (buildContext, model, dispatcher) =>
                {
                    var title = model.SelectedHabit != null ? model.SelectedHabit.Title : "Habbit";
                    
                    return new Text(title);
                }
            );
        }
    }
}