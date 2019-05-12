using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace HabitApp
{
    public class Habit : StatelessWidget
    {
        private HabitData mHabit;

        private Action mOnClick;

        private Action mOnEdit;
        
        private float mHabitWidth;

        public Habit(HabitData habit, Action onClick, Action onEdit, float habitWidth)
        {
            mHabit = habit;
            mOnClick = onClick;
            mOnEdit = onEdit;
            mHabitWidth = habitWidth;
        }

        public override Widget build(BuildContext context)
        {
            return new GestureDetector(
                onTap: () => { mOnClick(); },
                child: new Container(
                    width: mHabitWidth,
                    padding: EdgeInsets.only(left: 5, top: 3, bottom: 2, right: 5),
                    margin: EdgeInsets.symmetric(horizontal: Habits.Padding / 2, vertical: Habits.Padding / 2),
                    decoration: new BoxDecoration(
                        color: mHabit.Selected ? Colors.blue : Colors.white10,
                        borderRadius: BorderRadius.all(Radius.circular(4))
                    ),
                    child: new Column(
                        mainAxisAlignment: MainAxisAlignment.spaceBetween,
                        children: new List<Widget>()
                        {
                            new Container(
                                padding: EdgeInsets.all(4),
                                child: new Align(
                                    alignment: Alignment.topLeft,
                                    child: new Text(mHabit.Title,
                                        textAlign: TextAlign.left,
                                        style: new TextStyle(color: Colors.white, fontSize: 16))
                                )
                            ),
                            new Container(
                                height: 19,
                                child: new Row(
                                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                                    children: new List<Widget>()
                                    {
                                        new Text(mHabit.CreateAt.ToString("MM/dd/yyyy"),
                                            style: new TextStyle(color: Colors.white70, fontSize: 12)),
                                        new GestureDetector(
                                            child: new Icon(
                                                Icons.more_horiz,
                                                size: 20,
                                                color: Colors.white70
                                            ),
                                            onTap: () => { mOnEdit(); }),
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