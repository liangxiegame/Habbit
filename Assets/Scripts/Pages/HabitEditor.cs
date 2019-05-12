using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using DialogUtils = Unity.UIWidgets.material.DialogUtils;

namespace HabitApp
{
    public class HabitEditor : StatefulWidget
    {
        public HabitData Habit;

        public bool CreateMode => Habit == null;

        public HabitEditor(HabitData habit = null)
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
            return new StoreConnector<AppState, AppState>(
                converter: state => state,
                builder: (buildContext, model, dispatcher) =>
                {
                    return new Scaffold(
                        appBar: new AppBar(
                            centerTitle: true,
                            elevation: 0,
                            leading: new IconButton(
                                icon: new Icon(Icons.arrow_back),
                                onPressed: () => { Navigator.pop(context); }
                            ),
                            title: new Text(widget.CreateMode ? "New Habit" : "Edit Habit"),
                            bottom: new PreferredSize(
                                preferredSize: Size.fromHeight(1),
                                child: new Container(
                                    height: 0.5f,
                                    color: Colors.white30
                                )
                            ),
                            actions: new List<Widget>()
                            {
                                new FlatButton(
                                    child: new Text(widget.CreateMode ? "" : "Delete",
                                        style: new TextStyle(color: Colors.red)),
                                    onPressed: () =>
                                    {
                                        DialogUtils.showDialog(
                                            context: context,
                                            builder: (context1 =>
                                            {
                                                return new Theme(
                                                    data: new ThemeData(dialogBackgroundColor: new Color(0xFF333333)),
                                                    child: new AlertDialog(
                                                        contentPadding: EdgeInsets.fromLTRB(24, 24, 24, 0),
                                                        content: new Text("Really want to delete this habit?",
                                                            style: new TextStyle(color: Colors.white70)
                                                        ),
                                                        actions: new List<Widget>()
                                                        {
                                                            new FlatButton(
                                                                child: new Text("Yes",
                                                                    style: new TextStyle(color: Colors.red)
                                                                ),
                                                                onPressed: () =>
                                                                {
                                                                    dispatcher.dispatch(
                                                                        new DeleteHabitAction(widget.Habit));
                                                                    Navigator.pop(context1);
                                                                    Navigator.pop(context);

                                                                }),
                                                            new FlatButton(
                                                                child: new Text("No",
                                                                    style: new TextStyle(color: Colors.white70)
                                                                ),
                                                                onPressed: () => { Navigator.pop(context1); }),
                                                        }
                                                    )
                                                );
                                            }));
                                    }
                                )
                            },
                            backgroundColor: new Color(0xFF212121)
                        ),
                        body: new Container(
                            margin: EdgeInsets.all(15),
                            decoration: new BoxDecoration(),
                            child: new TextField(
                                style: new TextStyle(fontSize: 18, color: Colors.white70),
                                decoration: new InputDecoration(
                                    fillColor: new Color(0xFF424242),
                                    filled: true,
                                    hasFloatingPlaceholder: false,
                                    hintText: "New Habit",
                                    contentPadding: EdgeInsets.all(6),
                                    focusedBorder: new OutlineInputBorder(
                                        borderRadius: BorderRadius.all(Radius.circular(6)),
                                        borderSide: new BorderSide(width: 0, color: new Color(0xFF424242))
                                    )
                                ),
                                controller: new TextEditingController(widget.CreateMode
                                    ? string.Empty
                                    : widget.Habit.Title),
                                onSubmitted: (value =>
                                {
                                    if (widget.CreateMode)
                                    {
                                        dispatcher.dispatch(new AddHabitAction(new HabitData(value)));
                                    }
                                    else
                                    {

                                        if (value != widget.Habit.Title)
                                        {
                                            dispatcher.dispatch(new UpdateHabitAction(widget.Habit, value));
                                        }
                                    }

                                    Navigator.pop(context);
                                }))
                        )
                    );
                }
            );
        }
    }
}