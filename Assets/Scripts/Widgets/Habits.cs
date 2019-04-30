using System;
using QFramework;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;
using UnityEngine;
using Color = Unity.UIWidgets.ui.Color;

namespace HabitApp
{
    public class Habits : StatelessWidget
    {
        public const  float padding         = 16.0f;
        public const  float habitHeight     = 50.0f;
        public static float verticalPadding = 16.0f;


        TextEditingController mTitleController = new TextEditingController();

        int mHeight;

        public Habits(int height)
        {
            this.mHeight = height;
        }

        public override Widget build(BuildContext context)
        {
            var screenWidth = MediaQuery.of(context).size.width;
            var habitWidth = (screenWidth - padding * 3) / 2;
            var habitCount = (mHeight - padding).ToInt() / (habitHeight + padding).ToInt() * 2;
            
            verticalPadding = (mHeight - habitHeight * (habitCount / 2)) / (habitCount / 2 + 1);

            var aspect = (habitWidth + padding) / (habitHeight + verticalPadding);

            return new Container(
                padding: EdgeInsets.symmetric(horizontal: padding / 2, vertical: verticalPadding / 2),
                decoration: new BoxDecoration(new Color(0xFF111111)),
                height: mHeight,
                child:new StoreConnector<AppState,AppState>(
                    converter:state=>state,
                    builder: (buildContext, model, dispatcher) =>
                    {
                        var habits = model.Habits;
                        if (habits == null || habits.Count == 0)
                        {                     
                            return new Container(
                                padding: EdgeInsets.only(right: padding, bottom: verticalPadding),
                                alignment: Alignment.center,
                                child: new AddHabit(habitWidth, () =>
                                {
                                    var habit = new Habit()
                                    {
                                        Title= string.Empty
                                    };
                                    
                                    Debug.Log("hello world");
                                })
                            );
                        }

                        return new Text("待处理");
                    })

            );

        }
    }
}