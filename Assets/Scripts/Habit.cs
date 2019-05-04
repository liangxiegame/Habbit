using System;
using Unity.UIWidgets.widgets;

namespace HabitApp
{
    public class Habit : StatelessWidget
    {
        private string mHabit;

        private Action mOnClick;

        public Habit(string habit, Action onClick)
        {
            mHabit = habit;
            mOnClick = onClick;
        }

        public override Widget build(BuildContext context)
        {
            return new GestureDetector(
                child: new Text(mHabit),
                onTap: () => { mOnClick(); });
        }
    }
}