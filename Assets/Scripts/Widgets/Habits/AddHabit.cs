using System;
using System.Collections.Generic;
using QFramework.UIWidgets;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using Color = UnityEngine.Color;

namespace HabitApp
{
    public class AddHabit : StatelessWidget
    {
        private float  mItemWidth;
        private Action mTapHandler;

        public AddHabit(float itemWidth, Action tapHandler)
        {
            mItemWidth = itemWidth;
            mTapHandler = tapHandler;
        }

        public override Widget build(BuildContext context)
        {   
            return new GestureDetector(
                onTap: () => { this.mTapHandler(); },
                child: new LayoutBuilder(
                    builder: (buildContext, constraints) => new Container(
                        constraints: BoxConstraints.tightFor(width: mItemWidth),
                        height: Habits.habitHeight,
                        margin: EdgeInsets.symmetric(
                            horizontal: Habits.padding / 2, vertical: Habits.verticalPadding / 2),
                        decoration: new BoxDecoration(
                            color: Colors.white10,
                            borderRadius: BorderRadius.all(Radius.circular(4))
                        ),
                        child: new Row(
                            mainAxisAlignment: MainAxisAlignment.center,
                            children: new List<Widget>()
                            {
                                new Text("Create Habit",
                                    style: new TextStyle(fontSize: 16, color: Colors.white)
                                )
                            }
                        )
                    )
                )
            );
        }
    }
}