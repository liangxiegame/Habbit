using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace HabitApp
{
    public class HabitEditor : StatefulWidget
    {
        public HabitData Habit;

        public HabitEditor(HabitData habit)
        {
            Habit = habit;
        }

        public override State createState()
        {
            return new HabitEditorState();
        }
    }

    class HabitEditorState : State<HabitEditor>
    {
        public override Widget build(BuildContext context)
        {
            return new MaterialApp(
                home:new StoreConnector<AppState,AppState>(
                    converter:state=>state,
                    builder:((buildContext, model, dispatcher) =>
                    {
                        return new Scaffold(
                            appBar: new AppBar(
                                leading:new IconButton(
                                    icon:new Icon(Icons.arrow_back),
                                    onPressed:() => { Navigator.pop(context); }
                                    ),
                                title: new Text("Habit Editor"),
                                actions: new List<Widget>()
                                {
                                    new FlatButton(
                                        child: new Text("Delete"),
                                        onPressed: () =>
                                        {
                                            dispatcher.dispatch(new DeleteHabitAction(widget.Habit));
                                            Navigator.pop(context);
                                        }
                                    )
                                }
                            ),
                            body:new TextField(
                                controller:new TextEditingController(widget.Habit.Title),
                                onSubmitted:(value =>
                                {
                                    if (value != widget.Habit.Title)
                                    {
                                        dispatcher.dispatch(new UpdateHabitAction(widget.Habit, value));
                                    }

                                    Navigator.pop(context);
                                }))
                        );
                    })
                    ) 
                
            );
        }
    }
}