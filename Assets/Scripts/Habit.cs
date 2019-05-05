using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using Unity.UIWidgets.material;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace HabitApp
{
    public class Habit : StatelessWidget
    {
        private HabitData mHabit;

        private Action mOnClick;

        private Action mOnEdit;

        private bool mSelected = false;

        public Habit(HabitData habit, bool selected, Action onClick, Action onEdit)
        {
            mHabit = habit;
            mOnClick = onClick;
            mOnEdit = onEdit;
            mSelected = selected;
        }

        public override Widget build(BuildContext context)
        {
            return new Container(
                    color: mSelected ? Colors.white : Colors.grey,
                    child:new Column(
                        children:new List<Widget>()
                        {
                            new Row(
                                children: new List<Widget>()
                                {
                                    new GestureDetector(
                                        child: new Text(mHabit.Title),
                                        onTap: () => { mOnClick(); }),
                                    new RaisedButton(child: new Text("..."),
                                        onPressed: () => { mOnEdit(); })
                                }
                            ),
                            new Text(mHabit.CreateAt.ToString("MM/dd/yyyy"))
                        }
                        ) 
                    

                );
        }
    }
}