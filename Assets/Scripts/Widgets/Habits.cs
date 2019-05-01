using System;
using com.unity.uiwidgets.Runtime.rendering;
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
                child: new StoreConnector<AppState, AppState>(
                    converter: state => state,
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
                                        Title = string.Empty
                                    };

                                    var habitPage = new HabitModifier(habit,()=>{});
                                    Navigator.push(context,
                                            new MaterialPageRoute(builder: (context1 => habitPage)))
                                        .Then(result =>
                                        {
                                            var newHabit = result as Habit;

                                            if (newHabit.IsNotNull() && newHabit.Title.IsNotNullAndEmpty())
                                            {
                                                dispatcher.dispatch(new AddHabitAction(newHabit));
                                            }
                                        });
                                })
                            );
                        }

                        return GridView.builder(
                            physics: new NeverScrollableScrollPhysics(),
                            itemCount: Math.Min(habits.Count + 1, habitCount),
                            gridDelegate: new SliverGridDelegateWithFixedCrossAxisCount(
                                crossAxisCount: 2,
                                childAspectRatio: aspect
                            ),
                            itemBuilder: (context1, index) =>
                            {
                                if (index < habits.Count || habits.Count == habitCount)
                                {
                                    return new HabitView(habitWidth, habits[index], () =>
                                        {
                                            var habitPage = new HabitModifier(habits[index], () =>
                                                {
                                                    dispatcher.dispatch(new DeleteHabitAction(habits[index]));
                                                });
                                            Navigator.push(context,
                                                new MaterialPageRoute(builder: (context2)=> habitPage))
                                                .Then(result =>
                                                {
                                                    var updatedHabit = result as Habit;
                                                    if (updatedHabit != null && updatedHabit.Title != "")
                                                    {
                                                        dispatcher.dispatch(new UpdateHabitAction(updatedHabit));
                                                    }
                                                    
                                                });
                        
                                        },
                                        () => { dispatcher.dispatch(new SelectHabitAction(habits[index])); },
                                        habits[index].Selected);
                                }
                                else
                                {
                                    return new AddHabit(habitWidth, () =>
                                    {
                                        var habit = new Habit()
                                        {
                                            Title = string.Empty
                                        };

                                        var habitPage = new HabitModifier(habit,()=>{});
                                        Navigator.push(context,
                                                new MaterialPageRoute(builder: (context2 => habitPage)))
                                            .Then(result =>
                                            {
                                                var newHabit = result as Habit;

                                                if (newHabit.IsNotNull() && newHabit.Title.IsNotNullAndEmpty())
                                                {
                                                    dispatcher.dispatch(new AddHabitAction(newHabit));
                                                }
                                            });
                                    });
                                }
                            }
                        );
                    })
            );
        }
    }
}