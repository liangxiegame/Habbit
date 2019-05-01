using System;
using System.Collections.Generic;
using QFramework;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using Color = Unity.UIWidgets.ui.Color;
using DialogUtils = Unity.UIWidgets.material.DialogUtils;

namespace HabitApp
{
    public class HabitModifier : StatelessWidget
    {
        private          Habit                 mHabit;
        private readonly Action                mOnDelete;
        private          TextEditingController mTitleController = new TextEditingController();

        public HabitModifier(Habit habit, Action onDelete)
        {
            this.mHabit = habit;
            mOnDelete = onDelete;
            mTitleController.text = habit.Title;
        }

        public override Widget build(BuildContext context)
        {
            return new Scaffold(
                appBar: new AppBar(
                    centerTitle: true,
                    title: new Text(mHabit.Title.Length > 0 ? "Edit Habit" : "Add Habit"),
                    elevation: 0,
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
                            child: new Text(
                                mHabit.Title.Length > 0 ? "Delete" : "",
                                style: new TextStyle(color: Colors.red)
                            )
                            , onPressed: () =>
                            {
                                DialogUtils.showDialog(
                                    context: context,
                                    builder: (buildContext => new Theme(
                                        data: new ThemeData(dialogBackgroundColor: new Color(0xFF333333)),
                                        child: new AlertDialog(
                                            contentPadding: EdgeInsets.fromLTRB(24, 24, 24, 0),
                                            content: new Text(
                                                "Really want to delete this habit?",
                                                style: new TextStyle(color: Colors.white70)
                                            ),
                                            actions: new List<Widget>()
                                            {
                                                new FlatButton(
                                                    child: new Text(
                                                        "NO",
                                                        style: new TextStyle(color: Colors.white70)
                                                    )
                                                    , onPressed: () => { Navigator.of(context).pop(); }
                                                ),
                                                new FlatButton(
                                                    child: new Text("YES",
                                                        style: new TextStyle(color: Colors.red))
                                                    , onPressed: () =>
                                                    {
                                                        mOnDelete();
                                                        Navigator.pop(context);
                                                        Navigator.pop(context);
                                                    }

                                                )
                                            }
                                        )

                                    ))
                                );
                            }
                        )
                    },
                    backgroundColor: new Color(0xFF212121)
                ),
                body: new Container(
                    margin: EdgeInsets.all(15),
                    decoration: new BoxDecoration(),
                    child: new Column(
                        children: new List<Widget>()
                        {
                            new TextField(
                                autofocus: true,
                                style: new TextStyle(fontSize: 18, color: Colors.white70),
                                decoration: new InputDecoration(
                                    fillColor: new Color(0xFF424242),
                                    filled: true,
                                    hasFloatingPlaceholder: false,
                                    hintText: "New Habit",
                                    contentPadding: EdgeInsets.all(6),
                                    focusedBorder: new OutlineInputBorder(
                                        borderSide: new BorderSide(width: 0, color: new Color(0xFF424242)),
                                        borderRadius: BorderRadius.all(Radius.circular(6)))),
                                controller: mTitleController,
                                onSubmitted: (value =>
                                    {
                                        mHabit.Title = value;
                                        Navigator.pop(context, mHabit);
                                    }
                                )
                            )
                        }

                    )
                )
            );
        }
    }
}