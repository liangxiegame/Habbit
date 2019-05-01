using System;
using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace HabitApp
{
    public class HabitView : StatelessWidget
    {
        private readonly float  mItemWidth;
        private readonly Habit  mHabit;
        private readonly Action mOnSelectMore;
        private readonly Action mOnSelect;
        private readonly bool   mSelected;

        public HabitView(float itemWidth, Habit habit, Action onSelectMore, Action onSelect, bool selected = false)
        {
            mItemWidth = itemWidth;
            mHabit = habit;
            mOnSelectMore = onSelectMore;
            mOnSelect = onSelect;
            mSelected = selected;
        }

        public override Widget build(BuildContext context)
        {
            return new GestureDetector(
                onTap: () => { mOnSelect(); },
                child: new Container(
                    width: mItemWidth,
                    margin: EdgeInsets.symmetric(
                        horizontal: Habits.padding / 2, vertical: Habits.verticalPadding / 2),
                    decoration: new BoxDecoration(
                        color: this.mSelected ? Colors.blue : Colors.white10,
                        borderRadius: BorderRadius.all(Radius.circular(4))
                    ),
                    padding: EdgeInsets.only(left: 5, right: 5, top: 3.0f, bottom: 2.0f),
                    child: new Column(
                        mainAxisAlignment: MainAxisAlignment.spaceBetween,
                        children: new List<Widget>()
                        {
                            new Container(
                                padding: EdgeInsets.only(right: 4),
                                child: new Align(
                                    alignment: Alignment.topLeft,
                                    child: new Text(
                                        mHabit.Title,
                                        textAlign: TextAlign.left,
                                        overflow: TextOverflow.ellipsis,
                                        maxLines: 1,
                                        style: new TextStyle(fontSize: 16, color: Colors.white)
                                    )
                                )
                            ),
                            new Container(
                                height: 19,
                                child: new Row(
                                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                                    children: new List<Widget>()
                                    {
                                        new Text(
                                            mHabit.ToString(),
                                            style: new TextStyle(fontSize: 12, color: Colors.white70)
                                        ),
                                        new GestureDetector(
                                            onTap: () => { mOnSelectMore(); },
                                            child: new Icon(
                                                Icons.more_horiz,
                                                size: 20,
                                                color: Colors.white70
                                            )
                                        )
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