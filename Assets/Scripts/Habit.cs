using System;
using Unity.UIWidgets.widgets;

namespace HabitApp
{
    public class Habit : StatelessWidget
    {
        private HabitData mHabit;

        private Action mOnClick;

        public Habit(HabitData habit, Action onClick)
        {
            mHabit = habit;
            mOnClick = onClick;
        }

        public override Widget build(BuildContext context)
        {
            return new GestureDetector(
                child: new Text(mHabit.Title),
                onTap: () => { mOnClick(); });
        }
    }
}